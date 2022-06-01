import { useState } from 'react';
import { useEffect } from 'react';
import { Navigate, useLocation, useNavigate } from 'react-router';

import LocationSearch from '../Input/LocationSearch';
import { Client } from '../Api/Client';
import Score from '../Restaurant/Score';
import { foodTypes, RestaurantImageByFoodType } from '../Restaurant/Foodtype';

import '../../Styles/Pages/Search.scss';

const restrictions = ['senior', 'handicap']

const Search = () => {
	let routerLocation = useLocation(), query = routerLocation.state ? routerLocation.state.query : null;
	const guardCheck = !!query;
	const navigate = useNavigate();
	const [city, setCity] = useState(query);
	// const [location, setLocation] = useState({lat: 10, long: 10});
	const [restaurants, setRestaurants] = useState([]);
	const [foodTypeFilters, setFoodTypeFilters] = useState({})
	const [restrictionFilters, setRestrictionFilters] = useState({})
	// const [searchRadius, setSearchRadius] = useState(1000);

	useEffect(() => {
		async function getRestaurants()
		{
			setRestaurants(await searchRestaurants());
		}

		// TODO: Call backend for food types and restrictions
		foodTypes.forEach((element) => {
			setFoodTypeFilters({...foodTypeFilters, [element.name]: false})
		})
		restrictions.forEach((element) => {
			setRestrictionFilters({...restrictionFilters, [element]: false})
		})
		getRestaurants();
		// eslint-disable-next-line react-hooks/exhaustive-deps
	}, [])

	// Guard statement
	if(!guardCheck) return <Navigate to='/' />
	

	const handleSearchSubmit = (e) => {
		e.preventDefault();
		setRestaurants(searchRestaurants());
	}

	const handleRestaurantClick = (restaurant) => {
		navigate("/restaurant/" + restaurant.id);
	}

	const searchRestaurants = async () => {
		const res = await Client.get("Restaurant/restaurants", {params: { city: city }});
		if(res.status === 200)
			return res.data
		else
			return [];
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
						{/* <label className="searchRadiusInput" htmlFor='searchRadius'>
							<p>Search radius (m)</p>
							<input id='searchRadius' type='number' value={searchRadius} onChange={(e) => setSearchRadius(e.target.value)} />
						</label> */}
						<button type='button' className="confirmButton" onClick={handleSearchSubmit}>
							Search
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
					<div>
						<h2 className='resultTitle'>Restaurants</h2>
						<div className="resultContainer">
							{restaurants && restaurants.map(((restaurant, index) => (
								<div key={index} className="restaurantCard" onClick={() => handleRestaurantClick(restaurant)}>
									<RestaurantImageByFoodType foodtype={restaurant.foodType} />
									<div className="restaurantInformation">
										<p className="restaurantName"><b>{restaurant.name}</b></p>
										<p className="location">{restaurant.address.street} {restaurant.address.streetNo}, {restaurant.address.postalCode} {restaurant.address.city}</p>
									</div>
									<Score score={restaurant.totalScore} mini={true}/>
								</div>
							)))}
						</div>
					</div>
				)}
			</div>
		</div>
	)
};

export default Search;