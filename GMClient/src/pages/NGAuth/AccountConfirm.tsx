import { ReactElement, createRef, useEffect, useRef, useState, useCallback, ChangeEvent } from 'react';
import { useAppDispatch, useAppSelector } from "@hooks/index";
import { GetServerSideProps } from 'next';
import dynamic from 'next/dynamic';
import { OAuthAPI } from '@ngel/data/apis/oauthAPI';
import { OAuthAPIModels } from '@ngel/data/models/oauthAPIModels';
import { Helpers } from 'src/helpers/index';
import { Errors } from 'src/ngel/data/autoErrors';
import isEmpty from 'lodash/isEmpty';

const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Layout = dynamic(() => import('@components/layouts'), { ssr: false });
const ThemeLayout = dynamic(() => import('@components/layouts/theme'), { ssr: false });
const ConfirmLayout = dynamic(() => import('@components/layouts/confirm'), { ssr: false });
const KeyIcon = dynamic(() => import('@mui/icons-material/Key'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const TextField = dynamic(() => import('@mui/material/TextField'), { ssr: false });
const InputAdornment = dynamic(() => import('@mui/material/InputAdornment'), { ssr: false });

interface AccountConfirmProps {
    id: string;
}

function Page({ id }: AccountConfirmProps) {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const layouts = useAppSelector(state => state.layouts);
    const dispatch = useAppDispatch();
    const [marginTop, setMarginTop] = useState<number>(8);
    const [email, setEmail] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const passwordInput = createRef<HTMLDivElement>();
    const firstRender = useRef(true);

    useEffect(() => {
        if (firstRender.current) {
            try {
                const parameters = { id: id, requestURL: "/NGAuth/AccountConfirm" };
                const encodedDataString = Helpers.encodeMessagePack(parameters);
                const responsePromise = fetch((Helpers.getCookie('API_HOST_NAME') ?? "") + "/NGMS/OAuth/CheckConfirmEmail", {
                    method: 'POST',
                    headers: { 'Content-Type': 'text/plain' },
                    body: encodedDataString
                });

                responsePromise
                    .then(async (response) => {
                        const messagePack = await response.text();
                        const result = Helpers.decodeMessagePack<OAuthAPIModels.OAuthCheckConfirmEmailResponses>(messagePack);
                        if (result && result.user && false == isEmpty(result.user.email))
                            setEmail(result.user.email);
                    })
                    .catch(error => {
                        console.log(error);
                    });
                
            } catch (error) {
                console.log(error);
            }
        }
            
    }, [firstRender, setEmail, id]);

    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;
            
    }, [firstRender]);
    //#endregion

    const onConfirm = useCallback(async () => {
        if (!password) {
            alert('비밀번호를 입력해주세요.');
            passwordInput.current?.focus();
            return;
        }

        if (8 > password.length) {
            alert('비밀번호는 8글자 이상으로 입력해 주세요.');
            return;
        }

        if (!password.match(/[\d]+/g) || !password.match(/[a-zA-Z]+/g) || !password.match(/[\!\@\@\#\$\%\^\&\*\(\)\-_\=\+\,\\.\/\<\>\?\[\{\]\}\|]+/g)) {
            alert('비밀번호는 영문자, 숫자, 특수문자를 포함한 조합으로 입력해 주세요.');
            return;
        }

        var response = new OAuthAPIModels.OAuthInitEmailPasswordResponses();
        try {
            response = await OAuthAPI.InitEmailPasswordAsync({ id: id, password: password });
        } catch (error) {
            console.log(error);
        }

        if (response.result) {
            alert("비밀번호를 초기화했습니다.");
            document.location.href = `/NGAuth`;
        } else {
            switch (response.error) {
                case Errors.InitEmailPassword_AlreadyUsedPassword:
                    alert(`한번 사용한 적이 있는 비밀번호입니다.\n다른 비밀번호를 사용해 주세요.(error:${response.error})`);
                    break;

                default:
                    alert(`인증실패(error:${response.error})`);
            }
        }
    }, [password, passwordInput, id]);

    const onChangePassword = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>) => {
        setPassword(e.target.value);
    }, [setPassword]);

    const onEnter = useCallback(async (e) => {
        if ('Enter' == e.key)
            await onConfirm();
    }, [onConfirm]);
    
    return (
        isEmpty(email)
            ?
                <></>
            :
                <>
                    <Box sx={{ marginTop: marginTop, display: 'flex', flexDirection: 'column', alignItems: 'center', }} >
                        <Box sx={{ mt: 1, textAlign: 'center' }}>
                            <Typography variant='h4'>비밀번호 초기화</Typography>
                            <Grid container sx={{ padding: "15px 0", fontSize: "16px", color: "#6f6f6f", textAlign: "center" }}>
                                <Grid item xs={12} sx={{ padding: "10px 20px" }}>
                                    <Typography>{email}</Typography>
                                </Grid>
                                <Grid item xs={12} sx={{ padding: "10px 20px" }}>
                                    <TextField 
                                        inputRef={passwordInput}
                                        InputProps={{
                                            startAdornment: <InputAdornment position="start"><KeyIcon /></InputAdornment>,
                                        }}
                                        id="password"
                                        label="password"
                                        type="password"
                                        onChange={(e) => onChangePassword(e)}
                                        onKeyUp={async (e) => await onEnter(e)}
                                    />
                                </Grid>
                                <Grid item xs={12} sx={{ padding: "10px 20px" }}>
                                    <Button type="button" color="inherit" variant="outlined" sx={{ mt: 3, mb: 2, fontWeight: 600 }} onClick={async () => await onConfirm()}>초기화</Button>
                                </Grid>
                            </Grid>
                        </Box>
                    </Box>
                </>
    );
};

Page.getLayout = function getLayout(page: ReactElement) {
    return (
        <Layout>
            <ThemeLayout>
                <ConfirmLayout>{page}</ConfirmLayout>
            </ThemeLayout>
        </Layout>
    );
}

export const getServerSideProps: GetServerSideProps = async (context) => {
    const { req, query } = context;
    if (!query.id)
        return { notFound: true };

    const id = query.id ? query.id.toString() : "";
        
    return {
        props: { id: id }
    };
}

export default Page;