﻿using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_mc_02.Models;

namespace WebApplication_mc_02.Controllers
{
    public class SQLConnection
    {
        public static void insert(dynamic data)
        {
            MySqlConnection conn = new MySqlConnection("server=coms-309-mc-02.cs.iastate.edu;port=3306;database=StudentLink;user=root;password=46988c18374d9b7d;");
            conn.Open();
            MySqlCommand cmd = null;
            switch (data.GetType().Name)
            {
                case "Students":
                    cmd = new MySqlCommand("insert into StudentLink.Students (StudentID, FullName, CourseIDs, Attributes, Classification, Major, UserType, Friends) values (" + data.StudentID + ", '" + data.FullName + "', '" + data.CourseIDs + "', '" + data.Attributes + "', '" + data.Classification + "', '" + data.Major + "', '" + data.UserType + "' ')", conn);
                    break;
                case "Courses":
                    cmd = new MySqlCommand("insert into StudentLink.Courses (CourseID, Name, Mentors, TAs, Section, Term, Students, TermID, SectionID) values (" + data.CourseID + ", '" + data.Name + "', " + data.Mentors + ", " + data.TAs + ", '" + data.Section + "', '" + data.Term + "', " + "' '" + ", " + data.TermID + ", " + data.SectionID + ")", conn);
                    break;
                case "Groups":
                    cmd = new MySqlCommand("insert into StudentLink.Groups (GroupID, Students, TAs, Mentors) values (" + data.GroupID + ", '" + data.Students + "', " + data.TAs + ", " + data.Mentors + ")", conn);
                    break;
                case "Chats":
                    cmd = new MySqlCommand("insert into StudentLink.Chats (ChatID, Sender, Data) values (" + data.ChatID + ", '" + data.Sender + "', " + data.Data + ")", conn);
                    break;
                default:
                    return;
            }
            cmd.ExecuteReader();
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
            if (list.Count == 1)
                return list[0];
            else if (list.Count > 1)
                return new Students();
            else return list;
        }
    }
}