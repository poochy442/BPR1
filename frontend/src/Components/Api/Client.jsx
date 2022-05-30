import axios from 'axios'
import { apiURL } from '../../apiURL';

export async function Client(endpoint, { method, headers, params, body}, authKey){
	return axios({
		method,
		url: apiURL + endpoint,
		headers: authKey ? {...headers, Authorization: 'Bearer ' + authKey} : headers,
		params: params,
		data: body
	}).then((res) => {
		return res
	}).catch((err) => {
		return err.response
	})
}

Client.get = function (endpoint, {headers = '', params = ''}, authKey = null ){
	return Client(endpoint, {method: 'GET', headers, params, body: ''}, authKey)
}

Client.post = function (endpoint, {headers = '', params = '', body = ''}, authKey = null ){
	console.log("Posting", body, " to ", endpoint);
	return Client(endpoint, {method: 'POST', headers, params, body}, authKey)
}

Client.put = function (endpoint, {headers = '', params = '', body = ''}, authKey = null ){
	return Client(endpoint, {method: 'PUT', headers, params, body}, authKey)
}

Client.delete = function (endpoint, {headers = '', params = '', body = ''}, authKey = null ){
	return Client(endpoint, {method: 'DELETE', headers, params, body}, authKey)
}