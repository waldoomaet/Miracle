using PedidosYa.Services;
using System;
using System.Collections.Generic;

namespace PedidosYa.Data
{
    public class Restaurant
    {
        //private static List<Restaurant> Restaurants { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Rate { get; set; }
        public List<Meal> Menu { get; set; }

        /*static Restaurant()
        {
            Restaurants = DbServices.GetAllRestaurants();
        }*/

        public Restaurant(string name, double lat, double lng, double rate, List<Meal> menu)
        {
            this.Name = name;
            this.Latitude = lat;
            this.Longitude = lng;
            this.Rate = rate;
            this.Menu = menu;
        }

        /*public static Restaurant FindRestaurantByName(string name)
        {
            return Restaurants.Find(restaurant => restaurant.Name.ToLower().Contains(name.ToLower()));
        }*/
    }
}
