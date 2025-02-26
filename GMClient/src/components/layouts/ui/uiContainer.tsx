import { useEffect, useRef } from 'react';
import dynamic from 'next/dynamic';
const UIHeaderBar = dynamic(() => import('./uiHeaderBar'), { ssr: false });
const UILeftBar = dynamic(() => import('./uiLeftBar'), { ssr: false });

function UIContainer() {
    const firstRender = useRef(true);

    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [firstRender]);

    return (
        <>
            <UIHeaderBar />
            <UILeftBar />
        </>
    );
}

export default UIContainer;