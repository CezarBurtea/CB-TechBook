using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mysql_connect.Models;

namespace mysql_connect.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Laptop> Laptop { get; set; }
        public DbSet<Accesoriu> Accesorii { get; set; }
        public DbSet<Categorie> Cateogrii { get; set; }
        public DbSet<Client> Clienti { get; set; }
        public DbSet<Comanda> Comenzi { get; set; }
        public DbSet<SO> SOs { get; set; }
        public DbSet<AccesoriuComenzi> AccesoriuComenzi { get; set; }
        public DbSet<LaptopAccesoriu> LaptopAccesoriu { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AccesoriuComenzi>()
                .HasKey(ac => new { ac.ID_Accesoriu, ac.ID_Comenzi });

            modelBuilder.Entity<LaptopAccesoriu>()
                .HasKey(la => new { la.ID_Laptop, la.ID_Accesoriu });
        }
    }
}
