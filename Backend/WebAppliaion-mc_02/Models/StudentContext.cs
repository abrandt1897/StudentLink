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
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
        public DbSet<Students> Students { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          optionsBuilder.UseMySQL("server=coms-309-mc-02.cs.iastate.edu;database=studentlink;uid=root;pwd=f49f3940f1069cd0;");
        }
    }
}
