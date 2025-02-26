import { ReactElement, useEffect, useRef } from 'react';
import { GetServerSideProps } from 'next';
import { dayjs } from '@helpers/localizedDayjs';
import dynamic from 'next/dynamic';

const Layout = dynamic(() => import('@components/layouts'), { ssr: false });
const ThemeLayout = dynamic(() => import('@components/layouts/theme'), { ssr: false });
const ManageLayout = dynamic(() => import('@components/layouts/manage'), { ssr: false });
const List = dynamic(() => import('@components/GMS/Users/Penalties/List'), { ssr: false });
const Edit = dynamic(() => import('@components/GMS/Users/Penalties/Edit'), { ssr: false });

interface PageProps {
    pageProp: number;
    searchUidProp: number;
    searchMemberNoProp: number;
    searchNickProp: string;
    startDateProp: string;
    endDateProp: string;
    uidProp: number|null;
    reportStateProp: number|null;
    mode: 'list' | 'edit';
}

function Page({ pageProp, searchUidProp, searchMemberNoProp, searchNickProp, startDateProp, endDateProp, uidProp, reportStateProp, mode } : PageProps) {
    const firstRender = useRef(true);
    
    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [firstRender]);
    //#endregion

    const contents = (): ReactElement => {
        switch (mode) {
            case 'edit':
                return <Edit page={pageProp} searchUid={searchUidProp} searchMemberNo={searchMemberNoProp} searchNick={searchNickProp} uid={uidProp} reportState={reportStateProp} startDate={startDateProp} endDate={endDateProp} />;
    
            default:
                return <List page={pageProp} searchUid={searchUidProp} searchMemberNo={searchMemberNoProp} searchNick={searchNickProp} startDate={startDateProp} endDate={endDateProp} />;
        }
    };
    
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
    let uid: number|null = null;
    let searchUid: number|null = null;
    let searchMemberNo: number|null = null;
    let searchNick: string|null = null;
    let reportState: number|null = null;
    let mode: 'list' | 'edit' = 'list';

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
        case 'edit':
            mode = 'edit';
            if (!query || !query.uid)
                return { notFound: true };

            if (!query || !query.reportState)
                return { notFound: true };

            try {
                const uidString: string = query.uid ? query.uid.toString().trim() : '0';
                uid = !uidString.match(/[^\d]/g) && !isNaN(parseInt(uidString)) ? parseInt(uidString) : 0;
            
            } catch (error) {
                if ("production" != process.env.NODE_ENV)
                    console.log(error);
                return { notFound: true };
            }

            try {
                const reportStateString: string = query.reportState ? query.reportState.toString().trim() : '0';
                reportState = !reportStateString.match(/[^\d]/g) && !isNaN(parseInt(reportStateString)) ? parseInt(reportStateString) : 0;
            
            } catch (error) {
                if ("production" != process.env.NODE_ENV)
                    console.log(error);
                return { notFound: true };
            }
            break;

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
            uidProp: uid,
            reportStateProp: reportState,
            mode: mode
        }
    };
}

export default Page;