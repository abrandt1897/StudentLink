using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication_mc_02.Models;
using System.Net.Http;
using MySql.Data.MySqlClient;
using WebApplication_mc_02.Models.DTO;

namespace WebApplication_mc_02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chats>>> GetChat()
        {
            List<dynamic> list = new List<dynamic>();
            list = SQLConnection.get(typeof(Chats));
            var ret = list.Cast<Chats>();
            return Ok(ret);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chats>> GetChat(int id)
        {
            List<dynamic> chat = new List<dynamic>();
            chat = SQLConnection.get(typeof(Chats), "WHERE ChatID = " + id);
            if (chat.Count == 0)
                return BadRequest("theres no chat under that id");
            return Ok(chat);
        }
        // PUT: api/Students/5
        [HttpPut]
        public async Task<ActionResult<Chats>> PutChat([FromBody] int[] studIDs)
        {
            string query = "SELECT MAX(ChatID) FROM StudentLink.Student2ChatMap";
            object result = SQLConnection.get(query);
            if (result.ToString() == "")
                result = "0";
            foreach (int stuID in studIDs)
            {
                Student2ChatMap s2cm = new Student2ChatMap();
                s2cm.ChatID = Convert.ToInt32(result)+1;
                s2cm.StudentID = stuID;
                SQLConnection.insert(s2cm);
            }
            return Ok(Convert.ToInt32(result) + 1);
        }

        // POST: api/Students/
        [HttpPost]
        public async Task<ActionResult<Chats>> PostChat([FromBody] Chats chat)//TODO this method will allow users to add a message to the database
        {
            if (SQLConnection.get(typeof(Student2ChatMap), $"WHERE ChatID={chat.ChatID} and StudentID={chat.SenderID}").Count < 1)
                return BadRequest("your not allowed to send data in this chat");
            SQLConnection.insert(chat); //TODO CHECK IF THE DATA IS CLEAN AND SANITIZED
            return Ok(chat);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Chats>> DeleteChat(int id)
        {
            var chat = SQLConnection.get(typeof(Chats), $"WHERE ChatID = {id}")[0];
            if (SQLConnection.delete(chat))
                return Ok(chat);
            return BadRequest("sum went wrong, idk");
        }
    }
}
