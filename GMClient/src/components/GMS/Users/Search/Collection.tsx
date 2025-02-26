import { Models } from '@ngel/data/models';
import { PaginatedList } from '@helpers/paging';
import { ReactElement } from 'react';
import dynamic from 'next/dynamic';
import { SelectChangeEvent } from '@mui/material/Select';

const CollectionContents = dynamic(() => import('./CollectionContents'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });

export interface CollectionProps {
    datas : PaginatedList<Models.Collection>;
    onTabSearch: (value: number, categoryValue: number) => void;
    onChangeListCount: ( event: SelectChangeEvent<unknown> ) => void;
}

const Collection = ({ datas, onTabSearch, onChangeListCount }: CollectionProps): ReactElement => {

    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <CollectionContents datas={datas ?? []} onTabSearch={onTabSearch} onChangeListCount={onChangeListCount}/>  
            </Box>
        </>
    );
}

export default Collection;