import { ReactElement, useRef, useState, useEffect, useCallback, ChangeEvent } from 'react';
import { GetStaticProps } from 'next';
import dynamic from 'next/dynamic';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import { Models } from '@ngel/data/models';
import { ManagerAPI } from '@ngel/data/apis/managerAPI';
import deepmerge from 'deepmerge';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer } from '@mui/material';
import { Errors } from '@ngel/data/autoErrors';
import isEmpty from 'lodash/isEmpty'
import { Defines } from '@ngel/data/autoDefines';

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
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: true });
const TextField = dynamic(() => import('@mui/material/TextField'), { ssr: true });
const DateTimePicker = dynamic(() => import('@components/ui/DateTimePicker'), { ssr: true });

function Page() {
    const firstRender = useRef(true);

    const [data, setData] = useState<Models.Settings|null>(null);

    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [firstRender]);

    const loadDatas = useCallback(async () => {
        setData(null);

        const response = await ManagerAPI.ExpireLogSettingAsync();

        if (!response.result) {
            if ("production" != process.env.NODE_ENV)
                console.log(response.error);

            return;
        }

        setData(null == response.expireLogSetting ? null : new Models.Settings(response.expireLogSetting));

    }, [setData]);

    useEffect(() => {
        loadDatas();

    }, [loadDatas]);

    const onReload = useCallback(async () => {
        if (null != data && data.isChanged) {
            if (!confirm("작업중인 내용이 사라집니다.\n정말 새로고침 하시겠습니까?")) {
                return;
            }
        }

        await loadDatas();

    }, [loadDatas, data]);

    const onSaveData = useCallback(async () => {
        if (null == data || false == data.isChanged) {
            alert("저장할 변경된 내용이 없습니다.");
            return;
        }

        if (30 > data.value) {
            alert("로그 보관 기간은 30일 이상이어야 합니다.");
            return;
        }

        if (1095 < data.value) {
            alert("로그 보관 기간은 1095일 이하여야 합니다.");
            return;
        }

        const response = await ManagerAPI.SaveExpireLogSettingAsync({ value: data.value });

        if (!response || !response.result || Errors.None != response.error) {
            alert(`저장실패 (error: ${Errors[response.error]})`);
            return;
        }

        alert(`저장 했습니다.`);
        await loadDatas();
    }, [loadDatas, data]);

    const onChangeExpireDay = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        let value = parseInt(e.target.value);
        if (isNaN(value))
            value = 0;

        setData(prev => {
            if (!prev)
                prev = new Models.Settings({ key: Defines.SettingType[Defines.SettingType.ExpireLog], value: 30 });

            prev.value = value;
            prev.isChanged = true;
            return new Models.Settings(prev);
        });
    }, [setData]);

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;

        if (null != data) {
            let rowClass: string = data.isChanged ? `${commonUIStyles.row} ${commonUIStyles.changed}` : commonUIStyles.row;

            result = (
                <TableBody>
                    <BorderedTableRow className={rowClass}>
                        <TableCell>만료기간(일)</TableCell>
                        <TableCell>
                            <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.value} size='small' placeholder='expireDay' onChange={e => onChangeExpireDay(e)} />
                        </TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        } else {
            result = (
                <TableBody>
                    <BorderedTableRow>
                        <TableCell className={commonUIStyles.noneCell}>검색된 로그 만료일 설정이 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }


        return result;
    }, [data, onChangeExpireDay])
    return (
        <Box sx={{ mt: 5, mb: '100px', width: '100%' }}>
            <Box sx={{ mt: 2 }}>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* 운영툴 로그 보관기간 설정 기능.</Typography>
            </Box>
            <Grid sx={{ padding: '10px 0 20px' }}>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" sx={{ ml: 1 }} onClick={onSaveData}>저장</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload} sx={{ ml: 1 }}>새로고침</Button>
            </Grid>
            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>블럭 아이피</Typography>
                </Toolbar>
                <Table stickyHeader aria-label="sticky table">
                    {contents()}
                </Table>
            </TableContainer>
            <Grid sx={{ padding: '20px 0 10px' }}>
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