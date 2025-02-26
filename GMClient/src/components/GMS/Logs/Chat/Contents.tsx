import { ReactElement } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { Defines } from "@ngeldata/autoDefines";
import { dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import dynamic from 'next/dynamic';

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const DragNoneIcon = dynamic(() => import('@mui/icons-material/DragIndicator'), { ssr: false });
const DragAscIcon = dynamic(() => import('@mui/icons-material/ExpandLess'), { ssr: false });
const DragDescIcon = dynamic(() => import('@mui/icons-material/ExpandMore'), { ssr: false });

interface ContentsProps {
    datas: PaginatedList<Models.ChatLog>;
    incrementSortType: (sortStateType: Defines.ChatLogSortType) => void;
    history: number[];
}

const sortIcons = [<DragNoneIcon key="DragNone" />, <DragAscIcon key="DragAsc" />, <DragDescIcon key="DragDesc" />];

const styles = {
    smallCell: {
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        minWidth: "108px",
        cursor: "pointer"
    },
    mediumCell: {
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        minWidth: "332.7px",
        cursor: "pointer"
    }
}

const Contents = ({ datas, incrementSortType, history }: ContentsProps): ReactElement => {

    let result: ReactElement = <></>;

    if (0 < datas.total) {
        const list: ReactElement[] = [];
        for (let i = 0; i < datas.length; i++) {
            const data = datas.items[i];

            const row = (
                <BorderedTableRow key={i}>
                    <TableCell component="th" scope="row">{datas.offsetIndex - i}</TableCell>
                    <TableCell>{data?.accountId.toString().replaceAll(/(?<=.{3})./g, '*')}</TableCell>
                    <TableCell>{data?.accountName.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                    <TableCell>{data?.eventId.toString().replaceAll(/(?<=.{3})./g, '*')}</TableCell>
                    <TableCell>{data?.stoveMemberNo.toString().replaceAll(/(?<=.{3})./g, '*')}</TableCell>
                    <TableCell>{data?.stoveNicknameNo.toString().replaceAll(/(?<=.{3})./g, '*')}</TableCell>
                    <TableCell>{data?.sessionId.toString().replaceAll(/(?<=.{3})./g, '*')}</TableCell>
                    <TableCell>{data?.ipAddress.toString().replaceAll(/(?<=.{3})./g, '*')}</TableCell>
                    <TableCell>{data?.message.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                    <TableCell>{data?.timeStamp ? dayjs(data?.timeStamp).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                </BorderedTableRow>
            );

            list.push(row);
        }
        result = (
            <>
                <TableHead>
                    <BorderedTableRow>
                        <TableCell>순번</TableCell>
                        <TableCell onClick={() => { incrementSortType(Defines.ChatLogSortType.AccountId) }}><div style={styles.smallCell}>AccountID {sortIcons[history[1]]}</div></TableCell>
                        <TableCell onClick={() => { incrementSortType(Defines.ChatLogSortType.AccountName) }}><div style={styles.smallCell}>AccountName {sortIcons[history[2]]}</div></TableCell>
                        <TableCell onClick={() => { incrementSortType(Defines.ChatLogSortType.EventID) }}><div style={styles.smallCell}>EventID {sortIcons[history[3]]}</div></TableCell>
                        <TableCell onClick={() => { incrementSortType(Defines.ChatLogSortType.StoveMemberNo) }}><div style={styles.smallCell}>StoveMemberNo {sortIcons[history[4]]}</div></TableCell>
                        <TableCell onClick={() => { incrementSortType(Defines.ChatLogSortType.StoveNicknameNo) }}><div style={styles.smallCell}>StoveNicknameNo {sortIcons[history[5]]}</div></TableCell>
                        <TableCell onClick={() => { incrementSortType(Defines.ChatLogSortType.SessionId) }}><div style={styles.smallCell}>SessionId {sortIcons[history[6]]}</div></TableCell>
                        <TableCell onClick={() => { incrementSortType(Defines.ChatLogSortType.IpAddress) }}><div style={styles.mediumCell}>IP {sortIcons[history[7]]}</div></TableCell>
                        <TableCell onClick={() => { incrementSortType(Defines.ChatLogSortType.Message) }}><div style={styles.mediumCell}>메시지 {sortIcons[history[8]]}</div></TableCell>
                        <TableCell onClick={() => { incrementSortType(Defines.ChatLogSortType.TimeStamp) }}><div style={styles.mediumCell}>사용일시({timezoneName}) {sortIcons[history[9]]}</div></TableCell>
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
                    <TableCell className={commonUIStyles.noneCell}>검색된 로그 데이터가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }

    return result
}
export default Contents;