import { NextResponse } from 'next/server'
import type { NextRequest } from 'next/server'

// This function can be marked `async` if using `await` inside
export function middleware(request: NextRequest, response: NextResponse) {
	// const isDevelopment = "production" != process.env.NODE_ENV;

	// try {
	// 	const viewCookieName = `${process.env.NEXT_PUBLIC_SESSION_COOKIE_NAME}View`;
	// 	const isAuthentication = request.cookies.get(viewCookieName);
	// 	const rootPath = process.env.NEXT_PUBLIC_MANAGE_PATH ?? "/GMS";
	// 	const authPath = process.env.NEXT_PUBLIC_AUTH_PATH ?? "/NGAuth";
		
	// 	if ("/" == request.nextUrl.pathname) {
	// 		if (isAuthentication) {
	// 			if (isDevelopment)
	// 				console.log(`recirect to ${rootPath} from Home`);
	// 			return NextResponse.redirect(new URL (rootPath, request.url));
	// 		} else {
	// 			if (isDevelopment)
	// 				console.log(`recirect to ${authPath} from Home`);
	// 			return NextResponse.redirect(new URL (authPath, request.url));
	// 		}
	// 	} else if (request.nextUrl.pathname.match(/^\/(NGAuth|oauth\/callback)/)) {
	// 		if (isAuthentication) {
	// 			if (isDevelopment)
	// 				console.log(`recirect to ${rootPath} from Authentication`);
	// 			return NextResponse.redirect(new URL (rootPath, request.url));
	// 		}
    //     } else if (null == request.nextUrl.pathname.match(/^\/(_next)/)) {
	// 		if (!isAuthentication || undefined === isAuthentication) {
	// 			if (isDevelopment)
	// 				console.log(`recirect to '${authPath}' from GMS`);
	// 			return NextResponse.redirect(new URL (authPath, request.url));
	// 		}
	// 	}
	// } catch (error) {
	// 	if (isDevelopment)
	// 		console.log(`middleware - ${request.nextUrl.pathname}: ${error}`);
	// }
}