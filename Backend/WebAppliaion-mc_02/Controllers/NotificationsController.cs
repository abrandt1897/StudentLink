using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication_mc_02.Models;
using WebApplication_mc_02.Models.DTO;

namespace WebApplication_mc_02.Controllers
{
    /// <summary>
    /// Interface With the Notification Database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        /// <summary>
        /// Gets all of the notifications associate with a student
        /// </summary>
        /// <param name="user">Student ID</param>
        /// <returns>List of Notifications</returns>
        [HttpGet("{user}")]
        public ActionResult<List<Notifications>> Get(int user)
        {
            return SQLConnection.Get<Notifications>(typeof(Notifications), $"WHERE StudentID={user}").Result;
        }

        /// <summary>
        /// sends a notification
        /// </summary>
        /// <param name="noti">Notification Object</param>
        /// <returns>'Ok' if good or 'client websocket isnt listening and there was an error inserting into Notification Database' if bad</returns>
        [HttpPut]
        public async Task<ActionResult<string>> PutNotification([FromBody] Notifications noti)
        {
            //TODO sanitize data
            if (Global.websockets.ContainsKey(noti.StudentID))
            {
                return Ok(noti);
            }
            if (await SQLConnection.Insert(noti))
                return Ok("Ok");
            return BadRequest("client websocket isnt listening and there was an error inserting into Notification Database");
        }

        /// <summary>
        /// sends a friend request
        /// </summary>
        /// <param name="studentID">student ID of the friend</param>
        /// <param name="noti">notification object</param>
        /// <returns>'Ok' if good 'couldnt delete or insert idk' if bad</returns>
        [HttpPost("{student}")]
        public async Task<ActionResult<Students>> PostFriend(int studentID, [Bind("p[,Data,Type")] Notifications noti)
        {
            if (await SQLConnection.Insert(noti))
                return Ok("Ok");
            return BadRequest("couldnt delete or insert idk");
        }
    }
}
