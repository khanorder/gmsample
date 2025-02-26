import { ReactElement, useEffect, useRef, useState, useCallback } from 'react';
import { useAppDispatch, useAppSelector } from "@hooks/index";
import { GetStaticProps } from 'next';
import * as layoutsActions from '@store/reducers/layouts';
import dynamic from 'next/dynamic';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import { Models } from '@ngel/data/models';
import { ManagerAPI } from '@ngel/data/apis/managerAPI';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer } from '@mui/material';

const Layout = dynamic(() => import('@components/layouts'), { ssr: true });
const ThemeLayout = dynamic(() => import('@components/layouts/theme'), { ssr: true });
const ManageLayout = dynamic(() => import('@components/layouts/manage'), { ssr: true });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: true });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: true });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: true });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: true });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: true });
const DateTimePicker = dynamic(() => import('@components/ui/DateTimePicker'), { ssr: true });
const Contents = dynamic(() => import('@components/GMS/Stats/CCU/Contents'), { ssr : true });

class SearchInput {
    startDate?: Dayjs|null;
    endDate?: Dayjs|null;

    constructor(startDate?: Dayjs|null, endDate?: Dayjs|null) {
        this.startDate = startDate;
        this.endDate = endDate;
    }

    clone = () => new SearchInput(this.startDate, this.endDate);
}
function Page() {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const dispatch = useAppDispatch();
    const firstRender = useRef(true);
    
    const [searchInput, setSearchInput] = useState<SearchInput>(new SearchInput());
    const [datas, setDatas] = useState<Models.CCU[]>([]);

    const loadDatas = useCallback(async () => {
        dispatch(layoutsActions.startLoadingMessage("실시간 접속자 수 데이터를 불러오는 중입니다."));

        const response = await ManagerAPI.CCUAsync({
            startTime : searchInput.startDate ? 
            dayjs.tz(searchInput.startDate).utc().toDate() 
            : dayjs().utc().add(-1,'day').toDate(),

            endTime :searchInput.endDate ? 
            dayjs.tz(searchInput.endDate).utc().toDate() 
            : dayjs().utc().toDate(), 
        });

        if(response.result && null !== response.resultData){
            setDatas(response.resultData);
        }
    
        dispatch(layoutsActions.stopLoading());
        return;
    },[ dispatch, searchInput])
    
    //#region OnRender
    useEffect(() => {
        if (firstRender.current){
            firstRender.current = false;
            setSearchInput(prev => {
                let startDate: Dayjs | null = null;
                startDate = dayjs().tz().add(-3, 'hour');
                
                let endDate: Dayjs | null = null;
                endDate = dayjs().tz();

                prev.startDate = startDate;
                prev.endDate = endDate;
                return prev.clone();
            });
            
        }
    }, [ firstRender ]);
    //#endregion

    useEffect(() => {
        loadDatas();
    }, [searchInput.startDate, searchInput.endDate, loadDatas])

    const onChangeStartDate = useCallback((date: string | null) => {
        let dayjsDate: Dayjs | null = null;
        try {
            dayjsDate = dayjs.tz(date);
        } catch (error) {
            alert("시작일이 잘못 선택되었습니다.");
            return;
        }

        if(dayjs.utc(dayjsDate).isAfter(dayjs.utc(searchInput.endDate))){
            alert(`종료일이 시작일보다 같거나 이전입니다.`);
            return;
        }
    
        setSearchInput(prev => {
        
            try {
                dayjsDate = dayjs.tz(date);
            } catch (error) {
                prev.startDate = null;
                return prev.clone();
            }
            
            prev.startDate = dayjsDate;
            return prev.clone();
        });
    }, [searchInput, setSearchInput]);

    const onChangeEndDate = useCallback((date: string | null) => {
        let dayjsDate: Dayjs | null = null;
        try {
            dayjsDate = dayjs.tz(date);
        } catch (error) {
            alert("종료일이 잘못 선택되었습니다.");
            return;
        }

        if(dayjs.utc(dayjsDate).isBefore(dayjs.utc(searchInput.startDate))){
            alert(`종료일이 시작일보다 같거나 이전입니다.`);
            return;
        }

        setSearchInput(prev => {
            prev.endDate = dayjsDate;
            return prev.clone();
        });
    }, [searchInput, setSearchInput]);
    
    return (
        <>
            <Box sx={{ mb: '100px', width: '100%', minWidth: 450 }}>
                <Grid container justifyContent='center' sx={{ margin: '20px 0 0' }}>
                    <Grid item xs={2} sx={{ padding: '10px', flexBasis: { xs: '50%', sm: '50% !important', md: '33% !important', lg: '16% !important' }, maxWidth: { xs: '50%', sm: '50% !important', md: '33% !important', lg: '16% !important' } }}>
                        <DateTimePicker label={`시작일(${timezoneName})`} enableTimePicker={true} format="YYYY-MM-DD HH:mm" value={(searchInput.startDate && dayjs(searchInput.startDate).isValid() ? dayjs.utc(searchInput.startDate).tz().format("YYYY-MM-DD HH:mm") : null)} onChange={(date) => onChangeStartDate(date)} />
                    </Grid>
                    <Grid item xs={2} sx={{ padding: '10px', flexBasis: { xs: '50%', sm: '50% !important', md: '33% !important', lg: '16% !important' }, maxWidth: { xs: '50%', sm: '50% !important', md: '33% !important', lg: '16% !important' } }}>
                        <DateTimePicker label={`종료일(${timezoneName})`} enableTimePicker={true} format="YYYY-MM-DD HH:mm" value={(searchInput.endDate && dayjs(searchInput.endDate).isValid() ? dayjs.utc(searchInput.endDate).tz().format("YYYY-MM-DD HH:mm") : null)} onChange={(date) => onChangeEndDate(date)} />
                    </Grid>
                </Grid>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 700 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>실시간 접속자 수</Typography>
                    </Toolbar>
                    <Contents logs={datas}/>
                </TableContainer>
            </Box>
        </>
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

export const getStaticProps: GetStaticProps = async (context) => {
    return {
        props: {
        }
    };
}

export default Page;