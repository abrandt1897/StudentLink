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
        public static void update(dynamic data, Hashtable values, string condition="")
        {
            MySqlConnection conn = new MySqlConnection("server=coms-309-mc-02.cs.iastate.edu;port=3306;database=StudentLink;user=root;password=46988c18374d9b7d;");
            conn.Open();

            string query = "update StudentLink."+data.GetType().Name+" set ";
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
            MySqlConnection conn = new MySqlConnection("server=coms-309-mc-02.cs.iastate.edu;port=3306;database=StudentLink;user=root;password=46988c18374d9b7d;");
            conn.Open();
            MySqlCommand cmd = null;
            switch (data)
            {
                case Students s:
                    cmd = new MySqlCommand("insert into StudentLink.Students (StudentID, FullName, CourseIDs, Attributes, Classification, Major, UserType, Friends) values (" + s.StudentID + ", '" + s.FullName + "', '" + s.CourseIDs + "', '" + s.Attributes + "', '" + s.Classification + "', '" + s.Major + "', '" + s.UserType + "', '"+ s.Friends +"')", conn);
                    break;
                case Courses c:
                    cmd = new MySqlCommand("insert into StudentLink.Courses (CourseID, Name, Mentors, TAs, Section, Term, Students, TermID, SectionID) values (" + c.CourseID + ", '" + c.Name + "', " + c.Mentors + ", " + c.TAs + ", '" + c.Section + "', '" + c.Term + "', '" + c.Students + "', " + c.TermID + ", " + c.SectionID + ")", conn);
                    break;
                case Groups g:
                    cmd = new MySqlCommand("insert into StudentLink.Groups (GroupID, Students, TAs, Mentors) values (" + g.GroupID + ", '" + g.Students + "', " + g.TAs + ", " + g.Mentors + ")", conn);
                    break;
                case Chats ch:
                    cmd = new MySqlCommand("insert into StudentLink.Chats (ChatID, Sender, Data) values (" + ch.ChatID + ", '" + ch.Sender + "', '" + ch + "')", conn);
                    break;
                case Login l:
                    cmd = new MySqlCommand("insert into StudentLink.Login (Username, Email, Password) values ('" + l.Username + "', '" + l.Email + "', " + l.Password + "')", conn);
                    break;
                default:
                    return;
            }
            try
            {
                cmd.ExecuteReader();
            }
            catch (Exception e){ Debug.WriteLine(e.Message.ToString()); }
            conn.Close();
        }
        public static dynamic get(dynamic type, string column="*", string filter = "")
        {
            List<Students> list = new List<Students>();

            string query = "SELECT " + ( column == null ? "*" : column ) + " FROM StudentLink."+ type.Name + " "+ filter;

            MySqlConnection conn = new MySqlConnection("server=coms-309-mc-02.cs.iastate.edu;port=3306;database=StudentLink;user=root;password=46988c18374d9b7d;");
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Students()
                    {
                        StudentID = Convert.ToInt32(reader["StudentID"]),
                        FullName = reader["FullName"].ToString(),
                        CourseIDs = reader["CourseIDs"].ToString(),
                        Attributes = reader["Attributes"].ToString(),
                        Classification = reader["Classification"].ToString(),
                        Major = reader["Major"].ToString(),
                        UserType = reader["UserType"].ToString()
                    });
                }
            }
            conn.Close();
            if (list.Count > 1)
                return new Students();
            else return list;
        }
    }
}
