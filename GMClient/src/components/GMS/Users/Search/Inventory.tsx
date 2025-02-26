import { useAppSelector } from '@hooks/index';
import { Models } from '@ngel/data/models';
import { PaginatedList } from '@helpers/paging';
import { ReactElement } from 'react';
import dynamic from 'next/dynamic';
import { SelectChangeEvent } from '@mui/material/Select';

const InventoryContents = dynamic(() => import('./InventoryContents'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });

export interface InventoryProps {
    datas : PaginatedList<Models.Inventory>;
    onTabSearch: (value: number) => void;
    onChangeListCount: ( event: SelectChangeEvent<unknown> ) => void;
}

const Inventory = ({ datas, onTabSearch, onChangeListCount }: InventoryProps): ReactElement => {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const user = useAppSelector(state => state.user);
    const layouts = useAppSelector(state => state.layouts);

    return (
        <>
            <Box sx={{mt: 5}}>
                <InventoryContents datas={ datas ?? []} onTabSearch={onTabSearch} onChangeListCount={onChangeListCount}/>
            </Box>
        </>
    );
}

export default Inventory;