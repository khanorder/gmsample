import { useAppSelector } from "@hooks/index";
import { ReactElement, useCallback, ChangeEvent} from 'react';
import { Models } from '@ngel/data/models';
import dayjs, { Dayjs } from 'dayjs';
import 'dayjs/locale/ko';
import timezone from 'dayjs/plugin/timezone.js';
import utc from 'dayjs/plugin/utc.js';
// dayjs.extend(timezone);
// dayjs.extend(utc);
import commonUIStyles from '@styles/ui/common.module.sass';
import { styled } from '@mui/material';
import dynamic from 'next/dynamic';

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const TextField = dynamic(() => import('@mui/material/TextField'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });

interface ContentsProps {
    userAccount: Models.UserAccount;
    datas: Models.Asset[];
    onChangeDatas: (e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, maxValue: number, index: number) => void;
}

const EditAssetContents = ({ userAccount, datas, onChangeDatas }: ContentsProps): ReactElement => {

    const table = useAppSelector(state => state.tables);
    const assetTable = table.assetDataTable;
    let content: ReactElement = <></>;

    const isSigned = userAccount.IsSignIn;

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];

        for(let i = 0; i < datas.length; i++){
            const data = datas[i];
            const assetInfo = assetTable.find(element => element.ItemID == data?.AssetID);
            let rowClass: string = commonUIStyles.row;
            if (data.isChanged)
                rowClass = `${commonUIStyles.row} ${commonUIStyles.changed}`;

            list.push(
                <BorderedTableRow key={i} className={rowClass}>
                    <TableCell>{assetInfo?.NameStringWithID ? assetInfo?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.AssetID.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                    <TableCell>
                        <TextField disabled={isSigned} variant='outlined' value={data.Count ? data.Count.toLocaleString() : 0} size='small' placeholder='수량'
                        type="text" label="수량을 입력하세요" onChange={e => onChangeDatas(e, assetInfo?.MaxValue ?? 0, i)}/>
                    </TableCell>
                    <TableCell>{ !data.isNewData ? dayjs(data.CreateAt).format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    <TableCell>{ !data.isNewData ? dayjs(data.UpdateAt).format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                </BorderedTableRow>
            )
        }

        result = (
            <>
                {list}
            </>
        );
        return result;
    }, [assetTable, datas, isSigned, onChangeDatas]);

    
    content = (
        <>
            <TableHead>
                <BorderedTableRow>
                    <TableCell>AssetID</TableCell>
                    <TableCell>수량</TableCell>
                    <TableCell>생성일시</TableCell>
                    <TableCell>갱신일시</TableCell>
                </BorderedTableRow>
            </TableHead>
            <TableBody>
                {contents()}
            </TableBody>
        </>
    )
   
   return content;
}

export default EditAssetContents;