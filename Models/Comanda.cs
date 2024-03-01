using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using mysql_connect.Data;

namespace mysql_connect.Models
{
    public class ComandaContext : ApplicationDbContext
    {
        public ComandaContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Comanda> Comenzi { get; set; }
    }

    public class Comanda
    {
        [Key] public int ID_Comanda { get; set; }
        public int ID_Client { get; set; }
        public string Data_Comenzii { get; set; }
        public string Detalii_Comanda { get; set; }
    }
}

