import errorStyles from '@styles/layouts/error.module.sass';
import dynamic from 'next/dynamic';

const Error404 = dynamic(() => import('../errors/404'), { ssr: false });
const Footer = dynamic(() => import('../layouts/footer'), { ssr: false });

function Forbidden() {

    return (
        <>
            <div className={errorStyles.layoutWrapper}>
                <main className={errorStyles.mainWrapper}>
                    <Error404 />
                </main>
                <Footer />
            </div>
        </>
    );
}

export default Forbidden;