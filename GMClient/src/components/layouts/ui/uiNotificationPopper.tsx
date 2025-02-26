import { useCallback, useEffect, useRef, ReactElement, Fragment } from 'react';
import { useAppDispatch, useAppSelector } from '@hooks/index';
import * as layoutActions from '@store/reducers/layouts';
import * as userActions from '@store/reducers/user';
import * as homepageSettingActions from '@store/reducers/settings/hompageSettings';
import dynamic from 'next/dynamic';
import { Defines } from '@ngel/data/autoDefines';
import { dayjs } from '@helpers/localizedDayjs';
import styles from '@styles/ui/notification.module.sass';

const Close = dynamic(() => import('@mui/icons-material/Close'), { ssr: false });
const Refresh = dynamic(() => import('@mui/icons-material/Refresh'), { ssr: false });
const DeleteOutline = dynamic(() => import('@mui/icons-material/DeleteOutline'), { ssr: false });
const NotificationsNoneIcon = dynamic(() => import('@mui/icons-material/NotificationsNone'), { ssr: false });
const WorkOutlineIcon = dynamic(() => import('@mui/icons-material/WorkOutline'), { ssr: false });
const ExpandLess = dynamic(() => import('@mui/icons-material/ExpandLess'), { ssr: false });
const ExpandMore = dynamic(() => import('@mui/icons-material/ExpandMore'), { ssr: false });
const ErrorOutline = dynamic(() => import('@mui/icons-material/ErrorOutline'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const ListItem = dynamic(() => import('@mui/material/ListItem'), { ssr: false });
const ListItemButton = dynamic(() => import('@mui/material/ListItemButton'), { ssr: false });
const Fade = dynamic(() => import('@mui/material/Fade'), { ssr: false });
const Collapse = dynamic(() => import('@mui/material/Collapse'), { ssr: false });
const ListItemIcon = dynamic(() => import('@mui/material/ListItemIcon'), { ssr: false });
const ListItemText = dynamic(() => import('@mui/material/ListItemText'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const Divider = dynamic(() => import('@mui/material/Divider'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Chip = dynamic(() => import('@mui/material/Chip'), { ssr: false });
const ClickAwayListener = dynamic(() => import('@mui/material/ClickAwayListener'), { ssr: false });

import List from '@mui/material/List';
import Button from '@mui/material/Button';
import Popper from '@mui/material/Popper';
import Paper from '@mui/material/Paper';
import isEmpty from 'lodash/isEmpty';

function UINotificationPopper() {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const layouts = useAppSelector(state => state.layouts);
    const user = useAppSelector(state => state.user);
    const dispatch = useAppDispatch();
    const firstRender = useRef(true);
    const notificationButton = useRef<HTMLButtonElement|null>(null);

    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;
            
    }, [firstRender]);

    useEffect(() => {
        if (false == firstRender.current && homepageSettings.existsUpdate && false == homepageSettings.notifyUpdateDone) {
            dispatch(layoutActions.openNotification());
        }
            
    }, [firstRender, homepageSettings, dispatch]);

    const toggleJobMessage = useCallback((id: string) => {
        if (isEmpty(id))
            return;

        dispatch(userActions.toggleJobMessage(id));
    }, [dispatch]);

    const removeJob = useCallback((id: string) => {
        if (isEmpty(id))
            return;

        dispatch(userActions.removeJob(id));
    }, [dispatch]);

    const jobList = useCallback(() => {
        if (user.jobs && 0 < user.jobs.length) {
            const list: ReactElement[] = [];
            for (let i = (user.jobs.length - 1); i >= 0 ; i--) {
                const job = user.jobs[i];
                if (null == job)
                    continue;

                let jobName = '진행중인 작업';
                switch (job.jobType) {
                    case Defines.UserJobType.SendImmdGameMail:
                        jobName = `우편 즉시발송 (${(dayjs(job.startTime).tz().format('YYYY-MM-DD HH:mm:mm'))})`;
                        break;
                }
                let jobResult = (
                    <>
                        {0 < job.succeededCount ? <Chip label={`성공: ${job.succeededCount}`} size='small' color='success' variant='outlined' sx={{ fontSize: '0.8em', fontWeight: 700, marginRight: '5px' }} /> : <></>}
                        {0 < job.failedCount ? <Chip label={`실패: ${job.failedCount}`} size='small' color='error' variant='outlined' sx={{ fontSize: '0.8em', fontWeight: 700, marginRight: '5px' }} /> : <></>}
                        {0 < job.jobCount ? <Chip label={`전체: ${job.jobCount}`} size='small' color='default' variant='outlined' sx={{ fontSize: '0.8em', fontWeight: 700 }} /> : <></>}
                    </>
                );

                list.push(
                    <Fragment key={i}>
                        <ListItemButton sx={{ p: 2 }} onClick={e => toggleJobMessage(job.id)}>
                            <ListItemIcon>
                                {
                                    job.jobCount == (job.succeededCount + job.failedCount) ? <Button sx={{ padding: 0, minWidth: 10 }} size='small' color='inherit' onClick={e => removeJob(job.id)}><DeleteOutline /></Button> : <WorkOutlineIcon />
                                }
                            </ListItemIcon>
                            <ListItemText>
                                <Typography sx={{ fontSize: '0.8em', fontWeight: 700 }}>{jobName}</Typography>
                                <Typography sx={{ fontSize: '0.8em', fontWeight: 700 }}>{jobResult}</Typography>
                            </ListItemText>
                            {isEmpty(job.message) ? <></> : (job.isOpenMessage ? <ExpandLess /> : <ExpandMore />)}
                        </ListItemButton>
                        {
                            isEmpty(job.message)
                                ?
                                    <></>
                                :
                                    <Collapse in={job.isOpenMessage} timeout="auto" unmountOnExit>
                                        <List component="div" disablePadding>
                                            <ListItemButton sx={{ pl: 4 }}>
                                                <ListItemIcon>
                                                    <ErrorOutline />
                                                </ListItemIcon>
                                                <ListItemText sx={{maxHeight: 300, overflowY: 'auto'}}><Typography sx={{fontSize: '0.8em'}}>{job.message}</Typography></ListItemText>
                                            </ListItemButton>
                                        </List>
                                    </Collapse>
                        }
                    </Fragment>
                );
            }
            return list;
        } else if (false == homepageSettings.existsUpdate) {
            return (
                <ListItem>
                    <Typography sx={{ width: '100%', textAlign: 'center' }}>알림 내용이 없습니다.</Typography>
                </ListItem>
            );
        }
    }, [user, homepageSettings, toggleJobMessage, removeJob]);

    const notificationUpdate = useCallback(() => {
        if (homepageSettings.existsUpdate) {
            return (
                <ListItemButton sx={{ p: 2 }}>
                    <ListItemIcon>
                        <Button sx={{ padding: 0, minWidth: 10 }} size='small' color='inherit' onClick={e => location.reload()}><Refresh /></Button>
                    </ListItemIcon>
                    <ListItemText>
                        <Typography sx={{ fontSize: '0.8em', fontWeight: 700 }}>운영툴이 업데이트 됐습니다.</Typography>
                        <Typography sx={{ fontSize: '0.8em', fontWeight: 700 }}>브라우저를 새로고침(F5) 해주세요.</Typography>
                    </ListItemText>
                </ListItemButton>
            );
        } else {
            return <></>;
        }
    }, [homepageSettings]);

    const onClickNotification = useCallback(async () => {
        dispatch(layoutActions.toggleNotification());
    }, [dispatch]);

    const onCloseNotification = useCallback(async (e) => {
        dispatch(layoutActions.closeNotification());
        dispatch(homepageSettingActions.setNotifyUpdateDone(true));
    }, [dispatch]);

    const notification = useCallback(() => {
        return (
            <ClickAwayListener onClickAway={e => onCloseNotification(e)}>
                <Box>
                    <Button id="toggleNotification" color="inherit" sx={{ marginLeft: '12px', minWidth: 0 }} ref={notificationButton} onClick={onClickNotification}>
                        <NotificationsNoneIcon />
                    </Button>
                    <Popper
                        className={styles.notificationPopper} 
                        open={layouts.notificationActive} 
                        anchorEl={notificationButton.current} 
                        placement='bottom-end' 
                        transition>
                        {({ TransitionProps }) => (
                            <Fade {...TransitionProps} timeout={350}>
                                <Paper elevation={4}>
                                    <Grid container sx={{ padding: '5px 10px 0'}}>
                                        <Grid item xs={11}><Typography sx={{ fontSize: '14px', fontWeight: 700 }}>알림</Typography></Grid>
                                        <Grid item xs={1} sx={{ display: 'flex', alignItems: 'top' }}>
                                            <Button id="closeNotification" sx={{ minWidth: 1, padding: 0, height: '18px' }} color='error' onClick={onCloseNotification}>
                                                <Close sx={{ width: '18px', height: '18px' }} />
                                            </Button>
                                        </Grid>
                                    </Grid>
                                    <Divider />
                                    <List>
                                        {notificationUpdate()}
                                        {jobList()}
                                    </List>
                                </Paper>
                            </Fade>
                        )}
                    </Popper>
                </Box>
            </ClickAwayListener>
        );
    }, [notificationUpdate, jobList, layouts, notificationButton, onClickNotification, onCloseNotification]);

    return notification();
}

export default UINotificationPopper;