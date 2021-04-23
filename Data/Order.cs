using System;
using System.Collections.Generic;

namespace PedidosYa.Data
{
    public class Order
    {
        public Restaurant Restaurant { get; set; }
        public double Bill { get; set; } = 0;
        public List<Meal> Ordered { get; set; } = new List<Meal>();
        public Order(Restaurant restaurant) => this.Restaurant = restaurant;
        public void Add(Meal meal)
        {
            this.Ordered.Add(meal);
            this.Bill += meal.Price;
        }
        public void Remove(Meal meal)
        {
            this.Bill -= meal.Price;
            this.Ordered.Remove(meal);
        }
    }
}
