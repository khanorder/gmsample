import { ReactElement, useEffect, useRef } from 'react';
import { GetServerSideProps } from 'next';
import dynamic from 'next/dynamic';
const Layout = dynamic(() => import('@components/layouts'), { ssr: true });
const ThemeLayout = dynamic(() => import('@components/layouts/theme'), { ssr: true });
const ManageLayout = dynamic(() => import('@components/layouts/manage'), { ssr: true });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: true });
const ManageUserList = dynamic(() => import('@components/GMS/Manages/Users/List'), { ssr: true });
const ManageUserInsertForm = dynamic(() => import('@components/GMS/Manages/Users/InsertForm'), { ssr: true });
const ManageUserEditForm = dynamic(() => import('@components/GMS/Manages/Users/EditForm'), { ssr: true });

interface ManageUserProps {
    mode: 'list' | 'insert' | 'edit';
    id: string;
}

function Page({ mode, id }: ManageUserProps) {;
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
                return <ManageUserInsertForm />;

            case 'edit':
                return <ManageUserEditForm mode={mode} id={id} />;
    
            default:
                return <ManageUserList />;
        }
    };

    return (
        <Box sx={{pt:3}}>
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
        props: { mode: mode, id: id }
    };
}

export default Page;