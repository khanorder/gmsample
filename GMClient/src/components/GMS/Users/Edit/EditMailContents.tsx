import { useAppSelector } from '@hooks/index';
import { ReactElement, MutableRefObject, useCallback, useState, useEffect } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { Defines } from '@ngel/data/autoDefines';
import deepmerge from 'deepmerge';
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const List = dynamic(() => import('@mui/material/List'), { ssr: false });
const ListItem = dynamic(() => import('@mui/material/ListItem'), { ssr: false });
const ListItemText = dynamic(() => import('@mui/material/ListItemText'), { ssr: false });

const MinWidthTableCell = styled(TableCell)(({ theme }) => ({
    minWidth: "120px"
}));

interface ContentsProps {
    userAccount: Models.UserAccount;
    datas: PaginatedList<Models.Mail>;
    tabChanged: MutableRefObject<boolean>;
    deleteList: MutableRefObject<Models.Mail[]>
}

const EditMailContents = ({ userAccount, datas, tabChanged, deleteList }: ContentsProps): ReactElement => {
    let content: ReactElement = <></>;
    const tables = useAppSelector(state => state.tables);
    const itemTable = tables.itemTable;
    const [mails, setMails] = useState<Models.Mail[]>([]);

    useEffect(()=> {
        setMails(prev => deepmerge([], datas.items));

        //페이지 이동 시 삭제리스트 초기화
        if(0 < deleteList.current.length)
            deleteList.current = [];
    }, [datas, deleteList]);
    
    const onDelete = useCallback((e, mail:Models.Mail, index: number) => {
        const deleteData = mails[index];

        if (!deleteData) {
            alert(`${index + 1}번째 데이터가 없습니다.`);
            return;
        }

        setMails(prev => {
            if (prev[index]) {
                prev.splice(index, 1);
            }
            return deepmerge([], prev);
        });
        deleteList.current.push(mail);
        tabChanged.current = true;
        
    }, [mails, deleteList, tabChanged]);

    const contents = useCallback(() => {
        let result: ReactElement = <></>;

        if (mails && 0 < mails.length) {
            const list: ReactElement[] = [];

            const isSigned = userAccount.IsSignIn;
            
            for (let i = 0; i < mails.length; i++) {
                const data = mails[i];
                let delBtnDisabled = false;
                if(!([Defines.MailState[1], Defines.MailState[2]].includes(Defines.MailState[data?.State ?? 0]))) delBtnDisabled=true;
                

                const items: ReactElement[] = [];
                if (data.RewardList && 0 < data.RewardList.length) {
                    for (let k = 0; k < data.RewardList.length; k++) {
                        const itemInfo = itemTable.find(_ => _.ID === data.RewardList[k].RewardID);
                        items.push(
                            <ListItem key={k}>
                                <ListItemText>{itemInfo?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}</ListItemText>
                                <ListItemText sx={{ml: 1}}>{`${data?.RewardList[k]?.RewardCount}개`}</ListItemText>
                            </ListItem>
                        );
                    }
                }

                list.push(
                    <BorderedTableRow key={i}>
                        <TableCell>
                            <Button disabled={delBtnDisabled || isSigned} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant='outlined' size='small' color='error' onClick={(event)=>{onDelete(event, data, i)}}>삭제</Button>
                        </TableCell>
                        <TableCell>{data?.MailID.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{Defines.MailState[data?.State].toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{Defines.MailAttribute[data?.MailType].toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{data?.Title.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell style={{whiteSpace: 'pre-line', minWidth: 300, fontSize: 14}}>
                            {data?.Message.toString().replaceAll(/(?<=.{2})./g, '*')}
                        </TableCell>
                        <TableCell>{data?.ExpireAt ? dayjs.unix(data?.ExpireAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        <TableCell>{data?.ReceiveAt ? dayjs.unix(data?.ReceiveAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        <TableCell>
                            <List>
                                {items}
                            </List>
                        </TableCell>
                    </BorderedTableRow>
                )
            }

            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <MinWidthTableCell>관리</MinWidthTableCell>
                            <MinWidthTableCell>우편 ID</MinWidthTableCell>
                            <MinWidthTableCell>상태</MinWidthTableCell>
                            <MinWidthTableCell>성격</MinWidthTableCell>
                            <MinWidthTableCell>제목</MinWidthTableCell>
                            <MinWidthTableCell>내용</MinWidthTableCell>
                            <MinWidthTableCell>삭제일시({timezoneName})</MinWidthTableCell>
                            <MinWidthTableCell>수령일시({timezoneName})</MinWidthTableCell>
                            <MinWidthTableCell>생성일시({timezoneName})</MinWidthTableCell>
                            <MinWidthTableCell>수정일시({timezoneName})</MinWidthTableCell>
                            <MinWidthTableCell>아이템</MinWidthTableCell>
                        </BorderedTableRow>
                    </TableHead>
                    <TableBody>
                        {list}
                    </TableBody>
                </>
            );
        } else {
            result = (
                <TableBody>
                    <BorderedTableRow>
                        <TableCell className={commonUIStyles.noneCell}>검색된 우편 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }

    return result;
    }, [userAccount, itemTable, mails, onDelete]);

    content = (<>{contents()}</>);

    return content;
}

export default EditMailContents;