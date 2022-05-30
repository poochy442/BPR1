import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { COOKIE_NAMES, getCookie, setCookie } from '../Components/Cookies/Cookies';
import { Client } from '../Components/Api/Client';

export const logIn = createAsyncThunk(
	'auth/loginStatus',
	async (data) => {
		let payload = { Email: data.email, Password: data.password };
		const res = await Client.post("User/Login", { body: payload });
		if(!res || res.status !== 200)
		{
			return {error: "Login error"}
		} else
			return res.data
	}
)

export const signUp = createAsyncThunk(
	'auth/signUpStatus',
	async (data) => {
		let payload = { email: data.email, password: data.password, name: data.name, phoneNo: data.phoneNo };
		const res = await Client.post("User/register", { body: payload });
		if(!res || res.status !== 200)
		{
			return {error: "Registration error"}
		} else {
			let loginPayload = { email: data.email, password: data.password }
			const loginRes = Client.post("User/login", { body: loginPayload })
			if(!loginRes || loginRes.status !== 200)
			{
				return {error: "Registration succesful, error while logging in"}
			} else
				return loginRes.data
		}
	}
)

export const autoLogIn = createAsyncThunk(
	'auth/autoLogInStatus',
	async (data) => {
		console.log("autoLogIn Thunk", data);
		const res = await Client.post("User/AutoLogin", {}, data);
		if(!res || res.status !== 200)
		{
			return {error: "Login error"}
		} else
			return res.data
	}
)

const generateInitials = (fullName) => {
	let names = fullName.split(' ');
	return names.length >= 2 ? names[0][0] + names[1][0] : fullName.substring(0, 2)
}

const AuthSlice = createSlice({
	name: 'auth',
	initialState: {
		isLoaded: false,
		loggedIn: false,
		isManager: false,
		authKey: null,
		name: null,
		email: null,
		phoneNo: null,
		initials: null,
		error: null
	},
	reducers: {
		// TODO: Connect to database
		logOut(state){
			state.isLoaded = true;
			state.loggedIn = false;
			state.isManager = false;
			state.authKey = null;
			state.name = null;
			state.email = null;
			state.phoneNo = null;
			state.initials = null;
			state.error = null;

			setCookie(
				COOKIE_NAMES.required, 
				{
					...getCookie(COOKIE_NAMES.required), 
					auth:
					{
						isLoggedIn: false,
						authKey: null
					}
				}
			)
		}
	},
	extraReducers: {
		[logIn.fulfilled.type] : (state, action) => {
			if(action.payload.error)
			{
				state.loggedIn = false
				state.error = action.payload.error
			} else {
				state.isLoaded = true;
				state.loggedIn = true;
				state.authKey = action.payload.token;
				state.name = action.payload.user.name;
				state.email = action.payload.user.email;
				state.phoneNo = action.payload.user.phoneNo;
				state.initials = generateInitials(action.payload.user.name);
				state.error = null

				if(action.payload.user.role.name === "RestaurantManagerRole")
					state.isManager = true;
				else
					state.isManager = false;

				setCookie(
					COOKIE_NAMES.required, 
					{
						...getCookie(COOKIE_NAMES.required), 
						auth:
						{
							isLoggedIn: true,
							authKey: action.payload.token
						}
					}
				)
			}
		},
		[signUp.fulfilled.type] : (state, action) => {
			if(action.payload.error)
			{
				state.isLoaded = true;
				state.loggedIn = false
				state.error = action.payload.error
			} else {
				state.isLoaded = true;
				state.loggedIn = true
				state.authKey = action.payload.token;
				state.name = action.payload.user.name;
				state.email = action.payload.user.email;
				state.phoneNo = action.payload.user.phoneNo;
				state.initials = generateInitials(action.payload.user.name);
				state.error = null

				if(action.payload.user.role.name === "RestaurantManagerRole")
					state.isManager = true;
				else
					state.isManager = false;

				setCookie(
					COOKIE_NAMES.required, 
					{
						...getCookie(COOKIE_NAMES.required), 
						auth:
						{
							isLoggedIn: true,
							authKey: action.payload.token
						}
					}
				)
			}
		},
		[autoLogIn.fulfilled.type] : (state, action) => {
			if(action.payload.error)
			{
				state.loggedIn = false
				state.error = action.payload.error
			} else {
				state.isLoaded = true;
				state.loggedIn = true
				state.authKey = action.payload.token;
				state.name = action.payload.user.name;
				state.email = action.payload.user.email;
				state.phoneNo = action.payload.user.phoneNo;
				state.initials = generateInitials(action.payload.user.name);
				state.error = null

				if(action.payload.user.role.name === "RestaurantManagerRole")
					state.isManager = true;
				else
					state.isManager = false;

				setCookie(
					COOKIE_NAMES.required, 
					{
						...getCookie(COOKIE_NAMES.required), 
						auth:
						{
							isLoggedIn: true,
							authKey: action.payload.token
						}
					}
				)
			}
		}
	}
})

export const { logOut } = AuthSlice.actions
export default AuthSlice.reducer