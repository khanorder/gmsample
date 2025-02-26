import { LayoutProps } from '@defines/index';

function DefaultLayout({ children }: LayoutProps) {
    return (
        <>
            {children}
        </>
    );
}

export default DefaultLayout;