import { ReactElement, useCallback } from "react";
import dynamic from "next/dynamic";
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { Models } from "@ngel/data/models";
import { useAppSelector } from "@hooks/index";
import { EMarketCode, EPlatform } from "@ngel/data/models/lobby";

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });

interface ContentsProps {
    datas: Models.InstantGuide[];
}
const InstantGuideContents = ({ datas } : ContentsProps) : ReactElement => {
    const tables = useAppSelector(state => state.tables);

    const contents = useCallback(() => {
        let result: ReactElement = <></>;

        if(datas && 0 < datas.length && 0 < datas[0].InstantGuideList.length){
            const instanteGuideTable = tables?.instantGuideUIDataTable;
            const list: ReactElement[] = [];
            const sorted = datas[0].InstantGuideList.sort((a, b) => Number(a) - Number(b));
            for(let i = 0; i < sorted.length; i++){
                const instantGuide = instanteGuideTable?.find(element => element.ID ==  sorted[i]) ?? null;

                list.push(<BorderedTableRow key={`instantGuide-${i}-row`}>
                    <TableCell>{instantGuide?.GuideCategoryNameStringWithID ? instantGuide?.GuideCategoryNameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : datas[0].InstantGuideList[i]}</TableCell>
                    <TableCell colSpan={2}>{instantGuide?.TitleStringWithID ? instantGuide?.TitleStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : datas[0].InstantGuideList[i]}</TableCell>
                </BorderedTableRow>)
            }

            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>UID</TableCell>
                            <TableCell>생성시간({timezoneName})</TableCell>
                            <TableCell>수정시간({timezoneName})</TableCell>
                        </BorderedTableRow>
                    </TableHead>
                    <TableBody>
                        <BorderedTableRow>
                            <TableCell>{datas[0].UID}</TableCell>
                            <TableCell>{dayjs(datas[0].CreateAt).tz().format("YYYY-MM-DD HH:mm:ss")}</TableCell>
                            <TableCell>{dayjs(datas[0].UpdateAt).tz().format("YYYY-MM-DD HH:mm:ss")}</TableCell>
                        </BorderedTableRow>
                    </TableBody>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>가이* ****</TableCell>
                            <TableCell colSpan={2}>제목</TableCell>
                        </BorderedTableRow>
                    </TableHead>
                    <TableBody>
                        {list}
                    </TableBody>
                </>
            )
        } else {
            result = (
                <>
                    <TableBody>
                        <BorderedTableRow>
                            <TableCell className={commonUIStyles.noneCell}>검색된 인스** *** **가 없습니다.</TableCell>
                        </BorderedTableRow>
                    </TableBody>
                </>
            );
        }

        return result;
    }, [tables, datas])

    return <>{contents()}</>
}

export default InstantGuideContents;