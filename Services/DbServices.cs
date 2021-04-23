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
        private static string ConnectionString { get; set; } = "Data Source=D:\\Users\\waldo\\Desktop\\PedidosYa\\LiteDb; Version = 3; New = True; Compress = True;";
        public static List<Restaurant> GetAllRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            SQLiteConnection sqlite_conn = new SQLiteConnection(ConnectionString);
            sqlite_conn.Open();
            SQLiteDataReader rdr;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "select * from tblRestaurante";
            rdr = sqlite_cmd.ExecuteReader();
            while (rdr.Read())
            {
                List<Meal> meals = new List<Meal>();
                int id = (int)rdr["id"];
                SQLiteDataReader rdr1;
                sqlite_cmd.CommandText = "select nombre, descripcion, precio from tblMenu where idRestaurante = @idRestaurante";
                sqlite_cmd.Parameters.AddWithValue("@idRestaurante", id);
                rdr1 = sqlite_cmd.ExecuteReader();
                while (rdr1.Read())
                {
                    meals.Add(new Meal((string)rdr1["nombre"], (string)rdr1["descripcion"], (double)rdr1["precio"]));
                }
                restaurants.Add(new Restaurant((string)rdr["nombre"], (double)rdr["latitude"], (double)rdr["longitude"], (double)rdr["rate"], meals));
            }
            sqlite_conn.Close();
            return restaurants;
        }
    }
}
