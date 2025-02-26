import styles from '@styles/errors/500.module.sass';
import { useRouter } from 'next/router';
import { useEffect, useRef } from 'react';
import dynamic from 'next/dynamic';

const Link = dynamic(() => import('next/link'), { ssr: false });

function Error500() {
    var router = useRouter();
    const firstRender = useRef(true);
    const eyes = useRef<HTMLDivElement>(null);

    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [firstRender]);
    //#endregion

    useEffect(() => {
        if (firstRender.current)
            return;

        if ('undefined' === typeof document)
            return;

        if ('undefined' === typeof window)
            return;

        var pageX = document.documentElement.clientWidth;
        var pageY = document.documentElement.clientHeight;
        var mouseY = 0;
        var mouseX = 0;

        const handleMouseMove = (e: MouseEvent) => {
            //verticalAxis
            mouseY = e.pageY;
            const yAxis = (pageY/2-mouseY)/pageY*300; 
            //horizontalAxis
            mouseX = e.pageX / -pageX;
            const xAxis = -mouseX * 100 - 100;

            if (eyes.current) {
                eyes.current.style.setProperty('transform', 'translate('+ xAxis +'%,-'+ yAxis +'%)');
            }
            // $('.box__ghost-eyes').css({  });
        };

        window.addEventListener("mousemove", handleMouseMove);

        return () => {
            window.removeEventListener('mousemove', handleMouseMove);
        };
        
    }, [firstRender]);

    return (
        <div className={styles.errorContainer}>
            <div className={styles.box}>
                <div className={styles.box__ghost}>
                    <div className={styles.symbol}></div>
                    <div className={styles.symbol}></div>
                    <div className={styles.symbol}></div>
                    <div className={styles.symbol}></div>
                    <div className={styles.symbol}></div>
                    <div className={styles.symbol}></div>

                    <div className={styles.box__ghost_container}>
                        <div ref={eyes} className={styles.box__ghost_eyes}>
                            <div className={styles.box__eye_left}></div>
                            <div className={styles.box__eye_right}></div>
                        </div>
                        <div className={styles.box__ghost_bottom}>
                            <div></div>
                            <div></div>
                            <div></div>
                            <div></div>
                            <div></div>
                        </div>
                    </div>
                    <div className={styles.box__ghost_shadow}></div>
                </div>

                <div className={styles.box__description}>
                    <div className={styles.box__description_container}>
                        <div className={styles.box__description_title}>어랏!</div>
                        <div className={styles.box__description_text}>페이지가 없는 것 같아요.</div>
                    </div>
                    <Link href="/"><a className={styles.box__button}>메인 페이지</a></Link>
                </div>
            </div>
        </div>
    );
}

export default Error500;