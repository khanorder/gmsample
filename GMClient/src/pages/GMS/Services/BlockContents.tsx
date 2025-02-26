import { ReactElement, useEffect, useRef, useState, useCallback } from 'react';
import { useAppSelector } from "@hooks/index";
import { GetStaticProps } from 'next';
import { ManagerAPI } from '@ngel/data/apis/managerAPI';
import { Errors } from '@ngel/data/autoErrors';
import { Models } from '@ngel/data/models';
import deepmerge from 'deepmerge';
import styles from '@styles/pages/GMS/Services/states.module.sass';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer, TextField, Autocomplete } from '@mui/material';
import dynamic from 'next/dynamic';
import { DataTableModels } from '@ngel/data/tables/model';

const Layout = dynamic(() => import('@components/layouts'), { ssr: true });
const ThemeLayout = dynamic(() => import('@components/layouts/theme'), { ssr: true });
const ManageLayout = dynamic(() => import('@components/layouts/manage'), { ssr: true });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: true });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: true });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: true });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: true });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: true });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: true });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: true });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: true });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: true });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: true });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: true });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: true });

function Page() {
    const firstRender = useRef(true);
    const blockContentTable = useAppSelector(state => state.tables.blockContentDataTable);
    const [datas, setDatas] = useState<Models.BlockContent[]>([]);
    const [deleteDatas, setDeleteDatas] = useState<Models.BlockContent[]>([]);
    const [blockConInfos, setBlockConInfos] = useState<DataTableModels.BlockContentData[]>([]);
    
    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [ firstRender ]);
    //#endregion

    useEffect(() => {
        const blockConOptions:DataTableModels.BlockContentData[] = [];

        for(const key in blockContentTable){
            const item = new DataTableModels.BlockContentData(blockContentTable[key]);
            blockConOptions.push(item);
        }

        setBlockConInfos(blockConOptions);
    }, [blockContentTable]);

    const loadDatas = useCallback(async () => {
        setDatas(prev => prev = []);
        setDeleteDatas(prev => prev = []);

        const response = await ManagerAPI.BlockContentsAsync();

        if (!response.result) {
            if ("production" != process.env.NODE_ENV)
            console.log(response.error);
            
            return;
        }
        
        if(null != response.blockContents && 0 < response.blockContents.length){
            setDatas(prev => prev = deepmerge([], response.blockContents));
        }
    }, [setDatas, setDeleteDatas]);

    useEffect(() => {
        loadDatas();

    }, [loadDatas]);

    const onReload = useCallback(async () => {
        const changed = datas.filter(_ => _.isChanged);
        if ((changed && changed.length > 0) || (deleteDatas && deleteDatas.length > 0)) {
            if (!confirm("작업중인 내용이 사라집니다.\n정말 새로고침 하시겠습니까?")) {
                return;
            }
        }
        await loadDatas();
        
    }, [loadDatas, datas, deleteDatas]);

    const onSaveData = useCallback(async () => {
        const changedDatas = datas.filter(_ => _.isChanged);

        if ((!changedDatas || 1 > changedDatas.length) && (!deleteDatas || 1 > deleteDatas.length)) {
            alert("저장할 변경된 내용이 없습니다.");
            return;
        }

        if ((deleteDatas && 0 < deleteDatas.length)) {
            if (!confirm("삭제되는 데이터가 있습니다.\n계속 하시겠습니까?")) {
                return;
            }
        }
        else{
            if (!confirm("확인을 누르면 내용이 저장됩니다.\n계속 하시겠습니까?")) {
                return;
            }
        }

        if (deleteDatas && 0 < deleteDatas.length) {
            const deleteResponse = await ManagerAPI.DeleteBlockContentsAsync({ blockContents : deleteDatas });

            if (!deleteResponse) {
                alert(`삭제 오류!`);
                return;
            }
    
            if (!deleteResponse.result) {
                alert(`삭제 오류! (error: ${Errors[deleteResponse.error]}, index: ${deleteResponse.errorIndex})`);
                return;
            }
        }

        if (changedDatas && 0 < changedDatas.length) {
            const response = await ManagerAPI.SaveBlockContentsAsync({ blockContents: changedDatas });
    
            if (!response) {
                alert(`오류!`);
                return;
            }
    
            if (!response.result) {
                alert(`오류! (error: ${Errors[response.error]}, index: ${response.errorIndex})`);
                return;
            }
        }
        
        alert("저장 했습니다.");
        await loadDatas();
    }, [loadDatas, datas, deleteDatas]);

    const onAddData = useCallback(() => {
        const newData = new Models.BlockContent();
        if(datas.length >= Object.keys(blockContentTable).length) return ; 

        for(const [key, value] of Object.entries(blockContentTable)){
            let data = datas.find((element) => element.packetID === value?.PacketID);
            if(!data) {
                newData.packetID = blockContentTable[key].PacketID;
                break;
            }
        }
        
        newData.isChanged = true;
        newData.isNewData = true;

        setDatas(prev => {
            prev.splice(0, 0, newData);
            return deepmerge([], prev);
        });
    }, [blockContentTable, datas, setDatas]);

    const onDelete = useCallback((index: number) => {
        const deleteData = datas[index];

        if (!deleteData) {
            alert(`${index + 1}번째 데이터가 없습니다.`);
            return;
        }

        if (!deleteData.isNewData) {
            setDeleteDatas(prev => {
                prev.push(deleteData);
                return deepmerge([], prev);
            });
        }

        setDatas(prev => {
            if (prev[index]) {
                prev.splice(index, 1);
            }
            return deepmerge([], prev);
        });
    }, [datas, setDatas]);

    const onChangeData = useCallback((e, v: DataTableModels.BlockContentData, index: number) => {
        if(!v) return;

        const isOverlap = datas.find((element)=> element.packetID === v.PacketID);
        if(isOverlap){
            alert("중복된 값입니다.");
            return;
        }

        setDatas(prev => {
            prev[index].packetID = v.PacketID;
            prev[index].isChanged = true;
            return  deepmerge([], prev);
        })
    }, [datas, setDatas]);

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;

        if(datas && 0 < datas.length){
            const list: ReactElement[] = [];
            
            let rowClass: string = commonUIStyles.row;
            const isChanged = datas.find((element)=> element.isChanged);

            if(isChanged) rowClass = `${commonUIStyles.row} ${commonUIStyles.changed}`;
            const dataSelectOption = blockConInfos.filter((element)=> !datas.some((data)=> element.PacketID === data.packetID && !data.isNewData));
            
            for(let i = 0; i< datas.length; i++){
                const data = datas[i];
                const dataInfo = blockConInfos.find((element) => element.PacketID === data.packetID);

                list.push(
                    <BorderedTableRow key={`${i}-1`} className={rowClass}>
                        <TableCell>
                            <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant='outlined' size='small' color='inherit' onClick={e => onDelete(i)}>삭제</Button>
                        </TableCell>
                        <TableCell>
                            <FormControl>
                                { data.isNewData ? 
                                <Autocomplete options={dataSelectOption} sx={{width: 300}}
                                getOptionLabel={(option) => (option as DataTableModels.BlockContentData).PacketName.toString().replaceAll(/(?<=.{2})./g, '*')}
                                value={dataInfo} onChange={(e, v) => onChangeData(e, v as DataTableModels.BlockContentData, i)}
                                renderInput={(params) => <TextField {...params} label="패킷 이름" />} />
                                :
                                <Typography>{dataInfo?.PacketName.toString().replaceAll(/(?<=.{2})./g, '*')}</Typography>
                                }
                            </FormControl>
                        </TableCell>
                        <TableCell>
                            <Typography>{dataInfo?.PacketID ? dataInfo?.PacketID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.packetID.toString().replaceAll(/(?<=.{2})./g, '*')}</Typography>
                        </TableCell>
                    </BorderedTableRow>
                );
                
            }

            result = (
                <>
                    <TableBody>
                        {list}
                    </TableBody>
                </>
            )
        }
        else {
            result = (
                <TableBody>
                    <BorderedTableRow>
                        <TableCell colSpan={3} className={commonUIStyles.noneCell}>검색된 블럭 컨텐츠 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }
        
        return result;
    }, [datas, blockConInfos, onChangeData, onDelete]);

    return (
        <Box sx={{ mt: 5, mb: '100px', width: '100%' }}>
            <Box sx={{ mt: 2 }}>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* 블럭/비공개 컨텐츠 관리 기능.</Typography>
            </Box>
            <Grid sx={{ padding: '10px 0 20px' }}>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onAddData}>추가</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" sx={{ ml: 1 }} onClick={onSaveData}>저장</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" sx={{ ml: 1 }} onClick={onReload}>새로고침</Button>
            </Grid>
            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>블럭 컨텐츠</Typography>
                </Toolbar>
                <Table className={`${styles.statesTable} ${commonUIStyles.ellipsisTable}`} stickyHeader aria-label="sticky table">
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>관리</TableCell>
                            <TableCell>컨텐츠 이름</TableCell>
                            <TableCell>패킷ID</TableCell>
                        </BorderedTableRow>
                    </TableHead>
                    { contents()}
                </Table>
            </TableContainer>
            <Grid sx={{ padding: '20px 0 10px' }}>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onAddData}>추가</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" sx={{ ml: 1 }} onClick={onSaveData}>저장</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" sx={{ ml: 1 }} onClick={onReload}>새로고침</Button>
            </Grid>
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