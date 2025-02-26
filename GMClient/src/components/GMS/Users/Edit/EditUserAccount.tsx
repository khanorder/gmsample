import { useAppSelector } from '@hooks/index';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import { TableContainer, Paper, styled, Toolbar, Typography, TextField } from '@mui/material';
import { ReactElement, useCallback, ChangeEvent, useState, useEffect, useRef } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import styles from '@styles/pages/GMS/Users/search.module.sass';
import dynamic from 'next/dynamic';
import { GameAPI } from '@ngel/data/apis/gameAPI';
import { DataTableModels } from '@ngel/data/tables/model';
import { setPetDataTable } from '@ngel/data/tables/reducer';
import { EItemType, EProfileType } from '@ngel/data/models/lobby';
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Autocomplete = dynamic(() => import('@mui/material/Autocomplete'), { ssr: false });

export interface UserAccountProps {
    userAccount: Models.UserAccount | null;
    pets: Models.Pet[];
    enableForceLogout?: boolean;
    onEditUserAccount: (userAccount: Models.UserAccount) => void;
    onReload: () => void;
    onSave: () => void;
}

enum EditUserAccountType {
    UserLevel,
    UserExp,
    UserRanchLevel,
    UserFarmLevel,
    UserMineLevel,
    UserWorkShopLevel,
    UserTradeTrainLevel,
    UserCraftExp,
}

