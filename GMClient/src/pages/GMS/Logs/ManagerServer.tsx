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
const Contents = dynamic(() => import('@components/GMS/Logs/ManagerServer/Contents'), { ssr: false });
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
    logTypeProp: string;
    logDataProp: string;
    startDateProp: string;
    endDateProp: string;
    logCountValueProp: string;
    logSortValueProp: string;
    logSortHistoryProp: string;
}

class SearchInput {
    logType: string;
    logData: string;
    startDate?: Dayjs | null;
    endDate?: Dayjs | null;

    constructor(logType?: string, logData?: string, startDate?: Dayjs | null, endDate?: Dayjs | null) {
        this.logType = logType ?? "";
        this.logData = logData ?? "";
        this.startDate = startDate;
        this.endDate = endDate;
    }

    clone = () => new SearchInput(this.logType, this.logData, this.startDate, this.endDate);
}

const encodingBase62 = (str: string) => {
    const encode = base62.encode(Buffer.from(str, "hex"));
    return encode;
}

const decodingBase62 = (str: string) => {
    const decode = Buffer.from(base62.decode(str)).toString("hex");
    return decode;
}

const colList = Object.keys(new Models.ManagerServerLog());

function Page({ pageProp, logTypeProp, logDataProp, startDateProp, endDateProp, logCountValueProp, logSortValueProp, logSortHistoryProp }: PageProps) {
    const DefaultSortValue = "0000";
    const lastSortValue = useRef(logSortValueProp ?? "0000");

    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const dispatch = useAppDispatch();
    const firstRender = useRef(true);
    const router = useRouter();

    const sortTypeArray = useRef<Defines.LogSortState[]>(Array(8).fill(Defines.LogSortState.None));
    const sortedHistory = useRef<number[]>([]);
    const encodingSortState = useRef<string>(encodingBase62(sortTypeArray.current.join("")));
    const recordHistory = useRef<string>("");


    const [datas, setDatas] = useState<PaginatedList<Models.ManagerServerLog>>(new PaginatedList<Models.ManagerServerLog>([]));
    const [searchInput, setSearchInput] = useState<SearchInput>(new SearchInput("", ""));

    const [pageLogs, setPageLogs] = useState<number>(10);

    const loadDatas = useCallback(async () => {
        setDatas(new PaginatedList<Models.ManagerServerLog>([]));

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
        const response = await ManagerAPI.ManagerServerLogsAsync({
            startTime: startDateProp ? dayjs.tz(startDateProp).utc().format("YYYY-MM-DD") : dayjs.utc().format("YYYY-MM-DD"),
            endTime: endDateProp ? dayjs.tz(endDateProp).utc().add(1, 'day').format("YYYY-MM-DD") : dayjs.utc().add(1, 'day').format("YYYY-MM-DD")
        });

        if (!response.result) {
            switch (response.error) {
                case Errors.ManagerLogs_NotFoundData:
                    alert(`검색된 데이터가 없습니다. (${Errors[response.error]})`);
                    break;

                default:
                    alert(`검색 실패 (${Errors[response.error]})`);
                    break;
            }
            dispatch(layoutsActions.stopLoading());

            return;
        }

        console.log(response.managerServerLogs);

        const parameters = `logType=${logTypeProp}&logData=${logDataProp}&startDate=${startDateProp}&endDate=${endDateProp}&lc=${logCountValueProp}&lsv=${logSortValueProp}&lsh=${logSortHistoryProp}`;

        if (null != response.managerServerLogs && 0 < response.managerServerLogs.length) {
            let searchedDatas = response.managerServerLogs;
            if (logTypeProp)
                searchedDatas = searchedDatas.filter(_ => _.logType.toLowerCase().includes(logTypeProp.toLowerCase()));

            if (logDataProp)
                searchedDatas = searchedDatas.filter(_ => _.logData.toLowerCase().includes(logDataProp.toLowerCase()));

            if (startDateProp || endDateProp) {
                let startDayJS = dayjs.utc().add(-1, 'day');
                if (startDateProp) {
                    try {
                        startDayJS = dayjs.tz(startDateProp);
                    } catch (error) {
                        startDayJS = dayjs.utc().add(-1, 'day');
                    }
                }
                let endDayJS = dayjs.utc().add(1, 'day');
                if (endDateProp) {
                    try {
                        endDayJS = dayjs.tz(endDateProp);
                    } catch (error) {
                        endDayJS = dayjs.utc();
                    }
                }
                searchedDatas = searchedDatas.filter(_ => dayjs(_.logTime).isAfter(startDayJS) && dayjs(_.logTime).isBefore(endDayJS.add(1, "day")));
            }

            let pageSize = 10;
            if (logCountValueProp) {
                let value = parseInt(logCountValueProp);
                if ([10, 25, 50, 100, 200, 500].includes(value)) {
                    pageSize = value;
                    setPageLogs(value);
                };
            }

            searchedDatas = searchedDatas.sort((a, b) => dayjs(b.logTime).diff(a.logTime));

            if (logSortValueProp) {
                let sortCondition = "";
                sortedHistory.current.forEach((element, index) => {
                    sortCondition = addCondtion(element) + sortCondition;
                    if (index !== sortedHistory.current.length - 1) sortCondition = " || " + sortCondition;
                })

                searchedDatas.sort((a, b) => {
                    const sortFunction = new Function("a", "b", "dayjs", `return ${sortCondition}`);
                    return sortFunction(a, b, dayjs);
                })
            }

            setDatas(prev => {
                return new PaginatedList<Models.ManagerServerLog>(searchedDatas, pageProp, parameters, pageSize, prev.pageBlockSize);
            });
        }

        dispatch(layoutsActions.stopLoading());
    }, [setDatas, dispatch, pageProp, logTypeProp, logDataProp, startDateProp, endDateProp, logCountValueProp, logSortValueProp, logSortHistoryProp]);

    const onSearch = useCallback(async () => {
        if (!searchInput.startDate && !searchInput.endDate) {
            alert("검색 기간을 선택해 주세요.");
            return;
        }

        if (dayjs.utc(searchInput.endDate).diff(dayjs.utc(searchInput.startDate), 'minute') <= 0) {
            alert(`종료일이 시작일보다 같거나 이전입니다.`);
            return;
        }

        if (dayjs.utc(searchInput.endDate).diff(dayjs.utc(searchInput.startDate), 'day') > 28) {
            alert("검색 기간은 28일 이내로 선택해 주세요.");
            return;
        }

        const parameters = `?page=1&logType=${searchInput.logType}&logData=${searchInput.logData}&startDate=${(searchInput.startDate ? searchInput.startDate.tz().format("YYYY-MM-DD HH:mm:ss") : "")}&endDate=${(searchInput.endDate ? searchInput.endDate.tz().format("YYYY-MM-DD HH:mm:ss") : "")}&lc=${pageLogs}&lsv=${encodingSortState.current}&lsh=${recordHistory.current}`;

        firstRender.current = true;
        router.push(parameters);

    }, [firstRender, router, searchInput, pageLogs]);

    const onChangeLogType = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setSearchInput(prev => {
            prev.logType = e.target.value ?? "";
            return prev.clone();
        });
    }, [setSearchInput]);

    const onRemoveLogType = useCallback(() => {
        setSearchInput(prev => {
            prev.logType = "";
            return prev.clone();
        });
    }, [setSearchInput]);

    const onChangeLogData = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setSearchInput(prev => {
            prev.logData = e.target.value ?? "";
            return prev.clone();
        });
    }, [setSearchInput]);

    const onRemoveLogData = useCallback(() => {
        setSearchInput(prev => {
            prev.logData = "";
            return prev.clone();
        });
    }, [setSearchInput]);

    const onSubmit = useCallback((e) => {
        if ('Enter' == e.key)
            onSearch();
    }, [onSearch]);

    const onDownloadLog = useCallback(async () => {
        dispatch(layoutsActions.startLoadingMessage(`엑셀을 다운로드 중입니다. 0 / ${datas.totalItems.length}`));
        if (datas && 0 < datas.totalItems.length) {
            const sheet: string[] = [...colList];
            const sheets: string[][] = [
                [...sheet]
            ];

            for (let j = 0; j < datas.totalItems.length; j++) {
                const dataInfo = datas.totalItems[j];
                const dataSheet = [];

                for (let k = 0; k < sheet.length; k++) {
                    if (sheet[k] == "logTime")
                        dataSheet.push(dayjs(dataInfo[sheet[k]]).tz().format("YYYY-MM-DD HH:mm:ss"))
                    else if (dataInfo[sheet[k]])
                        dataSheet.push(dataInfo[sheet[k]].toString().replaceAll(/(?<=.{5})./g, '*'));
                    else
                        dataSheet.push('');
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
            link.download = `${homepageSettings.siteName.replaceAll(' ', '_')}_서버_로그_${dayjs(searchInput.startDate).tz().format("YYYY-MM-DD")}_${dayjs(searchInput.endDate).tz().format("YYYY-MM-DD")}.csv`;
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        }
        dispatch(layoutsActions.stopLoading());
    }, [dispatch, datas, searchInput.startDate, searchInput.endDate, homepageSettings]);

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

                prev.logType = logTypeProp;
                prev.logData = logDataProp;
                prev.startDate = startDate.startOf('day');
                prev.endDate = endDate;
                return prev.clone();
            });

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
            const parameters = `logType=${logTypeProp}&logData=${logDataProp}&startDate=${startDateProp}&endDate=${endDateProp}&lc=${logCountValueProp}&lsv=${logSortValueProp}&lsh=${logSortHistoryProp}`;
            setDatas(prev => {
                if (lastSortValue.current !== logSortValueProp) {
                    lastSortValue.current = logSortValueProp;

                    if (logSortValueProp !== DefaultSortValue) {
                        let sortCondition = "";
                        sortedHistory.current.forEach((element, index) => {
                            sortCondition = addCondtion(element) + sortCondition;
                            if (index !== sortedHistory.current.length - 1) sortCondition = " || " + sortCondition;
                        })

                        prev.totalItems.sort((a, b) => {
                            const sortFunction = new Function("a", "b", "dayjs", `return ${sortCondition}`);
                            return sortFunction(a, b, dayjs);
                        })
                    }
                    else {
                        prev.totalItems.sort((a, b) => dayjs(b.logTime).diff(a.logTime));
                    }
                }

                return new PaginatedList<Models.ManagerServerLog>(prev.totalItems, pageProp, parameters, prev.pageSize, prev.pageBlockSize);
            });
        }

    }, [firstRender, loadDatas, setSearchInput, dispatch, onSearch, pageProp, logTypeProp, logDataProp, startDateProp, endDateProp, logCountValueProp, logSortValueProp, logSortHistoryProp]);
    //#endregion

    const incrementSortType = (sortStateType: Defines.ManageLogSortType) => {
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
    const addCondtion = (index: number) => {
        let condition = "";
        const arrState = sortTypeArray.current[index];
        switch (index) {
            case Defines.ManageLogSortType.UserName:
                condition = arrState === 1 ? "a.userName.localeCompare(b.userName)"
                    : arrState === 2 ? "b.userName.localeCompare(a.userName)" : "";
                break;
            case Defines.ManageLogSortType.MethodName:
                condition = arrState === 1 ? "a.methodName.localeCompare(b.methodName)"
                    : arrState === 2 ? "b.methodName.localeCompare(a.methodName)" : "";
                break;
            case Defines.ManageLogSortType.UrlName:
                condition = arrState === 1 ? "a.urlName.localeCompare(b.urlName)"
                    : arrState === 2 ? "b.urlName.localeCompare(a.urlName)" : "";
                break;
            case Defines.ManageLogSortType.RegTime:
                condition = arrState === 1 ? "dayjs(a.regTime).diff(b.regTime)"
                    : arrState === 2 ? "dayjs(b.regTime).diff(a.regTime)" : "";
                break;
            case Defines.ManageLogSortType.ErrorName:
                condition = arrState === 1 ? "a.errorName.localeCompare(b.errorName)"
                    : arrState === 2 ? "b.errorName.localeCompare(a.errorName)" : "";
                break;
            case Defines.ManageLogSortType.Message:
                condition = arrState === 1 ? "a.message.localeCompare(b.message)"
                    : arrState === 2 ? "b.message.localeCompare(a.message)" : "";
                break;

        }
        //console.log("Manage 추가 명령어 : ", condition);
        return condition;
    }

    const updatedSortRoute = () => {
        encodingSortState.current = encodingBase62(sortTypeArray.current.join(""));
        const parameters = `logType=${logTypeProp}&logData=${logDataProp}&startDate=${startDateProp}&endDate=${endDateProp}&lc=${logCountValueProp}&lsv=${encodingSortState.current}&lsh=${recordHistory.current}`;
        router.push(`?page=${datas.page}&${parameters}`);
        firstRender.current = true;
    };

    const handleChange = (event: SelectChangeEvent<unknown>) => {
        const value = typeof event.target.value === "number" ? event.target.value : parseInt(event.target.value as string, 10);
        if ([10, 25, 50, 100, 200, 500].includes(value)) setPageLogs(value);
    }

    return (
        <Box sx={{ mb: '100px', width: '100%' }}>
            <Box sx={{ mt: 2 }}>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* 운영툴 서버로그 검색 기능(디버깅용).</Typography>
            </Box>
            <Grid container justifyContent='center' sx={{ margin: '20px 0 0' }}>
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: '50%', maxWidth: '50%' }}>
                    <FormControl fullWidth variant='outlined' size='small'>
                        <InputLabel htmlFor="logType-input">로그레벨</InputLabel>
                        <OutlinedInput sx={{ paddingRight: 0 }} id="logType-input" label="로그레벨" value={searchInput.logType} onChange={(e) => onChangeLogType(e)}
                            onKeyUp={e => onSubmit(e)}
                            endAdornment={
                                <InputAdornment position="end">
                                    <IconButton type="button" sx={{ p: '10px' }} aria-label="logType-input" onClick={onRemoveLogType}>
                                        <RemoveIcon />
                                    </IconButton>
                                </InputAdornment>
                            }
                        />
                    </FormControl>
                </Grid>
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: '50%', maxWidth: '50%' }}>
                    <FormControl fullWidth variant='outlined' size='small'>
                        <InputLabel htmlFor="logData-input">내용</InputLabel>
                        <OutlinedInput sx={{ paddingRight: 0 }} id="logData-input" label="내용" value={searchInput.logData} onChange={(e) => onChangeLogData(e)}
                            onKeyUp={e => onSubmit(e)}
                            endAdornment={
                                <InputAdornment position="end">
                                    <IconButton type="button" sx={{ p: '10px' }} aria-label="logData-input" onClick={onRemoveLogData}>
                                        <RemoveIcon />
                                    </IconButton>
                                </InputAdornment>
                            }
                        />
                    </FormControl>
                </Grid>
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: '50%', maxWidth: '50%' }}>
                    <DateTimePicker label={`시작일(${timezoneName})`} value={(searchInput.startDate && dayjs(searchInput.startDate).isValid() ? dayjs.utc(searchInput.startDate).tz().format("YYYY-MM-DD") : null)} onChange={(date) => onChangeStartDate(date)} />
                </Grid>
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: '50%', maxWidth: '50%' }}>
                    <DateTimePicker label={`종료일(${timezoneName})`} value={(searchInput.endDate && dayjs(searchInput.endDate).isValid() ? dayjs.utc(searchInput.endDate).tz().format("YYYY-MM-DD") : null)} onChange={(date) => onChangeEndDate(date)} />
                </Grid>
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: '50%', maxWidth: '50%' }}>
                    <FormControl fullWidth>
                        <InputLabel>페이지별 로그 개수</InputLabel>
                        <Select
                            value={pageLogs}
                            label='페이지별 로그 개수'
                            style={{ height: '40px' }}
                            onChange={(event) => { handleChange(event) }}
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
                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>운영툴 서버 로그</Typography>
                </Toolbar>
                <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                    <Contents datas={datas} incrementSortType={incrementSortType} history={sortTypeArray.current} />
                </Table>
            </TableContainer>
            <Paging datas={datas} />
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
    const { req, query } = context;
    const pageString: string = query.page ? query.page.toString().trim() : '1';
    // const accountID: string = query.accountID ? query.accountID.toString() : '';
    // const stoveMemberNO: string = query.stoveMemberNO ? query.stoveMemberNO.toString() : '';
    const logType: string = query.logType ? query.logType.toString() : '';
    const logData: string = query.logData ? query.logData.toString() : '';
    const startDateString: string = query.startDate ? query.startDate.toString() : "";
    const endDateString: string = query.endDate ? query.endDate.toString() : "";
    let page = 1;
    let startDate = dayjs(startDateString).isValid() ? dayjs.tz(startDateString).format("YYYY-MM-DD HH:mm:ss") : "";
    let endDate = dayjs(endDateString).isValid() ? dayjs.tz(endDateString).format("YYYY-MM-DD HH:mm:ss") : "";
    const pageLogCountValue: string = query.lc ? query.lc.toString() : '10';
    const logSort: string = query.lsv ? query.lsv.toString() : "0000"
    const logSortHistory: string = query.lsh ? query.lsh.toString() : '';

    try {
        page = !pageString.match(/[^\d]/g) && !isNaN(parseInt(pageString)) ? parseInt(pageString) : 1;
    } catch (error) {
        if ("production" != process.env.NODE_ENV)
            console.log(error);
    }

    return {
        props: { pageProp: page, logTypeProp: logType, logDataProp: logData, startDateProp: startDate, endDateProp: endDate, logCountValueProp: pageLogCountValue, logSortValueProp: logSort, logSortHistoryProp: logSortHistory },
    };
}

export default Page;