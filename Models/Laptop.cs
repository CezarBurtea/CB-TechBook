using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using mysql_connect.Data;

namespace mysql_connect.Models
{
    public class LaptopContext : ApplicationDbContext
    {
        public LaptopContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
		public DbSet<Laptop> Laptopuri { get; set; }
    }

    public class Laptop
	{
		[Key] public int ID_Laptop { get; set; }
		public int ID_SO { get; set; }
		public string Marca { get; set; }
		public string Model { get; set; }
		public int Pret { get; set; }
		public string Caracteristici_Tehnice { get; set; }
		public string Stoc { get; set; }


		
	}
}
		
