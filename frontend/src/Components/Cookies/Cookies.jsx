import Cookies from 'js-cookie';
import { autoLogIn } from '../../Store/AuthSlice.jsx';
import store from '../../Store/Store.jsx';

export const COOKIE_NAMES = {
	required: 'Required cookies',
	analytics: 'Cookies for analytics'
}

const initRequiredCookies = {
	auth: {
		isLoggedIn: false,
		authKey: null
	},
	cookiePermissions: {
		required: true,
		analytics: false
	}
}

const initAnalyticsCookies = {
	// TODO: Introduce Google Analytics or similar
	test: 'TestContent'
}

export const getCookie = (cookieName) => {
	let c = Cookies.get(cookieName)
	return c ? JSON.parse(c) : null;
}

export const setCookie = (cookieName, cookieContent) => {
	// TODO: Set options like expiry to match backend
	Cookies.set(cookieName, JSON.stringify(cookieContent));
}

export const setupCookies = (analyticsPermission) => {
	const requiredCookies = {
		...initRequiredCookies,
		cookiePermissions: {
			required: true,
			analytics: analyticsPermission
		}
	};
	setCookie(COOKIE_NAMES.required, requiredCookies);
	if(analyticsPermission)
		setCookie(COOKIE_NAMES.analytics, initAnalyticsCookies);
}

export const initCookies = () => {
	const rc = getCookie(COOKIE_NAMES.required), ac = getCookie(COOKIE_NAMES.analytics);
	console.log("Init cookies", rc, ac);
	if(!!rc)
	{
		if(rc.auth && rc.auth.isLoggedIn)
			store.dispatch(autoLogIn(rc.auth.authKey))
	}

	if(!!ac){
		// Start analytics
	}
}