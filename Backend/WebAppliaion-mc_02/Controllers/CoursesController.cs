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
        public async Task<IEnumerable<Courses>> Get()
        {
            List<Courses> courses = new List<Courses>();
            courses = await SQLConnection.Get<Courses>();
            return courses;
        }

        // GET api/<CoursesController>/5
        /// <summary>
        /// gets a list of all courses associated with a student ID
        /// </summary>
        /// <param name="id">Student ID</param>
        /// <returns>List of Courses</returns>
        [HttpGet("{id}")]
        public async Task<IEnumerable<Courses>> GetUser(int id)
        {
            List<Courses> courses = new List<Courses>();
            List<Student2CourseMap> courseIDs = await SQLConnection.Get<Student2CourseMap>( $"Where StudentID={id}");
            foreach(Student2CourseMap course in courseIDs)
            {
                courses.AddRange(await SQLConnection.Get<Courses>($"Where CourseID={course.CourseID}"));
            }
            return courses;
        }
    }
}
