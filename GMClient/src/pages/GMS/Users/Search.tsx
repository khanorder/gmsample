import { ReactElement, useEffect, useRef, useState, useCallback, SyntheticEvent } from 'react';
import { useAppDispatch, useAppSelector } from "@hooks/index";
import { GetServerSideProps } from 'next';
import { GameAPI } from '@ngeldata/apis/gameAPI';
import * as layoutsActions from '@store/reducers/layouts';
import { Errors } from '@ngel/data/autoErrors';
import { Models } from '@ngel/data/models';
import { useRouter } from 'next/router';
import styles from '@styles/pages/GMS/Users/search.module.sass';
import dynamic from 'next/dynamic';
import UserSearch from '@components/layouts/ui/uiUserSearch';
import { Defines } from '@ngel/data/autoDefines';
import isEmpty from 'lodash/isEmpty';
import { PaginatedList } from '@helpers/paging';
import { SelectChangeEvent } from '@mui/material/Select';

const Paging = dynamic(() => import('@components/ui/Paging'), { ssr: false });
const UserAccount = dynamic(() => import('@components/GMS/Users/Search/UserAccount'), { ssr: false });
const Account = dynamic(() => import('@components/GMS/Users/Search/Account'), { ssr: false });
const Asset = dynamic(() => import('@components/GMS/Users/Search/Asset'), { ssr: false });
const Inventory = dynamic(() => import('@components/GMS/Users/Search/Inventory'), { ssr: false });
const Mail = dynamic(() => import('@components/GMS/Users/Search/Mail'), { ssr: false });
const Hero = dynamic(() => import('@components/GMS/Users/Search/Hero'), { ssr: false });
const HeroSkin = dynamic(() => import('@components/GMS/Users/Search/HeroSkin'), { ssr: false });
const Artifact = dynamic(() => import('@components/GMS/Users/Search/Artifact'), { ssr: false });
const RpgAttribute = dynamic(() => import('@components/GMS/Users/Search/RpgAttribute'), { ssr: false });
const WeaponCategory = dynamic(() => import('@components/GMS/Users/Search/WeaponCategory'), { ssr: false });
const Collection = dynamic(() => import('@components/GMS/Users/Search/Collection'), { ssr: false });
const Pet = dynamic(() => import('@components/GMS/Users/Search/Pet'), { ssr: false });
const Attendance = dynamic(() => import('@components/GMS/Users/Search/Attendance'), { ssr: false });
const Entitlement = dynamic(() => import('@components/GMS/Users/Search/Entitlement'), { ssr: false });
const SeasonPass = dynamic(() => import('@components/GMS/Users/Search/SeasonPass'), { ssr: false });
const SeasonMission = dynamic(() => import('@components/GMS/Users/Search/SeasonMission'), { ssr: false });
const GuideMission = dynamic(() => import('@components/GMS/Users/Search/GuideMission'), { ssr: false });
const Profile = dynamic(() => import('@components/GMS/Users/Search/Profile'), { ssr: false });
const RankingReward = dynamic(() => import('@components/GMS/Users/Search/RankingReward'), { ssr: false });
const Friends = dynamic(() => import('@components/GMS/Users/Search/Friends'), { ssr: false });
const TreasureBox = dynamic(() => import('@components/GMS/Users/Search/TreasureBox'), { ssr: false });
const Achievement = dynamic(() => import('@components/GMS/Users/Search/Achievement'), { ssr: false });
const ArtifactDeck = dynamic(() => import('@components/GMS/Users/Search/ArtifactDeck'), { ssr: false });
const HeroSkinPreset = dynamic(() => import('@components/GMS/Users/Search/HeroSkinPreset'), { ssr: false });
const EventStore = dynamic(() => import('@components/GMS/Users/Search/EventStore'), { ssr: false });
const WonderStore = dynamic(() => import('@components/GMS/Users/Search/WonderStore'), { ssr: false });
const GlitchStore = dynamic(() => import('@components/GMS/Users/Search/GlitchStore'), { ssr: false });
const GoldMedalStore = dynamic(() => import('@components/GMS/Users/Search/SilverMedalStore'), { ssr: false });
const Penalty = dynamic(() => import('@components/GMS/Users/Search/Penalty'), { ssr: false });
const NicePlayer = dynamic(() => import('@components/GMS/Users/Search/NicePlayer'), { ssr: false });
const UserDevice = dynamic(() => import('@components/GMS/Users/Search/UserDevice'), { ssr: false });
const InstantGuide = dynamic(() => import('@components/GMS/Users/Search/InstantGuide'), { ssr: false });
const Expression = dynamic(() => import('@components/GMS/Users/Search/Expression'), { ssr: false });
const ExpressionPreset = dynamic(() => import('@components/GMS/Users/Search/ExpressionPreset'), { ssr: false });
const WonderCube = dynamic(() => import('@components/GMS/Users/Search/WonderCube'), { ssr: false });
const PetIncubation = dynamic(() => import('@components/GMS/Users/Search/PetIncubation'), { ssr: false });
const PlayRecordGoldClash = dynamic(() => import('@components/GMS/Users/Search/PlayRecordGoldClash'), { ssr: false });
const PlayRecordRpg = dynamic(() => import('@components/GMS/Users/Search/PlayRecordRpg'), { ssr: false });
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
    const [petIncubations, setPetIncubations] = useState<Models.Incubation[] | null>(null);
    const [attendances, setAttendances] = useState<Models.Attendance[] | null>(null);
    const [entitlements, setEntitlements] = useState<Models.Entitlement[] | null>(null);
    const [seasonPasses, setSeasonPasses] = useState<Models.SeasonPass[] | null>(null);
    const [seasonMissions, setSeasonMissions] = useState<Models.SeasonMission[] | null>(null);
    const [guideMissions, setGuideMissions] = useState<Models.GuideMission[] | null>(null);
    const [profiles, setProfiles] = useState<Models.Profile[] | null>(null);
    const [rankingReward, setRankingReward] = useState<Models.RankingReward[] | null>(null);
    const [friends, setFriends] = useState<Models.Friend[] | null>(null);
    const [treasureBox, setTreasureBox] = useState<Models.TreasureBox[] | null>(null);
    const [achievement, setAchievement] = useState<Models.Achievement[] | null>(null);
    const [artifactDeck, setArtifactDeck] = useState<Models.ArtifactDeck[] | null>(null);
    const [heroSkinPreset, setHeroSkinPreset] = useState<Models.HeroSkinPreset[] | null>(null);
    const [eventStore, setEventStore] = useState<Models.EventStore[] | null>(null);
    const [wonderStore, setWonderStore] = useState<Models.WonderStore[] | null>(null);
    const [glitchStore, setGlitchStore] = useState<Models.GlitchStore[] | null>(null);
    const [silverMedalStore, setSilverMedalStore] = useState<Models.SilverMedalStore[] | null>(null);
    const [penalties, setPenalties] = useState<Models.Penalty[] | null>(null);
    const [nicePlayers, setNicePlayers] = useState<Models.NicePlayer[] | null>(null);
    const [userDevices, setUserDevices] = useState<Models.UserDevice[] | null>(null);
    const [instantGuide, setInstantGuide] = useState<Models.InstantGuide[] | null>(null);
    const [expression, setExpression] = useState<Models.Expression[] | null>(null);
    const [expressionPreset, setExpressionPreset] = useState<Models.ExpressionPreset[] | null>(null);
    const [wonderCube, setWonderCube] = useState<Models.WonderCube[] | null>(null);
    const [playRecordGoldClashes, setPlayRecordGoldClashes] = useState<Models.PlayRecordGoldClash[] | null>(null);
    const [playRecordRpg, setPlayRecordRpg] = useState<Models.PlayRecordRpg[] | null>(null);

    const [tabIndex, setTabIndex] = useState<number>(tabProp);

    //for Paging
    const [invenDatas, setInvenDatas] = useState<PaginatedList<Models.Inventory>>(new PaginatedList<Models.Inventory>([]));
    const [mailDatas, setMailDatas] = useState<PaginatedList<Models.Mail>>(new PaginatedList<Models.Mail>([]));
    const [heroSkinDatas, setHeroSkinDatas] = useState<PaginatedList<Models.HeroSkin>>(new PaginatedList<Models.HeroSkin>([]));
    const [artifactDatas, setArtifactDatas] = useState<PaginatedList<Models.Artifact>>(new PaginatedList<Models.Artifact>([]));
    const [rpgAttributeDatas, setRpgAttributeDatas] = useState<PaginatedList<Models.RpgAttribute>>(new PaginatedList<Models.RpgAttribute>([]));
    const [collectDatas, setCollectDatas] = useState<PaginatedList<Models.Collection>>(new PaginatedList<Models.Collection>([]));
    const [petDatas, setPetDatas] = useState<PaginatedList<Models.Pet>>(new PaginatedList<Models.Pet>([]));
    const [seasonMissionDatas, setSeasonMissionDatas] = useState<PaginatedList<Models.SeasonMission>>(new PaginatedList<Models.SeasonMission>([]));
    const [guideMissionDatas, setGuideMissionDatas] = useState<PaginatedList<Models.GuideMission>>(new PaginatedList<Models.GuideMission>([]));
    const [rankingRewadDatas, setRankingRewadDatas] = useState<PaginatedList<Models.RankingReward>>(new PaginatedList<Models.RankingReward>([]));
    const [friendsDatas, setFriendsDatas] = useState<PaginatedList<Models.Friend>>(new PaginatedList<Models.Friend>([]));
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

        if (heroSkins && tabProp === 5) {
            if (0 < tabSearchValueProp.length) {
                const searched = heroSkins.filter(element => element.SkinID === parseInt(tabSearchValueProp));
                setHeroSkinDatas(prev => {
                    return new PaginatedList<Models.HeroSkin>(searched, pageProp, parameters, pageSize, prev.pageBlockSize);
                })
            }
            else {
                setHeroSkinDatas(prev => {
                    return new PaginatedList<Models.HeroSkin>(heroSkins, pageProp, parameters, pageSize, prev.pageBlockSize);
                })
            }
        }

        if (artifacts && tabProp === 7) {
            setArtifactDatas(prev => {
                return new PaginatedList<Models.Artifact>(artifacts, pageProp, parameters, pageSize, prev.pageBlockSize);
            })
        }

        if (rpgAttributes && tabProp === 9) {
            setRpgAttributeDatas(prev => {
                return new PaginatedList<Models.RpgAttribute>(rpgAttributes, pageProp, parameters, pageSize, prev.pageBlockSize);
            })
        }


        if (collections && tabProp === 11) {
            let searched = collections;
            if (tabSearchValueProp) searched = searched?.filter(element => element.CollectionID === parseInt(tabSearchValueProp));
            if (tabCateValueProp) searched = searched?.filter(element => element.CollectionType === parseInt(tabCateValueProp));
            setCollectDatas(prev => {
                return new PaginatedList<Models.Collection>(searched, pageProp, parameters, pageSize, prev.pageBlockSize);
            })
        }

        if (pets && tabProp === 12) {
            if (0 < tabSearchValueProp.length) {
                const searched = pets.filter(element => element.PetID === parseInt(tabSearchValueProp));
                setPetDatas(prev => {
                    return new PaginatedList<Models.Pet>(searched, pageProp, parameters, pageSize, prev.pageBlockSize);
                })
            }
            else {
                setPetDatas(prev => {
                    return new PaginatedList<Models.Pet>(pets, pageProp, parameters, pageSize, prev.pageBlockSize);
                })
            }
        }

        if (seasonMissions && tabProp === 17) {
            setSeasonMissionDatas(prev => {
                return new PaginatedList<Models.SeasonMission>(seasonMissions, pageProp, parameters, pageSize, prev.pageBlockSize);
            })
        }

        if (guideMissions && tabProp === 18) {
            setGuideMissionDatas(prev => {
                return new PaginatedList<Models.GuideMission>(guideMissions, pageProp, parameters, pageSize, prev.pageBlockSize);
            })
        }

        if (rankingReward && tabProp === 20) {
            setRankingRewadDatas(prev => {
                return new PaginatedList<Models.RankingReward>(rankingReward, pageProp, parameters, pageSize, prev.pageBlockSize);
            })
        }

        if (friends && tabProp === 21) {
            setFriendsDatas(prev => {
                return new PaginatedList<Models.Friend>(friends, pageProp, parameters, pageSize, prev.pageBlockSize);
            })
        }

        if (achievement && tabProp === 23) {
            setAchievementDatas(prev => {
                return new PaginatedList<Models.Achievement>(achievement, pageProp, parameters, pageSize, prev.pageBlockSize);
            })
        }
    }, [firstRender, inventories, mails, heroSkins, artifacts, rpgAttributes, collections, pets, seasonMissions, guideMissions, rankingReward, friends, achievement, pageProp, searchTypeProp, searchValueProp, tabProp, tabSearchValueProp, tabCateValueProp, pageListCountValueProp]);
    //#endregion

    const onChangeTab = useCallback(async (e: SyntheticEvent, index: number) => {
        if (!userAccount || 1 > userAccount.UID) {
            alert("사용자 정보가 없습니다.")
            return;
        }

        const parameters = `?page=1&searchType=${searchTypeProp}&searchValue=${searchValueProp}&tab=${index}&tabSearchValue=&lc=${pageListCountValueProp}`;
        router.push(parameters);

        setTabIndex(index);

    }, [router, setTabIndex, userAccount, searchTypeProp, searchValueProp, pageListCountValueProp]);

    const searchDetail = useCallback(async (index: number, searchAccountID: number): Promise<boolean> => {
        if (1 > searchAccountID) {
            alert("사용자 정보가 없습니다.")
            return false;
        }

        dispatch(layoutsActions.startLoadingMessage("사용자 정보를 검색 중입니다."));

        switch (index) {
            case 0:
                const responseLink = await GameAPI.AccountsAsync({ UID: searchAccountID });
                if (responseLink.result && null != responseLink.accounts && 0 < responseLink.accounts.length) {
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
                const responseHeroes = await GameAPI.HeroesAsync({ UID: searchAccountID });
                if (responseHeroes.result && null != responseHeroes.heroes) {
                    setHeroes(responseHeroes.heroes);
                }
                break;

            case 5:
                const responseHeroSkins = await GameAPI.SkinsAsync({ UID: searchAccountID });
                if (responseHeroSkins.result && null != responseHeroSkins.skins) {
                    setHeroSkins(responseHeroSkins.skins);
                }
                break;

            case 6:
                const responseHeroSkinPreset = await GameAPI.SkinPresetsAsync({ UID: searchAccountID });
                if (responseHeroSkinPreset.result && null != responseHeroSkinPreset.skinPresets) {
                    setHeroSkinPreset(responseHeroSkinPreset.skinPresets);
                }
                break;
            case 7:
                const responseArtifacts = await GameAPI.ArtifactsAsync({ UID: searchAccountID });
                if (responseArtifacts.result && null != responseArtifacts.artifacts) {
                    setArtifacts(responseArtifacts.artifacts);
                }
                break;
            case 8:
                const responseArtifactDeck = await GameAPI.ArtifactDecksAsync({ UID: searchAccountID });
                if (responseArtifactDeck.result && null != responseArtifactDeck.artifactDecks) {
                    setArtifactDeck(responseArtifactDeck.artifactDecks);
                }
                break;
            case 9:
                const responseRpgAttributes = await GameAPI.RpgAttributesAsync({ UID: searchAccountID });
                if (responseRpgAttributes.result && null != responseRpgAttributes.rpgAttributes) {
                    setRpgAttributes(responseRpgAttributes.rpgAttributes);
                }
                break;

            case 10:
                const responseWeaponCategories = await GameAPI.WeaponCategoriesAsync({ UID: searchAccountID });
                if (responseWeaponCategories.result && null != responseWeaponCategories.weaponCategories) {
                    setWeaponCategories(responseWeaponCategories.weaponCategories);
                }
                break;

            case 11:
                const responseCollections = await GameAPI.CollectionsAsync({ UID: searchAccountID });
                if (responseCollections.result && null != responseCollections.collections) {
                    const sortData = responseCollections.collections.sort((a, b) => Number(b.IsRewarded) - Number(a.IsRewarded));
                    setCollections(sortData);
                }
                break;

            case 12:
                const responsePets = await GameAPI.PetsAsync({ UID: searchAccountID });
                if (responsePets.result && null != responsePets.pets) {
                    setPets(responsePets.pets);
                }
                break;

            case 13:
                const responsePetIncubations = await GameAPI.IncubationsAsync({ UID: searchAccountID });
                if (responsePetIncubations.result && null != responsePetIncubations.incubations) {
                    setPetIncubations(responsePetIncubations.incubations);
                }
                break;

            case 14:
                const responseAttendances = await GameAPI.AttendancesAsync({ UID: searchAccountID });
                if (responseAttendances.result && null != responseAttendances.attendances) {
                    setAttendances(responseAttendances.attendances);
                }
                break;

            case 15:
                const responseEntitlements = await GameAPI.EntitlementsAsync({ UID: searchAccountID });
                if (responseEntitlements.result && null != responseEntitlements.entitlements) {
                    setEntitlements(responseEntitlements.entitlements);
                }
                break;

            case 16:
                const responseSeasonPasses = await GameAPI.SeasonPassesAsync({ UID: searchAccountID });
                if (responseSeasonPasses.result && null != responseSeasonPasses.seasonPasses) {
                    setSeasonPasses(responseSeasonPasses.seasonPasses);
                }
                break;

            case 17:
                const responseSeasonMission = await GameAPI.SeasonMissionsAsync({ UID: searchAccountID });
                if (responseSeasonMission.result && null != responseSeasonMission.seasonMissions) {
                    const sortData = responseSeasonMission.seasonMissions.sort((a, b) => Number(b.IsComplete) - Number(a.IsComplete));
                    setSeasonMissions(sortData);
                }
                break;

            case 18:
                const responseGuideMission = await GameAPI.GuideMissionsAsync({ UID: searchAccountID });
                if (responseGuideMission.result && null != responseGuideMission.guideMissions) {
                    const sortData = responseGuideMission.guideMissions.sort((a, b) => Number(b.IsCompleted) - Number(a.IsCompleted));
                    setGuideMissions(responseGuideMission.guideMissions);
                }
                break;

            case 19:
                const responseprofiles = await GameAPI.ProfilesAsync({ UID: searchAccountID });
                if (responseprofiles.result && null != responseprofiles.profiles) {
                    setProfiles(responseprofiles.profiles);
                }
                break;

            case 20:
                const responseRankingReward = await GameAPI.RankingRewardsAsync({ UID: searchAccountID });
                if (responseRankingReward.result && null != responseRankingReward.rankingRewards) {
                    setRankingReward(responseRankingReward.rankingRewards);
                }
                break;

            case 21:
                const responseFriends = await GameAPI.FriendsAsync({ UID: searchAccountID });
                if (responseFriends.result && null != responseFriends.friends) {
                    setFriends(responseFriends.friends);
                }
                break;

            case 22:
                const responseTreasureBox = await GameAPI.TreasureBoxesAsync({ UID: searchAccountID });
                if (responseTreasureBox.result && null != responseTreasureBox.treasureBoxes) {
                    setTreasureBox(responseTreasureBox.treasureBoxes);
                }
                break;

            case 23:
                const responseAchievement = await GameAPI.AchievementsAsync({ UID: searchAccountID });
                if (responseAchievement.result && null != responseAchievement.achievements) {
                    setAchievement(responseAchievement.achievements);
                }
                break;

            case 24:
                const responseEventStore = await GameAPI.EventStoresAsync({ UID: searchAccountID });
                if (responseEventStore.result && null != responseEventStore.eventStores) {
                    setEventStore(responseEventStore.eventStores);
                }
                break;

            case 25:
                const responseWonderStore = await GameAPI.WonderStoresAsync({ UID: searchAccountID });
                if (responseWonderStore.result && null != responseWonderStore.wonderStores) {
                    setWonderStore(responseWonderStore.wonderStores);
                }
                break;

            case 26:
                const responseGlitchStore = await GameAPI.GlitchStoresAsync({ UID: searchAccountID });
                if (responseGlitchStore.result && null != responseGlitchStore.glitchStores) {
                    setGlitchStore(responseGlitchStore.glitchStores);
                }
                break;

            case 27:
                const responseGoldMedalStore = await GameAPI.SilverMedalStoresAsync({ UID: searchAccountID });
                if (responseGoldMedalStore.result && null != responseGoldMedalStore.silverMedalStores) {
                    setSilverMedalStore(responseGoldMedalStore.silverMedalStores);
                }
                break;

            case 28:
                const responsePenalties = await GameAPI.PenaltiesAsync({ UID: searchAccountID });
                if (responsePenalties.result && null != responsePenalties.penalties) {
                    setPenalties(responsePenalties.penalties);
                }
                break;

            case 29:
                const responseNicePlayers = await GameAPI.NicePlayersAsync({ UID: searchAccountID });
                if (responseNicePlayers.result && null != responseNicePlayers.nicePlayers) {
                    setNicePlayers(responseNicePlayers.nicePlayers);
                }
                break;

            case 30:
                const responseUserDevice = await GameAPI.UserDevicesAsync({ UID: searchAccountID });
                if (responseUserDevice.result && null != responseUserDevice.userDevices) {
                    setUserDevices(responseUserDevice.userDevices);
                }
                break;
            case 31:
                const responseInstantGuides = await GameAPI.InstantGuidesAsync({ UID: searchAccountID })
                if (responseInstantGuides.result && null != responseInstantGuides.instantGuides) {
                    setInstantGuide(responseInstantGuides.instantGuides);
                }
                break;
            case 32:
                const responseExpression = await GameAPI.ExpressionsAsync({ UID: searchAccountID })
                if (responseExpression.result && null != responseExpression.expressions) {
                    setExpression(responseExpression.expressions);
                }
                break;
            case 33:
                const responseExpressionPreset = await GameAPI.ExpressionPresetsAsync({ UID: searchAccountID })
                if (responseExpressionPreset.result && null != responseExpressionPreset.expressionPresets) {
                    setExpressionPreset(responseExpressionPreset.expressionPresets);
                }
                break;
            case 34:
                const responseWonderCube = await GameAPI.WonderCubesAsync({ UID: searchAccountID });
                if (responseWonderCube.result && null != responseWonderCube.wonderCubes) {
                    setWonderCube(responseWonderCube.wonderCubes)
                }
                break;

            case 35:
                const responseGamePlayRecordColdClash = await GameAPI.PlayRecordGoldClashesAsync({ UID: searchAccountID });
                if (responseGamePlayRecordColdClash.result && null != responseGamePlayRecordColdClash.playRecordGoldClashes) {
                    setPlayRecordGoldClashes(responseGamePlayRecordColdClash.playRecordGoldClashes)
                }
                break;

            case 36:
                const responseGamePlayRpgs = await GameAPI.PlayRecordRpgsAsync({ UID: searchAccountID });
                if (responseGamePlayRpgs.result && null != responseGamePlayRpgs.playRecordRpgs) {
                    setPlayRecordRpg(responseGamePlayRpgs.playRecordRpgs)
                }
                break;
            default:
                alert("분류되지 않은 상세 정보입니다.");
                dispatch(layoutsActions.stopLoading());
                return false;
        }

        dispatch(layoutsActions.stopLoading());
        return true;
    }, [tables, dispatch, setAccounts, setAssets, setInventory, setMails, setHeroes, setHeroSkins, setArtifacts, setRpgAttributes, setWeaponCategories, setCollections, setPets, setPetIncubations, setAttendances, setEntitlements, setSeasonPasses, setSeasonMissions, setRankingReward, setFriends,
        setTreasureBox, setAchievement, setArtifactDeck, setHeroSkinPreset, setEventStore, setWonderStore, setGlitchStore, setSilverMedalStore, setPenalties, setNicePlayers, setInstantGuide, setExpression, setExpressionPreset, setWonderCube, setPlayRecordGoldClashes, setPlayRecordRpg]);

    const search = useCallback(async () => {
        dispatch(layoutsActions.startLoadingMessage("사용자 정보를 검색 중입니다."));
        setUserAccount(null);
        setAccounts([]);
        setAssets([]);
        setInventory(null);
        setMails(null);
        setHeroes(null);
        setHeroSkins(null);
        setHeroSkinPreset(null);
        setArtifacts(null);
        setArtifactDeck(null);
        setRpgAttributes(null);
        setWeaponCategories(null);
        setCollections(null);
        setPets(null);
        setPetIncubations(null);
        setAttendances(null);
        setEntitlements(null);
        setSeasonPasses(null);
        setSeasonMissions(null);
        setGuideMissions(null);
        setProfiles(null);
        setRankingReward(null);
        setFriends(null);
        setTreasureBox(null);
        setAchievement(null);
        setEventStore(null);
        setWonderStore(null);
        setGlitchStore(null);
        setSilverMedalStore(null);
        setPenalties(null);
        setNicePlayers(null);
        setUserDevices(null);
        setInstantGuide(null);
        setExpression(null);
        setExpressionPreset(null);
        setWonderCube(null);
        setPlayRecordGoldClashes(null)
        setPlayRecordRpg(null)

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
        dispatch(layoutsActions.stopLoading());

    }, [setUserAccount, dispatch, searchTypeProp, searchValueProp,
        setAccounts, setAssets, setInventory, setMails, setHeroes, setHeroSkins, setArtifacts, setRpgAttributes, setWeaponCategories, setCollections, setPets, setPetIncubations, setAttendances, setEntitlements,
        setSeasonPasses, setSeasonMissions, setGuideMissions, setProfiles, setRankingReward, setFriends, setTreasureBox, setAchievement, setArtifactDeck, setHeroSkinPreset,
        setEventStore, setWonderStore, setGlitchStore, setSilverMedalStore, setPenalties, setNicePlayers, setUserDevices, setInstantGuide, setExpression, setExpressionPreset, setWonderCube, setPlayRecordGoldClashes, setPlayRecordRpg]);

    const onTabSearch = useCallback((value: number = 0, categoryValue: number = 0) => {
        let parameters = `?page=1&searchType=${searchTypeProp}&searchValue=${searchValueProp}&tab=${tabProp}&tabSearchValue=${value == 0 ? "" : value}&tabCateValue=${categoryValue == 0 ? "" : categoryValue}&lc=${pageListCountValueProp}`

        router.push(parameters);

    }, [router, searchTypeProp, searchValueProp, tabProp, pageListCountValueProp]);

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

    useEffect(() => {
        if (isEmpty(searchTypeProp) || searchTypeProp.match(/[^\d]/g) || isNaN(parseInt(searchTypeProp)) || !Object.values(Defines.UserSearchType).includes(parseInt(searchTypeProp))) {
            setSearchType(Defines.UserSearchType.AccountID);
        } else {
            setSearchType(parseInt(searchTypeProp));
        }

        if (isEmpty(searchValueProp)) {
            setSearchValue("");
        } else {
            setSearchValue(searchValueProp.trim());
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
    }, [router, searchTypeProp, searchValueProp, tabProp, tabSearchValueProp, tabCateValueProp])

    return (
        <Box sx={{ pt: 3, pb: 3 }}>
            <Box>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* AccountID는 숫자로 검색.</Typography>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* 검색 후 세부 탭에서 유저의 상세정보 검색 가능.</Typography>
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
            <UserAccount userAccount={userAccount} enableForceLogout={true} />
            {
                userAccount
                    ?
                    <Box className={styles.detailInfoArea} sx={{ mb: '100px' }}>
                        <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                            <Tabs value={tabIndex} onChange={onChangeTab} aria-label="scrollable tabs" scrollButtons="auto" variant='scrollable'>
                                <Tab label="계정**" {...a11yProps(0)} />
                                <Tab label="재화" {...a11yProps(1)} />
                                <Tab label="인벤**" {...a11yProps(2)} />
                                <Tab label="우편" {...a11yProps(3)} />
                                <Tab label="영웅" {...a11yProps(4)} />
                                <Tab label="영웅**" {...a11yProps(5)} />
                                <Tab label="영웅*******" {...a11yProps(6)} />
                                <Tab label="유물" {...a11yProps(7)} />
                                <Tab label="유물*****" {...a11yProps(8)} />
                                <Tab label="특성" {...a11yProps(9)} />
                                <Tab label="무기" {...a11yProps(10)} />
                                <Tab label="도감" {...a11yProps(11)} />
                                <Tab label="펫" {...a11yProps(12)} />
                                <Tab label="펫*****" {...a11yProps(13)} />
                                <Tab label="출석**" {...a11yProps(14)} />
                                <Tab label="칭호" {...a11yProps(15)} />
                                <Tab label="시즌**" {...a11yProps(16)} />
                                <Tab label="시즌**" {...a11yProps(17)} />
                                <Tab label="가이***" {...a11yProps(18)} />
                                <Tab label="프로*" {...a11yProps(19)} />
                                <Tab label="랭킹**" {...a11yProps(20)} />
                                <Tab label="친구" {...a11yProps(21)} />
                                <Tab label="트레***" {...a11yProps(22)} />
                                <Tab label="업적" {...a11yProps(23)} />
                                <Tab label="상점*****" {...a11yProps(24)} />
                                <Tab label="상점" {...a11yProps(25)} />
                                <Tab label="상점*****" {...a11yProps(26)} />
                                <Tab label="상점******" {...a11yProps(27)} />
                                <Tab label="패널티" {...a11yProps(28)} />
                                <Tab label="칭찬" {...a11yProps(29)} />
                                <Tab label="기기**" {...a11yProps(30)} />
                                <Tab label="인스*****" {...a11yProps(31)} />
                                <Tab label="감정**" {...a11yProps(32)} />
                                <Tab label="감정*******" {...a11yProps(33)} />
                                <Tab label="큐브" {...a11yProps(34)} />
                                <Tab label="기록*******" {...a11yProps(35)} />
                                <Tab label="기록*******" {...a11yProps(36)} />
                            </Tabs>
                        </Box>
                        <TabPanel value={tabIndex} index={0}>
                            <Account accounts={accounts} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={1}>
                            <Asset assets={assets} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={2}>
                            <Inventory datas={invenDatas} onTabSearch={onTabSearch} onChangeListCount={onChangePageListCount} />
                            <Paging datas={invenDatas} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={3}>
                            <Mail datas={mailDatas} onChangeListCount={onChangePageListCount} />
                            <Paging datas={mailDatas} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={4}>
                            <Hero heroes={heroes} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={5}>
                            <HeroSkin datas={heroSkinDatas} onTabSearch={onTabSearch} onChangeListCount={onChangePageListCount} />
                            <Paging datas={heroSkinDatas} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={6}>
                            <HeroSkinPreset datas={heroSkinPreset} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={7}>
                            <Artifact datas={artifactDatas} onChangeListCount={onChangePageListCount} />
                            <Paging datas={artifactDatas} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={8}>
                            <ArtifactDeck datas={artifactDeck} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={9}>
                            <RpgAttribute datas={rpgAttributeDatas} onChangeListCount={onChangePageListCount} />
                            <Paging datas={rpgAttributeDatas} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={10}>
                            <WeaponCategory weaponCategories={weaponCategories} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={11}>
                            <Collection datas={collectDatas} onTabSearch={onTabSearch} onChangeListCount={onChangePageListCount} />
                            <Paging datas={collectDatas} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={12}>
                            <Pet datas={petDatas} onTabSearch={onTabSearch} onChangeListCount={onChangePageListCount} />
                            <Paging datas={petDatas} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={13}>
                            <PetIncubation datas={petIncubations} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={14}>
                            <Attendance attendances={attendances} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={15}>
                            <Entitlement entitlements={entitlements} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={16}>
                            <SeasonPass seasonPasses={seasonPasses} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={17}>
                            <SeasonMission datas={seasonMissionDatas} onChangeListCount={onChangePageListCount} />
                            <Paging datas={seasonMissionDatas} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={18}>
                            <GuideMission datas={guideMissionDatas} onChangeListCount={onChangePageListCount} />
                            <Paging datas={guideMissionDatas} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={19}>
                            <Profile profiles={profiles} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={20}>
                            <RankingReward rankingReward={rankingRewadDatas} onChangeListCount={onChangePageListCount} />
                            <Paging datas={rankingRewadDatas} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={21}>
                            <Friends friends={friendsDatas} onChangeListCount={onChangePageListCount} />
                            <Paging datas={friendsDatas} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={22}>
                            <TreasureBox treasures={treasureBox} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={23}>
                            <Achievement datas={achievementDatas} onChangeListCount={onChangePageListCount} />
                            <Paging datas={achievementDatas} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={24}>
                            <EventStore datas={eventStore} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={25}>
                            <WonderStore datas={wonderStore} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={26}>
                            <GlitchStore datas={glitchStore} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={27}>
                            <GoldMedalStore datas={silverMedalStore} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={28}>
                            <Penalty datas={penalties} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={29}>
                            <NicePlayer datas={nicePlayers} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={30}>
                            <UserDevice datas={userDevices} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={31}>
                            <InstantGuide datas={instantGuide} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={32}>
                            <Expression datas={expression} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={33}>
                            <ExpressionPreset datas={expressionPreset} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={34}>
                            <WonderCube datas={wonderCube} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={35}>
                            <PlayRecordGoldClash datas={playRecordGoldClashes} />
                        </TabPanel>
                        <TabPanel value={tabIndex} index={36}>
                            <PlayRecordRpg datas={playRecordRpg} />
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
    } catch (error) {
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