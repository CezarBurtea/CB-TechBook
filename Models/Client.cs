using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using mysql_connect.Data;

namespace mysql_connect.Models
{
    public class ClientContext : ApplicationDbContext
    {
        public ClientContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Client> Clienti { get; set; }
    }

    public class Client
    {
        [Key] public int ID_Client { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Adresa { get; set; }
        public string Nr_telefon { get; set; }
        public string Email { get; set; }
    }
}

