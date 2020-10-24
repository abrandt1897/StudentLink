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
using Nancy.Json.Simple;
using System.Net;
using System.IO;
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

            var courseResponse = await makeRequest("https://canvas.instructure.com/api/v1/courses?state[]=available&state[]=deleted&state[]=completed&include[]=sections&include[]=term&per_page=150", token);
            var profileResponse = await makeRequest("https://canvas.iastate.edu/api/v1/users/self/", token);
            //parse student data to json
            JsonArray courseJsonResponse = new JavaScriptSerializer().Deserialize<JsonArray>(courseResponse);
            JsonObject profileJsonResponse = new JavaScriptSerializer().Deserialize<JsonObject>(profileResponse);
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
            int majorStartIndex = data.IndexOf("Major:</span> ") + "Major:</span> ".Length;
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
            myStu.Role = "Student";

            List<Courses> courses = new List<Courses>();
            foreach (JsonObject jsonObject in courseJsonResponse)
            {
                //Build course object
                Courses c = new Courses();
                if (getJsonValue(jsonObject, "name") == "Error")
                {
                    continue;
                }
                c.Name = getJsonValue(jsonObject, "name");
                c.CourseID = Int64.Parse(getJsonValue(jsonObject, "id"));
                c.TermID = Int64.Parse(getJsonValue(new JavaScriptSerializer().Deserialize<JsonObject>(getJsonValue(jsonObject, "term")), "id"));
                c.Term = getJsonValue(new JavaScriptSerializer().Deserialize<JsonObject>(getJsonValue(jsonObject, "term")), "name");
                c.Students = studentID;
                string sectionsData = getJsonValue(jsonObject, "sections");
                JsonArray asdf = new JavaScriptSerializer().Deserialize<JsonArray>(sectionsData);
                IEnumerator e = asdf.GetEnumerator();
                e.MoveNext();
                JsonObject data69 = (JsonObject)e.Current;
                string IdData = getJsonValue(data69, "id");
                c.SectionID = Int64.Parse(IdData);
                c.Section = getJsonValue(data69, "name");
                courses.Add(c);
                //map student to course
                Student2CourseMap s2cm = new Student2CourseMap();
                s2cm.CourseID = c.CourseID;
                s2cm.StudentID = myStu.StudentID;
                s2cm.UserType = "Student";
                s2cm.CurrentlyEnrolled = getJsonValue((JsonObject)new JavaScriptSerializer().Deserialize<JsonArray>(getJsonValue(jsonObject, "enrollments"))[0], "enrollment_state") == "active" && getJsonValue(jsonObject, "workflow_state") == "active";
                SQLConnection.insert(c);
                SQLConnection.insert(s2cm);
            }
            return myStu;
        }
        public String getJsonValue(JsonObject obj, string key)
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
            if (!temp)
            {
                return "Error";
            }
            IEnumerator list1 = obj.Values.GetEnumerator();
            for (int i = 0; i < idx1 + 1; i++)
                list1.MoveNext();
            object ID1 = list1.Current;

            return ID1.ToString();
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
