import axios from 'axios'

export async function Client(endpoint, { method, params, body}){
	return axios({
		method,
		url: URL + endpoint,
		params,
		body
	}).then((res) => {
		return res
	}).catch((err) => {
		return err.response
	})
}

Client.get = function (endpoint, params = '' ){
	return Client(endpoint, {method: 'GET', params, body: ''})
}

Client.post = function (endpoint, params = '', body = '' ){
	return Client(endpoint, {method: 'POST', params, body})
}

Client.put = function (endpoint, params = '', body = ''){
	return Client(endpoint, {method: 'PUT', params, body})
}

Client.delete = function (endpoint, params = '', body = ''){
	return Client(endpoint, {method: 'DELETE', params, body})
}