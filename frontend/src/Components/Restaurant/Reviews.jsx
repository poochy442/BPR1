import { useEffect, useState } from 'react';
import '../../Styles/Restaurant/Reviews.scss';
import Score from './Score';

const Reviews = (props) => {
	const { restaurant, setShowReviews } = props;
	const [reviews, setReviews] = useState(null);

	useEffect(() => {
		// TODO: Get reviews
		setReviews(
			[{username: "Aleks Krogh", review: "I liked the restaurant", score: 4}],
			[{username: "Isabella Pedersen", review: "Wonderful experience!", score: 5}]
		)
	}, [])

	return (
		<div className='reviewsController'>
			<div className="reviews">
				<h2>{restaurant}</h2>
				{reviews && reviews.map((review, index) => (
					<div key={index} className="review">
						<h3>{review.username}</h3>
						<div className="reviewDetails">
							<p>{review.review}</p>
							<Score score={review.score} mini={true} />
						</div>
					</div>
				))}
				<p className='closeButton' onClick={() => setShowReviews(false)}>X</p>
			</div>
		</div>
	)
}

export default Reviews