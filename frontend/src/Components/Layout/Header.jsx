import '../../Styles/Layout/Header.scss';
import LoginHeader from '../Auth/LoginHeader';
import { Link } from 'react-router-dom'

const Header = () => {
	return (
		<section className='header'>
			<Link to='/' className='logo'>
				Reservable
			</Link>
			<div className='nav'>
				<LoginHeader />
			</div>
		</section>
	)
}

export default Header;