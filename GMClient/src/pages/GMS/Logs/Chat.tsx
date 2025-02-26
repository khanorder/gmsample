import { ChangeEvent, ReactElement, useCallback, useEffect, useRef, useState } from 'react';
import { useAppDispatch, useAppSelector } from "@hooks/index";
import { GetServerSideProps } from 'next';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import base from 'base-x';
import * as layoutsActions from '@store/reducers/layouts';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer } from '@mui/material';
import { Models } from '@ngel/data/models';
import { ManagerAPI } from '@ngel/data/apis/managerAPI';
import { PaginatedList } from '@helpers/paging';
import { useRouter } from 'next/router';
import { Defines } from '@ngel/data/autoDefines';
import dynamic from 'next/dynamic';
import { Errors } from '@ngel/data/autoErrors';
import { SelectChangeEvent } from '@mui/material/Select';
import isEmpty from 'lodash/isEmpty';
import * as XLSX from 'xlsx';

const Layout = dynamic(() => import('@components/layouts'), { ssr: false });
const ThemeLayout = dynamic(() => import('@components/layouts/theme'), { ssr: false });
const ManageLayout = dynamic(() => import('@components/layouts/manage'), { ssr: false });
const Contents = dynamic(() => import('@components/GMS/Logs/Chat/Contents'), { ssr: false });
const Paging = dynamic(() => import('@components/ui/Paging'), { ssr: false });
const DateTimePicker = dynamic(() => import('@components/ui/DateTimePicker'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const InputLabel = dynamic(() => import('@mui/material/InputLabel'), { ssr: false });
const InputAdornment = dynamic(() => import('@mui/material/InputAdornment'), { ssr: false });
const OutlinedInput = dynamic(() => import('@mui/material/OutlinedInput'), { ssr: false });
const Select = dynamic(() => import('@mui/material/Select'), { ssr: false });
const MenuItem = dynamic(() => import('@mui/material/MenuItem'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const IconButton = dynamic(() => import('@mui/material/IconButton'), { ssr: false });
const RemoveIcon = dynamic(() => import('@mui/icons-material/Clear'), { ssr: false });

const base62 = base('0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ');

interface PageProps {
    pageProp: number;
    accIdProp: number;
    accNameProp: string;
    eventIdProp: string;
    stoveMemNoProp: string;
    stoveNickNoProp: string;
    IpProp: string;
    sessionProp: string;
    startDateProp: string;
    endDateProp: string;
    messageProp: string;
    logCountValueProp: string;
    logSortValueProp: string;
    logSortHistoryProp: string;
}

class SearchInput {
    accountId: number;
    accountName: string;
    eventId: string;
    stoveMemberNo: string;
    stoveNicknameNo: string;
    sessionId: string;
    ipAddress: string;
    message: string;
    startDate?: Dayjs | null;
    endDate?: Dayjs | null;

    constructor(
        accountId?: number, accountName?: string, eventId?: string, stoveMemberNo?: string, stoveNicknameNo?: string, sessionId?: string, ipAddress?: string, message?: string, startDate?: Dayjs | null, endDate?: Dayjs | null
    ) {
        this.accountId = accountId ?? 0;
        this.accountName = accountName ?? "";
        this.eventId = eventId ?? "";
        this.stoveMemberNo = stoveMemberNo ?? "";
        this.stoveNicknameNo = stoveNicknameNo ?? "";
        this.sessionId = sessionId ?? ""
        this.ipAddress = ipAddress ?? "";
        this.message = message ?? "";
        this.startDate = startDate;
        this.endDate = endDate;
    }

    clone = () => new SearchInput(this.accountId, this.accountName, this.eventId, this.stoveMemberNo, this.stoveNicknameNo, this.sessionId, this.ipAddress, this.message, this.startDate, this.endDate);
}

enum SearchType {
    accountId = 0,
    accountName = 1,
    eventId = 2,
    stoveMemberNo = 3,
    stoveNicknameNo = 4,
    sessionId = 5,
    ipAddress = 6,
    message = 7,
}

const encodingBase62 = (str: string) => {
    const encode = base62.encode(Buffer.from(str, "hex"));
    return encode;
}

const decodingBase62 = (str: string) => {
    const decode = Buffer.from(base62.decode(str)).toString("hex");
    return decode;
}

const colList = Object.keys(new Models.ChatLog());

function Page({ pageProp, accIdProp, accNameProp, eventIdProp, stoveMemNoProp, stoveNickNoProp,
    IpProp, sessionProp, startDateProp, endDateProp, messageProp, logCountValueProp, logSortValueProp, logSortHistoryProp }: PageProps) {
    const DefaultSortValue = "0000000000";
    const lastSortValue = useRef(logSortValueProp ?? "0000000000");


    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const dispatch = useAppDispatch();
    const firstRender = useRef(true);
    const router = useRouter();

    const sortTypeArray = useRef<Defines.LogSortState[]>(Array(10).fill(Defines.LogSortState.None));
    const sortedHistory = useRef<number[]>([]);
    const encodingSortState = useRef<string>(encodingBase62(sortTypeArray.current.join("")));
    const recordHistory = useRef<string>("");

    const [datas, setDatas] = useState<PaginatedList<Models.ChatLog>>(new PaginatedList<Models.ChatLog>([]));
    const [searchInput, setSearchInput] = useState<SearchInput>(new SearchInput());

    const [pageLogs, setPageLogs] = useState<number>(10);

    const loadDatas = useCallback(async () => {
        setDatas(new PaginatedList<Models.ChatLog>([]));

        if (isEmpty(startDateProp) && isEmpty(endDateProp)) {
            return;
        }

        try {
            if (dayjs.tz(endDateProp).diff(dayjs.tz(startDateProp), 'day') > 100) {
                return;
            }
        } catch (error) {
            if (process.env.NODE_ENV !== 'production')
                console.log(error);

            return;
        }

        dispatch(layoutsActions.startLoadingMessage("로그 데이터를 불러오는 중입니다."));

        const response = await ManagerAPI.ChatLogsAsync({
            startTime: startDateProp ? dayjs.tz(startDateProp).utc().format("YYYY-MM-DD HH:mm:ss") : dayjs.utc().format("YYYY-MM-DD HH:mm:ss"),
            endTime: endDateProp ? dayjs.tz(endDateProp).add(1, 'day').utc().format("YYYY-MM-DD HH:mm:ss") : dayjs().add(1, 'day').utc().format("YYYY-MM-DD HH:mm:ss")
        });

        if (!response.result) {
            switch (response.error) {
                case Errors.ChatLogs_StartTimeRequired:
                    alert(`검색 시작 날짜가 필요합니다. (${Errors[response.error]})`);
                    break;
                case Errors.ChatLogs_EndTimeRequired:
                    alert(`검색 종료 날짜가 필요합니다. (${Errors[response.error]})`);
                    break
                default:
                    alert(`검색 실패 (${Errors[response.error]})`);
                    break;
            }
            dispatch(layoutsActions.stopLoading());

            return;
        }

        const parameters = `accId=${accIdProp}&accName=${accNameProp}&evnId=${eventIdProp}&sMemNo=${stoveMemNoProp}&sNickNo=${stoveNickNoProp}&ip=${IpProp}&sesId=${sessionProp}&message=${messageProp}&startDate=${startDateProp}&endDate=${endDateProp}&lc=${logCountValueProp}&lsv=${logSortValueProp}&lsh=${logSortHistoryProp}`;
        if (null != response.chatLogs && 0 < response.chatLogs.length) {
            let searchedDatas = response.chatLogs;
            if (accIdProp)
                searchedDatas = searchedDatas.filter(_ => _.accountId == accIdProp);
            if (accNameProp)
                searchedDatas = searchedDatas.filter(_ => _.accountName.toLowerCase().includes(accNameProp.toLowerCase()));
            if (eventIdProp)
                searchedDatas = searchedDatas.filter(_ => _.eventId.toLowerCase().includes(eventIdProp.toLowerCase()));
            if (stoveMemNoProp)
                searchedDatas = searchedDatas.filter(_ => _.stoveMemberNo.toLowerCase().includes(stoveMemNoProp.toLowerCase()));
            if (stoveNickNoProp)
                searchedDatas = searchedDatas.filter(_ => _.stoveNicknameNo.toLowerCase().includes(stoveNickNoProp.toLowerCase()));
            if (IpProp)
                searchedDatas = searchedDatas.filter(_ => _.ipAddress.toLowerCase().includes(IpProp.toLowerCase()));
            if (sessionProp)
                searchedDatas = searchedDatas.filter(_ => _.sessionId.toLowerCase().includes(sessionProp.toLowerCase()));
            if (messageProp)
                searchedDatas = searchedDatas.filter(_ => _.message.toLowerCase().includes(messageProp.toLowerCase()));

            let pageSize = 10;
            if (logCountValueProp) {
                let value = parseInt(logCountValueProp);
                if ([10, 25, 50, 100, 200, 500].includes(value)) {
                    pageSize = value;
                    setPageLogs(value);
                };
            }

            searchedDatas = searchedDatas.sort((a, b) => dayjs(b.timeStamp).diff(a.timeStamp));

            if (logSortValueProp) {
                let sortCondition = "";
                sortedHistory.current.forEach((element, index) => {
                    sortCondition = addCondition(element) + sortCondition;
                    if (index !== sortedHistory.current.length - 1) sortCondition = " || " + sortCondition;
                })

                searchedDatas.sort((a, b) => {
                    const sortFunction = new Function("a", "b", "dayjs", `return ${sortCondition}`);
                    return sortFunction(a, b, dayjs);
                })
            }

            setDatas(prev => {
                return new PaginatedList<Models.ChatLog>(searchedDatas, pageProp, parameters, pageSize, prev.pageBlockSize);
            });
        }

        dispatch(layoutsActions.stopLoading());
    }, [dispatch, pageProp, accIdProp, accNameProp, eventIdProp, stoveMemNoProp, stoveNickNoProp,
        IpProp, sessionProp, startDateProp, endDateProp, messageProp, logCountValueProp, logSortValueProp, logSortHistoryProp])

    const onSearch = useCallback(async () => {
        if (!searchInput.startDate && !searchInput.endDate) {
            alert("검색 기간을 선택해 주세요.");
            return;
        }

        if (dayjs.utc(searchInput.endDate).diff(dayjs.utc(searchInput.startDate), 'minute') <= 0) {
            alert(`종료일이 시작일보다 같거나 이전입니다.`);
            return;
        }

        if (dayjs.utc(searchInput.endDate).diff(dayjs.utc(searchInput.startDate), 'day') > 100) {
            alert("검색 기간은 100일 이내로 선택해 주세요.");
            return;
        }

        const parameters = `?page=1&accId=${searchInput.accountId}&accName=${searchInput.accountName}&evnId=${searchInput.eventId}&sMemNo=${searchInput.stoveMemberNo}&sNickNo=${searchInput.stoveNicknameNo}&ip=${searchInput.ipAddress}&sesId=${searchInput.sessionId}&message=${searchInput.message}&startDate=${(searchInput.startDate ? searchInput.startDate.tz().format("YYYY-MM-DD HH:mm:ss") : "")}&endDate=${(searchInput.endDate ? searchInput.endDate.tz().format("YYYY-MM-DD HH:mm:ss") : "")}&lc=${pageLogs}`;

        firstRender.current = true;
        router.push(parameters);
    }, [firstRender, router, searchInput, pageLogs]);

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

                prev.accountId = accIdProp;
                prev.accountName = accNameProp;
                prev.eventId = eventIdProp;
                prev.stoveMemberNo = stoveMemNoProp;
                prev.stoveNicknameNo = stoveNickNoProp;
                prev.ipAddress = IpProp;
                prev.sessionId = sessionProp;
                prev.startDate = startDate;
                prev.endDate = endDate;
                prev.message = messageProp;
                return prev.clone();
            })

            const decodeSortProp = decodingBase62(logSortValueProp);
            if (decodeSortProp.length === sortTypeArray.current.length) {
                decodeSortProp.split("").map((prop, idx) => {
                    const element = Number(prop);
                    if (element >= 0 && element <= 2) {
                        sortTypeArray.current[idx] = element;
                    }
                    else {
                        sortTypeArray.current[idx] = 0;
                    }
                })
            }

            sortedHistory.current = [];
            const decordHistoryProp = decodingBase62(logSortHistoryProp);
            decordHistoryProp?.match(/.{1,2}/g)?.map((element) => {
                const num = Number(element);
                if (num > 0 && num <= 51) sortedHistory.current.push(num);
            });

            loadDatas();
        } else {
            const parameters = `accId=${accIdProp}&accName=${accNameProp}&evnId=${eventIdProp}&sMemNo=${stoveMemNoProp}&sNickNo=${stoveNickNoProp}&ip=${IpProp}&sesId=${sessionProp}&message=${messageProp}&startDate=${startDateProp}&endDate=${endDateProp}&lc=${logCountValueProp}`;

            setDatas(prev => {
                if (lastSortValue.current !== logSortValueProp) {
                    lastSortValue.current = logSortValueProp;

                    if (logSortValueProp !== DefaultSortValue) {
                        let sortCondition = "";
                        sortedHistory.current.forEach((element, index) => {
                            sortCondition = addCondition(element) + sortCondition;
                            if (index !== sortedHistory.current.length - 1) sortCondition = " || " + sortCondition;
                        })

                        prev.totalItems.sort((a, b) => {
                            const sortFunction = new Function("a", "b", "dayjs", `return ${sortCondition}`);
                            return sortFunction(a, b, dayjs);
                        })
                    }
                    else {
                        prev.totalItems.sort((a, b) => dayjs(b.timeStamp).diff(a.timeStamp));
                    }
                }

                return new PaginatedList<Models.ChatLog>(prev.totalItems, pageProp, parameters, prev.pageSize, prev.pageBlockSize);
            });
        }
    }, [dispatch, loadDatas, onSearch, firstRender, pageProp, accIdProp, accNameProp, eventIdProp, stoveMemNoProp, stoveNickNoProp,
        IpProp, sessionProp, startDateProp, endDateProp, messageProp, logCountValueProp, logSortValueProp, logSortHistoryProp]);

    const onChangeData = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>, searchtTYPE: SearchType) => {
        if (searchtTYPE == SearchType.accountId && isNaN(parseInt(e.target.value))) return;

        setSearchInput(prev => {
            switch (searchtTYPE) {
                case SearchType.accountId:
                    prev.accountId = parseInt(e.target.value);
                    break;
                case SearchType.accountName:
                    prev.accountName = e.target.value ?? "";
                    break;
                case SearchType.eventId:
                    prev.eventId = e.target.value ?? "";
                    break;
                case SearchType.stoveMemberNo:
                    prev.stoveMemberNo = e.target.value ?? "";
                    break;
                case SearchType.stoveNicknameNo:
                    prev.stoveNicknameNo = e.target.value ?? "";
                    break;
                case SearchType.sessionId:
                    prev.sessionId = e.target.value ?? "";
                    break;
                case SearchType.ipAddress:
                    prev.ipAddress = e.target.value ?? "";
                    break;
                case SearchType.message:
                    prev.message = e.target.value ?? "";
                    break;
            }
            return prev.clone();
        });
    }, [setSearchInput]);

    const onRemoveData = useCallback((searchtTYPE: SearchType) => {
        setSearchInput(prev => {
            switch (searchtTYPE) {
                case SearchType.accountId:
                    prev.accountId = 0;
                    break;
                case SearchType.accountName:
                    prev.accountName = "";
                    break;
                case SearchType.eventId:
                    prev.eventId = "";
                    break;
                case SearchType.stoveMemberNo:
                    prev.stoveMemberNo = "";
                    break;
                case SearchType.stoveNicknameNo:
                    prev.stoveNicknameNo = "";
                    break;
                case SearchType.sessionId:
                    prev.sessionId = "";
                    break;
                case SearchType.ipAddress:
                    prev.ipAddress = "";
                    break;
                case SearchType.message:
                    prev.message = "";
                    break;
            }
            return prev.clone();
        });
    }, [setSearchInput]);

    const onSubmit = useCallback((e) => {
        if ('Enter' == e.key)
            onSearch();
    }, [onSearch]);

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

    const incrementSortType = (sortStateType: Defines.ChatLogSortType) => {
        const index = sortStateType;

        switch (sortTypeArray.current[index]) {
            case Defines.LogSortState.None:
                sortTypeArray.current[index] = Defines.LogSortState.ASC;
                if (!sortedHistory.current.includes(index)) sortedHistory.current.push(index);
                break;
            case Defines.LogSortState.ASC:
                sortTypeArray.current[index] = Defines.LogSortState.Desc;
                if (!sortedHistory.current.includes(index)) sortedHistory.current.push(index);
                break;
            case Defines.LogSortState.Desc:
                sortTypeArray.current[index] = Defines.LogSortState.None;
                if (sortedHistory.current.includes(index)) {
                    const idx = sortedHistory.current.indexOf(index);
                    if (idx > -1) {
                        sortedHistory.current.splice(idx, 1)
                    }
                }
                break;
        }

        const records = sortedHistory.current.map((num) => num.toString().padStart(2, '0')).join('');
        const encodeRecord = encodingBase62(records);
        recordHistory.current = encodeRecord;
        updatedSortRoute();
    }

    /**
     * @param index 운영 로그 각 자리수 정렬을 위한 index
     * @returns 다중 정렬을 위해 히스토리에 따라 명령어를 반환함
     */
    const addCondition = (index: number) => {
        let condition = "";
        const arrState = sortTypeArray.current[index];
        switch (index) {
            case Defines.ChatLogSortType.AccountId:
                condition = arrState === 1 ? "a.accountId - b.accountId"
                    : arrState === 2 ? "b.accountId - a.accountId" : "";
                break;
            case Defines.ChatLogSortType.AccountName:
                condition = arrState === 1 ? "a.accountName.localeCompare(b.accountName)"
                    : arrState === 2 ? "b.accountName.localeCompare(a.accountName)" : "";
                break;
            case Defines.ChatLogSortType.EventID:
                condition = arrState === 1 ? "a.eventId.localeCompare(b.eventId)"
                    : arrState === 2 ? "b.eventId.localeCompare(a.eventId)" : "";
                break;
            case Defines.ChatLogSortType.StoveMemberNo:
                condition = arrState === 1 ? "a.stoveMemberNo.localeCompare(b.stoveMemberNo)"
                    : arrState === 2 ? "b.stoveMemberNo.localeCompare(a.stoveMemberNo)" : "";
                break;
            case Defines.ChatLogSortType.StoveNicknameNo:
                condition = arrState === 1 ? "a.stoveNicknameNo.localeCompare(b.stoveNicknameNo)"
                    : arrState === 2 ? "b.stoveNicknameNo.localeCompare(a.stoveNicknameNo)" : "";
                break;
            case Defines.ChatLogSortType.SessionId:
                condition = arrState === 1 ? "a.sessionId.localeCompare(b.sessionId)"
                    : arrState === 2 ? "b.sessionId.localeCompare(a.sessionId)" : "";
                break;
            case Defines.ChatLogSortType.IpAddress:
                condition = arrState === 1 ? "a.ipAddress.localeCompare(b.ipAddress)"
                    : arrState === 2 ? "b.ipAddress.localeCompare(a.ipAddress)" : "";
                break;
            case Defines.ChatLogSortType.Message:
                condition = arrState === 1 ? "a.message.localeCompare(b.message)"
                    : arrState === 2 ? "b.message.localeCompare(a.message)" : "";
                break;
            case Defines.ChatLogSortType.TimeStamp:
                condition = arrState === 1 ? "dayjs(a.timeStamp).diff(b.timeStamp)"
                    : arrState === 2 ? "dayjs(b.timeStamp).diff(a.timeStamp)" : "";
                break;
        }
        return condition;
    }

    const updatedSortRoute = () => {
        encodingSortState.current = encodingBase62(sortTypeArray.current.join(""));
        const parameters = `accId=${accIdProp}&accName=${accNameProp}&evnId=${eventIdProp}&sMemNo=${stoveMemNoProp}&sNickNo=${stoveNickNoProp}&ip=${IpProp}&sesId=${sessionProp}&startDate=${startDateProp}&endDate=${endDateProp}&lc=${logCountValueProp}&lsv=${encodingSortState.current}&lsh=${recordHistory.current}`;
        router.push(`?page=${datas.page}&${parameters}`);
        firstRender.current = true;
    };

    const onChangeListCount = (event: SelectChangeEvent<unknown>) => {
        const value = typeof event.target.value === "number" ? event.target.value : parseInt(event.target.value as string, 10);
        if ([10, 25, 50, 100, 200, 500].includes(value)) setPageLogs(value);
    }

    const onDownloadLog = useCallback(async () => {
        if (datas && 0 < datas.totalItems.length) {
            dispatch(layoutsActions.startLoadingMessage(`엑셀을 다운로드 중입니다. 0 / ${datas.totalItems.length}`));

            const sheet: string[] = [...colList];
            const sheets: string[][] = [
                [...sheet]
            ];

            for (let j = 0; j < datas.totalItems.length; j++) {
                const dataInfo = datas.totalItems[j];
                const dataSheet = [];

                for (let k = 0; k < sheet.length; k++) {
                    if (sheet[k].toLowerCase() == "timestamp") dataSheet.push(dayjs(dataInfo[sheet[k]]).tz().format("YYYY-MM-DD HH:mm:ss"))
                    else dataSheet.push(dataInfo[sheet[k]].toString().replaceAll(/(?<=.{1})./g, '*'));
                }

                sheets.push(dataSheet);

                dispatch(layoutsActions.startLoadingMessage(`로그 데이터를 엑셀로 변환 중입니다. ( ${j + 1} / ${datas.totalItems.length} )`));

                if (j % Math.ceil((datas.totalItems.length * 3) / 100) == 0) await new Promise(resolve => setTimeout(resolve, 0));
            }

            const workbook = XLSX.utils.book_new();
            const worksheet = XLSX.utils.aoa_to_sheet(sheets);
            XLSX.utils.book_append_sheet(workbook, worksheet, "Sheet1");

            const csvData = XLSX.utils.sheet_to_csv(worksheet);
            const blob = new Blob([String.fromCharCode(0xFEFF), csvData], { type: 'text/csv;charset=utf-8;' });

            const link = document.createElement('a');
            link.href = URL.createObjectURL(blob);
            link.download = `샘플운영툴_채팅로그_${dayjs(searchInput.startDate).tz().format("YYYY-MM-DD")}_${dayjs(searchInput.endDate).tz().format("YYYY-MM-DD")}.csv`;
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);

            //XLSX.writeFile(workbook,  XLSX.writeFile(workbook, `샘플운영툴_채팅로그_${dayjs(searchInput.startDate).tz().format("YYYY-MM-DD")}_${dayjs(searchInput.endDate).tz().format("YYYY-MM-DD")}.xlsx`));
            dispatch(layoutsActions.stopLoading());
        }
    }, [dispatch, datas, searchInput.startDate, searchInput.endDate]);


    return (
        <Box sx={{ mb: '100px', width: '100%' }}>
            <Box sx={{ mt: 2 }}>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* 채팅로그 검색 기능.</Typography>
            </Box>
            <Grid container justifyContent='center' sx={{ margin: '20px 0 0' }}>
                <Grid item xs={3} sx={{ padding: '10px', flexBasis: '50%', maxWidth: '50%' }}>
                    <FormControl fullWidth variant='outlined' size='small'>
                        <InputLabel htmlFor="accountId-input">AccountID</InputLabel>
                        <OutlinedInput sx={{ paddingRight: 0 }} id="accountId-input" label="AccountID" value={searchInput.accountId} onChange={(e) => onChangeData(e, SearchType.accountId)}
                            onKeyUp={e => onSubmit(e)}
                            endAdornment={
                                <InputAdornment position="end">
                                    <IconButton type="button" sx={{ p: '10px' }} aria-label="accountId-input" onClick={() => onRemoveData(SearchType.accountId)}>
                                        <RemoveIcon />
                                    </IconButton>
                                </InputAdornment>
                            }
                        />
                    </FormControl>
                </Grid>
                <Grid item xs={3} sx={{ padding: '10px', flexBasis: '50%', maxWidth: '50%' }}>
                    <FormControl fullWidth variant='outlined' size='small'>
                        <InputLabel htmlFor="accountName-input">AccountName</InputLabel>
                        <OutlinedInput sx={{ paddingRight: 0 }} id="accountName-input" label="AccountName" value={searchInput.accountName} onChange={(e) => onChangeData(e, SearchType.accountName)}
                            onKeyUp={e => onSubmit(e)}
                            endAdornment={
                                <InputAdornment position="end">
                                    <IconButton type="button" sx={{ p: '10px' }} aria-label="accountName-input" onClick={() => onRemoveData(SearchType.accountName)}>
                                        <RemoveIcon />
                                    </IconButton>
                                </InputAdornment>
                            }
                        />
                    </FormControl>
                </Grid>
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: '50%', maxWidth: '50%' }}>
                    <FormControl fullWidth variant='outlined' size='small'>
                        <InputLabel htmlFor="eventId-input">eventId</InputLabel>
                        <OutlinedInput sx={{ paddingRight: 0 }} id="eventId-input" label="eventId" value={searchInput.eventId} onChange={(e) => onChangeData(e, SearchType.eventId)}
                            onKeyUp={e => onSubmit(e)}
                            endAdornment={
                                <InputAdornment position="end">
                                    <IconButton type="button" sx={{ p: '10px' }} aria-label="eventId-input" onClick={() => onRemoveData(SearchType.eventId)}>
                                        <RemoveIcon />
                                    </IconButton>
                                </InputAdornment>
                            }
                        />
                    </FormControl>
                </Grid>
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: '50%', maxWidth: '50%' }}>
                    <FormControl fullWidth variant='outlined' size='small'>
                        <InputLabel htmlFor="stoveMemberNo-input">stoveMemberNo</InputLabel>
                        <OutlinedInput sx={{ paddingRight: 0 }} id="stoveMemberNo-input" label="stoveMemberNo" value={searchInput.stoveMemberNo} onChange={(e) => onChangeData(e, SearchType.stoveMemberNo)}
                            onKeyUp={e => onSubmit(e)}
                            endAdornment={
                                <InputAdornment position="end">
                                    <IconButton type="button" sx={{ p: '10px' }} aria-label="stoveMemberNo-input" onClick={() => onRemoveData(SearchType.stoveMemberNo)}>
                                        <RemoveIcon />
                                    </IconButton>
                                </InputAdornment>
                            }
                        />
                    </FormControl>
                </Grid>
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: '50%', maxWidth: '50%' }}>
                    <FormControl fullWidth variant='outlined' size='small'>
                        <InputLabel htmlFor="stoveNicknameNo-input">stoveNicknameNo</InputLabel>
                        <OutlinedInput sx={{ paddingRight: 0 }} id="stoveNicknameNo-input" label="stoveNicknameNo" value={searchInput.stoveNicknameNo} onChange={(e) => onChangeData(e, SearchType.stoveNicknameNo)}
                            onKeyUp={e => onSubmit(e)}
                            endAdornment={
                                <InputAdornment position="end">
                                    <IconButton type="button" sx={{ p: '10px' }} aria-label="stoveNicknameNo-input" onClick={() => onRemoveData(SearchType.stoveNicknameNo)}>
                                        <RemoveIcon />
                                    </IconButton>
                                </InputAdornment>
                            }
                        />
                    </FormControl>
                </Grid>
                <Grid item xs={3} sx={{ padding: '10px', flexBasis: '50%', maxWidth: '50%' }}>
                    <FormControl fullWidth variant='outlined' size='small'>
                        <InputLabel htmlFor="message-input">message</InputLabel>
                        <OutlinedInput sx={{ paddingRight: 0 }} id="message-input" label="message" value={searchInput.message} onChange={(e) => onChangeData(e, SearchType.message)}
                            onKeyUp={e => onSubmit(e)}
                            endAdornment={
                                <InputAdornment position="end">
                                    <IconButton type="button" sx={{ p: '10px' }} aria-label="message-input" onClick={() => onRemoveData(SearchType.message)}>
                                        <RemoveIcon />
                                    </IconButton>
                                </InputAdornment>
                            }
                        />
                    </FormControl>
                </Grid>
                <Grid item xs={3} sx={{ padding: '10px', flexBasis: '50%', maxWidth: '50%' }}>
                    <FormControl fullWidth variant='outlined' size='small'>
                        <InputLabel htmlFor="ipAddress-input">ipAddress</InputLabel>
                        <OutlinedInput sx={{ paddingRight: 0 }} id="ipAddress-input" label="ipAddress" value={searchInput.ipAddress} onChange={(e) => onChangeData(e, SearchType.ipAddress)}
                            onKeyUp={e => onSubmit(e)}
                            endAdornment={
                                <InputAdornment position="end">
                                    <IconButton type="button" sx={{ p: '10px' }} aria-label="ipAddress-input" onClick={() => onRemoveData(SearchType.ipAddress)}>
                                        <RemoveIcon />
                                    </IconButton>
                                </InputAdornment>
                            }
                        />
                    </FormControl>
                </Grid>
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: '50%', maxWidth: '50%' }}>
                    <FormControl fullWidth variant='outlined' size='small'>
                        <InputLabel htmlFor="sessionId-input">sessionId</InputLabel>
                        <OutlinedInput sx={{ paddingRight: 0 }} id="sessionId-input" label="sessionId" value={searchInput.sessionId} onChange={(e) => onChangeData(e, SearchType.sessionId)}
                            onKeyUp={e => onSubmit(e)}
                            endAdornment={
                                <InputAdornment position="end">
                                    <IconButton type="button" sx={{ p: '10px' }} aria-label="sessionId-input" onClick={() => onRemoveData(SearchType.sessionId)}>
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
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: '50%', maxWidth: '50%' }}>
                    <FormControl fullWidth>
                        <InputLabel>페이지별 로그 개수</InputLabel>
                        <Select
                            value={pageLogs}
                            label='페이지별 로그 개수'
                            style={{ height: '40px' }}
                            onChange={(event) => { onChangeListCount(event) }}
                        >
                            <MenuItem value={10}>10개</MenuItem>
                            <MenuItem value={25}>25개</MenuItem>
                            <MenuItem value={50}>50개</MenuItem>
                            <MenuItem value={100}>100개</MenuItem>
                            <MenuItem value={200}>200개</MenuItem>
                            <MenuItem value={500}>500개</MenuItem>
                        </Select>
                    </FormControl>
                </Grid>
            </Grid>
            <Grid container justifyContent='center'>
                <Grid item xs={10} sx={{ padding: '10px', textAlign: 'center' }}>
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onSearch}>검색</Button>
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='success' size="small" onClick={async () => { await onDownloadLog(); }} sx={{ ml: 1 }}>엑셀</Button>
                </Grid>
            </Grid>
            <Paging datas={datas} />
            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 600 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>채팅 로그</Typography>
                </Toolbar>
                <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                    <Contents datas={datas} incrementSortType={incrementSortType} history={sortTypeArray.current} />
                </Table>
            </TableContainer>
            <Paging datas={datas} />
        </Box>
    )
}

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
    const { req, query } = context;
    const pageString: string = query.page ? query.page.toString().trim() : '1';
    const accountIDString: string = query.accId ? query.accId.toString().trim() : '0';
    const accName: string = query.accName ? query.accName.toString() : '';
    const evnId: string = query.evnId ? query.evnId.toString() : '';
    const sMemNo: string = query.sMemNo ? query.sMemNo.toString() : '';
    const sNickNo: string = query.sNickNo ? query.sNickNo.toString() : '';
    const ip: string = query.ip ? query.ip.toString() : '';
    const sesId: string = query.sesId ? query.sesId.toString() : '';
    const message: string = query.message ? query.message.toString() : '';
    const startDateString: string = query.startDate ? query.startDate.toString() : "";
    const endDateString: string = query.endDate ? query.endDate.toString() : "";
    let page = 1;
    let accId = 0;
    let startDate = dayjs(startDateString).isValid() ? dayjs.tz(startDateString).format("YYYY-MM-DD HH:mm:ss") : "";
    let endDate = dayjs(endDateString).isValid() ? dayjs.tz(endDateString).format("YYYY-MM-DD HH:mm:ss") : "";
    const pageLogCountValue: string = query.lc ? query.lc.toString() : '10';
    const logSort: string = query.lsv ? query.lsv.toString() : "0000000000"
    const logSortHistory: string = query.lsh ? query.lsh.toString() : '';
    try {
        page = !pageString.match(/[^\d]/g) && !isNaN(parseInt(pageString)) ? parseInt(pageString) : 1;
        accId = !accountIDString.match(/[^\d]/g) && !isNaN(parseInt(accountIDString)) ? parseInt(accountIDString) : 0;

    } catch (error) {
        if ("production" != process.env.NODE_ENV)
            console.log(error);
    }

    return {
        props: {
            pageProp: page, accIdProp: accId, accNameProp: accName, eventIdProp: evnId,
            stoveMemNoProp: sMemNo, stoveNickNoProp: sNickNo, IpProp: ip, sessionProp: sesId,
            startDateProp: startDate, endDateProp: endDate, messageProp: message, logCountValueProp: pageLogCountValue,
            logSortValueProp: logSort, logSortHistoryProp: logSortHistory
        }
    };
}

export default Page;