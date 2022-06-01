import { useState } from 'react';
import Score from './Score';

import '../../Styles/Restaurant/RestaurantDetails.scss';
import Reviews from './Reviews';
import { RestaurantImageByFoodType } from './Foodtype';

const getDayFromIndex = (index) => {
	switch(index){
		case 0:
			return 'Mon';
		case 1:
			return 'Tue';
		case 2:
			return 'Wed';
		case 3:
			return 'Thu';
		case 4:
			return 'Fri';
		case 5:
			return 'Sat';
		default:
			return 'Sun';
	}
}

const RestaurantDetails = (props) => {
	const {miniScore, showSchedule = true} = props;
	const {name, address, workingHours, foodType, totalScore} = props.restaurant;
	const [workingHourList] = useState(JSON.parse(workingHours));
	const [showReviews, setShowReviews] = useState(false);

	return (
		<div className='restaurantDetails'>
			<RestaurantImageByFoodType foodtype={foodType} />
			<div className="information">
				<h2 className='restaurantTitle'>{name}</h2>
				<p>{address.street} {address.streetNo}, {address.postalCode} {address.city}</p>
				{showSchedule ? <p>{getDayFromIndex(workingHourList[0].Day)}: {workingHourList[0].From} - {workingHourList[0].Till}</p> : null}
				{showSchedule ? <p>{getDayFromIndex(workingHourList[1].Day)}: {workingHourList[1].From} - {workingHourList[1].Till}</p> : null}
				{showSchedule ? <p>{getDayFromIndex(workingHourList[2].Day)}: {workingHourList[2].From} - {workingHourList[2].Till}</p> : null}
				{showSchedule ? <p>{getDayFromIndex(workingHourList[3].Day)}: {workingHourList[3].From} - {workingHourList[3].Till}</p> : null}
				{showSchedule ? <p>{getDayFromIndex(workingHourList[4].Day)}: {workingHourList[4].From} - {workingHourList[4].Till}</p> : null}
				{showSchedule ? <p>{getDayFromIndex(workingHourList[5].Day)}: {workingHourList[5].From} - {workingHourList[5].Till}</p> : null}
				{showSchedule ? <p>{getDayFromIndex(workingHourList[6].Day)}: {workingHourList[6].From} - {workingHourList[6].Till}</p> : null}
			</div>
			<div className='restaurantScoreContainer' onClick={() => setShowReviews(true)}>
				<Score score={totalScore} mini={miniScore} />
			</div>
			{showReviews ? <Reviews restaurant={name} setShowReviews={setShowReviews} /> : null}
		</div>
	)
};

export default RestaurantDetails;