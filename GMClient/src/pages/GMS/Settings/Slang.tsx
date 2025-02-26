import { ReactElement, useRef, useState, useEffect, useCallback, ChangeEvent } from 'react';
import { GetServerSideProps } from 'next';
import dynamic from 'next/dynamic';
import { Models } from '@ngel/data/models';
import { ManagerAPI } from '@ngel/data/apis/managerAPI';
import deepmerge from 'deepmerge';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer } from '@mui/material';
import { Errors } from '@ngel/data/autoErrors';
import isEmpty from 'lodash/isEmpty'
import { PaginatedList } from '@helpers/paging';
const Paging = dynamic(() => import('@components/ui/Paging'), { ssr: true });

const Layout = dynamic(() => import('@components/layouts'), { ssr: true });
const ThemeLayout = dynamic(() => import('@components/layouts/theme'), { ssr: true });
const ManageLayout = dynamic(() => import('@components/layouts/manage'), { ssr: true });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: true });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: true });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: true });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: true });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: true });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: true });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: true });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: true });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: true });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: true });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: true} );
const TextField = dynamic(() => import('@mui/material/TextField'), { ssr: true });

interface PagingProps {
    pageProp: number;
}

function Page({ pageProp } : PagingProps) {
    const firstRender = useRef(true);
    const [datas, setDatas] = useState<Models.Slang[]>([]);
    const [slangInput, setSlangInput] = useState<string>("");
    const [searchInput, setSearchInput] = useState<string>("");

    //for Paging
    const [slangDatas, setSlangDatas] = useState<PaginatedList<Models.Slang>>(new PaginatedList<Models.Slang>([]));

    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

        const parameters = ``;

        let searched = datas;

        if(searchInput) { searched = searched.filter(element => element.Word.toLowerCase().includes(searchInput)); }
        
        const pagingSize = searched.length >= 50 ? 50 : searched.length;
        setSlangDatas(prev => {
            return new PaginatedList<Models.Slang>(searched, pageProp, parameters, pagingSize, prev.pageBlockSize);
        })

    }, [ firstRender, datas, pageProp, searchInput ]);

    const loadDatas = useCallback(async () => {
        setDatas(prev => prev = []);

        const response = await ManagerAPI.SlangAsync();

        if (!response.result) {
            if ("production" != process.env.NODE_ENV)
                console.log(response.error);
            
            return;
        }
        setDatas(prev => {
            prev = response.resultData;
            return deepmerge([], prev);
        });

    }, [setDatas]);

    useEffect(() => {
        loadDatas();

    }, [loadDatas]);

    const onReload = useCallback(async () => {
        await loadDatas();
        
    }, [loadDatas]);

    const onAddData = useCallback(async () => {
        if(slangInput.toString().trim().length < 1)
            return

        const newData = new Models.Slang();
        newData.Word = slangInput.toString().trim();
        newData.isChanged = true;
        newData.isNewData = true;

        const isOverlap = datas.filter(element => element.Word == newData.Word).length > 0 ? true : false;

        if (!confirm("확인을 누르면 추가 됩니다.\n계속 하시겠습니까?")) {
            return;
        }

        if(isOverlap){
            alert("해당 비속어가 이미 있습니다.")
            return;
        }

        const addResponse = await ManagerAPI.SaveSlangAsync({ paramData: [newData] });

        if (!addResponse) {
            alert(`삭제 오류!`);
            return;
        }

        if (!addResponse.result) {
            alert(`삭제 오류! (error: ${Errors[addResponse.error]}, index: ${addResponse.errorIndex})`);
            return;
        }

        await loadDatas();

        setSlangInput("");
    }, [datas, loadDatas, slangInput, setSlangInput])

    const onDelete = useCallback(async (targetID: number) => {
        const deleteData = slangDatas.totalItems.filter(element => element.ID == targetID);

        if (!deleteData) {
            alert(`선택하신 데이터가 없습니다.`);
            return;
        }

        if (!confirm("확인을 누르면 삭제 됩니다.\n계속 하시겠습니까?")) {
            return;
        }
        const deleteResponse = await ManagerAPI.DeleteSlangAsync({ paramData: [targetID] });

        if (!deleteResponse) {
            alert(`삭제 오류!`);
            return;
        }

        if (!deleteResponse.result) {
            alert(`삭제 오류! (error: ${Errors[deleteResponse.error]}, index: ${deleteResponse.errorIndex})`);
            return;
        }

        await loadDatas();

    }, [loadDatas, slangDatas]);

    const onChangeSLang = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>) => {
        setSlangInput(e.target.value);
    }, []);

    const onChangeSearchInput = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>) => {
        let searchKeyword = e.target.value;

        if(searchKeyword) searchKeyword = searchKeyword.toLowerCase();
        setSearchInput(searchKeyword);
    }, []);

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;

        if(datas && 0 < slangDatas?.items.length){
            const list: ReactElement[] = [];

            for(let i = 0 ; i < slangDatas.items.length; i++){
                const data = slangDatas.items[i];
                let rowClass: string = data.isChanged ? `${commonUIStyles.row} ${commonUIStyles.changed}` : commonUIStyles.row;
            
                const row = (
                    <BorderedTableRow key={i} className={rowClass}>
                        <TableCell>
                            <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant='outlined' size='small' color='inherit' onClick={e => onDelete(data.ID)}>삭제</Button>
                        </TableCell>
                        <TableCell>{ data.Word } </TableCell>
                    </BorderedTableRow>
                )

                list.push(row);
            }

            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>관리</TableCell>
                            <TableCell>설정 비속어</TableCell>
                        </BorderedTableRow>
                    </TableHead>
                    <TableBody>
                        {list}
                    </TableBody>
                </>
            );
        }
        else{
            result = (
                <TableBody>
                    <BorderedTableRow>
                        <TableCell className={commonUIStyles.noneCell}>검색된 비속어 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }
        return result;
    }, [datas, slangDatas, onDelete])

    return (
        <Box sx={{ mt: 5, mb: '100px', width: '100%' }}>
            <Box sx={{ mt: 2 }}>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* 게임채팅 비속어 필터 단어 관리 기능.</Typography>
            </Box>
            <Grid sx={{ padding: '10px 0 20px' }}>
            <   Box sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between'}}>
                    <Box sx={{ display: 'flex', alignItems: 'center'}}>
                        <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload} sx={{ ml: 1 }}>새로고침</Button>
                        <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} sx={{marginLeft: 1}} variant="contained" color='primary' size="small" onClick={onAddData}>추가</Button>
                        <TextField className={commonUIStyles.input} sx={{marginLeft: 1}} variant='outlined' size='small' placeholder='추가 할 비속어' value={slangInput} onChange={e => onChangeSLang(e)}/>
                    </Box>
                    <Box>
                        <TextField className={commonUIStyles.input} sx={{marginLeft: 1}} variant='outlined' size='small' placeholder='검색할 비속어' value={searchInput} onChange={e => onChangeSearchInput(e)}/>
                    </Box>
                </Box>
            </Grid>
            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>비속어 관리</Typography>
                </Toolbar>
                <Table stickyHeader aria-label="sticky table">
                    {contents()}
                </Table>
            </TableContainer>
            <Paging datas={slangDatas} />
            <Grid sx={{ padding: '20px 0 10px' }}>
                <Box sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between'}}>
                    <Box sx={{ display: 'flex', alignItems: 'center'}}>
                        <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload} sx={{ ml: 1 }}>새로고침</Button>
                        <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} sx={{marginLeft: 1}} variant="contained" color='primary' size="small" onClick={onAddData}>추가</Button>
                        <TextField className={commonUIStyles.input} sx={{marginLeft: 1}} variant='outlined' size='small' placeholder='추가 할 비속어' value={slangInput} onChange={e => onChangeSLang(e)}/>
                    </Box>
                </Box>
            </Grid>
        </Box>

    );

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
    const { query } = context;
    const pageString: string = query.page ? query.page.toString() : '1';
    let page = 1;

    try {
        page = !pageString.match(/[^\d]/g) && !isNaN(parseInt(pageString)) ? parseInt(pageString) : 1;
    } catch (error) {
        if ("production" != process.env.NODE_ENV)
            console.log(error);
    }
    return {
        props: {
            pageProp: page,
        }
    };
}

export default Page;