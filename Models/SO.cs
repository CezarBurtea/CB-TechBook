using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using mysql_connect.Data;

namespace mysql_connect.Models
{
    public class SOContext : ApplicationDbContext
    {
        public SOContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<SO> SOs { get; set; }
    }

    public class SO
    {
        [Key] public int ID_SO { get; set; }
        public string Sistem_operare { get; set; }
    }
}

