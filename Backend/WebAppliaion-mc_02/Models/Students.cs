using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace WebApplication_mc_02.Models
{
    public class Students
    {
        [Key]
        public int StudentID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Attributes { get; set; }
        public string Classification { get; set; }        
        public string Major { get; set; }
        public string Role { get; set; }//'Student', 'TA', 'Admin'
        
        public IEnumerable<Claim> GetUserClaims()
        {
            IEnumerable<Claim> claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, FullName),
                new Claim("USERID", StudentID.ToString()),
                new Claim("CLASSIFICATION", Classification),
                new Claim(ClaimTypes.Role, Role),
                new Claim("MAJOR", Major)
            };
            return claims;
        }
        
    }
}
