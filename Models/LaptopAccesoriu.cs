using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using mysql_connect.Data;

namespace mysql_connect.Models
{
    public class LaptopAccesoriuContext : ApplicationDbContext
    {
        public LaptopAccesoriuContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<LaptopAccesoriu> LaptopAccesorii { get; set; }

        
    }

    public class LaptopAccesoriu
    {
        
        [Key]public int ID_Laptop { get; set; }

        
        [Key]public int ID_Accesoriu { get; set; }
    }
}
