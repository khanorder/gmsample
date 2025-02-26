import { ReactElement, useCallback, useEffect, useRef, useState, ChangeEvent } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { Defines } from "@ngeldata/autoDefines";
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import * as layoutsActions from '@store/reducers/layouts';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer } from '@mui/material';
import dynamic from 'next/dynamic';
import { useAppDispatch } from '@hooks/index';
import { GameAPI } from '@ngel/data/apis/gameAPI';
import { Errors } from '@ngel/data/autoErrors';
import { useRouter } from 'next/router';
import { EPenaltyReportState } from '@ngel/data/models/lobby';
import Link from 'next/link';
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
const PenaltyList = dynamic(() => import('@components/GMS/Users/Penalties/List'), { ssr: false });
const Paging = dynamic(() => import('@components/ui/Paging'), { ssr: false });
const DateTimePicker = dynamic(() => import('@components/ui/DateTimePicker'), { ssr: false });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const IconButton = dynamic(() => import('@mui/material/IconButton'), { ssr: false });
const RemoveIcon = dynamic(() => import('@mui/icons-material/Clear'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });

interface ListProps {
    page: number;
    searchUid: number;
    searchMemberNo: number;
    searchNick: string;
    startDate: string;
    endDate: string;
}

class SearchInput {
    searchUid?: number|null;
    searchMemberNo?: number|null;
    searchNick?: string|null;
    startDate?: Dayjs|null;
    endDate?: Dayjs|null;

    constructor(
        uid?: number|null, memberNo?: number|null, nick?: string|null, startDate?: Dayjs|null, endDate?: Dayjs|null
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
    }
}

const enum SearchType {
    uid = 0,
    memberNo = 1,
    nick = 2
}

