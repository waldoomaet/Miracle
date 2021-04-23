using PedidosYa.Data;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace PedidosYa.Services
{
    public static class DbServices
    {
        private static string ConnectionString { get; set; } = "Data Source=./LiteDb.db; Version = 3; New = True; Compress = True;";
        public static List<Restaurant> GetAllRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            List<int> ids = new List<int>();
            SQLiteConnection sqlite_conn = new SQLiteConnection(ConnectionString);
            sqlite_conn.Open();
            SQLiteDataReader rdr;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "select * from tblRestaurante";
            rdr = sqlite_cmd.ExecuteReader();
            while (rdr.Read())
            {
                int id = Convert.ToInt32((rdr["id"]));
                ids.Add(id);
                restaurants.Add(new Restaurant(id, (string)rdr["nombre"], (double)rdr["latitude"], (double)rdr["longitude"], (double)rdr["rate"]));
            }
            rdr.Close();

            foreach(int id in ids)
            {
                List<Meal> meals = new List<Meal>();
                sqlite_cmd.CommandText = $"select id, nombre, descripcion, precio from tblMenu where idRestaurante = @idResturante";
                sqlite_cmd.Parameters.AddWithValue("@idResturante", id);
                SQLiteDataReader rdr1 = sqlite_cmd.ExecuteReader();
                while (rdr1.Read())
                {
                    meals.Add(new Meal(Convert.ToInt32((rdr1["id"])), (string)rdr1["nombre"], (string)rdr1["descripcion"], (double)rdr1["precio"]));
                }
                int index = restaurants.IndexOf(restaurants.Find(restaurant => restaurant.Id == id));
                restaurants[index].Menu = meals;
            }
            sqlite_conn.Close();
            return restaurants;
        }

        public static void SaveOrder(Order order)
        {
            SQLiteConnection sqlite_conn = new SQLiteConnection(ConnectionString);
            sqlite_conn.Open();
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "insert into tblOrden ('precio') values (@precio); select last_insert_rowid();";
            sqlite_cmd.Parameters.AddWithValue("@precio", order.Bill);
            int id = Convert.ToInt32(sqlite_cmd.ExecuteScalar());
            foreach(Meal meal in order.Ordered)
            {
                sqlite_cmd.CommandText = "insert into tblMenuOrden ('idMenu', 'idOrden') values (@idMenu, @idOrden)";
                sqlite_cmd.Parameters.AddWithValue("@idMenu", meal.Id);
                sqlite_cmd.Parameters.AddWithValue("@idOrden", id);
                sqlite_cmd.ExecuteNonQuery();
            }
        }
    }
}
