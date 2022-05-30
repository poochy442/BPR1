import { useState } from 'react';
import Score from './Score';

import '../../Styles/Restaurant/RestaurantDetails.scss';
import Reviews from './Reviews';
import { RestaurantImageByFoodType } from './Foodtype';

const RestaurantDetails = (props) => {
	const {miniScore} = props;
	const {name, address, foodType, totalScore} = props.restaurant;
	const [showReviews, setShowReviews] = useState(false);

	console.log(props.restaurant);

	return (
		<div className='restaurantDetails'>
			<RestaurantImageByFoodType foodtype={foodType} />
			<div className="information">
				<h2 className='restaurantTitle'>{name}</h2>
				<p>{address}</p>
				<p>Mn-Fr: 10:00 - 19:00</p>
				<p>St-Sn: 11:00 - 21:00</p>
			</div>
			<div className='restaurantScoreContainer' onClick={() => setShowReviews(true)}>
				<Score score={totalScore} mini={miniScore} />
			</div>
			{showReviews ? <Reviews restaurant={name} setShowReviews={setShowReviews} /> : null}
		</div>
	)
};

export default RestaurantDetails;