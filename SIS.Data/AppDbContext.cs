using App.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace App.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=SIS_DB;Integrated Security=True;Trusted_Connection=True");

            base.OnConfiguring(optionsBuilder);
        }


    }
}
