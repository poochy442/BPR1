import { useDispatch, useSelector } from 'react-redux';
import { logOut } from '../../Store/AuthSlice';

import '../../Styles/Auth/LoginHeader.scss';

const LoginHeader = () => {
	const auth = useSelector(state => state.auth)
	const dispatch = useDispatch()
	
	const links = auth.loggedIn ? (
		<div className='authLinks'>
			<button className='buttonLink' onClick={() => dispatch(logOut())}><p>Log out</p></button>
			<div className='avatar'>
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