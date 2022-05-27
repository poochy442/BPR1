import Score from './Score';

import '../../Styles/Restaurant/RestaurantDetails.scss';

const RestaurantDetails = (props) => {
	const {miniScore} = props;
	const {name, address, totalScore} = props.restaurant;

	return (
		<div className='restaurantDetails'>
			<span className='restaurantImage'>This is the restaurant Image</span>
			<div className="information">
				<h2 className='restaurantTitle'>{name}</h2>
				<p>{address}</p>
				<p>Mn-Fr: 10:00 - 19:00</p>
				<p>St-Sn: 11:00 - 21:00</p>
			</div>
			<Score score={totalScore} mini={miniScore} />
		</div>
	)
};

export default RestaurantDetails;