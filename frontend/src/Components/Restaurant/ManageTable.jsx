import React, { useState } from 'react';
import { useSelector } from 'react-redux';

import '../../Styles/Restaurant/ManageTable.scss';
import { Client } from '../Api/Client';

const ManageTable = (props) => {
	const { table, exit } = props;
	console.log(table);
	const auth = useSelector(state => state.auth);
	const [input, setInput] = useState({
		seats: table.seats,
		startBooking: table.bookingTimes ? table.bookingTimes : '',
		endBooking: table.bookingTimes ? table.bookingTimes : '',
		age: table.restriction ? table.restriction.age : false,
		handicap: table.restriction ? table.restriction.handicap : false,
		deadline: table.deadline.substring(11,13),
		notes: table.notes
	})
	const [hasChanged, setHasChanged] = useState({
		seats: false,
		startBooking: false,
		endBooking: false,
		age: false,
		handicap: false,
		deadline: false,
		notes: false,
	})
	const [error, setError] = useState(null);

	const handleChange = (e) => {
		if(!hasChanged[e.target.id])
			setHasChanged({...hasChanged, [e.target.id]: true});
		
		setInput({
			...input,
			[e.target.id]: e.target.value
		})
	}

	const handleConfirm = () => {
		let success = true;
		console.log(hasChanged, input);

		if(hasChanged.age){
			Client.put('Table/update-age', {params: {
				tableId: table.id,
				age: input.age === "true"
			}}, auth.authKey).then((res) => {
				if(res.status !== 200){
					setError("Error updating age");
					success = false;
				}
			}).catch((err) => {
				console.log(err)
				setError("Error updating age");
				success = false;
			})
		}

		if(hasChanged.handicap){
			Client.put('Table/update-handicap', {params: {
				tableId: table.id,
				handicap: input.handicap === "true"
			}}, auth.authKey).then((res) => {
				if(res.status !== 200){
					setError("Error updating handicap");
					success = false;
				}
			}).catch((err) => {
				console.log(err)
				setError("Error updating handicap");
				success = false;
			})
		}

		if(hasChanged.notes){
			Client.put('Table/update-notes', {params: {
				tableId: table.id,
				notes: input.notes
			}}, auth.authKey).then((res) => {
				if(res.status !== 200){
					setError("Error updating notes");
					success = false;
				}
			}).catch((err) => {
				console.log(err)
				setError("Error updating notes");
				success = false;
			})
		}
		
		if(!success)
			exit();
	}
	
	return (
		<div className='manageTable'>
			<div className="container">
				<h2>Manage table {table.tableNo}</h2>
				<label className="tableLabel" htmlFor="seats" >
					<p>Seats</p>
					<input disabled id='seats' className='tableInput' type='number' value={input.seats} onChange={handleChange} />
				</label>
				<label className="tableLabel" htmlFor="startBooking" >
					<p>Booking start</p>
					<input id='startBooking' className='tableInput' type='time' value={input.startBooking} onChange={handleChange} />
				</label>
				<label className="tableLabel" htmlFor="endBooking" >
					<p>Booking start</p>
					<input id='endBooking' className='tableInput' type='time' value={input.endBooking} onChange={handleChange} />
				</label>
				<label className="tableLabel" htmlFor="age" >
					<p>Senior discount</p>
					<input id='age' className='tableInput' type='checkbox' value={input.age} onChange={handleChange} />
				</label>
				<label className="tableLabel" htmlFor="handicap" >
					<p>Handicap accesible</p>
					<input id='handicap' className='tableInput' type='checkbox' value={input.handicap} onChange={handleChange} />
				</label>
				<label className="tableLabel" htmlFor="deadline" >
					<p>Cancellation (hours)</p>
					<input id='deadline' className='tableInput' type='number' value={input.deadline} onChange={handleChange} />
				</label>
				<label className="tableLabel" htmlFor="notes" >
					<p>Notes</p>
					<textarea id='notes' className='tableInput' value={input.notes} onChange={handleChange} />
				</label>
				{error ? <p className='errorText'>{error}</p> : null}
				<div className="buttonContainer">
					<div className="button confirm" onClick={handleConfirm}>Confirm</div>
					<div className="button cancel" onClick={() => exit()}>Cancel</div>
				</div>
			</div>
		</div>
	)
}

export default ManageTable