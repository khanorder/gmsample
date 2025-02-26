import { useAppSelector } from '@hooks/index';
import { ReactElement, useEffect, useState, useRef, useCallback } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer, Paper, styled, Toolbar, Typography } from '@mui/material';
import dynamic from 'next/dynamic';
import { Defines } from '@ngel/data/autoDefines';
import { DataTableModels } from '@ngel/data/tables/model';
import { SelectChangeEvent } from '@mui/material/Select';
import ListCountSelector from './ListCountSelector';
import TextField from '@mui/material/TextField';

const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const Autocomplete = dynamic(() => import('@mui/material/Autocomplete'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const MinWidthTableCell = styled(TableCell)(({ theme }) => ({
    minWidth: "120px"
}));

interface ContentsProps {
    datas: PaginatedList<Models.Pet>;
    onTabSearch: (value: number) => void;
    onChangeListCount: ( event: SelectChangeEvent<unknown> ) => void;
}

const PetContents = ({ datas, onTabSearch, onChangeListCount }: ContentsProps): ReactElement => {

    let content: ReactElement = <></>;
    const tables = useAppSelector(state => state.tables);
    const paramTable = tables.parameterDataTable;
    const petTable = tables.petDataTable;
    const petAbilityTable = tables.petAbilityListDataTable;
    const skillTable = tables.skillDataTable;

    const [pets, setPets] = useState<DataTableModels.PetData[]>([]);
    const selectPet = useRef<DataTableModels.PetData | null>(null);

    useEffect(() => {
        const petOptions: DataTableModels.PetData[] = [];
        for (let i = 0; i < petTable.length; i++) {
            const item = new DataTableModels.PetData(petTable[i]);
                
            petOptions.push(item);
        }

        setPets(petOptions);

    }, [ petTable, setPets ]);

    const onSearch = useCallback(()=> {
        if(selectPet.current){
            onTabSearch(selectPet.current.ID);
        }
        else{
            onTabSearch(0);
        }
    }, [selectPet, onTabSearch]);

    const onChangeItem = useCallback((e, v: DataTableModels.PetData) => {
        if(!v) {
            selectPet.current = null;
            return;
        };

        selectPet.current = v;
    }, [selectPet ]);

    const abilityAssign = useCallback((ability: DataTableModels.PetAbilityListData|null, likeIndex: number) : string => {

        if(ability){
            const ablityInfo = paramTable.find(element => element.ParameterName == ability.AbilityType);

            let value = ability.AbilityRate[likeIndex] != 0 ? ability.AbilityRate[likeIndex] : ability.AbilityValue[likeIndex];
            let str = `${value ?? ''}`;
            if(ablityInfo && ablityInfo.CalcType=="Multi"){
                switch(ablityInfo.CalcType){
                    case "Multi":
                        str = (value * 100 * ablityInfo.CalcValue).toFixed(ablityInfo.DigitCount);
                        break;
                    default:
                        str = value.toFixed(ablityInfo.DigitCount);
                        break;
                }
                if(ablityInfo.AddString) str += "%"
            }
            return str.toString().replaceAll(/(?<=.{2})./g, '*');
        }
        return  "";
    }, [paramTable]);

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;

        if (datas && 0 < datas?.items.length) {
            const list: ReactElement[] = [];

            for (let i = 0; i < datas.items.length; i++) {
                const data = datas.items[i];
                const petInfo = petTable.find(element => element?.ID === data?.PetID);
                const abilities = data?.Ability.slice(1, -1).split(",");
                let abilityInfos = [];
                for (let j = 0; j < abilities.length; j++) {
                    const abiltyData = petAbilityTable.find(element => element?.ID === parseInt(abilities[j]));
                    abilityInfos.push(abiltyData);
                }
                let activeSkillInfo : DataTableModels.SkillData | null = null;
                let likeIndex : number = 0 ;
                let passiveSkillInfos = [];
                if (petInfo) {
                    activeSkillInfo = skillTable.find(element => element.SkillID == petInfo.ActiveSkill) ?? null;
                    likeIndex = petInfo.LikeSection.findIndex(element => data.Like <= element) ?? petInfo?.LikeSection.length - 1 ;

                    for (let k = 0; k < petInfo.PassiveSkill.length; k++) {
                        if(petInfo.PassiveSkill[k] == 0 ) continue;
                        const skillData = skillTable.find(element => element.SkillID == petInfo.PassiveSkill[k]);
                        passiveSkillInfos.push(skillData);
                    }
                }
                list.push(
                    <BorderedTableRow key={i}>
                        <TableCell component="th" scope="row">{data?.UniqueID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{petInfo?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>
                            { activeSkillInfo ? `${activeSkillInfo?.DesignName.toString().replaceAll(/(?<=.{1})./g, '*')}(${activeSkillInfo?.SkillID.toString().replaceAll(/(?<=.{1})./g, '*')})` : ""}
                        </TableCell>
                        <TableCell>
                            {abilityInfos.map((abilty, index) =>{
                                return <Typography key={`${index}-ability`}>{abilty?.AbilityType.toString().replaceAll(/(?<=.{1})./g, '*')}</Typography>
                            })}
                        </TableCell>
                        <TableCell>
                            {abilityInfos.map((abilty, index) =>{
                                return <Typography key={`${index}-abilityValRate`}>{ abilityAssign(abilty ?? null, likeIndex) }</Typography>
                            })}
                        </TableCell>
                        <TableCell>{data?.Like}</TableCell>
                        <TableCell>{data?.IsDeleted ? "v" : "x"}</TableCell>
                        <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    </BorderedTableRow>
                )
            }

            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <MinWidthTableCell>팻 ****</MinWidthTableCell>
                            <MinWidthTableCell>펫 ****</MinWidthTableCell>
                            <TableCell>액** **</TableCell>
                            <MinWidthTableCell>랜덤 **</MinWidthTableCell>
                            <MinWidthTableCell>능력 **</MinWidthTableCell>
                            <MinWidthTableCell>호**</MinWidthTableCell>
                            <MinWidthTableCell>삭제여부</MinWidthTableCell>
                            <MinWidthTableCell>생성일시({timezoneName})</MinWidthTableCell>
                            <MinWidthTableCell>수정일시({timezoneName})</MinWidthTableCell>
                        </BorderedTableRow>
                    </TableHead>
                    <TableBody>
                        {list}
                    </TableBody>
                </>
            )
        } else {
            result = (
                <>
                    <TableBody>
                        <BorderedTableRow>
                            <TableCell className={commonUIStyles.noneCell}>검색된 펫 정보가 없습니다.</TableCell>
                        </BorderedTableRow>
                    </TableBody>
                </>
            );
        }


        return result;
    },[datas, petTable, petAbilityTable, skillTable, abilityAssign]);

    content = (
        <>
            <Box display='flex' alignItems='center' sx={{ justifyContent: 'space-between', marginBottom: 1}}>
                <ListCountSelector pageLogs={datas.pageSize} handleChange={onChangeListCount}/>
                <Box display='flex' alignItems='center' sx={{ justifyContent: { xs: 'start', sm: 'end'}, maxWidth: '100%' }}>
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size='small' sx={{ mr: 1 }} onClick={onSearch}>검색</Button>
                    <FormControl>
                        <Autocomplete options={pets} size='small' sx={{ width: {xs: 260, sm : 300, md: 350}, maxWidth: '100%'}}
                        getOptionLabel={(option) => (option as DataTableModels.PetData)?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*')} 
                        renderInput={(params) => <TextField {...params} label="펫명" />}   
                        onChange={(e, v)=> onChangeItem(e, v as DataTableModels.PetData)}
                        />
                    </FormControl>
                </Box>
            </Box>
            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 700 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6'>펫</Typography>
                </Toolbar>
                <Table stickyHeader aria-label="sticky table">
                    { contents()}
                </Table>
            </TableContainer>
        </>
    )

    return content;
}

export default PetContents;