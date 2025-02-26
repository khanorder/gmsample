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
import { EMailStateType, EMailType, EPenaltyReportState } from '@ngel/data/models/lobby';
import Link from 'next/link';
import * as XLSX from 'xlsx';
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
const FormGroup = dynamic(() => import('@mui/material/FormGroup'), { ssr: false });
const FormControlLabel = dynamic(() => import('@mui/material/FormControlLabel'), { ssr: false });
const Checkbox = dynamic(() => import('@mui/material/Checkbox'), { ssr: false });

interface ListProps {
    page: number;
    searchUidProp: number;
    searchMemberNoProp: number;
    searchTidProp: string;
    searchOriTidProp: string;
    searchProductidProp: string;
    startDateProp: string;
    endDateProp: string;
}

class SearchInput {
    searchUid?: number|null;
    searchMemberNo?: number|null;
    searchTid?: string|null;
    searchOriTid?: string|null;
    searchProductid?: string|null;
    startDate?: Dayjs|null;
    endDate?: Dayjs|null;

    constructor(
        uid?: number|null, memberNo?: number|null, tid?: string|null, oriTid?: string|null, productid?: string|null, startDate?: Dayjs|null, endDate?: Dayjs|null,
    ) {
        this.searchUid = uid;
        this.searchMemberNo = memberNo;
        this.startDate = startDate;
        this.endDate = endDate;
        this.searchTid = tid;
        this.searchOriTid = oriTid;
        this.searchProductid = productid;
    }

    clone = () => new SearchInput(this.searchUid, this.searchMemberNo, this.searchTid, this.searchOriTid, this.searchProductid, this.startDate, this.endDate);
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
    tId = 2,
    originalTid = 3,
    productId = 4
}

const colList = Object.keys(new Models.Billing());

