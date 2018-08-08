using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using WebServer.Interfaces;
using WebServer.Model;
using Npgsql;
using WebServer.Utils;

namespace WebServer.DataAccessPostgreSqlProvider
{
    public partial class DataAccesPostgreSqlProvider : IPersonBase
    {
        private List<Person> _PersonsList;
        private List<City> _CitiesList;

        private string connectionString;

        public DataAccesPostgreSqlProvider()
        {
            connectionString = "User ID=postgres;Password=gt5000;Host=localhost;Port=5432;Database=base_people;Pooling=true;";
        }

        /// <summary>
        /// Добавляет нового пользователя в базу данных.
        /// </summary>
        /// <param name="person"> Обьект Person. </param>
        /// <returns></returns>
        public void Insert(Person person)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                try
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    string inquiryForIdCity = "(SELECT idcity FROM \"Cities\" WHERE town = '" + person.city + "')";
                    NpgsqlCommand adapter = new NpgsqlCommand("INSERT INTO \"Persons\"" + @"( name
                                                                                            , dateofbirth
			                                                                                , idcity
                                                                                            )
                                                               VALUES ( '" + person.name + @"'
	                                                                  , '" + person.dateofbirth + @"'
	                                                                  , " + inquiryForIdCity + @");", connection);
                    adapter.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Logs.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Изменяет информаю о существующем пользователе.
        /// </summary>
        /// <param name="person"> Обьект Person. </param>
        /// <returns></returns>
        public void Update(Person person)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                try
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    string inquiryForIdCity = "(SELECT idcity FROM \"Cities\" WHERE town = '" + person.city + "')";
                    NpgsqlCommand adapter = new NpgsqlCommand("UPDATE \"Persons\" SET name = '" + person.name + @"'
			                                                                        , dateofbirth = '" + person.dateofbirth + "'" +  @"
                                                                                    , idcity  = '" + inquiryForIdCity + @"' 
                                                                WHERE idperson = " + person.idperson + @";", connection);
                    adapter.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Logs.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Удаляет существующего пользователя.
        /// </summary>
        /// <param name="id"> Id пользователя. </param>
        /// <returns></returns>
        public void Delete(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                try
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    string qwerty = "DELETE FROM \"Persons\" WHERE idperson = " + id + ";";
                    NpgsqlCommand adapter = new NpgsqlCommand(qwerty, connection);
                    adapter.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Logs.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Получает список пользователей.
        /// </summary>
        /// <returns> Возвращает список пользователей. </returns>
        public List<Person> GetPersonsList()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                try
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    string qwerty = "SELECT ps.idperson, ps.name, ps.dateofbirth, cs.town"
                                     + " FROM \"Persons\" AS ps"
                                     + " INNER JOIN \"Cities\" AS cs ON ps.idcity = cs.idcity;";
                    NpgsqlCommand adapter = new NpgsqlCommand(qwerty, connection);
                    NpgsqlDataReader dr = adapter.ExecuteReader();
                    _PersonsList = new List<Person>();
                    while (dr.Read())
                    {
                        Person person = new Person
                        {
                            idperson = Convert.ToInt32(dr[0]),
                            name = dr[1].ToString(),
                            dateofbirth = Convert.ToDateTime(dr[2]),
                            city = dr[3].ToString()
                        };
                        _PersonsList.Add(person);
                    }

                    return _PersonsList;
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

        /// <summary>
        /// Находит пользователя по номеру идентификатора.
        /// </summary>
        /// <param name="id"> Идентификатор пользователя. </param>
        /// <returns> Возвращает обьект Person. </returns>
        public Person GetPerson(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                try
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    string qwerty = "SELECT * FROM \"Persons\" WHERE idperson = '" + id + "';";
                    NpgsqlCommand adapter = new NpgsqlCommand(qwerty, connection);
                    NpgsqlDataReader dr = adapter.ExecuteReader();
                    Person person = new Person();
                    while (dr.Read())
                    {                               
                        person.idperson = Convert.ToInt32(dr[0]);
                        person.name = dr[1].ToString();
                        person.dateofbirth = Convert.ToDateTime(dr[2]);
                        person.city = dr[3].ToString();
                    }  
                    return person;
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
