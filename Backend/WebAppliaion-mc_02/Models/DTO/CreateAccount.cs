using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_mc_02.Models.DTO
{
    public class CreateAccount
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string canvasOAuthToken { get; set; }
    }
}
