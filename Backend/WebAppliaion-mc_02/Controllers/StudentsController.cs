using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication_mc_02.Models;
using System.Net.Http;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApplication_mc_02.Models.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System.Net.WebSockets;
using System.Threading;
using Nancy.Json;

namespace WebApplication_mc_02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        public StudentsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Students>>> GetStudents()
        {
            return View("CreateStudent");
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<object>>> GetStudent(int id)
        {
            List<dynamic> student = new List<dynamic>();
            student = SQLConnection.get(typeof(Students), "WHERE StudentID = " + id)[0];
            return student;
        }

        // PUT: api/Students/{canvasOAuthToken}
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public async Task<ActionResult<Students>> PutStudent([Bind("Username,Password,canvasOAuthToken")] CreateAccount myuser )
        {
            List<dynamic> checkUser = SQLConnection.get(typeof(Students), $"WHERE Username='{myuser.Username}'");
            if (checkUser.Count > 0)
                return BadRequest("username already exists");
            Networking network = new Networking(_clientFactory);
            Students myStu = null;
            try
            {
                myStu = network.getStudentProfile(myuser.canvasOAuthToken).Result;
            }catch(Exception e)
            {
                return BadRequest("error getting student");
            }
            myStu.Username = myuser.Username;
            myStu.Password = LoginController.hashPassword(myuser.Password);
            if (SQLConnection.insert(myStu))
                return Ok(myStu);
            return BadRequest("couldnt insert user");
        }

        

        // POST: api/Students
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("{canvasOAuthToken}")]
        public async Task<ActionResult<Students>> PostStudent(string canvasOAuthToken, [Bind("Username,Password")] Login loginForm)
        {
            CreateAccount temp = new CreateAccount();
            temp.Username = loginForm.Username;
            temp.Password = loginForm.Password;
            temp.canvasOAuthToken = canvasOAuthToken;
            return await PutStudent(temp);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Student,Admin,Host")]
        public async Task<ActionResult<object>> DeleteStudent(int id)
        {
            var student = SQLConnection.get(typeof(Students), $"WHERE StudentID = {id}")[0];
            if (SQLConnection.delete(student))
                return Ok(student);
            return BadRequest("sum went wrong, idk");
        }
    }
}
