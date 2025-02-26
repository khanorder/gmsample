import { useAppSelector } from '@hooks/index';
import { ReactElement, MutableRefObject, useCallback, useState, useEffect } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { Defines } from '@ngel/data/autoDefines';
import { ERewardType } from '@ngel/data/models/lobby';
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const List = dynamic(() => import('@mui/material/List'), { ssr: false });
const ListItem = dynamic(() => import('@mui/material/ListItem'), { ssr: false });
const ListItemText = dynamic(() => import('@mui/material/ListItemText'), { ssr: false });
const MinWidthTableCell = styled(TableCell)(({ theme }) => ({
    minWidth: "120px"
}));

interface ContentsProps {
    datas: PaginatedList<Models.Mail>;
}

const MailContents = ({ datas }: ContentsProps): ReactElement => {
    const tables = useAppSelector(state => state.tables);
    const itemTable = tables.itemTable;
    
    const contents = useCallback(() => {
        let result: ReactElement = <></>;
        if (datas && 0 < datas?.items.length) {
            const list: ReactElement[] = [];

            for (let i = 0; i < datas.items.length; i++) {
                const mail = datas.items[i];
                const items: ReactElement[] = [];
                if (mail.RewardList && 0 < mail.RewardList.length) {
                    for (let k = 0; k < mail.RewardList.length; k++) {
                        const itemInfo = itemTable.find(_ => _.ID === mail.RewardList[k].RewardID);
                        items.push(
                            <ListItem key={k}>
                                <ListItemText>{itemInfo?.NameStringWithID}</ListItemText>
                                <ListItemText sx={{ml: 1}}>{`${mail?.RewardList[k]?.RewardCount}개`}</ListItemText>
                            </ListItem>
                        );
                    }
                }

                list.push(
                    <BorderedTableRow key={i}>
                        <TableCell component="th" scope="row">{mail?.MailID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{Defines.MailState[mail?.State].toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{Defines.MailAttribute[mail?.MailType].toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{mail?.Title.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell style={{whiteSpace: 'pre-line', minWidth: 300, fontSize: 14}}>
                            {mail?.Message.toString().replaceAll(/(?<=.{1})./g, '*')}
                        </TableCell>
                        <TableCell>{mail?.ExpireAt ? dayjs.unix(mail?.ExpireAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        <TableCell>{mail?.ReceiveAt ? dayjs.unix(mail?.ReceiveAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        <TableCell>{mail?.CreateAt ? dayjs(mail?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        <TableCell>{mail?.UpdateAt ? dayjs(mail?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
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
                            <MinWidthTableCell>우편 **</MinWidthTableCell>
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
    },[datas, itemTable]);

    return contents();
}

export default MailContents;