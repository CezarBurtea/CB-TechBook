using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using mysql_connect.Data;

namespace mysql_connect.Models
{
    public class AccesoriuContext : ApplicationDbContext
    {
        public AccesoriuContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Accesoriu> Accesorii { get; set; }
    }

    public class Accesoriu
    {
        [Key] public int ID_Accesoriu { get; set; }
        public int ID_Categorie { get; set; }
        public string Tip { get; set; }
        public string Nume_Accesoriu { get; set; }
        public int Pret { get; set; }
        public string Stoc { get; set; }
        public string ImageUrl { get; set; }
    }
}

