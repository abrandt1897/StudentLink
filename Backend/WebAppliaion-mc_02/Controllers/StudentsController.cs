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
            return SQLConnection.Get<Students>("WHERE StudentID = " + id).Result[0];
        }

        /// <summary>
        /// gets the student associated with a username
        /// </summary>
        /// <param name="username">username of the student</param>
        /// <returns></returns>
        [HttpGet("Username/{username}")]
        public async Task<ActionResult<Students>> GetStudent(string username)
        {
            return SQLConnection.Get<Students>($"WHERE Username = '{username}'").Result[0];
        }

        /// <summary>
        /// gets the friends of the given
        /// </summary>
        /// <param name="StudentID">StudentID</param>
        /// <returns></returns>
        [HttpGet("Friends/{StudentID}")]
        public async Task<ActionResult<IEnumerable<Student2StudentMap>>> GetFriends(int StudentID)
        {
            return Ok(await SQLConnection.Get<Student2StudentMap>($"WHERE StudentID = {StudentID} and Friends = {StudentID}"));
        }

        /// <summary>
        /// checks if the 2 studentIDs' are friends
        /// </summary>
        /// <param name="StudentID1">Student1</param>
        /// <param name="StudentID2">Student2</param>
        /// <returns>boolean</returns>
        [HttpGet("Friends/{StudentID1}/{StudentID2}")]
        public async Task<ActionResult<bool>> GetFriends(int StudentID1, int StudentID2)
        {
            return Ok(SQLConnection.Get<Student2StudentMap>($"WHERE StudentID = {StudentID1} and FriendID = {StudentID2} or FriendID = {StudentID1} and StudentID = {StudentID2}").Result.Count > 0);
        }

        // PUT: api/Students/{canvasOAuthToken}
        /// <summary>
        /// accepts friend request
        /// </summary>
        /// <param name="StudentID"></param>
        /// <param name="noti"></param>
        [HttpPut("{StudentID}")]
        public async void PutStudent(int StudentID, [Bind("StudentID,Data")] Notifications noti)
        {
            if (bool.Parse(noti.Data))
                await SQLConnection.Insert(new Student2StudentMap() { StudentID = StudentID, FriendID = noti.StudentID });

            SQLConnection.delete(new Notifications() { Record=noti.Record});
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

            List<Students> checkUser = await SQLConnection.Get<Students>($"WHERE Username='{myuser.Username}'");
            if (checkUser.Count > 0)
                return BadRequest("Username already taken");
            Networking network = new Networking(_clientFactory);
            Students myStu = null;
            try
            {
                myStu = network.getStudentProfile(myuser.canvasOAuthToken).Result;
            }
            catch (Exception e)
            {
                return BadRequest("Invalid Token");
            }
            myStu.Username = myuser.Username;
            myStu.Password = LoginController.hashPassword(myuser.Password);
            if (await SQLConnection.Insert(myStu))
                return Ok(myStu);
            return BadRequest("Student already has an account");

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
            var student = SQLConnection.Get<Students>($"WHERE StudentID = {id}").Result[0];
            if (SQLConnection.delete(student))
                return Ok(student);
            return BadRequest("sum went wrong, idk");
        }
    }
}
