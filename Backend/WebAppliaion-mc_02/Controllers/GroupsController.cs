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
    public class GroupsController : ControllerBase
    {
        private readonly StudentContext DB;
        private readonly IHttpClientFactory _clientFactory;
        private MySqlConnection conn;
        public GroupsController(IHttpClientFactory clientFactory)
        {
            conn = new MySqlConnection("server=coms-309-mc-02.cs.iastate.edu;port=3306;database=StudentLink;user=root;password=46988c18374d9b7d;");
            conn.Open();
            _clientFactory = clientFactory;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Groups>>> GetGroup()
        {
            List<Groups> list = new List<Groups>();
            MySqlCommand cmd = new MySqlCommand("select * from StudentLink.Groups", conn);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Groups()
                    {
                        Students = Convert.ToInt32(reader["Students"]),
                        GroupID = Convert.ToInt32(reader["GroupID"]),
                        TAs = Convert.ToInt32(reader["TAs"]),
                        Mentors = Convert.ToInt32(reader["Mentors"]),
                    });
                }
            }
            return list;
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Groups>> GetGroup(int id)
        {
            Groups group = new Groups();
            MySqlCommand cmd = new MySqlCommand("select * from StudentLink.Groups where GroupID = " + id, conn);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    group = new Groups()
                    {
                        GroupID = Convert.ToInt32(reader["GroupID"]),
                        Students = Convert.ToInt32(reader["Students"]),
                        TAs = Convert.ToInt32(reader["TAs"]),
                        Mentors = Convert.ToInt32(reader["Mentors"]),
                    };
                }
            }
            return group;
        }
        // PUT: api/Students/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public async Task<ActionResult<Groups>> PutGroup(Students[] studs)
        {
            int id = 23423;
            //INSERT INTO Table ((int)key1, key2, key3) VALUES (value1, 'value2', 'value3')
            Groups group = new Groups();
            //the list of students will be a mix of all user types determine where each "student" should go
            MySqlCommand cmd = new MySqlCommand("insert into StudentLink.Groups (GroupID, Students, TAs, Mentors) values (" + id + ", '" + studs[0].Major + "', '" + studs[0].Major + "', '" + studs[0].UserType + "')", conn);
            cmd.ExecuteReader();
            return group;
        }

        // POST: api/Students/
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Groups>> PostGroup(Students student, int groupID)
        {
            //adds a student to the given group id
            MySqlCommand cmd = new MySqlCommand("insert into StudentLink.Groups (GroupID, Students, TAs, Mentors) values (" + groupID + ", '" + student.Major + "', '" + student.Major + "', '" + student.UserType + "')", conn); cmd.ExecuteReader();
            return NoContent();
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Groups>> DeleteGroup(int id)
        {
            Groups group = GetGroup(id).Result.Value;
            MySqlCommand cmd = new MySqlCommand("delete from StudentLink.Groups where GroupID = " + id, conn);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read()) 
                {
                    group = new Groups()
                    {
                        GroupID = Convert.ToInt32(reader["GroupID"]),
                        Students = Convert.ToInt32(reader["Students"]),
                        TAs = Convert.ToInt32(reader["TAs"]),
                        Mentors = Convert.ToInt32(reader["Mentors"])
                    };
                }
            }
            return group;
        }
    }
}
