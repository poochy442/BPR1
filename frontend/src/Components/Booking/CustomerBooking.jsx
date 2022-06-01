import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router";
import { Client } from "../Api/Client";

import '../../Styles/Booking/CustomerBooking.scss';

const CustomerBooking = () => {
	const auth = useSelector(state => state.auth);
	const navigate = useNavigate();
	const [upcomingSelected, setUpcomingSelected] = useState(true);
	const [bookingsLoaded, setBookingsLoaded] = useState(false);
	const [upcomingBookings, setUpcomingBookings] = useState([]);
	const [previousBookings, setPreviousBookings] = useState([]);
	const [error, setError] = useState({upcoming: null, previous: null});
	const [cancelActive, setCancelActive] = useState({active: false, id: ''});
	const [isRating, setIsRating] = useState(false)
	const [ratingInput, setRatingInput] = useState({restaurantId: '', score: 3, comment: '', error: null})

	useEffect(() => {
		if(auth.isLoaded && !auth.loggedIn)
			navigate('/login');
		if(auth.isLoaded && !bookingsLoaded)
		{
			Client.get('Booking/customer-current-bookings', {}, auth.authKey).then((res) => {
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
		// eslint-disable-next-line react-hooks/exhaustive-deps
	}, [auth])

	const handleFilterClick = (toUpcoming) => {
		setUpcomingSelected(toUpcoming);
		setBookingsLoaded(false);
	}

	const handleCancel = (bookingId) => {
		if(!(cancelActive.active && cancelActive.id === bookingId)){
			setCancelActive({active: true, id: bookingId})
			return
		}
		
		Client.delete('Booking/cancel', {params: cancelActive.id}, auth.authKey).then((res) => {
			if(res.status !== 200){

			}
		})
		setCancelActive({active: false, id: ''});
	}

	const handleRating = (restaurantId) => {
		setRatingInput({...ratingInput, restaurantId: restaurantId});
		setIsRating(true);
	}

	const handleRatingChange = (e) => {
		setRatingInput({
			...ratingInput,
			[e.target.id]: e.target.value
		})
	}

	const handleRatingSubmit = (e) => {
		e.preventDefault();

		Client.post('Rating', {body: {
			score: ratingInput.score,
			comment: ratingInput.comment,
			restaurantId: ratingInput.restaurantId,
			userId: auth.userId
		}}, auth.authKey).then((res) => {
			if(res.status !== 201){
				setRatingInput({...ratingInput, error: 'Error posting review'})
				return
			}

			setIsRating(false);
			setRatingInput({restaurantId: '', score: 3, comment: '', error: null})
			setBookingsLoaded(false);
		}).catch((err) => {
			console.log(err);
			setRatingInput({...ratingInput, error: 'Error posting review'})
		})
	}

	const ratings = isRating ? (
		<div className="rating">
			<form className="container" onSubmit={handleRatingSubmit}>
				<h2>Review</h2>
				<label className="ratingLabel" htmlFor="score">
					<p>Score</p>
					<select id="score" className="ratingInput" value={ratingInput.score} onChange={handleRatingChange}>
						<option value={1}>1</option>
						<option value={2}>2</option>
						<option value={3}>3</option>
						<option value={4}>4</option>
						<option value={5}>5</option>
					</select>
				</label>
				<label className="ratingLabel" htmlFor="comment">
					<p>Comment</p>
					<textarea id="comment" className="ratingInput" value={ratingInput.comment} onChange={handleRatingChange} />
				</label>
				<button type='submit'>Submit review</button>
			</form>
		</div>
	) : null

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
						<div>
							<h3>{booking.restaurant.name}</h3>
							<p>Starts: {booking.startDate.split("T")[0]} {booking.startDate.split("T")[1]}</p>
							<p>Ends: {booking.endDate.split("T")[0]} {booking.endDate.split("T")[1]}</p>
							<p>Guests: {booking.guestNo}</p>
							<p>Note: {booking.note}</p>
						</div>
						<div className="buttonContainer">
							<div className="button cancel" onClick={() => handleCancel(booking.id)}>Cancel</div>
							{cancelActive.active && cancelActive.id === booking.id ? <p>Are you sure?<br />Press again to confirm</p> : null}
						</div>
					</div>
				))) : error.previous ? (
					<p className="errorText">{error.previous}</p>
				) : (previousBookings.map((booking, index) => (
					<div className="bookingItem" key={index}>
						<div>
							<h3>{booking.restaurant.name}</h3>
							<p>Starts: {booking.startDate.split("T")[0]} {booking.startDate.split("T")[1]}</p>
							<p>Ends: {booking.endDate.split("T")[0]} {booking.endDate.split("T")[1]}</p>
							<p>Guests: {booking.guestNo}</p>
							<p>Note: {booking.note}</p>
						</div>
						<div>
							<div className="button rate" onClick={() => handleRating(booking.restaurantId)}>Rate</div>
						</div>
						{isRating ? ratings : null}
					</div>
				)))}
			</div>
		</div>
	)
}

export default CustomerBooking;