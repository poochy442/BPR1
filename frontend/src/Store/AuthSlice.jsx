import { createSlice } from '@reduxjs/toolkit';

const AuthSlice = createSlice({
	name: 'auth',
	initialState: {
		loggedIn: false,
		userID: null,
		username: null,
		initials: null,
		error: null
	},
	reducers: {
		// TODO: Connect to database
		logIn(state, action){
			const user = action.payload;

			if(user.username && user.password){
				state.loggedIn = true
				state.userID = 1
				state.username = user.username
				state.initials = user.username.slice(0, 2)
				state.error = null
			} else {
				state.error = 'Login error'
			}
		},
		logOut(state){
			state.loggedIn = false
			state.userID = null
			state.username = null
			state.initials = null
			state.error = null
		},
		signUp(state, action){
			const user = action.payload;

			if(user.username && user.password){
				state.loggedIn = true
				state.userID = 1
				state.username = user.username
				state.initials = user.username.slice(0, 2)
				state.error = null
			} else {
				state.error = 'Login error'
			}
		},
		autoLogIn(state, action){
			const authKey = action.payload;
		}
	}
})

export const { logIn, logOut, signUp, autoLogIn } = AuthSlice.actions
export default AuthSlice.reducer