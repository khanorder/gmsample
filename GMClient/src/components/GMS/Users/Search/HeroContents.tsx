import { ReactElement, useCallback } from 'react';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { useAppSelector } from '@hooks/index';
import { ENpcType } from '@ngel/data/models/lobby';

const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });

const MinWidthTableCell = styled(TableCell)(({ theme }) => ({
    minWidth: "120px",
    textAlign: 'left',
}));

interface RGBAColorType {
    r: number,
    g: number,
    b: number,
    a: number
}

// const ColorBox = styled(Box)<RGBAColorType>(({r, g, b, a}) => ({
//     width: 15, 
//     height: 15, 
//     marginRight: 10,
//     border: '1px solid rgb(121,121,121)',
//     backgroundColor: `rgba(${r},${g},${b},${a})`
// }))
interface HEXColorType {
    hexColor: string
}

const ColorBox = styled(Box)<HEXColorType>(({ hexColor }) => ({
    width: 15, 
    height: 15, 
    marginRight: 10,
    border: '1px solid rgb(121,121,121)',
    backgroundColor: `#${hexColor ?? 'FFFFFFFF'}`
}))

interface ContentsProps {
    datas: Models.Hero[];
}

const HeroContents = ({ datas }: ContentsProps): ReactElement => {

    const itemTable = useAppSelector(state => state.tables.itemTable);
    const colorDataTable = useAppSelector(state => state.tables.colorDataTable);
    const npcHeroDataTable = useAppSelector(state => state.tables.characterDataTable).filter((element)=> element.NPCType === ENpcType.None);
    const npcHeroSkinDataTable = useAppSelector(state => state.tables.skinDataTable);

    const removeParentheses = useCallback((text: string) => {
        return text.replaceAll('(', "").replaceAll(')', "");
    }, []);

    const transColorCode = useCallback((colrValStr: string): RGBAColorType => {
        const {R, G, B, A} = Object.fromEntries(colrValStr.split(",").map(item => item.split("=")));

        const r = R >= 1.0 ? 255 : (R <= 0.0 ? 0 : Math.floor(R * 256.0)) ?? 0;
        const g = G >= 1.0 ? 255 : (G <= 0.0 ? 0 : Math.floor(G * 256.0)) ?? 0;
        const b = B >= 1.0 ? 255 : (B <= 0.0 ? 0 : Math.floor(B * 256.0)) ?? 0;
        const a =  A >= 1.0 ? 1 : parseFloat(A);

        return {r, g, b,  a};
    }, []);

    const contents = useCallback(() : ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];
        if(npcHeroDataTable && 0 < npcHeroDataTable.length){
            if (datas && 0 < datas.length) {
    
                for (let i = 0; i < npcHeroDataTable.length; i++) {
                    const data = datas.find((element)=> element?.HeroID === npcHeroDataTable[i]?.ID);
                    if(data){
                        const bodySkinItem = npcHeroSkinDataTable.find((element)=> element?.ID === data?.BodySkinID);
                        const hairSkinItem = npcHeroSkinDataTable.find((element)=> element?.ID === data?.HairSkinID);
                        const headItem = itemTable.find((element)=> element?.ID === data?.HeadID);
                        const faceItem = itemTable.find((element)=> element?.ID === data?.FaceID);
                        const backItem = itemTable.find((element)=> element?.ID === data?.BackID);
                        const weaponItem = itemTable.find((element)=> element?.ID === data?.WeaponID);
                        const pelvisItem = itemTable.find((element) => element?.ID === data?.PelvisID);
                        const winPoseItem = itemTable.find((element)=> element?.ID === data?.WinPoseID);

                        // const ltEyeColorInfo = colorDataTable.find((element) => element?.ID === data?.LeftEyeColorID);
                        // const rtEyeColorInfo = colorDataTable.find((element) => element?.ID === data?.RightEyeColorID);
                        // let ltEyeColor = null;
                        // let rtEyeColor = null;
                        // if(ltEyeColorInfo){
                        //     ltEyeColor = transColorCode(removeParentheses(ltEyeColorInfo?.Color ?? ""));
                        // }
                        // if(rtEyeColorInfo){
                        //     rtEyeColor = transColorCode(removeParentheses(rtEyeColorInfo?.Color ?? ""));
                        // }

                        list.push(
                            <BorderedTableRow key={i}>
                                <TableCell component="th" scope="row">{npcHeroDataTable[i]?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                                <TableCell>{data?.BattleLevel ? data?.BattleLevel.toLocaleString().toString().replaceAll(/(?<=.{1})./g, '*') : data?.BattleLevel}</TableCell>
                                <TableCell>{data?.BattleExp ? data?.BattleExp.toLocaleString().toString().replaceAll(/(?<=.{1})./g, '*') : data?.BattleExp}</TableCell>
                                <TableCell>{data?.RewardedLevel ? data?.RewardedLevel.toLocaleString().toString().replaceAll(/(?<=.{1})./g, '*') : data?.RewardedLevel}</TableCell>
                                <TableCell>
                                    <Box sx={{ display: 'flex', alignItems: 'center'}}>
                                        <ColorBox hexColor={data?.LeftEyeHexColor} />
                                        {data?.LeftEyeHexColor.toString().replaceAll(/(?<=.{2})./g, '*')}
                                    </Box>
                                </TableCell>
                                <TableCell>
                                    <Box sx={{ display: 'flex', alignItems: 'center'}}>
                                        <ColorBox hexColor={data?.RightEyeHexColor} />
                                        {data?.RightEyeHexColor.toString().replaceAll(/(?<=.{2})./g, '*')}
                                    </Box>
                                </TableCell>
                                <TableCell>{hairSkinItem?.NameStringWithID ? hairSkinItem?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.HairSkinID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                                <TableCell>{bodySkinItem?.NameStringWithID ? bodySkinItem?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.BodySkinID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                                <TableCell>{headItem?.NameStringWithID ? headItem?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.HeadID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                                <TableCell>{data?.HeadOffset.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                                <TableCell>{data?.HeadRotate.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                                <TableCell>{faceItem?.NameStringWithID ? faceItem?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : data?.FaceID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                                <TableCell>{data?.FaceOffset.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                                <TableCell>{data?.FaceRotate.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                                <TableCell>{backItem?.NameStringWithID ? backItem?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : data?.BackID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                                <TableCell>{data?.BackOffset.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                                <TableCell>{data?.BackRotate.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                                <TableCell>{pelvisItem?.NameStringWithID ? pelvisItem?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : data?.PelvisID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                                <TableCell>{data?.PelvisOffset.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                                <TableCell>{data?.PelvisRotate.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                                <TableCell>{weaponItem?.NameStringWithID ? weaponItem?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : data?.WeaponID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                                <TableCell>{winPoseItem?.NameStringWithID ? winPoseItem?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : data?.WinPoseID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                                <TableCell>{data?.AddPresetCount.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                                <TableCell>{data?.ExpireAt ? dayjs.unix(data?.ExpireAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                                <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                                <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                            </BorderedTableRow>
                        )
                    }
                    else{
                        list.push(
                            <BorderedTableRow key={i}>
                                <TableCell component="th" scope="row">{npcHeroDataTable[i].NameStringWithID}</TableCell>
                                <TableCell colSpan={12} sx={{ borderRight: '0'}}>보유하지 않은 캐릭터 입니다.</TableCell>
                                <TableCell colSpan={13} sx={{ borderLeft: '0'}}>보유하지 않은 캐릭터 입니다.</TableCell>
                            </BorderedTableRow>
                        )
                    }
                }
    
                result = (
                    <>
                        <TableHead>
                            <BorderedTableRow>
                                <MinWidthTableCell>영웅 **</MinWidthTableCell>
                                <MinWidthTableCell>전투 **</MinWidthTableCell>
                                <MinWidthTableCell>전투 ***</MinWidthTableCell>
                                <MinWidthTableCell>숙련도 ** **</MinWidthTableCell>
                                <MinWidthTableCell>왼쪽 * **</MinWidthTableCell>
                                <MinWidthTableCell>오른* * **</MinWidthTableCell>
                                <MinWidthTableCell>머리 ** **</MinWidthTableCell>
                                <MinWidthTableCell>몸통 ** **</MinWidthTableCell>
                                <MinWidthTableCell>머리** **</MinWidthTableCell>
                                <MinWidthTableCell>머리** **</MinWidthTableCell>
                                <MinWidthTableCell>머리** ***</MinWidthTableCell>
                                <MinWidthTableCell>얼굴** **</MinWidthTableCell>
                                <MinWidthTableCell>얼굴** **</MinWidthTableCell>
                                <MinWidthTableCell>얼굴** ***</MinWidthTableCell>
                                <MinWidthTableCell>등 ** **</MinWidthTableCell>
                                <MinWidthTableCell>등 ** **</MinWidthTableCell>
                                <MinWidthTableCell>등 ** ***</MinWidthTableCell>
                                <MinWidthTableCell>꼬리 ** **</MinWidthTableCell>
                                <MinWidthTableCell>꼬리 ** **</MinWidthTableCell>
                                <MinWidthTableCell>꼬리 ** ***</MinWidthTableCell>
                                <MinWidthTableCell>디피 ** **</MinWidthTableCell>
                                <MinWidthTableCell>승리 ***</MinWidthTableCell>
                                <MinWidthTableCell>프리* ****</MinWidthTableCell>
                                <MinWidthTableCell>체험* ****({timezoneName})</MinWidthTableCell>
                                <MinWidthTableCell>생성일시({timezoneName})</MinWidthTableCell>
                                <MinWidthTableCell>수정일시({timezoneName})</MinWidthTableCell>
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
                            <TableCell className={commonUIStyles.noneCell}>검색된 영웅 정보가 없습니다.</TableCell>
                        </BorderedTableRow>
                    </TableBody>
                );
            }
        }

    return result;
    }, [itemTable, npcHeroDataTable, npcHeroSkinDataTable, datas]);

    return (<>{contents()}</>);
}

export default HeroContents;