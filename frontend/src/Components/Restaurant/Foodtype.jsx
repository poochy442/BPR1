import pizzaImage from '../../Assets/Foodtypes/Pizza.jpg';
import asianImage from '../../Assets/Foodtypes/Asian.jpg';
import fastFoodImage from '../../Assets/Foodtypes/FastFood.jpg';
import danishImage from '../../Assets/Foodtypes/Danish.jpg';
import italianImage from '../../Assets/Foodtypes/Italian.jpg';

export const foodTypes = [
	{
		name: 'Pizza',
		img: pizzaImage
	}, {
		name: 'Asian',
		img: asianImage
	}, {
		name: 'Fast Food',
		img: fastFoodImage
	}, {
		name: 'Danish',
		img: danishImage
	}, {
		name: 'Italian',
		img: italianImage
	}
]
const getFoodTypeImage = (foodtypeName) => {
	let image = pizzaImage;

	foodTypes.forEach((foodtype) => {
		if(foodtype.name === foodtypeName)
		{
			image = foodtype.img;
		}
	})

	return image;
}
export const RestaurantImageByFoodType = (params) => {
	let src = getFoodTypeImage(params.foodtype);
	return <img src={src} className="restaurantImage" alt={'image of ' + params.foodtype} />
}