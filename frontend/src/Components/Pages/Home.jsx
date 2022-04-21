import { useState } from 'react';
import '../../Styles/Pages/Home.scss';
import LocationSearch from '../Input/LocationSearch';

const Home = () => {
	const [query, setQuery] = useState('');

	return (
		<div className='home'>
			<div className='searchContainer'>
				<h3>Find restaurant. Type in your address!</h3>
				<LocationSearch query={query} setQuery={setQuery} isSearching={false} />
			</div>
			<div className='searchDescription'>
				<div className='descriptionContainer'>
					<h4>1. Type in your address</h4>
					<p>Enter your address, city, and/or post code</p>
				</div>
				<div className='descriptionContainer middle'>
					<h4>2. Choose the restaurant</h4>
					<p>
						What restaurants do you like?
						Go through the list and choose one.
					</p>
				</div>
				<div className='descriptionContainer'>
					<h4>3. Reserve a table</h4>
					<p>
						Choose a table to be seated at, when you want to reserve it, and how many guests you are.
					</p>
				</div>
			</div>
		</div>
	)
};

export default Home;