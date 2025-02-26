import { ReactElement, useCallback, useEffect, useRef, useState } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { EGuideMissionCategory, EGuideMissionType } from '@ngel/data/models/lobby';
import { useAppSelector } from '@hooks/index';
import dynamic from 'next/dynamic';

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });

interface ContentsProps {
    datas: PaginatedList<Models.GuideMission>;
}
const categoryTranslate = (categoryNum: string, type: number = 0) => {
    if (isNaN(Number(categoryNum))) return "";

    if (type == 0) {
        return EGuideMissionCategory[categoryNum];
    } else {
        return EGuideMissionType[categoryNum];
    }
}

const GuideMissionContents = ({ datas }: ContentsProps): ReactElement => {
    const firstRender = useRef(true);
    const tables = useAppSelector(state => state.tables);
    const [wonderLicense, setWonderLicense] = useState<string>('');
    const guideMissionDataTable = tables.guideMissionDataTable;
    const guideMissionStepRewardTable = tables.guideMissionStepRewardTable;

    useEffect(() => {
        if (firstRender.current) {
            firstRender.current = false;
            let succeededCount = 0;
            if (datas && 0 < datas.totalItems.length) {
                for (let i = 0; i < datas.totalItems.length; i++) {
                    const data = datas.totalItems[i];
                    const filtered = guideMissionDataTable.filter(element => element.GuideMissionCategory == data.GuideMissionCategory);
                    const missionInfo = filtered.find(element => element?.ID === data?.MissionID);
                    succeededCount += missionInfo?.Step ?? 0;
                }

                const guideMissionStepRewardDatas = guideMissionStepRewardTable.filter(element => succeededCount >= element?.ReqCompleteMissionCount);
                if (guideMissionStepRewardDatas.length > 0) {
                    const guideMissionStepRewardData = guideMissionStepRewardDatas.sort((a, b) => b.ID - a.ID)[0];
                    if (guideMissionStepRewardData)
                        setWonderLicense(guideMissionStepRewardData?.GradeType ?? '');
                }
            }
        }
    }, [firstRender, datas, guideMissionDataTable, guideMissionStepRewardTable, setWonderLicense]);

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];

        if (datas && 0 < datas.items.length) {
            for (let i = 0; i < datas.items.length; i++) {
                const data = datas.items[i];
                const filtered = guideMissionDataTable.filter(element => element.GuideMissionCategory == data.GuideMissionCategory);
                const missionInfo = filtered.find(element => element?.ID === data?.MissionID);
                const maxStep = filtered.sort((a, b) => b.Step - a.Step)[0]?.Step ?? 0;

                list.push(
                    <BorderedTableRow key={i}>
                        <TableCell>{categoryTranslate(data?.GuideMissionCategory.toString(), 0).toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{data.MissionID.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{missionInfo ? categoryTranslate(missionInfo?.GuideMissionCategory.toString(), 1).toString().replaceAll(/(?<=.{2})./g, '*') : ""}</TableCell>
                        <TableCell>{`${missionInfo?.Step ?? 0}/${maxStep}`}</TableCell>
                        <TableCell>{missionInfo?.TitleStringWithID ? missionInfo?.TitleStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : ""}</TableCell>
                        <TableCell sx={{ color: data?.IsCompleted ? 'blue' : 'red' }}>{data?.IsCompleted ? "완료" : "미완료"}</TableCell>
                        <TableCell sx={{ color: data?.IsRewarded ? 'blue' : 'red' }}>{data?.IsRewarded ? "수령" : "미수령"}</TableCell>
                        <TableCell>{data?.CreateAt ? dayjs(data.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        <TableCell>{data?.UpdateAt ? dayjs(data.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    </BorderedTableRow>
                );
            }

            result = (
                <TableBody>
                    {list}
                </TableBody>
            );
        } else {
            result = (
                <TableBody>
                    <BorderedTableRow>
                        <TableCell colSpan={7} className={commonUIStyles.noneCell}>검색된 가이*** **가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }

        return result;
    }, [datas, guideMissionDataTable]);

    return (
        <>
            <TableHead>
                <BorderedTableRow>
                    <TableCell colSpan={2}>라이** **</TableCell>
                    <TableCell colSpan={1} component='td'><strong>{wonderLicense.toString().replaceAll(/(?<=.{2})./g, '*')}</strong></TableCell>
                    <TableCell colSpan={7} component='td'></TableCell>
                </BorderedTableRow>
            </TableHead>
            <TableHead>
                <BorderedTableRow>
                    <TableCell>카테**</TableCell>
                    <TableCell>미션**</TableCell>
                    <TableCell>미션****</TableCell>
                    <TableCell>진행단계</TableCell>
                    <TableCell>미션제목</TableCell>
                    <TableCell>완료여부</TableCell>
                    <TableCell>보상여부</TableCell>
                    <TableCell>생성일시({timezoneName})</TableCell>
                    <TableCell>수정일시({timezoneName})</TableCell>
                </BorderedTableRow>
            </TableHead>
            {contents()}
        </>
    );
}

export default GuideMissionContents;