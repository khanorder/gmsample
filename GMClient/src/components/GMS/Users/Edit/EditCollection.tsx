import { useAppSelector } from '@hooks/index';
import { Models } from '@ngel/data/models';
import { ReactElement, MutableRefObject, useCallback, useRef, useState, useEffect  } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer, styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { GameAPI } from '@ngel/data/apis/gameAPI';
import { Errors } from '@ngel/data/autoErrors';
import { PaginatedList } from '@helpers/paging';
import { SelectChangeEvent } from '@mui/material/Select';
import ListCountSelector from './EditListCountSelector';
import { DataTableModels } from '@ngel/data/tables/model';
import { ECollectionType } from '@ngel/data/models/lobby';

const Paging = dynamic(() => import('@components/ui/Paging'), { ssr: false });
const CollectionContents = dynamic(() => import('./EditCollectionContents'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });

export interface CollectionProps {
    userAccount: Models.UserAccount;
    tabIndex: number;
    datas : PaginatedList<Models.Collection>;
    searchDetail: (index: number, accountID: number) => void;
    tabContentChanged: MutableRefObject<boolean>;
    onChangePageListCount: ( event: SelectChangeEvent<unknown> ) => void;
}

class OptionType {
    ID: number;
    CollectionID: number;
    CollectionType: number;
    Name: string;
    NameString: string;
    NameStringWithID: string;
    
    constructor(ID?: number, CollectionID?: number, CollectionType?: number, Name?: string, NameString?: string, NameStringWithID?: string){
        this.ID = ID ?? 0;
        this.CollectionID = CollectionID ?? 0;
        this.CollectionType = CollectionType ?? 0,
        this.Name = Name ?? '';
        this.NameString = NameString ?? '';
        this.NameStringWithID = NameStringWithID ?? '';
    }
}

