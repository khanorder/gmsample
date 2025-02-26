import dynamic from 'next/dynamic';
import { useState, useEffect, ReactElement, useCallback } from 'react';
import { SelectChangeEvent } from '@mui/material/Select';

const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const InputLabel = dynamic(() => import('@mui/material/InputLabel'), { ssr: false });
const Select = dynamic(() => import('@mui/material/Select'), { ssr: false });
const MenuItem = dynamic(() => import('@mui/material/MenuItem'), { ssr: false });

export interface ListCountSelectorProps {
    pageLogs: number;
    handleChange: ( event: SelectChangeEvent<unknown> ) => void;
}

const ListCountSelector = ( { pageLogs, handleChange } : ListCountSelectorProps ) : ReactElement => {
    
    const contents = useCallback((): ReactElement => {
        return (<>
                <FormControl fullWidth>
                    <InputLabel>페이지별 로그 개수</InputLabel>
                        <Select
                            value={pageLogs}
                            label='페이지별 로그 개수'
                            style={{height:'40px', width: '200px'}}
                            onChange={(event)=> {handleChange(event)}}
                        >
                            <MenuItem value={10}>10개</MenuItem>
                            <MenuItem value={25}>25개</MenuItem>
                            <MenuItem value={50}>50개</MenuItem>
                            <MenuItem value={100}>100개</MenuItem>
                            <MenuItem value={200}>200개</MenuItem>
                            <MenuItem value={500}>500개</MenuItem>
                        </Select>
                </FormControl>
        </>);
    }, [pageLogs, handleChange]);

    return contents();
}

export default ListCountSelector;