const EditUserAccount = ({ userAccount, pets, enableForceLogout = false, onEditUserAccount, onReload, onSave }: UserAccountProps): ReactElement => {
    const firstRender = useRef(true);
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const user = useAppSelector(state => state.user);
    const layouts = useAppSelector(state => state.layouts);
    const tables = useAppSelector(state => state.tables);
    const accounts = useAppSelector(state => state.tables.accountLevelTable);

    const [characters, setCharacters] = useState<DataTableModels.CharacterData[]>([]);
    const [profileBGs, setProfileBGs] = useState<DataTableModels.ProfileData[]>([]);
    const [profileIcons, setProfileIcons] = useState<DataTableModels.ProfileData[]>([]);
    const [entitleInfos, setEntitleInfos] = useState<DataTableModels.EntitlementData[]>([]);
    const [petInfos, setPetInfos] = useState<DataTableModels.PetData[]>([]);
    const [vehicleInfos, setVehicleInfos] = useState<Models.Inventory[]>([]);
    const [worldMapDatas, setWorldMapDatas] = useState<DataTableModels.WorldMapData[]>([]);

    const loadData = useCallback(async() => {
        if(userAccount) {
            const responseHeroes = await GameAPI.HeroesAsync({ UID: userAccount.UID });
            console.log("respon : ", responseHeroes)
            const heroTable = tables.characterDataTable;
            const heroesOptions: DataTableModels.CharacterData[] = [];
            if (responseHeroes.result && null != responseHeroes.heroes) {
                for(let i = 0; i < responseHeroes.heroes.length; i++){
                    const current = responseHeroes.heroes[i];
                    const heroInfo = heroTable.find(element => element.ID == current.HeroID);
                    if(heroInfo){
                        const item = new DataTableModels.CharacterData(heroInfo);
                        heroesOptions.push(item);
                    }
                }
            }

            const responseProfiles = await GameAPI.ProfilesAsync({ UID: userAccount.UID });
            const profileTable = tables.profileDataTable;
            const profileIconOptions: DataTableModels.ProfileData[] = [];
            const profileBGOptions: DataTableModels.ProfileData[] = [];
            if (responseProfiles.result && null != responseProfiles.profiles) {
                
                for(let i = 0; i < responseProfiles.profiles.length; i++){
                    const current = responseProfiles.profiles[i];
                    const profileInfo = profileTable.find(element => element.ID == current.ProfileID);
                    
                    if(profileInfo){
                        const item = new DataTableModels.ProfileData(profileInfo);
                        if(item.Type === EProfileType.Icon){
                            profileIconOptions.push(item)
                        }
                        else if(item.Type === EProfileType.BG){
                            profileBGOptions.push(item)
                        }
                    }
                }
            }
            
            const responseEntitlements = await GameAPI.EntitlementsAsync({ UID: userAccount.UID });
            const entitleTable = tables.entitlementDataTable;
            const entitleOptions: DataTableModels.EntitlementData[] = [];
            if (responseEntitlements.result && null != responseEntitlements.entitlements) {

                for(let i = 0; i < responseEntitlements.entitlements.length; i++){
                    const current = responseEntitlements.entitlements[i]
                    const entitleInfo = entitleTable.find(element => element.ID == current.EntitlementID.toString());
                    const item = new DataTableModels.EntitlementData(entitleInfo);
                    entitleOptions.push(item);
                }
            }

            const responsePets = await GameAPI.PetsAsync({ UID: userAccount.UID });
            const petTable = tables.petDataTable;
            const petOptions: DataTableModels.PetData[] = [];
            if (responsePets.result && null != responsePets.pets) {
                for(let i = 0; i < responsePets.pets.length; i++){
                    const current = responsePets.pets[i];
                    const petInfo = petTable.find(element => element.ID == current.PetID);
                    if(petInfo){
                        const item = new DataTableModels.PetData(petTable[i]);
                        petOptions.push(item);
                    }
                }
            }

            const responseInventory = await GameAPI.InventoriesAsync({ UID: userAccount.UID });
            let vehicleOptions: Models.Inventory[] = [];

            if(responseInventory.result && null != responseInventory.inventories){

                responseInventory.inventories.map(invenElement => {
                    const itemInfo = tables.itemTable.find((tableElement) => tableElement?.ID === invenElement?.ItemID);
                    if(itemInfo){
                        invenElement.ItemType = itemInfo.ItemType;
                        invenElement.ItemNameStringWithID = itemInfo.NameStringWithID;
                    }
                })
                const vehicles = responseInventory.inventories.filter(item => item.ItemType == EItemType.Vehicle);
                if( vehicles && 0 < vehicles.length)
                    vehicleOptions = vehicles;

            }
            setCharacters(heroesOptions);
            setProfileIcons(profileIconOptions);
            setProfileBGs(profileBGOptions);
            setEntitleInfos(entitleOptions);
            setVehicleInfos(vehicleOptions);
            setPetInfos(petOptions);
        }
    }, [tables, userAccount]);

    useEffect(() => {
        if (firstRender.current && userAccount)
        {
            firstRender.current = false;
            loadData();
        }
    },[userAccount, firstRender, loadData]);

    useEffect(()=> {
        const worldMapDataTable = tables.worldMapDataTable;
        const worldMapDataOptions: DataTableModels.WorldMapData[] = [];

        for (let i = 0; i < worldMapDataTable.length; i++) {
            const item = new DataTableModels.WorldMapData(worldMapDataTable[i]);
            worldMapDataOptions.push(item);
        }
        setWorldMapDatas(worldMapDataOptions);

    }, [tables]);

    const onKickUser = useCallback(async () => {
        if (userAccount) {
            if (!confirm(`${userAccount.Nick}(UID: ${userAccount.UID}) 계정을 강제종료합니다.\n계속 하시겠습니까?`))
                return;

            var response = await GameAPI.KickUserAsync({ UID: userAccount.UID.toString() });
            if (response.result) {
                alert(`${userAccount.Nick}(UID: ${userAccount.UID}) 계정을 강제 종료했습니다.`);
            } else {
                alert(`강제 종료하지 못했습니다.`);
            }
        } else {
            alert("우선 강제 종료할 사용자를 검색해주세요.");
        }
    }, [userAccount]);

    const onChangeInputData = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>, type: EditUserAccountType) => {
        if (null == userAccount)
            return;

        userAccount.isChanged = true;
        onEditUserAccount(userAccount);
    }, [userAccount, onEditUserAccount]);

    const onChangeInfos = useCallback((e, v: DataTableModels.CharacterData | DataTableModels.ProfileData | DataTableModels.EntitlementData | DataTableModels.PetData | DataTableModels.WorldMapData) => {
        if (null == userAccount)
            return;

        if (v !== null && v !== undefined) {

            if (v instanceof DataTableModels.ProfileData) {
                switch (v.Type) {
                    case 1:
                        userAccount.ProfileIconID = v.ID;
                        break;
                    case 2:
                        userAccount.ProfileBGID = v.ID;
                        break;
                }
            } else if (v instanceof DataTableModels.CharacterData) {
                userAccount.HeroID = v.ID;
            }
            // else if(v instanceof DataTableModels.WorldMapData){ userAccount.LastZoneID = v.ID}

            userAccount.isChanged = true;
            onEditUserAccount(userAccount);
        }

    }, [userAccount, onEditUserAccount]);

    const onChangeEntitlementSlot = useCallback((e, v: DataTableModels.EntitlementData) => {
        if (null == userAccount)
            return;

        if (v !== null && v !== undefined) {
            userAccount.EntitlementID = parseInt(v.ID);
        } else if (v === null) {
            userAccount.EntitlementID = 0;
        }
        userAccount.isChanged = true;
        onEditUserAccount(userAccount);

    }, [userAccount, onEditUserAccount]);

    const onChangePetSlot = useCallback((e, v: DataTableModels.PetData) => {
        if (null == userAccount)
            return;

        if (v !== null && v !== undefined) {
            userAccount.PetUniqueID = v.ID;
        } else if (v === null) {
            userAccount.PetUniqueID = 0;
        }
        userAccount.isChanged = true;
        onEditUserAccount(userAccount);

    }, [userAccount, onEditUserAccount]);

    const onChangeVehicleSlot = useCallback((e, v: Models.Inventory) => {
        if (null == userAccount)
            return;

        if (v !== null && v !== undefined) {
            userAccount.VehicleID = v.ItemID;
        } else if (v === null) {
            userAccount.VehicleID = 0;
        }
        userAccount.isChanged = true;
        onEditUserAccount(userAccount);
    }, [userAccount, onEditUserAccount]);


    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;

        if (userAccount) {
            let rowClass: string = commonUIStyles.row;
            if (userAccount.isChanged)
                rowClass = `${commonUIStyles.row} ${commonUIStyles.changed}`;

            const characterVal = characters.find((element) => element.ID === userAccount.HeroID);
            const profileIconVal = profileIcons.find((element) => element.ID === userAccount.ProfileIconID) ?? null;
            const profileBGval = profileBGs.find((element) => element.ID === userAccount.ProfileBGID) ?? null;
            const entitleVal = entitleInfos.find((element) => parseInt(element.ID) === userAccount.EntitlementID) ?? null;
            const petVal = petInfos.find((element) => element.ID === userAccount.PetUniqueID) ?? null;
            const vehicleVal = vehicleInfos.find((element) => element.ItemID === userAccount.VehicleID) ?? null;
            // const worldMapVal = worldMapDatas.find((element) => element.ID === userAccount.LastZoneID);

            let isDisabled = userAccount.IsSignIn;

            result = (
                <Box sx={{ mt: 5 }}>
                    <Grid sx={{ padding: '10px 0 20px' }}>
                        <Button sx={{ display: userAccount.IsSignIn ? 'none' : 'inline-block', ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSave}>저장</Button>
                        <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" sx={{ ml: 1 }} onClick={onReload}>새로고침</Button>
                    </Grid>
                    <TableContainer component={Paper} elevation={4}>
                        <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                            <Typography variant='h6'>기본정보</Typography>
                        </Toolbar>
                        <Table>
                            <TableHead>
                                <BorderedTableRow className={styles.statusRow}>
                                    <TableCell colSpan={enableForceLogout ? 8 : 9} sx={{ textAlign: "left" }}>
                                        캐릭터 상태&nbsp;:&nbsp;{userAccount.IsSignIn ? <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color="primary">접속중</Button> : <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color="inherit">접속종료</Button>}
                                    </TableCell>
                                    {
                                        enableForceLogout
                                            ?
                                            <TableCell colSpan={1}>
                                                {
                                                    userAccount.IsSignIn
                                                        ?
                                                        <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color="error" size="small" aria-label="search" onClick={onKickUser}>강제종료</Button>
                                                        :
                                                        <></>
                                                }
                                            </TableCell>
                                            :
                                            <></>
                                    }
                                </BorderedTableRow>
                            </TableHead>
                            <TableHead>
                                <BorderedTableRow>
                                    <TableCell>UID</TableCell>
                                    <TableCell>WUID</TableCell>
                                    <TableCell>닉네임</TableCell>
                                    <TableCell>대표 영웅</TableCell>
                                    <TableCell>대표 프로필</TableCell>
                                    <TableCell>대표 프로필 배경</TableCell>
                                    <TableCell colSpan={3}>착용중칭호</TableCell>
                                </BorderedTableRow>
                            </TableHead>
                            <TableBody>
                                <BorderedTableRow className={rowClass}>
                                    <TableCell>{userAccount.UID}</TableCell>
                                    <TableCell>{userAccount.WUID}</TableCell>
                                    <TableCell>{userAccount.Nick}</TableCell>
                                    <TableCell>
                                        {isDisabled ?  
                                        characterVal?.NameStringWithID :
                                        <Autocomplete options={characters} disabled={isDisabled}
                                        getOptionLabel={(option) => (option as DataTableModels.CharacterData).NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')} sx={{minWidth: 150}}
                                        value={characterVal || null} renderInput={(params) => <TextField {...params} label="대표 영웅" />}
                                        onChange={(e, v) => onChangeInfos(e, v as DataTableModels.CharacterData)}/>
                                        }
                                    </TableCell>

                                    <TableCell>
                                        {isDisabled ?  
                                            profileIconVal?.NameStringWithID :
                                            <Autocomplete options={profileIcons} disabled={isDisabled} fullWidth
                                            getOptionLabel={(option) => (option as DataTableModels.ProfileData).NameStringWithID.toString().replaceAll(/(?<=.{3})./g, '*')} sx={{minWidth: 350}}
                                            value={profileIconVal} renderInput={(params) => <TextField {...params} label="대표 프로필" />} 
                                            onChange={(e, v) => onChangeInfos(e, v as DataTableModels.ProfileData)}/>
                                        }
                                    </TableCell>
                                    <TableCell>
                                        {isDisabled ?  
                                            profileBGval?.NameStringWithID :
                                            <Autocomplete options={profileBGs} disabled={isDisabled} fullWidth
                                            getOptionLabel={(option) => (option as DataTableModels.ProfileData).NameStringWithID.toString().replaceAll(/(?<=.{3})./g, '*')} sx={{minWidth: 350}}
                                            value={profileBGval} renderInput={(params) => <TextField {...params} label="대표 프로필 배경" />}
                                            onChange={(e, v) => onChangeInfos(e, v as DataTableModels.ProfileData)} />
                                        } 
                                    </TableCell>
                                    <TableCell colSpan={3}>
                                        {isDisabled ? 
                                            entitleVal?.NameStringWithID :
                                            <Autocomplete options={entitleInfos} disabled={isDisabled}
                                            getOptionLabel={(option) => (option as DataTableModels.EntitlementData).NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')} sx={{ minWidth: 150 }}
                                            value={entitleVal} renderInput={(params) => <TextField {...params} label="착용 중 칭호" />}
                                            onChange={(e, v) => onChangeEntitlementSlot(e, v as DataTableModels.EntitlementData)} />
                                        }
                                    </TableCell>
                                </BorderedTableRow>
                            </TableBody>
                            <TableHead>
                                <BorderedTableRow>
                                    <TableCell>유물 *** ** **</TableCell>
                                    <TableCell>마지막 완료 챕터</TableCell>
                                    <TableCell>글리* **** **</TableCell>
                                    <TableCell>골드***<br/>**** **</TableCell>
                                    <TableCell>탈것 **</TableCell>
                                    <TableCell>착용* *</TableCell>
                                    <TableCell sx={{ minWidth: 170}}>패널티 리포트 횟수<br/>(UTC 0 기준 초기화)</TableCell>
                                    <TableCell colSpan={2}>최종 패널티 리포트 시각</TableCell>
                                </BorderedTableRow>
                            </TableHead>
                            <TableBody>
                                <BorderedTableRow className={rowClass}>
                                    <TableCell>{userAccount?.AddArtifactDeckCount}</TableCell>
                                    <TableCell>{ userAccount?.LastClearChapterID }</TableCell>
                                    <TableCell>{ userAccount?.IsGlitchTutorialComplete ? '완료' : '' }</TableCell>
                                    <TableCell>{ userAccount?.IsGoldClashTutorialComplete ? '완료' : '' }</TableCell>
                                    <TableCell>
                                        {isDisabled ? 
                                            vehicleVal?.ItemNameStringWithID  :
                                            <Autocomplete options={vehicleInfos} disabled={isDisabled}
                                            getOptionLabel={(option) => {
                                                var inventory = (option as Models.Inventory);
                                                if (!inventory || !inventory.ItemNameStringWithID)
                                                    return '';

                                                return inventory.ItemNameStringWithID.toString().replaceAll(/(?<=.{3})./g, '*');
                                            }} sx={{minWidth: 150}}
                                            value={vehicleVal} renderInput={(params) => <TextField {...params} label="탈것" />}
                                            onChange={(e, v) => onChangeVehicleSlot(e, v as Models.Inventory)}/>
                                        }
                                    </TableCell>
                                    <TableCell>
                                        {isDisabled ? 
                                            petVal?.NameStringWithID  :
                                            <Autocomplete options={petInfos} disabled={isDisabled}
                                            getOptionLabel={(option) => {
                                                var pet = (option as DataTableModels.PetData);
                                                if (!pet || !pet.NameStringWithID)
                                                    return '';

                                                return pet.NameStringWithID.toString().replaceAll(/(?<=.{3})./g, '*');
                                            
                                            }} sx={{minWidth: 150}}
                                            value={petVal} renderInput={(params) => <TextField {...params} label="착용* *" />}
                                            onChange={(e, v) => onChangePetSlot(e, v as DataTableModels.PetData)}/>
                                        }
                                    </TableCell>
                                    <TableCell>{userAccount?.PenaltyReportCount}</TableCell>
                                    <TableCell colSpan={2}>{userAccount.LastPenaltyReportAt ? dayjs.unix(userAccount.LastPenaltyReportAt).tz().format('YYYY-MM-DD HH:mm:ss') : ''}</TableCell>
                                </BorderedTableRow>
                            </TableBody>
                            <TableHead>
                                <BorderedTableRow>
                                    <TableCell colSpan={2}>생성일시({timezoneName})</TableCell>
                                    <TableCell colSpan={2}>갱신일시({timezoneName})</TableCell>
                                    <TableCell>로그아웃 시각({timezoneName})</TableCell>
                                    <TableCell>로그아웃 위치</TableCell>
                                    <TableCell colSpan={3}>소개글</TableCell>
                                </BorderedTableRow>
                            </TableHead>
                            <TableBody>
                                <BorderedTableRow className={rowClass}>
                                    <TableCell colSpan={2}>{dayjs(userAccount.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss')}</TableCell>
                                    <TableCell colSpan={2}>{dayjs(userAccount.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss')}</TableCell>
                                    <TableCell>{dayjs.unix(userAccount.LastLoginAt).tz().format('YYYY-MM-DD HH:mm:ss')}</TableCell>
                                    {/* <TableCell>{worldMapVal?.WorldmapPopTitleStringWithID ?? userAccount.LastZoneID ?? 0}</TableCell> */}
                                    <TableCell>{userAccount.LastPosition ?? ''}</TableCell>
                                    <TableCell colSpan={3}>{userAccount.IntroduceID}</TableCell>
                                </BorderedTableRow>
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Box>
            );
        } else {
            result = <></>;
        }

        return result;
    }, [userAccount, enableForceLogout, characters, profileIcons, profileBGs, entitleInfos, petInfos, vehicleInfos, onChangeInfos, onChangePetSlot, onChangeVehicleSlot, onKickUser, onReload, onSave, onChangeEntitlementSlot]);

    return contents();
}

export default EditUserAccount;