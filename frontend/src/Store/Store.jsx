import { configureStore } from '@reduxjs/toolkit';
import { combineReducers } from 'redux'
import auth from './AuthSlice'

const reducer = combineReducers({
	auth: auth
})
const Store = configureStore({reducer});
export default Store