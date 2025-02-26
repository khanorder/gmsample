import { useAppSelector  } from '@hooks/index';
import { ChangeEvent, ReactElement, useCallback, useEffect, useState } from 'react';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { styled, TextField, Autocomplete, } from '@mui/material';
import dynamic from 'next/dynamic';
import { DataTableModels } from '@ngel/data/tables/model';
import deepmerge from 'deepmerge';

const DateTimePicker = dynamic(() => import('@components/ui/DateTimePicker'), { ssr: false });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });

interface ContentsProps {
    userAccount: Models.UserAccount;
    datas: Models.TreasureBox[];
    setTreasureBoxes: React.Dispatch<React.SetStateAction<Models.TreasureBox[]>>
    opts: DataTableModels.TreasureBoxData[];
    onDelete: (index: number) => void;
}

const EditTreasureBoxContents = ({ userAccount, datas, setTreasureBoxes, opts, onDelete } : ContentsProps) : ReactElement => {
    let content: ReactElement = <></>;

    const [treasureBoxOpts, setTreasureBoxOpts] = useState<DataTableModels.TreasureBoxData[]>([]);
    const isSigned = userAccount.IsSignIn;

    useEffect(() => {
        setTreasureBoxOpts(opts)
    }, [opts]);

    const onChangeData = useCallback((e, v: DataTableModels.TreasureBoxData, index: number) => {
        if(!v) return;
        if(isNaN(Number(v?.ID))) return;
        // const isOverlap = datas.find((element)=> element.BoxID == Number(v.ID));
        // if(isOverlap){
        //     alert("중복된 값입니다.");
        //     return;
        // }

        // setTreasureBoxes(prev => {
        //     if(prev[index]){
        //         prev[index].BoxID = Number(v.ID);
        //         prev[index].IsDailyBox = v.IsDailyBox;
        //     }
        //     return deepmerge([], prev);
        // })
    }, []);


    const onChangeOpenAt = useCallback((date: string | null, index: number) => {
        if(date == "Invalid Date") {
            return;
        }

        const dateStr = date ?? "";
        let dateObj, currentDate;
        try {
            dateObj = dayjs(dateStr);
            currentDate = new Date();

            if(dayjs(currentDate).diff(dateObj, 'second') <= 0){
                alert("현재 시간보다 이후 시간입니다.")
                return;
            }
        } catch (error) {
            return ;
        }

        if(dateObj){
            const unixTimestamp = dateObj?.unix();
            if(unixTimestamp < 0) return;
            // setTreasureBoxes(prev => {
            //     if(prev[index]){
            //         prev[index].OpenAt = unixTimestamp;
            //         prev[index].isChanged = true;
            //     }
            //     return deepmerge([], prev);
            // })
        }
            
    }, []);

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];

        for(let i = 0; i < datas.length; i++){
            const data = datas[i];
            let rowClass: string = commonUIStyles.row;
            if (data.isChanged)
                rowClass = `${commonUIStyles.row} ${commonUIStyles.changed}`;

            // const treasureBoxInfo = treasureBoxOpts.find(element => element.ID == data?.BoxID);
            // list.push(
            //     <BorderedTableRow key={i} className={rowClass}>
            //         <TableCell component="th" scope="row">
            //             <Button disabled={isSigned} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant='outlined' size='small' color='error' onClick={() => onDelete(i)}>삭제</Button>
            //         </TableCell>
            //         <TableCell>
            //             <FormControl fullWidth>
            //                 { data.isNewData ?
            //                     <Autocomplete disabled={isSigned} options={treasureBoxOpts} fullWidth
            //                     getOptionLabel={(option) => (option as DataTableModels.TreasureBoxData)?.ID.toString()}
            //                     value={treasureBoxInfo} onChange={(e, v) => onChangeData(e, v as DataTableModels.TreasureBoxData, i)}
            //                     renderInput={(params) => <TextField {...params} label="박스ID" />} />
            //                     :
            //                     <Typography>{treasureBoxInfo?.ID ?? data?.BoxID ?? ""}</Typography>
            //                 }
            //             </FormControl>
            //         </TableCell>
            //         <TableCell>
            //             { data.isNewData ?
            //                 <DateTimePicker label="생산시작시간" enableTimePicker={true} format="YYYY-MM-DD HH:mm" value={data?.OpenAt == 0 ? null : dayjs.unix(data?.OpenAt).tz().format("YYYY-MM-DD HH:mm")} onChange={(date) => onChangeOpenAt(date, i)} />    
            //                 :
            //                 <Typography>{data?.OpenAt ? dayjs.unix(data?.OpenAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</Typography>
            //             }
            //         </TableCell>
            //         <TableCell>{data?.IsDailyBox ? "O" : "X"}</TableCell>
            //         <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
            //         <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
            //     </BorderedTableRow>
            // )
        }
        result = (<>{list}</>);
        return result;
    }, [datas])

    if(datas && 0 < datas.length){
        content = (
            <>
                <TableHead>
                    <BorderedTableRow>
                        <TableCell>관리</TableCell>
                        <TableCell>박스ID</TableCell>
                        <TableCell>오픈시간({timezoneName})</TableCell>
                        <TableCell>데일리박스 여부</TableCell>
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
                    <TableCell className={commonUIStyles.noneCell}>검색된 트레저박스 정보가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }

    return content;
}

export default EditTreasureBoxContents;