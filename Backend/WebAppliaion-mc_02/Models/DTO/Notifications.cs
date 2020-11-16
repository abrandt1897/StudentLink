using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_mc_02.Models.DTO
{
    public class Notifications
    {
        public int Record { get; set; } = 0;
        public int StudentID { get; set; }
        public string Data { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
