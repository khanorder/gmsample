import { ReactElement, useEffect, useRef, useState, useCallback } from 'react';
import { useAppDispatch } from "@hooks/index";
import { GetStaticProps } from 'next';
import { GameAPI } from '@ngeldata/apis/gameAPI';
import * as layoutsActions from '@store/reducers/layouts';
import { Models } from '@ngel/data/models';
import { Defines } from '@ngel/data/autoDefines';
import deepmerge from 'deepmerge';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer, SelectChangeEvent } from '@mui/material';
import dynamic from 'next/dynamic';
import { GameAPIModels } from '@ngel/data/models/gameAPIModels';
import React from 'react';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import isEmpty from 'lodash/isEmpty';
const Layout = dynamic(() => import('@components/layouts'), { ssr: true });
const ThemeLayout = dynamic(() => import('@components/layouts/theme'), { ssr: true });
const ManageLayout = dynamic(() => import('@components/layouts/manage'), { ssr: true });
const DateTimePicker = dynamic(() => import('@components/ui/DateTimePicker'), { ssr: true });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: true });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: true });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: true });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: true });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: true });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: true });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: true });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: true });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: true });
const InputLabel = dynamic(() => import('@mui/material/InputLabel'), { ssr: false });
const Select = dynamic(() => import('@mui/material/Select'), { ssr: false });
const MenuItem = dynamic(() => import('@mui/material/MenuItem'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: true });

class SearchInput {
    searchType: Defines.GameType;
    searchDate: string;

