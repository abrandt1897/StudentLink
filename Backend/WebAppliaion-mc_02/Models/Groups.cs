using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_mc_02.Models
{
    public class Groups
    {
        [Key]
        public int ID { get; set; }
        public int Students { get; set; }
        public int TAs { get; set; }
        public int Mentors { get; set; }
    }
}
