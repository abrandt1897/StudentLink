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

namespace WebApplication_mc_02.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController
    {
        Hashtable table = new Hashtable();
        int rollingID = 0;
        public TestController()
        {
        }
        // GET test
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            string[] responceboi = { "[]" };
            try
            {
                responceboi = System.IO.File.ReadAllLines(@"testData.txt");
            }
            catch { }
            return responceboi;
        }

        // GET test/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var std = new Chats
            {
                ChatID = 420,
                Data = "yeet",
                Sender = 69
            };
            return JsonConvert.SerializeObject(std);

        }

        // POST test
        [HttpPost]
        public ActionResult<string> Post([FromBody] string value)
        {
            List<string> lines = new List<string>();
            lines.Add(value);
            System.IO.File.WriteAllLines(@"testData.txt", lines);
            return "data recieved";
        }

        // PUT test
        [HttpPut]
        public ActionResult<string> Put([FromBody] string value)
        {

            List<string> lines = new List<string>();
            lines.Add(value);
            System.IO.File.WriteAllLines(@"testData.txt", lines);
            return "data recieved";
        }

        // DELETE test/5
        [HttpDelete]
        public ActionResult<string> Delete()
        {
            return "data recieved";
        }
    }
}
