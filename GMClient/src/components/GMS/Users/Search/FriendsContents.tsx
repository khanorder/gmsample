import { ReactElement, useCallback } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import commonUIStyles from '@styles/ui/common.module.sass';
import dynamic from 'next/dynamic';
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });

interface ContentsProps {
    datas: PaginatedList<Models.Friend>;
}

const FriendsContents = ({ datas } : ContentsProps): ReactElement => {

    const contents = useCallback(() : ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];

        if(datas && 0 < datas.items.length){
            for(let i = 0; i < datas.items.length; i++){
                const data = datas.items[i];
                list.push(
                    <BorderedTableRow key={i}>
                        <TableCell>{data?.FriendUID.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{data?.FriendMemberNo.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{data?.FriendNick.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{data?.IsDeleted}</TableCell>
                    </BorderedTableRow>
                )
            }

            result = (<>
                <TableHead>
                    <BorderedTableRow>
                        <TableCell>친구 UID</TableCell>
                        <TableCell>친구 MemberNo</TableCell>
                        <TableCell>친구 닉네임</TableCell>
                        <TableCell>삭제여부</TableCell>
                    </BorderedTableRow>
                </TableHead>
                <TableBody>
                    {list}
                </TableBody>
            </>)
        }
        else {
            result = (<>
                <TableBody>
                    <BorderedTableRow>
                        <TableCell className={commonUIStyles.noneCell}>검색된 친구 **가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            </>)
        }
        return result;
    }, [datas])

    return contents();
};

export default FriendsContents;