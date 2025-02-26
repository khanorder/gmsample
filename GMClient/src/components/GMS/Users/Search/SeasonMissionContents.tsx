import { ReactElement, useCallback } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { Defines } from '@ngel/data/autoDefines';
import { useAppSelector } from '@hooks/index';
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const MinWidthTableCell = styled(TableCell)(({ theme }) => ({
    minWidth: "120px"
}));

interface ContentsProps {
    datas: PaginatedList<Models.SeasonMission>;
}

const SeasonMissionContents = ({ datas }: ContentsProps): ReactElement => {

    let content: ReactElement = <></>;
    const tables = useAppSelector(state => state.tables);
    const seasonMissionTable = tables.seasonMissionListDataTable;
    const characterDataTable = tables.characterDataTable;

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];

        if(datas && 0 < datas.items.length){
            for(let i = 0; i < datas.items.length; i++){
                const data = datas.items[i];
                const missionInfo = seasonMissionTable.find(element => element?.ID === data?.MissionID);

                let missionDes = missionInfo?.DescriptionStringWithID;
                if(missionDes?.includes("{Value}")) {
                    const ValueText = missionInfo?.MissionValue.toString() ?? "";
                    missionDes = missionDes.replace("{Value}", ValueText);
                }
                if(missionDes?.includes("{HeroName}") || missionDes?.includes("{MonsterName}")) {
                    const monster = characterDataTable.find(element => element?.ID === missionInfo?.MissionValue);
                    const monsterName = `${monster?.NameStringWithID} ` ?? "";
                    const characterName =`${missionInfo?.CharacterNameStringWithID} ` ?? "";
                    
                    missionDes = missionDes.replaceAll("{HeroName}", characterName).replaceAll("{MonsterName}", monsterName);
                }

                if(missionDes?.includes("{Count}")) {
                    const CountText = missionInfo?.MissionCount.toString() ?? "";
                    missionDes = missionDes.replace("{Count}", CountText);
                }
                
                list.push(
                    <BorderedTableRow key={i}>
                        <TableCell>{data?.SeasonPassID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell sx={{color: data?.IsComplete ? 'blue' : 'red'}}>{data?.IsComplete ? "완료" : "미완료" }</TableCell>
                        <TableCell>{missionDes?.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{data?.Count}</TableCell>
                        <TableCell>{data?.CreateAt ? dayjs(data.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        <TableCell>{data?.UpdateAt ? dayjs(data.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    </BorderedTableRow>
                )
            }
            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>시즌**</TableCell>
                            <TableCell>완료**</TableCell>
                            <TableCell>챕터</TableCell>
                            <TableCell>횟수</TableCell>
                            <TableCell>생성일시({timezoneName})</TableCell>
                            <TableCell>수정일시({timezoneName})</TableCell>
                        </BorderedTableRow>
                    </TableHead>
                    <TableBody>
                        {list}
                    </TableBody>
                </>
            );
        }
        else{
            result = (
                <TableBody>
                    <BorderedTableRow>
                        <TableCell colSpan={7} className={commonUIStyles.noneCell}>검색된 시즌** **가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }

        return result;
    }, [datas, characterDataTable, seasonMissionTable]);
    

    content = (
        <>
            {contents()}
        </>
    );
    
    return content;
}

export default SeasonMissionContents;