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
            student = SQLConnection.get(typeof(Students), "*", "WHERE StudentID = " + id);
            return student;
        }
        // PUT: api/Students/{canvasOAuthToken}
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{canvasOAuthToken}")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<string>> PutStudent( string canvasOAuthToken, [FromBody] Login myuser )
        {
            Networking network = new Networking(_clientFactory);
            Students myStu = network.getStudentProfile(canvasOAuthToken).Result;
            var token = new LoginController().PutLogin(myuser.Username, myuser.Password);
            myStu.Username = myuser.Username;
            myStu.Password = LoginController.hashPassword(myuser.Password);
            SQLConnection.insert(myStu);
            return token;
        }

        // POST: api/Students
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("{myStu}")]
        public async Task<ActionResult<Students>> PostStudent(Students myStu)
        {
            SQLConnection.update(myStu);
            return CreatedAtAction("GetStudent", new { id = myStu.StudentID }, myStu);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> DeleteStudent(int id)
        {

            var student = GetStudent(id).Result.Value.ToList()[0];
            return student;
        }
    }
}
