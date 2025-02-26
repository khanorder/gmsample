import { ReactElement, useEffect, useRef, useState, useCallback, SyntheticEvent } from 'react';
import { useAppDispatch, useAppSelector } from "@hooks/index";
import { GetServerSideProps } from 'next';
import { GameAPI } from '@ngeldata/apis/gameAPI';
import * as layoutsActions from '@store/reducers/layouts';
import { Errors } from '@ngel/data/autoErrors';
import { Models } from '@ngel/data/models';
import { useRouter } from 'next/router';
import styles from '@styles/pages/GMS/Users/edit.module.sass';
import dynamic from 'next/dynamic';
import UserSearch from '@components/layouts/ui/uiUserSearch';
import { Defines } from '@ngel/data/autoDefines';
import isEmpty from 'lodash/isEmpty';
import { PaginatedList } from '@helpers/paging';
import { SelectChangeEvent } from '@mui/material/Select';

const Paging = dynamic(() => import('@components/ui/Paging'), { ssr: false });
const UserAccount = dynamic(() => import('@components/GMS/Users/Edit/EditUserAccount'), { ssr: false });
const Account = dynamic(() => import('@components/GMS/Users/Edit/EditAccount'), { ssr: false });
const Asset = dynamic(() => import('@components/GMS/Users/Edit/EditAsset'), { ssr: false });
const Inventory = dynamic(() => import('@components/GMS/Users/Edit/EditInventory'), { ssr: false });
const Mail = dynamic(() => import('@components/GMS/Users/Edit/EditMail'), { ssr: false });
const Artifact = dynamic(() => import('@components/GMS/Users/Edit/EditArtifact'), { ssr: false });
const RpgAttribute = dynamic(() => import('@components/GMS/Users/Edit/EditRpgAttribute'), { ssr: false });
const WeaponCategory = dynamic(() => import('@components/GMS/Users/Edit/EditWeaponCategory'), { ssr: false });
const Collection = dynamic(() => import('@components/GMS/Users/Edit/EditCollection'), { ssr: false });
const Attendance = dynamic(() => import('@components/GMS/Users/Edit/EditAttendance'), { ssr: false });
const Entitlement = dynamic(() => import('@components/GMS/Users/Edit/EditEntitlement'), { ssr: false });
const Profile = dynamic(() => import('@components/GMS/Users/Edit/EditProfile'), { ssr: false });
const SeasonPass = dynamic(() => import('@components/GMS/Users/Edit/EditSeasonPass'), { ssr: false });
const SeasonMission = dynamic(() => import('@components/GMS/Users/Edit/EditSeasonMission'), { ssr: false });
const GuideMission = dynamic(() => import('@components/GMS/Users/Edit/EditGuideMission'), { ssr: false });
const Achievement = dynamic(() => import('@components/GMS/Users/Edit/EditAchievement'), { ssr: false });
const WonderStore = dynamic(() => import('@components/GMS/Users/Edit/EditWonderStore'), { ssr: false });
const WonderCube = dynamic(() => import('@components/GMS/Users/Edit/EditWonderCube'), { ssr: false });
const Layout = dynamic(() => import('@components/layouts'), { ssr: false });
const ThemeLayout = dynamic(() => import('@components/layouts/theme'), { ssr: false });
const ManageLayout = dynamic(() => import('@components/layouts/manage'), { ssr: false });

const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const Tabs = dynamic(() => import('@mui/material/Tabs'), { ssr: false });
const Tab = dynamic(() => import('@mui/material/Tab'), { ssr: false });

interface TabPanelProps {
    children?: React.ReactNode;
    index: number;
    value: number;
}

interface SearchProps {
    pageProp: number;
    searchTypeProp: string;
    searchValueProp: string;
    tabProp: number;
    tabSearchValueProp: string;
    tabCateValueProp: string;
    pageListCountValueProp: string;
}

function TabPanel(props: TabPanelProps) {
    const { children, value, index, ...other } = props;

    return (
        <div role="tabpanel" hidden={value !== index} id={`simple-tabpanel-${index}`} aria-labelledby={`simple-tab-${index}`} {...other}>
            {value === index && (<>{children}</>)}
        </div>
    );
}

function a11yProps(index: number) {
    return {
        id: `simple-tab-${index}`,
        'aria-controls': `simple-tabpanel-${index}`,
    };
}

