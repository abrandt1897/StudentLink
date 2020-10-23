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
        public async Task<ActionResult<Students>> PutStudent(CreateAccount myuser)
        {
            Networking network = new Networking(_clientFactory);
            Students myStu = network.getStudentProfile(myuser.canvasOAuthToken).Result;
            myStu.Username = myuser.Username;
            myStu.Password = LoginController.hashPassword(myuser.Password);
            if (SQLConnection.insert(myStu))
                return Ok(myStu);
            return View("/Views/Login/Login.cshtml");
        }

        [HttpPut]
        public async Task<ActionResult<Students>> PutFriendNotification([FromBody] Notifications noti)
        {
            //TODO sanitize data
            if (Global.websockets.ContainsKey(noti.StudentID) && WebSocketHandler.sendDataAsync(Global.websockets[noti.StudentID], noti).Result)
            {
                return Ok(noti);
            }
            if (SQLConnection.insert(noti))
                return Ok(noti);
            return BadRequest("client websocket isnt listening and there was an error inserting into Notification Database");
        }

        // POST: api/Students
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Students>> PostStudent([FromForm] CreateAccount myStu)
        {
            return await PutStudent(myStu);
        }
        //api/Students/{StudentID}
        [HttpPost("{student}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Student,Admin,Host")]
        public async Task<ActionResult<Students>> PostFriend(int studentID, [FromBody] Notifications noti)
        {
            Student2StudentMap friends = new Student2StudentMap();
            friends.StudentID = studentID;
            friends.FriendID = noti.StudentID;
            if (SQLConnection.insert(friends) && SQLConnection.delete(noti))
                return Ok(noti);
            return BadRequest("couldnt delete or insert idk");
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
