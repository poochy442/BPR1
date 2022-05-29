import React from 'react'
import { useEffect } from 'react';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router';

import '../../../Styles/Pages/Auth/Account.scss';

const Account = () => {
	const navigate = useNavigate();
	const auth = useSelector(state => state.auth);

	useEffect(() => {
		if(!auth.loggedIn)
			navigate('/login');
	})

	return (
		<div className='account'>
			<h1>Account</h1>
			<div className="informationItem">
				<p className='itemLabel'>Full name</p>
				<p className='itemValue'>Test user</p>
			</div>
			<div className="informationItem">
				<p className='itemLabel'>Email</p>
				<p className='itemValue'>test@testmail.com</p>
			</div>
			<div className="informationItem">
				<p className='itemLabel'>Phone number</p>
				<p className='itemValue'>+4588888888</p>
			</div>
		</div>
	)
}

export default Account