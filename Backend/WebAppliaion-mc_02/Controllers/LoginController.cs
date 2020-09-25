using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication_mc_02.Models;
using System.Net.Http;
using MySql.Data.MySqlClient;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication_mc_02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly StudentContext DB;
        private readonly IHttpClientFactory _clientFactory;
        private MySqlConnection conn;
        public LoginController(IHttpClientFactory clientFactory)
        {
            //conn = new MySqlConnection("server=coms-309-mc-02.cs.iastate.edu;port=3306;database=StudentLink;user=root;password=46988c18374d9b7d;");
            //conn.Open();
            _clientFactory = clientFactory;
        }

        // GET: api/<LoginController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LoginController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LoginController>
        [HttpPost("{myLogin}")]
        public async Task<ActionResult<Login>> PostLogin(Login myLogin)
        {
            MySqlCommand cmd = new MySqlCommand("insert into StudentLink.Login (UserID, Password) values (" + myLogin.UserID + ", " + myLogin.Password + ")", conn);
            cmd.ExecuteReader();
            return CreatedAtAction("GetLogin", new { id = myLogin.UserID }, myLogin);
        }

        // PUT api/<LoginController>/5
        [HttpPut("{UserID, Password}")]
        public void PutLogin(string UserID, string Password)
        {
            Networking network = new Networking(_clientFactory);
            Login myLogin = network.getLogin(UserID, Password).Result;
            SQLConnection.insert(myLogin);
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
