using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Nancy.Json;
using System.Collections;
using WebApplication_mc_02.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;
//Future Greyson
//allow students to be added to a course
//to do that you need to check if the course already exists
//and then if it does add the student to the course otherwise
//add the course with the current student in it
namespace WebApplication_mc_02.Controllers
{
    public class Networking
    {
        private IHttpClientFactory _clientFactory;
        //private MySqlConnection conn;
        public Networking(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            //conn = new MySqlConnection("server=coms-309-mc-02.cs.iastate.edu;port=3306;database=StudentLink;user=root;password=46988c18374d9b7d;");
            //conn.Open();
        }
        public async Task<Students> getStudentProfile(string token)
        {
            //get student string data
            
            var courseResponse = await makeRequest("https://canvas.instructure.com/api/v1/courses?include[]=sections&include[]=term&per_page=100", token);
            var profileResponse = await makeRequest("https://canvas.iastate.edu/api/v1/users/self/", token);
            //parse student data to json
            Nancy.Json.Simple.JsonArray courseJsonResponse = new JavaScriptSerializer().Deserialize<Nancy.Json.Simple.JsonArray>(courseResponse);
            Nancy.Json.Simple.JsonObject profileJsonResponse = new JavaScriptSerializer().Deserialize<Nancy.Json.Simple.JsonObject>(profileResponse);
            //initilize student object
            Students myStu = new Students();
            //retreive course data from json object
            string CourseIDs = "";
            CourseIDs = CourseIDs.Trim();
            //get name data from json object
            string studentName = getJsonValue(profileJsonResponse, "name");
            //get canvas Id data from json obj which will replace student ID
            string studentID = getJsonValue(profileJsonResponse, "id");
            var request = new HttpRequestMessage(HttpMethod.Get, "https://www.info.iastate.edu/individuals/search/" + studentName.Replace(' ', '+'));
            var client = _clientFactory.CreateClient();
            var iastateResponse = client.SendAsync(request).Result;
            string data = iastateResponse.Content.ReadAsStringAsync().Result;
            //parse major
            int majorStartIndex = data.IndexOf("Major:</span> ")+ "Major:</span> ".Length;
            int majorEndIndex = data.Substring(majorStartIndex).IndexOf("</div>");
            string major = data.Substring(majorStartIndex, majorEndIndex);
            //parse classification
            int classificationStartIndex = data.IndexOf("Classification:</span> ") + "Classification:</span> ".Length;
            int classificationEndIndex = data.Substring(classificationStartIndex).IndexOf("</div>");
            string classification = data.Substring(classificationStartIndex, classificationEndIndex);
            //build student object
            myStu.FullName = studentName;
            myStu.Classification = classification;
            myStu.Major = major;
            myStu.StudentID = Int32.Parse(studentID);
            
            myStu.Friends = " ";

            List<Courses> courses = new List<Courses>();
            foreach (Nancy.Json.Simple.JsonObject jsonObject in courseJsonResponse)
            {
                CourseIDs += getJsonValue(jsonObject, "id") + " ";
                //Build course object
                Courses c = new Courses();
                if (getJsonValue(jsonObject, "name") == "Error")
                {
                    continue;
                }
                c.Name = getJsonValue(jsonObject, "name");
                c.CourseID = Int64.Parse(getJsonValue(jsonObject, "id"));
                c.TermID = Int64.Parse(getJsonValue(new JavaScriptSerializer().Deserialize<Nancy.Json.Simple.JsonObject>(getJsonValue(jsonObject, "term")), "id"));
                c.Term = getJsonValue(new JavaScriptSerializer().Deserialize<Nancy.Json.Simple.JsonObject>(getJsonValue(jsonObject, "term")), "name");
                c.Students = studentID;
                string sectionsData = getJsonValue(jsonObject, "sections");
                Nancy.Json.Simple.JsonArray asdf = new JavaScriptSerializer().Deserialize<Nancy.Json.Simple.JsonArray>(sectionsData);
                IEnumerator e = asdf.GetEnumerator();
                e.MoveNext();
                Nancy.Json.Simple.JsonObject data69 = (Nancy.Json.Simple.JsonObject)e.Current;
                string IdData = getJsonValue(data69, "id");
                c.SectionID = Int64.Parse(IdData);
                c.Section = getJsonValue(data69, "name");
                courses.Add(c);
            }
            //Insert into table
            for (int i = 0; i < courses.Count; i++)
            {
                SQLConnection.insert(courses[i]);
                //MySqlCommand cmd2 = new MySqlCommand("insert into StudentLink.Courses (CourseID, Name, Mentors, TAs, Section, Term, Students, TermID, sectionID) values (" + courses[i].CourseID + ", '" + courses[i].Name + "', " +  courses[i].Mentors + ", " + courses[i].TAs + ", '" + courses[i].Section + "', '" + courses[i].Term + "', " + myStu.StudentID + ", " + courses[i].TermID + ", " + courses[i].SectionID + ")", conn);
                //cmd2.ExecuteReader();
            }
            //conn.Close();
            //return
            return myStu;
        }
        public String getJsonValue(Nancy.Json.Simple.JsonObject obj, string key)
        {
            int idx1 = 0;
            bool temp = false;
            foreach (string s in obj.Keys)
            {
                if (s.Equals(key))
                {
                    temp = true;
                    break;
                }
                idx1++;
            }
            if(!temp)
            {
                return "Error";
            }
            IEnumerator list1 = obj.Values.GetEnumerator();
            for (int i = 0; i < idx1 + 1; i++)
                list1.MoveNext();
            object ID1 = list1.Current;

            return ID1.ToString();
        }

        public async Task<String> logUserIn(string userID, string password)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://iastate.okta.com/api/v1/authn");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");
            request.Headers.Add("Authorization", "SSWS 00Dn1-IfOQVpsWQ-ziuTWpBtnHu__8BvWgyyT99d4s");
            request.Headers.Add("Cache-Control", "no-cache");
            request.Headers.Add("X-Fowarded-For", "23.235.46.133");
            request.Headers.Add("X-Device-Fingerprint", "application/json");
            request.Content = new StringContent("{\"password\":\""+ password + "\",\"userID\":\""+ userID, System.Text.Encoding.UTF8, "application/json");
            var client = _clientFactory.CreateClient();

            var courseResponse = await client.SendAsync(request);
            if (courseResponse.IsSuccessStatusCode)
            {
                string responce = await courseResponse.Content.ReadAsStringAsync();
                return responce;
            }
            else
            {
                return "";
            }

            return "";
        }
        public async Task<String> makeRequest(string URL, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, URL);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");
            request.Headers.Add("Authorization", "Bearer " + token);

            var client = _clientFactory.CreateClient();

            var courseResponse = await client.SendAsync(request);
            if (courseResponse.IsSuccessStatusCode)
            {
                string responce = await courseResponse.Content.ReadAsStringAsync();
                return responce;
            }
            else
            {
                return "";
            }
        }
    }
}
