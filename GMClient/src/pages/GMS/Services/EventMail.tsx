import { ReactElement, useEffect, useRef } from 'react';
import { GetServerSideProps } from 'next';
import dynamic from 'next/dynamic';
import React from 'react';
import isEmpty from 'lodash/isEmpty';
import { Typography } from '@mui/material';

const Layout = dynamic(() => import('@components/layouts'), { ssr: true });
const ThemeLayout = dynamic(() => import('@components/layouts/theme'), { ssr: true });
const ManageLayout = dynamic(() => import('@components/layouts/manage'), { ssr: true });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: true });
const EventMailList = dynamic(() => import('@components/GMS/Services/EventMail/List'), { ssr: true });
const EventMailInsertForm = dynamic(() => import('@components/GMS/Services/EventMail/InsertForm'), { ssr: true });
const EventMailEditForm = dynamic(() => import('@components/GMS/Services/EventMail/EditForm'), { ssr: true });

interface EventMailProps {
    page: number;
    pageListCount: string;
    mode: 'list' | 'insert' | 'edit';
    id: string;
}
function Page({ page, pageListCount, mode, id }: EventMailProps) {
    const firstRender = useRef(true);
    
    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [firstRender]);
    //#endregion

    const contents = (): ReactElement => {
        switch (mode) {
            case 'insert':
                return <EventMailInsertForm/>;

            case 'edit':
                return <EventMailEditForm pageProp={page} mode={mode} id={id} />;
    
            default:
                return <EventMailList pageProp={page}/>;
        }
    };

    return (
        <Box sx={{pt:3}}>
            <Box sx={{ mt: 2 }}>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* 이벤트 우편 발송/수정/삭제 기능.</Typography>
            </Box>
            {contents()}
        </Box>
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

export const getServerSideProps: GetServerSideProps = async (context) => {
    const { query } = context;

    const pageString: string = query.page ? query.page.toString() : '1';

    let page = 1;
    try {
        page = !pageString.match(/[^\d]/g) && !isNaN(parseInt(pageString)) ? parseInt(pageString) : 1;
     } catch (error) {
        if ("production" != process.env.NODE_ENV)
            console.log(error);
    }

    let mode: 'list' | 'insert' | 'edit' = 'list';
    let id: string = '';
    if ('edit' == query.mode) {
        mode = 'edit';
        if (!query || !query.id || "string" !== typeof query.id)
            return { notFound: true };

        id = query.id
    } else if ('insert' == query.mode) {
        mode = 'insert';
    } else {
        mode = 'list';
    }

    return {
        props: { page: page,
            mode: mode, id: id }
    };
}

export default Page;