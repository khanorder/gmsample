import { useAppSelector } from '@hooks/index';
import { Models } from '@ngel/data/models';
import dayjs, { Dayjs } from 'dayjs';
import timezone from 'dayjs/plugin/timezone.js';
import utc from 'dayjs/plugin/utc.js';
// dayjs.extend(timezone);
// dayjs.extend(utc);
import isEmpty from 'lodash/isEmpty';
import { TableContainer, Paper, styled, Toolbar, Typography, TextField } from '@mui/material';
import { Defines } from '@ngel/data/autoDefines';
import { PaginatedList } from '@helpers/paging';
import { ReactElement, MutableRefObject, useCallback, useEffect, useState, useRef, ChangeEvent } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import dynamic from 'next/dynamic';
import { Errors } from '@ngel/data/autoErrors';
import { EItemType } from '@ngel/data/models/lobby';
import { DataTableModels } from '@ngel/data/tables/model';
import ListCountSelector from './EditListCountSelector';
import { SelectChangeEvent } from '@mui/material/Select';
import { GameAPI } from '@ngel/data/apis/gameAPI';
import deepmerge from 'deepmerge';

const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const Autocomplete = dynamic(() => import('@mui/material/Autocomplete'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });

export interface InventoryProps {
    userAccount: Models.UserAccount;
    tabIndex: number;
    searchDetail: (index: number, accountID: number) => void;
    datas : PaginatedList<Models.Inventory>;
    onTabSearch: (value: number, categoryValue: number) => void;
    onChangeListCount: ( event: SelectChangeEvent<unknown> ) => void;
    tabContentChanged: MutableRefObject<boolean>;
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

const EditInventory = ({ userAccount, tabIndex, searchDetail, datas, onTabSearch, onChangeListCount, tabContentChanged }: InventoryProps): ReactElement => {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const user = useAppSelector(state => state.user);
    const layouts = useAppSelector(state => state.layouts);
    const tables = useAppSelector(state => state.tables);
    const [items, setItems] = useState<DataTableModels.Item[]>([]);
    const [categories, setCategories] = useState<CategoryInfo[]>([]);
    const selectItem = useRef<DataTableModels.Item | null>(null);
    const selectCategory = useRef<CategoryInfo | null>(null);

    const [deleteDatas, setDeleteDatas] = useState<Models.Inventory[]>([]);
    const [inventories, setInventories] = useState<PaginatedList<Models.Inventory>>(datas);

    const isSigned = userAccount.IsSignIn;

    useEffect(() => {
        var itemOptions: DataTableModels.Item[] = [];
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

    }, [ tables, setItems ]);

    useEffect(() => {
        setInventories(prev => {
            return new PaginatedList(datas.totalItems, datas.page, datas.parameters , datas.pageSize, prev.pageBlockSize);
        })
    }, [datas])
    
    const loadDatas = useCallback(async () => {
        setDeleteDatas(prev => []);

        await searchDetail(tabIndex, userAccount.UID);
        tabContentChanged.current = false;
    }, [userAccount, tabIndex, searchDetail, tabContentChanged]);

    const onReload = useCallback(async () => {
        const changedDatas : Models.Inventory[] = datas.totalItems.filter(_ => _.isChanged);

        if(changedDatas && 0 < changedDatas?.length || 0 < deleteDatas.length){
            if (!confirm("작업중인 내용이 사라집니다.\n정말 새로고침 하시겠습니까?")) {
                return;
            }
        }

        await loadDatas();
        
    },[datas, deleteDatas, loadDatas]);

    const onSave = useCallback(async() => {
        const changedDatas = datas.totalItems.filter(_ => _.isChanged);

        if ((!changedDatas || 1 > changedDatas.length && deleteDatas.length < 1)) {
            alert("저장할 변경된 내용이 없습니다.");
            return;
        }
        else{
            if (!confirm("확인을 누르면 내용이 저장됩니다.\n계속 하시겠습니까?")) {
                return;
            }
        }

        if( deleteDatas && 0 < deleteDatas.length){
            if (!confirm("삭제되는 데이터가 있습니다.\n계속 하시겠습니까?")) {
                return;
            }

            const response = await GameAPI.DeleteInventoriesAsync({ inventories : deleteDatas });
            if (!response) {
                alert(`오류!`);
                return;
            }
    
            if (!response.result) {
                alert(`오류! (error: ${Errors[response.error]}, index: ${response.errorIndex})`);
                return;
            }
        }

        if (changedDatas && 0 < changedDatas.length) {
            const response = await GameAPI.SaveInventoriesAsync({ inventories : changedDatas });
            if (!response) {
                alert(`오류!`);
                return;
            }
    
            if (!response.result) {
                alert(`오류! (error: ${Errors[response.error]}, index: ${response.errorIndex})`);
                return;
            }
        }

        alert(`저장 했습니다.`);
        await loadDatas();
    }, [loadDatas, datas, deleteDatas]);

    const onDelete = useCallback((id: number)=>{
        const index = inventories.totalItems.findIndex((element => element.ItemID === id))
        if(0 <= index){
            const item = inventories.totalItems[index];
            setDeleteDatas(prev => {
                prev.push(item);
                return deepmerge([], prev);
            });
            setInventories(prev => {
                prev.totalItems.splice(index, 1);
                return new PaginatedList(prev.totalItems, prev.page, prev.parameters, prev.pageSize, prev.pageBlockSize);
            });

            tabContentChanged.current = true;
        }
    },[inventories, tabContentChanged]);

    const onSearch = useCallback(()=> {
        onTabSearch(selectItem.current?.ID ?? 0, selectCategory.current?.ID ?? 0);
    }, [selectItem, selectCategory,  onTabSearch]);

    const onHandleChangeListCount = useCallback(( event: SelectChangeEvent<unknown> ) => {
        onChangeListCount(event);
    },[onChangeListCount]);

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

    const onChangeItemCount = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, id: number) => {
        const cleanedValue = Number(e.target.value.replace(/,/g, ''));
        if(!cleanedValue || cleanedValue > 10000) return;
        if(cleanedValue < 0) return;

        inventories.totalItems.map((element) => {
            if(element.ItemID == id) {
                element.Count = cleanedValue;
                element.isChanged = true;
            }
        })
        setInventories(prev => {
            return new PaginatedList(prev.totalItems, prev.page, prev.parameters, prev.pageSize, prev.pageBlockSize);
        });

        tabContentChanged.current = true;
        
    }, [inventories, tabContentChanged]);

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;

        if (inventories && 0 < inventories?.items.length && items.length) {
            const list: ReactElement[] = [];
            for (let i = 0; i < inventories.items.length; i++) {
                const inventory = inventories.items[i];

                let rowClass: string = commonUIStyles.row;
                if (inventory.isChanged)
                    rowClass = `${commonUIStyles.row} ${commonUIStyles.changed}`;
            
                let invenType = "";
                if(inventory?.ItemType) invenType = `${categoryTranslate(inventory?.ItemType?.toString() ?? "")} (${inventory?.ItemType ?? ""})`;

                list.push(
                    <BorderedTableRow key={i} className={rowClass}>
                        <TableCell>
                            <Button disabled={isSigned} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant='outlined' size='small' color='error'
                            onClick={() => onDelete(inventory.ItemID)}>삭제</Button>
                        </TableCell>
                        <TableCell>{inventory?.ItemNameStringWithID && inventory?.ItemNameStringWithID.length >= 1 ? inventory?.ItemNameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : inventory?.ItemID.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{invenType.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>
                            <TextField disabled={isSigned} variant='outlined' value={inventory.Count ? inventory.Count.toLocaleString() : 0} type='text' 
                            onChange={(e) => onChangeItemCount(e, inventory.ItemID)}
                            placeholder='수량' label="수량을 입력하세요"
                            />
                        </TableCell>
                        <TableCell>{dayjs(inventory.CreateAt).format('YYYY-MM-DD HH:mm:ss')}</TableCell>
                        <TableCell>{dayjs(inventory.UpdateAt).format('YYYY-MM-DD HH:mm:ss')}</TableCell>
                    </BorderedTableRow>
                )
            }
    
            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>관리</TableCell>
                            <TableCell>아이템명</TableCell>
                            <TableCell>아이템 타입</TableCell>
                            <TableCell>수량</TableCell>
                            <TableCell>생성일시</TableCell>
                            <TableCell>수정일시</TableCell>
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
                        <TableCell className={commonUIStyles.noneCell}>검색된 인벤토리 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }

        return result;
    }, [inventories, items, isSigned, onChangeItemCount, onDelete]);

    return (
        <>
            <Box sx={{ mt: 5}}>
                <Box sx={ { display: { xs: 'block', lg : 'flex'}, alignItems: { lg: 'center'}, justifyContent: { lg: 'space-between'}, mb: 1 }}>
                    <Box sx={{ display: 'flex', alignItems: 'center' }}>
                        <Box>
                            <Button sx={{ display: isSigned ? 'none' : 'inline-block' }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSave} >저장</Button>
                            <Button sx={{ ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload}>새로고침</Button>
                        </Box>
                        <Box sx={{ marginLeft : 1}}>
                            <ListCountSelector pageLogs={datas.pageSize} handleChange={onHandleChangeListCount}/>
                        </Box>
                    </Box>
                    <Box display='flex' alignItems='center' marginTop={1} sx={{ justifyContent: { xs: 'start', sm: 'end'}, maxWidth: '100%' }}>
                        <Box>
                            <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size='small' sx={{ ml: 1, mr: 1 }} onClick={onSearch}>검색</Button>
                        </Box>
                        <FormControl>
                            <Autocomplete options={categories} size='small' sx={{ width: {xs: 150, sm : 175, md: 200}, maxWidth: '100%', marginRight: 1}}
                            getOptionLabel={(option) => (option as CategoryInfo).nameStringID} 
                            renderInput={(params) => <TextField {...params} label="타입분류" />}
                            onChange={(e, v)=> onChangeCategory(e, v as CategoryInfo)} />
                        </FormControl>
                        <FormControl>
                            <Autocomplete options={items} size='small' sx={{ width: {xs: 260, sm : 300, md: 350}, maxWidth: '100%'}}
                            getOptionLabel={(option) => (option as DataTableModels.Item).NameStringWithID} 
                            renderInput={(params) => <TextField {...params} label="아이템명" />}   
                            onChange={(e, v)=> onChangeItem(e, v as DataTableModels.Item)}
                            />
                        </FormControl>
                    </Box>
                </Box>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>인벤토리</Typography>
                    </Toolbar>
                    <Table stickyHeader aria-label="sticky table">
                        {contents()}
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}

export default EditInventory;