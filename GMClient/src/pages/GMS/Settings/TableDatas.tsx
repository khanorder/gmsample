import { ReactElement, useEffect, useRef, useCallback } from 'react';
import { useAppSelector } from "@hooks/index";
import React from 'react';
import { GetServerSideProps } from 'next';
import { useRouter } from 'next/router';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer } from '@mui/material';
import dynamic from 'next/dynamic';
import isEmpty from 'lodash/isEmpty'
const Layout = dynamic(() => import('@components/layouts'), { ssr: true });
const ThemeLayout = dynamic(() => import('@components/layouts/theme'), { ssr: true });
const ManageLayout = dynamic(() => import('@components/layouts/manage'), { ssr: true });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: true });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: true });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: true });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: true });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: true });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: true });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: true });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: true });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: true });
const TableDatasContents = dynamic(() => import('@components/GMS/Settings/TableDatasContents'), {ssr: true});
const DragNoneIcon = dynamic(() => import('@mui/icons-material/ExpandLess'), { ssr:true });
const DragDescIcon = dynamic(() => import('@mui/icons-material/ExpandMore'), { ssr:true });

const sortIcons = [<DragNoneIcon key="DragNone"/>, <DragDescIcon key="DragDesc"/>];

interface PageProps{
    activeTableProp: number;
}

function Page({ activeTableProp } : PageProps) {
    const tables = useAppSelector(state => state.tables);
    const firstRender = useRef(true);
    const router = useRouter();
    
    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [ firstRender ]);
    //#endregion

    const onTableToggle = useCallback((e, index: number)=>{
        if(index === parseInt(activeTableProp.toString())){
            router.push(`${router.pathname}?activeTable=`);
        }
        else{
            router.push(`${router.pathname}?activeTable=${index}`);
        }
    }, [router, activeTableProp]);

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;

        if (0 < Object.keys(tables).length) {
            const list: ReactElement[] = [];
            
            for (let i = 0; i < Object.keys(tables).length; i++) {
                var key = Object.keys(tables)[i];
                if (false == key.endsWith("Version"))
                    continue;

                let data = [];
                let isOpen = false;
                if(activeTableProp){
                    isOpen = parseInt(activeTableProp.toString()) === i;
                    data = tables[key.replace("Version", "")];
                }
                const row = (
                    <React.Fragment key={`fragment-${i}`}>
                        <Table sx={{ minWidth: '100%', tableLayout:'fixed'}}>
                            <TableBody>
                                <BorderedTableRow key={i} className={commonUIStyles.row}>
                                    <TableCell sx={{cursor: 'pointer'}} onClick={(e => onTableToggle(e, i))}><div style={{display: 'flex', justifyContent: 'center', overflow: 'hidden'}}>{key.toString().replaceAll(/(?<=.{5})./g, '*')} {isOpen ? sortIcons[1] : sortIcons[0]}</div></TableCell>
                                    <TableCell>{tables[key]}</TableCell>
                                    <TableCell>{Object.keys(tables[key.replace("Version", "")] ?? {}).length ?? 0}</TableCell>
                                </BorderedTableRow>
                            </TableBody>
                        </Table>
                        {isOpen ? 
                            <>
                            <TableDatasContents datas={data}/>
                            </>
                        : <></>}
                    </React.Fragment>
                    
                );
                list.push(row);
            }

            result = (
                <>
                    <Table stickyHeader aria-label="sticky table" sx={{ minWidth: '100%', tableLayout:'fixed'}}>
                        <TableHead>
                            <BorderedTableRow>
                                <TableCell>테이블</TableCell>
                                <TableCell>버전</TableCell>
                                <TableCell>데이터 수</TableCell>
                            </BorderedTableRow>
                        </TableHead>
                    </Table>
                        {list}
                </>
            );
        } else {
            result = (
                <Table stickyHeader aria-label="sticky table">
                    <TableBody>
                        <BorderedTableRow>
                            <TableCell className={commonUIStyles.noneCell}>기획 테이블 정보가 없습니다.</TableCell>
                        </BorderedTableRow>
                    </TableBody>
                </Table>
            );
        }

        return result;
    }, [tables, activeTableProp, onTableToggle]);
    
    return (
        <Box sx={{ mt: 5, mb: '100px', width: '100%', minWidth: 750 }}>
            <Box sx={{ mt: 2 }}>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* 게임 컨텐츠의 설정 스크립트 값 조회 기능.</Typography>
            </Box>
            <TableContainer component={Paper} elevation={4}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>기획 테이블 정보</Typography>
                </Toolbar>
                {contents()}
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

export const getServerSideProps: GetServerSideProps = async (context) => {
    const { query } = context;
    const activeTable: string = query.activeTable ? query.activeTable.toString() : "";

    return {
        props: {
            activeTableProp : activeTable,
        }
    };
}

export default Page;