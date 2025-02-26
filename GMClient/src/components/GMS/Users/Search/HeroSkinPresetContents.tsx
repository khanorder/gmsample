import { useAppSelector } from "@hooks/index";
import { ReactElement, useCallback } from "react";
import dynamic from "next/dynamic";
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { Models } from "@ngel/data/models";
import { styled } from '@mui/material';
import { ENpcType } from '@ngel/data/models/lobby';

const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });

interface ContentsProps {
    datas: Models.HeroSkinPreset[];
}

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

interface PresetData { 
    PresetName: string;
    HeadID: number;
    FaceID: number;
    LeftEyeHexColor: string;
    RightEyeHexColor: string;
    HairSkinID: number;
    HairHexColor1: string;
    HairHexColor2: string;
    BodySkinID: number;
    BodyHexColor1: string;
    BodyHexColor2: string;
    BodyHexColor3: string;
    BodyHexColor4: string;  
    BackID: number;  
    PelvisID: number;
    WeaponID: number;
    WinPoseID: number;
}
const HeroSkinPresetContents = ({ datas } : ContentsProps) : ReactElement => {
    const tables = useAppSelector(state => state.tables);
    const itemTable = tables.itemTable;
    const colorDataTable = useAppSelector(state => state.tables.colorDataTable);
    const heroDataTable = tables.characterDataTable.filter((element)=> element.NPCType === ENpcType.None);
    let content: ReactElement = <></>;

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

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];

        for(let i = 0; i < datas.length; i++){
            const data = datas[i];
            const hero = heroDataTable.find((element)=> element?.ID === data?.HeroID);
            const parsed: PresetData = JSON.parse(data?.PresetData);
            const headItem = itemTable.find((element)=> element?.ID === parsed?.HeadID);
            const hairSkinItem = itemTable.find((element)=> element?.ID === parsed?.HairSkinID);
            const faceItem = itemTable.find((element)=> element?.ID === parsed?.FaceID);
            const bodyItem = itemTable.find((element)=> element?.ID === parsed?.BodySkinID);
            const backItem = itemTable.find((element)=> element?.ID === parsed?.BackID);
            const weaponItem = itemTable.find((element)=> element?.ID === parsed?.WeaponID);
            const pelvisItem = itemTable.find((element) => element?.ID === parsed?.PelvisID);
            const winPoseItem = itemTable.find((element)=> element?.ID === parsed?.WinPoseID);

            // const hairColorInfo1 = colorDataTable.find((element) => element?.ID === parsed?.HairColorID1);
            // const hairColorInfo2 = colorDataTable.find((element) => element?.ID === parsed?.HairColorID2);
            // let hairColor1 = hairColorInfo1 ? transColorCode(removeParentheses(hairColorInfo1?.Color ?? "")) : null;
            // let hairColor2 = hairColorInfo2 ? transColorCode(removeParentheses(hairColorInfo2?.Color ?? "")) : null;
 
            // const ltEyeColorInfo = colorDataTable.find((element) => element?.ID === parsed?.LeftEyeColorID);
            // const rtEyeColorInfo = colorDataTable.find((element) => element?.ID === parsed?.RightEyeColorID);
            // let ltEyeColor = ltEyeColorInfo ? transColorCode(removeParentheses(ltEyeColorInfo?.Color ?? "")) : null;
            // let rtEyeColor = rtEyeColorInfo ? transColorCode(removeParentheses(rtEyeColorInfo?.Color ?? "")) : null;

            // const bodyColorInfo1 = colorDataTable.find((element) => element?.ID === parsed?.BodyColorID1);
            // const bodyColorInfo2 = colorDataTable.find((element) => element?.ID === parsed?.BodyColorID2);
            // const bodyColorInfo3 = colorDataTable.find((element) => element?.ID === parsed?.BodyColorID3);
            // const bodyColorInfo4 = colorDataTable.find((element) => element?.ID === parsed?.BodyColorID4);
            // let bodyColor1 = bodyColorInfo1 ? transColorCode(removeParentheses(bodyColorInfo1?.Color ?? "")) : null;
            // let bodyColor2 = bodyColorInfo2 ? transColorCode(removeParentheses(bodyColorInfo2?.Color ?? "")) : null;
            // let bodyColor3 = bodyColorInfo3 ? transColorCode(removeParentheses(bodyColorInfo3?.Color ?? "")) : null;
            // let bodyColor4 = bodyColorInfo4 ? transColorCode(removeParentheses(bodyColorInfo4?.Color ?? "")) : null;

            list.push(
                <BorderedTableRow key={i}>
                    <TableCell>{parsed?.PresetName ? parsed?.PresetName.toString().replaceAll(/(?<=.{1})./g, '*') : ""}</TableCell>
                    <TableCell>{hero?.NameStringWithID ? hero?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : data?.HeroID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                    <TableCell>{data?.SlotID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                    <TableCell>{headItem?.NameStringWithID ? headItem?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : parsed?.HeadID ? parsed?.HeadID.toString().replaceAll(/(?<=.{1})./g, '*') : ""}</TableCell>
                    <TableCell>{hairSkinItem?.NameStringWithID ? hairSkinItem?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : parsed?.HairSkinID ? parsed?.HairSkinID.toString().replaceAll(/(?<=.{1})./g, '*') : ""}</TableCell>
                    <TableCell>
                        <Box sx={{ display: 'flex', alignItems: 'center'}}>
                            <ColorBox hexColor={parsed?.HairHexColor1} />
                            {parsed?.HairHexColor1.toString().replaceAll(/(?<=.{1})./g, '*')}
                        </Box>
                    </TableCell>
                    <TableCell>
                        <Box sx={{ display: 'flex', alignItems: 'center'}}>
                            <ColorBox hexColor={parsed?.HairHexColor2} />
                            {parsed?.HairHexColor2.toString().replaceAll(/(?<=.{1})./g, '*')}
                        </Box>
                    </TableCell>
                    <TableCell>{faceItem?.NameStringWithID ? faceItem?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : parsed?.FaceID ? parsed?.FaceID.toString().replaceAll(/(?<=.{1})./g, '*') : ""}</TableCell>
                    <TableCell>
                        <Box sx={{ display: 'flex', alignItems: 'center'}}>
                            <ColorBox hexColor={parsed?.LeftEyeHexColor} />
                            {parsed?.LeftEyeHexColor.toString().replaceAll(/(?<=.{1})./g, '*')}
                        </Box>
                    </TableCell>
                    <TableCell>
                        <Box sx={{ display: 'flex', alignItems: 'center'}}>
                            <ColorBox hexColor={parsed?.RightEyeHexColor} />
                            {parsed?.RightEyeHexColor.toString().replaceAll(/(?<=.{1})./g, '*')}
                        </Box>
                    </TableCell>
                    <TableCell>{bodyItem?.NameStringWithID ? bodyItem?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : parsed?.BodySkinID ? parsed?.BodySkinID.toString().replaceAll(/(?<=.{1})./g, '*') : ""}</TableCell>
                    <TableCell>
                        <Box sx={{ display: 'flex', alignItems: 'center'}}>
                            <ColorBox hexColor={parsed?.BodyHexColor1} />
                            {parsed?.BodyHexColor1.toString().replaceAll(/(?<=.{1})./g, '*')}
                        </Box>
                    </TableCell>
                    <TableCell>
                        <Box sx={{ display: 'flex', alignItems: 'center'}}>
                            <ColorBox hexColor={parsed?.BodyHexColor2} />
                            {parsed?.BodyHexColor2.toString().replaceAll(/(?<=.{1})./g, '*')}
                        </Box>
                    </TableCell>
                    <TableCell>
                        <Box sx={{ display: 'flex', alignItems: 'center'}}>
                            <ColorBox hexColor={parsed?.BodyHexColor3} />
                            {parsed?.BodyHexColor3.toString().replaceAll(/(?<=.{1})./g, '*')}
                        </Box>
                    </TableCell>
                    <TableCell>
                        <Box sx={{ display: 'flex', alignItems: 'center'}}>
                            <ColorBox hexColor={parsed?.BodyHexColor4} />
                            {parsed?.BodyHexColor4.toString().replaceAll(/(?<=.{1})./g, '*')}
                        </Box>
                    </TableCell>
                    <TableCell>{backItem?.NameStringWithID ? backItem?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : parsed?.BackID ? parsed?.BackID.toString().replaceAll(/(?<=.{1})./g, '*') : ""}</TableCell>
                    <TableCell>{weaponItem?.NameStringWithID ? weaponItem?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : parsed?.WeaponID ? parsed?.WeaponID.toString().replaceAll(/(?<=.{1})./g, '*') : ""}</TableCell>
                    <TableCell>{pelvisItem?.NameStringWithID ? pelvisItem?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : parsed?.PelvisID ? parsed?.PelvisID.toString().replaceAll(/(?<=.{1})./g, '*') : ""}</TableCell>
                    <TableCell>{winPoseItem?.NameStringWithID ? winPoseItem?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : parsed?.WinPoseID ? parsed?.WinPoseID.toString().replaceAll(/(?<=.{1})./g, '*') : ""}</TableCell>
                    <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                </BorderedTableRow>
            )
        }
        result = (<>{list}</>);
        return result;
    },[itemTable, heroDataTable, datas]);

   if(datas && 0 < datas.length){
        content = (
            <>
                <TableHead>
                    <BorderedTableRow>
                        <TableCell>프리* **</TableCell>
                        <TableCell>영웅**</TableCell>
                        <TableCell>슬롯**</TableCell>
                        <TableCell>머리** **</TableCell>
                        <TableCell>머리 ** **</TableCell>
                        <TableCell>머리 ***</TableCell>
                        <TableCell>머리 ***</TableCell>
                        <TableCell>얼굴** **</TableCell>
                        <TableCell>왼쪽 * **</TableCell>
                        <TableCell>오른* * **</TableCell>
                        <TableCell>몸통 ** **</TableCell>
                        <TableCell>몸통 ***</TableCell>
                        <TableCell>몸통 ***</TableCell>
                        <TableCell>몸통 ***</TableCell>
                        <TableCell>몸통 ***</TableCell>
                        <TableCell>등 ** **</TableCell>
                        <TableCell>디피 ** **</TableCell>
                        <TableCell>꼬리 ** **</TableCell>
                        <TableCell>승리 **</TableCell>
                        <TableCell>생성시간({timezoneName})</TableCell>
                        <TableCell>수정시간({timezoneName})</TableCell>
                    </BorderedTableRow>
                </TableHead>
                <TableBody>
                    {contents()}
                </TableBody>
            </>
        )
    }
    else{
        content = (
            <TableBody>
                <BorderedTableRow>
                    <TableCell className={commonUIStyles.noneCell}>검색된 스킨 *** **가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }

    return content;
} 

export default HeroSkinPresetContents;