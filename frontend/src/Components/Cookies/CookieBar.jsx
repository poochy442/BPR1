import { useEffect } from 'react';
import { useState } from 'react';
import '../../Styles/Cookies/CookieBar.scss'
import {setupCookies, getCookie, COOKIE_NAMES, initCookies} from './Cookies.jsx'

const CookieBar = () => {
	const requiredPermission = true;
	const [analyticsPermission, setAnalyticsPermission] = useState(false);
	const [cookieSet, setCookieSet] = useState(false);

	useEffect(() => {
		initCookies();
	}, [])
	
	useEffect(() => {
		if(getCookie(COOKIE_NAMES.required)){
			setCookieSet(true);
		}
	}, [cookieSet])
	

	const handleAll = () => {
		setupCookies(true);
		setCookieSet(true);
	}
	const handleNessecary = () => {
		setupCookies(false);
		setCookieSet(true);
	}
	const handleConfirm = () => {
		setupCookies(analyticsPermission);
		setCookieSet(true);
	}

	if(cookieSet) return null;

	return (
		<div className="cookieBar">
			<div className="cookieOptions">
				<p className="cookieText">
					This website uses cookies to give you the most relevant experience by remembering you and your preferences.
				</p>
				<div className="checkboxContainer">
					<label className="required ">
						Required
						<input type='checkbox' checked={requiredPermission} disabled />
					</label>
					<label className="analytics">
						Analytics
						<input
							type='checkbox'
							checked={analyticsPermission}
							onChange={() => setAnalyticsPermission(!analyticsPermission)} />
					</label>
				</div>
			</div>
			<div className="cookieButtons">
				<div className="button acceptAllButton" onClick={handleAll}>
					<p>Accept ALL</p>
				</div>
				<div className="button confirmButton" onClick={handleConfirm}>
					<p>Confirm choices</p>
				</div>
				<div className="button acceptNessecaryButton" onClick={handleNessecary}>
					<p>Accept nessecary</p>
				</div>
			</div>
		</div>
	)
}

export default CookieBar;