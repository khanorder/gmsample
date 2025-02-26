import { ChangeEvent, ReactElement, useEffect, useRef, useState, useCallback } from 'react';
import { GetStaticProps } from 'next';
import { Errors } from '@ngel/data/autoErrors';
import { Models } from '@ngel/data/models';
import { Defines } from '@ngel/data/autoDefines';
import deepmerge from 'deepmerge';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer, SelectChangeEvent } from '@mui/material';
import dynamic from 'next/dynamic';
import { ManagerAPI } from '@ngel/data/apis/managerAPI';
import isEmpty from 'lodash/isEmpty'
const Layout = dynamic(() => import('@components/layouts'), { ssr: true });
const ThemeLayout = dynamic(() => import('@components/layouts/theme'), { ssr: true });
const ManageLayout = dynamic(() => import('@components/layouts/manage'), { ssr: true });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: true });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: true });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: true });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: true });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: true });
const TextField = dynamic(() => import('@mui/material/TextField'), { ssr: true });
const Select = dynamic(() => import('@mui/material/Select'), { ssr: true });
const MenuItem = dynamic(() => import('@mui/material/MenuItem'), { ssr: true });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: true });
const IconButton = dynamic(() => import('@mui/material/IconButton'), { ssr: true });
const DownLoadIcon = dynamic(() => import('@mui/icons-material/Download'), { ssr: true });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: true });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: true });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: true });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: true });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: true });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: true });

