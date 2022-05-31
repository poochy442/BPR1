import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router';
import { useSelector } from 'react-redux';

import RestaurantDetails from '../Restaurant/RestaurantDetails';
import { Client } from '../Api/Client';
import tableMap from '../../Assets/tablemap.png';

import '../../Styles/Pages/Restaurant.scss';

const Restaurant = (props) => {
	const {manage} = props;
	const navigate = useNavigate();
	const auth = useSelector(state => state.auth);
	const [restaurantLoaded, setRestaurantLoaded] = useState(false);
	const [restaurant, setRestaurant] = useState(null);
	const [tablesLoaded, setTablesLoaded] = useState(false);
	const [tables, setTables] = useState(null);
	const [reservationPlaced, setReservationPlaced] = useState(false);
	const params = useParams();
	const restaurantId = params.restaurantId;
	
	const date = new Date();
	const minDate = date.toISOString().substring(0, date.toISOString().length - 8); // Removing ending precision to conform to yyyy-MM-dd hh-mm
	const defaultEnd = new Date(date.getTime() + (30 * 60 * 1000))
						.toISOString().substring(0, date.toISOString().length - 8); // Removing ending precision to conform to yyyy-MM-dd hh-mm
	const [input, setInput] = useState({startTime: minDate, endTime: defaultEnd, guestNo: 1, table: '', note: ''});
	const [error, setError] = useState(null);

	useEffect(() => {
		if(auth.isLoaded && !auth.loggedIn)
			navigate('/login')
		else if(auth.isLoaded && !restaurantLoaded){
			Client.get('Restaurant', {params: {id: manage ? auth.restaurantId : restaurantId}}, auth.authKey).then((res) => {
				setRestaurant(res.data.restaurants[0])
				console.log("Restaurant:", res.data.restaurants[0])
				setRestaurantLoaded(true);
			}).catch((err) => {
				console.log(err);
				setRestaurantLoaded(true);
			})

			if(manage)
			{
				Client.get('Table/tables', {params: {
					restaurantId: auth.restaurantId
				}}, auth.authKey).then((res) => {
					if(res.status !== 200){
						setError(res.data.error ? res.data.error : 'Error loading tables')
						return;
					}
					
					console.log("Tables:", res.data.tables);
					setTables(res.data.tables);
					setInput({...input, table: '1'});
					setTablesLoaded(true);
				}).catch((err) => {
					console.log(err);
				})
			}
		}
		
		return () => {
			setRestaurantLoaded(false);
			setTablesLoaded(false)
		}
		// eslint-disable-next-line react-hooks/exhaustive-deps
	}, [auth])

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

	const validateInput = (isReservation) => {
		if(input.startTime > input.endTime){
			setError("End time must be before start time");
			return false;
		}
		if(isReservation && input.table === ''){
			setError("Must select a table");
			return false;
		}
		
		return true;
	}

	const handleTableSearch = (e) => {
		e.preventDefault();
		if(!validateInput(false))
			return

		Client.get('Table/available-tables', {params: {
			restaurantId: restaurantId,
			guests: input.guestNo,
			start: input.startTime,
			end: input.endTime
		}}, auth.authKey).then((res) => {
			console.log(res);
			if(res.status !== 200){
				setError(res.data.error ? res.data.error : 'Error loading tables')
				return;
			}

			setTables(res.data.availableTables);
			setInput({...input, table: '1'});
			setTablesLoaded(true);
		}).catch((err) => {
			console.log(err);
		})
	}

	const handlePlaceReservation = (e) => {
		e.preventDefault();
		
		Client.post('Booking', {body: {
			date: input.startTime,
			startDate: input.startTime,
			endDate: input.endTime,
			guestNo: input.guestNo,
			note: input.note,
			restaurantId: restaurantId,
			tableId: input.table,
			userId: auth.userId
		}}, auth.authKey).then((res) => {
			if(res.status !== 200){
				setError(res.data.error);
				return;
			}

			setReservationPlaced(true);
		})
	}

	const handleChange = (e) => {
		if(e.target.id === 'startTime'){
			const newDate = new Date(e.target.value);
			const maxDated = new Date(input.endTime);
			const newEnd = new Date(newDate.getTime() + (150 * 60 * 1000));

			if(newDate >= maxDated)
				setInput({
					...input,
					[e.target.id]: e.target.value,
					endTime: newEnd.toISOString().substring(0, newDate.toISOString().length - 8)
				})
			else
				setInput({
					...input,
					[e.target.id]: e.target.value
				});
		} else
			setInput({
				...input,
				[e.target.id]: e.target.value
			});
	}

	const form = !tablesLoaded ? (
		<form className='reservationForm' onSubmit={handleTableSearch}>
			<label className='reservationLabel' htmlFor='startTime'>
				<p>Choose start time</p>
				<input id='startTime' className='reservationInput' type='datetime-local' value={input.startTime} min={minDate} onChange={handleChange} />
			</label>
			<label className='reservationLabel' htmlFor='endTime'>
				<p>Choose end time</p>
				<input id='endTime' className='reservationInput' type='datetime-local' value={input.endTime} min={minDate} onChange={handleChange} />
			</label>
			<label className='reservationLabel' htmlFor='guestNo'>
				<p>Specify guest No.</p>
				<input id='guestNo' className='reservationInput' type='number' value={input.guestNo} min={1} onChange={handleChange} />
			</label>
			<input className='button searchButton' type='submit' value='Search for tables' />
			{error ? <p className='errorText'>{error}</p> : null}
		</form>
	) : tables == null || tables.count === 0 ? (
		<form className='reservationForm' onSubmit={handlePlaceReservation}>
			<label className='reservationLabel' htmlFor='startTime'>
				<p>Choose start time</p>
				<input id='startTime' className='reservationInput' type='datetime-local' value={input.startTime} min={minDate} onChange={handleChange} />
			</label>
			<label className='reservationLabel' htmlFor='endTime'>
				<p>Choose end time</p>
				<input id='endTime' className='reservationInput' type='datetime-local' value={input.endTime} min={minDate} onChange={handleChange} />
			</label>
			<label className='reservationLabel' htmlFor='guestNo'>
				<p>Specify guest No.</p>
				<input id='guestNo' className='reservationInput' type='number' value={input.guestNo} min={1} onChange={handleChange} />
			</label>
			<p className='errorText'>No available tables found<br />Try again</p>
			<input className='button searchButton' type='submit' value='Search for tables' />
			{error ? <p className='errorText'>{error}</p> : null}
		</form>
	) : (
		<form className='reservationForm' onSubmit={handlePlaceReservation}>
			<label className='reservationLabel' htmlFor='startTime'>
				<p>Choose start time</p>
				<input disabled id='startTime' className='reservationInput' type='datetime-local' value={input.startTime} min={minDate} onChange={handleChange} />
			</label>
			<label className='reservationLabel' htmlFor='endTime'>
				<p>Choose end time</p>
				<input disabled id='endTime' className='reservationInput' type='datetime-local' value={input.endTime} min={minDate} onChange={handleChange} />
			</label>
			<label className='reservationLabel' htmlFor='guestNo'>
				<p>Specify guest No.</p>
				<input disabled id='guestNo' className='reservationInput' type='number' value={input.guestNo} min={1} onChange={handleChange} />
			</label>
			<label className='reservationLabel' htmlFor='table'>
				<p>Select table</p>
				<select id='table' className='reservationInput' value={input.table} onChange={handleChange}>
					{tables.map((table, index) => (
						<option key={index} value={table.tableId}>{table.tableId}</option>
					))}
				</select>
			</label>
			<img src={tableMap} alt='Map of the restaurant' className='restaurantMap' />
			<label className='reservationLabel' htmlFor='note'>
				<p>Write a note</p>
				<textarea id='note' className='reservationInput' value={input.note} onChange={handleChange} />
			</label>
			<input className='button placeButton' type='submit' value='Place reservation' />
			{error ? <p className='errorText'>{error}</p> : null}
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
				<RestaurantDetails restaurant={restaurant} miniScore={true} showSchedule={false} />
				<div className="bookingInfo">
					<div>
						<h2>Table:</h2><p> {input.table}</p>
					</div>
					<div>
						<h2>Guests:</h2><p> {input.guestNo}</p>
					</div>
					<div>
						<h2>Date:</h2><p> {input.startTime}</p>
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

	const handleAge = (tableId, age) => {
		Client.put('Table/update-age', {params: {
			tableId: tableId,
			age: age
		}}, auth.authKey).then((res) => {
			if(res.status == 200){
				
			}
		}).catch((err) => {
			console.log(err)
		})
	}

	const managerRestaurant = (
		<div className='restaurantContainer'>
			<h2>Manage restaurant</h2>
			<img src={tableMap} />
			<table className='tableTable'>
				<thead>
					<tr>
						<td>Table</td>
						<td>Seats</td>
						<td>Booking time</td>
						<td>Age</td>
						<td>Handicap</td>
						<td>Cancellation (h)</td>
						<td>Notes</td>
					</tr>
				</thead>
				<tbody>
					{tablesLoaded && tables.map((table, index) => (
						<tr className='tableItem' key={index}>
							<td>{table.tableNo}</td>
							<td>{table.bookingTimes}</td>
							<td>{table.seats}</td>
							{table.age ? (
								<td onClick={() => handleAge(table.id, !table.age)}>&#x2713;</td>
							) : (
								<td>X</td>
							)}
							{table.handicap ? (<td>&#x2713;</td>) : (<td>X</td>)}
							<td>{table.deadline.substring(11,13)}</td>
							<td>{table.notes}</td>
						</tr>
					))}
				</tbody>
			</table>
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