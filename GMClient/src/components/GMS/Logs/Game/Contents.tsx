import { ReactElement } from 'react';
import { useAppSelector } from "@hooks/index";
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { Defines } from "@ngeldata/autoDefines";
import { dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import dynamic from 'next/dynamic';
import { DataTableModels } from '@ngel/data/tables/model';

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const DragNoneIcon = dynamic(() => import('@mui/icons-material/DragIndicator'), { ssr: false });
const DragAscIcon = dynamic(() => import('@mui/icons-material/ExpandLess'), { ssr: false });
const DragDescIcon = dynamic(() => import('@mui/icons-material/ExpandMore'), { ssr: false });

interface ContentsProps {
    datas: PaginatedList<Models.BiskitLog>;
    incrementSortType: (sortStateType: Defines.GameLogSortType) => void;
    history: number[];
    visibleStates: number[];
}

const sortIcons = [<DragNoneIcon key="DragNone" />, <DragAscIcon key="DragAsc" />, <DragDescIcon key="DragDesc" />];

const styles = {
    smallCell: {
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        width: "100%",
        minWidth: "108px",
        height: "100%",
        cursor: "pointer"
    },
    mediumCell: {
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        width: "100%",
        minWidth: "158px",
        height: "100%",
        cursor: "pointer"
    }
}

const Contents = ({ datas, incrementSortType, history, visibleStates = Array(51).fill(1) }: ContentsProps): ReactElement => {

    let result: ReactElement = <></>;
    const eventIdTable = useAppSelector(state => state.tables.biskitLogEventIDTable);

    const hasVisibleColumn = visibleStates.filter(element => element == 1).length > 0 ? true : false;

    if (0 < datas.total) {
        if (hasVisibleColumn) {
            const list: ReactElement[] = [];

            for (let i = 0; i < datas.length; i++) {
                const data = datas.items[i];
                const existInTable: DataTableModels.BiskitLogEventID = eventIdTable[datas.items[i].eventID] ?? null;
                const row = (
                    <BorderedTableRow key={i}>
                        <TableCell component="th" scope="row">{datas.offsetIndex - i}</TableCell>
                        {visibleStates[0] ? <TableCell>{data.logID.toString().replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[1] ? <TableCell>{data.eventGroupID.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[2] ? <TableCell>{existInTable ? `${existInTable.EventName.replaceAll(/(?<=.{3})./g, '*')} (${data.eventID.replaceAll(/(?<=.{3})./g, '*')})` : data.eventID.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[3] ? <TableCell>{dayjs(data.timestamp).tz().format('YYYY-MM-DD HH:mm:ss')}</TableCell> : ""}
                        {visibleStates[4] ? <TableCell>{data.sequenceNumber.toString().replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[5] ? <TableCell>{data.stoveMemberNO.toString().replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[6] ? <TableCell>{data.stoveNickNameNO.toString().replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[7] ? <TableCell>{data.accountID.toString().replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[8] ? <TableCell>{data.accountLevel.toString().replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[9] ? <TableCell>{data.accountName.toString().replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[10] ? <TableCell>{data.characterID.toString().replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[11] ? <TableCell>{data.characterLevel.toString().replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[12] ? <TableCell>{data.sessionID.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[13] ? <TableCell>{data.marketCode.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[14] ? <TableCell>{data.serverCode.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[15] ? <TableCell>{data.channelCode.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[16] ? <TableCell>{data.ipAddress.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[17] ? <TableCell>{data.deviceID.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[18] ? <TableCell>{data.deviceType.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[19] ? <TableCell>{data.deviceModel.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[20] ? <TableCell>{data.os.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[21] ? <TableCell>{data.v01.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[22] ? <TableCell>{data.v02.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[23] ? <TableCell>{data.v03.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[24] ? <TableCell>{data.v04.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[25] ? <TableCell>{data.v05.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[26] ? <TableCell>{data.v06.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[27] ? <TableCell>{data.v07.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[28] ? <TableCell>{data.v08.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[29] ? <TableCell>{data.v09.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[30] ? <TableCell>{data.v10.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[31] ? <TableCell>{data.v11.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[32] ? <TableCell>{data.v12.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[33] ? <TableCell>{data.v13.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[34] ? <TableCell>{data.v14.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[35] ? <TableCell>{data.v15.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[36] ? <TableCell>{data.v16.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[37] ? <TableCell>{data.v17.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[38] ? <TableCell>{data.v18.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[39] ? <TableCell>{data.v19.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[40] ? <TableCell>{data.v20.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[41] ? <TableCell>{data.v21.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[42] ? <TableCell>{data.v22.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[43] ? <TableCell>{data.v23.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[44] ? <TableCell>{data.v24.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[45] ? <TableCell>{data.v25.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[46] ? <TableCell>{data.v26.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[47] ? <TableCell>{data.v27.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[48] ? <TableCell>{data.v28.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[49] ? <TableCell>{data.v29.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                        {visibleStates[50] ? <TableCell>{data.v30.replaceAll(/(?<=.{3})./g, '*')}</TableCell> : ""}
                    </BorderedTableRow>
                );

                list.push(row);
            }
            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>순번</TableCell>
                            {visibleStates[0] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.LogID); }}><div style={styles.smallCell}> log_id {sortIcons[history[1]]}</div></TableCell> : ""}
                            {visibleStates[1] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.EventGroupID); }}><div style={styles.mediumCell}> event_group_id {sortIcons[history[2]]}</div></TableCell> : ""}
                            {visibleStates[2] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.EventID); }}> <div style={styles.mediumCell}> event_name (id) {sortIcons[history[3]]}</div></TableCell> : ""}
                            {visibleStates[3] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.TimeStamp); }}> <div style={styles.mediumCell}> timestamp({timezoneName}) {sortIcons[history[4]]}</div></TableCell> : ""}
                            {visibleStates[4] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.SequenceNumber); }}> <div style={styles.mediumCell}> sequence_number {sortIcons[history[5]]}</div></TableCell> : ""}
                            {visibleStates[5] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.StoveMember); }}> <div style={styles.mediumCell}> stove_member_no {sortIcons[history[6]]}</div></TableCell> : ""}
                            {visibleStates[6] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.StoveNickname); }}> <div style={styles.mediumCell}> stove_nickname_no {sortIcons[history[7]]}</div></TableCell> : ""}
                            {visibleStates[7] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.AccountID); }}> <div style={styles.mediumCell}> account_id {sortIcons[history[8]]}</div></TableCell> : ""}
                            {visibleStates[8] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.AccountLevel); }}> <div style={styles.mediumCell}> account_level {sortIcons[history[9]]}</div></TableCell> : ""}
                            {visibleStates[9] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.AccountName); }}> <div style={styles.mediumCell}> account_name {sortIcons[history[10]]}</div></TableCell> : ""}
                            {visibleStates[10] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.CharacterID); }}> <div style={styles.mediumCell}> character_id {sortIcons[history[11]]}</div></TableCell> : ""}
                            {visibleStates[11] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.CharacterLevel); }}> <div style={styles.mediumCell}> character_level {sortIcons[history[12]]}</div></TableCell> : ""}
                            {visibleStates[12] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.SessionID); }}> <div style={styles.mediumCell}> session_id {sortIcons[history[13]]}</div></TableCell> : ""}
                            {visibleStates[13] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.MarketCode); }}> <div style={styles.mediumCell}> market_code {sortIcons[history[14]]}</div></TableCell> : ""}
                            {visibleStates[14] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.ServerCode); }}> <div style={styles.mediumCell}> server_code {sortIcons[history[15]]}</div></TableCell> : ""}
                            {visibleStates[15] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.ChannelCode); }}> <div style={styles.mediumCell}> channel_code {sortIcons[history[16]]}</div></TableCell> : ""}
                            {visibleStates[16] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.IP); }}> <div style={styles.mediumCell}> ip_address {sortIcons[history[17]]}</div></TableCell> : ""}
                            {visibleStates[17] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.DeviceID); }}> <div style={styles.mediumCell}> device_id {sortIcons[history[18]]}</div></TableCell> : ""}
                            {visibleStates[18] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.DeviceType); }}> <div style={styles.mediumCell}> device_type {sortIcons[history[19]]}</div></TableCell> : ""}
                            {visibleStates[19] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.DeviceModel); }}> <div style={styles.mediumCell}> device_model {sortIcons[history[20]]}</div></TableCell> : ""}
                            {visibleStates[20] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.OS); }}> <div style={styles.mediumCell}> os {sortIcons[history[21]]}</div></TableCell> : ""}
                            {visibleStates[21] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V01); }}> <div style={styles.mediumCell}> v1 {sortIcons[history[22]]}</div></TableCell> : ""}
                            {visibleStates[22] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V02); }}> <div style={styles.mediumCell}> v2 {sortIcons[history[23]]}</div></TableCell> : ""}
                            {visibleStates[23] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V03); }}> <div style={styles.mediumCell}> v3 {sortIcons[history[24]]}</div></TableCell> : ""}
                            {visibleStates[24] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V04); }}> <div style={styles.mediumCell}> v4 {sortIcons[history[25]]}</div></TableCell> : ""}
                            {visibleStates[25] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V05); }}> <div style={styles.mediumCell}> v5 {sortIcons[history[26]]}</div></TableCell> : ""}
                            {visibleStates[26] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V06); }}> <div style={styles.mediumCell}> v6 {sortIcons[history[27]]}</div></TableCell> : ""}
                            {visibleStates[27] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V07); }}> <div style={styles.mediumCell}> v7 {sortIcons[history[28]]}</div></TableCell> : ""}
                            {visibleStates[28] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V08); }}> <div style={styles.mediumCell}> v8 {sortIcons[history[29]]}</div></TableCell> : ""}
                            {visibleStates[29] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V09); }}> <div style={styles.mediumCell}> v9 {sortIcons[history[30]]}</div></TableCell> : ""}
                            {visibleStates[30] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V10); }}> <div style={styles.mediumCell}> v10 {sortIcons[history[31]]}</div></TableCell> : ""}
                            {visibleStates[31] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V11); }}> <div style={styles.mediumCell}> v11 {sortIcons[history[32]]}</div></TableCell> : ""}
                            {visibleStates[32] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V12); }}> <div style={styles.mediumCell}> v12 {sortIcons[history[33]]}</div></TableCell> : ""}
                            {visibleStates[33] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V13); }}> <div style={styles.mediumCell}> v13 {sortIcons[history[34]]}</div></TableCell> : ""}
                            {visibleStates[34] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V14); }}> <div style={styles.mediumCell}> v14 {sortIcons[history[35]]}</div></TableCell> : ""}
                            {visibleStates[35] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V15); }}> <div style={styles.mediumCell}> v15 {sortIcons[history[36]]}</div></TableCell> : ""}
                            {visibleStates[36] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V16); }}> <div style={styles.mediumCell}> v16 {sortIcons[history[37]]}</div></TableCell> : ""}
                            {visibleStates[37] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V17); }}> <div style={styles.mediumCell}> v17 {sortIcons[history[38]]}</div></TableCell> : ""}
                            {visibleStates[38] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V18); }}> <div style={styles.mediumCell}> v18 {sortIcons[history[39]]}</div></TableCell> : ""}
                            {visibleStates[39] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V19); }}> <div style={styles.mediumCell}> v19 {sortIcons[history[40]]}</div></TableCell> : ""}
                            {visibleStates[40] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V20); }}> <div style={styles.mediumCell}> v20 {sortIcons[history[41]]}</div></TableCell> : ""}
                            {visibleStates[41] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V21); }}> <div style={styles.mediumCell}> v21 {sortIcons[history[42]]}</div></TableCell> : ""}
                            {visibleStates[42] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V22); }}> <div style={styles.mediumCell}> v22 {sortIcons[history[43]]}</div></TableCell> : ""}
                            {visibleStates[43] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V23); }}> <div style={styles.mediumCell}> v23 {sortIcons[history[44]]}</div></TableCell> : ""}
                            {visibleStates[44] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V24); }}> <div style={styles.mediumCell}> v24 {sortIcons[history[45]]}</div></TableCell> : ""}
                            {visibleStates[45] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V25); }}> <div style={styles.mediumCell}> v25 {sortIcons[history[46]]}</div></TableCell> : ""}
                            {visibleStates[46] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V26); }}> <div style={styles.mediumCell}> v26 {sortIcons[history[47]]}</div></TableCell> : ""}
                            {visibleStates[47] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V27); }}> <div style={styles.mediumCell}> v27 {sortIcons[history[48]]}</div></TableCell> : ""}
                            {visibleStates[48] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V28); }}> <div style={styles.mediumCell}> v28 {sortIcons[history[49]]}</div></TableCell> : ""}
                            {visibleStates[49] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V29); }}> <div style={styles.mediumCell}> v29 {sortIcons[history[50]]}</div></TableCell> : ""}
                            {visibleStates[50] ? <TableCell onClick={() => { incrementSortType(Defines.GameLogSortType.V30); }}> <div style={styles.mediumCell}> v30 {sortIcons[history[51]]}</div></TableCell> : ""}
                        </BorderedTableRow>
                    </TableHead>
                    <TableBody>
                        {list}
                    </TableBody>
                </>
            );
        }
        else {
            result = (
                <TableBody>
                    <BorderedTableRow>
                        <TableCell className={commonUIStyles.noneCell}>활성화된 컬럼이 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }
    }
    else {
        result = (
            <TableBody>
                <BorderedTableRow>
                    <TableCell className={commonUIStyles.noneCell}>검색된 로그 데이터가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }

    return result;
}

export default Contents;