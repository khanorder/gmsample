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
    datas: Models.ExpressionPreset[];
}
const ExpressionPresetContents = ({ datas } : ContentsProps) : ReactElement => {
    const tables = useAppSelector(state => state.tables);

    const contents = useCallback(() => {
        let result: ReactElement = <></>;
        if(datas && datas.length){
            const characterTable = tables?.characterDataTable;
            const expressionTable = tables?.expressionDataTable;
            const list: ReactElement[] = [];
            for(let i = 0; i < datas.length; i++){
                const data = datas[i];
                const character = characterTable?.find(element => element.ID == data?.HeroID) ?? null;

                const expressionList =  data.Preset;
                list.push(<BorderedTableRow key={`expression-preset-${i}-row`}>
                    <TableCell>{ character?.NameStringWithID ? character?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : data?.HeroID}</TableCell>
                    {
                        expressionList.map((element, i) => {
                        const expression = expressionTable?.find((info) => info.ID == Number(element))?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') ?? '0';
                        return <TableCell key={`expression-slot-${i}`}>{expression ?? 0}</TableCell>;
                        })
                    }
                    <TableCell>{dayjs(data.CreateAt).tz().format("YYYY-MM-DD HH:mm:ss")}</TableCell>
                    <TableCell>{dayjs(data.UpdateAt).tz().format("YYYY-MM-DD HH:mm:ss")}</TableCell>
                </BorderedTableRow>)
            }

            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>캐릭터</TableCell>
                            <TableCell>1* **</TableCell>
                            <TableCell>2* **</TableCell>
                            <TableCell>3* **</TableCell>
                            <TableCell>4* **</TableCell>
                            <TableCell>5* **</TableCell>
                            <TableCell>6* **</TableCell>
                            <TableCell>7* **</TableCell>
                            <TableCell>8* **</TableCell>
                            <TableCell>9* **</TableCell>
                            <TableCell>1* ***</TableCell>
                            <TableCell>1* ***</TableCell>
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
                            <TableCell className={commonUIStyles.noneCell}>검색된 감정******* 정보가 없습니다.</TableCell>
                        </BorderedTableRow>
                    </TableBody>
                </>
            );
        }

        return result;
    }, [tables, datas])

    return <>{contents()}</>
}

export default ExpressionPresetContents;