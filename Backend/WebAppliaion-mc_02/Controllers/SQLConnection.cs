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
        private static MySqlConnection conn = new MySqlConnection("server=coms-309-mc-02.cs.iastate.edu;port=3306;database=StudentLink;user=root;password=46988c18374d9b7d;");
        public static void update(dynamic data, Hashtable values, string condition="")
        {
            conn.Open();

            string query = $"update StudentLink. {data.GetType().Name} set ";
            foreach(DictionaryEntry d in values)
            {
                query += d.Key + " = '" + d.Value + "', ";
            }
            query = query.Substring(0, query.Length - 3);
            query += condition;

            MySqlCommand cmd = new MySqlCommand(query);
            
            try
            {
                cmd.ExecuteReader();
            }
            catch (Exception e) { Debug.WriteLine(e.Message.ToString()); }
            conn.Close();
        }
        public static void insert(object data)
        {
            conn.Open();
            var properties = data.GetType().GetProperties(System.Reflection.BindingFlags.Default);
            string query = $"insert into StudentLink.{data.GetType().Name} values (";

            foreach (var property in properties)
            {
                query += property.GetValue(data);
                var @switch = new Dictionary<Type, Action> {
                    { typeof(String), () => query += "'"+ property.GetValue(data) + "'" },
                    { typeof(Int32), () => query += property.GetValue(data) },
                };
                @switch[property.PropertyType]();
                query += ", ";
            }
            query += ");";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            try {
                cmd.ExecuteReader();
            } catch (Exception e){ Debug.WriteLine(e.Message.ToString()); }
            conn.Close();
        }
        public static List<dynamic> get(Type type, string column="*", string filter = "")
        {
            List<dynamic> list = new List<dynamic>();

            string query = "SELECT " + ( column == null ? "*" : column ) + " FROM StudentLink."+ type.Name + " "+ filter;

            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var data = Activator.CreateInstance(type);
                    foreach (var property in type.GetProperties())
                    {
                        var @switch = new Dictionary<Type, Action> {
                            { typeof(String), () => property.SetValue(data, reader[property.Name].ToString()) },
                            { typeof(Int32), () => property.SetValue(data, Convert.ToInt32(reader[property.Name])) },
                            { typeof(bool), () => property.SetValue(data, Convert.ToBoolean(reader[property.Name])) },
                        };
                        @switch[property.PropertyType]();
                    }
                    list.Add(data);
                }
            }
            conn.Close();
            return list;
        }
    }
}