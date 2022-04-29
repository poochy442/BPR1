// await PostBooking(new Booking(DateTime.Now, DateTime.Now.AddDays(1), 2));
// await PostBooking(new Booking(DateTime.Now, DateTime.Now.AddDays(1), 6));
// await PostBooking(new Booking(DateTime.Now, DateTime.Now.AddDays(1), 4)); 

// await PostRating(new Rating(1, 4, "Pretty good service, decent food"));
// await PostRating(new Rating(2, 4, "Pretty good service, decent food"));
// await PostRating(new Rating(3, 4, "Pretty good service, decent food"));
// await PostRating(new Rating(1, 2, "Pretty bad service"));
// await PostRating(new Rating(3, 2, "Pretty bad service"));
// await PostRating(new Rating(1, 1, "Absolutely awful"));
// await PostRating(new Rating(2, 1, "Absolutely awful"));

// List<string> ft1 = new List<string>(new string[] {"Kebab", "Pizza", "Durum"});
// string ft1String = JsonSerializer.Serialize(ft1);
// List<long> t1 = new List<long>(new long[] {1, 2, 3, 4});
// string t1String = JsonSerializer.Serialize(t1);
// await PostRestaurant(new Restaurant(1, t1String, "Sundvej 8", 12.8f, 28f, "Delon\'s", ft1String));
// List<string> ft2 = new List<string>(new string[] {"Fastfood", "Dessert"});
// string ft2String = JsonSerializer.Serialize(ft2);
// List<long> t2 = new List<long>(new long[] {5, 6, 7, 8, 9, 10});
// string t2String = JsonSerializer.Serialize(t2);
// await PostRestaurant(new Restaurant(2, t2String, "Sundvej 10", 12.9f, 28f, "McDonald\'s", ft2String));
// List<string> ft3 = new List<string>(new string[] {"Ribs", "Burger"});
// string ft3String = JsonSerializer.Serialize(ft3);
// List<long> t3 = new List<long>(new long[] {11, 12, 13});
// string t3String = JsonSerializer.Serialize(t3);
// await PostRestaurant(new Restaurant(3, t3String, "Sundvej 12", 13f, 28f, "Bone\'s", ft3String));

// List<string> r1 = new List<string>(new string[] {"Handicap"});
// string r1String = JsonSerializer.Serialize(r1);
// List<string> r2 = new List<string>(new string[] {"Senior"});
// string r2String = JsonSerializer.Serialize(r2);
// // Delon's tables
// await PostTable(1, new Table(1, 4, true, "", r1String));
// await PostTable(1, new Table(2, 4, true, "", r2String));
// await PostTable(1, new Table(3, 2, true, "", ""));
// await PostTable(1, new Table(4, 6, true, "", ""));
// // McDonald's tables
// await PostTable(2, new Table(1, 4, true, "", r1String));
// await PostTable(2, new Table(2, 4, true, "", r2String));
// await PostTable(2, new Table(3, 2, true, "", ""));
// await PostTable(2, new Table(4, 2, true, "", ""));
// await PostTable(2, new Table(5, 8, true, "", ""));
// await PostTable(2, new Table(6, 6, true, "", ""));
// // Bone's tables
// await PostTable(3, new Table(1, 4, true, "", r1String));
// await PostTable(3, new Table(2, 2, true, "", ""));
// await PostTable(3, new Table(3, 2, true, "", ""));