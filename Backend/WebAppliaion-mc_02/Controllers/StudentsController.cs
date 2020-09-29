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
    public class StudentsController : ControllerBase
    {
        private readonly StudentContext DB;
        private readonly IHttpClientFactory _clientFactory;
        private MySqlConnection conn;
        public StudentsController(IHttpClientFactory clientFactory)
        {
            //conn = new MySqlConnection("server=coms-309-mc-02.cs.iastate.edu;port=3306;database=StudentLink;user=root;password=46988c18374d9b7d;");
            //conn.Open();
            _clientFactory = clientFactory;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Students>>> GetStudents()
        {
            List<dynamic> list = new List<dynamic>();
            //MySqlCommand cmd = new MySqlCommand("select * from StudentLink.Students", conn);
            list = SQLConnection.get(typeof(Students));
            var ret = list.Cast<Students>();
            return Ok(ret);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<object>>> GetStudent(int id)
        {
            List<dynamic> student = new List<dynamic>();
            student = SQLConnection.get(typeof(Students), "*", "WHERE StudentID = " + id);
            return student;
        }
        // PUT: api/Students/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{canvasOAuthToken}")]
        public async Task<ActionResult<Students>> PutStudent( string canvasOAuthToken )
        {
            //INSERT INTO Table ((int)key1, key2, key3) VALUES (value1, 'value2', 'value3')
            //conn.Close();
            Networking network = new Networking(_clientFactory);
            Students myStu = network.getStudentProfile(canvasOAuthToken).Result;
            SQLConnection.insert(myStu); 
            //conn.Open();
            //MySqlCommand cmd = new MySqlCommand("insert into StudentLink.Students (StudentID, FullName, CourseIDs, Attributes, Classification, Major, UserType) values (" + myStu.StudentID + ", '" + myStu.FullName + "', '" + myStu.CourseIDs + "', '" + myStu.Attributes + "', '" + myStu.Classification + "', '" + myStu.Major + "', '" + myStu.UserType + "')", conn);
            //cmd.ExecuteReader();
            return myStu;
        }

        // POST: api/Students
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("{myStu}")]
        public async Task<ActionResult<Students>> PostStudent(Students myStu)
        {
            MySqlCommand cmd = new MySqlCommand("insert into StudentLink.Students (StudentID, FullName, CourseIDs, Attributes, Classification, Major, UserType) values (" + myStu.StudentID + ", " + myStu.FullName + ", " + myStu.CourseIDs + ", " + myStu.Attributes + ", " + myStu.Classification + ", " + myStu.Major + ", " + myStu.UserType + ")", conn);
            cmd.ExecuteReader();
            return CreatedAtAction("GetStudent", new { id = myStu.StudentID }, myStu);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> DeleteStudent(int id)
        {
            var student = GetStudent(id).Result.Value.ToList()[0];
            MySqlCommand cmd = new MySqlCommand("delete from StudentLink.Students where StudentID = " + id, conn);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    student = new Students()
                    {
                        StudentID = Convert.ToInt32(reader["StudentID"]),
                        FullName = reader["FullName"].ToString(),
                        CourseIDs = reader["CourseIDs"].ToString(),
                        Attributes = reader["Attributes"].ToString(),
                        Classification = reader["Classification"].ToString(),
                        Major = reader["Major"].ToString(),
                        UserType = reader["UserType"].ToString()
                    };
                }
            }
            return student;
        }
    }
}
