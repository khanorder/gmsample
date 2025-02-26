import { ReactElement, useEffect, useRef } from 'react'
import { GetStaticProps } from 'next';
import dynamic from 'next/dynamic';

const Layout = dynamic(() => import('@components/layouts'), { ssr: false });
const ThemeLayout = dynamic(() => import('@components/layouts/theme'), { ssr: false });
const ManageLayout = dynamic(() => import('@components/layouts/manage'), { ssr: false });
const Chat = dynamic(() => import('@components/GMS/Chat'), { ssr: false });
const Intro = dynamic(() => import('@components/GMS/Intro'), { ssr: false });

function Page() {
    const firstRender = useRef(true);

    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [firstRender]);
    //#endregion

    const devEnv = 'development';

    return (
        <>
            <Chat devEnv={devEnv} />
            <Intro devEnv={devEnv} />
        </>
    );
};

Page.getLayout = function getLayout(page: ReactElement) {
    return (
        <Layout>
            <ThemeLayout>
                <ManageLayout>{page}</ManageLayout>
            </ThemeLayout>
        </Layout>
    );
}

export const getStaticProps: GetStaticProps = async (context) => {
    return {
        props: {
        }
    };
}

export default Page;