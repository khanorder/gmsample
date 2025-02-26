import { ReactElement } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import bignumber from 'bignumber.js';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
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
    datas: Models.HeroSkin[];
}

const EditHeroSkinContents = ({ datas }: ContentsProps): ReactElement => {

    let result: ReactElement = <></>;

    if (datas && 0 < datas.length) {
        const list: ReactElement[] = [];

        for (let i = 0; i < datas.length; i++) {
            const data = datas[i];
            list.push(
                <BorderedTableRow key={i}>
                    <TableCell component="th" scope="row">{data.SkinID}</TableCell>
                    <TableCell>{data.HexColor1}</TableCell>
                    <TableCell>{data.HexColor2}</TableCell>
                    <TableCell>{data.HexColor3}</TableCell>
                    <TableCell>{data.HexColor4}</TableCell>
                    <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                </BorderedTableRow>
            )
        }

        result = (
            <>
                <TableHead>
                    <BorderedTableRow>
                        <MinWidthTableCell>영웅 스킨 ID</MinWidthTableCell>
                        <MinWidthTableCell>Color1</MinWidthTableCell>
                        <MinWidthTableCell>Color2</MinWidthTableCell>
                        <MinWidthTableCell>Color3</MinWidthTableCell>
                        <MinWidthTableCell>Color4</MinWidthTableCell>
                        <MinWidthTableCell>생성일시({timezoneName})</MinWidthTableCell>
                        <MinWidthTableCell>수정일시({timezoneName})</MinWidthTableCell>
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
                    <TableCell className={commonUIStyles.noneCell}>검색된 영웅스킨 정보가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }

    return result;
}

export default EditHeroSkinContents;