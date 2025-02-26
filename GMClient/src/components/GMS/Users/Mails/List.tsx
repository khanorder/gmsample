import { ReactElement, useCallback, useEffect, useRef, useState, ChangeEvent } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import * as layoutsActions from '@store/reducers/layouts';
import commonUIStyles from '@styles/ui/common.module.sass';
import mailStyles from '@styles/pages/GMS/Users/mails.module.sass';
import { TableContainer } from '@mui/material';
import dynamic from 'next/dynamic';
import { useAppDispatch } from '@hooks/index';
import { GameAPI } from '@ngel/data/apis/gameAPI';
import { Errors } from '@ngel/data/autoErrors';
import { useRouter } from 'next/router';
import { EMailStateType, EMailType } from '@ngel/data/models/lobby';
import isEmpty from 'lodash/isEmpty';

const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const InputLabel = dynamic(() => import('@mui/material/InputLabel'), { ssr: false });
const InputAdornment = dynamic(() => import('@mui/material/InputAdornment'), { ssr: false });
const OutlinedInput = dynamic(() => import('@mui/material/OutlinedInput'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Paging = dynamic(() => import('@components/ui/Paging'), { ssr: false });
const DateTimePicker = dynamic(() => import('@components/ui/DateTimePicker'), { ssr: false });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const IconButton = dynamic(() => import('@mui/material/IconButton'), { ssr: false });
const RemoveIcon = dynamic(() => import('@mui/icons-material/Clear'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const FormGroup = dynamic(() => import('@mui/material/FormGroup'), { ssr: false });
const FormControlLabel = dynamic(() => import('@mui/material/FormControlLabel'), { ssr: false });
const Checkbox = dynamic(() => import('@mui/material/Checkbox'), { ssr: false });

interface ListProps {
    page: number;
    searchUid: number;
    searchMemberNo: number;
    searchNick: string;
    startDate: string;
    endDate: string;
}

class SearchInput {
    searchUid?: number | null;
    searchMemberNo?: number | null;
    searchNick?: string | null;
    startDate?: Dayjs | null;
    endDate?: Dayjs | null;

    constructor(
        uid?: number | null, memberNo?: number | null, nick?: string | null, startDate?: Dayjs | null, endDate?: Dayjs | null
    ) {
        this.searchUid = uid;
        this.searchMemberNo = memberNo;
        this.searchNick = nick;
        this.startDate = startDate;
        this.endDate = endDate;
    }

    clone = () => new SearchInput(this.searchUid, this.searchMemberNo, this.searchNick, this.startDate, this.endDate);
}

const styles = {
    smallCell: {
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        minWidth: "108px",
        cursor: "pointer"
    },
    mediumCell: {
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        minWidth: "332.7px",
        cursor: "pointer"
    },
    rowStyle: {
        fontSize: 20
    }
}

const enum SearchType {
    uid = 0,
    memberNo = 1,
    nick = 2
}

const List = ({ page, searchUid, searchMemberNo, searchNick, startDate, endDate }: ListProps): ReactElement => {
    const dispatch = useAppDispatch();
    const firstRender = useRef(true);
    const router = useRouter();

    const [datas, setDatas] = useState<PaginatedList<Models.MailWithUser>>(new PaginatedList<Models.MailWithUser>([]));
    const [searchInput, setSearchInput] = useState<SearchInput>(new SearchInput());

    const loadDatas = useCallback(async () => {
        setDatas(new PaginatedList<Models.MailWithUser>([]));

        dispatch(layoutsActions.startLoadingMessage("우편 데이터를 불러오는 중입니다."));

        const response = await GameAPI.MailWithUsersAsync({
            startTime: startDate ? dayjs.tz(startDate).utc().format("YYYY-MM-DD HH:mm:ss") : dayjs.utc().format("YYYY-MM-DD HH:mm:ss"),
            endTime: endDate ? dayjs.tz(endDate).add(1, 'day').utc().format("YYYY-MM-DD HH:mm:ss") : dayjs().add(1, 'day').utc().format("YYYY-MM-DD HH:mm:ss")
        });

        if (!response.result) {
            switch (response.error) {
                default:
                    alert(`검색 실패 (${Errors[response.error]})`);
                    break;
            }
            dispatch(layoutsActions.stopLoading());

            return;
        }

        const parameters = `searchUid=${searchUid}&searchMemberNo=${searchMemberNo}&searchNick=${searchNick}&startDate=${startDate}&endDate=${endDate}`;
        if (null != response.mails && 0 < response.mails.length) {
            let searchedDatas = response.mails;
            if (searchUid)
                searchedDatas = searchedDatas.filter(_ => _.UID == searchUid);

            if (searchMemberNo)
                searchedDatas = searchedDatas.filter(_ => _.MemberNo == searchMemberNo);

            if (searchNick)
                searchedDatas = searchedDatas.filter(_ => _.Nick == searchNick);

            let pageSize = 10;

            searchedDatas = searchedDatas.sort((a, b) => dayjs(b.CreateAt).diff(a.CreateAt));

            setDatas(prev => {
                return new PaginatedList<Models.MailWithUser>(searchedDatas, page, parameters, pageSize, prev.pageBlockSize);
            });
        }
        dispatch(layoutsActions.stopLoading());
    }, [dispatch, setDatas, page, searchUid, searchMemberNo, searchNick, startDate, endDate]);

    const search = useCallback(async () => {
        const parameters = `?page=1&searchUid=${searchInput.searchUid ?? ''}&searchMemberNo=${searchInput.searchMemberNo ?? ''}&searchNick=${searchInput.searchNick ?? ''}&startDate=${(searchInput.startDate ? searchInput.startDate.tz().format("YYYY-MM-DD HH:mm:ss") : "")}&endDate=${(searchInput.endDate ? searchInput.endDate.tz().format("YYYY-MM-DD HH:mm:ss") : "")}`;

        firstRender.current = true;
        router.push(parameters);
    }, [firstRender, router, searchInput]);

    const onSearch = useCallback(async () => {
        if (!searchInput.startDate && !searchInput.endDate) {
            alert("검색 기간을 선택해 주세요.");
            return;
        }

        if (dayjs.utc(searchInput.endDate).diff(dayjs.utc(searchInput.startDate), 'day') <= 0) {
            alert(`종료일이 시작일보다 같거나 이전입니다.`);
            return;
        }

        if (dayjs.utc(searchInput.endDate).diff(dayjs.utc(searchInput.startDate), 'day') > 100) {
            alert("검색 기간은 100일 이내로 선택해 주세요.");
            return;
        }

        const deleteDatas = datas.totalItems.filter(_ => _.isSelected);
        if (0 < deleteDatas.length) {
            if (false == confirm("삭제하려고 선택된 데이터가 있습니다.\n계속하면 선택이 해제됩니다.\n계속 하시겠습니까?")) {
                return;
            }
        }

        await search();
    }, [searchInput, datas, search]);

    //#region OnRender
    useEffect(() => {
        if (firstRender.current) {
            firstRender.current = false;

            setSearchInput(prev => {
                let dayjsStartDate: Dayjs | null = null;
                let dayjsEndDate: Dayjs | null = null;

                try {
                    dayjsStartDate = isEmpty(startDate) ? null : dayjs.tz(startDate);
                } catch (error) {
                    console.log(error);
                }

                try {
                    dayjsEndDate = isEmpty(endDate) ? null : dayjs.tz(endDate);
                } catch (error) {
                    console.log(error);
                }

                return new SearchInput(searchUid, searchMemberNo, searchNick, dayjsStartDate, dayjsEndDate);
            });

            loadDatas();
        } else {
            const parameters = `searchUid=${searchUid}&searchMemberNo=${searchMemberNo}&searchNick=${searchNick}&startDate=${startDate}&endDate=${endDate}`;
            setDatas(prev => {
                return new PaginatedList<Models.MailWithUser>(prev.totalItems, page, parameters, prev.pageSize, prev.pageBlockSize);
            });
        }

    }, [firstRender, loadDatas, setDatas, setSearchInput, search, page, searchUid, searchMemberNo, searchNick, startDate, endDate]);
    //#endregion

    const onChangeStartDate = useCallback((date: string | null) => {
        setSearchInput(prev => {
            let dayjsDate: Dayjs | null = null;

            try {
                dayjsDate = dayjs.tz(date);
            } catch (error) {
                return prev;
            }

            prev.startDate = dayjsDate;
            return prev.clone();
        });
    }, [setSearchInput]);

    const onChangeEndDate = useCallback((date: string | null) => {
        setSearchInput(prev => {
            let dayjsDate: Dayjs | null = null;

            try {
                dayjsDate = dayjs.tz(date);
            } catch (error) {
                return prev;
            }

            prev.endDate = dayjsDate;
            return prev.clone();
        });
    }, [setSearchInput]);

    const onChangeData = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>, searchtTYPE: SearchType) => {
        setSearchInput(prev => {
            switch (searchtTYPE) {
                case SearchType.uid:
                    if (isNaN(parseInt(e.target.value)))
                        return prev;

                    prev.searchUid = parseInt(e.target.value);
                    break;

                case SearchType.memberNo:
                    if (isNaN(parseInt(e.target.value)))
                        return prev;

                    prev.searchMemberNo = parseInt(e.target.value);
                    break;

                case SearchType.nick:
                    prev.searchNick = e.target.value ?? "";
                    break;

            }
            return prev.clone();
        });
    }, [setSearchInput]);

    const onRemoveData = useCallback((searchtTYPE: SearchType) => {
        setSearchInput(prev => {
            switch (searchtTYPE) {
                case SearchType.uid:
                    prev.searchUid = null;
                    break;

                case SearchType.memberNo:
                    prev.searchMemberNo = null;
                    break;

                case SearchType.nick:
                    prev.searchNick = '';
                    break;

            }
            return prev.clone();
        });
    }, [setSearchInput]);

    const onCheckToDeleteMail = useCallback((mailId: number) => {
        const parameters = `searchUid=${searchUid}&searchMemberNo=${searchMemberNo}&searchNick=${searchNick}&startDate=${startDate}&endDate=${endDate}`;
        let pageSize = 10;

        setDatas(prev => {
            const data = prev.totalItems.find(_ => _.MailID == mailId);
            if (null == data) {
                alert("삭제할 우편이 없습니다.");
                return prev;
            }

            if (EMailStateType.Arrived != data.State && EMailStateType.None != data.State) {
                alert('이미 수령한 우편입니다.');
                return prev;
            }

            data.isSelected = !data.isSelected;

            return new PaginatedList<Models.MailWithUser>(prev.totalItems, page, parameters, pageSize, prev.pageBlockSize);
        });
    }, [setDatas, page, searchUid, searchMemberNo, searchNick, startDate, endDate]);

    const onCheckToDeleteAllMail = useCallback(() => {
        const parameters = `searchUid=${searchUid}&searchMemberNo=${searchMemberNo}&searchNick=${searchNick}&startDate=${startDate}&endDate=${endDate}`;
        let pageSize = 10;

        setDatas(prev => {
            if ('undefined' == typeof datas.items || null == datas.items || 1 > datas.items.length) {
                alert("삭제할 우편이 없습니다.");
                return prev;
            }

            for (let i = 0; i <= datas.items.length; i++) {
                const targetMail = datas.items[i];
                if (null == targetMail)
                    continue;

                const data = prev.totalItems.find(_ => _.MailID == targetMail.MailID);
                if (null == data) {
                    alert("삭제할 우편이 없습니다.");
                    return prev;
                }

                if (EMailStateType.Arrived != data.State && EMailStateType.None != data.State)
                    continue;

                data.isSelected = !data.isSelected;
            }

            return new PaginatedList<Models.MailWithUser>(prev.totalItems, page, parameters, pageSize, prev.pageBlockSize);
        });
    }, [datas, setDatas, page, searchUid, searchMemberNo, searchNick, startDate, endDate]);

    const onDeleteMail = useCallback(async () => {
        const cantDeleteItems = datas.totalItems.filter(_ => _.isSelected && EMailStateType.Arrived != _.State && EMailStateType.None != _.State);
        if (0 < cantDeleteItems.length) {
            alert('수령한 우편은 삭제할 수 없습니다.');
            return;
        }

        const deleteItems = datas.totalItems.filter(_ => _.isSelected && (EMailStateType.Arrived == _.State || EMailStateType.None == _.State));
        if (1 > deleteItems.length) {
            alert('삭제할 우편을 선택해 주세요.');
            return;
        }

        if (false == confirm(`${deleteItems.length}개의 우편 데이터를 삭제하시겠습니까?\n한번 삭제한 데이터는 복구할 수 없습니다.`)) {
            return;
        }

        dispatch(layoutsActions.startLoadingMessage("우편 데이터를 삭제중 입니다."));

        const deleteMailIds: number[] = [];

        for (let i = 0; i < deleteItems.length; i++)
            deleteMailIds.push(deleteItems[i].MailID);

        const response = await GameAPI.DeleteMailWithUsersAsync({ mailIDs: deleteMailIds });
        if (response.result) {
            alert("선택한 우편 데이터를 삭제했습니다.");
            const parameters = `?page=1&searchUid=${searchInput.searchUid ?? ''}&searchMemberNo=${searchInput.searchMemberNo ?? ''}&searchNick=${searchInput.searchNick ?? ''}&startDate=${(searchInput.startDate ? searchInput.startDate.tz().format("YYYY-MM-DD HH:mm:ss") : "")}&endDate=${(searchInput.endDate ? searchInput.endDate.tz().format("YYYY-MM-DD HH:mm:ss") : "")}`;
            firstRender.current = true;
            router.push(parameters);
        } else {
            alert(`우편을 삭제하는데 실패했습니다.(error: ${Errors[response.error]}, MailID: ${response.errorMailId})`);
        }

        dispatch(layoutsActions.stopLoading());

    }, [datas, dispatch, firstRender, searchInput, router]);

    const contents = useCallback(() => {
        let result: ReactElement = <></>;

        if (0 < datas.total) {
            const list: ReactElement[] = [];
            for (let i = 0; i < datas.length; i++) {
                const data = datas.items[i];
                let rowClass = `${mailStyles.rowStyle}`;
                const notRecieved = EMailStateType.Arrived == data.State || EMailStateType.None == data.State;
                if (notRecieved) {
                    rowClass += ` ${mailStyles.notRecieved}`;
                }

                const row = (
                    <BorderedTableRow key={i} className={rowClass}>
                        <TableCell component="th" scope="row">{datas.offsetIndex - i}</TableCell>
                        <TableCell>
                            {
                                notRecieved
                                    ?
                                    <FormGroup sx={{ flexWrap: 'wrap', justifyContent: 'center', alignContent: 'center' }}>
                                        <FormControlLabel control={
                                            <Checkbox size="small" checked={data.isSelected} onClick={() => onCheckToDeleteMail(data?.MailID)} sx={{ padding: 0 }} />
                                        } label='' sx={{ minWidth: 1, justifyContent: 'center', margin: 0 }} />
                                    </FormGroup>
                                    :
                                    <></>
                            }
                        </TableCell>
                        <TableCell>{data?.MailID}</TableCell>
                        <TableCell>{data?.UID.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{data?.MemberNo.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{data?.Nick.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{EMailType[data?.MailType].toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{EMailStateType[data?.State].toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{(true == data?.IsBM ? 'BM 우편' : '')}</TableCell>
                        <TableCell>{data?.Title.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{data?.Message.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{0 < data.ExpireAt ? dayjs.unix(data.ExpireAt).tz().format('YYYY-MM-DD HH:mm:ss') : ''}</TableCell>
                        <TableCell>{0 < data.ReceiveAt ? dayjs.unix(data.ReceiveAt).tz().format('YYYY-MM-DD HH:mm:ss') : ''}</TableCell>
                        <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    </BorderedTableRow>
                );

                list.push(row);
            }
            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>순번</TableCell>
                            <TableCell>
                                <FormGroup sx={{ flexWrap: 'wrap', justifyContent: 'center', alignContent: 'center' }}>
                                    <FormControlLabel control={
                                        <Checkbox size="small" checked={false} onClick={onCheckToDeleteAllMail} sx={{ padding: 0 }} />
                                    } label={<Typography sx={{ fontSize: '0.875rem' }}>삭제</Typography>} sx={{ minWidth: 1, justifyContent: 'center', alignItems: 'center', margin: 0 }} />
                                </FormGroup>
                            </TableCell>
                            <TableCell><div style={styles.smallCell}>MailID</div></TableCell>
                            <TableCell><div style={styles.smallCell}>UID</div></TableCell>
                            <TableCell><div style={styles.smallCell}>MemberNo</div></TableCell>
                            <TableCell><div style={styles.smallCell}>Nick</div></TableCell>
                            <TableCell><div style={styles.smallCell}>MailType</div></TableCell>
                            <TableCell><div style={styles.smallCell}>State</div></TableCell>
                            <TableCell><div style={styles.smallCell}>IsBM</div></TableCell>
                            <TableCell><div style={styles.smallCell}>Title</div></TableCell>
                            <TableCell><div style={styles.smallCell}>Message</div></TableCell>
                            <TableCell><div style={styles.smallCell}>ExpireAt</div></TableCell>
                            <TableCell><div style={styles.smallCell}>ReceiveAt</div></TableCell>
                            <TableCell><div style={styles.smallCell}>CreateAt</div></TableCell>
                            <TableCell><div style={styles.smallCell}>UpdateAt</div></TableCell>
                        </BorderedTableRow>
                    </TableHead>
                    <TableBody>
                        {list}
                    </TableBody>
                </>
            )
        } else {
            result = (
                <TableBody>
                    <BorderedTableRow>
                        <TableCell className={commonUIStyles.noneCell}>검색된 우편 데이터가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }

        return result;
    }, [datas, onCheckToDeleteAllMail, onCheckToDeleteMail]);

    return (
        <Box sx={{ mb: '100px', width: '100%' }}>
            <Box sx={{ mt: 2 }}>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* 발송된 우편검색 및 미수령 우편 삭제 가능.</Typography>
            </Box>
            <Grid container justifyContent='center' sx={{ margin: '20px 0 0' }}>
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: '50%', maxWidth: '50%' }}>
                    <FormControl fullWidth variant='outlined' size='small'>
                        <InputLabel htmlFor="uid-input">AccountID</InputLabel>
                        <OutlinedInput sx={{ paddingRight: 0 }} id="uid-input" label="AccountID" value={searchInput.searchUid ?? ''} onChange={(e) => onChangeData(e, SearchType.uid)}
                            endAdornment={
                                <InputAdornment position="end">
                                    <IconButton type="button" sx={{ p: '10px' }} aria-label="uid-input" onClick={() => onRemoveData(SearchType.uid)}>
                                        <RemoveIcon />
                                    </IconButton>
                                </InputAdornment>
                            }
                        />
                    </FormControl>
                </Grid>
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: '50%', maxWidth: '50%' }}>
                    <FormControl fullWidth variant='outlined' size='small'>
                        <InputLabel htmlFor="memberNo-input">MemberNo</InputLabel>
                        <OutlinedInput sx={{ paddingRight: 0 }} id="memberNo-input" label="MemberNo" value={searchInput.searchMemberNo ?? ''} onChange={(e) => onChangeData(e, SearchType.memberNo)}
                            endAdornment={
                                <InputAdornment position="end">
                                    <IconButton type="button" sx={{ p: '10px' }} aria-label="memberNo-input" onClick={() => onRemoveData(SearchType.memberNo)}>
                                        <RemoveIcon />
                                    </IconButton>
                                </InputAdornment>
                            }
                        />
                    </FormControl>
                </Grid>
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: '50%', maxWidth: '50%' }}>
                    <FormControl fullWidth variant='outlined' size='small'>
                        <InputLabel htmlFor="nick-input">Nick</InputLabel>
                        <OutlinedInput sx={{ paddingRight: 0 }} id="nick-input" label="Nick" value={searchInput.searchNick ?? ''} onChange={(e) => onChangeData(e, SearchType.nick)}
                            endAdornment={
                                <InputAdornment position="end">
                                    <IconButton type="button" sx={{ p: '10px' }} aria-label="nick-input" onClick={() => onRemoveData(SearchType.nick)}>
                                        <RemoveIcon />
                                    </IconButton>
                                </InputAdornment>
                            }
                        />
                    </FormControl>
                </Grid>
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: '50%', maxWidth: '50%' }}>
                    <DateTimePicker label={`시작일(${timezoneName})`} enableTimePicker={true} format="YYYY-MM-DD HH:mm" value={(searchInput.startDate && dayjs(searchInput.startDate).isValid() ? dayjs.tz(searchInput.startDate).format("YYYY-MM-DD HH:mm") : null)} onChange={(date) => onChangeStartDate(date)} />
                </Grid>
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: '50%', maxWidth: '50%' }}>
                    <DateTimePicker label={`종료일(${timezoneName})`} enableTimePicker={true} format="YYYY-MM-DD HH:mm" value={(searchInput.endDate && dayjs(searchInput.endDate).isValid() ? dayjs.tz(searchInput.endDate).format("YYYY-MM-DD HH:mm") : null)} onChange={(date) => onChangeEndDate(date)} />
                </Grid>
            </Grid>
            <Grid container justifyContent='center'>
                <Grid item xs={10} sx={{ padding: '10px', textAlign: 'center' }}>
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onSearch}>검색</Button> &nbsp;
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='error' size="small" onClick={onDeleteMail}>삭제</Button>
                </Grid>
            </Grid>
            <Paging datas={datas} />
            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 600 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>우편 관리</Typography>
                </Toolbar>
                <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                    {contents()}
                </Table>
            </TableContainer>
            <Paging datas={datas} />
        </Box>
    )
}
export default List;