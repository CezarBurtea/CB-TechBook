using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using mysql_connect.Data;

namespace mysql_connect.Models
{
    public class AccesoriuComenziContext : ApplicationDbContext
    {
        public AccesoriuComenziContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<AccesoriuComenzi> AccesoriuComenzi { get; set; }

       
    }

    public class AccesoriuComenzi
    {
        [Key]
        public int ID_Accesoriu { get; set; }
        [Key]
        public int ID_Comenzi { get; set; }

    }
}
