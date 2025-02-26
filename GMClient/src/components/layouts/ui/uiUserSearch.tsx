import { ChangeEvent, ReactElement, useState, useCallback, KeyboardEvent } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import { Defines } from '@ngel/data/autoDefines';
import isEmpty from 'lodash/isEmpty';
import { SelectChangeEvent } from '@mui/material';
import dynamic from 'next/dynamic';

const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const InputLabel = dynamic(() => import('@mui/material/InputLabel'), { ssr: false });
const InputAdornment = dynamic(() => import('@mui/material/InputAdornment'), { ssr: false });
const OutlinedInput = dynamic(() => import('@mui/material/OutlinedInput'), { ssr: false });
const Select = dynamic(() => import('@mui/material/Select'), { ssr: false });
const MenuItem = dynamic(() => import('@mui/material/MenuItem'), { ssr: false });
const IconButton = dynamic(() => import('@mui/material/IconButton'), { ssr: false });
const SearchIcon = dynamic(() => import('@mui/icons-material/Search'), { ssr: false });

interface UserSearchProps {
    onChange: (type: Defines.UserSearchType, value: string) => void;
    onSubmit: (type: Defines.UserSearchType, value: string) => void;
    ignoreSearchType?: Defines.UserSearchType[];
    size?: "small" | "medium";
    label?: string;
}

function UserSearch ({ onChange, onSubmit, ignoreSearchType, size, label }: UserSearchProps) {
    const [searchType, setSearchType] = useState<Defines.UserSearchType>(Defines.UserSearchType.AccountID);
    const [searchValue, setSearchValue] = useState<string>("");

    const onChangeSearchType = useCallback((e: SelectChangeEvent<unknown>) => {
        let value: Defines.UserSearchType = 0;

        if (e && e.target && e.target.value) {
            try {
                value = parseInt(e.target.value.toString());
            } catch (error) {
                if ("production" != process.env.NODE_ENV) {
                    console.log(error);
                }
            }
        }

        setSearchType(value);
        onChange(value, searchValue);
    }, [searchValue, setSearchType, onChange]);

    const onChangeSearchValue = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>) => {
        var value = "";
        if (e && e.target && !isEmpty(e.target.value)) {
            value = e.target.value.trim();
        }

        setSearchValue(value);
        onChange(searchType, value);
    }, [searchType, onChange]);

    const submitValue = useCallback(() => {
        onSubmit(searchType, searchValue);
    }, [searchType, searchValue, onSubmit])

    const onSubmitValue = useCallback((e: KeyboardEvent) => {
        if ('Enter' == e.key)    
            submitValue();
    }, [submitValue]);

    const searchTypeSelectList = () => {
        const list: ReactElement[] = [];
        let enumSize = 0;
        if (0 < Object.values(Defines.UserSearchType).length)
            enumSize = Object.values(Defines.UserSearchType).length / 2;

        for (let i = 0; i < enumSize; i++) {
            if (0 == i)
                continue;

            if (ignoreSearchType && ignoreSearchType.includes(i))
                continue;

            list.push(<MenuItem key={i} value={i}>{Defines.UserSearchType[i]}</MenuItem>);
        }

        return list;
    }

    return (
        <FormControl fullWidth variant='outlined' size={size}>
            <InputLabel htmlFor="search-user-input">{label ?? "검색"}</InputLabel>
            <OutlinedInput size='small' id="search-user-input" label={label ?? "검색"} sx={(size == "small" ? { paddingRight: 0 } : {})} value={searchValue} onChange={e => onChangeSearchValue(e)} onKeyUp={async (e) => await onSubmitValue(e)} 
                startAdornment={
                    <InputAdornment position='start'>
                        <Select className={commonUIStyles.select} sx={{ fontSize: { xs: 10, sm: 12, md: 16 }}} value={searchType} size={size} variant='standard' onChange={(e) => onChangeSearchType(e)}>
                            {searchTypeSelectList()}
                        </Select>
                    </InputAdornment>
                }
                endAdornment={
                    <InputAdornment position="end">
                        <IconButton type="button" sx={{ p: '10px' }} aria-label="search" onClick={submitValue}>
                            <SearchIcon />
                        </IconButton>
                    </InputAdornment>
                }
            />
        </FormControl>
    );
}

export default UserSearch;