const EditCollection = ({ userAccount, tabIndex, datas, searchDetail, tabContentChanged, onChangePageListCount }: CollectionProps): ReactElement => {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const user = useAppSelector(state => state.user);
    const layouts = useAppSelector(state => state.layouts);
    const tables = useAppSelector(state => state.tables);
    const collectionTable = tables.collectionDataTable;
    const itemTable = tables.itemTable;
    const monsterTable = tables.characterDataTable;
    const petTable = tables.petDataTable;

    const [collectionOpts, setCollectionOpts] = useState<OptionType[]>([]);
    const [collections, setCollections] = useState<PaginatedList<Models.Collection>>(new PaginatedList<Models.Collection>([]));
    const deleteDatas = useRef<Models.Collection[]>([]);

    const isSigned = userAccount.IsSignIn;

    useEffect(() => {
        const collectionOptions: OptionType[] = [];

        const newData = new OptionType(0, 0, 0, "도감 항목을 선택해주세요", "도감 항목을 선택해주세요(0)", "도감 항목을 선택해주세요(0)");
        collectionOptions.push(newData);

        for(const key in collectionTable){
            const collectionData = collectionTable[key];
            let item;
            switch(collectionData?.Type){
                case ECollectionType.Accessory:
                case ECollectionType.Costume:
                case ECollectionType.Weapon:
                case ECollectionType.Vehicle:
                    item = itemTable.find((element)=> element?.ID === collectionData?.ReqID); break;
                case ECollectionType.Monster:
                    item = monsterTable.find((element)=> element?.ID === collectionData?.ReqID); break;
                case ECollectionType.Pet:
                    item = petTable.find((element)=> element?.ID === collectionData?.ReqID); break;
            }
            if(item){
                const newOpt = new OptionType(item.ID, collectionData.ID, collectionData.Type, item.Name, item.NameString, item.NameStringWithID);
                collectionOptions.push(newOpt);
            }
        }

        setCollectionOpts(collectionOptions);
    }, [collectionTable, itemTable, monsterTable, petTable]);

    useEffect(() => {
        setCollections(datas)
    }, [datas]);

    const loadDatas = useCallback(async () => {
        await searchDetail(tabIndex, userAccount.UID);
    }, [tabIndex, userAccount.UID, searchDetail])

    const onReload = useCallback(async () => {
        if(!collections) return;

        const changedDatas = collections.totalItems.filter(_ => _.isChanged);
        if((0 < changedDatas.length)){
            if (!confirm("작업중인 내용이 사라집니다.\n정말 새로고침 하시겠습니까?")) {
                return;
            }
        }

        await loadDatas();
        
        deleteDatas.current = [];
        tabContentChanged.current = false;
    },[loadDatas, tabContentChanged, collections]);

    const onHandleChangeListCount = useCallback(( event: SelectChangeEvent<unknown> ) => {
        if((0 < deleteDatas.current.length)){
            if (!confirm("작업중인 내용이 사라집니다.\n정말 변경 하시겠습니까?")) {
                return;
            }
        }
        onChangePageListCount(event);
        tabContentChanged.current = false;
    },[tabContentChanged, onChangePageListCount]);

    const onAdd = useCallback(() => {
        const newData = new Models.Collection();
        newData.UID = userAccount.UID;
        newData.isChanged = true;
        newData.isNewData = true;
        setCollections(prev => {
            prev.totalItems.splice((prev.page-1)*prev.pageSize, 0, newData);
            return new PaginatedList<Models.Collection>(prev.totalItems, prev.page, prev.parameters, prev.pageSize, prev.pageBlockSize);
        })
    }, [userAccount, setCollections])

    const onDelete = useCallback((index: number) => {
        setCollections(prev => {
            if(!(prev.totalItems[(prev.page-1)*prev.pageSize+index].isNewData)) deleteDatas.current.push(prev.totalItems[(prev.page-1)*prev.pageSize+index]);
            prev.totalItems.splice((prev.page-1)*prev.pageSize+index, 1);
            return new PaginatedList<Models.Collection>(prev.totalItems, prev.page, prev.parameters, prev.pageSize, prev.pageBlockSize);
        })
    }, [setCollections]);

    const onSave = useCallback(async () => {
        const changedDatas = collections.totalItems.filter(_ => _.isChanged && _.CollectionID != 0);
        if ((!changedDatas || 1 > changedDatas.length && deleteDatas.current.length < 1)) {
            alert("저장할 변경된 내용이 없습니다.");
            return;
        }
        else{
            if (!confirm("확인을 누르면 내용이 저장됩니다.\n계속 하시겠습니까?")) {
                return;
            }
        }
        
        if( deleteDatas.current && 0 < deleteDatas.current.length){
            if (!confirm("삭제되는 데이터가 있습니다.\n계속 하시겠습니까?")) {
                return;
            }

            const response = await GameAPI.DeleteCollectionsAsync({ collections : deleteDatas.current });
            if (!response) {
                alert(`오류!`);
                return;
            }
    
            if (!response.result) {
                alert(`오류! (error: ${Errors[response.error]}, index: ${response.errorIndex})`);
                return;
            }
        }

        if( 0 < changedDatas.length){
            const response = await GameAPI.SaveCollectionsAsync({collections: changedDatas});
            if (!response) {
                alert(`오류!`);
                return;
            }
            if (!response.result) {
                alert(`오류! (error: ${Errors[response.error]}, index: ${response.errorIndex})`);
                return;
            }
        }

        alert("저장 했습니다.");
        deleteDatas.current = [];

        await loadDatas();
        
    }, [collections, deleteDatas, loadDatas]);

    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <Box sx={{ display: 'flex', mb: 1 }}>
                    <Box sx={{ display: 'flex', alignItems: 'center' }}>
                        <Button sx={{ display: isSigned ? 'none' : 'inline-block' }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onAdd}>추가</Button>
                        <Button sx={{ display: isSigned ? 'none' : 'inline-block', ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSave}>저장</Button>
                        <Button sx={{ ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload}>새로고침</Button>
                    </Box>
                    <Box marginLeft={1}>
                        <ListCountSelector pageLogs={datas?.pageSize} handleChange={onHandleChangeListCount}/>
                    </Box>
                </Box>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>도감</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                       <CollectionContents userAccount={userAccount} datas={collections ?? []} opts={collectionOpts} onDelete={onDelete}/>
                    </Table>
                </TableContainer>
            </Box>
            <Paging datas={collections} />
        </>
    );
}

export default EditCollection;