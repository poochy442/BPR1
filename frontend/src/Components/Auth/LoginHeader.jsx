import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router';
import { autoLogIn, logOut } from '../../Store/AuthSlice';

import '../../Styles/Auth/LoginHeader.scss';
import { COOKIE_NAMES, getCookie } from '../Cookies/Cookies';

const LoginHeader = () => {
	const navigate = useNavigate();
	const auth = useSelector(state => state.auth)
	const dispatch = useDispatch()

	useEffect(() => {
		if(!auth.loaded){
			let cookie = getCookie(COOKIE_NAMES.required);
			if(cookie && cookie.authKey)
				dispatch(autoLogIn(cookie.authKey));
		}
	}, [auth])
	
	const links = auth.loggedIn ? (
		<div className='authLinks'>
			<button className='buttonLink' onClick={() => dispatch(logOut())}><p>Log out</p></button>
			<div className='avatar' onClick={() => {navigate("/account")}}>
				<p>{auth.initials}</p>
			</div>
		</div>
	) : (
		<div className='authLinks'>
			<a className='link' href='/login'><p>Log in</p></a>
			<a className='link' href='/signup'><p>Sign up</p></a>
		</div>
	);

	return (
		<div className='loginHeader'>
			{links}
		</div>
	)
}

export default LoginHeader