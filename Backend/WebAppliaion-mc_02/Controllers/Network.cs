﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Nancy.Json;
using System.Collections;
using WebApplication_mc_02.Models;

namespace WebApplication_mc_02.Controllers
{
    public class Networking
    {
        IHttpClientFactory _clientFactory;
        public Networking(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<Students> getStudentProfile(string token)
        {
            //get student string data
            var courseResponse = await makeRequest("https://canvas.instructure.com/api/v1/courses?enrollment_state=active&include[]=sections&include[]=term&include[]=concluded", token);
            var profileResponse = await makeRequest("https://canvas.iastate.edu/api/v1/users/self/", token);
            //parse student data to json
            Nancy.Json.Simple.JsonArray courseJsonResponse = new JavaScriptSerializer().Deserialize<Nancy.Json.Simple.JsonArray>(courseResponse);
            Nancy.Json.Simple.JsonObject profileJsonResponse = new JavaScriptSerializer().Deserialize<Nancy.Json.Simple.JsonObject>(profileResponse);
            //initilize student object
            Students myStu = new Students();
            //retreive course data from json object
            string CourseIDs = "";
            foreach (Nancy.Json.Simple.JsonObject jsonObject in courseJsonResponse)
                CourseIDs += getJsonValue(jsonObject, "id") + " ";
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
            myStu.UserType = "Student";
            myStu.CourseIDs = CourseIDs;
            //return
            return myStu;
        }
        public String getJsonValue(Nancy.Json.Simple.JsonObject obj, string key)
        {
            int idx1 = 0;
            foreach (string s in obj.Keys)
            {
                if (s.Equals(key))
                    break;
                idx1++;
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