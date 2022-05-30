import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router';

import RestaurantDetails from '../Restaurant/RestaurantDetails';
import RestaurantMap from '../Restaurant/RestaurantMap';
import { Client } from '../Api/Client';

import '../../Styles/Pages/Restaurant.scss';

const Restaurant = (props) => {
	const {manage} = props;
	const params = useParams();
	const restaurantId = params.restaurantId;
	const navigate = useNavigate();
	const [restaurantLoaded, setRestaurantLoaded] = useState(false);
	const [restaurant, setRestaurant] = useState(null);
	const [tablesLoaded, setTablesLoaded] = useState(false);
	const [tables, setTables] = useState(null);
	const [reservationPlaced, setReservationPlaced] = useState(false);
	
	const date = new Date();
	const minDate = date.toISOString().substring(0, date.toISOString().length - 14); // Removing ending precision to conform to yyyy-MM-dd
	const [input, setInput] = useState({date: minDate, guestNo: 1, table: '', note: ''});

	useEffect(() => {
		Client.get('Restaurant/' + restaurantId, {}).then((res) => {
			setRestaurant(res.data)
			setRestaurantLoaded(true);
		}).catch((err) => {
			// TODO: Redirect to error page
			console.log(err);
			setRestaurantLoaded(true);
		})
	}, [restaurantId])

	const details = !restaurantLoaded ? (
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
		</div>	
	)

	const handleTableSearch = (e) => {
		e.preventDefault();
		// TODO: Search for tables
		setTables([{tableNo: 1, seats: 4, available: true, tableNote: ''}]);
		setInput({...input, table: '1'});
		setTablesLoaded(true);
	}

	const handlePlaceReservation = (e) => {
		e.preventDefault();
		// TODO: Place reservation
		setReservationPlaced(true);
	}

	const handleChange = (e) => {
		setInput({
			...input,
			[e.target.id]: e.target.value
		});
	}

	const form = !tablesLoaded ? (
		<form className='reservationForm' onSubmit={handleTableSearch}>
			<label className='reservationLabel' htmlFor='datePicker'>
				<p>Choose date</p>
				<input id='datePicker' className='reservationInput' type='date' value={input.date} min={minDate} onChange={handleChange} />
			</label>
			<label className='reservationLabel' htmlFor='guestNo'>
				<p>Specify guest No.</p>
				<input id='guestNo' className='reservationInput' type='number' value={input.guestNo} min={1} onChange={handleChange} />
			</label>
			<input className='button searchButton' type='submit' value='Search for tables' />
		</form>
	) : tables == null || tables.count === 0 ? (
		<form className='reservationForm' onSubmit={handlePlaceReservation}>
			<label className='reservationLabel' htmlFor='datePicker'>
				<p>Choose date</p>
				<input id='datePicker' className='reservationInput' type='date' value={input.date} min={minDate} onChange={handleChange} />
			</label>
			<label className='reservationLabel' htmlFor='guestNo'>
				<p>Specify guest No.</p>
				<input id='guestNo' className='reservationInput' type='number' value={input.guestNo} min={1} onChange={handleChange} />
			</label>
			<p className='errorText'>No available tables found<br />Try again</p>
			<input className='button searchButton' type='submit' value='Search for tables' />
		</form>
	) : (
		<form className='reservationForm' onSubmit={handlePlaceReservation}>
			<label className='reservationLabel' htmlFor='datePicker'>
				<p>Choose date</p>
				<input id='datePicker' className='reservationInput' type='date' value={input.date} min={minDate} onChange={handleChange} />
			</label>
			<label className='reservationLabel' htmlFor='guestNo'>
				<p>Specify guest No.</p>
				<input id='guestNo' className='reservationInput' type='number' value={input.guestNo} min={1} onChange={handleChange} />
			</label>
			<label className='reservationLabel' htmlFor='table'>
				<p>Select table</p>
				<select id='table' className='reservationInput' value={input.table} onChange={handleChange}>
					{tables.map((table, index) => (
						<option key={index} value={table.tableNo}>{table.tableNo}</option>
					))}
				</select>
			</label>
			<div className='restaurantMap'>
				This is the restaurant map
			</div>
			<label className='reservationLabel' htmlFor='note'>
				<p>Write a note</p>
				<textarea id='note' className='reservationInput' value={input.note} onChange={handleChange} />
			</label>
			<input className='button placeButton' type='submit' value='Place reservation' />
		</form>
	)

	const exitConfirmation = () => {
		navigate('/');
	}

	const confirmation = reservationPlaced ? (
		<div className="confirmation">
			<div className='confirmationModal'>
				<p className='closeButton' onClick={exitConfirmation}>X</p>
				<h2 className="confirmationTitle">Confirmation</h2>
				<p>You have completed the reservation</p>
				<RestaurantDetails restaurant={restaurant} miniScore={true} />
				<div className="bookingInfo">
					<div>
						<h2>Table:</h2><p> {input.table}</p>
					</div>
					<div>
						<h2>Guests:</h2><p> {input.guestNo}</p>
					</div>
					<div>
						<h2>Date:</h2><p> {input.date}</p>
					</div>
				</div>
			</div>
		</div>
	) : (null)

	const customerRestaurant = (
		<div className='restaurantContainer'>
			<h2>Table reservation</h2>
			{form}
			{confirmation}
		</div>
	)

	const managerRestaurant = (
		<div className='restaurantContainer'>
			<h2>Manage restaurant</h2>
		</div>
	)

	return (
		<div className = 'restaurant'>
			{details}
			{manage ? managerRestaurant : customerRestaurant}
		</div>
	)
};

export default Restaurant;