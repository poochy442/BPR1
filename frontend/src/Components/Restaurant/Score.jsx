import React from 'react'

import '../../Styles/Restaurant/Score.scss';

function Score(props) {
	const { score } = props;

	return (
		<div className="scoreContainer">
			<div className="starContainer">
				<span className='scoreStar'>&#9733;</span>
				<span className='scoreStar'>&#9733;</span>
				<span className='scoreStar'>&#9733;</span>
				<span className='scoreStar'>&#9733;</span>
				<span className='scoreStar'>&#9733;</span>
				<div
					className='scoreMask'
					style={{width: (100 - (score / 5) * 100) + '%'}} >
					&nbsp;
				</div>
			</div>
			<p className='scoreText'>{score}</p>
		</div>
	)
}

export default Score