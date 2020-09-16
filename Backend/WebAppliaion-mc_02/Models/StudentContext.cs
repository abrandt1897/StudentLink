using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace WebApplication_mc_02.Models
{
    public class StudentContext : DbContext
{
        public StudentContext(DbContextOptions<StudentContext> options)
            : base(options)
        {
        }
        public string ConnectionString { get; set; }
        public DbSet<Students> Students { get; set; }
    }
}
