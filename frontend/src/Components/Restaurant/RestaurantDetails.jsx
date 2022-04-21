import React from 'react';

const RestaurantDetails = (props) => {
	const {name, foodtype, score} = props.restaurant;

	return (
		<div className='restaurantDetails'>
			<h2 className='restaurantTitle'>{name}</h2>
			<p className="restaurantFoodtype">{foodtype}</p>
			{score ? <div className='scoreContainer'>
				<p className='scoreHeader'>Score: </p>
				<div className="scoreDetails">
					<div className="starContainer">
						<span className='scoreStar'>&#9733;</span>
						<span className='scoreStar'>&#9733;</span>
						<span className='scoreStar'>&#9733;</span>
						<span className='scoreStar'>&#9733;</span>
						<span className='scoreStar'>&#9733;</span>
						<div className='scoreMask' style={{
							display: 'inline-block',
							position: 'absolute',
							right: 0,
							width: (100 - (score / 5) * 100) + '%',
							backgroundColor: 'white',
							opacity: 0.75
						}}>&nbsp;</div>
					</div>
					<p className='scoreText'>{score}</p>
				</div>
			</div> : <div>No score yet</div>}
		</div>
	)
};

export default RestaurantDetails;