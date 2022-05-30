import { useState } from 'react'
import { Link, Navigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';

import { signUp } from '../../../Store/AuthSlice';
import '../../../Styles/Pages/Auth/SignUp.scss'

const SignUp = (props) => {
	const [input, setInput] = useState({
		username: '',
		password: '',
		repeatPassword: ''
	});
	const [error, setError] = useState(null);

	const auth = useSelector(state => state.auth);
	const dispatch = useDispatch();
	
	const handleValueChange = (e) => {
		setInput({
			...input,
			[e.target.id]: e.target.value
		})
	}

	const handleSignup = () => {
		if(confirmContent())
			dispatch(signUp({
				username: input.username,
				password: input.password
			}))
	}

	const confirmContent = () => {
		if(input.username === ''){
			setError('Must provide a username.')
			return false
		}
		if(input.password === '' || input.repeatPassword === ''){
			setError('Password cannot be empty')
			return false
		}
		if(input.password !== input.repeatPassword){
			setError('Passwords must match')
			return false
		}

		// Request is good
		setError(null)
		return true;
	}

	// Authentication guard
	if (auth.loggedIn) return <Navigate to='/' />;

	return (
		<div className='signUp'>
			<div className='signUpContainer'>
				<h1>Sign Up</h1>
				<div className='inputContainer'>
					<div className='signUpInput'>
						<label htmlFor='username'>Username</label>
						<input type='text' placeholder='Input username...' id='username' value={input.username} onChange={handleValueChange} />
					</div>
					<div className='signUpInput'>
						<label htmlFor='password'>Password</label>
						<input type='password' id='password' placeholder='Input password...' value={input.password} onChange={handleValueChange} />
					</div>
					<div className='signUpInput'>
						<label htmlFor='repeatPassword'>Repeat password</label>
						<input type='password' id='repeatPassword' placeholder='Input password...' value={input.repeatPassword} onChange={handleValueChange} />
					</div>
					{auth.error ? (
						<p className='errorText'>{auth.error}</p>
					) : error ? (
						<p className='errorText'>{error}</p>
					) : null}
				</div>
				<div className='buttonContainer'>
					<button className='signUpButton button' onClick={handleSignup}>Sign up</button>
					<div className='loginContainer'>
						<p>Already have an account?</p>
						<Link to='/login' className='loginButton button'>Log in</Link>
					</div>
				</div>
			</div>
		</div>
	)
}

export default SignUp