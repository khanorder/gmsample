import { ReactElement } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { Defines } from "@ngeldata/autoDefines";
import { dayjs } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import dynamic from 'next/dynamic';
import { useAppSelector } from '@hooks/index';

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const DragNoneIcon = dynamic(() => import('@mui/icons-material/DragIndicator'), { ssr: false });
const DragAscIcon = dynamic(() => import('@mui/icons-material/ExpandLess'), { ssr: false });
const DragDescIcon = dynamic(() => import('@mui/icons-material/ExpandMore'), { ssr: false });

interface ContentsProps {
    datas: PaginatedList<Models.ManagerServerLog>;
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

    let result: ReactElement = <></>;
    if (0 < datas.total) {
        const list: ReactElement[] = [];

        for (let i = 0; i < datas.length; i++) {
            const data = datas.items[i];

            // const root : DataTableModels.NavMenuData = naveMenuTable[data.urlName.replace("/GMS", "")] ?? null;
            // const error : DataTableModels.ErrorsData  = errorTable[data.errorId] ?? null;

            const row = (
                <BorderedTableRow key={i}>
                    <TableCell component="th" scope="row">{datas.offsetIndex - i}</TableCell>
                    <TableCell>{data.logType}</TableCell>
                    <TableCell>{dayjs(data.logTime).tz().format('YYYY-MM-DD HH:mm:ss')}</TableCell>
                    <TableCell sx={{ textAlign: 'left !important' }}>{data.logData.replaceAll(/(?<=.{10})./g, '*')}</TableCell>
                </BorderedTableRow>
            );

            list.push(row);
        }
        result = (
            <>
                <TableHead>
                    <BorderedTableRow>
                        <TableCell>순번</TableCell>
                        <TableCell>로그레벨</TableCell>
                        <TableCell>시간</TableCell>
                        <TableCell>내용</TableCell>
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
                    <TableCell className={commonUIStyles.noneCell}>검색된 로그 데이터가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }

    return result;
}

export default Contents;