import { useEffect, useRef } from 'react'
import dynamic from 'next/dynamic';
import { Card, Paper } from '@mui/material';
import styles from '@styles/layouts/leftBar.module.sass';

const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const CardContent = dynamic(() => import('@mui/material/CardContent'), { ssr: false });
const Divider = dynamic(() => import('@mui/material/Divider'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });

interface IntroProps {
    devEnv: string;
}

function Intro({ devEnv }: IntroProps) {
    const firstRender = useRef(true);
    
    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;
            
    }, [ firstRender ]);
    //#endregion
    
    return (
        devEnv === process.env.NODE_ENV
            ?
                <></>
            :
                <>
                    <h1>샘플 운영툴 안내</h1>
                    <Divider variant="middle" sx={{ m: 0 }} />
                    <Box sx={{ pt: 3, pb: 3 }}>
                        <Box sx={{ display: 'flex', flexDirection: 'column', justifyContent: 'center' }}>
                            <Grid container spacing={4}>
                                <Grid item xs={12}>
                                    <Card variant="outlined" component={Paper} elevation={4}>
                                        <CardContent sx={{ paddingBottom: '16px !important' }}>
                                            <Typography variant="body1" sx={{ borderLeft: '4px solid #1976d2', pl: 2, lineHeight: 1, mb: 0 }} display="block" gutterBottom><strong>좌측 메뉴</strong>를 이용해 주세요.</Typography>
                                        </CardContent>
                                    </Card>
                                </Grid>
                                <Grid item xs={12}>
                                    <Card variant="outlined" component={Paper} elevation={4}>
                                        <CardContent sx={{ paddingBottom: '16px !important' }}>
                                            <Typography variant='h6' sx={{ borderLeft: '4px solid #1976d2', pl: 2, lineHeight: 1 }} fontWeight={600} display="block">
                                                당부의 글
                                            </Typography>
                                            <Divider variant="middle" sx={{ mt: 1, mb: 1, ml: 0, mr: 0 }} />
                                            <Typography variant="body1" display="block" gutterBottom>○ 샘플 운영툴은 게임운영 <strong>기능</strong>을 간략하게 <strong>보여주기 위한</strong> 프로그램입니다.</Typography>
                                            <Typography variant="body1" display="block" gutterBottom sx={{ mb: 0 }}>○ 출력되는 <strong>게임 데이터</strong>들은 회사 프로젝트 보안상 <strong>대체문자로 치환했습니다.</strong></Typography>
                                        </CardContent>
                                    </Card>
                                </Grid>
                                <Grid item xs={12} sm={3}>
                                    <Card variant="outlined" component={Paper} elevation={4}>
                                        <CardContent sx={{ paddingBottom: '16px !important' }}>
                                            <Typography variant='h6' sx={{ borderLeft: '4px solid #1976d2', pl: 2, lineHeight: 1 }} fontWeight={600} display="block">언어</Typography>
                                            <Divider variant="middle" sx={{ mt: 1, mb: 1, ml: 0, mr: 0 }} />
                                            <Typography variant="body1" display="block" gutterBottom sx={{ mb: 0 }}>샘플 운영툴은 <strong>.NET Core로 백엔드 서버</strong>를, <strong>TypeScript Next.js로 클라이언트</strong>를 제작했습니다.</Typography>
                                        </CardContent>
                                    </Card>
                                </Grid>
                                <Grid item xs={12} sm={3}>
                                    <Card variant="outlined" component={Paper} elevation={4}>
                                        <CardContent sx={{ paddingBottom: '16px !important' }}>
                                            <Typography variant='h6' sx={{ borderLeft: '4px solid #1976d2', pl: 2, lineHeight: 1 }} fontWeight={600} display="block">통신(API/WebSocket)</Typography>
                                            <Divider variant="middle" sx={{ mt: 1, mb: 1, ml: 0, mr: 0 }} />
                                            <Typography variant="body1" display="block" gutterBottom sx={{ mb: 0 }}>운영툴 서버와 운영툴 클라이언트는 <strong>API</strong> 및 <strong>SignalR WebSocket</strong>을 이용해 <strong>통신</strong>합니다.</Typography>
                                        </CardContent>
                                    </Card>
                                </Grid>
                                <Grid item xs={12} sm={3}>
                                    <Card variant="outlined" component={Paper} elevation={4}>
                                        <CardContent sx={{ paddingBottom: '16px !important' }}>
                                            <Typography variant='h6' sx={{ borderLeft: '4px solid #1976d2', pl: 2, lineHeight: 1 }} fontWeight={600} display="block">STOMP</Typography>
                                            <Divider variant="middle" sx={{ mt: 1, mb: 1, ml: 0, mr: 0 }} />
                                            <Typography variant="body1" display="block" gutterBottom sx={{ mb: 0 }}>운영툴 서버와 게임서버는 <strong>STOMP</strong>를 이용해 <strong>실시간</strong>으로 <strong>데이터를 주고 받습니다.</strong></Typography>
                                        </CardContent>
                                    </Card>
                                </Grid>
                                <Grid item xs={12} sm={3}>
                                    <Card variant="outlined" component={Paper} elevation={4}>
                                        <CardContent sx={{ paddingBottom: '16px !important' }}>
                                            <Typography variant='h6' sx={{ borderLeft: '4px solid #1976d2', pl: 2, lineHeight: 1 }} fontWeight={600} display="block">MariaDB</Typography>
                                            <Divider variant="middle" sx={{ mt: 1, mb: 1, ml: 0, mr: 0 }} />
                                            <Typography variant="body1" display="block" gutterBottom sx={{ mb: 0 }}>운영 및 게임 데이터를 저장하는 <strong>RDBMS</strong>는 <strong>MariaDB</strong>를 사용했습니다.</Typography>
                                        </CardContent>
                                    </Card>
                                </Grid>
                                <Grid item xs={12} sm={3}>
                                    <Card variant="outlined" component={Paper} elevation={4}>
                                        <CardContent sx={{ paddingBottom: '16px !important' }}>
                                            <Typography variant='h6' sx={{ borderLeft: '4px solid #1976d2', pl: 2, lineHeight: 1 }} fontWeight={600} display="block">Stored Procedure</Typography>
                                            <Divider variant="middle" sx={{ mt: 1, mb: 1, ml: 0, mr: 0 }} />
                                            <Typography variant="body1" display="block" gutterBottom sx={{ mb: 0 }}>RDB 요청 횟수, 전송 파라미터, 쿼리문을 <strong>최소화/최적화</strong> 하기위해 <strong>Store Procedure</strong>를 적극 활용했습니다.</Typography>
                                        </CardContent>
                                    </Card>
                                </Grid>
                                <Grid item xs={12} sm={3}>
                                    <Card variant="outlined" component={Paper} elevation={4}>
                                        <CardContent sx={{ paddingBottom: '16px !important' }}>
                                            <Typography variant='h6' sx={{ borderLeft: '4px solid #1976d2', pl: 2, lineHeight: 1 }} fontWeight={600} display="block">Redis</Typography>
                                            <Divider variant="middle" sx={{ mt: 1, mb: 1, ml: 0, mr: 0 }} />
                                            <Typography variant="body1" display="block" gutterBottom sx={{ mb: 0 }}><strong>Redis</strong>를 통해 각 서비스 사이에 <strong>캐시 데이터</strong>를 공유, 데이터 공유 <strong>성능과 편의성</strong>을 높였습니다.</Typography>
                                        </CardContent>
                                    </Card>
                                </Grid>
                                <Grid item xs={12} sm={3}>
                                    <Card variant="outlined" component={Paper} elevation={4}>
                                        <CardContent sx={{ paddingBottom: '16px !important' }}>
                                            <Typography variant='h6' sx={{ borderLeft: '4px solid #1976d2', pl: 2, lineHeight: 1 }} fontWeight={600} display="block">중복 로그인 방지</Typography>
                                            <Divider variant="middle" sx={{ mt: 1, mb: 1, ml: 0, mr: 0 }} />
                                            <Typography variant="body1" display="block" gutterBottom sx={{ mb: 0 }}><strong>같은 관리자 계정</strong>으로 <strong>동시에 로그인 할 수 없습니다.</strong></Typography>
                                        </CardContent>
                                    </Card>
                                </Grid>
                            </Grid>
                            <Divider variant="middle" sx={{ mt: 2, mb: 2, ml: 0, mr: 0 }} />
                            <Grid className={styles.leftBarBottom} container spacing={4}>
                                <Grid item xs={12}>
                                    <Card variant="outlined" component={Paper} elevation={4}>
                                        <CardContent sx={{ paddingBottom: '16px !important' }}>
                                            <Typography variant='h6' sx={{ borderLeft: '4px solid #1976d2', pl: 2, lineHeight: 1 }} fontWeight={600} display="block">Made by <strong>Bae JangHo</strong></Typography>
                                            <Divider variant="middle" sx={{ mt: 1, mb: 1, ml: 0, mr: 0 }} />
                                            <Typography variant="body2" display="block" gutterBottom><a href="mailto:zanghobae@gmail.com" title="배장호에게 메일 발송"><strong>zanghobae@gmail.com</strong></a></Typography>
                                            <Typography variant="body2" display="block" gutterBottom><a href="tel:010-2070-4274" title="배장호에게 전화 걸기"><strong>010-2070-4274</strong></a></Typography>
                                        </CardContent>
                                    </Card>
                                </Grid>
                            </Grid>
                        </Box>
                    </Box>
                </>
    );
};

export default Intro;