const pageListCounts = [10, 25, 50, 100, 200, 500];

function Page({ pageProp, searchTypeProp, searchValueProp, tabProp, tabSearchValueProp, tabCateValueProp, pageListCountValueProp }: SearchProps) {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const tables = useAppSelector(state => state.tables);

    const dispatch = useAppDispatch();
    const firstRender = useRef(true);
    const contentsChanged = useRef(false);
    const router = useRouter();
    const [searchValue, setSearchValue] = useState<string>();
    const [searchType, setSearchType] = useState<Defines.UserSearchType>(Defines.UserSearchType.AccountID);
    const [userAccount, setUserAccount] = useState<Models.UserAccount | null>(null);
    const [accounts, setAccounts] = useState<Models.Account[]>([]);
    const [assets, setAssets] = useState<Models.Asset[]>([]);
    const [inventories, setInventory] = useState<Models.Inventory[] | null>(null);
    const [mails, setMails] = useState<Models.Mail[] | null>(null);
    const [heroes, setHeroes] = useState<Models.Hero[] | null>(null);
    const [heroSkins, setHeroSkins] = useState<Models.HeroSkin[] | null>(null);
    const [artifacts, setArtifacts] = useState<Models.Artifact[] | null>(null);
    const [rpgAttributes, setRpgAttributes] = useState<Models.RpgAttribute[] | null>(null);
    const [weaponCategories, setWeaponCategories] = useState<Models.WeaponCategory[] | null>(null);
    const [collections, setCollections] = useState<Models.Collection[] | null>(null);
    const [pets, setPets] = useState<Models.Pet[] | null>(null);
    const [attendances, setAttendances] = useState<Models.Attendance[] | null>(null);
    const [entitlements, setEntitlements] = useState<Models.Entitlement[] | null>(null);
    const [profiles, setProfiles] = useState<Models.Profile[] | null>(null);
    const [treasureBoxes, setTreasureBoxes] = useState<Models.TreasureBox[] | null>(null);
    const [seasonPasses, setSeasonPasses] = useState<Models.SeasonPass[] | null>(null);
    const [seasonMissions, setSeasonMissions] = useState<Models.SeasonMission[] | null>(null);
    const [guideMissions, setGuideMissions] = useState<Models.GuideMission[] | null>(null);
    const [achievement, setAchievement] = useState<Models.Achievement[] | null>(null);
    const [wonderStore, setWonderStore] = useState<Models.WonderStore[] | null>(null);
    const [wonderCube, setWonderCube] = useState<Models.WonderCube[] | null>(null);
    const [tabIndex, setTabIndex] = useState<number>(tabProp);

    //for Paging
    const [invenDatas, setInvenDatas] = useState<PaginatedList<Models.Inventory>>(new PaginatedList<Models.Inventory>([]));
    const [mailDatas, setMailDatas] = useState<PaginatedList<Models.Mail>>(new PaginatedList<Models.Mail>([]));
    const [artifactDatas, setArtifactDatas] = useState<PaginatedList<Models.Artifact>>(new PaginatedList<Models.Artifact>([]));
    const [rpgAttributesDatas, setRpgAttributesDatas] = useState<PaginatedList<Models.RpgAttribute>>(new PaginatedList<Models.RpgAttribute>([]));
    const [collectionDatas, setCollectionDatas] = useState<PaginatedList<Models.Collection>>(new PaginatedList<Models.Collection>([]));
    const [seasonMissionDatas, setSeasonMissionDatas] = useState<PaginatedList<Models.SeasonMission>>(new PaginatedList<Models.SeasonMission>([]));
    const [guideMissionDatas, setGuideMissionDatas] = useState<PaginatedList<Models.GuideMission>>(new PaginatedList<Models.GuideMission>([]));
    const [achievementDatas, setAchievementDatas] = useState<PaginatedList<Models.Achievement>>(new PaginatedList<Models.Achievement>([]));

    const [refreshCount, setRefreshCount] = useState<number>(0);

    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

        const parameters = `searchType=${searchTypeProp}&searchValue=${searchValueProp}&tab=${tabProp}&tabSearchValue=${tabSearchValueProp}&tabCateValue=${tabCateValueProp}&lc=${pageListCountValueProp}`
        const pageSize = pageListCounts.includes(parseInt(pageListCountValueProp)) ? parseInt(pageListCountValueProp) : 10;

        if (inventories && tabProp === 2) {
            let searched = inventories;
            if (tabSearchValueProp) searched = searched?.filter(element => element.ItemID === parseInt(tabSearchValueProp));
            if (tabCateValueProp) searched = searched?.filter(element => element.ItemType === parseInt(tabCateValueProp));

            setInvenDatas(prev => {
                return new PaginatedList<Models.Inventory>(searched, pageProp, parameters, pageSize, prev.pageBlockSize);
            })
        }

        if (mails && tabProp === 3) {
            setMailDatas(prev => {
                return new PaginatedList<Models.Mail>(mails, pageProp, parameters, pageSize, prev.pageBlockSize);
            })
        }

        if (artifacts && tabProp === 4) {
            setArtifactDatas(prev => {
                return new PaginatedList<Models.Artifact>(artifacts, pageProp, parameters, pageSize, prev.pageBlockSize);
            })
        }

        if (rpgAttributes && tabProp === 5) {
            setRpgAttributesDatas(prev => {
                return new PaginatedList<Models.RpgAttribute>(rpgAttributes, pageProp, parameters, pageSize, prev.pageBlockSize);
            })
        }

        if (collections && tabProp === 7) {
            setCollectionDatas(prev => {
                return new PaginatedList<Models.Collection>(collections, pageProp, parameters, pageSize, prev.pageBlockSize);
            })
        }

        if (seasonMissions && tabProp === 12) {
            setSeasonMissionDatas(prev => {
                return new PaginatedList<Models.SeasonMission>(seasonMissions, pageProp, parameters, pageSize, prev.pageBlockSize);
            })
        }

        if (guideMissions && tabProp === 13) {
            setGuideMissionDatas(prev => {
                return new PaginatedList<Models.GuideMission>(guideMissions, pageProp, parameters, pageSize, prev.pageBlockSize);
            })
        }

        if (achievement && tabProp === 14) {
            setAchievementDatas(prev => {
                return new PaginatedList<Models.Achievement>(achievement, pageProp, parameters, pageSize, prev.pageBlockSize);
            })
        }

    }, [firstRender, inventories, mails, artifacts, rpgAttributes, collections, seasonMissions, guideMissions, achievement,
        pageProp, searchTypeProp, searchValueProp, tabProp, tabSearchValueProp, tabCateValueProp, pageListCountValueProp]);
    //#endregion

    const onChangeTab = useCallback(async (e: SyntheticEvent, index: number) => {
        if (!userAccount || 1 > userAccount.UID) {
            alert("사용자 정보가 없습니다.")
            return;
        }

        if (contentsChanged.current) {
            if (!confirm("작업중인 내용이 사라집니다.\n정말 이동 하시겠습니까?")) {
                return;
            }
            contentsChanged.current = false;
        }

        const parameters = `?page=1&searchType=${searchTypeProp}&searchValue=${searchValueProp}&tab=${index}&tabSearchValue=&lc=${pageListCountValueProp}`;
        router.push(parameters);

        setTabIndex(index);

    }, [router, setTabIndex, userAccount, contentsChanged, searchTypeProp, searchValueProp, pageListCountValueProp]);

    const searchDetail = useCallback(async (index: number, searchAccountID: number): Promise<boolean> => {
        if (1 > searchAccountID) {
            alert("사용자 정보가 없습니다.")
            return false;
        }

        dispatch(layoutsActions.startLoadingMessage("사용자 상세 정보를 검색 중입니다."));

        switch (index) {
            case 0:
                const responseLink = await GameAPI.AccountsAsync({ UID: searchAccountID });
                if (responseLink.result && null != responseLink.accounts) {
                    setAccounts(responseLink.accounts);
                }
                break;

            case 1:
                const responseAssets = await GameAPI.AssetsAsync({ UID: searchAccountID });
                if (responseAssets.result && null != responseAssets.assets) {
                    setAssets(responseAssets.assets);
                }
                break;

            case 2:
                const responseInventory = await GameAPI.InventoriesAsync({ UID: searchAccountID });
                if (responseInventory.result && null != responseInventory.inventories) {
                    responseInventory.inventories.map(invenElement => {
                        const itemInfo = tables.itemTable.find((tableElement) => tableElement?.ID === invenElement?.ItemID);
                        if (itemInfo) {
                            invenElement.ItemType = itemInfo.ItemType;
                            invenElement.ItemNameStringWithID = itemInfo.NameStringWithID;
                        }
                    })
                    setInventory(responseInventory.inventories);
                }
                break;

            case 3:
                const responseMail = await GameAPI.MailsAsync({ UID: searchAccountID });
                if (responseMail.result && null != responseMail.mails) {
                    setMails(responseMail.mails);
                }
                break;

            case 4:
                const responseArtifacts = await GameAPI.ArtifactsAsync({ UID: searchAccountID });
                if (responseArtifacts.result && null != responseArtifacts.artifacts) {
                    setArtifacts(responseArtifacts.artifacts);
                }
                break;

            case 5:
                const responseRpgAttributes = await GameAPI.RpgAttributesAsync({ UID: searchAccountID });
                if (responseRpgAttributes.result && null != responseRpgAttributes.rpgAttributes) {
                    setRpgAttributes(responseRpgAttributes.rpgAttributes);
                }
                break;

            case 6:
                const responseWeaponCategories = await GameAPI.WeaponCategoriesAsync({ UID: searchAccountID });
                if (responseWeaponCategories.result && null != responseWeaponCategories.weaponCategories) {
                    setWeaponCategories(responseWeaponCategories.weaponCategories);
                }
                break;

            case 7:
                const responseCollections = await GameAPI.CollectionsAsync({ UID: searchAccountID });
                if (responseCollections.result && null != responseCollections.collections) {
                    const sortData = responseCollections.collections.sort((a, b) => Number(b.IsRewarded) - Number(a.IsRewarded));
                    setCollections(sortData);
                }
                break;

            case 8:
                const responseAttendances = await GameAPI.AttendancesAsync({ UID: searchAccountID });
                if (responseAttendances.result && null != responseAttendances.attendances) {
                    setAttendances(responseAttendances.attendances);
                }
                break;

            case 9:
                const responseEntitlements = await GameAPI.EntitlementsAsync({ UID: searchAccountID });
                if (responseEntitlements.result && null != responseEntitlements.entitlements) {
                    setEntitlements(responseEntitlements.entitlements);
                }
                break;

            case 10:
                const responseProfiles = await GameAPI.ProfilesAsync({ UID: searchAccountID });
                if (responseProfiles.result && null != responseProfiles.profiles) {
                    setProfiles(responseProfiles.profiles);
                }
                break;

            case 11:
                const responseSeasonPasses = await GameAPI.SeasonPassesAsync({ UID: searchAccountID });
                if (responseSeasonPasses.result && null != responseSeasonPasses.seasonPasses) {
                    setSeasonPasses(responseSeasonPasses.seasonPasses);
                }
                break;

            case 12:
                const responseSeasonMission = await GameAPI.SeasonMissionsAsync({ UID: searchAccountID });
                if (responseSeasonMission.result && null != responseSeasonMission.seasonMissions) {
                    const sortData = responseSeasonMission.seasonMissions.sort((a, b) => Number(b.IsComplete) - Number(a.IsComplete));
                    setSeasonMissions(sortData);
                }
                break;

            case 13:
                const responseGuideMission = await GameAPI.GuideMissionsAsync({ UID: searchAccountID });
                if (responseGuideMission.result && null != responseGuideMission.guideMissions) {
                    const sortData = responseGuideMission.guideMissions.sort((a, b) => Number(b.IsCompleted) - Number(a.IsCompleted));
                    setGuideMissions(sortData);
                }
                break;

            case 14:
                const responseAchievement = await GameAPI.AchievementsAsync({ UID: searchAccountID });
                if (responseAchievement.result && null != responseAchievement.achievements) {
                    setAchievement(responseAchievement.achievements);
                }
                break;

            case 15:
                const respontWonderStore = await GameAPI.WonderStoresAsync({ UID: searchAccountID });
                if (respontWonderStore.result && null != respontWonderStore.wonderStores) {
                    setWonderStore(respontWonderStore.wonderStores)
                }
                break;

            case 16:
                const responseWonderCube = await GameAPI.WonderCubesAsync({ UID: searchAccountID });
                if (responseWonderCube.result && null != responseWonderCube.wonderCubes) {
                    setWonderCube(responseWonderCube.wonderCubes)
                }
                break;
            default:
                alert("분류되지 않은 상세 정보입니다.");
                dispatch(layoutsActions.stopLoading());
                return false;
        }

        dispatch(layoutsActions.stopLoading());
        return true;
    }, [dispatch, tables.itemTable, setAccounts, setAssets, setInventory, setMails, setAttendances, setEntitlements, setProfiles]);

    const search = useCallback(async () => {
        dispatch(layoutsActions.startLoadingMessage("사용자 정보를 검색 중입니다."));
        setUserAccount(null);
        setAccounts([]);
        setAssets([]);
        setInventory(null);
        setMails(null);
        setHeroes(null);
        setHeroSkins(null);
        setArtifacts(null);
        setRpgAttributes(null);
        setWeaponCategories(null);
        setCollections(null);
        setPets(null);
        setAttendances(null);
        setEntitlements(null);
        setProfiles(null);
        setTreasureBoxes(null);
        setSeasonPasses(null);
        setSeasonMissions(null);
        setGuideMissions(null);
        setAchievement(null);

        let searchedUserAccount: Models.UserAccount | null = null;

        if (!isEmpty(searchValueProp)) {
            switch (searchTypeProp) {
                case Defines.UserSearchType.AccountID.toString():
                    if (searchValueProp.match(/[^\d]/g) || isNaN(parseInt(searchValueProp))) {
                        alert("AccountID는 숫자로 입력해 주세요");
                        return;
                    }

                    const responseSearchByUID = await GameAPI.UserAccountByUIDAsync({ UID: searchValueProp });

                    if (false == responseSearchByUID.result || null == responseSearchByUID.userAccount) {
                        switch (responseSearchByUID.error) {
                            case Errors.UserAccountByUID_NotFoundUserAccount:
                                alert(`AccountID가 '${searchValueProp}'인 사용자는 없습니다.`);
                                break;

                            default:
                                alert(`사용자 검색 실패. (error:${Errors[responseSearchByUID.error]})`);
                        }
                        dispatch(layoutsActions.stopLoading());
                        return;
                    }

                    searchedUserAccount = responseSearchByUID.userAccount;
                    break;

                case Defines.UserSearchType.Nick.toString():
                    const responseSearchByNick = await GameAPI.UserAccountByNickAsync({ Nick: searchValueProp });

                    if (false == responseSearchByNick.result || null == responseSearchByNick.userAccount) {
                        switch (responseSearchByNick.error) {
                            case Errors.UserAccountByNick_NotFoundUserAccount:
                                alert(`닉네임이 '${searchValueProp}'인 사용자는 없습니다.`);
                                break;

                            default:
                                alert(`사용자 검색 실패. (error:${Errors[responseSearchByNick.error]})`);
                        }
                        dispatch(layoutsActions.stopLoading());
                        return;
                    }

                    searchedUserAccount = responseSearchByNick.userAccount;
                    break;

                case Defines.UserSearchType.StoveMemberNo.toString():
                    if (searchValueProp.match(/[^\d]/g) || isNaN(parseInt(searchValueProp))) {
                        alert("StoveMemberNo는 숫자로 입력해 주세요");
                        return;
                    }

                    const responseSearchByMemberNo = await GameAPI.UserAccountByMemberNoAsync({ MemberNo: searchValueProp });

                    if (false == responseSearchByMemberNo.result || null == responseSearchByMemberNo.userAccount) {
                        switch (responseSearchByMemberNo.error) {
                            case Errors.UserAccountByUID_NotFoundUserAccount:
                                alert(`StoveMemberNo가 '${searchValueProp}'인 사용자는 없습니다.`);
                                break;

                            default:
                                alert(`사용자 검색 실패. (error:${Errors[responseSearchByMemberNo.error]})`);
                        }
                        dispatch(layoutsActions.stopLoading());
                        return;
                    }

                    searchedUserAccount = responseSearchByMemberNo.userAccount;
                    break;

                default:
                    return;
            }
        }

        if (["1", "2", "3"].includes(searchTypeProp) && searchValueProp && (!searchedUserAccount || 1 > searchedUserAccount.UID)) {
            alert("사용자 검색 실패.");
            dispatch(layoutsActions.stopLoading());
            return;
        }

        setUserAccount(searchedUserAccount);
        if (null != searchedUserAccount) {
            const responsePets = await GameAPI.PetsAsync({ UID: searchedUserAccount.UID });
            if (responsePets.result && responsePets.pets && 0 < responsePets.pets.length) {
                setPets(responsePets.pets);
            }
        }
        dispatch(layoutsActions.stopLoading());

    }, [setUserAccount, dispatch, searchTypeProp, searchValueProp, setAccounts, setAssets, setInventory, setMails, setHeroes, setHeroSkins, setArtifacts, setRpgAttributes, setWeaponCategories, setCollections, setPets, setAttendances, setEntitlements,]);

    const onSearch = useCallback(async () => {
        await search();
    }, [search]);

    const onChangeSearch = useCallback((type: Defines.UserSearchType, value: string) => {
        if (!isEmpty(value)) {
            switch (type) {
                case Defines.UserSearchType.AccountID:
                    break;

                case Defines.UserSearchType.Nick:
                    break;

                case Defines.UserSearchType.StoveMemberNo:
                    break;

                default:
                    alert("검색 타입을 선택해주세요.");
                    return;
            }
        }

        setSearchType(type);
        setSearchValue(value);
    }, [setSearchType, setSearchValue]);

    const onSearchUser = useCallback((type: Defines.UserSearchType, value: string) => {
        if (!isEmpty(value)) {
            switch (type) {
                case Defines.UserSearchType.AccountID:
                    if (value.match(/[^\d]/g) || isNaN(parseInt(value))) {
                        alert("AccountID는 숫자로 입력해 주세요");
                        return;
                    }
                    break;

                case Defines.UserSearchType.Nick:
                    break;

                case Defines.UserSearchType.StoveMemberNo:
                    if (value.match(/[^\d]/g) || isNaN(parseInt(value))) {
                        alert("StoveMemberNo는 숫자로 입력해 주세요");
                        return;
                    }
                    break;

                default:
                    alert("검색 타입을 선택해주세요.");
                    return;
            }
        }
        router.push(`${router.pathname}?searchType=${(type ?? 0)}&searchValue=${value}&tab=${tabProp ?? 0}&lc=${pageListCountValueProp ?? 10}`);

        if (value == searchValueProp)
            setRefreshCount(prev => prev + 1);

    }, [router, tabProp, searchValueProp, pageListCountValueProp]);


    const onSaveUserAccount = useCallback(async () => {
        const changed = userAccount?.isChanged;

        if (!changed) {
            alert("저장할 변경된 내용이 없습니다.");
            return;
        }
        else {
            if (!confirm("확인을 누르면 내용이 저장됩니다.\n계속 하시겠습니까?")) {
                return;
            }

            const response = await GameAPI.SaveUserAccountAsync({ userAccount: userAccount });
            if (!response) {
                alert(`오류!`);
                return;
            }
            if (!response.result) {
                alert(`오류! (error: ${Errors[response.error]})`);
                console.log(Errors[response.error]);
                return;
            }
        }
        alert(`저장 했습니다.`);
        await search();

    }, [search, userAccount])

    const onReloadUserAccount = useCallback(async () => {
        const changed = userAccount?.isChanged;
        if (changed) {
            if (!confirm("작업중인 내용이 사라집니다.\n정말 새로고침 하시겠습니까?")) {
                return;
            }
        }
        await search();

    }, [search, userAccount]);

    const onEditUserAccout = useCallback((userAccountParam: Models.UserAccount) => {
        setUserAccount(new Models.UserAccount(userAccountParam));
    }, [setUserAccount]);

    useEffect(() => {
        if (isEmpty(searchTypeProp) || searchTypeProp.match(/[^\d]/g) || isNaN(parseInt(searchTypeProp)) || !Object.values(Defines.UserSearchType).includes(parseInt(searchTypeProp))) {
            setSearchType(Defines.UserSearchType.AccountID);
        } else {
            setSearchType(parseInt(searchTypeProp));
        }

        if (isEmpty(searchValueProp)) {
            setSearchValue(searchValueProp.trim());
        } else {
            setSearchValue("");
        }

        onSearch();
    }, [searchTypeProp, searchValueProp, setSearchType, setSearchValue, onSearch, refreshCount]);

    useEffect(() => {
        if (userAccount && userAccount.UID)
            searchDetail(tabIndex, userAccount.UID);

    }, [tabIndex, userAccount, searchDetail]);

    const onChangePageListCount = useCallback((event: SelectChangeEvent<unknown>) => {
        const value = typeof event.target.value === "number" ? event.target.value : parseInt(event.target.value as string, 10);
        if (pageListCounts.includes(value)) {
            const parameter = `?page=1&searchType=${searchTypeProp}&searchValue=${searchValueProp}&tab=${tabProp}&tabSearchValue=${tabSearchValueProp}&tabCateValue=${tabCateValueProp}&lc=${value}`
            router.replace(parameter);
        }
    }, [router, searchTypeProp, searchValueProp, tabProp, tabCateValueProp, tabSearchValueProp])

    const onTabSearch = useCallback((value: number = 0, categoryValue: number = 0) => {
        let parameters = `?page=1&searchType=${searchTypeProp}&searchValue=${searchValueProp}&tab=${tabProp}&tabSearchValue=${value == 0 ? "" : value}&tabCateValue=${categoryValue == 0 ? "" : categoryValue}&lc=${pageListCountValueProp}`

        router.push(parameters);

    }, [router, searchTypeProp, searchValueProp, tabProp, pageListCountValueProp]);

    return (
        <Box sx={{ pt: 3, pb: 3 }}>
            <Box>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* AccountID는 숫자로 검색하면 됩니다.</Typography>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* 검색 후 세부 탭에서 유저의 상세정보 편집 가능.</Typography>
            </Box>
            <Box>
                <Grid container alignItems="center" justifyContent="center">
                    <Grid item xs={3} sx={{
                        padding: "0 5px",
                        flexBasis: { xs: '100%', sm: '50%', md: '35%', lg: '25%' }, maxWidth: { xs: '100%', sm: '50%', md: '35%', lg: '25%' }
                    }}>
                        <UserSearch onChange={(type, value) => onChangeSearch(type, value)} onSubmit={(type, value) => onSearchUser(type, value)} ignoreSearchType={[Defines.UserSearchType.StoveNickNameNo]} />
                    </Grid>
                </Grid>
            </Box>
            <UserAccount userAccount={userAccount} pets={pets as Models.Pet[]} enableForceLogout={true} onEditUserAccount={(userAccount) => onEditUserAccout(userAccount)} onReload={onReloadUserAccount} onSave={onSaveUserAccount} />
            {
                userAccount
                    ?
                    <Box className={styles.detailInfoArea} sx={{ mb: '100px' }}>
                        <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>

                            <Tabs value={tabIndex} onChange={onChangeTab} aria-label="basic tabs example">
                                <Tab label="계정연결" {...a11yProps(0)} />
                                <Tab label="재화" {...a11yProps(1)} />
                                <Tab label="인벤토리" {...a11yProps(2)} />
                                <Tab label="우편" {...a11yProps(3)} />
                                <Tab label="유물" {...a11yProps(4)} />
                                <Tab label="특성" {...a11yProps(5)} />
                                <Tab label="무기" {...a11yProps(6)} />
                                <Tab label="도감" {...a11yProps(7)} />
                                <Tab label="출석체크" {...a11yProps(8)} />
                                <Tab label="칭호" {...a11yProps(9)} />
                                <Tab label="프로필" {...a11yProps(10)} />
                                <Tab label="시즌패스" {...a11yProps(11)} />
                                <Tab label="시즌미션" {...a11yProps(12)} />
                                <Tab label="가이드미션" {...a11yProps(13)} />
                                <Tab label="업적" {...a11yProps(14)} />
                                <Tab label="상점" {...a11yProps(15)} />
                                <Tab label="큐브" {...a11yProps(16)} />
                            </Tabs>
                        </Box>
                        <TabPanel value={tabIndex} index={0}>
                            <Account accounts={accounts} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={1}>
                            <Asset userAccount={userAccount} tabIndex={tabIndex} assets={assets} searchDetail={searchDetail} tabContentChanged={contentsChanged} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={2}>
                            <Inventory userAccount={userAccount} tabIndex={tabIndex} datas={invenDatas} searchDetail={searchDetail} onTabSearch={onTabSearch} tabContentChanged={contentsChanged} onChangeListCount={onChangePageListCount} />
                            <Paging datas={invenDatas} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={3}>
                            <Mail userAccount={userAccount} tabIndex={tabIndex} datas={mailDatas} searchDetail={searchDetail} tabContentChanged={contentsChanged} onChangePageListCount={onChangePageListCount} />
                            <Paging datas={mailDatas} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={4}>
                            <Artifact userAccount={userAccount} tabIndex={tabIndex} datas={artifactDatas} searchDetail={searchDetail} tabContentChanged={contentsChanged} onChangePageListCount={onChangePageListCount} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={5}>
                            <RpgAttribute userAccount={userAccount} tabIndex={tabIndex} datas={rpgAttributesDatas} searchDetail={searchDetail} tabContentChanged={contentsChanged} onChangePageListCount={onChangePageListCount} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={6}>
                            <WeaponCategory userAccount={userAccount} tabIndex={tabIndex} datas={weaponCategories ?? []} searchDetail={searchDetail} tabContentChanged={contentsChanged} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={7}>
                            <Collection userAccount={userAccount} tabIndex={tabIndex} datas={collectionDatas} searchDetail={searchDetail} tabContentChanged={contentsChanged} onChangePageListCount={onChangePageListCount} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={8}>
                            <Attendance userAccount={userAccount} tabIndex={tabIndex} attendances={attendances} searchDetail={searchDetail} tabContentChanged={contentsChanged} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={9}>
                            <Entitlement userAccount={userAccount} tabIndex={tabIndex} datas={entitlements ?? []} searchDetail={searchDetail} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={10}>
                            <Profile userAccount={userAccount} tabIndex={tabIndex} datas={profiles ?? []} searchDetail={searchDetail} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={11}>
                            <SeasonPass userAccount={userAccount} tabIndex={tabIndex} datas={seasonPasses ?? []} searchDetail={searchDetail} tabContentChanged={contentsChanged} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={12}>
                            <SeasonMission userAccount={userAccount} tabIndex={tabIndex} datas={seasonMissionDatas} searchDetail={searchDetail} tabContentChanged={contentsChanged} onChangePageListCount={onChangePageListCount} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={13}>
                            <GuideMission userAccount={userAccount} tabIndex={tabIndex} datas={guideMissionDatas} searchDetail={searchDetail} tabContentChanged={contentsChanged} onChangePageListCount={onChangePageListCount} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={14}>
                            <Achievement userAccount={userAccount} tabIndex={tabIndex} datas={achievementDatas} searchDetail={searchDetail} tabContentChanged={contentsChanged} onChangePageListCount={onChangePageListCount} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={15}>
                            <WonderStore userAccount={userAccount} tabIndex={tabIndex} datas={wonderStore ?? []} searchDetail={searchDetail} tabContentChanged={contentsChanged} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={16}>
                            <WonderCube userAccount={userAccount} tabIndex={tabIndex} datas={wonderCube ?? []} searchDetail={searchDetail} tabContentChanged={contentsChanged} />
                        </TabPanel>
                    </Box>
                    :
                    <></>
            }
        </Box>
    );
};

