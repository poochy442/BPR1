// Imports
import Header from './Layout/Header';
import Footer from './Layout/Footer';
import {BrowserRouter, Routes, Route} from 'react-router-dom';

// Pages
import Home from './Pages/Home';
import LogIn from './Pages/Auth/LogIn';
import SignUp from './Pages/Auth/SignUp';

// Style imports
import '../Styles/App.scss';
import '../Styles/Reusable/Text.scss';
import '../Styles/Reusable/Link.scss';
import '../Styles/Reusable/Avatar.scss';

const App = () => {
	return (
		<BrowserRouter>
			<Header />
			<div className='main'>
				<Routes>
					<Route exact path='/' element={<Home />} />
					<Route path='/login' element={<LogIn />} />
					<Route path='/signup' element={<SignUp />} />
				</Routes>
			</div>
			<Footer />
		</BrowserRouter>
	)
}

export default App