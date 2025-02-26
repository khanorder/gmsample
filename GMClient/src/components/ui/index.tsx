import dynamic from 'next/dynamic';
const BorderedTableRow = dynamic(() => import('./BorderedTableRow'), { ssr: false });
const Paging = dynamic(() => import('./Paging'), { ssr: false });

const UI = { BorderedTableRow, Paging };

export default UI;