using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SoapComputerApi.Model;


namespace SoapComputerApi.Dao
{
    public static class ComputerDao
    {
        private static readonly MySqlConnection cnx = new MySqlConnection();

        public static void OpenConnection() {
            cnx.ConnectionString = "Server=local.apiseb.com;Uid=homestead;Pwd=secret;Database=homestead;";
            cnx.Open();
        }

        public static List<Computer> GetAll() {
            OpenConnection();
            var computers = new List<Computer>();
            var cmd = new MySqlCommand();
            //DB request
            cmd.CommandText = "SELECT * FROM computers";
            cmd.Connection = cnx;
            //execute and read request
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) {
                var computer = new Computer();
                computer.ID = dr.GetString("id");
                computer.Brand = dr.GetString("brand");
                computer.Model = dr.GetString("model");
                computer.Sku = dr.GetString("sku");
                computers.Add(computer);
            }
            CloseConnection();

            return computers;
        }

        public static Computer GetById(string id) {
            OpenConnection();
            var computer = new Computer();
            var cmd = new MySqlCommand();
            // DB request
            cmd.Connection = cnx;
            cmd.CommandText = "SELECT * FROM computers WHERE id=@pid";
            cmd.Parameters.AddWithValue("@pid",id);
            cmd.Prepare();
            // execute and read DB request
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) {
                computer.ID = dr.GetString("id");
                computer.Brand = dr.GetString("brand");
                computer.Model = dr.GetString("model");
                computer.Sku = dr.GetString("sku");
            }
            CloseConnection();
            return computer;
        }

        private static void CloseConnection() {
            cnx.Close();
        }
    }
}
