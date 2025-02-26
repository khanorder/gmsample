import { useAppSelector } from '@hooks/index';
import { ChangeEvent, ReactElement, MutableRefObject, useCallback, useState, useEffect } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TextField, Autocomplete, styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { Defines } from '@ngel/data/autoDefines';
import { DataTableModels } from '@ngel/data/tables/model';

const DateTimePicker = dynamic(() => import('@components/ui/DateTimePicker'), { ssr: false });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });

const MinWidthTableCell = styled(TableCell)(({ theme }) => ({
    minWidth: "120px"
}));

interface ContentsProps {
    userAccount: Models.UserAccount;
    datas: PaginatedList<Models.Achievement>;
    opts: DataTableModels.AchievementData[];
    onDelete: (index: number) => void;
}

const EditArtifactContents = ({ userAccount, datas, opts, onDelete }: ContentsProps): ReactElement => {
    let content: ReactElement = <></>;
    const tables = useAppSelector(state => state.tables);
    const achievementGroupDataTable = tables.achievementGroupDataTable;

    const [achievementOpts, setAchievementOpts] = useState<DataTableModels.AchievementData[]>([]);
    const [achievements, setAchievements] = useState<PaginatedList<Models.Achievement>>(new PaginatedList<Models.Achievement>([]));
    const isSigned = userAccount.IsSignIn;

    useEffect(() => {
        setAchievements(datas)
    }, [datas]);

    useEffect(() => {
        setAchievementOpts(opts)
    }, [opts]);

    const onChangeData = useCallback((e, v: DataTableModels.AchievementData, index: number) => {
        if (!v) return;

        const isOverlap = achievements.totalItems.find((element) => element.AchievementID === v.ID);
        if (isOverlap) {
            alert("중복된 값입니다.");
            return;
        }

        setAchievements(prev => {
            if (prev.totalItems[(prev.page - 1) * prev.pageSize + index]) {
                prev.totalItems[(prev.page - 1) * prev.pageSize + index].AchievementID = v.ID;
                prev.totalItems[(prev.page - 1) * prev.pageSize + index].isChanged = true;
            }
            return new PaginatedList<Models.Achievement>(prev.totalItems, prev.page, prev.parameters, prev.pageSize, prev.pageBlockSize);
        })
    }, [achievements, setAchievements]);

    const onChangeCount = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>, index: number, maxCount: number) => {
        if (!e.target.value || isNaN(Number(e.target.value))) return;

        let countVal = Number(e.target.value) <= maxCount ? Number(e.target.value) : maxCount;
        countVal = countVal == 0 ? 1 : countVal;
        setAchievements(prev => {
            if (prev.totalItems[(prev.page - 1) * prev.pageSize + index]) {
                prev.totalItems[(prev.page - 1) * prev.pageSize + index].Count = countVal;
                prev.totalItems[(prev.page - 1) * prev.pageSize + index].isChanged = true;
            }
            return new PaginatedList<Models.Achievement>(prev.totalItems, prev.page, prev.parameters, prev.pageSize, prev.pageBlockSize);
        })
    }, [setAchievements]);

    const onChangeCompleteAt = useCallback((date: string | null, index: number) => {
        if (date == "Invalid Date") {
            return;
        }

        const dateStr = date ?? "";
        let dateObj, currentDate;
        try {
            dateObj = dayjs(dateStr);
            currentDate = new Date();

            if (dayjs(currentDate).diff(dateObj, 'second') <= 0) {
                alert("현재 시간보다 이후 시간입니다.")
                return;
            }
        } catch (error) {
            return;
        }

        if (dateObj) {
            const unixTimestamp = dateObj?.unix();
            if (unixTimestamp < 0) return;
            setAchievements(prev => {
                if (prev.totalItems[(prev.page - 1) * prev.pageSize + index]) {
                    prev.totalItems[(prev.page - 1) * prev.pageSize + index].CompleteAt = unixTimestamp;
                    prev.totalItems[(prev.page - 1) * prev.pageSize + index].isChanged = true;
                }
                return new PaginatedList<Models.Achievement>(prev.totalItems, prev.page, prev.parameters, prev.pageSize, prev.pageBlockSize);
            })
        }

    }, [setAchievements]);

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;

        if (achievements && 0 < achievements?.items.length) {
            const list: ReactElement[] = [];

            for (let i = 0; i < achievements.items.length; i++) {
                const data = achievements.items[i];

                let rowClass: string = commonUIStyles.row;
                if (data?.isChanged) rowClass = `${commonUIStyles.row} ${commonUIStyles.changed}`

                let achInfo = achievementOpts.find((element) => element?.ID == data?.AchievementID) ?? achievementOpts[0];
                const achGroupInfo = achievementGroupDataTable.find(element => element?.ID === achInfo?.AchievementGroupID) ?? null;

                list.push(
                    <BorderedTableRow key={i} className={rowClass}>
                        <TableCell component="th" scope="row">
                            <Button disabled={isSigned} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant='outlined' size='small' color='error' onClick={() => onDelete(i)}>삭제</Button>
                        </TableCell>
                        <TableCell>
                            <FormControl>
                                {data?.isNewData ?
                                    <Autocomplete disabled={isSigned} options={achievementOpts} sx={{ width: 300 }} size='small'
                                        getOptionLabel={(option) => (option as DataTableModels.AchievementData)?.NameStringWithID}
                                        value={achInfo} onChange={(e, v) => onChangeData(e, v as DataTableModels.AchievementData, i)}
                                        renderInput={(params) => <TextField {...params} label="업적명" />} />
                                    :
                                    <Typography>{achInfo?.NameStringWithID ? achInfo?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.AchievementID.toString().replaceAll(/(?<=.{2})./g, '*')}</Typography>
                                }
                            </FormControl>
                        </TableCell>
                        <TableCell>{achGroupInfo?.CategoryNameStringWithID ? achGroupInfo?.CategoryNameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : achInfo?.AchievementGroupID ? achGroupInfo?.CategoryNameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : ""}</TableCell>
                        <TableCell>
                            <TextField disabled={isSigned} value={data?.Count} onChange={(e) => { onChangeCount(e, i, achInfo?.AchievementCount ?? 0) }} size='small' />
                        </TableCell>
                        <TableCell>{achInfo?.AchievementCount ?? 0}</TableCell>
                        <TableCell>
                            {
                                data?.isNewData
                                    ?
                                    <DateTimePicker label="완료시간" enableTimePicker={true} format="YYYY-MM-DD HH:mm" value={data?.CompleteAt == 0 ? null : dayjs.unix(data?.CompleteAt).tz().format("YYYY-MM-DD HH:mm")} onChange={(date) => onChangeCompleteAt(date, i)} />
                                    :
                                    <Typography>{data?.CompleteAt ? dayjs.unix(data?.CompleteAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</Typography>
                            }
                        </TableCell>
                        <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>

                    </BorderedTableRow>
                )
            }
            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <MinWidthTableCell>관리</MinWidthTableCell>
                            <MinWidthTableCell>업적 이름(ID)</MinWidthTableCell>
                            <MinWidthTableCell>업적 카테고리(ID)</MinWidthTableCell>
                            <MinWidthTableCell>횟수</MinWidthTableCell>
                            <MinWidthTableCell>최대횟수</MinWidthTableCell>
                            <MinWidthTableCell>완료시간({timezoneName})</MinWidthTableCell>
                            <MinWidthTableCell>생성시간({timezoneName})</MinWidthTableCell>
                            <MinWidthTableCell>수정시간({timezoneName})</MinWidthTableCell>
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
                        <TableCell className={commonUIStyles.noneCell}>검색된 유물 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }
        return result
    }, [achievementGroupDataTable, isSigned, achievementOpts, achievements, onChangeCount, onChangeData, onDelete, onChangeCompleteAt]);

    content = (<>
        {contents()}
    </>);

    return content;
}

export default EditArtifactContents;