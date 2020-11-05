using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication_mc_02.Models;
using WebApplication_mc_02.Models.DTO;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication_mc_02.Controllers
{
    /// <summary>
    /// Interface with the Courses Database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        // GET: api/<CoursesController>
        /// <summary>
        /// gets the list of all courses
        /// </summary>
        /// <returns>a list of courses</returns>
        [HttpGet]
        public IEnumerable<Courses> Get()
        {
            List<Courses> courses = new List<Courses>();
            courses = SQLConnection.get<Courses>(typeof(Courses));
            return courses;
        }

        // GET api/<CoursesController>/5
        /// <summary>
        /// gets a list of all courses associated with a student ID
        /// </summary>
        /// <param name="id">Student ID</param>
        /// <returns>List of Courses</returns>
        [HttpGet("{id}")]
        public IEnumerable<Courses> Get(int id)
        {
            List<Courses> courses = new List<Courses>();
            List<Student2CourseMap> courseIDs = SQLConnection.get<Student2CourseMap>(typeof(Student2CourseMap), $"Where StudentID={id}");
            foreach(Student2CourseMap course in courseIDs)
            {
                courses.AddRange(SQLConnection.get<Courses>(typeof(Courses), $"Where CourseID={course.CourseID}"));
            }
            return courses;
        }
    }
}
