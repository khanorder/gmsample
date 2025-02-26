import { ReactElement } from 'react';
import { NextPageContext } from 'next/types';
import { ErrorComponentProps } from '@defines/index';
import dynamic from 'next/dynamic';
const ErrorLayout = dynamic(() => import('@components/layouts/error'), { ssr: false });
const Error404 = dynamic(() => import('@components/errors/404'), { ssr: false });
const Error500 = dynamic(() => import('@components/errors/500'), { ssr: false });

function Error({ statusCode }: ErrorComponentProps) {
    switch (statusCode) {
        case 404:
            return <Error404 />

        case 500:
            return <Error500 />
    }
    return <Error404 />
}

Error.getLayout = function getLayout(page: ReactElement) {
    return (<ErrorLayout>{page}</ErrorLayout>);
}

Error.getInitialProps = ({ res, err }: NextPageContext) => {
    const statusCode = res ? res.statusCode : err ? err.statusCode : 404
    return { statusCode }
}

export default Error