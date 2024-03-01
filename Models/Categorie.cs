using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using mysql_connect.Data;

namespace mysql_connect.Models
{
    public class CategorieContext : ApplicationDbContext
    {
        public CategorieContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Categorie> Cateogrii { get; set; }
    }

    public class Categorie
    {
        [Key] public int ID_Categorie { get; set; }
        public string Accesorii_Componente { get; set; }
    }
}