    constructor(searchType?: Defines.GameType, searchDate?: string) {
        this.searchType = searchType ?? Defines.GameType.None;
        this.searchDate = searchDate ?? dayjs().format("YYYY-MM-DD HH:mm");
    }
    clone = () => new SearchInput(this.searchType, this.searchDate);
}
function Page() {
    const dispatch = useAppDispatch();
    const searchRender = useRef(true);
    const noneStateTry = useRef(true);
    const [datas, setDatas] = useState<Models.GameRankingScore[]>([]);
    const [searchInput, setSearchInput] = useState<SearchInput>(new SearchInput(Defines.GameType.None, dayjs().format("YYYY-MM-DD")));

    const loadDatas = useCallback(async () => {
        if(searchRender.current){
            dispatch(layoutsActions.startLoadingMessage("게임 랭킹 정보를 검색 중입니다."));

            if(searchInput.searchType !== Defines.GameType.None){
                noneStateTry.current = false;
                const parameter = new GameAPIModels.GameDailyRankingParameters();
                parameter.gameType = searchInput.searchType;
                parameter.dateTime = new Date(searchInput.searchDate);
                
                const response = await GameAPI.DailyRankingAsync(parameter);
                
                if (!response.result) {
                    if ("production" != process.env.NODE_ENV)
                    console.log(response.error);
                    
                    return;
                }

                if (null != response.gameRankingScore && 0 < response.gameRankingScore.length){
                    response.gameRankingScore.sort((a, b) => { return b?.Score - a?.Score ;});
                    setDatas(prev => prev = deepmerge([], response.gameRankingScore));
                }
                else{
                    setDatas(prev => prev = []);
                }
            }
            else{
                noneStateTry.current = true;
                setDatas(prev => prev = deepmerge([], prev));
            }
            searchRender.current = false;

            dispatch(layoutsActions.stopLoading());
        }
    }, [searchRender, dispatch, noneStateTry, setDatas, searchInput]);

    useEffect(() => {
        if (searchRender.current) {
            searchRender.current = false;
        }
    }, [searchRender, ]);

    const onChangeSearchDate = useCallback((date: string | null) => {
        setSearchInput(prev => {
            prev.searchDate = date && dayjs(date).isValid() ? dayjs(date).format("YYYY-MM-DD") : dayjs().format("YYYY-MM-DD");
            return prev.clone();
        });
    }, [setSearchInput]);

    const onSearch = useCallback(async () => {
        searchRender.current = true;
        await loadDatas();
    }, [loadDatas]);

    const onChangeBlockReasonType = useCallback((e: SelectChangeEvent<unknown>) => {
        switch(e.target.value){
            case Defines.GameType.None:
                setSearchInput(prev => {
                prev.searchType = Defines.GameType.None;
                return prev.clone(); });
                break;
            case Defines.GameType.GoldClash:
                setSearchInput(prev => {
                prev.searchType = Defines.GameType.GoldClash;
                return prev.clone(); });
                break;
            case Defines.GameType.Felling:
                setSearchInput(prev => {
                prev.searchType = Defines.GameType.Felling;
                return prev.clone(); });
                break;
            case Defines.GameType.OXQuiz:
                setSearchInput(prev => {
                prev.searchType = Defines.GameType.OXQuiz;
                return prev.clone(); });
                break;
            case Defines.GameType.VehicleRace:
                setSearchInput(prev => {
                prev.searchType = Defines.GameType.VehicleRace;
                return prev.clone(); });
                break;
            case Defines.GameType.DiceGame:
                setSearchInput(prev => {
                prev.searchType = Defines.GameType.DiceGame;
                return prev.clone(); });
                break;
        }
    }, [setSearchInput]);

    const contents = useCallback((): ReactElement => {
        
        if(noneStateTry.current){
            return (
                <TableBody>
                    <BorderedTableRow>
                        <TableCell className={commonUIStyles.noneCell} colSpan={4}>게임모드를 선택해주세요.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            )
        }
        if(datas && 0 < datas.length){
            const list: ReactElement[] = [];
            let lowScore = datas[0].Score;
            let sameScoreCount = 0, rank = 1;
            for(let i = 0 ; i < datas.length; i++){
                const data = datas[i];
                
                if(lowScore == data?.Score) sameScoreCount++;
                else if(lowScore > data?.Score){
                    rank += sameScoreCount;
                    sameScoreCount = 1;
                    lowScore = data?.Score;
                }

                list.push(
                    <BorderedTableRow key={`btr-${i}`}>
                        <TableCell>{data?.UID.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{`${rank}위`}</TableCell>
                        <TableCell>{data?.UserAccount?.Nick.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{data?.Score}</TableCell>
                    </BorderedTableRow>
                )
            }

            return (<>
                <TableHead>
                    <BorderedTableRow>
                        <TableCell>UID</TableCell>
                        <TableCell>랭킹순위</TableCell>
                        <TableCell>닉네임</TableCell>
                        <TableCell>점수</TableCell>
                    </BorderedTableRow>
                </TableHead>
                <TableBody>
                    {list}
                </TableBody>
            </>)
        }
        else{
            return (
            <TableBody>
                <BorderedTableRow>
                    <TableCell className={commonUIStyles.noneCell} colSpan={4}>검색된 데이터가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
            )
        }

    }, [noneStateTry, datas]);

    return (
        <Box sx={{ mt: 5, width: '100%' }}>
            <Box sx={{ display: 'flex', width: "100%", mb: 1, justifyContent: 'start'}}>
                <Box sx={{ display: 'flex', width: "100%", justifyContent: 'end'}}>
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size='small' sx={{ ml: 1, mr: 1 }} onClick={onSearch}>검색</Button>
                    <Box sx={{ mr: 1}}>
                        <FormControl >
                            <InputLabel id="select-gamemode">게임모드 선택</InputLabel>
                            <Select labelId="select-gamemode" className={commonUIStyles.select} value={searchInput.searchType} label="게임모드 선택" size='small'  onChange={e => onChangeBlockReasonType(e)}>
                                <MenuItem value={Defines.GameType.None}>{"게임모드를 선택해주세요".toString().replaceAll(/(?<=.{2})./g, '*')}</MenuItem>
                                <MenuItem value={Defines.GameType.GoldClash}>{"골드클래시".toString().replaceAll(/(?<=.{2})./g, '*')}</MenuItem>
                                <MenuItem value={Defines.GameType.Felling}>{"나무베기".toString().replaceAll(/(?<=.{2})./g, '*')}</MenuItem>
                                <MenuItem value={Defines.GameType.OXQuiz}>{"OX퀴즈".toString().replaceAll(/(?<=.{2})./g, '*')}</MenuItem>
                                <MenuItem value={Defines.GameType.VehicleRace}>{"탈것 경주".toString().replaceAll(/(?<=.{2})./g, '*')}</MenuItem>
                                {/* <MenuItem value={Defines.GameType.DiceGame}>{"주사위게임"}</MenuItem> */}
                            </Select>
                        </FormControl>
                    </Box>
                    <Box>
                        <DateTimePicker label='검색일시' value={dayjs(searchInput.searchDate).format('YYYY-MM-DD')} onChange={(date) => onChangeSearchDate(date)} />
                    </Box>
                </Box>
            </Box>
            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>게임랭킹</Typography>
                </Toolbar>
                <Table stickyHeader aria-label="sticky table">
                    {contents()}
                </Table>
            </TableContainer>
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

export const getStaticProps: GetStaticProps = async (context) => {
    return {
        props: {
        }
    };
}

export default Page;