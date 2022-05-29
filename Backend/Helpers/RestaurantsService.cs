namespace Backend.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Backend.DataAccess.Models;
using BingMapsRESTToolkit;
using Backend.Helpers.Models.Requests;

public class RestaurantService
{
    public List<Restaurant> ExecuteDbQuery(decimal origLat, decimal origLong, int radius)
    {
        //set the connection string
        string connString = @"Data Source=LAPTOP-D5VQT9SU;Initial Catalog=BookingSystem;Integrated Security=True;";

        // variable to store the query results
        var restaurants = new List<Restaurant>();

        try
        {
            //sql connection object
            using (SqlConnection conn = new SqlConnection(connString))
            {

                // //retrieve the SQL Server instance version
                // string query = $@"DECLARE @orig_lat DECIMAL(12, 9)
                //                 DECLARE @orig_lng DECIMAL(12, 9)
                //                 SET @orig_lat={origLat} set @orig_lng={origLong}
                //                 DECLARE @orig geography = geography::Point(@orig_lat, @orig_lng, 4326);

                //                 SELECT *
                //                 FROM dbo.Addresses
                //                 where @orig.STDistance(geography::Point(Latitude, Longtitude, 4326)) < {radius}";

                string query = $@"DECLARE @orig_lat DECIMAL(12, 9)
                                DECLARE @orig_lng DECIMAL(12, 9)
                                SET @orig_lat={origLat} set @orig_lng={origLong}
                                DECLARE @orig geography = geography::Point(@orig_lat, @orig_lng, 4326);

                                SELECT  r.Id, r.Name, r.Foodtype, r.StudentDiscount, r.WorkingHours, r.TotalScore, r.AddressId, a.PostalCode, a.City, a.Street, a.StreetNo, a.Longtitude, a.Latitude
                                FROM dbo.Restaurants r join dbo.Addresses a on  r.AddressId = a.Id
                                where @orig.STDistance(geography::Point(Latitude, Longtitude, 4326)) < {radius}";

                //define the SqlCommand object
                SqlCommand cmd = new SqlCommand(query, conn);

                //open connection
                conn.Open();

                //execute the SQLCommand
                SqlDataReader dr = cmd.ExecuteReader();

                Console.WriteLine(Environment.NewLine + "Retrieving data from database..." + Environment.NewLine);
                //Console.WriteLine("Retrieved records:");

                //check if there are records
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var restaurant = new Restaurant
                        {
                            Id = dr.GetInt32(0),
                            Name = dr.GetString(1),
                            FoodType = dr.GetString(2),
                            StudentDiscount = dr.GetInt32(3),
                            WorkingHours = dr.GetString(4),
                            TotalScore = (decimal)dr.GetSqlDecimal(5),
                            Address = new Backend.DataAccess.Models.Address()
                            {
                                Id = dr.GetInt32(6),
                                PostalCode = dr.GetString(7),
                                City = dr.GetString(8),
                                Street = dr.GetString(9),
                                StreetNo = dr.GetString(10),
                                Longtitude = (decimal)dr.GetSqlDecimal(11),
                                Latitude = (decimal)dr.GetSqlDecimal(12)
                            }

                        };

                        // add restaurant
                        restaurants.Add(restaurant);
                    }
                }
                else
                {
                    Console.WriteLine("No data found.");
                }

                //close data reader
                dr.Close();

                //close connection
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            //display error message
            Console.WriteLine("Exception: " + ex.Message);
        }

        return restaurants;

    }

    public async Task<Dictionary<decimal, decimal>> GetAddressLocation(RestaurantsByLocationRequest req)
    {
        var location = new Dictionary<decimal, decimal>();

        var request = new GeocodeRequest();
        request.BingMapsKey = "ApJM9msqWQ6egjptqHy9oAMivkE6r0xuKZxr1xSzn_eWy2yGQE4AW0hA4_YcGpeD";

        request.Address = new SimpleAddress()
        {
            CountryRegion = req.Country,
            AddressLine = req.Street + " " + req.StreetNo,
            Locality = req.City,
            PostalCode = req.PostalCode,
        };

        var result = await request.Execute();
        if (result.StatusCode == 200)
        {
            var toolkitLocation = (result?.ResourceSets?.FirstOrDefault())
                ?.Resources?.FirstOrDefault() as BingMapsRESTToolkit.Location;
            var latitude = (decimal)toolkitLocation.Point.Coordinates[0];
            var longitude = (decimal)toolkitLocation.Point.Coordinates[1];

            // Use latitude and longitude
            location.Add(latitude, longitude);
        }

        return location;
    }
}