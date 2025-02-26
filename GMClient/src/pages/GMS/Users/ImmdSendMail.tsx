import { ChangeEvent, ReactElement, useEffect, useRef, useState, useCallback } from 'react';
import { useAppDispatch, useAppSelector } from "@hooks/index";
import { GetServerSideProps } from 'next';
import { dayjs, timezoneName } from '@helpers/localizedDayjs';
import { GameAPI } from '@ngeldata/apis/gameAPI';
import * as layoutsActions from '@store/reducers/layouts';
import { sendGameMailReq } from 'src/ngel/data/hubs/GMServer/reducer';
import { Errors } from '@ngel/data/autoErrors';
import { Models } from '@ngel/data/models';
import deepmerge from 'deepmerge';
import { useRouter } from 'next/router';
import commonUIStyles from '@styles/ui/common.module.sass';
import styles from '@styles/pages/GMS/Users/ImmdSendMail.module.sass';
import { TableContainer, SelectChangeEvent } from '@mui/material';
import isEmpty from 'lodash/isEmpty';
import dynamic from 'next/dynamic';
import { EItemType, EMailType, ERewardType } from '@ngel/data/models/lobby';
import { GameAPIModels } from '@ngel/data/models/gameAPIModels';
import { Helpers } from '@helpers/index';

