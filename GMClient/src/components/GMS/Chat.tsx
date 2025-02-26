import { KeyboardEvent, useCallback, useEffect, useRef, useState, createRef } from 'react'
import dynamic from 'next/dynamic';
import { useAppDispatch, useAppSelector } from '@hooks/index';
import * as GMServerHubActions from '@ngeldata/hubs/GMServer/reducer';
import { Models } from '@ngel/data/models';
import { v4 as uuid } from 'uuid';
import { dayjs } from '@helpers/localizedDayjs';
import { Defines } from '@ngel/data/autoDefines';
import deepmerge from 'deepmerge';
import styles from '@styles/pages/GMS/chat.module.sass';

import { Grid } from '@mui/material';
const SendIcon = dynamic(() => import('@mui/icons-material/Send'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Card = dynamic(() => import('@mui/material/Card'), { ssr: false });
const CardContent = dynamic(() => import('@mui/material/CardContent'), { ssr: false });
const Divider = dynamic(() => import('@mui/material/Divider'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const TextField = dynamic(() => import('@mui/material/TextField'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });

interface ChatProps {
    devEnv: string;
}

function Chat({ devEnv }: ChatProps) {
    const firstRender = useRef(true);
    const chattingMessageListRef = createRef<HTMLDivElement>();
    const user = useAppSelector(state => state.user);
    const GMServerHub = useAppSelector(state => state.GMServerHub);
    const chattings = useAppSelector(state => state.chattings);
    const dispatch = useAppDispatch();
    const [inputMessage, setInputMessage] = useState("");

    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [firstRender]);
    //#endregion

    const changeInputMessage = useCallback((e) => {
        setInputMessage(e.target.value);
    }, [setInputMessage]);

    const sendMessage = useCallback(() => {
        const sendMessage: Models.ChattingMessage = new Models.ChattingMessage();
        sendMessage.id = uuid();
        sendMessage.message = inputMessage;
        sendMessage.localSendTime = dayjs().utc().toDate();

        dispatch(GMServerHubActions.sendChattingMessageReq({ chattingMessage: sendMessage }));
        setInputMessage("");
    }, [inputMessage, dispatch, setInputMessage]);

    const onKeyUpMessageInput = useCallback((e: KeyboardEvent) => {
        if (e.ctrlKey && e.key === 'Enter') {
            setInputMessage(inputMessage + '\n');
        } else if (e.key === 'Enter') {
            sendMessage();
        }
    }, [sendMessage, setInputMessage, inputMessage]);

    useEffect(() => {
        if (false == firstRender.current) {
            if (chattingMessageListRef.current?.scrollHeight)
                chattingMessageListRef.current.scrollTop = chattingMessageListRef.current.scrollHeight;
        }

    }, [firstRender, chattings, chattingMessageListRef]);

    const contents = useCallback(() => {
        return (
            <Box sx={{ marginTop: 3 }}>
                <Typography variant="h5">{ devEnv === 'development' ? '' : '운영툴 채팅' }</Typography>
                <Divider variant="middle" sx={{ m: 0, mt: 2, mb: 2 }} />
                <Card>
                    <CardContent>
                        <Grid container direction="column" spacing={2} sx={{ margin: 0, width: '100%' }}>
                            <Grid item ref={chattingMessageListRef} className={styles.chattingMessageList}>
                                {chattings.chattingList.map((message: Models.ChattingMessage, index) => {
                                    let messageWrapperClass = styles.messageWrapper;
                                    let messageTypeClass = styles.messageType;

                                    switch (message.messageType) {
                                        case Defines.ChattingMessageType.Normal:
                                            messageTypeClass += ' ' + styles.messageTypeNormal;
                                            if (message.sendSigninId == user.signinId) {
                                                messageWrapperClass += ' ' + styles.messageWrapperMine;
                                            } else {
                                                messageWrapperClass += ' ' + styles.messageWrapperTheir;
                                            }
                                            break;

                                        case Defines.ChattingMessageType.Notice:
                                            messageTypeClass += ' ' + styles.messageTypeNotice;
                                            break;
                                    }

                                    return (
                                        <Box key={index}>
                                            {
                                                Defines.ChattingMessageType.Notice == message.messageType || message.sendSigninId == user.signinId
                                                    ?
                                                    <></>
                                                    :
                                                    <Paper className={styles.sendUserNameWrapper}>
                                                        <Typography className={styles.sendUserName}>
                                                            {message.sendUserName}
                                                        </Typography>
                                                    </Paper>
                                            }
                                            <Paper className={messageWrapperClass}>
                                                <Typography className={messageTypeClass}>
                                                    {message.message}
                                                </Typography>
                                                {
                                                    Defines.ChattingMessageType.Notice == message.messageType
                                                        ?
                                                        <></>
                                                        :
                                                        <Typography className={styles.messageTime}>
                                                            {dayjs(message.serverSendTime).fromNow(true)}
                                                        </Typography>
                                                }
                                            </Paper>
                                        </Box>
                                    );
                                })}
                                {
                                    chattings.chattingList.length === 0
                                        ?
                                        <Paper>
                                            <Typography sx={{
                                                padding: '12px',
                                                whiteSpace: 'pre-wrap',
                                                marginBottom: '12px',
                                                textAlign: 'center'
                                            }}>
                                                대화 내용이 없습니다.
                                            </Typography>
                                        </Paper>
                                        :
                                        <></>
                                }
                            </Grid>
                            <Grid item sx={{
                                display: 'flex',
                                marginTop: '12px',
                                marginLeft: 0
                            }}>
                                <TextField
                                    id="inputMessage"
                                    label="메세지 입력"
                                    variant="outlined"
                                    size='small'
                                    value={inputMessage}
                                    onKeyUp={(e) => onKeyUpMessageInput(e)}
                                    onChange={(e) => changeInputMessage(e)}
                                    multiline
                                    sx={{
                                        flex: "1 1 auto",
                                        "& div": {
                                            borderRadius: '4px 0 0 4px',
                                            borderRight: '0px !important'
                                        },
                                        "&:hover": {
                                            borderRight: '0px !important'
                                        }
                                    }}
                                />
                                <Button
                                    onClick={sendMessage}
                                    variant="contained"
                                    endIcon={<SendIcon />}
                                    sx={{
                                        borderRadius: '0 4px 4px 0'
                                    }}
                                >
                                </Button>
                            </Grid>
                        </Grid>
                    </CardContent>
                </Card>
            </Box>
        );
    }, [user, chattings, chattingMessageListRef, inputMessage, changeInputMessage, sendMessage, onKeyUpMessageInput, devEnv]);

    return contents();
};

export default Chat;