using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using WebServer.Interfaces;
using WebServer.Model;
using Npgsql;
using WebServer.Utils;

namespace WebServer.DataAccessPostgreSqlProvider
{
    public partial class DataAccesPostgreSqlProvider
    {
        /// <summary>
        /// Получает список городов.
        /// </summary>
        /// <returns> Возвращает список пользователей. </returns>
        public List<City> GetCitiesList()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                try
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    string qwerty = "SELECT * FROM \"Cities\";";
                    NpgsqlCommand adapter = new NpgsqlCommand(qwerty, connection);
                    NpgsqlDataReader dr = adapter.ExecuteReader();
                    _CitiesList = new List<City>();
                    while (dr.Read())
                    {
                        City city = new City
                        {
                            idcity = Convert.ToInt32(dr[0]),
                            town = dr[1].ToString()
                        };
                        _CitiesList.Add(city);
                    }

                    return _CitiesList;
                }
                catch (Exception ex)
                {
                    Logs.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString());
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
