import React, { useState } from 'react';
import { useEffect } from 'react';
import { Navigate, useLocation, useNavigate } from 'react-router';

import LocationSearch from '../Input/LocationSearch';

import '../../Styles/Pages/Search.scss';

import pizzaImage from '../../Assets/Foodtypes/Pizza.jpg';
import sushiImage from '../../Assets/Foodtypes/Sushi.jpg';
import thaiImage from '../../Assets/Foodtypes/Thai.jpg';
import Score from '../Restaurant/Score';
import { Client } from '../Api/Client';
const foodTypes = [
	{
		name: 'Pizza',
		img: pizzaImage
	}, {
		name: 'Sushi',
		img: sushiImage
	}, {
		name: 'Thai',
		img: thaiImage
	}
]
const restrictions = ['studyDiscount', 'open_24_7', 'LGBT']
// const restaurants = [
// 	{
// 		name: 'Pizza King',
// 		location: 'Slotsgade 10, 8700 Horsens',
// 		foodtype: 'Pizza',
// 		score: 4.25,
// 		id: 1
// 	},
// 	{
// 		name: 'McDonald\'s',
// 		location: 'Slotsgade 9, 8700 Horsens',
// 		foodtype: 'Fast Food',
// 		score: 3.4,
// 		id: 2
// 	},
// 	{
// 		name: 'Mamma Mia',
// 		location: 'Slotsgade 12, 8700 Horsens',
// 		foodtype: 'Italian',
// 		score: 2.25,
// 		id: 3
// 	}
// ]

const Search = () => {
	let routerLocation = useLocation(), query = routerLocation.state ? routerLocation.state.query : null;
	const guardCheck = !!query;
	const navigate = useNavigate();
	const [city, setCity] = useState(query);
	const [location, setLocation] = useState({lat: 10, long: 10});
	const [restaurants, setRestaurants] = useState([]);
	const [foodTypeFilters, setFoodTypeFilters] = useState({})
	const [restrictionFilters, setRestrictionFilters] = useState({})

	useEffect(async () => {
		foodTypes.forEach((element) => {
			setFoodTypeFilters({...foodTypeFilters, [element.name]: false})
		})
		restrictions.forEach((element) => {
			setRestrictionFilters({...restrictionFilters, [element]: false})
		})
		let newRestaurants = await searchRestaurants(collectFilters());
		console.log("New restaurants", newRestaurants);
		setRestaurants(newRestaurants);
	}, [])

	// Guard statement
	if(!guardCheck) return <Navigate to='/' />
	

	const handleSearchSubmit = (e) => {
		e.preventDefault();
		setRestaurants(searchRestaurants(collectFilters()));
	}

	const handleRestaurantClick = (restaurant) => {
		navigate("/restaurant/" + restaurant.id);
	}

	const collectFilters = () => {
		let selectedfoodTypeFilter = {}
		foodTypes.forEach((element) => {
			if(foodTypeFilters[element])
				selectedfoodTypeFilter = {...selectedfoodTypeFilter, [element.name]: true}
		})

		let selectedrestrictionFilter = {}
		restrictions.forEach((element) => {
			if(restrictionFilters[element])
				selectedrestrictionFilter = {...selectedrestrictionFilter, [element]: true}
		})

		return {
			location: location,
			foodtype: selectedfoodTypeFilter,
			advanced: selectedrestrictionFilter
		};
	}

	const searchRestaurants = async (filters) => {
		const res = await Client.post("Restaurant/Search", {}, filters);
		console.log('search data', res.data);
		return res.data;
	}

	return (
		<div className='search'>
			<div className='locationSearchContainer'>
				<LocationSearch query={city} setQuery={setCity} isSearching={true} handleSearchSubmit={handleSearchSubmit} />
			</div>
			<div className='searchContainer'>
				<div className="filterContainer">
					<h2>Food types</h2>
					<div className="foodtypeFilters">
						{foodTypes.map((foodtype, index) => (
							<div
								key={index}
								className={foodTypeFilters[foodtype.name] ? "foodtypeOption chosen" : 'foodtypeOption'}
								onClick={() => {
									setFoodTypeFilters({...foodTypeFilters, [foodtype.name]: !foodTypeFilters[foodtype.name]});
								}}>
								<img className="foodImage" src={foodtype.img} alt={foodtype.name} />
								<p className="foodName">{foodtype.name}</p>
							</div>
						))}
					</div>
					<div className="bottomRow">
						<div className="advancedFilters">
							<div
								className={restrictionFilters.studyDiscount ? 'advancedOption chosen' : 'advancedOption'}
								onClick={() => {
									setRestrictionFilters({
										...restrictionFilters,
										studyDiscount: !restrictionFilters.studyDiscount
									})
								}}>
									Study discount
								</div>
							<div
								className={restrictionFilters.open_24_7 ? 'advancedOption chosen' : 'advancedOption'}
								onClick={() => {setRestrictionFilters({
									...restrictionFilters,
									open_24_7: !restrictionFilters.open_24_7
								})}}>
								Open 24/7
								</div>
							<div
								className={restrictionFilters.LGBT ? 'advancedOption chosen' : 'advancedOption'}
								onClick={() => {setRestrictionFilters({
									...restrictionFilters,
									LGBT: !restrictionFilters.LGBT
								})}}>
								LGBT-owned
								</div>
						</div>
						<button type='button' className="confirmButton" onClick={handleSearchSubmit}>
							Confirm
						</button>
					</div>
				</div>

				{!restaurants ? (
					<div className='error'>
						Error
					</div>
				) : restaurants.length === 0 ? (
					<div className='notFound'>
						No restaurants found, please try again.
					</div>
				) : (
					<div className="resultContainer">
						{restaurants.map(((restaurant, index) => (
							<div key={index} className="restaurantContainer" onClick={() => handleRestaurantClick(restaurant)}>
								<div className="restaurantImage"></div>
								<div className="restaurantDetails">
									<p className="foodName"><b>{restaurant.name}</b></p>
									<p className="location">{restaurant.location}</p>
									<Score score={restaurant.score} />
								</div>
							</div>
						)))}
					</div>
				)}
			</div>
		</div>
	)
};

export default Search;