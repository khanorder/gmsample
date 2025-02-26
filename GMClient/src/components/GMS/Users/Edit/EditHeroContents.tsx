import { ReactElement } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
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
import { useAppSelector } from '@hooks/index';
import { ENpcType } from '@ngel/data/models/lobby';
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const MinWidthTableCell = styled(TableCell)(({ theme }) => ({
    minWidth: "120px"
}));

interface ContentsProps {
    datas: Models.Hero[];
}

const EditHeroContents = ({ datas }: ContentsProps): ReactElement => {

    let result: ReactElement = <></>;
    const ItemTable = useAppSelector(state => state.tables.itemTable);
    const NpcHeroDataTable = useAppSelector(state => state.tables.characterDataTable).filter((element)=> element.NPCType === ENpcType.None);
    const NpcHeroSkinDataTable = useAppSelector(state => state.tables.skinDataTable);

    if (datas && 0 < datas.length) {
        const list: ReactElement[] = [];

        for (let i = 0; i < datas.length; i++) {
            const data = datas[i];
            const bodySkinItem = NpcHeroSkinDataTable.find((element)=> element?.ID === data?.BodySkinID);
            const hairSkinItem = NpcHeroSkinDataTable.find((element)=> element?.ID === data?.HairSkinID);
            const headItem = ItemTable.find((element)=> element?.ID === data?.HeadID);
            const faceItem = ItemTable.find((element)=> element?.ID === data?.FaceID);
            const backItem = ItemTable.find((element)=> element?.ID === data?.BackID);
            const weaponItem = ItemTable.find((element)=> element?.ID === data?.WeaponID);
            const pelvisItem = ItemTable.find((element) => element?.ID === data?.PelvisID);
            const winPoseItem = ItemTable.find((element)=> element?.ID === data?.WinPoseID);
            list.push(
                <BorderedTableRow key={i}>
                    <TableCell component="th" scope="row">{NpcHeroDataTable[i]?.NameStringWithID}</TableCell>
                        <TableCell>{data?.BattleLevel ? data?.BattleLevel.toLocaleString() : data?.BattleLevel}</TableCell>
                        <TableCell>{data?.BattleExp ? data?.BattleExp.toLocaleString() : data?.BattleExp}</TableCell>
                        <TableCell>{data?.RewardedLevel ? data?.RewardedLevel.toLocaleString() : data?.RewardedLevel}</TableCell>
                        <TableCell>{data?.LeftEyeHexColor}</TableCell>
                        <TableCell>{data?.RightEyeHexColor}</TableCell>
                        <TableCell>{hairSkinItem?.NameStringWithID ?? data?.HairSkinID}</TableCell>
                        <TableCell>{bodySkinItem?.NameStringWithID ?? data?.BodySkinID}</TableCell>
                        <TableCell>{headItem?.NameStringWithID ?? data?.HeadID}</TableCell>
                        <TableCell>{data?.HeadOffset}</TableCell>
                        <TableCell>{data?.HeadRotate}</TableCell>
                        <TableCell>{faceItem?.NameStringWithID ?? data?.FaceID}</TableCell>
                        <TableCell>{data?.FaceOffset}</TableCell>
                        <TableCell>{data?.FaceRotate}</TableCell>
                        <TableCell>{backItem?.NameStringWithID ?? data?.BackID}</TableCell>
                        <TableCell>{data?.BackOffset}</TableCell>
                        <TableCell>{data?.BackRotate}</TableCell>
                        <TableCell>{pelvisItem?.NameStringWithID ?? data?.PelvisID}</TableCell>
                        <TableCell>{data?.PelvisOffset}</TableCell>
                        <TableCell>{data?.PelvisRotate}</TableCell>
                        <TableCell>{weaponItem?.NameStringWithID ?? data?.WeaponID}</TableCell>
                        <TableCell>{winPoseItem?.NameStringWithID ?? data?.WinPoseID}</TableCell>
                        <TableCell>{data?.AddPresetCount}</TableCell>
                        <TableCell>{data?.ExpireAt ? dayjs.unix(data?.ExpireAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                </BorderedTableRow>
            )
        }

        result = (
            <>
                <TableHead>
                        <BorderedTableRow>
                        <MinWidthTableCell>영웅 ID</MinWidthTableCell>
                        <MinWidthTableCell>전투 레벨</MinWidthTableCell>
                        <MinWidthTableCell>전투 경험치</MinWidthTableCell>
                        <MinWidthTableCell>숙련도 보상 레벨</MinWidthTableCell>
                        <MinWidthTableCell>왼쪽 눈 색상</MinWidthTableCell>
                        <MinWidthTableCell>오른쪽 눈 색상</MinWidthTableCell>
                        <MinWidthTableCell>머리 스킨 ID</MinWidthTableCell>
                        <MinWidthTableCell>몸통 스킨 ID</MinWidthTableCell>
                        <MinWidthTableCell>머리장식 ID</MinWidthTableCell>
                        <MinWidthTableCell>머리장식 위치</MinWidthTableCell>
                        <MinWidthTableCell>머리장식 회전값</MinWidthTableCell>
                        <MinWidthTableCell>얼굴장식 ID</MinWidthTableCell>
                        <MinWidthTableCell>얼굴장식 위치</MinWidthTableCell>
                        <MinWidthTableCell>얼굴장식 회전값</MinWidthTableCell>
                        <MinWidthTableCell>등 장식 ID</MinWidthTableCell>
                        <MinWidthTableCell>등 장식 위치</MinWidthTableCell>
                        <MinWidthTableCell>등 장식 회전값</MinWidthTableCell>
                        <MinWidthTableCell>꼬리 장식 ID</MinWidthTableCell>
                        <MinWidthTableCell>꼬리 장식 위치</MinWidthTableCell>
                        <MinWidthTableCell>꼬리 장식 회전값</MinWidthTableCell>
                        <MinWidthTableCell>디피 무기 ID</MinWidthTableCell>
                        <MinWidthTableCell>승리 포즈</MinWidthTableCell>
                        <MinWidthTableCell>프리셋 추가횟수</MinWidthTableCell>
                        <MinWidthTableCell>체험권 만료일시({timezoneName})</MinWidthTableCell>
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
                    <TableCell className={commonUIStyles.noneCell}>검색된 영웅 정보가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }

    return result;
}

export default EditHeroContents;