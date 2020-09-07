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
using System.Text;

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
            return DB.Students.ToArray<Students>();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Students>> GetStudent(int id)
        {
            Students student = await DB.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return student;
        }
        // PUT: api/Students/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public async Task<IActionResult> PutStudent( string canvasOAuthToken)
        {
            //DB.Entry(student).State = EntityState.Modified;
            //DB.Add(student);
            Networking network = new Networking(_clientFactory);
            Students myStu = network.getStudentProfile(canvasOAuthToken).Result;
            DB.Add(myStu);
            try
            {
                await DB.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(myStu.StudentID))
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
                return NotFound();
            DB.Students.Remove(student);
            await DB.SaveChangesAsync();
            return student;
        }

        private bool StudentExists(int id)
        {
            return DB.Students.Any(e => e.StudentID == id);
        }
    }
}
