import { ReactElement, useCallback, useEffect, useRef } from 'react';
import { GetServerSideProps } from 'next';
import { dayjs } from '@helpers/localizedDayjs';
import dynamic from 'next/dynamic';

const Layout = dynamic(() => import('@components/layouts'), { ssr: false });
const ThemeLayout = dynamic(() => import('@components/layouts/theme'), { ssr: false });
const ManageLayout = dynamic(() => import('@components/layouts/manage'), { ssr: false });
const List = dynamic(() => import('@components/GMS/Users/Mails/List'), { ssr: false });

interface PageProps {
    pageProp: number;
    searchUidProp: number;
    searchMemberNoProp: number;
    searchNickProp: string;
    startDateProp: string;
    endDateProp: string;
    mode: 'list';
}

function Page({ pageProp, searchUidProp, searchMemberNoProp, searchNickProp, startDateProp, endDateProp, mode }: PageProps) {
    const firstRender = useRef(true);

    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [firstRender]);
    //#endregion

    const contents = useCallback((): ReactElement => {
        switch (mode) {

            default:
                return <List page={pageProp} searchUid={searchUidProp} searchMemberNo={searchMemberNoProp} searchNick={searchNickProp} startDate={startDateProp} endDate={endDateProp} />;
        }
    }, [pageProp, searchUidProp, searchMemberNoProp, searchNickProp, startDateProp, endDateProp, mode]);

    return (
        <>{contents()}</>
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
    const { req, query } = context;
    const pageString: string = query.page ? query.page.toString().trim() : '1';
    const searchUidString: string = query.searchUid ? query.searchUid.toString().trim() : '';
    const searchMemberNoString: string = query.searchMemberNo ? query.searchMemberNo.toString().trim() : '';
    const searchNickString: string = query.searchNick ? query.searchNick.toString().trim() : '';
    const startDateString: string = query.startDate ? query.startDate.toString() : "";
    const endDateString: string = query.endDate ? query.endDate.toString() : "";

    let page = 1;
    let startDate = dayjs(startDateString).isValid() ? dayjs.tz(startDateString).format("YYYY-MM-DD HH:mm:ss") : "";
    let endDate = dayjs(endDateString).isValid() ? dayjs.tz(endDateString).format("YYYY-MM-DD HH:mm:ss") : "";
    let searchUid: number | null = null;
    let searchMemberNo: number | null = null;
    let searchNick: string | null = null;
    // let mode: 'list' | 'edit' = 'list';
    let mode: 'list';

    try {
        page = !pageString.match(/[^\d]/g) && !isNaN(parseInt(pageString)) ? parseInt(pageString) : 1;
    } catch (error) {
        if ("production" != process.env.NODE_ENV)
            console.log(error);
    }

    try {
        searchUid = !searchUidString.match(/[^\d]/g) && !isNaN(parseInt(searchUidString)) ? parseInt(searchUidString) : null;
    } catch (error) {
        if ("production" != process.env.NODE_ENV)
            console.log(error);
    }

    try {
        searchMemberNo = !searchMemberNoString.match(/[^\d]/g) && !isNaN(parseInt(searchMemberNoString)) ? parseInt(searchMemberNoString) : null;
    } catch (error) {
        if ("production" != process.env.NODE_ENV)
            console.log(error);
    }

    try {
        searchNick = searchNickString ?? '';
    } catch (error) {
        if ("production" != process.env.NODE_ENV)
            console.log(error);
    }

    switch (query.mode) {

        default:
            mode = 'list';
            break;
    }

    return {
        props: {
            pageProp: page,
            searchUidProp: searchUid,
            searchMemberNoProp: searchMemberNo,
            searchNickProp: searchNick,
            startDateProp: startDate,
            endDateProp: endDate,
            mode: mode
        }
    };
}

export default Page;