import { styled } from '@mui/material';
import dynamic from 'next/dynamic';
const TableRow = dynamic(() => import('@mui/material/TableRow'), { ssr: false });

const BorderedTableRow = styled(TableRow)(({ theme }) => ({
    ":first-of-type ": {
        " th, td": {
            borderTop: "1px solid #d9d9d9"
        }
    },
    " th, td": {
        borderLeft: "1px solid #d9d9d9",
        padding: "6px 10px",
        textAlign: "center",
    },
    " th": {
        background: "#f3f3f3"
        // borderBottom: "1px solid #898989"
    },
    " td": {
        padding: "7px 10px"
    },
    " th:first-of-type, td:first-of-type": {
        borderLeft: 0
    },
    " th:first-of-type + td:first-of-type": {
        borderLeft: "1px solid #d9d9d9"

    },
}));

export default BorderedTableRow;