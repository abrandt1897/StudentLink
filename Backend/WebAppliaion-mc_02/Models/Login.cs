using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_mc_02.Models
{
    public class Login
    {
        [Key]
        public int Username { get; set; }
        public int Email { get; set; }
        public string Password { get; set; }
    }
}