const List = ({ page, searchUid, searchMemberNo, searchNick, startDate, endDate }: ListProps) : ReactElement => {
    const dispatch = useAppDispatch();
    const firstRender = useRef(true);
    const router = useRouter();

    const [datas, setDatas] = useState<PaginatedList<Models.PenaltyWithUser>>(new PaginatedList<Models.PenaltyWithUser>([]));
    const [searchInput, setSearchInput] = useState<SearchInput>(new SearchInput());

    const loadDatas = useCallback(async () => {
        setDatas(new PaginatedList<Models.PenaltyWithUser>([]));

        dispatch(layoutsActions.startLoadingMessage("패널티 데이터를 불러오는 중입니다."));

        const response = await GameAPI.PenaltiesAllAsync({
            startTime: startDate ? dayjs.tz(startDate).utc().format("YYYY-MM-DD HH:mm:ss") : dayjs.utc().format("YYYY-MM-DD HH:mm:ss"), 
            endTime: endDate ? dayjs.tz(endDate).add(1, 'day').utc().format("YYYY-MM-DD HH:mm:ss") : dayjs().add(1, 'day').utc().format("YYYY-MM-DD HH:mm:ss"),
            uid: searchUid,
            memberNo: searchUid,
            nick: searchNick
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
        if (null != response.penalties && 0 < response.penalties.length) {
            let searchedDatas = response.penalties;
            if (searchUid)
                searchedDatas = searchedDatas.filter(_ => _.UID == searchUid);

            if (searchMemberNo)
                searchedDatas = searchedDatas.filter(_ => _.MemberNo == searchMemberNo);

            if (searchNick)
                searchedDatas = searchedDatas.filter(_ => _.Nick == searchNick);
            
            let pageSize = 10;
            
            searchedDatas = searchedDatas.sort((a, b) => dayjs(b.CreateAt).diff(a.CreateAt));

            setDatas(prev => {
                return new PaginatedList<Models.PenaltyWithUser>(searchedDatas, page, parameters, pageSize, prev.pageBlockSize);
            });
        }
        dispatch(layoutsActions.stopLoading());
    }, [dispatch, page, searchUid, searchMemberNo, searchNick, startDate, endDate]);

    //#region OnRender
    useEffect(() => {
        if (firstRender.current) {
            firstRender.current = false;

            loadDatas();
        } else {
            const parameters = `searchUid=${searchUid}&searchMemberNo=${searchMemberNo}&searchNick=${searchNick}&startDate=${startDate}&endDate=${endDate}`;
            setDatas(prev => {
                return new PaginatedList<Models.PenaltyWithUser>(prev.totalItems, page, parameters, prev.pageSize, prev.pageBlockSize);
            });
        }

        setSearchInput(prev => {
            let dayjsStartDate: Dayjs|null = dayjs.utc().add(-1, 'day');
            let dayjsEndDate: Dayjs|null = dayjs.utc();

            try {
                dayjsStartDate = dayjs.tz(startDate);
            } catch (error) {
                console.log(error);
            }

            try {
                dayjsEndDate = dayjs.tz(endDate);
            } catch (error) {
                console.log(error);
            }

            return new SearchInput(searchUid, searchMemberNo, searchNick, dayjsStartDate, dayjsEndDate);
        });

    }, [firstRender, setSearchInput, loadDatas, setDatas, page, searchUid, searchMemberNo, searchNick, startDate, endDate]);
    //#endregion

    const onSearch = useCallback(async () => {
        if (!searchInput.searchUid && !searchInput.searchMemberNo && !searchInput.searchNick) {
        } else {
            if (!searchInput.startDate && !searchInput.endDate) {
                alert("검색 기간을 선택해 주세요.");
                return;
            }
    
            if (dayjs.utc(searchInput.endDate).diff(dayjs.utc(searchInput.startDate), 'day') <= 0){
                alert(`종료일이 시작일보다 같거나 이전입니다.`);
                return;
            }
            
            if (dayjs.utc(searchInput.endDate).diff(dayjs.utc(searchInput.startDate), 'day') > 100) {
                alert("검색 기간은 100일 이내로 선택해 주세요.");
                return;
            }
        }

        const parameters = `?page=1&searchUid=${searchInput.searchUid ?? ''}&searchMemberNo=${searchInput.searchMemberNo ?? ''}&searchNick=${searchInput.searchNick ?? ''}&startDate=${(searchInput.startDate ? searchInput.startDate.tz().format("YYYY-MM-DD HH:mm:ss") : "")}&endDate=${(searchInput.endDate ? searchInput.endDate.tz().format("YYYY-MM-DD HH:mm:ss") : "")}`;
        
        firstRender.current = true;
        router.push(parameters);
    },[firstRender, router, searchInput]);

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

    const onChangeData = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, searchtTYPE: SearchType) => {
        setSearchInput(prev => {
            switch(searchtTYPE){
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
            switch(searchtTYPE){
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

    const contents = useCallback(() => {
        let result: ReactElement = <></>;

        if (0 < datas.total) {
            const list: ReactElement[] = [];
            for (let i = 0; i < datas.length; i++) {
                const data = datas.items[i];

                const row = (
                    <BorderedTableRow key={i}>
                        <TableCell component="th" scope="row">{datas.offsetIndex - i}</TableCell>
                        <TableCell>
                            <Link href={`/GMS/Users/Penalties?mode=edit&uid=${data?.UID}&reportState=${data.ReportState}&page=${page}&searchUid=${searchUid ?? ''}&searchMemberNo=${searchMemberNo ?? ''}&searchNick=${searchNick ?? ''}&startDate=${startDate ?? ''}&endDate=${endDate ?? ''}`}>
                                <Button variant='outlined' size='small' sx={{ lineHeight: '1.2rem', margin: 0.1 }}>수정</Button>
                            </Link>
                        </TableCell>
                        <TableCell>{data?.UID.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{data?.MemberNo.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{data?.Nick.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{EPenaltyReportState[data?.ReportState].toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{(true == data?.IsActive ? '활성화' : '')}</TableCell>
                        <TableCell>{data?.PenaltyGrade}</TableCell>
                        <TableCell>{data?.PenaltyPoint}</TableCell>
                        <TableCell>{data?.PenaltyCount}</TableCell>
                        <TableCell>{0 < data.PenaltyEndAt ? dayjs.unix(data.PenaltyEndAt).tz().format('YYYY-MM-DD HH:mm:ss') : ''}</TableCell>
                        <TableCell>{0 < data.ClearPenaltyAt ? dayjs.unix(data.ClearPenaltyAt).tz().format('YYYY-MM-DD HH:mm:ss') : ''}</TableCell>
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
                            <TableCell><div style={styles.smallCell}>관리</div></TableCell>
                            <TableCell><div style={styles.smallCell}>UID</div></TableCell>
                            <TableCell><div style={styles.smallCell}>MemberNo</div></TableCell>
                            <TableCell><div style={styles.smallCell}>Nick</div></TableCell>
                            <TableCell><div style={styles.smallCell}>ReportState</div></TableCell>
                            <TableCell><div style={styles.smallCell}>IsActive</div></TableCell>
                            <TableCell><div style={styles.smallCell}>PenaltyGrade</div></TableCell>
                            <TableCell><div style={styles.smallCell}>PenaltyPoint</div></TableCell>
                            <TableCell><div style={styles.smallCell}>PenaltyCount</div></TableCell>
                            <TableCell><div style={styles.smallCell}>PenaltyEndAt</div></TableCell>
                            <TableCell><div style={styles.smallCell}>ClearPenaltyAt</div></TableCell>
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
                        <TableCell className={commonUIStyles.noneCell}>검색된 패널티 데이터가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }

        return result;
    }, [datas, page, startDate, endDate, searchMemberNo, searchNick, searchUid]);

    return (
        <Box sx={{ mb: '100px', width: '100%' }}>
            <Box sx={{ mt: 2 }}>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* 검색 후 유저의 패널티 상태 편집 가능.</Typography>
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
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onSearch}>검색</Button>
                </Grid>
            </Grid>
            <Paging datas={datas} />
            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 600 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>패널티 사용자 관리</Typography>
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