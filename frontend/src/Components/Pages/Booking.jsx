import { useState } from 'react';
import '../../Styles/Pages/Booking.scss';
import CustomerBooking from '../Booking/CustomerBooking';
import ManagerBooking from '../Booking/ManagerBooking';

const Booking = (props) => {
	const { manage } = props;

	return (
		<div className='booking'>
			<h1>Reservations</h1>
			{manage ? <ManagerBooking /> : <CustomerBooking />}
		</div>
	)
};

export default Booking;