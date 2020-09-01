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

namespace WebApplication_mc_02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentContext DB;

        public StudentsController(StudentContext context)
        {
            DB = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Students>>> GetStudents()
        {
            Students[] s = DB.Students.ToArray<Students>();
            foreach(Students student in s)
            {
                if(student.Attributes.Length > 0)
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
            var student = await DB.Students.FindAsync(id);
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
        public async Task<IActionResult> PutStudent(int id, [FromBody] Students student)
        {
            //DB.Entry(student).State = EntityState.Modified;
            DB.Add(student);
            try
            {
                DB.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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
}
