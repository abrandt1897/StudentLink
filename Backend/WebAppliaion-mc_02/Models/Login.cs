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
        public string UserID { get; set; }
        public string Password { get; set; }
    }
}
