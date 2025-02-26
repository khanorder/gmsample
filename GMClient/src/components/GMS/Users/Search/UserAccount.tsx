import { useAppSelector } from '@hooks/index';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import { TableContainer, Paper, styled, Toolbar, Typography } from '@mui/material';
import { useState, useEffect, ReactElement, useCallback } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import styles from '@styles/pages/GMS/Users/search.module.sass';
import dynamic from 'next/dynamic';
import { GameAPI } from '@ngel/data/apis/gameAPI';
import { DataTableModels } from '@ngel/data/tables/model';
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });

export interface UserAccountProps {
    userAccount: Models.UserAccount|null;
    enableForceLogout?: boolean;
}

const UserAccount = ({ userAccount, enableForceLogout = false }: UserAccountProps): ReactElement => {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const user = useAppSelector(state => state.user);
    const layouts = useAppSelector(state => state.layouts);

    const tables = useAppSelector(state => state.tables);

    const [characters, setCharacters] = useState<DataTableModels.CharacterData[]>([]);
    const [profileBGs, setProfileBGs] = useState<DataTableModels.ProfileData[]>([]);
    const [profileIcons, setProfileIcons] = useState<DataTableModels.ProfileData[]>([]); 
    const [entitleInfos, setEntitleInfos] = useState<DataTableModels.EntitlementData[]>([]);
    const [petInfos, setPetInfos] = useState<DataTableModels.PetData[]>([]);
    const [vehicleInfos, setVehicleInfos] = useState<DataTableModels.VehicleData[]>([]);
    const [worldMapDatas, setWorldMapDatas] = useState<DataTableModels.WorldMapData[]>([]);

    useEffect(()=> {
        const characterTable = tables.characterDataTable;
        const characterOptions: DataTableModels.CharacterData[] = [];
        for(let i = 0; i < characterTable.length; i++){
            const item = new DataTableModels.CharacterData(characterTable[i]);
            characterOptions.push(item);
        }

        const profileTable = tables.profileDataTable;
        const profileIconOptions: DataTableModels.ProfileData[] = [];
        const profileBGOptions: DataTableModels.ProfileData[] = [];
        for(let i = 0; i < profileTable.length; i++){
            const item = new DataTableModels.ProfileData(profileTable[i]);
            if (item.Type === 1) {
                profileIconOptions.push(item)
            } else if (item.Type === 2) {
                profileBGOptions.push(item)
            }
        }

        const entitleTable = tables.entitlementDataTable;
        const entitleOptions: DataTableModels.EntitlementData[] = [];
        for(let i = 0; i < entitleTable.length; i++){
            const item = new DataTableModels.EntitlementData(entitleTable[i]);
            entitleOptions.push(item);
        }
        
        const skinTable = tables.skinDataTable;
        const skinOptions: DataTableModels.SkinData[] = [];
        for(let i = 0; i < skinTable.length; i++){
            const item = new DataTableModels.SkinData(skinTable[i]);
            skinOptions.push(item);
        }

        const petTable = tables.petDataTable;
        const petOptions: DataTableModels.PetData[] = [];

        for(let i = 0; i < petTable.length; i++){
            const item = new DataTableModels.PetData(petTable[i]);
            petOptions.push(item);
        }

        const vehicleTable = tables.vehicleDataTable;
        const vehicleOptions: DataTableModels.VehicleData[] = [];

        for(let i = 0; i < vehicleTable.length; i++){
            const item = new DataTableModels.VehicleData(vehicleTable[i]);
            vehicleOptions.push(item);
        }

        const worldMapDataTable = tables.worldMapDataTable;
        const worldMapDataOptions: DataTableModels.WorldMapData[] = [];

        for(let i = 0; i < worldMapDataTable.length; i++){
            const item = new DataTableModels.WorldMapData(worldMapDataTable[i]);
            worldMapDataOptions.push(item);
        }

        setCharacters(characterOptions);
        setProfileIcons(profileIconOptions);
        setProfileBGs(profileBGOptions);
        setEntitleInfos(entitleOptions);
        setPetInfos(petOptions);
        setVehicleInfos(vehicleOptions);
        setWorldMapDatas(worldMapDataOptions);

    }, [tables, setCharacters, setProfileIcons, setProfileBGs, setEntitleInfos, setPetInfos, setVehicleInfos, setWorldMapDatas]);

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

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;

        if (userAccount) {

            const characterVal = characters.find((element) => element.ID === userAccount.HeroID);
            const profileIconVal = profileIcons.find((element) => element.ID === userAccount.ProfileIconID);
            const profileBGval = profileBGs.find((element) => element.ID === userAccount.ProfileBGID);
            const entitleVal = entitleInfos.find((element) => parseInt(element.ID) === userAccount.EntitlementID);
            const petVal = petInfos.find((element) => element.ID === userAccount.PetID);
            const vehicleVal = vehicleInfos.find((element) => element.ID === userAccount.VehicleID);
            // const worldMapVal = worldMapDatas.find((element) => element.ID === userAccount.LastZoneID);

            result = (
                <Box sx={{mt: 5}}>
                    <TableContainer component={Paper} elevation={4}>
                        <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                            <Typography variant='h6'>기본정보</Typography>
                        </Toolbar>
                        <Table>
                            <TableHead>
                                <BorderedTableRow className={styles.statusRow}>
                                    <TableCell colSpan={enableForceLogout ? 7 : 8} sx={{ textAlign: "left" }}>
                                        캐릭터 상태&nbsp;:&nbsp;{userAccount.IsSignIn ? <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color="primary">접속중</Button> : <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color="inherit">접속종료</Button> }
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
                                    <TableCell>착용중칭호</TableCell>
                                    <TableCell>착용중 펫</TableCell>
                                </BorderedTableRow>
                            </TableHead>
                            <TableBody>
                                <BorderedTableRow>
                                    <TableCell>{userAccount.UID}</TableCell>
                                    <TableCell>{userAccount.WUID.toString().replaceAll(/(?<=.{3})./g, '*')}</TableCell>
                                    <TableCell>{userAccount.Nick.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                                    <TableCell>{ characterVal?.NameStringWithID ? characterVal?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : userAccount?.HeroID ? userAccount?.HeroID.toString().replaceAll(/(?<=.{1})./g, '*') : 0 }</TableCell>
                                    <TableCell>{ profileIconVal?.NameStringWithID ? profileIconVal?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : userAccount?.ProfileIconID ? userAccount?.ProfileIconID.toString().replaceAll(/(?<=.{1})./g, '*') : 0 }</TableCell>
                                    <TableCell>{ profileBGval?.NameStringWithID ? profileBGval?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : userAccount?.ProfileBGID ? userAccount?.ProfileBGID.toString().replaceAll(/(?<=.{1})./g, '*') : 0 }</TableCell>
                                    <TableCell>{ entitleVal?.NameStringWithID ? entitleVal?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : userAccount?.EntitlementID ? userAccount?.EntitlementID.toString().replaceAll(/(?<=.{1})./g, '*') : 0 }</TableCell>
                                    <TableCell>{ petVal?.NameStringWithID ? petVal?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : userAccount?.PetID ? userAccount?.PetID.toString().replaceAll(/(?<=.{1})./g, '*') : 0}</TableCell>
                                </BorderedTableRow>
                            </TableBody>
                            <TableHead>
                                <BorderedTableRow>
                                    <TableCell>탈것 슬롯</TableCell>
                                    <TableCell>마지막 완료 챕터</TableCell>
                                    <TableCell>글리* **** **</TableCell>
                                    <TableCell>골드*** **** **</TableCell>
                                    <TableCell>유물 *** ** **</TableCell>
                                    <TableCell>패널티 리포트 횟수(UTC 0 기준 초기화)</TableCell>
                                    <TableCell colSpan={2}>최종 패널티 리포트 시각</TableCell>
                                </BorderedTableRow>
                            </TableHead>
                            <TableBody>
                                <BorderedTableRow>
                                    <TableCell>{ vehicleVal?.DescriptionStringWithID ? vehicleVal?.DescriptionStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : userAccount?.VehicleID ? userAccount?.VehicleID.toString().replaceAll(/(?<=.{1})./g, '*') : 0 }</TableCell>
                                    <TableCell>{ userAccount?.LastClearChapterID }</TableCell>
                                    <TableCell>{ userAccount?.IsGlitchTutorialComplete ? '완료' : '' }</TableCell>
                                    <TableCell>{ userAccount?.IsGoldClashTutorialComplete ? '완료' : '' }</TableCell>
                                    <TableCell>{userAccount?.AddArtifactDeckCount}</TableCell>
                                    <TableCell>{userAccount?.PenaltyReportCount}</TableCell>
                                    <TableCell colSpan={2}>{ userAccount.LastPenaltyReportAt ? dayjs.unix(userAccount.LastPenaltyReportAt).tz().format('YYYY-MM-DD HH:mm:ss') : ''}</TableCell>
                                </BorderedTableRow>
                            </TableBody>
                            <TableHead>
                                <BorderedTableRow>
                                    <TableCell>생성일시({timezoneName})</TableCell>
                                    <TableCell>갱신일시({timezoneName})</TableCell>
                                    <TableCell>로그아웃 시각({timezoneName})</TableCell>
                                    <TableCell>로그아웃 위치</TableCell>
                                    <TableCell colSpan={4}>소개글</TableCell>
                                </BorderedTableRow>
                            </TableHead>
                            <TableBody>
                                <BorderedTableRow>
                                    <TableCell>{dayjs(userAccount.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss')}</TableCell>
                                    <TableCell>{dayjs(userAccount.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss')}</TableCell>
                                    <TableCell>{dayjs.unix(userAccount.LastLoginAt).tz().format('YYYY-MM-DD HH:mm:ss')}</TableCell>
                                    {/* <TableCell>{worldMapVal?.WorldmapPopTitleStringWithID ?? userAccount.LastZoneID ?? 0}</TableCell> */}
                                    <TableCell>{userAccount.LastPosition ? userAccount.LastPosition.toString().replaceAll(/(?<=.{3})./g, '*') : ''}</TableCell>
                                    <TableCell colSpan={4}>{userAccount.IntroduceID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
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
    }, [userAccount, enableForceLogout, characters, profileIcons, profileBGs, entitleInfos, petInfos, vehicleInfos, onKickUser]);

    return contents();
}

export default UserAccount;