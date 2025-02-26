import { useAppSelector } from '@hooks/index';
import { ReactElement, useEffect, useState, useRef, useCallback } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import bignumber from 'bignumber.js';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import dynamic from 'next/dynamic';
import { Defines } from '@ngel/data/autoDefines';
import { DataTableModels } from '@ngel/data/tables/model';
import { TableContainer, Paper, styled, Toolbar, Typography } from '@mui/material';
import { SelectChangeEvent } from '@mui/material/Select';
import ListCountSelector from './ListCountSelector';
import TextField from '@mui/material/TextField';

const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const Autocomplete = dynamic(() => import('@mui/material/Autocomplete'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });

const MinWidthTableCell = styled(TableCell)(({ theme }) => ({
    minWidth: "120px"
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
    datas: PaginatedList<Models.HeroSkin>;
    onTabSearch: (value: number) => void;
    onChangeListCount: ( event: SelectChangeEvent<unknown> ) => void;
}

const HeroSkinContents = ({ datas, onTabSearch, onChangeListCount }: ContentsProps): ReactElement => {

    let content: ReactElement = <></>;
    const tables = useAppSelector(state => state.tables);
    const [skins, setSkins] = useState<DataTableModels.SkinData[]>([]);
    const selectSkin = useRef<DataTableModels.SkinData | null>(null);
    const colorDataTable = useAppSelector(state => state.tables.colorDataTable);
    const heroSkinDataTable = useAppSelector(state => state.tables.skinDataTable);

    useEffect(() => {
        const skinOptions: DataTableModels.SkinData[] = [];
        for (let i = 0; i < tables.skinDataTable.length; i++) {
            const item = new DataTableModels.SkinData(tables.skinDataTable[i]);
                
            skinOptions.push(item);
        }

        setSkins(skinOptions);

    }, [ tables, setSkins ]);

    const onSearch = useCallback(()=> {
        if(selectSkin.current){
            onTabSearch(selectSkin.current.ID);
        }
        else{
            onTabSearch(0);
        }
    }, [selectSkin, onTabSearch]);

    const onChangeItem = useCallback((e, v: DataTableModels.SkinData) => {
        if(!v) {
            selectSkin.current = null;
            return;
        };

        selectSkin.current = v;
    }, [selectSkin ]);

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

        if(datas && 0 < datas?.items.length){
            for (let i = 0; i < datas?.items.length; i++) {
                const data = datas.items[i];
                const SkinInfo = heroSkinDataTable.find((element) => element?.ID === data?.SkinID);
                // const ColorInfo1 = colorDataTable.find((element) => element?.ID === data.ColorID1);
                // const ColorInfo2 = colorDataTable.find((element) => element?.ID === data.ColorID2);
                // const ColorInfo3 = colorDataTable.find((element) => element?.ID === data.ColorID3);
                // const ColorInfo4 = colorDataTable.find((element) => element?.ID === data.ColorID4);

                // let color1 = null;
                // let color2 = null;
                // let color3 = null;
                // let color4 = null;
                // if(ColorInfo1){
                //     color1 = transColorCode(removeParentheses(ColorInfo1?.Color ?? ""));
                // }
                // if(ColorInfo2){
                //     color2 = transColorCode(removeParentheses(ColorInfo2?.Color ?? ""));
                // }
                // if(ColorInfo3){
                //     color3 = transColorCode(removeParentheses(ColorInfo3?.Color ?? ""));
                // }
                // if(ColorInfo4){
                //     color4 = transColorCode(removeParentheses(ColorInfo4?.Color ?? ""));
                // }
                
                if(data){
                    list.push(
                        <BorderedTableRow key={i}>
                            <TableCell component="th" scope="row">{SkinInfo?.NameStringWithID ? SkinInfo?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : data.SkinID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                            <TableCell>
                                <Box sx={{ display: 'flex', alignItems: 'center'}}>
                                    <ColorBox hexColor={data?.HexColor1} />
                                    {data?.HexColor1.toString().replaceAll(/(?<=.{1})./g, '*')}
                                </Box>
                            </TableCell>
                            <TableCell>
                                <Box sx={{ display: 'flex', alignItems: 'center'}}>
                                    <ColorBox hexColor={data?.HexColor2} />
                                    {data?.HexColor2.toString().replaceAll(/(?<=.{1})./g, '*')}
                                </Box>
                            </TableCell>
                            <TableCell>
                                <Box sx={{ display: 'flex', alignItems: 'center'}}>
                                    <ColorBox hexColor={data?.HexColor3} />
                                    {data?.HexColor3.toString().replaceAll(/(?<=.{1})./g, '*')}
                                </Box>
                            </TableCell>
                            <TableCell>
                                <Box sx={{ display: 'flex', alignItems: 'center'}}>
                                    <ColorBox hexColor={data?.HexColor4} />
                                    {data?.HexColor4.toString().replaceAll(/(?<=.{1})./g, '*')}
                                </Box>
                            </TableCell>
                            <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                            <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        </BorderedTableRow>
                    )
                }
                
            }
            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <MinWidthTableCell>영웅 ** **</MinWidthTableCell>
                            <MinWidthTableCell>Co****</MinWidthTableCell>
                            <MinWidthTableCell>Co****</MinWidthTableCell>
                            <MinWidthTableCell>Co****</MinWidthTableCell>
                            <MinWidthTableCell>Co****</MinWidthTableCell>
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
                <>
                    <TableBody>
                        <BorderedTableRow>
                            <TableCell className={commonUIStyles.noneCell}>검색된 영웅** **가 없습니다.</TableCell>
                        </BorderedTableRow>
                    </TableBody>
                </>
            );
        }

        return result;
    },[heroSkinDataTable, datas]);

    content = (
        <>
            <Box sx={ { display: { xs: 'block', lg : 'flex'}, alignItems: { lg: 'center'}, justifyContent: { lg: 'space-between'}, mb: 1}}>
                <ListCountSelector pageLogs={datas.pageSize} handleChange={onChangeListCount}/>
                <Box display='flex' alignItems='center' marginTop={1} sx={{ justifyContent: { xs: 'start', sm: 'end'}, maxWidth: '100%' }}>
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size='small' sx={{ mr: 1 }} onClick={onSearch}>검색</Button>
                    <FormControl>
                        <Autocomplete options={skins} size='small' sx={{ width: {xs: 260, sm : 300, md: 350}, maxWidth: '100%'}}
                        getOptionLabel={(option) => (option as DataTableModels.SkinData)?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')} 
                        renderInput={(params) => <TextField {...params} label="아이템명" />}   
                        onChange={(e, v)=> onChangeItem(e, v as DataTableModels.SkinData)}
                        />
                    </FormControl>
                </Box>
            </Box>
            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 700 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6'>영웅 **</Typography>
                </Toolbar>
                <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                {contents()}
                </Table>
            </TableContainer>
        </>
    );
    
    return content;
}

export default HeroSkinContents;