const Layout = dynamic(() => import('@components/layouts'), { ssr: false });
const ThemeLayout = dynamic(() => import('@components/layouts/theme'), { ssr: false });
const ManageLayout = dynamic(() => import('@components/layouts/manage'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const FormControlLabel = dynamic(() => import('@mui/material/FormControlLabel'), { ssr: false });
const InputLabel = dynamic(() => import('@mui/material/InputLabel'), { ssr: false });
const TextField = dynamic(() => import('@mui/material/TextField'), { ssr: false });
const Radio = dynamic(() => import('@mui/material/Radio'), { ssr: false })
const RadioGroup = dynamic(() => import('@mui/material/RadioGroup'), { ssr: false })
const Select = dynamic(() => import('@mui/material/Select'), { ssr: false });
const MenuItem = dynamic(() => import('@mui/material/MenuItem'), { ssr: false });
const Divider = dynamic(() => import('@mui/material/Divider'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const List = dynamic(() => import('@mui/material/List'), { ssr: false });
const ListItem = dynamic(() => import('@mui/material/ListItem'), { ssr: false });
const Autocomplete = dynamic(() => import('@mui/material/Autocomplete'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });

enum SearchType {
    UID = 0,
    MemberNo = 1
}

interface SearchProps {
    memberNoProp: string;
    uidProp: string;
    nickProp: string;
}
class ItemInput {
    id: number;
    name: string;
    count: number;
    rewardType: ERewardType;

    constructor(id?: number, name?: string, count?: number, type?: ERewardType) {
        this.id = id ?? 0;
        this.name = name ?? "";
        this.count = count ?? 0;
        this.rewardType = type ?? ERewardType.None;
    }

    clone = () => new ItemInput(this.id, this.name, this.count, this.rewardType);
    toMailInputItem = () => {
        var item = new Models.MailInputItem();
        item.id = this.id;
        item.rewardType = this.rewardType;
        item.name = this.name;
        item.count = this.count;
        return item;
    }
}

class MailInput {
    title: string;
    message: string;
    items: ItemInput[];
    isBM: boolean;
    mailType: EMailType;
    expireAt: number;

    constructor(title?: string, message?: string, items?: ItemInput[], isBM?: boolean, mailType?: EMailType, expireAt?: number) {
        this.title = title ?? "";
        this.message = message ?? "";
        this.items = items ?? [];
        this.isBM = isBM ?? false;
        this.mailType = mailType ?? EMailType.OpMail;
        this.expireAt = expireAt ?? (this.isBM ? 0 : 1);
    }

    clone = () => new MailInput(this.title, this.message, this.items, this.isBM, this.mailType, this.expireAt);
    toMailInput = () => {
        var mail = new Models.MailInput();
        mail.mailtype = this.mailType;
        mail.title = this.title;
        mail.message = this.message;
        mail.isBM = this.isBM;
        mail.expireAt = this.expireAt;
        this.items.map(_ => mail.items.push(_.toMailInputItem()));
        return mail;
    }
}

function Page({ memberNoProp, uidProp, nickProp }: SearchProps) {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const tables = useAppSelector(state => state.tables);
    const GMServerHub = useAppSelector(state => state.GMServerHub);
    const dispatch = useAppDispatch();
    const firstRender = useRef(true);
    const router = useRouter();
    const [itemLimit, setItemLimit] = useState<number>(0);
    const [searchType, setSearchType] = useState<SearchType>(SearchType.UID);
    const [userUIDStrings, setUserUIDStrings] = useState<string>("");
    const [items, setItems] = useState<ItemInput[]>([]);
    const [userAccounts, setUserAccounts] = useState<Map<number, Models.UserAccount | null>>(new Map());
    const [mailInput, setMailInput] = useState<MailInput>(new MailInput());

    let mailExpireTime = 720;
    var mailExpireTimeDefine = tables.globalDefineDataTable.find(_ => _.ID == "MailExpireTime");
    if (mailExpireTimeDefine)
        mailExpireTime = Helpers.getIntFromStringArray(mailExpireTimeDefine.Value, 0, 720);

    var itemLimitDefine = tables.globalDefineDataTable.find(_ => _.ID == "MailLimit_PackageItem");

    useEffect(() => {
        if (firstRender.current) {
            if (itemLimitDefine)
                setItemLimit(Helpers.getIntFromStringArray(itemLimitDefine.Value, 0));
        }

    }, [firstRender, tables, setItemLimit, itemLimitDefine]);

    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [firstRender]);
    //#endregion

    useEffect(() => {
        var itemOptions: ItemInput[] = [];

        for (let i = 0; i < tables.skinDataTable.length; i++) {
            let costume = tables.skinDataTable[i];
            itemOptions.push(new ItemInput(costume.ID, costume.NameStringWithID, 0, ERewardType.Costume));
        }

        const acceptItemType = [
            EItemType.Gem, EItemType.Recipe, EItemType.Use, EItemType.Dye, EItemType.Accessory_Head, EItemType.Accessory_Face,
            EItemType.Accessory_Back, EItemType.Accessory_Weapon, EItemType.Accessory_Pelvis, EItemType.Vehicle, EItemType.PetEgg,
            EItemType.Box, EItemType.PetLike, EItemType.Vehicle, EItemType.Ticket
        ];
        for (let i = 0; i < tables.itemTable.length; i++) {
            let item = tables.itemTable[i];

            if (!acceptItemType.includes(item.ItemType)) continue;

            switch (item.ItemType) {
                case EItemType.Accessory_Head:
                case EItemType.Accessory_Face:
                case EItemType.Accessory_Pelvis:
                case EItemType.Accessory_Back:
                case EItemType.Accessory_Weapon:
                    itemOptions.push(new ItemInput(item.ID, item.NameStringWithID, 0, ERewardType.Accessory));
                    break;

                case EItemType.Vehicle:
                    itemOptions.push(new ItemInput(item.ID, item.NameStringWithID, 0, ERewardType.Vehicle));
                    break;

                default:
                    itemOptions.push(new ItemInput(item.ID, item.NameStringWithID, 0, ERewardType.Item));
            }
        }

        for (let i = 0; i < tables.assetDataTable.length; i++) {
            let asset = tables.assetDataTable[i];
            //itemOptions.push(new ItemInput(asset.ID, `${asset.NameString}(${asset.ItemID})`, 0, ERewardType.Asset));
            itemOptions.push(new ItemInput(asset.ID, asset.NameStringWithID, 0, ERewardType.Asset));
        }
        itemOptions.splice(0, 0, new ItemInput(0, "아이템을 선택해 주세요.", 0, ERewardType.None));

        setItems(itemOptions);
    }, [tables, setItems]);

    const onChangeUID = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setUserUIDStrings(e.target.value ? e.target.value.replace(/(?<=[0-9])[\s\t ]+/g, ',') : "");
    }, [setUserUIDStrings]);

    const onChangeTitle = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        let text = e.target.value ?? "";
        if (text.length >= 40) text = text.substring(0, 40);

        setMailInput(prev => {
            prev.title = text ?? "";
            return prev.clone();
        });
    }, [setMailInput]);

    const onChangeMessage = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        let text = e.target.value ?? "";
        if (text.length >= 1000) text = text.substring(0, 1000);

        setMailInput(prev => {
            prev.message = text ?? "";
            return prev.clone();
        });
    }, [setMailInput]);

    const onChangeMailType = useCallback((e) => {
        let mailType: EMailType = EMailType.SystemMail;
        if (!e?.target?.value) return;
        try {
            mailType = isNaN(parseInt(e.target.value)) ? EMailType.SystemMail : parseInt(e.target.value);
        }
        catch (err) {
            console.log(err);
        }
        setMailInput(prev => {
            prev.mailType = mailType;
            return prev.clone();
        });
    }, [setMailInput]);

    const onChangeIsBM = useCallback((e) => {
        setMailInput(prev => {
            prev.isBM = e.target.value == 0 ? false : true;
            if (prev.isBM) {
                prev.expireAt = 0;
            } else if (prev.expireAt < 1) {
                prev.expireAt = 1;
            }

            return prev.clone();
        });
    }, [setMailInput]);

    const onChangeRewardType = useCallback((e, v: ERewardType, index: number) => {
        if (!e?.target?.value) return;

        setMailInput(prev => {
            if (!prev.items[index]) {
                return prev;
            }

            prev.items[index].id = 0;
            prev.items[index].name = "";
            return prev.clone();
        });

        let rewardType: ERewardType = ERewardType.None;
        try {
            rewardType = isNaN(parseInt(e.target.value)) ? ERewardType.None : parseInt(e.target.value);
        }
        catch (err) {
            console.log(err);
        }
        setMailInput(prev => {
            // prev.rewardTypes[index] = rewardType;
            prev.items[index].rewardType = rewardType;
            return prev.clone();
        });
    }, [setMailInput]);

    const onChangeExpireAt = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        var value = 0;
        if (!isEmpty(e.target.value.trim()) && !e.target.value.trim().match(/[^\d]/g) && !isNaN(parseInt(e.target.value.trim()))) {
            value = parseInt(e.target.value.trim());
        }

        if (value > mailExpireTime) {
            alert(`만료시간 설정은 ${mailExpireTime} 시간까지 가능합니다.`);
            return;
        }

        setMailInput(prev => {
            prev.expireAt = value;
            return prev.clone();
        });
    }, [setMailInput, mailExpireTime]);

    const onChangeItem = useCallback((e, v: ItemInput, index: number) => {
        if (!v || 1 > v.id) {
            return;
        }

        setMailInput(prev => {
            if (!prev.items[index]) {
                return prev;
            }

            prev.items[index].id = v.id;
            prev.items[index].name = v.name;
            prev.items[index].rewardType = v.rewardType;
            return prev.clone();
        });
    }, [setMailInput]);

    const onChangeItemCount = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>, index: number) => {
        var count = 0;
        if (!isEmpty(e.target.value.trim()) && !e.target.value.trim().match(/[^\d]/g) && !isNaN(parseInt(e.target.value.trim()))) {
            count = parseInt(e.target.value.trim());
        }

        setMailInput(prev => {
            if (!prev.items[index]) {
                return prev;
            }

            prev.items[index].count = count;
            return prev.clone();
        });
    }, [setMailInput]);

    const onChangeSearchType = useCallback((e: SelectChangeEvent<unknown>) => {
        let value: SearchType.UID = SearchType.UID;
        if (e && e.target && e.target.value) {
            try {
                value = isNaN(parseInt(e.target.value.toString())) ? SearchType.UID : parseInt(e.target.value.toString());
            } catch (error) {
                if ("production" != process.env.NODE_ENV) {
                    console.log(error);
                }
            }
        }

        setSearchType(value);

    }, [setSearchType]);

    const onAddItem = useCallback(() => {
        setMailInput(prev => {
            if ((itemLimit - 1) < prev.items.length) {
                alert(`한번에 발송가능한 아이템은 ${itemLimit}개 입니다.`);
                return prev;
            }
            prev.items.push(new ItemInput());
            return prev.clone();
            // return prev;
        });
    }, [setMailInput, itemLimit]);

    const onRemoveItem = useCallback(() => {
        setMailInput(prev => {
            if (1 > prev.items.length) {
                alert("삭제할 아이템이 없습니다.");
                return prev;
            }
            prev.items.pop();
            return prev.clone();
        });
    }, [setMailInput]);

    const onSearch = useCallback(async () => {
        setUserAccounts(new Map());
        setMailInput(new MailInput());
        if (!userUIDStrings) {
            alert("우편을 발송을 사용자의 UID를 입력해 주세요.");
            return;
        }

        var userUIDStringArray = userUIDStrings.trim().replace(/,$/, '').split(",");

        if (1 > userUIDStringArray.length) {
            alert("우편을 발송을 사용자의 UID를 입력해 주세요.");
            return;
        }

        const uidArray: number[] = [];

        for (var i = 0; i < userUIDStringArray.length; i++) {
            var uidString = userUIDStringArray[i].trim();

            if (uidString.match(/[^\d]/g)) {
                alert("UID는 숫자로 입력해주세요.");
                return;
            }

            var uid = parseInt(uidString);

            if (isNaN(uid)) {
                alert("UID는 숫자로 입력해주세요.");
                return;
            }

            if (uidArray.includes(uid)) {
                alert(`'${uid}' UID가 중복입력 됐습니다.`);
                return;
            }

            uidArray.push(uid);
        }

        if (1 > uidArray.length) {
            alert("우편을 발송할 UID정보가 없습니다.");
            return;
        }


        dispatch(layoutsActions.startLoadingMessage("우편을 발송할 사용자 정보를 검색중입니다."));
        var response: GameAPIModels.GameSearchUserAccountsResponses;
        switch (searchType) {
            case SearchType.MemberNo:
                response = await GameAPI.SearchUserAccountsByMemberNoAsync({ memberNos: uidArray });
                break;
            case SearchType.UID:
            default:
                response = await GameAPI.SearchUserAccountsAsync({ userUIDs: uidArray });
                break;

        }
        if (!response.result) {
            switch (response.error) {
                case Errors.SearchUserAccounts_UIDRequired:
                    alert("우편을 발송할 UID정보가 없습니다.");
                    return;

                case Errors.SearchUserAccounts_NotFoundUserAccount:
                    alert(`${response.errorIndex + 1}번째 입력한 UID의 사용자를 찾을 수 없습니다.`);
                    return;
            }
        } else {
            setUserAccounts(prev => deepmerge(new Map(), response.userAccounts));
        }
        dispatch(layoutsActions.stopLoading());
    }, [dispatch, userUIDStrings, setUserAccounts, searchType]);

    const onSend = useCallback(async () => {
        if (1 > Object.keys(userAccounts).length) {
            alert("우편을 발송할 사용자를 먼저 검색해주세요.");
        }

        const uids: number[] = [];
        for (var i = 0; i < Object.keys(userAccounts).length; i++) {
            var uid = Object.keys(userAccounts)[i];
            var userAccount = userAccounts[uid];
            if (userAccount) {
                uids.push(userAccount.UID);
            }
        }

        if (isEmpty(mailInput.title)) {
            alert("우편 제목을 입력해 주세요.");
            return;
        }

        if (isEmpty(mailInput.message)) {
            alert("우편 내용을 입력해 주세요.");
            return;
        }

        if (false == mailInput.isBM && mailInput.expireAt < 1) {
            alert("만료시간을 입력해 주세요.");
            return;
        }

        if (mailExpireTime < mailInput.expireAt) {
            alert(`만료시간은 ${mailExpireTime} 이하로 설정해 주세요.`);
            return;
        }

        if (0 < mailInput.items.length) {
            for (var i = 0; i < mailInput.items.length; i++) {
                var item = mailInput.items[i];
                if (1 > item.id) {
                    alert(`${i + 1}번째 아이템을 선택해주세요.`);
                    return;
                }

                if (1 > item.count) {
                    alert(`${i + 1}번째 아이템의 수량을 입력해주세요.`);
                    return;
                }
            }
        }

        if (false == confirm("우편을 발송하시겠습니까?"))
            return;

        const datasTrim = mailInput;
        datasTrim.title = datasTrim.title.trim(); datasTrim.message = datasTrim.message.trim();
        dispatch(sendGameMailReq({ userUIDs: uids, mailInput: datasTrim.toMailInput() }));
        alert("우편을 발송했습니다.");

    }, [userAccounts, mailInput, dispatch, mailExpireTime]);

    const contents = useCallback((): ReactElement[] => {
        var result: ReactElement[] = [];

        if (0 < Object.keys(userAccounts).length) {
            for (var i = 0; i < Object.keys(userAccounts).length; i++) {
                var uid = Object.keys(userAccounts)[i];
                var userAccount = userAccounts[uid];
                result.push(
                    <BorderedTableRow key={i}>
                        <TableCell>{uid}</TableCell>
                        {
                            userAccount
                                ?
                                <>
                                    <TableCell>{userAccount.Nick?.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                                    <TableCell>{userAccount.UserLevel?.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                                    <TableCell>{dayjs(userAccount.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss')}</TableCell>
                                    <TableCell>{dayjs(userAccount.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss')}</TableCell>
                                    <TableCell>{dayjs(userAccount.LastLogoutAt).tz().format('YYYY-MM-DD HH:mm:ss')}</TableCell>
                                </>
                                :
                                <TableCell colSpan={5}>사용자 정보 없음</TableCell>
                        }

                    </BorderedTableRow>
                );
            }
        }
        return result;
    }, [userAccounts]);

    const itemSelectList = useCallback(() => {
        let result: ReactElement = <></>;
        if (0 < mailInput.items.length) {
            const list: ReactElement[] = [];
            for (let i = 0; i < mailInput.items.length; i++) {
                const item = mailInput.items[i];
                const types = item.rewardType;
                // const types = mailInput.rewardTypes[i];

                const opts = items.filter(element => element.rewardType === types || element.rewardType === ERewardType.None);
                list.push(
                    <ListItem key={i}>
                        <Typography sx={{ pl: 1, pr: 1 }} variant='body1'>{i + 1}</Typography>
                        <FormControl fullWidth size='small' sx={{ marginLeft: 1 }}>
                            <InputLabel id={`select-rewardType-${i + 1}`}>보상 종류</InputLabel>
                            <Select labelId={`select-rewardType-${i + 1}`} id={`select-rewardType-${i + 1}`} value={types} onChange={(e, v) => onChangeRewardType(e, v as ERewardType, i)} label="보상 종류">
                                <MenuItem value={ERewardType.None}>{ERewardType[0].toString().replaceAll(/(?<=.{2})./g, '*')}</MenuItem>
                                <MenuItem value={ERewardType.Costume}>{ERewardType[2].toString().replaceAll(/(?<=.{2})./g, '*')}</MenuItem>
                                <MenuItem value={ERewardType.Item}>{ERewardType[3].toString().replaceAll(/(?<=.{2})./g, '*')}</MenuItem>
                                <MenuItem value={ERewardType.Asset}>{ERewardType[4].toString().replaceAll(/(?<=.{2})./g, '*')}</MenuItem>
                                <MenuItem value={ERewardType.Accessory}>{ERewardType[7].toString().replaceAll(/(?<=.{2})./g, '*')}</MenuItem>
                                <MenuItem value={ERewardType.Vehicle}>{ERewardType[8].toString().replaceAll(/(?<=.{2})./g, '*')}</MenuItem>
                            </Select>
                        </FormControl>
                        <FormControl fullWidth sx={{ marginLeft: 1 }}>
                            <Autocomplete id={`item_${(i + 1)}`} options={opts} size='small' onChange={(e, v, r) => onChangeItem(e, v as ItemInput, i)} getOptionLabel={(option) => (option as ItemInput).name.toString().replaceAll(/(?<=.{2})./g, '*')} isOptionEqualToValue={(option, value) => (option as ItemInput).id == (value as ItemInput).id} value={item} renderInput={(params) => <TextField {...params} label={`아이템 ${(i + 1)}`} />} />
                        </FormControl>
                        <FormControl fullWidth sx={{ marginLeft: 1 }}>
                            <TextField className={commonUIStyles.input} variant='outlined' value={item.count} size='small' placeholder={`아이템 ${(i + 1)} 수량`} onChange={e => onChangeItemCount(e, i)} />
                        </FormControl>
                    </ListItem>
                );
            }
            result = <List>{list}</List>;
        } else {
            result = <List><ListItem><Typography key={1}>발송할 아이템을 추가해 주세요.</Typography></ListItem></List>;
        }

        return result;
    }, [mailInput.items, items, onChangeItem, onChangeItemCount, onChangeRewardType]);

    return (
        <Box sx={{ pt: 3, pb: 3 }}>
            <Box>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* AccountID는 숫자로 검색하면 됩니다. (ex: 101, 102, 1001)</Typography>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* 유저 검색 후 우편발송 가능.</Typography>
            </Box>
            <Box>
                <Grid container alignItems="center" justifyContent="center">
                    <Grid item xs={12} sx={{ padding: "0 5px" }}>
                        <Select className={commonUIStyles.select} sx={{ fontSize: { xs: 10, sm: 12, md: 16 }, mb: 1 }} value={searchType} size="small" variant='standard' onChange={onChangeSearchType} >
                            <MenuItem value={SearchType.UID}>AccountID</MenuItem>
                            <MenuItem value={SearchType.MemberNo}>MemberNo</MenuItem>
                        </Select>
                        <FormControl fullWidth variant='outlined'>
                            <TextField
                                id="outlined-multiline-static"
                                label={searchType == SearchType.UID ? "사용자 AccountID" : "Member No"}
                                multiline
                                rows={4}
                                placeholder="ex) 101, 202, 203"
                                onChange={e => onChangeUID(e)}
                                value={userUIDStrings}
                            />
                        </FormControl>
                    </Grid>
                </Grid>
                <Grid sx={{ padding: '10px 0 20px', textAlign: 'center' }}>
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onSearch}>검색</Button>
                </Grid>
            </Box>
            {
                0 < Object.keys(userAccounts).length && mailInput
                    ?
                    <>
                        <Divider sx={{ margin: "20px 0" }} variant="middle" />
                        <Grid sx={{ padding: '20px 0 10px', textAlign: 'left' }}>
                            <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSend}>발송</Button>
                        </Grid>
                        <Box>
                            <TableContainer component={Paper} elevation={4} sx={{ marginBottom: 3 }}>
                                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>우편 내용</Typography>
                                </Toolbar>
                                <Table className={`${styles.statesTable} ${commonUIStyles.ellipsisTable}`} stickyHeader aria-label="sticky table">
                                    <TableBody>
                                        <BorderedTableRow>
                                            <TableCell className={styles.mailInputTH} component="th" scope="row">
                                                <Typography variant="subtitle2">제목</Typography>
                                                <Typography>{`( ${mailInput?.title.length} / 40자 )`}</Typography>
                                            </TableCell>
                                            <TableCell className={styles.mailInputTD}>
                                                <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={mailInput.title} size='small' placeholder='제목' onChange={e => onChangeTitle(e)} />
                                            </TableCell>
                                        </BorderedTableRow>
                                        <BorderedTableRow>
                                            <TableCell className={styles.mailInputTH} component="th" scope="row">
                                                <Typography variant="subtitle2">내용</Typography>
                                                <Typography>{`( ${mailInput?.message.length} / 1000자 )`}</Typography>
                                            </TableCell>
                                            <TableCell className={styles.mailInputTD}>
                                                <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={mailInput.message} size='small' placeholder='내용' multiline onChange={e => onChangeMessage(e)} />
                                            </TableCell>
                                        </BorderedTableRow>
                                        <BorderedTableRow>
                                            <TableCell className={styles.mailInputTH} component="th" scope="row"><Typography variant="subtitle2">mailtype</Typography></TableCell>
                                            <TableCell className={styles.mailInputTD}>
                                                <FormControl fullWidth size="small">
                                                    <RadioGroup aria-label="mailType" name="select-mailType" value={mailInput?.mailType ?? EMailType.SystemMail} onChange={(e) => onChangeMailType(e)} style={{ display: 'flex', flexDirection: 'row' }}>
                                                        {/* <FormControlLabel value={EMailType.None} control={<Radio />} label={EMailType[0]}/> */}
                                                        <FormControlLabel value={EMailType.SystemMail} control={<Radio />} label={EMailType[1]} />
                                                        <FormControlLabel value={EMailType.OpMail} control={<Radio />} label={EMailType[2]} />
                                                    </RadioGroup>
                                                </FormControl>
                                            </TableCell>
                                        </BorderedTableRow>
                                        <BorderedTableRow>
                                            <TableCell className={styles.mailInputTH} component="th" scope="row"><Typography variant="subtitle2">isBM</Typography></TableCell>
                                            <TableCell className={styles.mailInputTD}>
                                                <FormControl fullWidth size="small">
                                                    <RadioGroup aria-label="isBM" name="select-isBM" value={mailInput.isBM == false ? 0 : 1} onChange={(e) => onChangeIsBM(e)}
                                                        style={{ display: 'flex', flexDirection: 'row' }}>
                                                        <FormControlLabel value={0} control={<Radio />} label={`False`} />
                                                        <FormControlLabel value={1} control={<Radio />} label={`True`} />
                                                    </RadioGroup>
                                                </FormControl>
                                            </TableCell>
                                        </BorderedTableRow>
                                        <BorderedTableRow>
                                            <TableCell className={styles.mailInputTH} component="th" scope="row"><Typography variant="subtitle2">만료시간{mailInput.isBM ? '' : (<><br />(1 ~ {mailExpireTime})</>)}</Typography></TableCell>
                                            <TableCell className={styles.mailInputTD}>
                                                <TextField className={commonUIStyles.input} disabled={mailInput.isBM} variant='outlined' fullWidth value={mailInput.expireAt} size='small' placeholder='ExpireAt' multiline onChange={e => onChangeExpireAt(e)} />
                                            </TableCell>
                                        </BorderedTableRow>
                                        <BorderedTableRow>
                                            <TableCell className={styles.mailInputTH} component="th" scope="row">
                                                <Typography variant="subtitle2">아이템</Typography>
                                                <Box>
                                                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonTiny}`} onClick={onAddItem} sx={{ backgroundColor: "white" }} variant="outlined" color="inherit">추가</Button>
                                                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonTiny}`} onClick={onRemoveItem} sx={{ backgroundColor: "white", marginLeft: 1 }} variant="outlined" color="error">삭제</Button>
                                                </Box>
                                            </TableCell>
                                            <TableCell className={styles.mailInputTD}>
                                                {itemSelectList()}
                                            </TableCell>
                                        </BorderedTableRow>
                                    </TableBody>
                                </Table>
                            </TableContainer>
                        </Box>
                        <Grid sx={{ padding: '0 0 10px', textAlign: 'left' }}>
                            <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSend}>발송</Button>
                        </Grid>
                        <Divider sx={{ margin: "20px 0" }} variant="middle" />
                        <Box sx={{ mb: '100px' }}>
                            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 400 }}>
                                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>우편발송 대상</Typography>
                                </Toolbar>
                                <Table className={`${styles.statesTable} ${commonUIStyles.ellipsisTable}`} stickyHeader aria-label="sticky table">
                                    <TableHead>
                                        <BorderedTableRow>
                                            <TableCell>UID</TableCell>
                                            <TableCell>닉네임</TableCell>
                                            <TableCell>레벨</TableCell>
                                            <TableCell>생성일시({timezoneName})</TableCell>
                                            <TableCell>갱신일시({timezoneName})</TableCell>
                                            <TableCell>로그아웃 시각({timezoneName})</TableCell>
                                        </BorderedTableRow>
                                    </TableHead>
                                    <TableBody>
                                        {contents()}
                                    </TableBody>
                                </Table>
                            </TableContainer>
                        </Box>
                    </>
                    :
                    <></>
            }
        </Box>
    );
};

Page.getLayout = function getLayout(page: ReactElement) {
    return (
        <Layout>
            <ThemeLayout>
                <ManageLayout>{page}</ManageLayout>
            </ThemeLayout>
        </Layout>
    );
}

export const getServerSideProps: GetServerSideProps = async (context) => {
    const { query } = context;
    let nick: string = "";
    let uid: string = "";
    let memberNo: string = "";

    if (query) {
        if (query.nick)
            nick = query.nick.toString();

        if (query.uid)
            uid = query.uid.toString();

        if (query.memberNo)
            memberNo = query.memberNo.toString();
    }

    return {
        props: {
            nickProp: nick,
            uidProp: uid,
            memberNoProp: memberNo
        }
    };
}

export default Page;