import '../../Styles/Restaurant/Score.scss';

function Score(props) {
	const { score = 0, mini = false } = props;

	const scoreText = mini ? (
		<p className='scoreText'>{score.toFixed(2)}</p>
	) : (
		<p className='scoreText'>{score.toFixed(2)} with 8 reviews</p>
	)

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
			{/* TODO: Actual reviewcount */}
			{scoreText}
		</div>
	)
}

export default Score