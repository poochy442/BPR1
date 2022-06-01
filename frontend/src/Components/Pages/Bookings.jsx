import '../../Styles/Pages/Booking.scss';
import CustomerBooking from '../Booking/CustomerBooking';
import ManagerBooking from '../Booking/ManagerBooking';

const Bookings = (props) => {
	const { manage } = props;

	return (
		<div className='booking'>
			<h1>Reservations</h1>
			{manage ? <ManagerBooking /> : <CustomerBooking />}
		</div>
	)
};

export default Bookings;