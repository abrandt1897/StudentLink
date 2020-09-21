using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections;
using WebApplication_mc_02.Models;

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
            String[] responceboi = { "{value:1}"};
            return responceboi;
        }

        // GET test/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            foreach(string value in table.Values)
                return table[id].ToString();
            return "Null: id doesnt exist";
        }

        // POST test
        [HttpPost]
        public void Post([FromBody] Students value)
        {
            table.Add(rollingID, value);
        }

        // PUT test
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Students value)
        {
            table.Add(id, value);
        }

        // DELETE test/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            table.Remove(id);
        }
    }
}
