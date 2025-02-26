import { useAppSelector } from '@hooks/index';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import { PaginatedList } from '@helpers/paging';
import { TableContainer, Paper, styled, Toolbar, Typography } from '@mui/material';
import { Defines } from '@ngel/data/autoDefines';
import { ReactElement, useCallback, useState, useEffect, useRef } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import dynamic from 'next/dynamic';
import { EItemType } from '@ngel/data/models/lobby';
import { DataTableModels } from '@ngel/data/tables/model';
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


export interface ContentsProps {
    datas : PaginatedList<Models.Inventory>;
    onTabSearch: (value: number, categoryValue: number) => void;
    onChangeListCount: ( event: SelectChangeEvent<unknown> ) => void;
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

const CategoryTypes = Object.keys(EItemType).filter(key => isNaN(Number(key)));
const categoryTranslate = (categoryNum : string) => {
    if(isNaN(Number(categoryNum))) return "";
    return CategoryTypes[categoryNum];
}

const InventoryContents = ({ datas, onTabSearch, onChangeListCount }: ContentsProps): ReactElement => {
    
    let content: ReactElement = <></>;
    const tables = useAppSelector(state => state.tables);
    const [items, setItems] = useState<DataTableModels.Item[]>([]);
    const [categories, setCategories] = useState<CategoryInfo[]>([]);

    const selectItem = useRef<DataTableModels.Item | null>(null);
    const selectCategory = useRef<CategoryInfo | null>(null);

    useEffect(() => {
        const itemOptions: DataTableModels.Item[] = [];
        for (let i = 0; i < tables.itemTable.length; i++) {
            const item = new DataTableModels.Item(tables.itemTable[i]);
                
            itemOptions.push(item);
        }

        let categoriesOptions: CategoryInfo[] = [];
        
        for(let j = 0; j < CategoryTypes.length; j++){
            if( j == EItemType.None ) continue;
            
            let categoryString = categoryTranslate(j.toString());
            
            const category = new CategoryInfo( categoryString, `${categoryString} (${j})`, j);
            
            categoriesOptions.push(category);
        }
        
        setItems(itemOptions);
        setCategories(categoriesOptions);
    }, [ tables, setItems, setCategories ]);

    const onSearch = useCallback(()=> {
        onTabSearch(selectItem.current?.ID ?? 0, selectCategory.current?.ID ?? 0);
    }, [selectItem, selectCategory,  onTabSearch]);

    const onChangeItem = useCallback((e, v: DataTableModels.Item) => {
        if(!v) {
            selectItem.current = null;
            return;
        };

        selectItem.current = v;
    }, [selectItem ]);

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
                for(let i = 0 ; i < datas.items.length; i++){
                    const inventory = datas.items[i];
                    
                    let invenType = "";
                    if(inventory?.ItemType) invenType = `${categoryTranslate(inventory?.ItemType?.toString() ?? "")} (${inventory?.ItemType ?? ""})`;

                    list.push(
                        <BorderedTableRow key={i}>
                            <TableCell component="th" scope="row">{inventory?.ItemNameStringWithID ? inventory?.ItemNameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : inventory?.ItemID.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                            <TableCell>{invenType.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                            <TableCell>{inventory?.Count ? inventory?.Count.toLocaleString().toString().replaceAll(/(?<=.{2})./g, '*') : 0}</TableCell>
                            <TableCell>{inventory?.CreateAt ? dayjs(inventory?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                            <TableCell>{inventory?.UpdateAt ? dayjs(inventory?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        </BorderedTableRow>
                    )
                }
        
                result = (
                    <>
                        <TableHead>
                            <BorderedTableRow>
                                <TableCell>아이** *******</TableCell>
                                <TableCell>아이* **</TableCell>
                                <TableCell>수량</TableCell>
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
                <>
                    <TableBody>
                        <BorderedTableRow>
                            <TableCell colSpan={3} className={commonUIStyles.noneCell}>검색된 인벤** **가 없습니다.</TableCell>
                        </BorderedTableRow>
                    </TableBody>
                </>
            )
        }
        return result;
    }, [datas]);

    content = (
        <>
            <Box sx={ { display: { xs: 'block', lg : 'flex'}, alignItems: { lg: 'center'}, justifyContent: { lg: 'space-between'}, marginBottom: 1}}>
                <ListCountSelector pageLogs={datas.pageSize} handleChange={onChangeListCount}/>
                <Box display='flex' alignItems='center' marginTop={1} sx={{ justifyContent: { xs: 'start', sm: 'end'}, maxWidth: '100%'}}>
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size='small' sx={{ mr: 1 }} onClick={onSearch}>검색</Button>
                    <FormControl>
                        <Autocomplete options={categories} size='small' sx={{ width: {xs: 150, sm : 175, md: 200}, maxWidth: '100%', marginRight: 1}}
                        getOptionLabel={(option) => (option as CategoryInfo).nameStringID.toString().replaceAll(/(?<=.{1})./g, '*')} 
                        renderInput={(params) => <TextField {...params} label="타입분류" />}
                        onChange={(e, v)=> onChangeCategory(e, v as CategoryInfo)} />

                    </FormControl>
                    <FormControl>
                        <Autocomplete options={items} size='small' sx={{ width: {xs: 260, sm : 300, md: 350}, maxWidth: '100%'}}
                        getOptionLabel={(option) => (option as DataTableModels.Item).NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*')} 
                        renderInput={(params) => <TextField {...params} label="아이템명" />}   
                        onChange={(e, v)=> onChangeItem(e, v as DataTableModels.Item)}
                        />
                    </FormControl>
                </Box>
            </Box>
            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 700 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6'>인벤토리</Typography>
                </Toolbar>
                <Table stickyHeader aria-label="sticky table">
                    { contents()}
                </Table>
            </TableContainer>
        </>
    );
    
    return content;
}

export default InventoryContents;