using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PedidosYa.Data
{
    public class Meal
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Meal(string name, string description, double price)
        {
            this.Name = name;
            this.Description = description;
            this.Price = price;
        }
    }
}
