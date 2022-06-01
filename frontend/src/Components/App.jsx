// Imports
import {BrowserRouter, Routes, Route} from 'react-router-dom';

// Pages
import Home from './Pages/Home';
import Search from './Pages/Search';
import Bookings from './Pages/Bookings';
import Restaurant from './Pages/Restaurant';
import LogIn from './Pages/Auth/LogIn';
import SignUp from './Pages/Auth/SignUp';
import Account from './Pages/Auth/Account';

import Header from './Layout/Header';
import Footer from './Layout/Footer';
import CookieBar from './Cookies/CookieBar';

// Style imports
import '../Styles/App.scss';
import '../Styles/Reusable/Text.scss';
import '../Styles/Reusable/Link.scss';
import '../Styles/Reusable/Avatar.scss';
import '../Styles/Reusable/Input.scss';

const App = () => {
	return (
		<BrowserRouter>
			<CookieBar />
			<Header />
			<div className='main'>
				<Routes>
					<Route exact path='/' element={<Home />} />
					<Route path='/search' element={<Search />} />
					<Route path='/bookings' element={<Bookings manage={false} />} />
					<Route path='/restaurant/:restaurantId' element={<Restaurant manage={false} />} />
					<Route path='/login' element={<LogIn />} />
					<Route path='/signup' element={<SignUp />} />
					<Route path='/account' element={<Account />} />
					<Route path='/manage'>
						<Route path='bookings' element={<Bookings manage={true} />} />
						<Route path='restaurant' element={<Restaurant manage={true} />} />
					</Route>
				</Routes>
			</div>
			<Footer />
		</BrowserRouter>
	)
}

export default App