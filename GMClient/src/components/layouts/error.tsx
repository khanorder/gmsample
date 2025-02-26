import dynamic from 'next/dynamic';
const Footer = dynamic(() => import('@components/layouts/footer'), { ssr: false });
import { LayoutProps } from '@defines/index';
import styles from '@styles/layouts/error.module.sass';

function ErrorLayout({ children }: LayoutProps) {
    return (
        <div className={styles.layoutWrapper}>
            <main className={styles.mainWrapper}>{children}</main>
            <Footer />
        </div>
    );
}

export default ErrorLayout;