const List = ({ page, searchUidProp, searchMemberNoProp, searchTidProp, searchOriTidProp, searchProductidProp, startDateProp, endDateProp }: ListProps) : ReactElement => {
    const dispatch = useAppDispatch();
    const firstRender = useRef(true);
    const router = useRouter();

    const [datas, setDatas] = useState<PaginatedList<Models.Billing>>(new PaginatedList<Models.Billing>([]));
    const [searchInput, setSearchInput] = useState<SearchInput>(new SearchInput());

    const loadDatas = useCallback(async () => {
        setDatas(new PaginatedList<Models.Billing>([]));

        dispatch(layoutsActions.startLoadingMessage("결제 데이터를 불러오는 중입니다."));

        const response = await GameAPI.BillingsAsync({
            startTime: startDateProp ? dayjs.tz(startDateProp).utc().format("YYYY-MM-DD HH:mm:ss") : dayjs.utc().format("YYYY-MM-DD HH:mm:ss"), 
            endTime: endDateProp ? dayjs.tz(endDateProp).add(1, 'day').utc().format("YYYY-MM-DD HH:mm:ss") : dayjs().add(1, 'day').utc().format("YYYY-MM-DD HH:mm:ss")
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
        
        const parameters = `searchUid=${searchUidProp ?? ''}&searchMemberNo=${searchMemberNoProp ?? ''}&searchTid=${searchTidProp ?? ''}&searchOriTid=${searchOriTidProp ?? ''}&searchProductid=${searchProductidProp ?? ''}&startDate=${startDateProp}&endDate=${endDateProp}`;
        if (null != response.billings && 0 < response.billings.length) {
            let searchedDatas = response.billings;
            if (searchUidProp)
                searchedDatas = searchedDatas.filter(_ => _.UID == searchUidProp);

            if (searchMemberNoProp)
                searchedDatas = searchedDatas.filter(_ => _.MemberNo == searchMemberNoProp.toString());
            
            if (searchTidProp)
                searchedDatas = searchedDatas.filter(_ => _.TID.includes(searchTidProp.toString()));
            
            if (searchOriTidProp)
                searchedDatas = searchedDatas.filter(_ => _.OriginalTID.includes(searchOriTidProp.toString()))
            
            if (searchProductidProp)
                searchedDatas = searchedDatas.filter(_ => _.ProductID.includes(searchProductidProp.toString()))

            let pageSize = 10;
            
            searchedDatas = searchedDatas.sort((a, b) => dayjs(b.CreateAt).diff(a.CreateAt));

            setDatas(prev => {
                return new PaginatedList<Models.Billing>(searchedDatas, page, parameters, pageSize, prev.pageBlockSize);
            });
        }
        dispatch(layoutsActions.stopLoading());
    }, [dispatch, page, searchUidProp, searchMemberNoProp, searchTidProp, searchOriTidProp, searchProductidProp, startDateProp, endDateProp]);
    //#region OnRender
    useEffect(() => {
        if (firstRender.current) {
            firstRender.current = false;

            setSearchInput(prev => {
                let startDate: Dayjs | null = null;
        
                try {
                    startDate = dayjs.tz(startDateProp);
                } catch (error) {
                    startDate = dayjs().tz();
                }

                let endDate: Dayjs | null = null;
        
                try {
                    endDate = dayjs.tz(endDateProp);
                } catch (error) {
                    endDate = dayjs().tz().add(1, 'day');
                }

                prev.startDate = startDate.startOf('day');
                prev.endDate = endDate;
                return prev.clone();
            });
            
            loadDatas();
        } else {
            const parameters = `searchUid=${searchUidProp}&searchMemberNo=${searchMemberNoProp}&searchTid=${searchTidProp}&searchOriTid=${searchOriTidProp}&searchProductid=${searchProductidProp}&startDate=${startDateProp}&endDate=${endDateProp}`;
            setDatas(prev => {
                return new PaginatedList<Models.Billing>(prev.totalItems, page, parameters, prev.pageSize, prev.pageBlockSize);
            });
        }

    }, [firstRender, setSearchInput, loadDatas, setDatas, page, searchUidProp, searchMemberNoProp, searchTidProp, searchOriTidProp, searchProductidProp, startDateProp, endDateProp]);
    //#endregion

    const onSearch = useCallback(async () => {
        if (!searchInput.startDate && !searchInput.endDate) {
            alert("검색 기간을 선택해 주세요.");
            return;
        }

        if (dayjs.utc(searchInput.endDate).diff(dayjs.utc(searchInput.startDate), 'minute') <= 0){
            alert(`종료일이 시작일보다 같거나 이전입니다.`);
            return;
        }
        
        if (dayjs.utc(searchInput.endDate).diff(dayjs.utc(searchInput.startDate), 'day') > 100) {
            alert("검색 기간은 100일 이내로 선택해 주세요.");
            return;
        }

        const parameters = `?page=1&searchUid=${searchInput.searchUid ?? ''}&searchMemberNo=${searchInput.searchMemberNo ?? ''}&searchTid=${searchInput.searchTid ?? ''}&searchOriTid=${searchInput.searchOriTid ?? ''}&searchProductid=${searchInput.searchProductid ?? ''}&startDate=${(searchInput.startDate ? searchInput.startDate.tz().format("YYYY-MM-DD HH:mm:ss") : "")}&endDate=${(searchInput.endDate ? searchInput.endDate.tz().format("YYYY-MM-DD HH:mm:ss") : "")}`;
        
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

                case SearchType.tId:
                    prev.searchTid = e.target.value;
                    break;

                case SearchType.originalTid:
                    prev.searchOriTid = e.target.value;
                    break;

                case SearchType.productId:
                    prev.searchProductid = e.target.value;
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

                case SearchType.tId:
                    prev.searchTid = null;    
                    break;

                case SearchType.originalTid:
                    prev.searchOriTid = null;    
                    break;

                case SearchType.productId:
                    prev.searchProductid = null;    
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
                    <BorderedTableRow key={i} >
                        <TableCell component="th" scope="row">{datas.offsetIndex - i}</TableCell>
                        <TableCell>{data?.UID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{data?.TID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{data?.OriginalTID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{data?.MemberNo.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{data?.NickNameNo.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{data?.NotiType.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{(true == data?.IsRewarded ? '보상 획득' : '')}</TableCell>
                        <TableCell>{data?.ProductID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{data?.Price.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{data?.SubStatusCode.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{JSON.parse(data?.BillingData)?.data["market_code"] ? "" : ""}</TableCell>
                        <TableCell>{JSON.parse(data?.BillingData)?.data["product_currency"] ? "" : ""}</TableCell>
                        <TableCell>{data?.BillingData.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{0 < data.TxnTime ? dayjs.unix(data.TxnTime / 1000).tz().format('YYYY-MM-DD HH:mm:ss') : ''}</TableCell>
                        <TableCell>{0 < data.ExpireTime ? dayjs.unix(data.ExpireTime / 1000).tz().format('YYYY-MM-DD HH:mm:ss') : ''}</TableCell>
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
                            <TableCell><div style={styles.smallCell}>UID</div></TableCell>
                            <TableCell><div style={styles.smallCell}>TID</div></TableCell>
                            <TableCell><div style={styles.smallCell}>OriginalTID</div></TableCell>
                            <TableCell><div style={styles.smallCell}>MemberNo</div></TableCell>
                            <TableCell><div style={styles.smallCell}>NickNameNo</div></TableCell>
                            <TableCell><div style={styles.smallCell}>NotiType</div></TableCell>
                            <TableCell><div style={styles.smallCell}>IsRewarded</div></TableCell>
                            <TableCell><div style={styles.smallCell}>ProductID</div></TableCell>
                            <TableCell><div style={styles.smallCell}>Price</div></TableCell>
                            <TableCell><div style={styles.smallCell}>SubStatusCode</div></TableCell>
                            <TableCell><div style={styles.smallCell}>MarketCode</div></TableCell>
                            <TableCell><div style={styles.smallCell}>ProductCurrency</div></TableCell>
                            <TableCell><div style={styles.smallCell}>BillingData</div></TableCell>
                            <TableCell><div style={styles.smallCell}>TxnTime</div></TableCell>
                            <TableCell><div style={styles.smallCell}>ExpireTime</div></TableCell>
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
                        <TableCell className={commonUIStyles.noneCell}>검색된 결제 데이터가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }

        return result;
    }, [datas]);

    const onDownloadLog = useCallback(async () => {
        dispatch(layoutsActions.startLoadingMessage(`엑셀을 다운로드 중입니다. 0 / ${datas.totalItems.length}`));
        if(datas && 0 < datas.totalItems.length){
            const sheet: string[] = [...colList];
            const sheets: string[][] = [
                [...sheet]
            ];

            for (let j = 0; j < datas.totalItems.length; j++) {
                const dataInfo = datas.totalItems[j];
                const dataSheet = [];

                for(let k = 0; k < sheet.length; k++){
                    if(sheet[k] == "regTime") dataSheet.push(dayjs(dataInfo[sheet[k]]).tz().format("YYYY-MM-DD HH:mm"))
                    else if((sheet[k] == "ExpireTime" || sheet[k] == "TxnTime") && dataInfo[sheet[k]]) dataSheet.push(dayjs.unix(dataInfo[sheet[k]] / 1000).tz().format("YYYY-MM-DD HH:mm"))
                    else if(dataInfo[sheet[k]]) dataSheet.push(dataInfo[sheet[k]].toString().replaceAll(/(?<=.{1})./g, '*'));
                    else dataSheet.push('');
                }
                sheets.push(dataSheet);

                
                dispatch(layoutsActions.startLoadingMessage(`로그 데이터를 엑셀로 변환 중입니다. ( ${j+1} / ${datas.totalItems.length} )`));

                if(j % Math.ceil((datas.totalItems.length * 3) / 100) == 0) await new Promise(resolve => setTimeout(resolve, 0));
            }

            const workbook = XLSX.utils.book_new();
            const worksheet = XLSX.utils.aoa_to_sheet(sheets);
            XLSX.utils.book_append_sheet(workbook, worksheet, "Sheet1");
            XLSX.writeFile(workbook, `샘플운영툴_결제내역_${dayjs(searchInput.startDate).tz().format("YYYY-MM-DD")}_${dayjs(searchInput.endDate).tz().format("YYYY-MM-DD")}.xlsx`);
            dispatch(layoutsActions.stopLoading());
        }
        dispatch(layoutsActions.stopLoading());
    }, [dispatch, datas, searchInput.startDate, searchInput.endDate]);

    return (
        <Box sx={{ mb: '100px', width: '100%' }}>
            <Box sx={{ mt: 2 }}>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* 결제정보 검색 기능.</Typography>
            </Box>
            <Grid container justifyContent='center' sx={{ margin: '20px 0 0' }}>
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: '50%', maxWidth: '50%' }}>
                    <FormControl fullWidth variant='outlined' size='small'>
                        <InputLabel htmlFor="uid-input">UID</InputLabel>
                        <OutlinedInput sx={{ paddingRight: 0 }} id="uid-input" label="UID" value={searchInput.searchUid ?? ''} onChange={(e) => onChangeData(e, SearchType.uid)} 
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
                        <InputLabel htmlFor="tid-input">Tid</InputLabel>
                        <OutlinedInput sx={{ paddingRight: 0 }} id="tid-input" label="Tid" value={searchInput.searchTid ?? ''} onChange={(e) => onChangeData(e, SearchType.tId)} 
                            endAdornment={
                                <InputAdornment position="end">
                                    <IconButton type="button" sx={{ p: '10px' }} aria-label="tid-input" onClick={() => onRemoveData(SearchType.tId)}>
                                        <RemoveIcon />
                                    </IconButton>
                                </InputAdornment>
                            }
                        />
                    </FormControl>
                </Grid>
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: '50%', maxWidth: '50%' }}>
                    <FormControl fullWidth variant='outlined' size='small'>
                        <InputLabel htmlFor="oriTid-input">OriginalTid</InputLabel>
                        <OutlinedInput sx={{ paddingRight: 0 }} id="oriTid-input" label="OriginalTid" value={searchInput.searchOriTid ?? ''} onChange={(e) => onChangeData(e, SearchType.originalTid)} 
                            endAdornment={
                                <InputAdornment position="end">
                                    <IconButton type="button" sx={{ p: '10px' }} aria-label="oriTid-input" onClick={() => onRemoveData(SearchType.originalTid)}>
                                        <RemoveIcon />
                                    </IconButton>
                                </InputAdornment>
                            }
                        />
                    </FormControl>
                </Grid>
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: '50%', maxWidth: '50%' }}>
                    <FormControl fullWidth variant='outlined' size='small'>
                        <InputLabel htmlFor="productId-input">ProductID</InputLabel>
                        <OutlinedInput sx={{ paddingRight: 0 }} id="productId-input" label="ProductID" value={searchInput.searchProductid ?? ''} onChange={(e) => onChangeData(e, SearchType.productId)} 
                            endAdornment={
                                <InputAdornment position="end">
                                    <IconButton type="button" sx={{ p: '10px' }} aria-label="productId-input" onClick={() => onRemoveData(SearchType.productId)}>
                                        <RemoveIcon />
                                    </IconButton>
                                </InputAdornment>
                            }
                        />
                    </FormControl>
                </Grid>
                
            </Grid>
            <Grid container justifyContent='center'>
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
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='success' size="small" onClick={async () => { await onDownloadLog(); }} sx={{ ml: 1 }}>엑셀</Button>
                </Grid>
            </Grid>
            <Paging datas={datas} />
            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 600 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>결제 관리</Typography>
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