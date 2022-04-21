import React, { useState } from 'react';
import { useEffect } from 'react';
import { Navigate, useLocation } from 'react-router';

import LocationSearch from '../Input/LocationSearch';

import '../../Styles/Pages/Search.scss';

import pizzaImage from '../../Assets/Foodtypes/Pizza.jpg';
import sushiImage from '../../Assets/Foodtypes/Sushi.jpg';
import thaiImage from '../../Assets/Foodtypes/Thai.jpg';
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


const Search = () => {
	let location = useLocation(), query = location.state ? location.state.query : null;
	const guardCheck = !!query;
	const [search, setSearch] = useState([]);
	const [city, setCity] = useState(query);
	const [foodTypeFilters, setFoodTypeFilters] = useState({})
	const [advancedFilters, setAdvancedFilters] = useState({
		studyDiscount: false,
		open_24_7: false,
		LGBT: false
	})

	useEffect(() => {
		foodTypes.forEach((element) => {
			setFoodTypeFilters({...foodTypeFilters, [element.name]: false})
		})
		console.log(foodTypes);
	}, [])

	// Guard statement
	if(!guardCheck) return <Navigate to='/' />
	

	const handleSearchSubmit = (e) => {
		e.preventDefault();
		// TODO: Reset search
		console.log('Reset search');
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
								<img className="foodImage" src={foodtype.img} />
								<p className="foodName">{foodtype.name}</p>
							</div>
						))}
					</div>
					<div className="bottomRow">
						<div className="advancedFilters">
							<div
								className={advancedFilters.studyDiscount ? 'advancedOption chosen' : 'advancedOption'}
								onClick={() => {
									setAdvancedFilters({
										...advancedFilters,
										studyDiscount: !advancedFilters.studyDiscount
									})
								}}>
									Study discount
								</div>
							<div
								className={advancedFilters.open_24_7 ? 'advancedOption chosen' : 'advancedOption'}
								onClick={() => {setAdvancedFilters({
									...advancedFilters,
									open_24_7: !advancedFilters.open_24_7
								})}}>
								Open 24/7
								</div>
							<div
								className={advancedFilters.LGBT ? 'advancedOption chosen' : 'advancedOption'}
								onClick={() => {setAdvancedFilters({
									...advancedFilters,
									LGBT: !advancedFilters.LGBT
								})}}>
								LGBT-owned
								</div>
						</div>
						<button type='button' className="confirmButton" onClick={handleSearchSubmit}>
							Confirm
						</button>
					</div>
				</div>
			</div>
		</div>
	)
};

export default Search;