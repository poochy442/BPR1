import React from 'react';
import Score from './Score';

const RestaurantDetails = (props) => {
	const {name, foodtype, score} = props.restaurant;

	return (
		<div className='restaurantDetails'>
			<h2 className='restaurantTitle'>{name}</h2>
			<p className="restaurantFoodtype">{foodtype}</p>
			<Score score={score} />
		</div>
	)
};

export default RestaurantDetails;