import { ReactElement, useCallback } from 'react';
import { PaginatedList } from '@helpers/paging';
import styles from '@styles/ui/paging.module.sass';
import dynamic from 'next/dynamic';
import { useRouter } from 'next/router';
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });

interface PageProps {
    datas: PaginatedList<any>;
}

const Paging = ({ datas }: PageProps) => {
    const router = useRouter();

    const goToPage = useCallback(async (parameters: string) => {
        router.push(parameters);
    }, [router]);
    
    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const pageList: ReactElement[] = [];
        if (datas.hasPreviousPageBlock) {
            pageList.push(<Button key={`first`} className={`${styles.pagingButton} ${styles.pagingButtonFirst}`} variant="outlined" size="small" color="inherit" title="처음 페이지로 이동" onClick={() => goToPage(`?page=1&${datas.parameters}`)}>처음</Button>);
            pageList.push(<Button key={`prev`} className={`${styles.pagingButton} ${styles.pagingButtonPrev}`} variant="outlined" size="small" color="inherit" title="이전 페이지 목록으로 이동" onClick={() => goToPage(`?page=${(datas.startPage > 1) ? datas.startPage - 1 : 1}&${datas.parameters}`)}>이전</Button>);
        }

        for (let i = datas.startPage; i <= datas.endPage; i++) {
            if (i == datas.page) {
                pageList.push(<Button key={i} className={`${styles.pagingButton} ${styles.pagingButtonCurrent}`} variant="contained" size="small" color="primary">{i}</Button>);
            } else {
                pageList.push(<Button key={i} className={`${styles.pagingButton}`} variant="outlined" size="small" color="inherit" onClick={() => goToPage(`?page=${i}&${datas.parameters}`)}>{i}</Button>);
            }
        }
        
        if (datas.hasNextPageBlock) {
            pageList.push(<Button key={`next`} className={`${styles.pagingButton} ${styles.pagingButtonNext}`} variant="outlined" size="small" color="inherit" title="다음 페이지 목록으로 이동" onClick={() => goToPage(`?page=${(datas.endPage < datas.totalPages) ? datas.endPage + 1 : datas.totalPages}&${datas.parameters}`)}>다음</Button>);
            pageList.push(<Button key={`last`} className={`${styles.pagingButton} ${styles.pagingButtonLast}`} variant="outlined" size="small" color="inherit" title="마지막 페이지로 이동" onClick={() => goToPage(`?page=${datas.totalPages}&${datas.parameters}`)}>마지막</Button>);
        }

        result = <>{pageList}</>;
        return result;
    }, [datas, goToPage]);

    return (
        <Box className={styles.pagingWrapper}>
            {contents()}
        </Box>
    );
}

export default Paging;