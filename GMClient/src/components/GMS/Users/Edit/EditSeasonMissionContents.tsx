import { ChangeEvent, ReactElement, useCallback, useEffect, useState } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TextField, styled } from '@mui/material';
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
    userAccount: Models.UserAccount;
    datas: PaginatedList<Models.SeasonMission>;
}

const SeasonMissionContents = ({ userAccount, datas }: ContentsProps): ReactElement => {

    let content: ReactElement = <></>;
    const tables = useAppSelector(state => state.tables);
    const seasonMissionTable = tables.seasonMissionListDataTable;
    const characterDataTable = tables.characterDataTable;

    const [seasonMissions, setSeasonMissions] = useState<PaginatedList<Models.SeasonMission>>(new PaginatedList<Models.SeasonMission>([]));
    const isSigned = userAccount.IsSignIn;

    useEffect(() => {
        setSeasonMissions(datas)
    }, [datas]);

    const onChangeMissionCount = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>, index: number) => {
        if ((!e.target.value) && Number(e.target.value) != 0 || isNaN(Number(e.target.value)))
            return;

        let maxCount = 10;
        setSeasonMissions(prev => {
            const data = prev.totalItems[(prev.page - 1) * prev.pageSize + index];
            if (data) {
                maxCount = seasonMissionTable.find(element => element?.ID == data?.MissionID)?.MissionCount ?? 0;
            }

            if (Number(e.target.value) >= maxCount) {
                prev.totalItems[(prev.page - 1) * prev.pageSize + index].Count = maxCount;
                prev.totalItems[(prev.page - 1) * prev.pageSize + index].IsComplete = true;
            }
            else {
                prev.totalItems[(prev.page - 1) * prev.pageSize + index].Count = Number(e.target.value);
                prev.totalItems[(prev.page - 1) * prev.pageSize + index].IsComplete = false;
            }
            prev.totalItems[(prev.page - 1) * prev.pageSize + index].isChanged = true;
            return new PaginatedList<Models.SeasonMission>(prev.totalItems, prev.page, prev.parameters, prev.pageSize, prev.pageBlockSize);
        })
    }, [seasonMissionTable, setSeasonMissions]);

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];

        if (seasonMissions && 0 < seasonMissions.items.length) {
            for (let i = 0; i < seasonMissions.items.length; i++) {
                const data = seasonMissions.items[i];
                let rowClass: string = commonUIStyles.row;
                if (data.isChanged)
                    rowClass = `${commonUIStyles.row} ${commonUIStyles.changed}`;

                const missionInfo = seasonMissionTable.find(element => element?.ID === data?.MissionID);

                let missionDes = missionInfo?.DescriptionStringWithID;
                if (missionDes?.includes("{Value}")) {
                    const ValueText = missionInfo?.MissionValue.toString() ?? "";
                    missionDes = missionDes.replace("{Value}", ValueText);
                }
                if (missionDes?.includes("{HeroName}") || missionDes?.includes("{CharacterName}") || missionDes?.includes("{MonsterName}")) {
                    const monster = characterDataTable.find(element => element?.ID === missionInfo?.MissionValue);
                    const monsterName = `${monster?.NameStringWithID} ` ?? "";
                    const characterName = `${missionInfo?.CharacterNameStringWithID} ` ?? "";

                    missionDes = missionDes.replaceAll("{HeroName}", characterName).replaceAll("{CharacterName}", characterName).replaceAll("{MonsterName}", monsterName);
                }

                if (missionDes?.includes("{Count}")) {
                    const CountText = missionInfo?.MissionCount.toString() ?? "";
                    missionDes = missionDes.replace("{Count}", CountText);
                }

                list.push(
                    <BorderedTableRow key={i} className={rowClass}>
                        <TableCell>{data?.SeasonPassID}</TableCell>
                        <TableCell sx={{ color: data?.IsComplete ? 'blue' : 'red' }}>{data?.IsComplete ? "완료" : "미완료"}</TableCell>
                        <TableCell>{missionDes?.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>
                            <TextField disabled={isSigned} className={commonUIStyles.input} variant='outlined' fullWidth value={data?.Count} size='small' placeholder='제목' onChange={e => onChangeMissionCount(e, i)} />
                        </TableCell>
                        <TableCell>{data?.CreateAt ? dayjs(data.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        <TableCell>{data?.UpdateAt ? dayjs(data.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    </BorderedTableRow>
                )
            }
            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>시즌패스</TableCell>
                            <TableCell>완료여부</TableCell>
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
        else {
            result = (
                <TableBody>
                    <BorderedTableRow>
                        <TableCell colSpan={7} className={commonUIStyles.noneCell}>검색된 시즌미션 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }

        return result;
    }, [seasonMissions, characterDataTable, seasonMissionTable, isSigned, onChangeMissionCount]);


    content = (
        <>
            {contents()}
        </>
    );

    return content;
}

export default SeasonMissionContents;