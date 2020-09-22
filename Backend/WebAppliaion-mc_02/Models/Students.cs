using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace WebApplication_mc_02.Models
{
    public class Students
    {
        [Key]
        public int StudentID { get; set; }
        public string FullName { get; set; }
        public string CourseIDs { get; set; }
        public string Attributes { get; set; }
        public string Classification { get; set; }        
        public string Major { get; set; }
        public string UserType { get; set; }
        public string Friends { get; set; }
    }
}
