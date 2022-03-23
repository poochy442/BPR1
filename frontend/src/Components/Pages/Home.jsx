import React from 'react';

import { Client } from '../Api/Client'

const Home = () => {
	const data = Client.get('bookings', {}).then((res) => {
		console.log(res);
		return res;
	})

	return (
		<div className='Home'>
			
		</div>
	)
};

export default Home;