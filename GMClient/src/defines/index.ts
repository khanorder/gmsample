import { ReactNode } from 'react';
import { NextRouter } from "next/router";

declare global {
    interface Window {
        FB?: FB;
    }

    interface FB {
        ui: Function;
    }
}

export interface ErrorComponentProps {
    statusCode?: number;
}

export interface LayoutProps {
    children: ReactNode;
}

export interface WithRouterProps {
    router: NextRouter;
}