import { ReactElement, useCallback } from "react";
import dynamic from "next/dynamic";
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { Models } from "@ngel/data/models";
import { useAppSelector } from "@hooks/index";

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });

interface ContentsProps {
    datas: Models.PlayRecordGoldClash[];
}
const PlayRecordGoldClashContents = ({ datas } : ContentsProps) : ReactElement => {
    const tables = useAppSelector(state => state.tables);
    const characterTable = tables.characterDataTable;
    const contents = useCallback(() => {
        let result: ReactElement = <></>;
        if(datas && datas.length){
            const list: ReactElement[] = [];

            for(let i = 0; i < datas.length; i++){
                const data = datas[i];
                const character = characterTable?.find(element => element.ID == data?.HeroID ?? 0) ?? null;

                list.push(
                <BorderedTableRow key={`row-record-gold-${i}`}>
                    <TableCell>{data?.SeasonID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                    <TableCell>{character?.NameStringWithID ? character?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : data.HeroID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                    <TableCell>{data?.Win ? data?.Win.toString().replaceAll(/(?<=.{1})./g, '*') : ''}</TableCell>
                    <TableCell>{data?.Lose ? data?.Lose.toString().replaceAll(/(?<=.{1})./g, '*') : ''}</TableCell>
                    <TableCell>{data?.Kill ? data?.Kill.toString().replaceAll(/(?<=.{1})./g, '*') : ''}</TableCell>
                    <TableCell>{data?.Death ? data?.Death.toString().replaceAll(/(?<=.{1})./g, '*') : ''}</TableCell>
                    <TableCell>{data?.Mvp.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                    <TableCell>{data?.AvgDamage.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                    <TableCell>{data?.AvgDamageBlock.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                    <TableCell>{data?.AvgHeal.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                    <TableCell>{data?.AvgGold.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                    <TableCell>{dayjs(data.CreateAt).tz().format("YYYY-MM-DD HH:mm:ss")}</TableCell>
                    <TableCell>{dayjs(data.UpdateAt).tz().format("YYYY-MM-DD HH:mm:ss")}</TableCell>
                </BorderedTableRow>)
            }

            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>시즌</TableCell>
                            <TableCell>캐**</TableCell>
                            <TableCell>승* **</TableCell>
                            <TableCell>패* **</TableCell>
                            <TableCell>킬</TableCell>
                            <TableCell>사*</TableCell>
                            <TableCell>MV*</TableCell>
                            <TableCell>평* **</TableCell>
                            <TableCell>평* ****</TableCell>
                            <TableCell>평* **</TableCell>
                            <TableCell>평* **</TableCell>
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
                            <TableCell className={commonUIStyles.noneCell}>검색된 골**** ** **가 없습니다.</TableCell>
                        </BorderedTableRow>
                    </TableBody>
                </>
            );
        }

        return result;
    }, [characterTable, datas])

    return <>{contents()}</>
}

export default PlayRecordGoldClashContents;