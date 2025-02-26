import { ReactElement, useEffect, useRef, useState, useCallback } from 'react';
import { useAppDispatch, useAppSelector } from "@hooks/index";
import { GetServerSideProps } from 'next';
import { GameAPI } from '@ngeldata/apis/gameAPI';
import * as layoutsActions from '@store/reducers/layouts';
import { Errors } from '@ngel/data/autoErrors';
import { Models } from '@ngel/data/models';
import { useRouter } from 'next/router';;
import dynamic from 'next/dynamic';
import { Defines } from '@ngel/data/autoDefines';
import isEmpty from 'lodash/isEmpty';

const UserAccount = dynamic(() => import('@components/GMS/Users/Search/UserAccount'), { ssr: false });
const UserAccountLink = dynamic(() => import('@components/GMS/Users/Search/Account'), { ssr: false });
const Layout = dynamic(() => import('@components/layouts'), { ssr: false });
const ThemeLayout = dynamic(() => import('@components/layouts/theme'), { ssr: false });
const ManageLayout = dynamic(() => import('@components/layouts/manage'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const UserBlock = dynamic(() => import('@components/GMS/Users/Block/UserBlock'), { ssr: false });
const UserSearch = dynamic(() => import('@components/layouts/ui/uiUserSearch'), { ssr: false });

interface SearchProps {
    searchTypeProp: string;
    searchValueProp: string;
}

function Page({ searchTypeProp, searchValueProp }: SearchProps) {
    const dispatch = useAppDispatch();
    const firstRender = useRef(true);
    const router = useRouter();
    const [searchValue, setSearchValue] = useState<string>();
    const [searchType, setSearchType] = useState<Defines.UserSearchType>(Defines.UserSearchType.AccountID);
    const [userAccount, setUserAccount] = useState<Models.UserAccount | null>(null);
    const [accounts, setAccounts] = useState<Models.Account[]>([]);
    const [userBlocks, setUserBlocks] = useState<Models.UserBlock[]>([]);
    const [refreshCount, setRefreshCount] = useState<number>(0);

    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [firstRender]);
    //#endregion

    const search = useCallback(async () => {
        dispatch(layoutsActions.startLoadingMessage("사용자 정보를 검색 중입니다."));
        setUserAccount(null);
        setUserBlocks([]);

        let searchedUserAccount: Models.UserAccount | null = null;
        let searchedAccounts: Models.Account[] = [];
        let searchedUserBlocks: Models.UserBlock[] = [];

        if (!isEmpty(searchValueProp)) {
            switch (searchTypeProp) {
                case Defines.UserSearchType.AccountID.toString():
                    if (searchValueProp.match(/[^\d]/g) || isNaN(parseInt(searchValueProp))) {
                        alert("AccountID는 숫자로 입력해 주세요");
                        return;
                    }

                    const responseSearchByUID = await GameAPI.UserBlockByUIDAsync({ UID: searchValueProp });

                    if (false == responseSearchByUID.result || null == responseSearchByUID.userAccount) {
                        switch (responseSearchByUID.error) {
                            case Errors.UserBlockByMemberNo_NotFoundUserAccount:
                                alert(`AccountID가 '${searchValueProp}'인 사용자는 없습니다.`);
                                break;

                            case Errors.UserBlockByMemberNo_NotFoundAccount:
                                alert(`AccountID가 '${searchValueProp}'인 사용자 계정 연결정보가 없습니다.`);
                                break;

                            case Errors.UserBlockByUID_NotFoundMemberNo:
                                alert(`AccountID가 '${searchValueProp}'인 사용자의 스토브 계정 정보가 없습니다.`);
                                break;

                            default:
                                alert(`사용자 검색 실패. (error:${Errors[responseSearchByUID.error]})`);
                        }
                        dispatch(layoutsActions.stopLoading());
                        return;
                    }

                    searchedUserAccount = responseSearchByUID.userAccount;
                    searchedAccounts = responseSearchByUID.accounts;
                    searchedUserBlocks = responseSearchByUID.userBlocks;
                    break;

                case Defines.UserSearchType.Nick.toString():
                    const responseSearchByNick = await GameAPI.UserBlockByNickAsync({ Nick: searchValueProp });

                    if (false == responseSearchByNick.result || null == responseSearchByNick.userAccount) {
                        switch (responseSearchByNick.error) {
                            case Errors.UserBlockByNick_NotFoundUserAccount:
                                alert(`닉네임이 '${searchValueProp}'인 사용자는 없습니다.`);
                                break;

                            case Errors.UserBlockByNick_NotFoundAccount:
                                alert(`닉네임이 '${searchValueProp}'인 사용자 계정 연결정보가 없습니다.`);
                                break;

                            case Errors.UserBlockByNick_NotFoundMemberNo:
                                alert(`닉네임이 '${searchValueProp}'인 사용자의 스토브 계정 정보가 없습니다.`);
                                break;

                            default:
                                alert(`사용자 검색 실패. (error:${Errors[responseSearchByNick.error]})`);
                        }
                        dispatch(layoutsActions.stopLoading());
                        return;
                    }

                    searchedUserAccount = responseSearchByNick.userAccount;
                    searchedAccounts = responseSearchByNick.accounts;
                    searchedUserBlocks = responseSearchByNick.userBlocks;
                    break;

                case Defines.UserSearchType.StoveMemberNo.toString():
                    if (searchValueProp.match(/[^\d]/g) || isNaN(parseInt(searchValueProp))) {
                        alert("StoveMemberNo는 숫자로 입력해 주세요");
                        return;
                    }

                    const responseSearchByMemberNo = await GameAPI.UserBlockByMemberNoAsync({ MemberNo: searchValueProp });

                    if (false == responseSearchByMemberNo.result || null == responseSearchByMemberNo.userAccount) {
                        switch (responseSearchByMemberNo.error) {
                            case Errors.UserBlockByMemberNo_NotFoundUserAccount:
                                alert(`StoveMemberNo가 '${searchValueProp}'인 사용자는 없습니다.`);
                                break;

                            case Errors.UserBlockByMemberNo_NotFoundAccount:
                                alert(`StoveMemberNo가 '${searchValueProp}'인 사용자의 계정 연결정보가 없습니다.`);
                                break;

                            case Errors.UserBlockByMemberNo_NotFoundMemberNo:
                                alert(`StoveMemberNo가 '${searchValueProp}'인 사용자의 스토브 계정 정보가 없습니다.`);
                                break;

                            default:
                                alert(`사용자 검색 실패. (error:${Errors[responseSearchByMemberNo.error]})`);
                        }
                        dispatch(layoutsActions.stopLoading());
                        return;
                    }

                    searchedUserAccount = responseSearchByMemberNo.userAccount;
                    searchedAccounts = responseSearchByMemberNo.accounts;
                    searchedUserBlocks = responseSearchByMemberNo.userBlocks;
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
        setAccounts(searchedAccounts);
        searchedUserBlocks = searchedUserBlocks === null ? [] : searchedUserBlocks;
        setUserBlocks(searchedUserBlocks);

        dispatch(layoutsActions.stopLoading());

    }, [dispatch, searchTypeProp, searchValueProp, setUserAccount, setAccounts, setUserBlocks,]);

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
        router.push(`${router.pathname}?searchType=${(type ?? 0)}&searchValue=${value}`);

        if (value == searchValueProp)
            setRefreshCount(prev => prev + 1);
    }, [router, searchValueProp]);

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

    return (
        <Box sx={{ pt: 3, pb: 3 }}>
            <Box>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* AccountID는 숫자로 검색하면 됩니다.</Typography>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* 검색 후 유저의 블럭상태 설정 가능.</Typography>
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
                userAccount && accounts
                    ?
                    <>
                        <UserAccountLink accounts={accounts} />
                        {
                            0 < accounts.length && 0 < accounts[0].MemberNo
                                ?
                                <UserBlock userAccount={accounts[0]} datas={userBlocks} onSearch={onSearch} />
                                :
                                <Box>
                                    <Typography sx={{ textAlign: "center", pt: 5 }}>스토브 계정 정보가 없습니다.</Typography>
                                </Box>
                        }
                        <Box sx={{ height: '100px' }} />
                    </>
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
    const searchType: string = query.searchType ? query.searchType.toString() : "1";
    const searchValue: string = query.searchValue ? query.searchValue.toLocaleString() : "";

    return {
        props: {
            searchTypeProp: searchType,
            searchValueProp: searchValue
        }
    };
}

export default Page;