import { ReactElement, useCallback } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { Defines } from "@ngeldata/autoDefines";
import { dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import dynamic from 'next/dynamic';
import { useAppSelector } from '@hooks/index';
import { DataTableModels } from '@ngel/data/tables/model';

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const DragNoneIcon = dynamic(() => import('@mui/icons-material/DragIndicator'), { ssr: false });
const DragAscIcon = dynamic(() => import('@mui/icons-material/ExpandLess'), { ssr: false });
const DragDescIcon = dynamic(() => import('@mui/icons-material/ExpandMore'), { ssr: false });

interface ContentsProps {
    datas: PaginatedList<Models.GMCombinedLog>;
    incrementSortType: (sortStateType: Defines.ManageLogSortType) => void;
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

    const tables = useAppSelector(state => state.tables);
    const naveMenuTable = tables.navMenuDataTable;
    const errorTable = tables.errorsDataTable;

    const result = useCallback(() => {
        if (0 < datas.total) {
            const list: ReactElement[] = [];

            for (let i = 0; i < datas.length; i++) {
                const data = datas.items[i];

                const root: DataTableModels.NavMenuData = naveMenuTable[data.urlName.replace("/GMS", "")] ?? null;
                const error: DataTableModels.ErrorsData = errorTable[data.errorId] ?? null;
                let message = '';
                if (data.message) {
                    try {
                        message = JSON.stringify(JSON.parse(data.message), null, '\t');
                    } catch (error) {
                        console.error(error);
                    }
                } else {

                }

                const row = (
                    <BorderedTableRow key={i}>
                        <TableCell component="th" scope="row">{datas.offsetIndex - i}</TableCell>
                        <TableCell>{Defines.GMLogType[data.type]}</TableCell>
                        <TableCell>{data.userName.replaceAll(/(?<=[a-zA-Z가-힇]{1})./g, '*')}</TableCell>
                        <TableCell>{data.methodName.replaceAll(/(?<=[a-zA-Z가-힇]{5})./g, '*')}</TableCell>
                        <TableCell>{root ? `${root?.Name.replaceAll(/(?<=.{5})./g, '*')} (${data.urlName.replaceAll(/(?<=.{5})./g, '*')})` : data.urlName.replaceAll(/(?<=.{5})./g, '*')}</TableCell>
                        <TableCell>{dayjs(data.regTime).tz().format('YYYY-MM-DD HH:mm:ss')}</TableCell>
                        <TableCell>{error && error.Error != 0 ? `${data.errorName.replaceAll(/(?<=.{5})./g, '*')} (${error.Error})` : (data.errorId == 0 ? "" : data.errorName.replaceAll(/(?<=.{5})./g, '*'))}</TableCell>
                        <TableCell sx={{ textAlign: 'left !important' }}>{message.replaceAll(/(?<=.{5})./g, '*')}</TableCell>
                        <TableCell>{data.remoteAddress.replaceAll(/(?<=.{3})./g, '*')}</TableCell>
                    </BorderedTableRow>
                );

                list.push(row);
            }

            return (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>순번</TableCell>
                            <TableCell>로그타입</TableCell>
                            <TableCell onClick={() => { incrementSortType(Defines.ManageLogSortType.UserName); }}><div style={styles.smallCell}>이름 {sortIcons[history[1]]} </div></TableCell>
                            <TableCell onClick={() => { incrementSortType(Defines.ManageLogSortType.MethodName); }}><div style={styles.mediumCell}>기능 {sortIcons[history[2]]} </div></TableCell>
                            <TableCell onClick={() => { incrementSortType(Defines.ManageLogSortType.UrlName); }}><div style={styles.mediumCell}>경로 {sortIcons[history[3]]} </div></TableCell>
                            <TableCell onClick={() => { incrementSortType(Defines.ManageLogSortType.RegTime); }}><div style={styles.mediumCell}>사용일시({timezoneName}) {sortIcons[history[4]]} </div></TableCell>
                            <TableCell onClick={() => { incrementSortType(Defines.ManageLogSortType.ErrorName); }}><div style={styles.mediumCell}>오류명 {sortIcons[history[5]]} </div></TableCell>
                            <TableCell onClick={() => { incrementSortType(Defines.ManageLogSortType.Message); }}><div style={styles.mediumCell}>인자 {sortIcons[history[6]]} </div></TableCell>
                            <TableCell>IP</TableCell>
                        </BorderedTableRow>
                    </TableHead>
                    <TableBody>
                        {list}
                    </TableBody>
                </>
            );
        } else {
            return (
                <TableBody>
                    <BorderedTableRow>
                        <TableCell className={commonUIStyles.noneCell}>검색된 로그 데이터가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }
    }, [datas, naveMenuTable, errorTable, incrementSortType, history]);

    return result();
}

export default Contents;