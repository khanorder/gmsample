import { ReactElement, useRef, useState, useEffect, useCallback, ChangeEvent } from 'react';
import { GetStaticProps } from 'next';
import dynamic from 'next/dynamic';
import { Models } from '@ngel/data/models';
import { ManagerAPI } from '@ngel/data/apis/managerAPI';
import deepmerge from 'deepmerge';
import commonUIStyles from '@styles/ui/common.module.sass';
import { FormControl, InputLabel, MenuItem, Select, SelectChangeEvent, TableContainer } from '@mui/material';
import { Defines } from '@ngeldata/autoDefines';
import { Errors } from '@ngel/data/autoErrors';
import { v4 as uuid, NIL as NIL_UUID } from 'uuid';
import isEmpty from 'lodash/isEmpty'

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

function Page() {
    const firstRender = useRef(true);

    const [datas, setDatas] = useState<Models.Firewall[]>([]);
    const [deleteDatas, setDeleteDatas] = useState<string[]>([]);

    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [ firstRender ]);

    const loadDatas = useCallback(async () => {
        setDatas(prev => prev = []);
        setDeleteDatas(prev => prev = []);

        const response = await ManagerAPI.FirewallsAsync();

        if (!response.result) {
            if ("production" != process.env.NODE_ENV)
                console.log(response.error);

            return;
        }

        setDatas(prev => {
            prev = response.resultData;
            return deepmerge([], prev);
        });

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
            const deleteResponse = await ManagerAPI.DeleteFirewallAsync({ paramData: deleteDatas });

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

                if (isEmpty(data.ipAddress)) {
                    alert(`${i + 1}번째 데이터에 IP를 입력해 주세요.`);
                    return;
                }

                var hasAster = data.ipAddress.includes('*') && data.ipAddress[data.ipAddress.length-1] == "*";
                var ipSplit = data.ipAddress.split(".");
                if (4 != ipSplit.length) {
                    if(!hasAster){
                        alert(`${i + 1}번째 데이터를 IP형식(1.0.0.0 ~ 255.255.255.255)에 맞게 입력해 주세요.`);
                        return;
                    }
                }

                for (let j = 0; j < ipSplit.length; j++) {
                    var ipSplitNumberString = ipSplit[j];

                    if (isEmpty(ipSplitNumberString)) {
                        alert(`${i + 1}번째 데이터를 IP형식(1.0.0.0 ~ 255.255.255.255)에 맞게 입력해 주세요.`);
                        return;
                    }

                    var ipSplitNumber = parseInt(ipSplitNumberString);
                    var isAster = ipSplitNumberString == '*' && j == ipSplit.length-1;
                    
                    if (isNaN(ipSplitNumber)) {
                        if(!isAster){
                            alert(`${i + 1}번째 데이터를 IP형식(1.0.0.0 ~ 255.255.255.255)에 맞게 입력해 주세요.`);
                            return;
                        }
                    }

                    if (0 > ipSplitNumber || 255 < ipSplitNumber && !isAster) {
                        alert(`${i + 1}번째 데이터를 IP형식(1.0.0.0 ~ 255.255.255.255)에 맞게 입력해 주세요.`);
                        return;
                    }

                    if (0 == j && 1 > ipSplitNumber && !isAster) {
                        alert(`${i + 1}번째 데이터를 IP형식(1.0.0.0 ~ 255.255.255.255)에 맞게 입력해 주세요.`);
                        return;
                    }
                }


                if (isEmpty(data.description)) {
                    alert(`${i + 1}번째 데이터에 설명을 입력해 주세요.`);
                    return;
                }
            }

            for (let j = 0; j < changedDatas.length; j++){
                changedDatas[j].description = changedDatas[j].description.trim();
            }

            const response = await ManagerAPI.SaveFirewallAsync({ paramData : changedDatas });

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

    const onAddData = useCallback(() => {
        const newData = new Models.Firewall();
        newData.id = NIL_UUID.toString();
        newData.method = 0;
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
                prev.push(deleteData.id);
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

    const onChangeMethod = useCallback((e: SelectChangeEvent<Defines.FirewallMethod>, index: number) => {
        const transFirewallMethod = e.target.value as Defines.FirewallMethod;
        setDatas(prev => {
            if (prev[index]) {
                prev[index].method = transFirewallMethod ? transFirewallMethod : Defines.FirewallMethod.Allow;
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);
    
    const onChangeIP = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, index: number) => {
        const overlapCheck = datas.filter(element => element.ipAddress == e.target.value).length ? true : false;

        if(overlapCheck){
            alert("중복되는 IP가 존재합니다.");
            return;
        }

        setDatas(prev => {
            if (prev[index]) {
                prev[index].ipAddress = e.target.value;
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [datas, setDatas]);

    const onChangeDescription = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, index: number) => {
        setDatas(prev => {
            if (prev[index]) {
                prev[index].description = e.target.value ? e.target.value : "";
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const contents = useCallback ((): ReactElement => {
        let result: ReactElement = <></>;

        if(0 < datas.length){
            const list: ReactElement[] = [];

            for(let i = 0 ; i < datas.length; i++){
                const data = datas[i];
                let rowClass: string = data.isChanged ? `${commonUIStyles.row} ${commonUIStyles.changed}` : commonUIStyles.row;

                const row = (
                    <BorderedTableRow key={i} className={rowClass}>
                        <TableCell>
                            <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant='outlined' size='small' color='inherit' onClick={e => onDelete(i)}>삭제</Button>
                        </TableCell>
                        <TableCell>
                            {
                                data.isNewData ?
                                <FormControl>
                                    <InputLabel>상태</InputLabel>
                                    <Select
                                    aria-label="method"
                                    label="method"
                                    value={data.method}
                                    onChange={e => onChangeMethod(e, i)}>
                                        <MenuItem value={Defines.FirewallMethod.Allow}>Allow</MenuItem>
                                        <MenuItem value={Defines.FirewallMethod.Deny}>Deny</MenuItem>
                                    </Select>
                                </FormControl>
                                :
                                data.method == 0 ? "Allow" : "Deny"
                            }</TableCell>
                        <TableCell>
                            {
                                data.isNewData ?
                                <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.ipAddress} size='small' placeholder='IP' onChange={e => onChangeIP(e, i)} />
                                :
                                data.ipAddress
                            }</TableCell>
                        <TableCell>
                            {
                                data.isNewData ?
                                <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.description} size='small' placeholder='사유' onChange={e => onChangeDescription(e, i)} />
                                :
                                data.description
                            }</TableCell>
                    </BorderedTableRow>
                )

                list.push(row);
            }
            
            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>관리</TableCell>
                            <TableCell>상태</TableCell>
                            <TableCell>IP</TableCell>
                            <TableCell>사유</TableCell>
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
                        <TableCell className={commonUIStyles.noneCell}>검색된 운영툴 방화벽 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }

        return result;
    }, [datas, onChangeDescription, onChangeIP, onChangeMethod, onDelete]);

    return (
        <Box sx={{ mt: 5, mb: '100px', width: '100%' }}>
            <Grid sx={{ padding: '10px 0 20px' }}>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onAddData}>추가</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" sx={{ ml: 1 }} onClick={onSaveData}>저장</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload} sx={{ ml: 1 }}>새로고침</Button>
            </Grid>
            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>운영툴 방화벽</Typography>
                </Toolbar>
                <Table stickyHeader aria-label="sticky table">
                    {contents()}
                </Table>
            </TableContainer>
            <Grid sx={{ padding: '20px 0 10px' }}>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onAddData}>추가</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" sx={{ ml: 1 }} onClick={onSaveData}>저장</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload} sx={{ ml: 1 }}>새로고침</Button>
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

export const getStaticProps: GetStaticProps = async (context) => {
    return {
        props: {
        }
    };
}

export default Page;