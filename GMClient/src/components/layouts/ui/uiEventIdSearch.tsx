import { ReactElement, useEffect, useState, useCallback } from 'react';
import { useAppSelector } from "@hooks/index";
import dynamic from 'next/dynamic';
import { DataTableModels } from '@ngel/data/tables/model';
import TextField from '@mui/material/TextField';

const Autocomplete = dynamic(() => import('@mui/material/Autocomplete'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });

interface EventIdSearchProps {
    size?: "small" | "medium";
    label?: string;
    onChange: (event : any, v: DataTableModels.BiskitLogEventID) => void;
}

function EventIdSearch ({size, onChange}: EventIdSearchProps) {
    const EventIdTable = useAppSelector( state => state.tables.biskitLogEventIDTable );
    const [eventIdInfos, setEventIdInfos] = useState<DataTableModels.BiskitLogEventID[]>([]);

    useEffect(()=> {
        const eventIdOptions: DataTableModels.BiskitLogEventID[] = [];

        for(const key in EventIdTable){
            const item = new DataTableModels.BiskitLogEventID(EventIdTable[key]);
            eventIdOptions.push(item);
        }

        setEventIdInfos(eventIdOptions);
    }, [EventIdTable]);

    const contents = useCallback((): ReactElement => {
        return (
        <FormControl fullWidth variant='outlined' size={size}>
             <Autocomplete options={eventIdInfos} getOptionLabel={(option) => `${(option as DataTableModels.BiskitLogEventID).EventName.replaceAll(/(?<=.{3})./g, '*')} (${(option as DataTableModels.BiskitLogEventID).EventID.replaceAll(/(?<=.{5})./g, '*')})` }
             renderInput={(params) => <TextField {...params} label="이벤트ID 검색" size='small'
             />} onChange={(e, v) => onChange(e, v as DataTableModels.BiskitLogEventID)}/>
        </FormControl>
        );
    }, [eventIdInfos, size, onChange]);
    
    return contents();
}

export default EventIdSearch;