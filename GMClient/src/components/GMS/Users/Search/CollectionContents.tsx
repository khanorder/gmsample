import { useAppSelector } from '@hooks/index';
import { ReactElement, useState, useEffect, useRef, useCallback } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer, styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { ECollectionType } from '@ngel/data/models/lobby';
import { SelectChangeEvent } from '@mui/material/Select';

const ListCountSelector = dynamic(() => import('./ListCountSelector'), { ssr: false });
const TextField = dynamic(() => import('@mui/material/TextField'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const Autocomplete = dynamic(() => import('@mui/material/Autocomplete'), { ssr: false });

const MinWidthTableCell = styled(TableCell)(({ theme }) => ({
    minWidth: "120px"
}));

interface ContentsProps {
    datas: PaginatedList<Models.Collection>;
    onTabSearch: (value: number, categoryValue: number) => void;
    onChangeListCount: ( event: SelectChangeEvent<unknown> ) => void;
}

class ItemInfo {
    ID: number;
    reqID: number;
    NameStringID: string;
    constructor (id?: number, reqID?: number, nameid?: string) {
        this.ID = id ?? 0;
        this.reqID = reqID ?? 0;
        this.NameStringID = nameid ?? "";
    }
}

class CategoryInfo {
    name: string;
    nameStringID: string;
    ID: number;

    constructor (name?: string, nameStringID?: string, id?: number) {
        this.name = name ?? "";
        this.nameStringID = nameStringID ?? "";
        this.ID = id ?? 0;
    }
}

const CollectionContents = ({ datas, onTabSearch, onChangeListCount }: ContentsProps): ReactElement => {

    let content: ReactElement = <></>;
    const tables = useAppSelector(state => state.tables);
    const collectionDataTable = tables.collectionDataTable;
    const collectionGroupDataTable = tables.collectionGroupDataTable;
    const [items, setItems] = useState<ItemInfo[]>([]);
    const [categories, setCategories] = useState<CategoryInfo[]>([]);

    const itemTable = tables.itemTable;
    const monsterTable = tables.characterDataTable;
    const petTable = tables.petDataTable;
    const selectItem = useRef<ItemInfo | null>(null);
    const selectCategory = useRef<CategoryInfo | null>(null);

    useEffect(() => {
        let itemOptions: ItemInfo[] = [];
        for (let i = 0; i < collectionDataTable.length; i++) {
            const collectionData = collectionDataTable[i];
            let dataInfo;
            switch(collectionData?.Type){
                case ECollectionType.Accessory:
                case ECollectionType.Costume:
                case ECollectionType.Weapon:
                case ECollectionType.Vehicle:
                    dataInfo = itemTable.find((element)=> element?.ID === collectionData?.ReqID); break;
                case ECollectionType.Monster:
                    dataInfo = monsterTable.find((element)=> element?.ID === collectionData?.ReqID); break;
                case ECollectionType.Pet:
                    dataInfo = petTable.find((element)=> element?.ID === collectionData?.ReqID); break;
            }
            const item = new ItemInfo( collectionData?.ID ?? 0, collectionData?.ReqID ?? 0, dataInfo?.NameStringWithID ?? "",);
            
            itemOptions.push(item);
        }

        let categoriesOptions: CategoryInfo[] = [];

        for(let j = 0; j < collectionGroupDataTable.length; j++){
            let categoryString = collectionGroupDataTable[j];

            const category = new CategoryInfo( categoryString?.CategoryNameString, categoryString?.CategoryNameStringWithID, categoryString?.ID);
            
            categoriesOptions.push(category);
        }

        setItems(itemOptions);
        setCategories(categoriesOptions);
    }, [ tables, setItems, setCategories, collectionDataTable, collectionGroupDataTable, itemTable, monsterTable, petTable ]);

    const onSearch = useCallback(()=> {
        onTabSearch(selectItem.current?.ID ?? 0, selectCategory.current?.ID ?? 0);
    }, [selectItem, selectCategory,  onTabSearch]);

    const onChangeItem = useCallback((e, v: ItemInfo) => {
        if(!v) {
            selectItem.current = null;
            return;
        };

        selectItem.current = v;
    }, [selectItem]);

    const onChangeCategory = useCallback((e, v: CategoryInfo) => {
        if(!v) {
            selectCategory.current = null;
            return;
        };

        selectCategory.current = v;
    }, [selectCategory]);

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        if (datas && 0 < datas?.items.length) {
            const list: ReactElement[] = [];

            for (let i = 0; i < datas.items.length; i++) {
                const data = datas.items[i];
                const reqID = collectionDataTable.find((element)=> element?.ID === data?.CollectionID)?.ReqID;
                const type = collectionGroupDataTable.find(element => element.ID === data?.CollectionType)?.CategoryNameStringWithID ?? "";
                let item = null;
                if ([ECollectionType.None, ECollectionType.Accessory, ECollectionType.Costume, ECollectionType.Weapon, ECollectionType.Vehicle].includes(data.CollectionType)) {
                    item = itemTable.find((element)=> element?.ID === reqID);
                } else if(ECollectionType.Monster === data?.CollectionType){
                    item = monsterTable.find((element)=> element.ID === reqID);
                } else if(ECollectionType.Pet === data?.CollectionType){
                    item = petTable.find((element)=> element.ID === reqID);
                }

                list.push(
                    <BorderedTableRow key={i}>
                        <TableCell>{type}</TableCell>
                        <TableCell>{item?.NameStringWithID ? item?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.CollectionID}</TableCell>
                        <TableCell sx={{color: data?.IsRewarded ? 'blue' : 'red'}}>{data?.IsRewarded ? "수령" : "미수령"}</TableCell>
                        <TableCell>{dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss')}</TableCell>
                        <TableCell>{dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss')}</TableCell>
                    </BorderedTableRow>
                )
            }

            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <MinWidthTableCell>도감 타입</MinWidthTableCell>
                            <MinWidthTableCell>도감 ID</MinWidthTableCell>
                            <MinWidthTableCell>보상 수령</MinWidthTableCell>
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
                <>
                    <TableBody>
                        <BorderedTableRow>
                            <TableCell className={commonUIStyles.noneCell}>검색된 도감 정보가 없습니다.</TableCell>
                        </BorderedTableRow>
                    </TableBody>
                </>
            );
        }

        return result;
    }, [datas, itemTable, collectionDataTable, collectionGroupDataTable, monsterTable, petTable]);

    content = (
        <>
            <Box sx={ { display: { xs: 'block', lg : 'flex'}, alignItems: { lg: 'center'}, justifyContent: { lg: 'space-between'}, mb: 1 }}>
                <ListCountSelector pageLogs={datas.pageSize} handleChange={onChangeListCount}/>
                <Box display='flex' alignItems='center' marginTop={1} sx={{ justifyContent: { xs: 'start', sm: 'end'}, maxWidth: '100%' }}>
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size='small' sx={{ mr: 1 }} onClick={onSearch}>검색</Button>
                    <FormControl>
                        <Autocomplete options={categories} size='small' sx={{ width: {xs: 150, sm : 175, md: 200}, maxWidth: '100%' }}
                        getOptionLabel={(option) => (option as CategoryInfo).nameStringID.toString().replaceAll(/(?<=.{2})./g, '*')} 
                        renderInput={(params) => <TextField {...params} label="타입분류" />}
                        onChange={(e, v)=> onChangeCategory(e, v as CategoryInfo)} />

                    </FormControl>
                    <FormControl>
                        <Autocomplete options={items} size='small' sx={{ width: {xs: 260, sm : 300, md: 350}, maxWidth: '100%', ml: 1 }}
                        getOptionLabel={(option) => (option as ItemInfo).NameStringID.toString().replaceAll(/(?<=.{2})./g, '*')} 
                        renderInput={(params) => <TextField {...params} label="아이템명" />}   
                        onChange={(e, v)=> onChangeItem(e, v as ItemInfo)} />
                    </FormControl>
                </Box>
            </Box>
            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 700 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6'>도감</Typography>
                </Toolbar>
                <Table stickyHeader aria-label="sticky table">
                    { contents()}
                </Table>
            </TableContainer>
        </>)
    
    return content;

}

export default CollectionContents;