using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_mc_02.Models
{
    public class Student2CourseMap
    {
        public int StudentID { get; set; }
        public long CourseID { get; set; } 
        public bool CurrentlyEnrolled { get; set; }
        public string UserType { get; set; }

    }


}
