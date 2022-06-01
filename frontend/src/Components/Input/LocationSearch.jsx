import { useNavigate } from 'react-router';

import '../../Styles/Input/LocationSearch.scss';

function LocationSearch(props) {
	const { query, setQuery, isSearching, handleSearchSubmit } = props;
	const navigate = useNavigate();
	
	const handleChange = (e) => {
		e.preventDefault();
		setQuery(e.target.value);
	}

	const handleSubmit = (e) => {
		e.preventDefault();
		if(!isSearching)
			navigate('/search', {state: {query: query}});
		else
			handleSearchSubmit(e);
	}

	return (
		<form className="inputContainer">
			<input
				type='text'
				placeholder='Insert city...'
				className={isSearching ? 'input searching' : 'input'}
				value={query}
				onChange={handleChange} />
			<button
				type='submit'
				className='searchButton'
				onClick={handleSubmit} >
				&#128269;
			</button>
		</form>
	)
}

export default LocationSearch