function Page() {
    const firstRender = useRef(true);
    const [datas, setDatas] = useState<Models.VersionInfo[]>([]);
    const [deleteDatas, setDeleteDatas] = useState<Models.VersionInfo[]>([]);
    
    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [ firstRender ]);
    //#endregion

    const loadDatas = useCallback(async () => {
        setDatas(prev => prev = []);
        setDeleteDatas(prev => prev = []);

        const response = await ManagerAPI.VersionInfosAsync();

        if (!response.result) {
            if ("production" != process.env.NODE_ENV)
                    console.log(response.error);

            return;
        }

        if (null != response.versionInfos && 0 < response.versionInfos.length)
            setDatas(prev => prev = deepmerge([], response.versionInfos));

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
            const deleteResponse = await ManagerAPI.DeleteVersionInfosAsync({ versionInfos: deleteDatas });

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
            for (let i = 0; i < datas.length; i++) {
                const data = datas[i];
                if (!data.isChanged)
                    continue;

                if (!data.version) {
                    alert(`${i + 1}번째 데이터에 버전을 입력해 주세요.`);
                    return;
                }

                if (0 > data.platform) {
                    alert(`${i + 1}번째 데이터에 플랫폼을 선택해 주세요.`);
                    return;
                }

                if (1 > data.serverState) {
                    alert(`${i + 1}번째 데이터에 서버 상태를 선택해 주세요.`);
                    return;
                }

                if (1 > data.clientState) {
                    alert(`${i + 1}번째 데이터에 클라이언트 상태 선택해 주세요.`);
                    return;
                }

                if (!data.cdnInfo) {
                    alert(`${i + 1}번째 데이터에 CDN 주소를 입력해 주세요.`);
                    return;
                }

                const existsDatas = datas.filter(_ => _.version == data.version && _.platform == data.platform);
                if (1 < existsDatas.length) {
                    alert(`${i + 1}번째 데이터의 버전(${data.version}), 플랫폼(${Defines.ServiceVersionManagementPlatform[data.platform]})은 이미 존재합니다.`);
                    return;
                }
            }

            const response = await ManagerAPI.SaveVersionInfosAsync({ versionInfos: changedDatas });
    
            if (!response) {
                alert(`오류!`);
                return;
            }
    
            if (!response.result) {
                alert(`오류! (error: ${Errors[response.error]}, index: ${response.errorIndex})`);
                return;
            }
        }

        alert(`저장 했습니다.`);
        await loadDatas();
    }, [loadDatas, datas, deleteDatas]);

    const addData = useCallback(() => {
        const newData = new Models.VersionInfo();
        newData.serverState = 1;
        newData.clientState = 1;
        newData.isChanged = true;
        newData.isNewData = true;

        setDatas(prev => {
            prev.splice(0, 0, newData);
            return deepmerge([], prev);
        });
    }, [setDatas]);

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

    const onChangeVersion = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, index: number) => {
        setDatas(prev => {
            if (prev[index]) {
                prev[index].version = e.target.value;
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangePlatform = useCallback((e: SelectChangeEvent<unknown>, index: number) => {
        let value = 0;

        if (e && e.target && e.target.value) {
            try {
                value = parseInt(e.target.value.toString());
            } catch (error) {
                if ("production" != process.env.NODE_ENV) {
                    console.log(error);
                }
            }
        }

        setDatas(prev => {
            if (prev[index]) {
                prev[index].platform = value;
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangeServerState = useCallback((e: SelectChangeEvent<unknown>, index: number) => {
        let value = 0;

        if (e && e.target && e.target.value) {
            try {
                value = parseInt(e.target.value.toString());
            } catch (error) {
                if ("production" != process.env.NODE_ENV) {
                    console.log(error);
                }
            }
        }

        setDatas(prev => {
            if (prev[index]) {
                prev[index].serverState = value;
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangeClientState = useCallback((e: SelectChangeEvent<unknown>, index: number) => {
        let value = 0;

        if (e && e.target && e.target.value) {
            try {
                value = parseInt(e.target.value.toString());
            } catch (error) {
                if ("production" != process.env.NODE_ENV) {
                    console.log(error);
                }
            }
        }

        setDatas(prev => {
            if (prev[index]) {
                prev[index].clientState = value;
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangeCDNInfo = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, index: number) => {
        setDatas(prev => {
            if (prev[index]) {
                prev[index].cdnInfo = e.target.value;
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onDownloadVersionInfo = useCallback(async (data: Models.VersionInfo) => {
        var response = await ManagerAPI.DownloadVersionInfosAsync({ version: data.version, platform: data.platform });

        switch (response.error) {
            case Errors.None:
                break;

            default:
                alert(`다운로드 실패(error:${response.error})`);    
        }
    }, []);

    const platformSelectList = () => {
        const list: ReactElement[] = [];
        let enumSize = 0;
        if (0 < Object.values(Defines.ServiceVersionManagementPlatform).length)
            enumSize = Object.values(Defines.ServiceVersionManagementPlatform).length / 2;

        for (let i = 0; i < enumSize; i++) {
            list.push(<MenuItem key={i} value={i}>{Defines.ServiceVersionManagementPlatform[i]}</MenuItem>);
        }

        return list;
    }

    const serverStateSelectList = () => {
        const list: ReactElement[] = [];
        let enumSize = 0;
        if (0 < Object.values(Defines.ServiceVersionManagementServerState).length)
            enumSize = Object.values(Defines.ServiceVersionManagementServerState).length / 2;

        for (let i = 0; i < enumSize; i++) {
            if (1 > i)
                continue;
            list.push(<MenuItem key={i} value={i}>{Defines.ServiceVersionManagementServerState[i]}</MenuItem>);
        }

        return list;
    }

    const clientStateSelectList = () => {
        const list: ReactElement[] = [];
        let enumSize = 0;
        if (0 < Object.values(Defines.ServiceVersionManagementClientState).length)
            enumSize = Object.values(Defines.ServiceVersionManagementClientState).length / 2;

        for (let i = 0; i < enumSize; i++) {
            if (1 > i)
                continue;

            list.push(<MenuItem key={i} value={i}>{Defines.ServiceVersionManagementClientState[i]}</MenuItem>);
        }

        return list;
    }

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;

        if (0 < datas.length) {
            const list: ReactElement[] = [];

            for (let i = 0; i < datas.length; i++) {
                const data = datas[i];
                let rowClass: string = commonUIStyles.row;
                if (data.isChanged)
                    rowClass = `${commonUIStyles.row} ${commonUIStyles.changed}`;

                const row = (
                    <BorderedTableRow key={i} className={rowClass}>
                        <TableCell>
                            <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant='outlined' size='small' color='inherit' onClick={e => onDelete(i)}>삭제</Button>
                        </TableCell>
                        <TableCell>
                            {
                                data.isNewData
                                    ?
                                        <></>
                                    :
                                        <IconButton onClick={async () => { await onDownloadVersionInfo(data); }}><DownLoadIcon /></IconButton>
                            }
                        </TableCell>
                        <TableCell>
                            {
                                data.isNewData
                                    ?
                                        <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.version} size='small' placeholder='버전' onChange={e => onChangeVersion(e, i)} />
                                    :
                                        data.version
                            }
                        </TableCell>
                        <TableCell>
                            {
                                data.isNewData
                                    ?
                                        <FormControl>
                                            <Select className={commonUIStyles.select} value={data.platform} size='small' onChange={e => onChangePlatform(e, i)}>
                                                {platformSelectList()}
                                            </Select>
                                        </FormControl>
                                    :
                                        Defines.ServiceVersionManagementPlatform[data.platform]
                            }
                        </TableCell>
                        <TableCell>
                            <FormControl>
                                <Select className={commonUIStyles.select} value={data.serverState} size='small' onChange={e => onChangeServerState(e, i)}>
                                    {serverStateSelectList()}
                                </Select>
                            </FormControl>
                        </TableCell>
                        <TableCell>
                            <FormControl>
                                <Select className={commonUIStyles.select} value={data.clientState} size='small' onChange={e => onChangeClientState(e, i)}>
                                    {clientStateSelectList()}
                                </Select>
                            </FormControl>
                        </TableCell>
                        <TableCell>
                            <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.cdnInfo} size='small' placeholder='CDN 주소' onChange={e => onChangeCDNInfo(e, i)} />
                        </TableCell>
                    </BorderedTableRow>
                );

                list.push(row);
            }

            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>관리</TableCell>
                            <TableCell>JSON</TableCell>
                            <TableCell>버전</TableCell>
                            <TableCell>플랫폼</TableCell>
                            <TableCell>서버 상태</TableCell>
                            <TableCell>클라이언트 상태</TableCell>
                            <TableCell sx={{ minWidth: 500}}>CDN 주소</TableCell>
                        </BorderedTableRow>
                    </TableHead>
                    <TableBody>
                        {list}
                    </TableBody>
                </>
            );
        } else {
            result = (
                <TableBody>
                    <BorderedTableRow>
                        <TableCell className={commonUIStyles.noneCell}>검색된 버전 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }

        return result;
    }, [datas, onChangeVersion, onChangePlatform, onChangeServerState, onChangeClientState, onChangeCDNInfo, onDelete, onDownloadVersionInfo]);
    
    return (
        <Box sx={{ mt: 5, mb: '100px', width: '100%' }}>
            <Box sx={{ mt: 2 }}>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* 서비스 레벨, 버전, 플랫폼 각 조건 별 빌드파일 다운로드 정보 관리 기능.</Typography>
            </Box>
            <Grid sx={{ padding: '10px 0 20px' }}>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={addData}>추가</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSaveData} sx={{ ml: 1 }}>저장</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload} sx={{ ml: 1 }}>새로고침</Button>
            </Grid>
            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>버전 관리</Typography>
                    {/* <IconButton edge="end" onClick={loadDatas}><RefreshIcon /></IconButton> */}
                </Toolbar>
                <Table stickyHeader aria-label="sticky table">
                    {contents()}
                </Table>
            </TableContainer>
            <Grid sx={{ padding: '20px 0 10px' }}>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={addData}>추가</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSaveData} sx={{ ml: 1 }}>저장</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload} sx={{ ml: 1 }}>새로고침</Button>
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