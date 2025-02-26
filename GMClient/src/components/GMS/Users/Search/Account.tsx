import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import { TableContainer } from '@mui/material';
import { Defines } from '@ngel/data/autoDefines';
import { ReactElement, useCallback } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import dynamic from 'next/dynamic';
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });

export interface AccountProps {
    accounts: Models.Account[];
}

const UserAccountLink = ({ accounts }: AccountProps): ReactElement => {

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;

        if (0 < accounts.length) {
            const list = [];
            for (let i = 0; i < accounts.length; i++) {
                const account = accounts[i];
                list.push(
                    <BorderedTableRow key={i}>
                        <TableCell>{Defines.GameAuthType[account.AccountType].toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{account.AccountID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{account.MemberNo.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{account.WorldID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{account.IsLeave ? "탈퇴" : ""}</TableCell>
                        <TableCell>{account.CreateAt ? dayjs(account.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    </BorderedTableRow>
                );
            }

            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>계정타입</TableCell>
                            <TableCell>연동계정 ID</TableCell>
                            <TableCell>MemberNo</TableCell>
                            <TableCell>WorldID</TableCell>
                            <TableCell>계정탈퇴</TableCell>
                            <TableCell>생성일시({timezoneName})</TableCell>
                        </BorderedTableRow>
                    </TableHead>
                    <TableBody>
                        {list}
                    </TableBody>
                </>
            )
        } else {
            result = (
                <TableBody>
                    <BorderedTableRow>
                        <TableCell className={commonUIStyles.noneCell}>검색된 계정연결 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }

        return result;
    }, [accounts]);

    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <TableContainer component={Paper} elevation={4}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>계정**</Typography>
                    </Toolbar>
                    <Table stickyHeader aria-label="sticky table">
                        {contents()}
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}

export default UserAccountLink;