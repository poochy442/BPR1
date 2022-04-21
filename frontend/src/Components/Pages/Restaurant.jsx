import React from 'react';
import RestaurantDetails from '../Restaurant/RestaurantDetails';
import RestaurantMap from '../Restaurant/RestaurantMap';
import RestaurantMenu from '../Restaurant/RestaurantMenu';

import '../../Styles/Pages/Restaurant.scss';

const Restaurant = (props) => {
	// TODO: Change to actual restaurant
	const restaurant = {
		name: "Test restaurant",
		foodtype: "Italian",
		score: 4.3
	}

	return (
		<div className='restaurant'>
			<RestaurantDetails restaurant={restaurant} />
			<RestaurantMap />
			<RestaurantMenu />
		</div>
	)
};

export default Restaurant;