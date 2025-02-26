import { ReactElement, useEffect, useRef } from 'react';
import { GetServerSideProps } from 'next';
import { dayjs } from '@helpers/localizedDayjs';
import dynamic from 'next/dynamic';
const Layout = dynamic(() => import('@components/layouts'), { ssr: true });
const ThemeLayout = dynamic(() => import('@components/layouts/theme'), { ssr: true });
const ManageLayout = dynamic(() => import('@components/layouts/manage'), { ssr: true });
const List = dynamic(() => import('@components/GMS/Services/Billings/List'), { ssr: true });

interface PageProps {
    pageProp: number;
    searchUidProp: number;
    searchMemberNoProp: number;
    searchTidProp: string;
    searchOriTidProp: string;
    searchProductIdProp: string;
    startDateProp: string;
    endDateProp: string;
    uidProp: number|null;
    reportStateProp: number|null;
}

function Page({ pageProp, searchUidProp, searchMemberNoProp, searchTidProp, searchOriTidProp, searchProductIdProp, startDateProp, endDateProp, uidProp, reportStateProp } : PageProps) {
    const firstRender = useRef(true);
    
    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [firstRender]);
    //#endregion
    
    return (
        <List page={pageProp} searchUidProp={searchUidProp} searchMemberNoProp={searchMemberNoProp} searchTidProp={searchTidProp} searchOriTidProp={searchOriTidProp} searchProductidProp={searchProductIdProp} startDateProp={startDateProp} endDateProp={endDateProp} />
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
    const searchTid: string = query.searchTid ? query.searchTid.toString().trim() : '';
    const searchOriTid: string = query.searchOriTid ? query.searchOriTid.toString().trim() : '';
    const searchProductId: string = query.searchProductid ? query.searchProductid.toString().trim() : '';
    const startDateString: string = query.startDate ? query.startDate.toString() : "";
    const endDateString: string = query.endDate ? query.endDate.toString() : "";

    let page = 1;
    let startDate = dayjs(startDateString).isValid() ? dayjs.tz(startDateString).format("YYYY-MM-DD HH:mm:ss") : "";
    let endDate = dayjs(endDateString).isValid() ? dayjs.tz(endDateString).format("YYYY-MM-DD HH:mm:ss") : "";
    let uid: number|null = null;
    let searchUid: number|null = null;
    let searchMemberNo: number|null = null;
    let reportState: number|null = null;

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
    return {
        props: {
            pageProp: page, 
            searchUidProp: searchUid, 
            searchMemberNoProp: searchMemberNo, 
            searchTidProp: searchTid,
            searchOriTidProp: searchOriTid,
            searchProductIdProp: searchProductId,
            startDateProp: startDate, 
            endDateProp: endDate,
            uidProp: uid,
            reportStateProp: reportState,
        }
    };
}

export default Page;