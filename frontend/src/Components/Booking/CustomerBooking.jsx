import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router";
import { Client } from "../Api/Client";

import '../../Styles/Booking/CustomerBooking.scss';

const CustomerBooking = () => {
	const auth = useSelector(state => state.auth);
	const navigate = useNavigate();
	const [hasLoaded, setHasLoaded] = useState(false);
	const [upcomingSelected, setUpcomingSelected] = useState(true);
	const [bookingsLoaded, setBookingsLoaded] = useState(false);
	const [upcomingBookings, setUpcomingBookings] = useState([]);
	const [previousBookings, setPreviousBookings] = useState([]);
	const [error, setError] = useState({upcoming: null, previous: null});

	useEffect(() => {
		if(auth.isLoaded && !auth.loggedIn){
			navigate('/login');
		}
	}, [auth])

	useEffect(() => {
		if(!bookingsLoaded && auth.isLoaded)
		{
			Client.get('Booking/customer-current-bookings', {}, auth.authKey).then((res) => {
				console.log(res);
				if(res.status !== 200){
					setError("Error loading bookings, please try again.");
					return;
				}
				setUpcomingBookings(res.data.bookings);
				setError({...error, upcoming: null});
			}).catch((err) => {
				console.log(err);
				setError({...error, upcoming: "Error loading bookings, please try again."});
			})

			Client.get('Booking/customer-past-bookings', {}, auth.authKey).then((res) => {
				if(res.status !== 200){
					setError("Error loading bookings, please try again.");
					return;
				}
				setPreviousBookings(res.data.bookings)
				setError({...error, previous: null});
			}).catch((err) => {
				console.log(err);
				setError({...error, previous: "Error loading bookings, please try again."});
			})

			setBookingsLoaded(true);
		}

		return () => {
			setBookingsLoaded(false);
		}
	}, [])

	const handleFilterClick = (toUpcoming) => {
		setUpcomingSelected(toUpcoming);
		setBookingsLoaded(false);
	}
	

	return (
		<div className='customerBooking'>
			<div className="filters">
				<div
					className={upcomingSelected ? "filter active" : "filter"}
					onClick={() => handleFilterClick(true)}
					>
					Upcoming reservations
				</div>
				<div
					className={upcomingSelected ? "filter" : "filter active"}
					onClick={() => handleFilterClick(false)}
					>
					Previous reservations
				</div>
			</div>
			<div className="bookingList">
				{upcomingSelected && error.upcoming ? (
					<p className="errorText">{error.upcoming}</p>
				) : upcomingSelected ? (upcomingBookings.map((booking, index) => (
					<div className="bookingItem" key={index}>
						<h3>{booking.restaurant.name}</h3>
						<p>Starts: {booking.startDate.split("T")[0]} {booking.startDate.split("T")[1]}</p>
						<p>Ends: {booking.endDate.split("T")[0]} {booking.endDate.split("T")[1]}</p>
						<p>Guests: {booking.guestNo}</p>
						<p>Note: {booking.note}</p>
					</div>
				))) : error.previous ? (
					<p className="errorText">{error.previous}</p>
				) : (previousBookings.map((booking, index) => (
					<div className="bookingItem" key={index}>
						<h3>{booking.restaurant.name}</h3>
						<p>Starts: {booking.startDate}</p>
						<p>Ends: {booking.endDate}</p>
						<p>Guests: {booking.guestNo}</p>
						<p>Note: {booking.note}</p>
					</div>
				)))}
			</div>
		</div>
	)
}

export default CustomerBooking;