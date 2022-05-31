import { useState, useEffect } from 'react';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router';
import { Client } from '../Api/Client';

import '../../Styles/Booking/ManagerBooking.scss';

const ManagerBooking = () => {
	const auth = useSelector(state => state.auth);
	const navigate = useNavigate();
	
	const date = new Date();
	const minDate = date.toISOString().substring(0, date.toISOString().length - 14); // Removing ending precision to conform to yyyy-MM-dd
	const [input, setInput] = useState({type: "All", table: "All", date: minDate});
	const [isLoaded, setIsLoaded] = useState(false);
	const [bookings, setBookings] = useState([]);
	const [tables, setTables] = useState([]);

	useEffect(() => {
		if(auth.isLoaded && !auth.isManager)
			navigate('/Bookings');
	}, [auth])

	useEffect(() => {
		if(!isLoaded)
		{
			Client.get('Booking/bookings-for-tables', {params: {restaurantId: auth.restaurantId}}, auth.authKey).then((res) => {
				if(res.status === 200){
					setBookings(res.data.tableBookings);
					bookings.foreach(booking => {
						if(!tables.includes(booking.tableNo)){
							setTables([...tables, booking.tableNo]);
						}
					})
				}
			}).catch((err) => {
				console.log(err);
			})
		}

		return () => {
			setIsLoaded(false);
		}
	}, [])

	const handleChange = (e) => {
		setInput({
			...input,
			[e.target.id]: e.target.value
		})
	}

	const handleManage = (id) => {
		console.log("Managing booking", id)
	}

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
						{tables.map((table, index) => {
							<option key={index} value={table}>{table}</option>
						})}
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
						<th>Customer</th>
						<th>Customer info</th>
						<th>Time</th>
						<th>Guests</th>
						<th>Manage</th>
					</tr>
				</thead>
				<tbody>
					{input.table === 'All' ? bookings.map((booking, index) => (
						<tr className="bookingItem" key={index}>
							<td>{booking.id}</td>
							<td>{booking.type}</td>
							<td>{booking.customer}</td>
							<td>{booking.customerInfo}</td>
							<td>{booking.startTime} - {booking.endTime}</td>
							<td>{booking.guestNo}</td>
							<td onClick={() => handleManage(booking.id)}>&#9881;</td>
						</tr>
					)) : bookings.filter((booking => booking.tableNo == input.table)).map((booking, index) => (
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
				</tbody>
			</table>
		</div>
	)
}

export default ManagerBooking;