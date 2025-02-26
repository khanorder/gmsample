import { ChangeEvent, ReactElement, useEffect, useRef, useState, useCallback } from 'react';
import { useAppDispatch, useAppSelector } from "@hooks/index";
import { GetServerSideProps } from 'next';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import { GameAPI } from '@ngeldata/apis/gameAPI';
import * as layoutsActions from '@store/reducers/layouts';
import { Errors } from '@ngel/data/autoErrors';
import { Models } from '@ngel/data/models';
import { Defines } from '@ngel/data/autoDefines';
import deepmerge from 'deepmerge';
import { useRouter } from 'next/router';
import styles from '@styles/pages/GMS/Services/states.module.sass';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer, styled, Toolbar, Typography } from '@mui/material';
import dynamic from 'next/dynamic';
import { GameAPIModels } from '@ngel/data/models/gameAPIModels';
import { DataTableModels } from '@ngel/data/tables/model';
import { PaginatedList } from '@helpers/paging';
import isEmpty from 'lodash/isEmpty';
import { EMailType, ERewardType } from '@ngel/data/models/lobby';
import { removeEventMailReq, updateEventMailReq } from 'src/ngel/data/hubs/GMServer/reducer';

const Paging = dynamic(() => import('@components/ui/Paging'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableRow = dynamic(() => import('@mui/material/TableRow'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const DateTimePicker = dynamic(() => import('@components/ui/DateTimePicker'), { ssr: false });
const TextField = dynamic(() => import('@mui/material/TextField'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Link = dynamic(() => import('next/link'), { ssr: false });

interface EventMailListProps {
    pageProp: number;
}

class SearchInput {
    startDate: Dayjs;
    endDate: Dayjs;

    constructor(startDate?: Dayjs, endDate?: Dayjs) {
        this.startDate = startDate ?? dayjs.utc().add(-30, 'day');
        this.endDate = endDate ?? dayjs().utc().add(30, 'day');
    }
    clone = () => new SearchInput(this.startDate, this.endDate);
}


const MailTypes = Object.keys(EMailType).filter(key => isNaN(Number(key)));
const categoryTranslate = (categoryNum: EMailType) => {
    if (isNaN(Number(categoryNum))) return "";

    return MailTypes[categoryNum];
}

const EventMailList = ({ pageProp }: EventMailListProps): ReactElement => {
    const dispatch = useAppDispatch();
    const firstRender = useRef(true);
    const router = useRouter();
    const tables = useAppSelector(state => state.tables);
    const pages = useAppSelector(state => state.pages);
    const [datas, setDatas] = useState<Models.EventMail[]>([]);
    const [searchInput, setSearchInput] = useState<SearchInput>(new SearchInput(dayjs.utc().tz().add(-30, 'day').startOf('day'), dayjs.utc().tz().add(30, 'day').endOf('day')));

    //for Paging
    const [pagingDatas, setPagingDatas] = useState<PaginatedList<Models.EventMail>>(new PaginatedList<Models.EventMail>([]));

    useEffect(() => {
        const parameters = ``
        const pageSize = 10;
        const totalPageCount = Math.ceil(datas.length / pageSize);
        const page = pageProp > totalPageCount ? totalPageCount : pageProp;
        setPagingDatas(prev => {
            return new PaginatedList<Models.EventMail>(datas, page, parameters, pageSize, prev.pageBlockSize);
        })
    }, [datas, pageProp]);

    const loadDatas = useCallback(async () => {
        if (firstRender.current) {
            setDatas(prev => prev = []);

            dispatch(layoutsActions.startLoadingMessage("이벤트 우편 정보를 불러오는 중입니다."));

            const parameter = new GameAPIModels.GameEventMailsParameters();
            parameter.startTime = searchInput.startDate.toDate();
            parameter.endTime = searchInput.endDate.toDate();

            const response = await GameAPI.EventMailsAsync(parameter);
            if (!response.result) {
                if ("production" != process.env.NODE_ENV)
                    console.log(response.error);

                dispatch(layoutsActions.stopLoading());
                return;
            }

            if (null != response.eventMails && 0 < response.eventMails.length) {
                setDatas(prev => prev = deepmerge([], response.eventMails));
            }
            firstRender.current = false;

            dispatch(layoutsActions.stopLoading());
        }
    }, [dispatch, setDatas, searchInput]);

    useEffect(() => {
        if (firstRender.current) {
            loadDatas();
        }
    }, [firstRender, loadDatas]);

    useEffect(() => {
        firstRender.current = true;
        loadDatas();
    }, [pages.servicesEventMail, firstRender, loadDatas]);

    const onSearch = useCallback(async () => {
        firstRender.current = true;
        await loadDatas();

    }, [loadDatas]);

    const onChangeSearchStartDate = useCallback((date: string | null) => {
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

    const onChangeSearchEndDate = useCallback((date: string | null) => {
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

    const insertEventMail = useCallback(() => {
        router.push(`?mode=insert`);
    }, [router]);

    const deleteEventMail = useCallback(async (data: Models.EventMail) => {
        if (!confirm("확인을 누르면 내용이 삭제됩니다.\n계속 하시겠습니까?")) {
            return;
        }

        dispatch(removeEventMailReq({ eventMail: data }));

    }, [dispatch])

    const findItem = useCallback((itemID: number, type: ERewardType) => {
        let item = null;
        switch (type) {
            case ERewardType.Costume:
                item = tables.skinDataTable.find(element => element.ID === itemID);
                break;
            case ERewardType.Item:
                item = tables.itemTable.find(element => element.ID === itemID);
                break;
            case ERewardType.Asset:
                item = tables.assetDataTable.find(element => element.ItemID === itemID);
                break;
            default:
                break;
        }

        return item;
    }, [tables])

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];
        if (pagingDatas.items && pagingDatas.items.length > 0) {
            for (let i = 0; i < pagingDatas?.items.length; i++) {
                const data = pagingDatas.items[i];

                if (data) {
                    list.push(
                        <BorderedTableRow key={i}>
                            <TableCell>{data?.ID}</TableCell>
                            <TableCell>{data?.Title.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                            <TableCell>{data?.Message.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                            <TableCell>{`${categoryTranslate(data?.MailType)}`.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                            <TableCell>{data?.ExpireTime}</TableCell>
                            <TableCell>{data?.StartTime ? dayjs(data?.StartTime).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                            <TableCell>{data?.EndTime ? dayjs(data?.EndTime).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                            <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                            <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                            <TableCell></TableCell>
                            <TableCell>
                                <Link href={`/GMS/Services/EventMail?mode=edit&id=${data?.ID}&page=${pageProp}`}>
                                    <Button variant='outlined' size='small' sx={{ lineHeight: '1.2rem', margin: 0.1 }}>수정</Button>
                                </Link>
                                <Button color="error" variant='outlined' size='small' sx={{ lineHeight: '1.2rem', margin: 0.1 }} onClick={() => deleteEventMail(data)}>삭제</Button>
                            </TableCell>
                        </BorderedTableRow>
                    )
                }

            }
            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>ID</TableCell>
                            <TableCell>제목</TableCell>
                            <TableCell>메시지</TableCell>
                            <TableCell>MailType</TableCell>
                            <TableCell>ExpireTime</TableCell>
                            <TableCell>시작일시({timezoneName})</TableCell>
                            <TableCell>종료일시({timezoneName})</TableCell>
                            <TableCell>생성일시({timezoneName})</TableCell>
                            <TableCell>수정일시({timezoneName})</TableCell>
                            <TableCell>아이템 정보</TableCell>
                            <TableCell>관리</TableCell>
                        </BorderedTableRow>
                    </TableHead>
                    <TableBody>
                        {list}
                    </TableBody>
                </>
            );
        }
        else {
            result = (
                <>
                    <TableBody>
                        <BorderedTableRow>
                            <TableCell className={commonUIStyles.noneCell}>검색된 이벤트 우편 정보가 없습니다.</TableCell>
                        </BorderedTableRow>
                    </TableBody>
                </>
            );
        }
        return result;
    }, [pageProp, pagingDatas, deleteEventMail]);

    return (
        <Box sx={{ mt: 5, width: '100%', marginBottom: 10 }}>
            <Grid container justifyContent='space-between' sx={{ margin: '20px 0 0' }}>
                <Grid item sx={{ padding: '10px 0 10px' }}>
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={insertEventMail}>추가</Button>
                </Grid>
                <Grid item sx={{ padding: '10px 0 10px', ml: 'auto' }} >
                    <Box sx={{ display: 'flex' }}>
                        <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size='small' sx={{ ml: 1 }} onClick={onSearch}>검색</Button>
                        <Box sx={{ ml: 1 }}>
                            <DateTimePicker label={`시작일시(${timezoneName})`} enableTimePicker={true} format="YYYY-MM-DD HH:mm" value={dayjs.tz(searchInput.startDate).format("YYYY-MM-DD HH:mm:ss")} onChange={(date) => onChangeSearchStartDate(date)} />
                        </Box>
                        <Box sx={{ ml: 1 }}>
                            <DateTimePicker label={`종료일시(${timezoneName})`} enableTimePicker={true} format="YYYY-MM-DD HH:mm" value={dayjs.tz(searchInput.endDate).format("YYYY-MM-DD HH:mm:ss")} onChange={(date) => onChangeSearchEndDate(date)} />
                        </Box>
                    </Box>
                </Grid>
            </Grid>
            <TableContainer component={Paper} elevation={4}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>이벤트 우편</Typography>
                </Toolbar>
                <Table>
                    {contents()}
                </Table>
            </TableContainer>
            <Paging datas={pagingDatas} />
        </Box>
    );
}

export default EventMailList;