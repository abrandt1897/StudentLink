using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_mc_02.Models;

namespace WebApplication_mc_02.Controllers
{
    public class SQLConnection
    {
        //private static MySqlConnection conn = new MySqlConnection("server=coms-309-mc-02.cs.iastate.edu;port=3306;database=StudentLink;user=root;password=46988c18374d9b7d;");
        /// <summary>
        /// updates SQL database of the given object's key ID with the new data in the object
        /// </summary>
        /// <param name="data">the object to be updated in the database</param>
        public static bool update(dynamic data)
        {
            using (MySqlConnection conn = new MySqlConnection("server=coms-309-mc-02.cs.iastate.edu;port=3306;database=StudentLink;user=root;password=46988c18374d9b7d;"))
            {
                conn.Open();

                string query = $"update StudentLink.{data.GetType().Name} set ";

                var properties = data.GetType().GetProperties();
                foreach (var property in properties)
                {
                    //query += property.GetValue(data);
                    var @switch = new Dictionary<Type, Action> {
                    { typeof(String), () => query += $"{property.Name} = '{property.GetValue(data)}'" },
                    { typeof(Int32), () => query += $"{property.Name} = {property.GetValue(data)}" },
                    { typeof(Int64), () => query += $"{property.Name} = {property.GetValue(data)}" },
                    { typeof(bool), () => query += $"{property.Name} = {Convert.ToBoolean(property.GetValue(data))}" },
                };
                    @switch[property.PropertyType]();
                    query += ", ";
                }
                query = query.Trim(' ');
                query = query.Trim(',');
                query += $" where {properties[0].Name} = {properties[0].GetValue(data)}";
                query += ";";

                MySqlCommand cmd = new MySqlCommand(query);

                try
                {
                    cmd.ExecuteReader();
                }
                catch (Exception e) { Debug.WriteLine(e.Message.ToString()); }
                //conn.Close();
                return true;
            }
        }
        /// <summary>
        /// inserts data into the data type's database
        /// </summary>
        /// <param name="data">the object that is to be inserted into the database</param>
        public static bool insert(dynamic data)
        {
            using (MySqlConnection conn = new MySqlConnection("server=coms-309-mc-02.cs.iastate.edu;port=3306;database=StudentLink;user=root;password=46988c18374d9b7d;"))
            {
                conn.Open();
                var properties = data.GetType().GetProperties();
                string query = $"insert into StudentLink.{data.GetType().Name} values (";

                foreach (var property in properties)
                {
                    //query += property.GetValue(data);
                    var @typeSwitch = new Dictionary<Type, Action> {
                    { typeof(String), () => query += "'"+ property.GetValue(data) + "'" },
                    { typeof(Int32), () => query += property.GetValue(data) },
                    { typeof(Int64), () => query += property.GetValue(data) },
                    { typeof(bool), () => query += property.GetValue(data) },
                };
                    @typeSwitch[property.PropertyType]();
                    query += ", ";
                }
                query = query.Trim(' ');
                query = query.Trim(',');
                query += ");";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                try
                {
                    cmd.ExecuteReader();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message.ToString());
                    return false;
                }
                //conn.Close();
                return true;
            }
        }
        /// <summary>
        /// gets the data of the given type from the database and returns a list of them
        /// </summary>
        /// <param name="type">the data type denoting the database to read from</param>
        /// <param name="column">a string that represents the property of the given type and returns the column of that database. If null it assumes all columns</param>
        /// <param name="filter">a where clause that filters the values to be returned from the database</param>
        /// <returns>a generic list of the given type</returns>
        public static List<dynamic> get(Type type, string filter = "", string column = "*")//TODO change the filter and the column to take in a variable of the type.
        {
            using (MySqlConnection conn = new MySqlConnection("server=coms-309-mc-02.cs.iastate.edu;port=3306;database=StudentLink;user=root;password=46988c18374d9b7d;"))
            {
                conn.Open();
                List<dynamic> list = new List<dynamic>();

                string query = "SELECT " + (column == null ? "*" : column) + " FROM StudentLink." + type.Name + " " + filter;

                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var data = Activator.CreateInstance(type);
                        if (column == "*")
                        {
                            foreach (var property in type.GetProperties())
                            {
                                var @switch = new Dictionary<Type, Action> {
                                    { typeof(String), () => property.SetValue(data, reader[property.Name].ToString()) },
                                    { typeof(Int32), () => property.SetValue(data, Convert.ToInt32(reader[property.Name])) },
                                    { typeof(Int64), () => property.SetValue(data, Convert.ToInt64(reader[property.Name])) },
                                    { typeof(bool), () => property.SetValue(data, Convert.ToBoolean(reader[property.Name])) },
                                };
                                @switch[property.PropertyType]();
                            }
                        }
                        else
                        {
                            foreach (var property in type.GetProperties())
                            {
                                if (column.Contains(property.Name))
                                {
                                    var @switch = new Dictionary<Type, Action> {
                                        { typeof(String), () => property.SetValue(data, reader[property.Name].ToString()) },
                                        { typeof(Int32), () => property.SetValue(data, Convert.ToInt32(reader[property.Name])) },
                                        { typeof(Int64), () => property.SetValue(data, Convert.ToInt64(reader[property.Name])) },
                                        { typeof(bool), () => property.SetValue(data, Convert.ToBoolean(reader[property.Name])) },
                                    };
                                    @switch[property.PropertyType]();
                                }
                            }
                        }
                        list.Add(data);
                    }
                }
                //conn.Close();
                return list;
            }
        }

        public static bool delete(dynamic data)
        {
            using (MySqlConnection conn = new MySqlConnection("server=coms-309-mc-02.cs.iastate.edu;port=3306;database=StudentLink;user=root;password=46988c18374d9b7d;"))
            {
                conn.Open();

                string query = $"delete from StudentLink.{data.GetType().Name}";

                var property = data.GetType().GetProperties()[0];
                query += $" where {property.Name}={property.GetValue(data)};";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                try
                {
                    cmd.ExecuteReader();
                }
                catch (Exception e) { 
                    Debug.WriteLine(e.Message.ToString());
                    return false;
                }
                return true;
            }
        }

        public static dynamic get(string query)
        {
            using (MySqlConnection conn = new MySqlConnection("server=coms-309-mc-02.cs.iastate.edu;port=3306;database=StudentLink;user=root;password=46988c18374d9b7d;"))
            {
                conn.Open();
                string obj = "";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return reader.GetValue(0);
                    }
                }
                return obj;
            }
        }

        public static dynamic insert(string query)
        {
            using (MySqlConnection conn = new MySqlConnection("server=coms-309-mc-02.cs.iastate.edu;port=3306;database=StudentLink;user=root;password=46988c18374d9b7d;"))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                try
                {
                    cmd.ExecuteReader();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message.ToString());
                    return false;
                }
                return true;
            }
        }
    }
}
