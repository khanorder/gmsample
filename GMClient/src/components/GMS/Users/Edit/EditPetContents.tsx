import { ReactElement } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import dayjs, { Dayjs } from 'dayjs';
import 'dayjs/locale/ko';
import timezone from 'dayjs/plugin/timezone.js';
import utc from 'dayjs/plugin/utc.js';
// dayjs.extend(timezone);
// dayjs.extend(utc);
import commonUIStyles from '@styles/ui/common.module.sass';
import { styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { Defines } from '@ngel/data/autoDefines';
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const MinWidthTableCell = styled(TableCell)(({ theme }) => ({
    minWidth: "120px"
}));

interface ContentsProps {
    datas: Models.Pet[];
}

const EditPetContents = ({ datas }: ContentsProps): ReactElement => {

    let result: ReactElement = <></>;

    if (datas && 0 < datas.length) {
        const list: ReactElement[] = [];

        for (let i = 0; i < datas.length; i++) {
            const data = datas[i];
            list.push(
                <BorderedTableRow key={i}>
                    <TableCell component="th" scope="row">{data.UniqueID}</TableCell>
                    <TableCell>{data.PetID}</TableCell>
                    <TableCell>{data.Ability}</TableCell>
                    <TableCell>{data.Like}</TableCell>
                    <TableCell>{data.IsDeleted}</TableCell>
                    <TableCell>{dayjs(data.CreateAt).format('YYYY-MM-DD HH:mm:ss')}</TableCell>
                    <TableCell>{dayjs(data.UpdateAt).format('YYYY-MM-DD HH:mm:ss')}</TableCell>
                </BorderedTableRow>
            )
        }

        result = (
            <>
                <TableHead>
                    <BorderedTableRow>
                        <MinWidthTableCell>팻 고유번호</MinWidthTableCell>
                        <MinWidthTableCell>펫 ID</MinWidthTableCell>
                        <MinWidthTableCell>랜덤 능력</MinWidthTableCell>
                        <MinWidthTableCell>호감도</MinWidthTableCell>
                        <MinWidthTableCell>삭제여부</MinWidthTableCell>
                        <MinWidthTableCell>생성일시</MinWidthTableCell>
                        <MinWidthTableCell>수정일시</MinWidthTableCell>
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
                    <TableCell className={commonUIStyles.noneCell}>검색된 펫 정보가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }

    return result;
}

export default EditPetContents;