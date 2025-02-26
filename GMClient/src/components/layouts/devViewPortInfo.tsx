import styles from '@styles/layouts/index.module.sass';
import { useCallback, useEffect, useRef, useState } from 'react';

function DevViewPortInfo() {
    const firstRender = useRef(true);
    
    //#region dev
    const [devViewPortClass, setDevViewPortClass] = useState<string>(styles.devViewPortInfo);
    const [innerWidth, setInnerWidth] = useState<number>();
    const [innerHeight, setInnerHeight] = useState<number>();
    const [outerWidth, setOuterWidth] = useState<number>();
    const [outerHeight, setOuterHeight] = useState<number>();
    
    useEffect(() => {
        if ("development" === process.env.NODE_ENV) {
            if (firstRender.current) {
                handleResize();
                window.addEventListener("resize", handleResize);
            }
        }
    }, []);
    //#endregion

    const handleResize = () => {
        setInnerWidth(window.innerWidth);
        setInnerHeight(window.innerHeight);
        setOuterWidth(window.outerWidth);
        setOuterHeight(window.outerHeight);
    }

    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [firstRender]);
    //#endregion


    const closeViewPortInfo = useCallback(() => {
        setDevViewPortClass(`${styles.devViewPortInfo} ${styles.disable}`);
    }, [setDevViewPortClass]);

    return (
        <>
            {
                'development' === process.env.NODE_ENV
                    ? (
                        <div className={devViewPortClass} onClick={() => closeViewPortInfo()}>
                            <div>클릭하면 사라짐</div>
                            <div>개발환경 확인용</div>
                            <div>innerWidth: {innerWidth}</div>
                            <div>innerHeight: {innerHeight}</div>
                            <div>outerWidth: {outerWidth}</div>
                            <div>outerHeight: {outerHeight}</div>
                            {/* <div>ua: {homepageSettings.uaParser.getUA()}</div> */}
                        </div>
                    )
                    : ""
            }
        </>
    );
}

export default DevViewPortInfo;