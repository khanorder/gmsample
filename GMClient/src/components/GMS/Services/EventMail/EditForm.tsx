import { ReactElement, useCallback, useEffect, useRef, useState, ChangeEvent } from "react";
import commonUIStyles from '@styles/ui/common.module.sass';
import styles from '@styles/pages/GMS/Manages/users.module.sass';
import { useAppDispatch, useAppSelector } from "@hooks/index";
import { TableContainer, styled, Toolbar } from '@mui/material';
import { Errors } from '@ngel/data/autoErrors';
import dynamic from 'next/dynamic';
import deepmerge from "deepmerge";
import { GameAPI } from "@ngel/data/apis/gameAPI";
import { GameAPIModels } from '@ngel/data/models/gameAPIModels';
import * as layoutsActions from '@store/reducers/layouts';
import { Models } from "@ngel/data/models";
import { EItemType, EMailType, ERewardType } from '@ngel/data/models/lobby';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import { updateEventMailReq } from 'src/ngel/data/hubs/GMServer/reducer';
import { useRouter } from "next/router";
import isEmpty from 'lodash/isEmpty';
import { Helpers } from "@helpers/index";

const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const FormControlLabel = dynamic(() => import('@mui/material/FormControlLabel'), { ssr: false });
const InputLabel = dynamic(() => import('@mui/material/InputLabel'), { ssr: false });
const Select = dynamic(() => import('@mui/material/Select'), { ssr: false });
const MenuItem = dynamic(() => import('@mui/material/MenuItem'), { ssr: false });
const TextField = dynamic(() => import('@mui/material/TextField'), { ssr: false });
const Autocomplete = dynamic(() => import('@mui/material/Autocomplete'), { ssr: false });
const ListItem = dynamic(() => import('@mui/material/ListItem'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const DateTimePicker = dynamic(() => import('@components/ui/DateTimePicker'), { ssr: false });
const Radio = dynamic(() => import('@mui/material/Radio'), { ssr: false })
const RadioGroup = dynamic(() => import('@mui/material/RadioGroup'), { ssr: false })

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

interface EventMailFormProps {
    pageProp: number;
    mode: 'edit';
    id: string;
}

const EventMailEditForm = ({ pageProp, mode, id }: EventMailFormProps): ReactElement => {
    const tables = useAppSelector(state => state.tables);
    const GMServerHub = useAppSelector(state => state.GMServerHub);
    const dispatch = useAppDispatch();
    const firstRender = useRef(true);
    const router = useRouter();

    const [datas, setDatas] = useState<Models.EventMail>(new Models.EventMail);
    const [items, setItems] = useState<ItemInput[]>([]);
    const [itemLimit, setItemLimit] = useState<number>(0);

    let mailExpireTime = 720;
    var mailExpireTimeDefine = tables.globalDefineDataTable.find(_ => _.ID == "MailExpireTime");
    if (mailExpireTimeDefine)
        mailExpireTime = Helpers.getIntFromStringArray(mailExpireTimeDefine.Value, 0, 720);

    var itemLimitDefine = tables.globalDefineDataTable.find(_ => _.ID == "MailLimit_PackageItem");

    const loadData = useCallback(async () => {
        if (firstRender.current) {
            dispatch(layoutsActions.startLoadingMessage("이벤트 우편 정보를 불러오는 중입니다."));

            const parameter = new GameAPIModels.GameEventMailParameters();
            parameter.id = parseInt(id) ?? 0;
            const response = await GameAPI.EventMailAsync(parameter);
            if (!response.result) {
                if ("production" != process.env.NODE_ENV)
                    console.log(response.error);

                dispatch(layoutsActions.stopLoading());
                return;
            }

            if (null != response.eventMail && response.eventMail) {
                setDatas(new Models.EventMail(response.eventMail));
            }
            firstRender.current = false;

            dispatch(layoutsActions.stopLoading());
        }
    }, [dispatch, id, setDatas]);

    useEffect(() => {
        if (firstRender.current) {
            if (itemLimitDefine)
                setItemLimit(Helpers.getIntFromStringArray(itemLimitDefine.Value, 0));

            loadData();
        }

    }, [firstRender, tables, setItemLimit, setDatas, loadData, itemLimitDefine]);

    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [firstRender]);
    //#endregion

    useEffect(() => {
        let itemOptions: ItemInput[] = [];

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
            itemOptions.push(new ItemInput(asset.ID, asset.NameStringWithID, 0, ERewardType.Asset));
        }
        itemOptions.splice(0, 0, new ItemInput(0, "아이템을 선택해 주세요.", 0, ERewardType.None));

        setItems(itemOptions);
    }, [tables, setItems]);


    const onChangeTitle = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        let text = e.target.value ?? "";
        if (text.length >= 40) text = text.substring(0, 40);

        setDatas(prev => {
            prev.Title = text ?? "";
            prev.isChanged = true;
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangeMessage = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        let text = e.target.value ?? "";
        if (text.length >= 1000) text = text.substring(0, 1000);

        setDatas(prev => {
            prev.Message = text ?? "";
            prev.isChanged = true;
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangeDate = useCallback((date: string | null, type: number) => {
        //type 0: StartDate, 1: EndDate
        setDatas(prev => {
            let dayjsDate: Dayjs | null = null;

            try {
                dayjsDate = dayjs.tz(date);
            } catch (error) {
                return prev;
            }

            switch (type) {
                case 0:
                    prev.StartTime = dayjsDate.toDate();
                    break;
                case 1:
                    prev.EndTime = dayjsDate.toDate();
                    break;
            }

            prev.isChanged = true;
            return deepmerge([], prev);
        })
    }, [setDatas]);

    const onChangeExpiredTime = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        var value = 0;
        if (!isEmpty(e.target.value.trim()) && !e.target.value.trim().match(/[^\d]/g) && !isNaN(parseInt(e.target.value.trim()))) {
            value = parseInt(e.target.value.trim());
        }

        if (1 > value || value > mailExpireTime) {
            alert(`만료시간 설정은 1 ~ ${mailExpireTime} 시간까지 가능합니다.`);
            return;
        }

        setDatas(prev => {
            prev.ExpireTime = value;
            prev.isChanged = true;
            return deepmerge([], prev);
        });
    }, [setDatas, mailExpireTime]);

    const onChangeMailType = useCallback((e) => {
        let mailType: EMailType = EMailType.OpMail;
        if (!e?.target?.value) return;
        try {
            mailType = isNaN(parseInt(e.target.value)) ? EMailType.SystemMail : parseInt(e.target.value);
        } catch (err) {
            console.log(err);
        }
        setDatas(prev => {
            prev.MailType = mailType;
            prev.isChanged = true;
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangeRewardType = useCallback((e, v: ERewardType, index: number) => {
        let rewardType: ERewardType = ERewardType.None;
        if (!e?.target?.value) return;
        try {
            rewardType = isNaN(parseInt(e.target.value)) ? ERewardType.None : parseInt(e.target.value);
        }
        catch (err) {
            console.log(err);
        }
        setDatas(prev => {
            if (prev.RewardList[index]) {
                prev.RewardList[index].RewardID = 0;
                prev.RewardList[index].RewardType = rewardType;
                prev.RewardList[index].RewardCount = 0;
            }
            else {
                return prev;
            }

            prev.isChanged = true;
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangeItem = useCallback((e, v: ItemInput, index: number) => {
        if (!v || 1 > v.id) {
            setDatas(prev => {
                if (prev.RewardList[index]) {
                    prev.RewardList[index].RewardID = 0;
                    prev.RewardList[index].RewardCount = 0;
                }
                else {
                    return prev;
                }

                prev.isChanged = true;
                return deepmerge([], prev);
            });
        }
        else {
            setDatas(prev => {
                if (prev.RewardList[index]) {
                    prev.RewardList[index].RewardID = v.id;
                    prev.RewardList[index].RewardType = v.rewardType;
                }
                else {
                    return prev;
                }
                prev.isChanged = true;
                return deepmerge([], prev);
            });
        }
    }, [setDatas]);

    const onChangeItemCount = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>, index: number) => {
        var count = 0;
        if (!isEmpty(e.target.value.trim()) && !e.target.value.trim().match(/[^\d]/g) && !isNaN(parseInt(e.target.value.trim()))) {
            count = parseInt(e.target.value.trim());
        }

        setDatas(prev => {
            if (prev.RewardList[index]) {
                prev.RewardList[index].RewardCount = count;
            }
            else {
                return prev;
            }
            prev.isChanged = true;
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onAddItemData = useCallback(() => {
        setDatas(prev => {
            if ((itemLimit - 1) < prev.RewardList.length) {
                alert(`한번에 발송가능한 아이템은 ${itemLimit}개 입니다.`);
                return prev;
            }
            prev.RewardList.push(new Models.MailReward());
            prev.isChanged = true;
            return deepmerge([], prev);
        })
    }, [setDatas, itemLimit]);

    const saveData = useCallback(async () => {
        if (!datas.isChanged) {
            alert("변경된 항목이 없습니다.")
            return;
        }

        if (!datas?.Title) {
            alert("제목을 입력해주세요.");
            return;
        }

        if (!datas?.Message) {
            alert("내용을 입력해주세요.");
            return;
        }

        if (datas?.EndTime?.getTime() <= datas?.StartTime?.getTime()) {
            alert(`종료일이 시작일보다 같거나 이전입니다.`);
            return;
        }

        if (1 > datas?.ExpireTime) {
            alert("만료시간은 1 이상 정수로 입렵해주세요.");
            return;
        }

        if (datas?.ExpireTime > mailExpireTime) {
            alert(`만료시간은 ${mailExpireTime} 이하 정수로 입력해주세요.`);
            return;
        }

        if (!datas.RewardList || 1 > datas.RewardList.length) {
            alert(`하나 이상 보상을 추가해주세요.`);
            return;
        }

        for (let i = 0; i < datas?.RewardList.length; i++) {
            const item = datas?.RewardList[i];
            if (item.RewardType !== 0 && item.RewardID == 0) {
                alert(`보상 종류가 선택된 아이템${i + 1} 정보가 선택되지 않았습니다.`);
                return;
            }

            if (item.RewardType !== 0 && item.RewardID !== 0 && item.RewardCount < 1) {
                alert(`선택한 아이템${i + 1}의 수량이 0이거나 0보다 작습니다.`);
                return;
            }
        }

        if (!confirm("확인을 누르면 수정 사항이 저장됩니다.\n계속 하시겠습니까?")) {
            return;
        }

        const datasTrim = new Models.EventMail(datas);
        datasTrim.Title = datasTrim.Title.trim(); datasTrim.Message = datasTrim.Message.trim();

        dispatch(updateEventMailReq({ eventMail: datasTrim }));
    }, [datas, dispatch, mailExpireTime]);

    const selectRewardType = useCallback((rewardIndex: number): ReactElement => {

        const types = datas.RewardList[rewardIndex].RewardType;
        const opts = items.filter(element => element.rewardType === types || element.rewardType === ERewardType.None);
        const item = opts.find(element => element.id === datas.RewardList[rewardIndex].RewardID) ?? opts.find(element => element.id === 0);
        return (
            <>
                {
                    items.length > 0
                        ?
                        <>
                            <FormControl fullWidth size="small">
                                <InputLabel id={`label-rewardType${rewardIndex}`}>{`보상종류${rewardIndex}`}</InputLabel>
                                <Select labelId={`select-rewardType${rewardIndex}`} id={`select-rewardType${rewardIndex}`} value={datas.RewardList[rewardIndex].RewardType} onChange={(e, v) => onChangeRewardType(e, v as ERewardType, rewardIndex)} label={`보상종류${rewardIndex}`}>
                                    <MenuItem value={ERewardType.None}>{ERewardType[0].toString().replaceAll(/(?<=.{2})./g, '*')}</MenuItem>
                                    <MenuItem value={ERewardType.Costume}>{ERewardType[2].toString().replaceAll(/(?<=.{2})./g, '*')}</MenuItem>
                                    <MenuItem value={ERewardType.Item}>{ERewardType[3].toString().replaceAll(/(?<=.{2})./g, '*')}</MenuItem>
                                    <MenuItem value={ERewardType.Asset}>{ERewardType[4].toString().replaceAll(/(?<=.{2})./g, '*')}</MenuItem>
                                    <MenuItem value={ERewardType.Accessory}>{ERewardType[7].toString().replaceAll(/(?<=.{2})./g, '*')}</MenuItem>
                                    <MenuItem value={ERewardType.Vehicle}>{ERewardType[8].toString().replaceAll(/(?<=.{2})./g, '*')}</MenuItem>
                                </Select>
                            </FormControl>
                            <FormControl fullWidth sx={{ marginLeft: 1 }}>
                                <Autocomplete id={`item_${(rewardIndex)}`} options={opts} size='small' onChange={(e, v, r) => onChangeItem(e, v as ItemInput, rewardIndex)} getOptionLabel={(option) => (option as ItemInput).name.toString().replaceAll(/(?<=.{2})./g, '*')} isOptionEqualToValue={(option, value) => (option as ItemInput).id == (value as ItemInput).id} value={item} renderInput={(params) => <TextField {...params} label={`아이템 ${(rewardIndex)}`} />} />
                            </FormControl>
                            <FormControl fullWidth sx={{ marginLeft: 1 }}>
                                <TextField className={commonUIStyles.input} variant='outlined' value={datas.RewardList[rewardIndex].RewardCount}
                                    disabled={datas.RewardList[rewardIndex].RewardType === 0} size='small' placeholder={`아이템 ${(rewardIndex)} 수량`} onChange={e => onChangeItemCount(e, rewardIndex)} />
                            </FormControl>
                        </>
                        :
                        ""
                }
            </>
        )
    }, [items, datas, onChangeItem, onChangeItemCount, onChangeRewardType]);

    const contents = useCallback((): ReactElement => {
        let itemSelectList: ReactElement[] = [];
        for (let i = 0; i < datas.RewardList.length; i++) {
            itemSelectList.push(<ListItem key={i}>{selectRewardType(i)}</ListItem>);
        }
        let result: ReactElement =
            <TableBody>
                <BorderedTableRow>
                    <TableCell className={styles.mailInputTH} component="th" scope="row"><Typography variant="subtitle2">ID</Typography></TableCell>
                    <TableCell className={styles.mailInputTD} component="th" scope="row"><Typography variant="subtitle2">생성일시({timezoneName})</Typography></TableCell>
                    <TableCell className={styles.mailInputTD} component="th" scope="row"><Typography variant="subtitle2">수정일시({timezoneName})</Typography></TableCell>
                </BorderedTableRow>
                <BorderedTableRow>
                    <TableCell className={styles.mailInputTH}><Typography variant="subtitle2">{datas?.ID}</Typography></TableCell>
                    <TableCell className={styles.mailInputTD}><Typography variant="subtitle2">{datas?.CreateAt ? dayjs(datas?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</Typography></TableCell>
                    <TableCell className={styles.mailInputTD}><Typography variant="subtitle2">{datas?.UpdateAt ? dayjs(datas?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</Typography></TableCell>
                </BorderedTableRow>
                <BorderedTableRow>
                    <TableCell className={styles.mailInputTH} component="th" scope="row">
                        <Typography variant="subtitle2">제목</Typography>
                        <Typography>{`( ${datas?.Title.length} / 40자 )`}</Typography>
                    </TableCell>
                    <TableCell className={styles.mailInputTD} colSpan={2}>
                        <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={datas?.Title} size='small' placeholder='제목' onChange={e => onChangeTitle(e)} />
                    </TableCell>
                </BorderedTableRow>
                <BorderedTableRow>
                    <TableCell className={styles.mailInputTH} component="th" scope="row">
                        <Typography variant="subtitle2">내용</Typography>
                        <Typography>{`( ${datas?.Message.length} / 1000자 )`}</Typography>
                    </TableCell>
                    <TableCell className={styles.mailInputTD} colSpan={2}>
                        <TextField className={commonUIStyles.input} variant='outlined' fullWidth multiline value={datas?.Message} size='small' placeholder='내용' onChange={e => onChangeMessage(e)} />
                    </TableCell>
                </BorderedTableRow>
                <BorderedTableRow>
                    <TableCell component="th" scope="row">StartTime({timezoneName})</TableCell>
                    <TableCell colSpan={2}>
                        <DateTimePicker label={`StartTime(${timezoneName})`} enableTimePicker={true} format="YYYY-MM-DD HH:mm" value={dayjs.tz(datas?.StartTime).format("YYYY-MM-DD HH:mm:ss")} onChange={(date) => onChangeDate(date, 0)} />
                    </TableCell>
                </BorderedTableRow>
                <BorderedTableRow>
                    <TableCell component="th" scope="row">EndTime({timezoneName})</TableCell>
                    <TableCell colSpan={2}>
                        <DateTimePicker label={`EndTime(${timezoneName})`} enableTimePicker={true} format="YYYY-MM-DD HH:mm" value={dayjs.tz(datas?.EndTime).format("YYYY-MM-DD HH:mm:ss")} onChange={(date) => onChangeDate(date, 1)} />
                    </TableCell>
                </BorderedTableRow>
                <BorderedTableRow>
                    <TableCell component="th" scope="row">만료시간<br />(1 ~ {mailExpireTime})</TableCell>
                    <TableCell className={styles.mailInputTD} colSpan={2}>
                        <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={datas?.ExpireTime} size='small' placeholder='ExpiredTime' onChange={e => onChangeExpiredTime(e)} />
                    </TableCell>
                </BorderedTableRow>
                <BorderedTableRow>
                    <TableCell className={styles.mailInputTH} component="th" scope="row"><Typography variant="subtitle2">Mailtype</Typography></TableCell>
                    <TableCell className={styles.mailInputTD} colSpan={2}>
                        <FormControl fullWidth size="small">
                            <RadioGroup aria-label="MailType" name="select-mailType" value={datas?.MailType ?? EMailType.SystemMail} onChange={(e) => onChangeMailType(e)}
                                style={{ display: 'flex', flexDirection: 'row' }}>
                                {/* <FormControlLabel value={EMailType.None} control={<Radio />} label={EMailType[0]}/> */}
                                <FormControlLabel value={EMailType.SystemMail} control={<Radio />} label={EMailType[1]} />
                                <FormControlLabel value={EMailType.OpMail} control={<Radio />} label={EMailType[2]} />
                            </RadioGroup>
                        </FormControl>
                    </TableCell>
                </BorderedTableRow>
                <BorderedTableRow>
                    <TableCell component="th" scope="row">
                        <Box>아이템 리스트</Box>
                        <Box><Button className={`${commonUIStyles.button} ${commonUIStyles.buttonTiny}`} onClick={onAddItemData} sx={{ backgroundColor: "white" }} variant="outlined" color="inherit">추가</Button></Box>
                    </TableCell>
                    <TableCell colSpan={2}>
                        {itemSelectList}
                    </TableCell>
                </BorderedTableRow>
            </TableBody>;

        return result;
    }, [datas, onAddItemData, onChangeDate, onChangeExpiredTime, onChangeMailType, onChangeTitle, onChangeMessage, selectRewardType, mailExpireTime]);

    return (
        <Box sx={{ mt: 5, width: '100%' }}>
            <Grid container justifyContent='space-between' sx={{ margin: '20px 0 0' }}>
                <Grid item sx={{ padding: '10px 0 10px' }}>
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" sx={{ margin: 0.5 }} onClick={saveData}>저장</Button>
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" sx={{ margin: 0.5 }} onClick={() => router.back()}>취소</Button>
                </Grid>
            </Grid>
            <TableContainer component={Paper} elevation={4} sx={{ marginBottom: 10 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>이벤트 우편 발송 (기존 데이터 수정)</Typography>
                </Toolbar>
                <Table>
                    {contents()}
                </Table>
            </TableContainer>
        </Box>
    )
}

export default EventMailEditForm;