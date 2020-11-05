using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections;
using WebApplication_mc_02.Models;
using Nancy;
using Nancy.Json;
using Newtonsoft.Json;
using System.Net.Http;

namespace WebApplication_mc_02.Controllers
{
    /// <summary>
    /// a test endpoint to make sure that the api is working well
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class TestController
    {
        Hashtable table = new Hashtable();
        // GET test
        /// <summary>
        /// returns a list of test data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            string[] responceboi = { "[]" };
            try
            {
                responceboi = System.IO.File.ReadAllLines(@"testData.txt");
            }
            catch { }
            System.IO.File.AppendAllText(@"TestLog.txt", "Test get request");
            return responceboi;
        }

        // GET test/5
        /// <summary>
        /// gets a test object
        /// </summary>
        /// <param name="id">line number to read</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var std = new Chats
            {
                ChatID = 420,
                Data = "yeet",
                SenderID = 69
            };
            return JsonConvert.SerializeObject(System.IO.File.ReadAllLines(@"TestLog.txt")[id]);

        }

        // POST test
        /// <summary>
        /// adds test data to the test data
        /// </summary>
        /// <param name="value">string to add</param>
        /// <returns>'data recieved'</returns>
        [HttpPost]
        public ActionResult<string> Post([Bind] string value)
        {
            List<string> lines = new List<string>
            {
                value
            };
            System.IO.File.AppendAllLines(@"TestLog.txt", lines);
            return "data recieved";
        }

        // PUT test
        /// <summary>
        /// adds test data to the test data
        /// </summary>
        /// <param name="value">string to add</param>
        /// <returns>'data recieved'</returns>
        [HttpPut]
        public ActionResult<string> Put([Bind] string value)
        {

            List<string> lines = new List<string>
            {
                value
            };
            System.IO.File.AppendAllLines(@"TestLog.txt", lines);
            return "data recieved";
        }

        // DELETE test/5
        /// <summary>
        /// deletes all test data
        /// </summary>
        /// <returns>all data in the test</returns>
        [HttpDelete]
        public ActionResult<IEnumerable<string>> Delete()
        {
            string[] data = System.IO.File.ReadAllLines(@"TestLog.txt");
            System.IO.File.WriteAllText(@"TestLog.txt", "");
            return data;
        }
    }
}
