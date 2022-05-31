import { useState } from 'react';

import '../../Styles/Booking/ManagerBooking.scss';

const ManagerBooking = () => {
	const [bookings, setBookings] = useState([]);

	const handleManage = (id) => {
		console.log("Managing booking", id)
	}

	return (
		<div className='managerBooking'>
			<form className="filters">
				<label className="bookingFilter" htmlFor="type">
					<p>Booking type</p>
					<select className="filterInput" id="type">
						<option value="All">All</option>
						<option value="System">System</option>
						<option value="In-call">In-call</option>
					</select>
				</label>
				<label className="bookingFilter" htmlFor="table">
					<p>Table</p>
					<select className="filterInput" id="table">
						<option value="All">All</option>
						<option value="1">1</option>
						<option value="2">2</option>
						<option value="3">3</option>
					</select>
				</label>
				<label className="bookingFilter" htmlFor="date">
					<p>Date</p>
					<input className="filterInput" id="date" type='date' />
				</label>
			</form>
			<table className="bookingList">
				<tr className='listHeader'>
					<th>Booking id</th>
					<th>Type</th>
					<th>Customer</th>
					<th>Customer info</th>
					<th>Time</th>
					<th>Guests</th>
					<th>Manage</th>
				</tr>
				{bookings.map((booking, index) => (
					<tr className="bookingItem" key={index}>
						<td>{booking.id}</td>
						<td>{booking.type}</td>
						<td>{booking.customer}</td>
						<td>{booking.customerInfo}</td>
						<td>{booking.startTime} - {booking.endTime}</td>
						<td>{booking.guestNo}</td>
						<td onClick={() => handleManage(booking.id)}>&#9881;</td>
					</tr>
				))}
			</table>
		</div>
	)
}

export default ManagerBooking;