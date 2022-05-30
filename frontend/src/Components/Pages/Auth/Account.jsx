import React, { useState, useEffect } from 'react';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router';

import '../../../Styles/Pages/Auth/Account.scss';

const Account = () => {
	const navigate = useNavigate();
	const auth = useSelector(state => state.auth);
	const [input, setInput] = useState({
		name: auth.name,
		email: auth.email,
		phoneNo: auth.phoneNo
	})
	const [isEditing, setIsEditing] = useState(false);

	useEffect(() => {
		if(!auth.loggedIn)
			navigate('/login');
	}, [auth])

	const handleChange = (e) => {
		setInput({
			...input,
			[e.target.id]: e.target.value
		});
	}

	const handleConfirm = () => {
		// TODO: Call backend and store information
	}

	const handleCancel = () => {
		setInput({
			name: auth.name,
			email: auth.email,
			phoneNo: auth.phoneNo
		});
		setIsEditing(false);
	}

	return (
		<div className='account'>
			<h1>Account</h1>
			<label className="informationItem" htmlFor='name'>
				<p>Full name</p>
				<input id='name' disabled={isEditing ? false : true} value={input.name} onChange={handleChange} className='itemInput' />
			</label>
			<label className="informationItem" htmlFor='email'>
				<p>Email</p>
				<input id='email' disabled={isEditing ? false : true} value={input.email} onChange={handleChange} className='itemInput' />
			</label>
			<label className="informationItem" htmlFor='phoneNo'>
				<p>Phone number</p>
				<input id='phoneNo' disabled={isEditing ? false : true} value={input.phoneNo} onChange={handleChange} className='itemInput' />
			</label>
			{
				isEditing ? (
					<div className='buttonContainer'>
						<div className="button confirm" onClick={handleConfirm}>Confirm</div>
						<div className="button cancel" onClick={handleCancel}>Cancel</div>
					</div>
					) : (
					<div className='button edit' onClick={() => setIsEditing(true)}>Edit</div>
				)
			}
		</div>
	)
}

export default Account