import { useState, useEffect } from 'react';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router';
import { Client } from '../Api/Client';

import '../../Styles/Booking/ManagerBooking.scss';
import ManageBooking from './ManageBooking';

const ManagerBooking = () => {
	const auth = useSelector(state => state.auth);
	const navigate = useNavigate();
	
	const date = new Date();
	const minDate = date.toISOString().substring(0, date.toISOString().length - 14); // Removing ending precision to conform to yyyy-MM-dd
	const [input, setInput] = useState({type: "All", table: "All", date: minDate});
	const [isLoaded, setIsLoaded] = useState(false);
	const [bookings, setBookings] = useState([]);
	const [tables, setTables] = useState([]);
	const [cancelState, setCancelState] = useState({isCancelling: false, bookingId: null, error: null});
	const [isReserving, setIsReserving] = useState(false);

	useEffect(() => {
		if(auth.isLoaded && !auth.isManager)
			navigate('/Bookings');
		else if(auth.isLoaded && !isLoaded)
		{
			Client.get('Booking/bookings-for-tables', {params: {restaurantId: auth.restaurantId}}, auth.authKey).then((res) => {
				if(res.status === 200){
					let resTables = res.data.tableBookings;
					let initTables = [];
					let initBookings = [];

					resTables.forEach(table => {
						if(!tables.includes(table.tableNo + ''))
							initTables = [...initTables, table.tableNo]
						
						let tableBookings = table.bookings;
						tableBookings.forEach(booking => {
							initBookings = [...initBookings, {...booking, tableNo: table.tableNo, type: 'System'}]
						})
					})
					setTables(initTables);
					setBookings(initBookings);
				}
			}).catch((err) => {
				console.log(err);
			})
		}

		return () => {
			setIsLoaded(false);
		}
		// eslint-disable-next-line react-hooks/exhaustive-deps
	}, [auth])

	const handleChange = (e) => {
		setInput({
			...input,
			[e.target.id]: e.target.value
		})
	}

	const handleManage = (id) => {
		setCancelState({isCancelling: true, bookingId: id, error: null})
	}

	const tableRow = (booking, index) => (
		<tr className="bookingItem" key={index}>
			<td>{booking.id}</td>
			<td>System</td>
			<td>{booking.tableNo}</td>
			<td>{booking.user ? booking.user.name : 'N/A'}</td>
			<td>Email: {booking.user ? booking.user.email : 'N/A'}<br />Phone: {booking.user ? booking.user.phoneNo : 'N/A'}</td>
			<td>{booking.date.substring(0, 10)}</td>
			<td>{booking.startDate.substring(11)} - {booking.endDate.substring(11)}</td>
			<td>{booking.guestNo}</td>
			<td className='cancelIcon' onClick={() => handleManage(booking.id)}>X</td>
		</tr>
	)

	const tablebody = input.type === 'All' && input.table === 'All' ? (
		<tbody>
			{bookings.map((booking, index) => tableRow(booking, index))}
		</tbody>
	) : input.type !== 'All' && input.table === 'All' ? (
		<tbody>
			{bookings.filter((booking => booking.type == input.type)).map((booking, index) => tableRow(booking, index))}
		</tbody>
	) : input.type === 'All' && input.table !== 'All' ? (
		<tbody>
			{bookings.filter((booking => booking.tableNo == input.table)).map((booking, index) => tableRow(booking, index))}
		</tbody>
	) : (
		<tbody>
			{bookings.filter((booking => booking.tableNo == input.table && booking.type == input.type)).map((booking, index) => tableRow(booking, index))}
		</tbody>	
	)

	const confirmCancel = () => {
		Client.delete('Booking/delete', {params: {bookingId: cancelState.bookingId}}, auth.authKey).then((res) => {
			if(res.status !== 200){
				setCancelState({...cancelState, error: 'Error deleting booking, please try again.'})
				return
			}
			
			Client.get('Booking/bookings-for-tables', {params: {restaurantId: auth.restaurantId}}, auth.authKey).then((res) => {
				if(res.status === 200){
					let resTables = res.data.tableBookings;
					let initTables = [];
					let initBookings = [];

					resTables.forEach(table => {
						if(!tables.includes(table.tableNo + ''))
							initTables = [...initTables, table.tableNo]
						
						let tableBookings = table.bookings;
						tableBookings.forEach(booking => {
							initBookings = [...initBookings, {...booking, tableNo: table.tableNo, type: 'System'}]
						})
					})
					setTables(initTables);
					setBookings(initBookings);
				}
			}).catch((err) => {
				console.log(err);
			})
			setCancelState({isCancelling: false, bookingId: null, error: null})
		}).catch((err) => {
			console.log(err);
			setCancelState({...cancelState, error: 'Error deleting booking, please try again.'})
		})
	}

	const handleReservationPlaced = () => {
		setIsReserving(false);
		setIsLoaded(false);
	}

	const cancelConfirmation = cancelState.isCancelling ? (
		<div className="cancelConfirmation">
			<div className="container">
				<h2>Cancel reservation</h2>
				<p>Are you sure you want to cancel this reservaiton?</p>
				{cancelState.error ? <p className='errorText'>{cancelState.error}</p> : null}
				<div className="buttonContainer">
					<div className="button confirm" onClick={confirmCancel}>Confirm</div>
					<div className="button cancel" onClick={() => setCancelState({isCancelling: false, bookingId: null, error: null})}>Cancel</div>
				</div>
			</div>
		</div>
	) : null

	return (
		<div className='managerBooking'>
			<form className="filters">
				<label className="bookingFilter" htmlFor="type">
					<p>Booking type</p>
					<select className="filterInput" id="type" value={input.type} onChange={handleChange}>
						<option value="All">All</option>
						<option value="System">System</option>
						<option value="In-call">In-call</option>
					</select>
				</label>
				<label className="bookingFilter" htmlFor="table">
					<p>Table</p>
					<select className="filterInput" id="table" value={input.table} onChange={handleChange}>
						<option value="All">All</option>
						{tables.map((table, index) => (
							<option key={index} value={table}>{table}</option>
						))}
					</select>
				</label>
				<label className="bookingFilter" htmlFor="date">
					<p>Date</p>
					<input className="filterInput" id="date" type='date' value={input.date} min={minDate} onChange={handleChange} />
				</label>
			</form>
			<table className="bookingList">
				<thead>
					<tr className='listHeader'>
						<th>Booking id</th>
						<th>Type</th>
						<th>Table</th>
						<th>Customer</th>
						<th>Customer info</th>
						<th>Date</th>
						<th>Time</th>
						<th>Guests</th>
						<th>Cancel</th>
					</tr>
				</thead>
				{tablebody}
			</table>
			{cancelConfirmation}
			<div className="newBookingButton" onClick={() => setIsReserving(true)}>Book reservation</div>
			{isReserving ? <ManageBooking restaurantId={auth.restaurantId} exit={handleReservationPlaced} /> : null}
		</div>
	)
}

export default ManagerBooking;