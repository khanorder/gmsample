import { ReactElement, useCallback } from "react";
import dynamic from "next/dynamic";
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { Models } from "@ngel/data/models";
import { useAppSelector } from "@hooks/index";
import { EMarketCode, EPlatform } from "@ngel/data/models/lobby";

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });

interface ContentsProps {
    datas: Models.UserDevice[];
}

const UserDeviceContents = ({ datas } : ContentsProps) : ReactElement => {
    const tables = useAppSelector(state => state.tables);

    let content: ReactElement = <></>;
    
    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];
        if(datas && 0 < datas.length){
            for(let i = 0; i < datas.length; i++){
                const data = datas[i];

                list.push(
                    <BorderedTableRow key={i}>
                        <TableCell>{data?.UID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{data?.DeviceID.toString().replaceAll(/(?<=.{3})./g, '*')}</TableCell>
                        <TableCell>{data?.DeviceModel.toString().replaceAll(/(?<=.{3})./g, '*')}</TableCell>
                        <TableCell>{data?.DeviceType.toString().replaceAll(/(?<=.{3})./g, '*')}</TableCell>
                        <TableCell>{`${EMarketCode[data.MarketCode ?? 0].toString().replaceAll(/(?<=.{3})./g, '*')} (${data?.MarketCode})`}</TableCell>
                        <TableCell>{data?.OS.toString().replaceAll(/(?<=.{3})./g, '*')}</TableCell>
                        <TableCell>{`${EPlatform[data.Platform ?? 0].toString().replaceAll(/(?<=.{3})./g, '*')} (${data.Platform})`}</TableCell>
                        <TableCell>{data?.Provider.toString().replaceAll(/(?<=.{3})./g, '*')}</TableCell>
                        <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    </BorderedTableRow>
                )
            }
        }
        result = (<>{list}</>);
        return result;
    },[datas]);

   if(datas && 0 < datas.length){
        content = (
            <>
                <TableHead>
                    <BorderedTableRow>
                        <TableCell>UID</TableCell>
                        <TableCell>기기 ID</TableCell>
                        <TableCell>기기 Model</TableCell>
                        <TableCell>기기 Type</TableCell>
                        <TableCell>Ma********</TableCell>
                        <TableCell>OS</TableCell>
                        <TableCell>플랫폼</TableCell>
                        <TableCell>Pro*****</TableCell>
                        <TableCell>생성시간({timezoneName})</TableCell>
                        <TableCell>수정시간({timezoneName})</TableCell>
                    </BorderedTableRow>
                </TableHead>
                <TableBody>
                    {contents()}
                </TableBody>
            </>
        )
    }
    else{
        content = (
            <TableBody>
                <BorderedTableRow>
                    <TableCell className={commonUIStyles.noneCell}>검색된 기기 정보가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }

    return content;
} 

export default UserDeviceContents;