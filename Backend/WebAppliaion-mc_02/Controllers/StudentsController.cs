using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_mc_02.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Nancy.Json;
using Microsoft.Diagnostics.Instrumentation.Extensions.Intercept;
using System.Diagnostics;

namespace WebApplication_mc_02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentContext DB;
        private readonly IHttpClientFactory _clientFactory;
        public StudentsController(StudentContext context, IHttpClientFactory clientFactory)
        {
            DB = context;
            _clientFactory = clientFactory;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Students>>> GetStudents()
        {
            Students[] s = DB.Students.ToArray<Students>();
            foreach(Students student in s)
            {
                if(student.Attributes != null && student.Attributes.Length > 0)
                    student.Attributes = student.Attributes.Trim();
                student.Classification = student.Classification.Trim();
                student.CourseIDs = student.CourseIDs.Trim();
                student.FullName = student.FullName.Trim();
            }
            return s;
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Students>> GetStudent(int id)
        {
            Students student = await DB.Students.FindAsync(id);
            if (student.Attributes.Length > 0)
                student.Attributes = student.Attributes.Trim();
            student.Classification = student.Classification.Trim();
            student.CourseIDs = student.CourseIDs.Trim();
            student.FullName = student.FullName.Trim();
            if (student == null)
            {
                return NotFound();
            }

            return student;
        }
        // PUT: api/Students/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(string canvasOAuthToken)
        {
            //DB.Entry(student).State = EntityState.Modified;
            //DB.Add(student);

            var request = new HttpRequestMessage(HttpMethod.Get, "https://canvas.instructure.com/api/v1/courses?enrollment_state=active&include[]=sections&include[]=term&include[]=concluded");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");
            string[] path = this.Request.Path.Value.Split("/");
            request.Headers.Add("Authorization", "Bearer " + path[path.Length-1]);

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                JavaScriptSerializer j = new JavaScriptSerializer();
                Object o = j.DeserializeObject(responseString);
                Students myStu = new Students();
                myStu.FullName = "Adam Brandt";
                myStu.Classification = "Junior";
                myStu.Major = "Software Engineering";
                myStu.StudentID = 734236833;
                myStu.UserType = "Student";
                foreach (Nancy.Json.Simple.JsonObject o1 in (Nancy.Json.Simple.JsonArray)o)
                {
                    int idx = 0;
                    foreach (string s in o1.Keys)
                    {
                        if (s.Equals("id"))
                            break;
                        idx++;
                    }
                    IEnumerator list = o1.Values.GetEnumerator();
                    for (int i = 0; i < idx + 1; i++)
                        list.MoveNext();
                    object ID = list.Current;
                    
                    myStu.CourseIDs += ID.ToString() + " ";
                    
                    System.Diagnostics.Debug.WriteLine("**" + ID.ToString());
                }
                DB.Add(myStu);
                System.Diagnostics.Debug.WriteLine("big happy");

                
            }
            else
            {
                Console.WriteLine("big sad");
            }
            try
            {
                DB.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(0))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Students>> PostStudent(Students student)
        {
            DB.Students.Add(student);
            await DB.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.StudentID }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Students>> DeleteStudent(int id)
        {
            var student = await DB.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            DB.Students.Remove(student);
            await DB.SaveChangesAsync();

            return student;
        }

        private bool StudentExists(int id)
        {
            return DB.Students.Any(e => e.StudentID == id);
        }
    }
    class courseObj
    {
        public int id { get; set; }
        public string name { get; set; }
        public Boolean concluded { get; set; }
    }
}