Page.getLayout = function getLayout(page: ReactElement) {
    return (
        <Layout>
            <ThemeLayout>
                <ManageLayout>{page}</ManageLayout>
            </ThemeLayout>
        </Layout>
    );
}

export const getServerSideProps: GetServerSideProps = async (context) => {
    const { query } = context;
    const pageString: string = query.page ? query.page.toString() : '1';
    const searchType: string = query.searchType ? query.searchType.toString() : "1";
    const searchValue: string = query.searchValue ? query.searchValue.toLocaleString() : "";
    const tabString: string = query.tab ? query.tab.toString() : "0";
    const tabSearchValue: string = query.tabSearchValue ? query.tabSearchValue.toString() : "";
    const tabCateValue: string = query.tabCateValue ? query.tabCateValue.toString() : "";
    const pageListCountValue: string = query.lc ? query.lc.toString() : '10';

    let page = 1;
    let tab = 0;
    try {
        page = !pageString.match(/[^\d]/g) && !isNaN(parseInt(pageString)) ? parseInt(pageString) : 1;
        tab = !tabString.match(/[^\d]/g) && !isNaN(parseInt(tabString)) ? parseInt(tabString) : 0;
    }
    catch (error) {
        if ("production" != process.env.NODE_ENV)
            console.log(error);
    }

    return {
        props: {
            pageProp: page,
            searchTypeProp: searchType,
            searchValueProp: searchValue,
            tabProp: tab,
            tabSearchValueProp: tabSearchValue,
            tabCateValueProp: tabCateValue,
            pageListCountValueProp: pageListCountValue,
        }
    };
}

export default Page;