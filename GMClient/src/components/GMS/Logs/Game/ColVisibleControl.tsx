import { ReactElement, useState } from 'react';
import dynamic from 'next/dynamic';

const List = dynamic(() => import('@mui/material/List'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const ListSubheader = dynamic(() => import('@mui/material/ListSubheader'), { ssr: false });
const ListItem = dynamic(() => import('@mui/material/ListItem'), { ssr: false });
const ListItemButton = dynamic(() => import('@mui/material/ListItemButton'), { ssr: false });
const ListItemText = dynamic(() => import('@mui/material/ListItemText'), { ssr: false });
const Checkbox = dynamic(() => import('@mui/material/Checkbox'), { ssr: false });
const Collapse = dynamic(() => import('@mui/material/Collapse'), { ssr: false });
const ExpandLess = dynamic(() => import('@mui/icons-material/ExpandLess'), { ssr: false });
const ExpandMore = dynamic(() => import('@mui/icons-material/ExpandMore'), { ssr: false });

interface ControlProps {
    states: number[];
    stateUpdate: (index: number) => void;
    colList: string[];
}
const ColVisibleControl = ({ states, stateUpdate, colList }: ControlProps): ReactElement => {
    const [open, setOpen] = useState(false);

    const handleClick = () => {
        setOpen(!open);
    };

    const contents = (): ReactElement => {
        const list: ReactElement[] = [];

        for (let i = 0; i < colList.length; i++) {
            const row = (
                <Grid key={`list-grid=${i}`} item xs={12} sm={6} md={4} lg={3}>
                    <ListItem disablePadding key={`list-item-${i}`} sx={{ border: 1, borderColor: 'rgba(191,191,191, 0.3)', backgroundColor: "rgba(235, 235, 235, 0.3)" }}>
                        <ListItemButton key={`list-btn-${i}`} onClick={() => stateUpdate(i)}>
                            <ListItemText key={`list-label-${i}`} primary={`${colList[i]}`} />
                            <Checkbox key={`list-checkbox-${i}`} checked={states[i] ? true : false} />
                        </ListItemButton>
                    </ListItem>
                </Grid>
            )

            list.push(row);
        }
        return (<>
            <List disablePadding
                aria-labelledby="비활성화 리스트" subheader={<ListSubheader onClick={handleClick} sx={{ cursor: "pointer", display: 'flex', alignItems: 'center', fontSize: 16 }}>비활성화 설정 {open ? <ExpandLess /> : <ExpandMore />}</ListSubheader>}>
                <Collapse in={open} timeout={850} sx={{ padding: 1, borderRadius: 2, backgroundColor: "rgba(245, 245, 245, 0.5)" }}>
                    <Grid container spacing={2}>
                        {list}
                    </Grid>
                </Collapse>
            </List>
        </>);
    }

    return <>{contents()}</>;
}

export default ColVisibleControl;