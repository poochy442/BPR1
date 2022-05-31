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
						console.log("tableBookings", table, tableBookings)
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
		console.log("Managing booking", id)
	}

	const tableRow = (booking, index) => (
		<tr className="bookingItem" key={index}>
			<td>{booking.id}</td>
			<td>System</td>
			<td>{booking.tableNo}</td>
			<td>{booking.user.name}</td>
			<td>Email: {booking.user.email}<br />Phone: {booking.user.phoneNo}</td>
			<td>{booking.date.substring(0, 10)}</td>
			<td>{booking.startDate.substring(11)} - {booking.endDate.substring(11)}</td>
			<td>{booking.guestNo}</td>
			<td className='manageIcon' onClick={() => handleManage(booking.id)}>&#9881;</td>
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
						<th>Manage</th>
					</tr>
				</thead>
				{tablebody}
			</table>
		</div>
	)
}

export default ManagerBooking;