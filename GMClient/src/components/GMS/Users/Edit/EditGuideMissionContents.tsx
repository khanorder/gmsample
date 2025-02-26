import { ReactElement, useCallback, useEffect, useState } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { EGuideMissionCategory, EGuideMissionType } from '@ngel/data/models/lobby';
import dynamic from 'next/dynamic';
import { TextField, Autocomplete, styled, SelectChangeEvent } from '@mui/material';
import { useAppSelector } from '@hooks/index';
import { DataTableModels } from '@ngel/data/tables/model';
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const Select = dynamic(() => import('@mui/material/Select'), { ssr: false });
const MenuItem = dynamic(() => import('@mui/material/MenuItem'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });

interface ContentsProps {
    userAccount: Models.UserAccount;
    datas: PaginatedList<Models.GuideMission>;
}

const categoryTranslate = (categoryNum: string, type: number = 0) => {
    if (isNaN(Number(categoryNum))) return "";

    if (type == 0) {
        return EGuideMissionCategory[categoryNum];
    }
    else {
        return EGuideMissionType[categoryNum];
    }
}

const GuideMissionContents = ({ userAccount, datas }: ContentsProps): ReactElement => {

    let content: ReactElement = <></>;
    const tables = useAppSelector(state => state.tables);
    const guideMissionDataTable = tables.guideMissionDataTable;

    const [opts, setOpts] = useState<DataTableModels.GuideMissionData[]>([]);
    const [guideMissions, setGuideMissions] = useState<PaginatedList<Models.GuideMission>>(new PaginatedList<Models.GuideMission>([]));

    const isSigned = userAccount?.IsSignIn ?? false;

    useEffect(() => {
        setGuideMissions(datas);
    }, [datas])

    useEffect(() => {
        const options: DataTableModels.GuideMissionData[] = [];

        for (const key in guideMissionDataTable) {
            const guideMissionInfo = guideMissionDataTable[key];

            options.push(guideMissionInfo);
        }

        setOpts(options);
    }, [guideMissionDataTable])

    const onChangeData = useCallback((e, v: DataTableModels.GuideMissionData, index: number) => {
        if (!v) return;

        setGuideMissions(prev => {
            if (prev.totalItems[(prev.page - 1) * prev.pageSize + index]) {
                prev.totalItems[(prev.page - 1) * prev.pageSize + index].MissionID = v.ID;
                prev.totalItems[(prev.page - 1) * prev.pageSize + index].isChanged = true;
            }
            return new PaginatedList<Models.GuideMission>(prev.totalItems, prev.page, prev.parameters, prev.pageSize, prev.pageBlockSize);
        })
    }, [setGuideMissions]);

    const onChangeCompleteNRewardType = useCallback((e: SelectChangeEvent<unknown>, index: number, isRewardType: boolean) => {
        let value = false;
        if (e && e.target && e.target.value) {
            try {
                value = e.target.value as boolean ?? false;
            } catch (error) {
                if ("production" != process.env.NODE_ENV) {
                    console.log(error);
                }
            }
        }

        setGuideMissions(prev => {
            if (prev.totalItems[(prev.page - 1) * prev.pageSize + index]) {
                if (isRewardType)
                    prev.totalItems[(prev.page - 1) * prev.pageSize + index].IsRewarded = value;
                else
                    prev.totalItems[(prev.page - 1) * prev.pageSize + index].IsCompleted = value;

                prev.totalItems[(prev.page - 1) * prev.pageSize + index].isChanged = true;
            }
            return new PaginatedList<Models.GuideMission>(prev.totalItems, prev.page, prev.parameters, prev.pageSize, prev.pageBlockSize);
        })

    }, [setGuideMissions]);


    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];

        if (guideMissions && 0 < guideMissions.items.length) {
            for (let i = 0; i < guideMissions.items.length; i++) {
                const data = guideMissions.items[i];

                let rowClass: string = commonUIStyles.row;
                if (data.isChanged)
                    rowClass = `${commonUIStyles.row} ${commonUIStyles.changed}`;

                const filtered = opts.filter(element => element.GuideMissionCategory == data.GuideMissionCategory);
                const missionInfo = filtered.find(element => element?.ID === data?.MissionID);
                const maxStep = filtered.sort((a, b) => b.Step - a.Step)[0]?.Step ?? 0;

                list.push(
                    <BorderedTableRow key={i} className={rowClass}>
                        <TableCell>{categoryTranslate(data?.GuideMissionCategory.toString(), 0)}</TableCell>
                        <TableCell>
                            <Autocomplete disabled={isSigned} options={filtered} sx={{ minWidth: 300 }} size='small'
                                getOptionLabel={(option) => (option as DataTableModels.GuideMissionData)?.TitleStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}
                                value={missionInfo} onChange={(e, v) => onChangeData(e, v as DataTableModels.GuideMissionData, i)}
                                renderInput={(params) => <TextField {...params} label="미션ID" />} />
                        </TableCell>
                        <TableCell>{missionInfo ? categoryTranslate(missionInfo?.GuideMissionCategory.toString(), 1).toString().replaceAll(/(?<=.{2})./g, '*') : ""}</TableCell>
                        <TableCell>{`${missionInfo?.Step ?? 0}/${maxStep}`}</TableCell>
                        <TableCell>{missionInfo?.TitleStringWithID ? missionInfo?.TitleStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : ""}</TableCell>
                        <TableCell>
                            <Select disabled={isSigned} size='small' sx={{ color: data?.IsCompleted ? 'blue' : 'red' }} value={data?.IsCompleted} onChange={(e) => { onChangeCompleteNRewardType(e, i, false) }}>
                                <MenuItem sx={{ color: 'red' }} value={false as any}>미완료</MenuItem>
                                <MenuItem sx={{ color: 'blue' }} value={true as any}>완료</MenuItem>
                            </Select>
                        </TableCell>
                        <TableCell>
                            <Select disabled={isSigned} size='small' sx={{ color: data?.IsRewarded ? 'blue' : 'red' }} value={data?.IsRewarded} onChange={(e) => { onChangeCompleteNRewardType(e, i, true) }}>
                                <MenuItem sx={{ color: 'red' }} value={false as any}>미수령</MenuItem>
                                <MenuItem sx={{ color: 'blue' }} value={true as any}>수령</MenuItem>
                            </Select>
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
                            <TableCell>카테고리</TableCell>
                            <TableCell>미션ID</TableCell>
                            <TableCell>미션카테고리</TableCell>
                            <TableCell>진행단계</TableCell>
                            <TableCell>미션제목</TableCell>
                            <TableCell>완료여부</TableCell>
                            <TableCell>보상여부</TableCell>
                            <TableCell>생성일시({timezoneName})</TableCell>
                            <TableCell>수정일시({timezoneName})</TableCell>
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
                        <TableCell colSpan={7} className={commonUIStyles.noneCell}>검색된 가이드미션 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }

        return result;
    }, [guideMissions, opts, isSigned, onChangeCompleteNRewardType, onChangeData]);


    content = (
        <>
            {contents()}
        </>
    );

    return content;
}

export default GuideMissionContents;