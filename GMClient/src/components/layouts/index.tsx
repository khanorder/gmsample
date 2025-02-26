import { useEffect, useRef } from 'react';
import {  useAppSelector } from "@hooks/index";
import { LayoutProps } from '@defines/index';
import dynamic from 'next/dynamic';
const Head = dynamic(() => import('next/head'), { ssr: false });

function Layout({ children }: LayoutProps) {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const firstRender = useRef(true);

    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [firstRender]);
    //#endregion

    return (
        <>
            <Head>
                <title>{homepageSettings.title}</title>
                <meta name="title" content={homepageSettings.title} />
                <meta name="subject" content={homepageSettings.title} />
                <meta name="description" content={homepageSettings.description} />
                <meta name="keyword" content={homepageSettings.keyword} />
                <meta name="author" content={homepageSettings.author} />
                <meta name="copyright" content={homepageSettings.copyrights} />
                <meta property="og:title" content={homepageSettings.title} />
                <meta property="og:site_name" content={homepageSettings.siteName} />
                <meta property="og:type" content={homepageSettings.siteType} />
                <meta property="og:description" content={homepageSettings.description} />
                <meta property="og:url" content={homepageSettings.url} />
                <meta property="og:image" content={`${homepageSettings.url}${homepageSettings.ogImageUrl}`} />
                <meta property="og:image:type" content="image/jpeg" />
                <meta property="og:image:alt" content={homepageSettings.title} />
                <meta name="twitter:card" content="summary_large_image"  />
                <meta name="twitter:title" content={homepageSettings.title}  />
                <meta name="twitter:description" content={homepageSettings.description} />
                <meta name="twitter:image" content={`${homepageSettings.url}${homepageSettings.ogImageUrlTwitter}`} />
            </Head>
            {children}
        </>
    );
}

export default Layout;