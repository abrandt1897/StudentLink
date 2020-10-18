﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication_mc_02.Models;
using System.Net.Http;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApplication_mc_02.Models.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApplication_mc_02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        public StudentsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Students>>> GetStudents()
        {
            List<dynamic> list = new List<dynamic>();
            list = SQLConnection.get(typeof(Students));
            var ret = list.Cast<Students>();
            return Ok(ret);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<object>>> GetStudent(int id)
        {
            List<dynamic> student = new List<dynamic>();
            student = SQLConnection.get(typeof(Students), "WHERE StudentID = " + id)[0];
            return student;
        }
        // PUT: api/Students/{canvasOAuthToken}
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{canvasOAuthToken}")]
        [AllowAnonymous]
        public async Task<ActionResult<Students>> PutStudent( string canvasOAuthToken, [FromBody] Login myuser )
        {
            Networking network = new Networking(_clientFactory);
            Students myStu = network.getStudentProfile(canvasOAuthToken).Result;
            myStu.Username = myuser.Username;
            myStu.Password = LoginController.hashPassword(myuser.Password);
            if(SQLConnection.insert(myStu))
                return Ok(myStu);
            return BadRequest("try being gud");
        }

        // POST: api/Students
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Student,Admin,Host")]
        public async Task<ActionResult<Students>> PostStudent([FromBody] Students myStu)
        {
            SQLConnection.update(myStu);
            return CreatedAtAction("GetStudent", new { id = myStu.StudentID }, myStu);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Student,Admin,Host")]
        public async Task<ActionResult<object>> DeleteStudent(int id)
        {
            var student = SQLConnection.get(typeof(Students), $"WHERE StudentID = {id}")[0];
            if (SQLConnection.delete(student))
                return Ok(student);
            return BadRequest("sum went wrong, idk");
        }
    }
}
