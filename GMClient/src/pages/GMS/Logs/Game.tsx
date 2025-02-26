import { ReactElement, useCallback, useEffect, useRef, useState } from 'react';
import { useAppDispatch, useAppSelector } from "@hooks/index";
import { GetServerSideProps } from 'next';
import base from 'base-x';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import * as layoutsActions from '@store/reducers/layouts';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer } from '@mui/material';
import { Models } from '@ngel/data/models';
import { ManagerAPI } from '@ngel/data/apis/managerAPI';
import deepmerge from 'deepmerge';
import { PaginatedList } from '@helpers/paging';
import { useRouter } from 'next/router';
import dynamic from 'next/dynamic';
import { Defines } from '@ngel/data/autoDefines';
import isEmpty from 'lodash/isEmpty';
import * as XLSX from 'xlsx';
import { SelectChangeEvent } from '@mui/material/Select';
import { DataTableModels } from '@ngel/data/tables/model';
import { Errors } from '@ngel/data/autoErrors';

const EventIdSearch = dynamic(() => import('@components/layouts/ui/uiEventIdSearch'), { ssr: false });
const GameLogSearch = dynamic(() => import('@components/layouts/ui/uiGameLogSearch'), { ssr: false });
const UserSearch = dynamic(() => import('@components/layouts/ui/uiUserSearch'), { ssr: false });
const Layout = dynamic(() => import('@components/layouts'), { ssr: false });
const ThemeLayout = dynamic(() => import('@components/layouts/theme'), { ssr: false });
const ManageLayout = dynamic(() => import('@components/layouts/manage'), { ssr: false });
const Contents = dynamic(() => import('@components/GMS/Logs/Game/Contents'), { ssr: false });
const ColVisibleControl = dynamic(() => import('@components/GMS/Logs/Game/ColVisibleControl'), { ssr: false });
const Paging = dynamic(() => import('@components/ui/Paging'), { ssr: false });
const DateTimePicker = dynamic(() => import('@components/ui/DateTimePicker'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const InputLabel = dynamic(() => import('@mui/material/InputLabel'), { ssr: false });
const Select = dynamic(() => import('@mui/material/Select'), { ssr: false });
const MenuItem = dynamic(() => import('@mui/material/MenuItem'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const Chip = dynamic(() => import('@mui/material/Chip'), { ssr: false });

const base62 = base('0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ');

interface PageProps {
    pageProp: number;
    userSearchTypeProp: string;
    userSearchValueProp: string;
    gameLogSearchTypeProp: string;
    gameLogSearchValueProp: string;
    eventIdSearchTypeProp: string;
    startDateProp: string;
    endDateProp: string;
    logCountValueProp: string;
    logSortValueProp: string;
    logSortHistoryProp: string;
}

class SearchInput {
    userSearchType: Defines.UserSearchType;
    userSearchValue: string;
    gameLogSearchType: Defines.GameLogSearchType;
    gameLogSearchValue: string;
    eventIdSearchType: string;
    startDate?: Dayjs | null;
    endDate?: Dayjs | null;

    constructor(userSearchType?: Defines.UserSearchType, userSearchValue?: string, gameLogSearchType?: Defines.GameLogSearchType, gameLogSearchValue?: string, eventIdSearchType?: string, startDate?: Dayjs | null, endDate?: Dayjs | null) {
        this.userSearchType = userSearchType ?? Defines.UserSearchType.AccountID;
        this.userSearchValue = userSearchValue ?? "";
        this.gameLogSearchType = gameLogSearchType ?? Defines.GameLogSearchType.EventGroupID;
        this.gameLogSearchValue = gameLogSearchValue ?? "";
        this.eventIdSearchType = eventIdSearchType ?? "";
        this.startDate = startDate;
        this.endDate = endDate;
    }

    clone = () => new SearchInput(this.userSearchType, this.userSearchValue, this.gameLogSearchType, this.gameLogSearchValue, this.eventIdSearchType, this.startDate, this.endDate);
}

const encodingBase62 = (str: string) => {
    const encode = base62.encode(Buffer.from(str, "hex"));
    return encode;
}

const decodingBase62 = (str: string) => {
    const decode = Buffer.from(base62.decode(str)).toString("hex");
    return decode;
}

const colList = Object.keys(new Models.BiskitLog());

function Page({ pageProp, userSearchTypeProp, userSearchValueProp, gameLogSearchTypeProp, gameLogSearchValueProp, eventIdSearchTypeProp, startDateProp, endDateProp, logCountValueProp, logSortValueProp, logSortHistoryProp }: PageProps) {
    const DefaultSortValue = "00000000000000000000000000";
    const lastSortValue = useRef(logSortValueProp ?? "00000000000000000000000000");

    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const tables = useAppSelector(state => state.tables);
    const eventIdTable = tables?.biskitLogEventIDTable;
    const dispatch = useAppDispatch();
    const firstRender = useRef(true);
    const router = useRouter();

    const sortTypeArray = useRef<Defines.LogSortState[]>(Array(52).fill(Defines.LogSortState.None));
    const sortedHistory = useRef<number[]>([]);
    const encodingSortState = useRef<string>(encodingBase62(sortTypeArray.current.join("")));
    const recordHistory = useRef<string>("");
    const [eventIDSearchList, setEventIDSearchList] = useState<number[]>(Array((Object.keys(eventIdTable).length) ?? 0).fill(0));
    const [colVisibleState, setColVisibleState] = useState<number[]>(Array(51).fill(1));

    const [datas, setDatas] = useState<PaginatedList<Models.BiskitLog>>(new PaginatedList<Models.BiskitLog>([]));
    const [searchInput, setSearchInput] = useState<SearchInput>(new SearchInput(Defines.UserSearchType.AccountID, "", Defines.GameLogSearchType.EventGroupID, "", ""));

    const [pageLogs, setPageLogs] = useState<number>(10);

    const loadDatas = useCallback(async () => {
        setDatas(new PaginatedList<Models.BiskitLog>([]));

        if (isEmpty(startDateProp) && isEmpty(endDateProp)) {
            return;
        }

        if (isEmpty(userSearchTypeProp) && isEmpty(userSearchValueProp) && isEmpty(eventIdSearchTypeProp)) {
            return;
        }

        let userSearchType: Defines.UserSearchType = 0;
        try {
            userSearchType = parseInt(userSearchTypeProp);
        } catch (error) {
            if (process.env.NODE_ENV !== 'production')
                console.log(error);

            return;
        }

        const eventIdSearchPropList: string[] = [];

        for (let i = 0; i < eventIDSearchList.length; i++) {
            if (eventIDSearchList[i] == 0) continue;
            eventIdSearchPropList.push(eventIdTable[Object.keys(eventIdTable)[i]]?.EventID);
        }

        if (isEmpty(userSearchValueProp) && eventIdSearchPropList.length < 1) {
            return;
        }
        dispatch(layoutsActions.startLoadingMessage("로그 데이터를 불러오는 중입니다."));

        const response = await ManagerAPI.BiskitLogsAsync({
            startDate: startDateProp ? dayjs.tz(startDateProp).utc().format("YYYY-MM-DD HH:mm") + ":00" : dayjs.utc().format("YYYY-MM-DD"),
            endDate: endDateProp ? dayjs.tz(endDateProp).utc().format("YYYY-MM-DD HH:mm") + ":59" : dayjs.utc().add(1, 'day').format("YYYY-MM-DD"),
            userSearchType: userSearchType,
            userSearchValue: userSearchValueProp,
            eventIdSearchType: eventIdSearchPropList
        });

        if (!response.result && response.error !== Errors.BiskitLogs_NotFoundData) {
            if ("production" != process.env.NODE_ENV)
                console.log(response.error);

            switch (response.error) {
                /*case Errors.BiskitLogs_NotFoundData:
                    alert(`검색된 데이터가 없습니다. (${Errors[response.error]})`);
                    break;*/

                case Errors.BiskitLogs_TooManyData:
                    alert(`검색된 데이터가 너무 많습니다.\n검색 범위를 좁혀주세요.\n(${Errors[response.error]})`);
                    break;

                default:
                    alert(`검색 실패 (${Errors[response.error]})`);
                    break;
            }
            dispatch(layoutsActions.stopLoading());
            return;
        }

        const parameters = `userSearchType=${userSearchTypeProp}&userSearchValue=${userSearchValueProp}&gameLogSearchType=${gameLogSearchTypeProp}&gameLogSearchValue=${gameLogSearchValueProp}&eventIdSearchType=${eventIdSearchTypeProp}&startDate=${startDateProp}&endDate=${endDateProp}&lc=${logCountValueProp}&lsv=${logSortValueProp}&lsh=${logSortHistoryProp}`;

        if (null != response.biskitLogs && 0 < response.biskitLogs.length) {
            let searchedDatas = response.biskitLogs;
            if (userSearchValueProp) {
                switch (userSearchTypeProp) {
                    case Defines.UserSearchType.AccountID.toString():
                        let accountID = 0;
                        if (!userSearchValueProp.trim().match(/[^\d]/g) && !isNaN(parseInt(userSearchValueProp.trim())))
                            accountID = parseInt(userSearchValueProp.trim());
                        searchedDatas = searchedDatas.filter(_ => _.accountID == accountID);
                        break;

                    case Defines.UserSearchType.Nick.toString():
                        searchedDatas = searchedDatas.filter(_ => _.accountName.toLowerCase().includes(userSearchValueProp.trim().toLowerCase()));
                        break;

                    case Defines.UserSearchType.StoveMemberNo.toString():
                        let stoveMemberNo = 0;
                        if (!userSearchValueProp.trim().match(/[^\d]/g) && !isNaN(parseInt(userSearchValueProp.trim())))
                            stoveMemberNo = parseInt(userSearchValueProp.trim());
                        searchedDatas = searchedDatas.filter(_ => _.stoveMemberNO == stoveMemberNo);
                        break;

                    case Defines.UserSearchType.StoveNickNameNo.toString():
                        let stoveNickNameNo = 0;
                        if (!userSearchValueProp.trim().match(/[^\d]/g) && !isNaN(parseInt(userSearchValueProp.trim())))
                            stoveNickNameNo = parseInt(userSearchValueProp.trim());
                        searchedDatas = searchedDatas.filter(_ => _.stoveNickNameNO == stoveNickNameNo);
                        break;
                }
            }

            if (gameLogSearchValueProp) {
                switch (gameLogSearchTypeProp) {
                    case Defines.GameLogSearchType.EventGroupID.toString():
                        searchedDatas = searchedDatas.filter(_ => _.eventGroupID.toLowerCase().includes(gameLogSearchValueProp.trim().toLowerCase()));
                        break;

                    case Defines.GameLogSearchType.EventID.toString():
                        searchedDatas = searchedDatas.filter(_ => _.eventID.toLowerCase().includes(gameLogSearchValueProp.trim().toLowerCase()));
                        break;

                    case Defines.GameLogSearchType.SequenceNumber.toString():
                        let sequenceNumber = 0;
                        if (!gameLogSearchValueProp.trim().match(/[^\d]/g) && !isNaN(parseInt(gameLogSearchValueProp.trim())))
                            sequenceNumber = parseInt(gameLogSearchValueProp.trim());
                        searchedDatas = searchedDatas.filter(_ => _.sequenceNumber == sequenceNumber);
                        break;

                    case Defines.GameLogSearchType.AccountLevel.toString():
                        let accountLevel = 0;
                        if (!gameLogSearchValueProp.trim().match(/[^\d]/g) && !isNaN(parseInt(gameLogSearchValueProp.trim())))
                            accountLevel = parseInt(gameLogSearchValueProp.trim());
                        searchedDatas = searchedDatas.filter(_ => _.accountLevel == accountLevel);
                        break;

                    case Defines.GameLogSearchType.CharacterID.toString():
                        let characterID = 0;
                        if (!gameLogSearchValueProp.trim().match(/[^\d]/g) && !isNaN(parseInt(gameLogSearchValueProp.trim())))
                            characterID = parseInt(gameLogSearchValueProp.trim());
                        searchedDatas = searchedDatas.filter(_ => _.characterID == characterID);
                        break;

                    case Defines.GameLogSearchType.CharacterLevel.toString():
                        let characterLevel = 0;
                        if (!gameLogSearchValueProp.trim().match(/[^\d]/g) && !isNaN(parseInt(gameLogSearchValueProp.trim())))
                            characterLevel = parseInt(gameLogSearchValueProp.trim());
                        searchedDatas = searchedDatas.filter(_ => _.characterLevel == characterLevel);
                        break;

                    case Defines.GameLogSearchType.SessionID.toString():
                        searchedDatas = searchedDatas.filter(_ => _.sessionID.toLowerCase().includes(gameLogSearchValueProp.trim().toLowerCase()));
                        break;

                    case Defines.GameLogSearchType.ServerCode.toString():
                        searchedDatas = searchedDatas.filter(_ => _.serverCode.toLowerCase().includes(gameLogSearchValueProp.trim().toLowerCase()));
                        break;

                    case Defines.GameLogSearchType.ChannelCode.toString():
                        searchedDatas = searchedDatas.filter(_ => _.channelCode.toLowerCase().includes(gameLogSearchValueProp.trim().toLowerCase()));
                        break;

                    case Defines.GameLogSearchType.IP.toString():
                        searchedDatas = searchedDatas.filter(_ => _.ipAddress.toLowerCase().includes(gameLogSearchValueProp.trim().toLowerCase()));
                        break;

                    case Defines.GameLogSearchType.DeviceID.toString():
                        searchedDatas = searchedDatas.filter(_ => _.deviceID.toLowerCase().includes(gameLogSearchValueProp.trim().toLowerCase()));
                        break;

                    case Defines.GameLogSearchType.DeviceType.toString():
                        searchedDatas = searchedDatas.filter(_ => _.deviceType.toLowerCase().includes(gameLogSearchValueProp.trim().toLowerCase()));
                        break;

                    case Defines.GameLogSearchType.DeviceModel.toString():
                        searchedDatas = searchedDatas.filter(_ => _.deviceModel.toLowerCase().includes(gameLogSearchValueProp.trim().toLowerCase()));
                        break;

                    case Defines.GameLogSearchType.OS.toString():
                        searchedDatas = searchedDatas.filter(_ => _.os.toLowerCase().includes(gameLogSearchValueProp.trim().toLowerCase()));
                        break;
                }
            }

            if (startDateProp || endDateProp) {
                let startDayJS = dayjs.utc().add(-1, 'day');
                if (startDateProp) {
                    try {
                        startDayJS = dayjs.tz(startDateProp);
                    } catch (error) {
                        startDayJS = dayjs.utc();
                    }
                }

                let endDayJS = dayjs.utc().add(1, 'day');
                if (endDateProp) {
                    try {
                        endDayJS = dayjs.tz(endDateProp);
                    } catch (error) {
                        endDayJS = dayjs.utc().add(1, 'day');
                    }
                }

                searchedDatas = searchedDatas.filter(_ => dayjs(_.timestamp).isAfter(startDayJS) && dayjs(_.timestamp).isBefore(endDayJS.add(1, "day")));
            }

            let pageSize = 10;
            if (logCountValueProp) {
                let value = parseInt(logCountValueProp);
                if ([10, 25, 50, 100, 200, 500].includes(value)) {
                    pageSize = value;
                    setPageLogs(value);
                };
            }

            searchedDatas = searchedDatas.sort((a, b) => dayjs(b.timestamp).diff(a.timestamp));

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
                return new PaginatedList<Models.BiskitLog>(searchedDatas, pageProp, parameters, pageSize, prev.pageBlockSize);
            });
        }

        dispatch(layoutsActions.stopLoading());
    }, [eventIdTable, eventIDSearchList, setDatas, dispatch, pageProp, userSearchTypeProp, userSearchValueProp, gameLogSearchTypeProp, gameLogSearchValueProp, eventIdSearchTypeProp, startDateProp, endDateProp, logCountValueProp, logSortValueProp, logSortHistoryProp]);

    const onSearch = useCallback(async () => {
        if (!searchInput.startDate && !searchInput.endDate) {
            alert("검색 기간을 선택해 주세요.");
            return;
        }

        if (dayjs.utc(searchInput.endDate).diff(dayjs.utc(searchInput.startDate), 'minute') <= 0) {
            alert(`종료일이 시작일보다 같거나 이전입니다.`);
            return;
        }

        if (searchInput.userSearchType == Defines.UserSearchType.None) {
            alert("사용자 검색 방식을 선택해 주세요.");
            return;
        }

        const hasValueEventID = eventIDSearchList.filter(element => element == 1);
        if (isEmpty(searchInput.userSearchValue) && (hasValueEventID.length < 1)) {
            alert("사용자 검색 또는 이벤트ID 검색 값을 입력해 주세요.");
            return;
        }

        const encodeEventIDSearchList = encodingBase62(eventIDSearchList.join(""));
        const parameters = `?page=1&userSearchType=${searchInput.userSearchType.toString()}&userSearchValue=${searchInput.userSearchValue}&gameLogSearchType=${searchInput.gameLogSearchType.toString()}&gameLogSearchValue=${searchInput.gameLogSearchValue}&eventIdSearchType=${encodeEventIDSearchList}&startDate=${(searchInput.startDate ? searchInput.startDate.tz().format("YYYY-MM-DD HH:mm:ss") : "")}&endDate=${(searchInput.endDate ? searchInput.endDate.tz().format("YYYY-MM-DD HH:mm:ss") : "")}&lc=${pageLogs}&lsv=${encodingSortState.current}&lsh=${recordHistory.current}`;

        firstRender.current = true;
        router.push(parameters);

    }, [firstRender, router, searchInput, pageLogs, eventIDSearchList]);

    const onChangeUserSearch = useCallback((type: Defines.UserSearchType, value: string) => {
        switch (type) {
            case Defines.UserSearchType.AccountID:
                break;

            case Defines.UserSearchType.Nick:
                break;

            case Defines.UserSearchType.StoveMemberNo:
                break;

            case Defines.UserSearchType.StoveNickNameNo:
                break;

            default:
                alert("검색 타입을 선택해주세요.");
                return;

        }

        setSearchInput(prev => {
            prev.userSearchType = type;
            prev.userSearchValue = value;

            return prev.clone();
        });
    }, [setSearchInput]);

    const onChangeGameLogSearch = useCallback((type: Defines.GameLogSearchType, value: string) => {
        switch (type) {
            case Defines.GameLogSearchType.EventGroupID:
                break;

            case Defines.GameLogSearchType.EventID:
                break;

            case Defines.GameLogSearchType.SequenceNumber:
                break;

            case Defines.GameLogSearchType.AccountLevel:
                break;

            case Defines.GameLogSearchType.CharacterID:
                break;

            case Defines.GameLogSearchType.CharacterLevel:
                break;

            case Defines.GameLogSearchType.SessionID:
                break;

            case Defines.GameLogSearchType.MarketCode:
                break;

            case Defines.GameLogSearchType.ServerCode:
                break;

            case Defines.GameLogSearchType.ChannelCode:
                break;

            case Defines.GameLogSearchType.IP:
                break;

            case Defines.GameLogSearchType.DeviceID:
                break;

            case Defines.GameLogSearchType.DeviceType:
                break;

            case Defines.GameLogSearchType.DeviceModel:
                break;

            case Defines.GameLogSearchType.OS:
                break;

            default:
                alert("검색 타입을 선택해주세요.");
                return;

        }

        setSearchInput(prev => {
            prev.gameLogSearchType = type;
            prev.gameLogSearchValue = value;

            return prev.clone();
        });
    }, [setSearchInput]);

    const onSearchUser = useCallback((type: Defines.UserSearchType, value: string) => {
        if (!isEmpty(value)) {
            switch (type) {
                case Defines.UserSearchType.AccountID:
                    if (value.match(/[^\d]/g) || isNaN(parseInt(value))) {
                        alert("AccountID는 숫자로 입력해 주세요");
                        return;
                    }
                    break;

                case Defines.UserSearchType.Nick:
                    break;

                case Defines.UserSearchType.StoveMemberNo:
                    if (value.match(/[^\d]/g) || isNaN(parseInt(value))) {
                        alert("StoveMemberNo는 숫자로 입력해 주세요");
                        return;
                    }
                    break;

                case Defines.UserSearchType.StoveNickNameNo:
                    if (value.match(/[^\d]/g) || isNaN(parseInt(value))) {
                        alert("StoveNickNameNo는 숫자로 입력해 주세요");
                        return;
                    }
                    break;

                default:
                    alert("검색 타입을 선택해주세요.");
                    return;
            }
        }

        if (!searchInput.startDate && !searchInput.endDate) {
            alert("검색 기간을 선택해 주세요.");
            return;
        }

        if (dayjs.utc(searchInput.endDate).diff(dayjs.utc(searchInput.startDate), 'minute') <= 0) {
            alert(`종료일이 시작일보다 같거나 이전입니다.`);
            return;
        }

        if (searchInput.userSearchType == Defines.UserSearchType.None) {
            alert("사용자 검색 방식을 선택해 주세요.");
            return;
        }

        const hasValueEventID = eventIDSearchList.filter(element => element == 1);
        if (isEmpty(searchInput.userSearchValue) && (hasValueEventID.length < 1)) {
            alert("사용자 검색 또는 이벤트ID 검색 값을 입력해 주세요.");
            return;
        }

        const encodeEventIDSearchList = encodingBase62(eventIDSearchList.join(""));
        const parameters = `?page=1&userSearchType=${type.toString()}&userSearchValue=${value}&gameLogSearchType=${searchInput.gameLogSearchType.toString()}&gameLogSearchValue=${searchInput.gameLogSearchValue}&eventIdSearchType=${encodeEventIDSearchList}&startDate=${(searchInput.startDate ? searchInput.startDate.tz().format("YYYY-MM-DD HH:mm:ss") : "")}&endDate=${(searchInput.endDate ? searchInput.endDate.tz().format("YYYY-MM-DD HH:mm:ss") : "")}&lc=${pageLogs}&lsv=${encodingSortState.current}&lsh=${recordHistory.current}`;
        firstRender.current = true;
        router.push(parameters);
    }, [router, searchInput, pageLogs, eventIDSearchList]);

    const onSearchGameLog = useCallback((type: Defines.GameLogSearchType, value: string) => {
        if (!isEmpty(value)) {
            switch (type) {
                case Defines.GameLogSearchType.EventGroupID:
                    break;

                case Defines.GameLogSearchType.EventID:
                    break;

                case Defines.GameLogSearchType.SequenceNumber:
                    if (value.match(/[^\d]/g) || isNaN(parseInt(value))) {
                        alert("SequenceNumber는 숫자로 입력해 주세요");
                        return;
                    }
                    break;

                case Defines.GameLogSearchType.AccountLevel:
                    if (value.match(/[^\d]/g) || isNaN(parseInt(value))) {
                        alert("AccountLevel은 숫자로 입력해 주세요");
                        return;
                    }
                    break;

                case Defines.GameLogSearchType.CharacterID:
                    if (value.match(/[^\d]/g) || isNaN(parseInt(value))) {
                        alert("CharacterID는 숫자로 입력해 주세요");
                        return;
                    }
                    break;

                case Defines.GameLogSearchType.CharacterLevel:
                    if (value.match(/[^\d]/g) || isNaN(parseInt(value))) {
                        alert("CharacterLevel은 숫자로 입력해 주세요");
                        return;
                    }
                    break;

                case Defines.GameLogSearchType.SessionID:
                    break;

                case Defines.GameLogSearchType.MarketCode:
                    break;

                case Defines.GameLogSearchType.ServerCode:
                    break;

                case Defines.GameLogSearchType.ChannelCode:
                    break;

                case Defines.GameLogSearchType.IP:
                    break;

                case Defines.GameLogSearchType.DeviceID:
                    break;

                case Defines.GameLogSearchType.DeviceType:
                    break;

                case Defines.GameLogSearchType.DeviceModel:
                    break;

                case Defines.GameLogSearchType.OS:
                    break;

                default:
                    alert("검색 타입을 선택해주세요.");
                    return;
            }
        }

        if (!searchInput.startDate && !searchInput.endDate) {
            alert("검색 기간을 선택해 주세요.");
            return;
        }

        if (dayjs.utc(searchInput.endDate).diff(dayjs.utc(searchInput.startDate), 'minute') <= 0) {
            alert(`종료일이 시작일보다 같거나 이전입니다.`);
            return;
        }

        if (searchInput.userSearchType == Defines.UserSearchType.None) {
            alert("사용자 검색 방식을 선택해 주세요.");
            return;
        }

        const hasValueEventID = eventIDSearchList.filter(element => element == 1);
        if (isEmpty(searchInput.userSearchValue) && (hasValueEventID.length < 1)) {
            alert("사용자 검색 또는 이벤트ID 검색 값을 입력해 주세요.");
            return;
        }

        const encodeEventIDSearchList = encodingBase62(eventIDSearchList.join(""));
        const parameters = `?page=1&userSearchType=${searchInput.userSearchType.toString()}&userSearchValue=${searchInput.userSearchValue}&gameLogSearchType=${type.toString()}&gameLogSearchValue=${value}&eventIdSearchType=${encodeEventIDSearchList}&startDate=${(searchInput.startDate ? searchInput.startDate.tz().format("YYYY-MM-DD HH:mm:ss") : "")}&endDate=${(searchInput.endDate ? searchInput.endDate.tz().format("YYYY-MM-DD HH:mm:ss") : "")}&lc=${pageLogs}&lsv=${encodingSortState.current}&lsh=${recordHistory.current}`;
        firstRender.current = true;
        router.push(parameters);
    }, [router, searchInput, pageLogs, eventIDSearchList]);

    const onChangeEventID = useCallback((event: any, v: DataTableModels.BiskitLogEventID) => {
        let eventIDstr = "";
        if (v) eventIDstr = v.SeqID.toString();

        setSearchInput(prev => {
            prev.eventIdSearchType = eventIDstr ?? "";
            return prev.clone();
        });


        if (!v) return;

        let eventIdListIndex: number = 0;
        for (const key in eventIdTable) {
            if (v.SeqID === eventIdTable[key]?.SeqID) break;
            eventIdListIndex++;
        }
        if (eventIDSearchList[eventIdListIndex]) {
            alert("이미 선택되어있는 이벤트 ID입니다.")
            return;
        }
        else {
            setEventIDSearchList(prev => {
                prev[eventIdListIndex] = 1;

                return deepmerge([], prev);
            })
        }


    }, [setSearchInput, eventIdTable, eventIDSearchList]);

    const onChangeStartDate = useCallback((date: string | null) => {
        setSearchInput(prev => {
            let dayjsDate: Dayjs | null = null;

            try {
                dayjsDate = dayjs.tz(date);
            } catch (error) {
                // prev.startDate = null;
                return prev.clone();
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
                // prev.endDate = null;
                return prev.clone();
            }

            prev.endDate = dayjsDate;
            return prev.clone();
        });
    }, [setSearchInput]);
    //#region OnRender

    useEffect(() => {
        if (firstRender.current) {
            firstRender.current = false;
            let userSearchType = Defines.UserSearchType.AccountID;
            if (!userSearchTypeProp.match(/[^\d]/g) && !isNaN(parseInt(userSearchTypeProp.trim())))
                userSearchType = parseInt(userSearchTypeProp.trim());

            let gameLogSearchType = Defines.GameLogSearchType.EventGroupID;
            if (!gameLogSearchTypeProp.match(/[^\d]/g) && !isNaN(parseInt(gameLogSearchTypeProp.trim())))
                gameLogSearchType = parseInt(gameLogSearchTypeProp.trim());

            setSearchInput(prev => {
                let startDate: Dayjs | null = null;

                try {
                    startDate = dayjs.tz(startDateProp);
                } catch (error) {
                    startDate = dayjs().tz().add(-1, 'day');
                }

                let endDate: Dayjs | null = null;

                try {
                    endDate = dayjs.tz(endDateProp);
                } catch (error) {
                    endDate = dayjs().tz().add(1, 'day');
                }

                prev.userSearchType = userSearchType;
                prev.userSearchValue = userSearchValueProp;
                prev.gameLogSearchType = gameLogSearchType;
                prev.gameLogSearchValue = gameLogSearchValueProp;
                prev.startDate = startDate;
                prev.endDate = endDate;
                return prev.clone();
            });

            const decodeEventIDSearchList = decodingBase62(eventIdSearchTypeProp).split("").map(Number);
            if (decodeEventIDSearchList.length === eventIDSearchList.length && !decodeEventIDSearchList.includes(NaN)) {
                setEventIDSearchList(decodeEventIDSearchList);
            }

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
            const parameters = `userSearchType=${userSearchTypeProp}&userSearchValue=${userSearchValueProp}&gameLogSearchType=${gameLogSearchTypeProp}&gameLogSearchValue=${gameLogSearchValueProp}&eventIdSearchType=${eventIdSearchTypeProp}&startDate=${startDateProp}&endDate=${endDateProp}&lc=${logCountValueProp}&lsv=${logSortValueProp}&lsh=${logSortHistoryProp}`;
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
                        prev.totalItems.sort((a, b) => dayjs(b.timestamp).diff(a.timestamp));
                    }
                }

                return new PaginatedList<Models.BiskitLog>(prev.totalItems, pageProp, parameters, prev.pageSize, prev.pageBlockSize);
            });
        }

    }, [tables, firstRender, loadDatas, dispatch, onSearch, eventIDSearchList.length, pageProp, userSearchTypeProp, userSearchValueProp, gameLogSearchTypeProp, gameLogSearchValueProp, eventIdSearchTypeProp, startDateProp, endDateProp, logCountValueProp, logSortValueProp, logSortHistoryProp]);
    //#endregion

    const incrementSortType = (sortStateType: Defines.GameLogSortType) => {
        const index = sortStateType;

        switch (sortTypeArray.current[index]) {
            case Defines.LogSortState.None:
                sortTypeArray.current[index] = Defines.LogSortState.ASC;
                if (!sortedHistory.current.includes(index))
                    sortedHistory.current.push(index);
                break;
            case Defines.LogSortState.ASC:
                sortTypeArray.current[index] = Defines.LogSortState.Desc;
                if (!sortedHistory.current.includes(index))
                    sortedHistory.current.push(index);
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
     * @param index 게임 로그 각 자리수 정렬을 위한 index
     * @returns 다중 정렬을 위해 히스토리에 따라 명령어를 반환함
     */
    const addCondtion = (index: number) => {
        let condition = "";
        const arrState = sortTypeArray.current[index];
        switch (index) {
            case Defines.GameLogSortType.LogID:
                condition = arrState === 1 ? "a.logID - b.logID"
                    : arrState === 2 ? "b.logID - a.logID"
                        : ""; break;
            case Defines.GameLogSortType.EventGroupID:
                condition = arrState === 1 ? "a.eventGroupID.localeCompare(b.eventGroupID)"
                    : arrState === 2 ? "b.eventGroupID.localeCompare(a.eventGroupID)"
                        : ""; break;
            case Defines.GameLogSortType.EventID:
                condition = arrState === 1 ? "a.eventID.localeCompare(b.eventID)"
                    : arrState === 2 ? "b.eventID.localeCompare(a.eventID)"
                        : ""; break;
            case Defines.GameLogSortType.TimeStamp:
                condition = arrState === 1 ? "dayjs(a.timestamp).diff(b.timestamp)"
                    : arrState === 2 ? "dayjs(b.timestamp).diff(a.timestamp)"
                        : ""; break;
            case Defines.GameLogSortType.SequenceNumber:
                condition = arrState === 1 ? "a.sequenceNumber - b.sequenceNumber"
                    : arrState === 2 ? "b.sequenceNumber - a.sequenceNumber"
                        : ""; break;
            case Defines.GameLogSortType.StoveMember:
                condition = arrState === 1 ? "a.stoveMemberNO - b.stoveMemberNO"
                    : arrState === 2 ? "b.stoveMemberNO - a.stoveMemberNO"
                        : ""; break;
            case Defines.GameLogSortType.StoveNickname:
                condition = arrState === 1 ? "a.stoveNickNameNO - b.stoveNickNameNO"
                    : arrState === 2 ? "b.stoveNickNameNO - a.stoveNickNameNO"
                        : ""; break;
            case Defines.GameLogSortType.AccountID:
                condition = arrState === 1 ? "a.accountID - b.accountID"
                    : arrState === 2 ? "b.accountID - a.accountID"
                        : ""; break;
            case Defines.GameLogSortType.AccountLevel:
                condition = arrState === 1 ? "a.accountLevel - b.accountLevel"
                    : arrState === 2 ? "b.accountLevel - a.accountLevel"
                        : ""; break;
            case Defines.GameLogSortType.AccountName:
                condition = arrState === 1 ? "a.accountName.localeCompare(b.accountName)"
                    : arrState === 2 ? "b.accountName.localeCompare(a.accountName)"
                        : ""; break;
            case Defines.GameLogSortType.CharacterID:
                condition = arrState === 1 ? "a.characterID - b.characterID"
                    : arrState === 2 ? "b.characterID - a.characterID"
                        : ""; break;
            case Defines.GameLogSortType.CharacterLevel:
                condition = arrState === 1 ? "a.characterLevel - b.characterLevel"
                    : arrState === 2 ? "b.characterLevel - a.characterLevel"
                        : ""; break;
            case Defines.GameLogSortType.SessionID:
                condition = arrState === 1 ? "a.sessionID - b.sessionID"
                    : arrState === 2 ? "b.sessionID - a.sessionID"
                        : ""; break;
            case Defines.GameLogSortType.MarketCode:
                condition = arrState === 1 ? "a.marketCode.localeCompare(b.marketCode)"
                    : arrState === 2 ? "b.marketCode.localeCompare(a.marketCode)"
                        : ""; break;
            case Defines.GameLogSortType.ServerCode:
                condition = arrState === 1 ? "a.serverCode.localeCompare(b.serverCode)"
                    : arrState === 2 ? "b.serverCode.localeCompare(a.serverCode)"
                        : ""; break;
            case Defines.GameLogSortType.ChannelCode:
                condition = arrState === 1 ? "a.channelCode.localeCompare(b.channelCode)"
                    : arrState === 2 ? "b.channelCode.localeCompare(a.channelCode)"
                        : ""; break;
            case Defines.GameLogSortType.IP:
                condition = arrState === 1 ? "a.ipAddress.localeCompare(b.ipAddress)"
                    : arrState === 2 ? "b.ipAddress.localeCompare(a.ipAddress)"
                        : ""; break;
            case Defines.GameLogSortType.DeviceID:
                condition = arrState === 1 ? "a.deviceID.localeCompare(b.deviceID)"
                    : arrState === 2 ? "b.deviceID.localeCompare(a.deviceID)"
                        : ""; break;
            case Defines.GameLogSortType.DeviceType:
                condition = arrState === 1 ? "a.deviceType.localeCompare(b.deviceType)"
                    : arrState === 2 ? "b.deviceType.localeCompare(a.deviceType)"
                        : ""; break;
            case Defines.GameLogSortType.DeviceModel:
                condition = arrState === 1 ? "a.deviceModel.localeCompare(b.deviceModel)"
                    : arrState === 2 ? "b.deviceModel.localeCompare(a.deviceModel)"
                        : ""; break;
            case Defines.GameLogSortType.OS:
                condition = arrState === 1 ? "a.os.localeCompare(b.os)"
                    : arrState === 2 ? "b.os.localeCompare(a.os)"
                        : ""; break;
            default:
                //v01 - v30까지 명령어 return
                const vProperty = String(index - 21).padStart(2, "0");
                condition = arrState === 1 ? `a.v${vProperty} - b.v${vProperty}`
                    : arrState === 2 ? `b.v${vProperty} - a.v${vProperty}`
                        : ""; break;
        }
        //console.log("Game 추가 명령어 : ", condition);
        return condition;
    }

    const updatedSortRoute = () => {
        encodingSortState.current = encodingBase62(sortTypeArray.current.join(""));
        const parameters = `userSearchType=${userSearchTypeProp}&userSearchValue=${userSearchValueProp}&gameLogSearchType=${gameLogSearchTypeProp}&gameLogSearchValue=${gameLogSearchValueProp}&eventIdSearchType=${eventIdSearchTypeProp}&startDate=${startDateProp}&endDate=${endDateProp}&lc=${logCountValueProp}&lsv=${encodingSortState.current}&lsh=${recordHistory.current}`;
        router.push(`?page=${datas.page}&${parameters}`);
        firstRender.current = true;
    };

    const handleChange = (event: SelectChangeEvent<unknown>) => {
        const value = typeof event.target.value === "number" ? event.target.value : parseInt(event.target.value as string, 10);
        if ([10, 25, 50, 100, 200, 500].includes(value)) setPageLogs(value);
    }

    const updatedVisibleState = useCallback((index: number) => {
        setColVisibleState((prev) => {
            prev[index] = prev[index] ? 0 : 1;
            return deepmerge(prev, []);
        })
    }, [setColVisibleState])

    const onDownloadLog = useCallback(async () => {
        if (datas && 0 < datas.totalItems.length) {
            dispatch(layoutsActions.startLoadingMessage(`엑셀을 다운로드 중입니다. 0 / ${datas.totalItems.length}`));

            const sheet: string[] = [];
            for (let i = 0; i < colVisibleState.length; i++) {
                if (colVisibleState[i] && colList[i]) sheet.push(colList[i]);
            }
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
            link.download = `샘플운영툴_게임로그_${dayjs(searchInput.startDate).tz().format("YYYY-MM-DD")}_${dayjs(searchInput.endDate).tz().format("YYYY-MM-DD")}.csv`;
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);

            // XLSX.writeFile(workbook, `샘플운영툴_게임로그_${dayjs(searchInput.startDate).tz().format("YYYY-MM-DD")}_${dayjs(searchInput.endDate).tz().format("YYYY-MM-DD")}.xlsx`);
            dispatch(layoutsActions.stopLoading());
        }
    }, [dispatch, datas, colVisibleState, searchInput.startDate, searchInput.endDate]);

    const onDelete = useCallback((index: number) => {
        setEventIDSearchList(prev => {
            prev[index] = 0;
            return deepmerge([], prev);
        })
    }, [setEventIDSearchList])

    const event = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        if (eventIDSearchList.length < 1) return <></>;

        const list: ReactElement[] = [];
        for (let i = 0; i < eventIDSearchList.length; i++) {
            if (!eventIDSearchList[i]) continue;
            const eventData = eventIdTable[Object.keys(eventIdTable)[i]];
            list.push(<Chip key={`eventId-Chip-${i}`} sx={{ marginRight: 1, marginTop: 1 }} color='primary' size='small'
                label={eventData.EventName.replaceAll(/(?<=.{3})./g, '*')} variant="outlined" onDelete={() => onDelete(i)} />)
        }
        result = <>{list}</>;

        return result;
    }, [eventIdTable, eventIDSearchList, onDelete])
    return (
        <Box sx={{ mb: '100px', width: '100%', minWidth: 450 }}>
            <Box sx={{ mt: 2 }}>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* 게임로그 검색 기능.</Typography>
            </Box>
            <Grid container justifyContent='center' sx={{ margin: '20px 0 0' }}>
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: { xs: '50%', sm: '50% !important', md: '33% !important', lg: '16% !important' }, maxWidth: { xs: '50%', sm: '50% !important', md: '33% !important', lg: '16% !important' } }}>
                    <UserSearch onChange={(type, value) => onChangeUserSearch(type, value)} onSubmit={(type, value) => onSearchUser(type, value)} size="small" label="사용자 검색" />
                </Grid>
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: { xs: '50%', sm: '50% !important', md: '33% !important', lg: '16% !important' }, maxWidth: { xs: '50%', sm: '50% !important', md: '33% !important', lg: '16% !important' } }}>
                    <GameLogSearch onChange={(type, value) => onChangeGameLogSearch(type, value)} onSubmit={(type, value) => onSearchGameLog(type, value)} size="small" label="게임로그 검색"
                        ignoreSearchType={[
                            Defines.GameLogSearchType.EventID,
                            Defines.GameLogSearchType.V01,
                            Defines.GameLogSearchType.V02,
                            Defines.GameLogSearchType.V03,
                            Defines.GameLogSearchType.V04,
                            Defines.GameLogSearchType.V05,
                            Defines.GameLogSearchType.V06,
                            Defines.GameLogSearchType.V07,
                            Defines.GameLogSearchType.V08,
                            Defines.GameLogSearchType.V09,
                            Defines.GameLogSearchType.V10,
                            Defines.GameLogSearchType.V11,
                            Defines.GameLogSearchType.V12,
                            Defines.GameLogSearchType.V13,
                            Defines.GameLogSearchType.V14,
                            Defines.GameLogSearchType.V15,
                            Defines.GameLogSearchType.V16,
                            Defines.GameLogSearchType.V17,
                            Defines.GameLogSearchType.V18,
                            Defines.GameLogSearchType.V19,
                            Defines.GameLogSearchType.V20,
                            Defines.GameLogSearchType.V21,
                            Defines.GameLogSearchType.V22,
                            Defines.GameLogSearchType.V23,
                            Defines.GameLogSearchType.V24,
                            Defines.GameLogSearchType.V25,
                            Defines.GameLogSearchType.V26,
                            Defines.GameLogSearchType.V27,
                            Defines.GameLogSearchType.V28,
                            Defines.GameLogSearchType.V29,
                            Defines.GameLogSearchType.V30,
                        ]}
                    />
                </Grid>
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: { xs: '50%', sm: '50% !important', md: '33% !important', lg: '16% !important' }, maxWidth: { xs: '50%', sm: '50% !important', md: '33% !important', lg: '16% !important' } }}>
                    <EventIdSearch size="medium" label="사용자 검색" onChange={onChangeEventID} />
                </Grid>
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: { xs: '50%', sm: '50% !important', md: '33% !important', lg: '16% !important' }, maxWidth: { xs: '50%', sm: '50% !important', md: '33% !important', lg: '16% !important' } }}>
                    <DateTimePicker label={`시작일(${timezoneName})`} enableTimePicker={true} format="YYYY-MM-DD HH:mm" value={(searchInput.startDate && dayjs(searchInput.startDate).isValid() ? dayjs.utc(searchInput.startDate).tz().format("YYYY-MM-DD HH:mm") : null)} onChange={(date) => onChangeStartDate(date)} />
                </Grid>
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: { xs: '50%', sm: '50% !important', md: '33% !important', lg: '16% !important' }, maxWidth: { xs: '50%', sm: '50% !important', md: '33% !important', lg: '16% !important' } }}>
                    <DateTimePicker label={`종료일(${timezoneName})`} enableTimePicker={true} format="YYYY-MM-DD HH:mm" value={(searchInput.endDate && dayjs(searchInput.endDate).isValid() ? dayjs.utc(searchInput.endDate).tz().format("YYYY-MM-DD HH:mm") : null)} onChange={(date) => onChangeEndDate(date)} />
                </Grid>
                <Grid item xs={2} sx={{ padding: '10px', flexBasis: { xs: '50%', sm: '50% !important', md: '33% !important', lg: '16% !important' }, maxWidth: { xs: '50%', sm: '50% !important', md: '33% !important', lg: '16% !important' } }}>
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
                <Grid item xs={3} sx={{ padding: '10px', textAlign: 'center', flexBasis: '50% !important', maxWidth: '50% !important' }}>
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onSearch}>검색</Button>
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='success' size="small" onClick={async () => { await onDownloadLog(); }} sx={{ ml: 1 }}>엑셀</Button>
                </Grid>
            </Grid>
            <Grid item sx={{ padding: '10px' }}>
                {event()}
            </Grid>
            <ColVisibleControl states={colVisibleState} stateUpdate={updatedVisibleState} colList={colList} />
            <Paging datas={datas} />
            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 600 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>게임 로그</Typography>
                </Toolbar>
                <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                    <Contents datas={datas} incrementSortType={incrementSortType} history={sortTypeArray.current} visibleStates={colVisibleState} />
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
    const pageString: string = query.page ? query.page.toString() : '1';
    const userSearchType: string = query.userSearchType ? query.userSearchType.toString() : '1';
    const userSearchValue: string = query.userSearchValue ? query.userSearchValue.toString() : '';
    const gameLogSearchType: string = query.gameLogSearchType ? query.gameLogSearchType.toString() : '1';
    const gameLogSearchValue: string = query.gameLogSearchValue ? query.gameLogSearchValue.toString() : '';
    const eventIdSearchType: string = query.eventIdSearchType ? query.eventIdSearchType.toString() : '';
    const pageLogCountValue: string = query.lc ? query.lc.toString() : '10';
    const startDateString: string = query.startDate ? query.startDate.toString() : "";
    const endDateString: string = query.endDate ? query.endDate.toString() : "";
    const gameLogSort: string = query.lsv ? query.lsv.toString() : "00000000000000000000000000"
    const gameLogSortHistory: string = query.lsh ? query.lsh.toString() : '';

    let page = 1;
    let startDate = dayjs(startDateString).isValid() ? dayjs.tz(startDateString).format("YYYY-MM-DD HH:mm") + ":00" : "";
    let endDate = dayjs(endDateString).isValid() ? dayjs.tz(endDateString).format("YYYY-MM-DD HH:mm") + ":59" : "";

    try {
        page = !pageString.match(/[^\d]/g) && !isNaN(parseInt(pageString)) ? parseInt(pageString) : 1;
    } catch (error) {
        if ("production" != process.env.NODE_ENV)
            console.log(error);
    }

    return {
        props: { pageProp: page, userSearchTypeProp: userSearchType, userSearchValueProp: userSearchValue, gameLogSearchTypeProp: gameLogSearchType, gameLogSearchValueProp: gameLogSearchValue, eventIdSearchTypeProp: eventIdSearchType, startDateProp: startDate, endDateProp: endDate, logCountValueProp: pageLogCountValue, logSortValueProp: gameLogSort, logSortHistoryProp: gameLogSortHistory },
    };
}

export default Page;