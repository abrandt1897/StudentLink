using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace WebApplication_mc_02.Models
{
    public class Courses
    {
        [Key]
        public int CourseID { get; set; }
        public string Name { get; set; }
        public int Mentors { get; set; }
        public int TAs { get; set; }
        public string Section { get; set; }
        public string Term { get; set; }
        public int TermID { get; set; }
        public int SectionID { get; set; }
    }
}
