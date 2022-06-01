import { useState } from 'react'
import { useSelector } from 'react-redux';
import '../../Styles/Booking/ManageBooking.scss'
import { Client } from '../Api/Client';
import tableMap from '../../Assets/tablemap.png';

const ManageBooking = (props) => {
	const { restaurantId, exit } = props;
	const auth = useSelector(state => state.auth);
	const date = new Date();
	const minDate = date.toISOString().substring(0, date.toISOString().length - 8); // Removing ending precision to conform to yyyy-MM-dd hh-mm
	const defaultEnd = new Date(date.getTime() + (30 * 60 * 1000))
						.toISOString().substring(0, date.toISOString().length - 8); // Removing ending precision to conform to yyyy-MM-dd hh-mm
	
	const [input, setInput] = useState({
		startTime: minDate,
		endTime: defaultEnd,
		guestNo: 1,
		note: '',
		tableNo: 1
	});
	const [error, setError] = useState(null)
	const [tableNosLoaded, setTableNosLoaded] = useState(false);
	const [tableNos, setTableNos] = useState(null);

	const handleChange = (e) => {
		if(e.target.id === 'startTime'){
			const newDate = new Date(e.target.value);
			const maxDate = new Date(input.endTime);
			const newEnd = new Date(newDate.getTime() + (120 * 60 * 1000));

			if(newDate >= maxDate)
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
			})
	}

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

			setTableNos(res.data.availableTables);
			setInput({...input, table: '1'});
			setTableNosLoaded(true);
		}).catch((err) => {
			console.log(err);
		})
	}

	const handlePlaceReservation = (e) => {
		e.preventDefault();
		
		Client.post('Booking/incall-booking', {body: {
			date: input.startTime,
			startDate: input.startTime,
			endDate: input.endTime,
			guestNo: input.guestNo,
			note: input.note,
			restaurantId: restaurantId,
			tableId: input.table
		}}, auth.authKey).then((res) => {
			if(res.status !== 200){
				setError(res.data.error);
				return;
			}

			exit();
		})
	}

	return (
		<div className='manageBooking'>
			{!tableNosLoaded ? (
				<form className='reservationForm' onSubmit={handleTableSearch}>
					<h2>Table reservation</h2>
					<label className='reservationLabel' htmlFor='startTime'>
						<p>Start time</p>
						<input id='startTime' className='reservationInput' type='datetime-local' value={input.startTime} min={minDate} onChange={handleChange} />
					</label>
					<label className='reservationLabel' htmlFor='endTime'>
						<p>End time</p>
						<input id='endTime' className='reservationInput' type='datetime-local' value={input.endTime} min={minDate} onChange={handleChange} />
					</label>
					<label className='reservationLabel' htmlFor='guestNo'>
						<p>Specify guest No.</p>
						<input id='guestNo' className='reservationInput' type='number' value={input.guestNo} min={1} onChange={handleChange} />
					</label>
					<div className="buttonContainer">
						<input className='button searchButton' type='submit' value='Search for tables' />
						<div className='button cancelButton' onClick={exit}>Cancel</div>
					</div>
					{error ? <p className='errorText'>{error}</p> : null}
				</form>
			) : tableNos == null || tableNos.count === 0 ? (
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
					<p className='errorText'>No available tables found<br />Try again</p>
					<div className="buttonContainer">
						<input className='button searchButton' type='submit' value='Search for tables' />
						<div className='button cancelButton' onClick={exit}>Cancel</div>
					</div>
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
							{tableNos.map((table, index) => (
								<option key={index} value={table.tableId}>{table.tableNo}</option>
							))}
						</select>
					</label>
					<img src={tableMap} alt='Map of the restaurant' className='restaurantMap' />
					<label className='reservationLabel' htmlFor='note'>
						<p>Write a note</p>
						<textarea id='note' className='reservationInput' value={input.note} onChange={handleChange} />
					</label>
					<div className="buttonContainer">
						<input className='button placeButton' type='submit' value='Place reservation' />
						<div className='button cancelButton' onClick={exit}>Cancel</div>
					</div>
					{error ? <p className='errorText'>{error}</p> : null}
				</form>
			)}
		</div>
	)
}

export default ManageBooking