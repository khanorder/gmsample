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
    datas: Models.PlayRecordRpg[];
}
const PlayRecordRpgContents = ({ datas } : ContentsProps) : ReactElement => {
    const tables = useAppSelector(state => state.tables);
    const chapterTable = tables.chapterDataTable;
    const characterTable = tables.characterDataTable;

    const contents = useCallback(() => {
        let result: ReactElement = <></>;
        if(datas && datas.length){
            const list: ReactElement[] = [];

            for(let i = 0 ; i < datas.length ; i++){
                const data = datas[i];
                const chapter = chapterTable?.find(element => element.ID == data?.ChapterID) ?? null;
                const character = characterTable?.find(element => element.ID == data?.HeroID) ?? null;

                list.push(
                    <BorderedTableRow key={`row-record-gold-${i}`}>
                        <TableCell>{chapter?.NameStringWithID ? chapter?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : data?.ChapterID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{character?.NameStringWithID ? character?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : data.HeroID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{data?.TopPoint ? data?.TopPoint.toString().replaceAll(/(?<=.{1})./g, '*') : ''}</TableCell>
                        <TableCell>{data?.AvgLevel ? data?.AvgLevel.toString().replaceAll(/(?<=.{1})./g, '*') : ''}</TableCell>
                        <TableCell>{data?.AvgArtifact ? data?.AvgArtifact.toString().replaceAll(/(?<=.{1})./g, '*') : ''}</TableCell>
                        <TableCell>{data?.AvgFragment ? data?.AvgFragment.toString().replaceAll(/(?<=.{1})./g, '*') : ''}</TableCell>
                        <TableCell>{data?.BossClear.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{data?.ShortestPlayTime.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{dayjs(data.CreateAt).tz().format("YYYY-MM-DD HH:mm:ss")}</TableCell>
                        <TableCell>{dayjs(data.UpdateAt).tz().format("YYYY-MM-DD HH:mm:ss")}</TableCell>
                    </BorderedTableRow>)
            }
            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>챕*</TableCell>
                            <TableCell>캐**</TableCell>
                            <TableCell>최* **</TableCell>
                            <TableCell>평* **</TableCell>
                            <TableCell>평* ** **</TableCell>
                            <TableCell>평* **</TableCell>
                            <TableCell>보* ** **</TableCell>
                            <TableCell>최* *** **</TableCell>
                            <TableCell>생성시간({timezoneName})</TableCell>
                            <TableCell>수정시간({timezoneName})</TableCell>
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
                            <TableCell className={commonUIStyles.noneCell}>검색된 글**** ** **가 없습니다.</TableCell>
                        </BorderedTableRow>
                    </TableBody>
                </>
            );
        }

        return result;
    }, [chapterTable, characterTable, datas])

    return <>{contents()}</>
}

export default PlayRecordRpgContents;