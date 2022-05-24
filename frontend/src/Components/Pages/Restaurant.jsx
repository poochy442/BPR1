import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router';

import RestaurantDetails from '../Restaurant/RestaurantDetails';
import RestaurantMap from '../Restaurant/RestaurantMap';
import RestaurantMenu from '../Restaurant/RestaurantMenu';
import { Client } from '../Api/Client';

import '../../Styles/Pages/Restaurant.scss';

const Restaurant = (props) => {
	const params = useParams();
	const restaurantId = params.restaurantId;
	const [loading, setLoading] = useState(true);
	const [restaurant, setRestaurant] = useState(null);

	useEffect(() => {
		Client.get('Restaurant/' + restaurantId).then((res) => {
			setRestaurant(res.data)
			setLoading(false);
		}).catch((err) => {
			console.log(err);
		})
	}, [restaurantId])

	const details = loading ? (
		<div className = 'loading'>
			<p>Loading...</p>
		</div>
	) : restaurant == null ? (
		<div className = 'notFound'>
			Restaurant not found
		</div>	
	) : (
		<div className = 'details'>
			<RestaurantDetails restaurant={restaurant} />
			<RestaurantMap />
			<RestaurantMenu />
		</div>	
	)

	return (
		<div className = 'restaurant'>
			{details}
		</div>
	)
};

export default Restaurant;