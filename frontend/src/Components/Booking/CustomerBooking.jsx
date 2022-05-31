import { useEffect } from "react";
import { useState } from "react";

import RestaurantDetails from "../Restaurant/RestaurantDetails";
import { Client } from "../Api/Client";

import '../../Styles/Booking/CustomerBooking.scss';

const CustomerBooking = () => {
	const [upcomingSelected, setUpcomingSelected] = useState(true);
	const [restaurantsLoaded, setRestaurantsLoaded] = useState(false);
	const [restaurants, setRestaurants] = useState([]);

	useEffect(() => {
		// TODO: Call booking endpoint
		if(!restaurantsLoaded)
		{
			var upcomingRestaurant = {
				"id": 5,
				"managerId": 1,
				"tableIds": "[1,2,3,4]",
				"address": "Upcoming",
				"latitude": 12.8,
				"longitude": 28,
				"name": "Upcoming",
				"foodTypes": "[\"Kebab\",\"Pizza\",\"Durum\"]",
				"totalScore": 0
			}
			var previousRestaurant = {
				"id": 5,
				"managerId": 1,
				"tableIds": "[1,2,3,4]",
				"address": "Previous",
				"latitude": 12.8,
				"longitude": 28,
				"name": "Previous",
				"foodTypes": "[\"Kebab\",\"Pizza\",\"Durum\"]",
				"totalScore": 0
			}

			Client.get('Restaurant').then((res) => {
				if(upcomingSelected)
					setRestaurants([...res.data, upcomingRestaurant])
				else
					setRestaurants([...res.data, previousRestaurant])
				setRestaurantsLoaded(true);
			}).catch((err) => {
				setRestaurantsLoaded(true);
			})
		}
	}, [upcomingSelected])

	const handleFilterClick = () => {
		setUpcomingSelected(!upcomingSelected);
		setRestaurantsLoaded(false);
	}
	

	return (
		<div className='customerBooking'>
			<div className="filters">
				<div
					className={upcomingSelected ? "filter active" : "filter"}
					onClick={handleFilterClick}
					>
					Upcoming reservations
				</div>
				<div
					className={upcomingSelected ? "filter" : "filter active"}
					onClick={handleFilterClick}
					>
					Previous reservations
				</div>
			</div>
			<div className="bookingList">
				{restaurants.map((restaurant, index) => (
					<div className="restaurantItem" key={index}>
						<RestaurantDetails restaurant={restaurant} miniScore={true} />
					</div>
				))}
			</div>
		</div>
	)
}

export default CustomerBooking;