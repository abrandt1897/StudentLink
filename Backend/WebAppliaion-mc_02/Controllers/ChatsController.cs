using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication_mc_02.Models;
using System.Net.Http;
using MySql.Data.MySqlClient;

namespace WebApplication_mc_02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly StudentContext DB;
        private readonly IHttpClientFactory _clientFactory;
        private MySqlConnection conn;
        public ChatsController(IHttpClientFactory clientFactory)
        {
            conn = new MySqlConnection("server=coms-309-mc-02.cs.iastate.edu;port=3306;database=StudentLink;user=root;password=46988c18374d9b7d;");
            conn.Open();
            _clientFactory = clientFactory;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chats>>> GetChat()
        {
            List<Chats> list = new List<Chats>();
            MySqlCommand cmd = new MySqlCommand("select * from StudentLink.Chats", conn);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Chats()
                    {
                        ChatID = Convert.ToInt32(reader["ChatID"]),
                        Sender = Convert.ToInt32(reader["Sender"]),
                        Data = reader["Data"].ToString(),
                    });
                }
            }
            return list;
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chats>> GetChat(int id)
        {
            Chats chat = new Chats();
            MySqlCommand cmd = new MySqlCommand("select * from StudentLink.Chats where ChatID = " + id, conn);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    chat = new Chats()
                    {
                        ChatID = Convert.ToInt32(reader["ChatID"]),
                        Sender = Convert.ToInt32(reader["Sender"]),
                        Data = reader["Data"].ToString(),
                    };
                }
            }
            return chat;
        }
        // PUT: api/Students/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public async Task<ActionResult<Chats>> PutChat(Students[] studs)
        {
            int id = 23423;
            //INSERT INTO Table ((int)key1, key2, key3) VALUES (value1, 'value2', 'value3')
            Chats chat = new Chats();
            //the list of students will be a mix of all user types determine where each "student" should go
            MySqlCommand cmd = new MySqlCommand("insert into StudentLink.Chats (ChatID, Sender, Data) values (" + id + ", '" + studs[0].Major + "', '" + studs[0].Major + "')", conn);
            //cmd.ExecuteReader();
            return chat;
        }

        // POST: api/Students/
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Chats>> PostChat(Students student, int groupID)
        {
            //adds a student to the given group id
            MySqlCommand cmd = new MySqlCommand("insert into StudentLink.Chats (ChatID, Sender, Data) values (" + groupID + ", '" + student.Major + "', '" + student.Major + "')", conn); cmd.ExecuteReader();
            return NoContent();
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Chats>> DeleteChat(int id)
        {
            Chats chat = GetChat(id).Result.Value;
            MySqlCommand cmd = new MySqlCommand("delete from StudentLink.Chats where ChatID = " + id, conn);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    chat = new Chats()
                    {
                        ChatID = Convert.ToInt32(reader["ChatID"]),
                        Sender = Convert.ToInt32(reader["Sender"]),
                        Data = reader["Data"].ToString(),
                    };
                }
            }
            return chat;
        }
    }
}
