import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router';
import { Link } from 'react-router-dom'
import { logOut } from '../../Store/AuthSlice';

import '../../Styles/Auth/LoginHeader.scss';

const LoginHeader = () => {
	const navigate = useNavigate();
	const auth = useSelector(state => state.auth)
	const dispatch = useDispatch()
	
	const links = auth.loggedIn ? (
		<div className='authLinks'>
			{auth.isManager ? <Link className='link' to='/manage/restaurant'><p>Restaurant</p></Link> : null}
			{auth.isManager ? <Link className='link' to='/manage/bookings'><p>Bookings</p></Link> : <Link className='link' to='/bookings'><p>Bookings</p></Link>}
			<button className='buttonLink' onClick={() => dispatch(logOut())}><p>Log out</p></button>
			<div className='avatar' onClick={() => {navigate("/account")}}>
				<p>{auth.initials}</p>
			</div>
		</div>
	) : (
		<div className='authLinks'>
			<Link className='link' to='/login'><p>Log in</p></Link>
			<Link className='link' to='/signup'><p>Sign up</p></Link>
		</div>
	);

	return (
		<div className='loginHeader'>
			{links}
		</div>
	)
}

export default LoginHeader