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
    /// <summary>
    /// Interface with the Students Database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        public StudentsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        // GET: api/Students/5
        /// <summary>
        /// Gets a Student
        /// </summary>
        /// <param name="id">Student ID</param>
        /// <returns>Student Object</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Students>> GetStudent(int id)
        {
            return SQLConnection.get<Students>(typeof(Students), "WHERE StudentID = " + id)[0];
        }

        [HttpGet("Username/{username}")]
        public async Task<ActionResult<Students>> GetStudent(string username)
        {
            return SQLConnection.get<Students>(typeof(Students), $"WHERE Username = '{username}'")[0];
        }

        // PUT: api/Students/{canvasOAuthToken}
        /// <summary>
        /// accept friend request
        /// </summary>
        /// <param name="StudentID">student ID</param>
        /// <returns></returns>
        [HttpPut("{StudentID}")]
        public async void PutStudent(int StudentID, [Bind("StudentID,Data")] Notifications noti)
        {
            if (bool.Parse(noti.Data))
                SQLConnection.insert(new Student2StudentMap() { StudentID = noti.StudentID, FriendID = StudentID});
        }



        // POST: api/Students
        /// /// <summary>
        /// Creates a user/Student
        /// </summary>
        /// <param name="canvasOAuthToken">canvas token</param>
        /// <param name="loginForm">creates username and password</param>
        /// <returns>Student Object associated with the canvas token</returns>
        [HttpPost("{canvasOAuthToken}")]
        public async Task<ActionResult<Students>> PostStudent(string canvasOAuthToken, [Bind("Username,Password")] Login loginForm)
        {
            CreateAccount myuser = new CreateAccount();
            myuser.Username = loginForm.Username;
            myuser.Password = loginForm.Password;
            myuser.canvasOAuthToken = canvasOAuthToken;

            List<Students> checkUser = SQLConnection.get<Students>(typeof(Students), $"WHERE Username='{myuser.Username}'");
            if (checkUser.Count > 0)
                return BadRequest("username already exists");
            Networking network = new Networking(_clientFactory);
            Students myStu = null;
            try
            {
                myStu = network.getStudentProfile(myuser.canvasOAuthToken).Result;
            }
            catch (Exception e)
            {
                return BadRequest("error getting student");
            }
            myStu.Username = myuser.Username;
            myStu.Password = LoginController.hashPassword(myuser.Password);
            if (SQLConnection.insert(myStu))
                return Ok(myStu);
            return BadRequest("couldnt insert user");

            return Ok(myuser);
        }

        // DELETE: api/Students/5
        /// <summary>
        /// removes student from database
        /// </summary>
        /// <param name="id">student ID</param>
        /// <returns>Student object that was removed</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Student,Admin,Host")]
        public async Task<ActionResult<Students>> DeleteStudent(int id)
        {
            var student = SQLConnection.get<Students>(typeof(Students), $"WHERE StudentID = {id}")[0];
            if (SQLConnection.delete(student))
                return Ok(student);
            return BadRequest("sum went wrong, idk");
        }
    }
}
