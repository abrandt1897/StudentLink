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
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        [HttpGet("{user}")]
        public ActionResult<List<dynamic>> Get(int user)
        {
            return SQLConnection.get(typeof(Notifications), $"WHERE StudentID={user}");
        }

        [HttpPut]
        public async Task<ActionResult<Students>> PutNotification([FromBody] Notifications noti)
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
    }
}
