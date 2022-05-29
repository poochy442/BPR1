import { useState } from 'react'
import { Link, Navigate } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux'
import { logIn } from '../../../Store/AuthSlice';

import '../../../Styles/Pages/Auth/LogIn.scss'

const LogIn = (props) => {
	const [input, setInput] = useState({
		username: '',
		password: ''
	});
	const [error, setError] = useState(null);

	const dispatch = useDispatch();
	const auth = useSelector(state => state.auth);

	const handleValueChange = (e) => {
		setInput({
			...input,
			[e.target.id]: e.target.value
		})
	}

	const confirmContent = () => {
		if(input.username === ''){
			setError('Must provide an email.')
			return false
		}
		if(input.password === ''){
			setError('Password cannot be empty')
			return false
		}

		// Request is good
		setError(null)
		return true;
	}

	const handleLogin = () => {
		if(confirmContent())
			dispatch(logIn({
				username: input.username,
				password: input.password
			}))
	}

	// Authentication guard
	if (auth.userID) return <Navigate to='/' />;

	return (
		<div className='login'>
			<div className='loginContainer'>
				<h1>Log in</h1>
				<div className='inputContainer'>
					<div className='loginInput'>
						<label htmlFor='email'>Username</label>
						<input type='text' placeholder='Input username...' id='username' value={input.username} onChange={handleValueChange} />
					</div>
					<div className='loginInput'>
						<label htmlFor='password'>Password</label>
						<input type='password' id='password' placeholder='Input password...' value={input.password} onChange={handleValueChange} />
					</div>
					{auth.error ? (
						<p className='errorText'>{auth.error}</p>
					) : error ? (
						<p className='errorText'>{error}</p>
					) : null}
				</div>
				<div className='buttonContainer'>
					<button className='loginButton button' onClick={handleLogin}>Log in</button>
					<div className='signUpContainer'>
						<p>Don't have an account?</p>
						<Link to='/signup' className='signUpButton button'>Sign up</Link>
					</div>
				</div>
			</div>
		</div>
	)
}

export default LogIn