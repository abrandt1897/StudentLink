using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication_mc_02.Models;
using System.Net.Http;
using MySql.Data.MySqlClient;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using WebApplication_mc_02.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication_mc_02.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class LoginController : Controller
    {
        
        // GET api/<LoginController>/5
        // POST api/<LoginController>
        /// <summary>
        /// Login for a student
        /// </summary>
        /// <param name="loginForm"></param>
        /// <returns>Student ID and Bearer token split buy a space.</returns>
        [HttpPost]
        public ActionResult<string> PostLogin([Bind("Username,Password")] Login loginForm)
        {
            return PutLogin(loginForm);
        }
        
        // PUT api/<LoginController>/5
        /// <summary>
        /// saves a token for the user
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Password"></param>
        /// <returns>token for users access</returns>
        [HttpPut]
        public ActionResult<string> PutLogin([Bind("Username,Password")] Login loginForm)
        {
            List<Students> students = SQLConnection.get<Students>(typeof(Students), $"WHERE Username='{loginForm.Username}'");
            if (students.Count < 1)
                return BadRequest("student doesnt exist");
            Students student = students[0];
            //Authenticate User, Check if it’s a registered user in Database
            if (loginForm == null)
                return "No student under that username";
            string hashedPasswd = hashPassword(loginForm.Password);
            if (hashedPasswd == student.Password)
            {
                //Authentication successful, Issue Token with user credentials
                //Provide the security key which was given in the JWToken configuration in Startup.cs
                var key = Encoding.ASCII.GetBytes("YuurKey-2374-OFFKDI94LMAO:56753253-yeet-6969-0420-kfirox29zoxv");
                //Generate Token for user 
                var JWToken = new JwtSecurityToken(
                    issuer: "http://localhost:5001/",
                    audience: "http://localhost:5001/",
                    claims: student.GetUserClaims(),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                    //Using HS256 Algorithm to encrypt Token
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                );
                var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                var studentID = SQLConnection.get<Students>(typeof(Students), $"Where Username='{loginForm.Username}'", "StudentID")[0].StudentID.ToString();
                try
                {
                    HttpContext.Response.Headers.Add("JWToken", token);
                    HttpContext.Response.Headers.Add("StudentID", studentID);
                    HttpContext.Response.Cookies.Append("JWToken", token);
                    HttpContext.Response.Cookies.Append("StudentID", studentID);
                }
                catch (Exception e){ }
                return studentID + " " + token;
            }else
                return "Wrong Password";
        }
        public static string hashPassword(string Password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string hashedPW = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: Password,
                salt: new byte[1],
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashedPW;
        }
        // DELETE api/<LoginController>/5
        /// <summary>
        /// Deletes a bearer token from user
        /// </summary>
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Student,TA,Admin")]
        public void Delete()
        {
            HttpContext.Session.Remove("JWToken");
        }
    }
}
