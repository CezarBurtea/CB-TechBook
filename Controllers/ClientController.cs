using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mysql_connect.Data;
using mysql_connect.Models;

namespace mysql_connect.Controllers
{
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Client
        public IActionResult Index()
        {
            var clientList = _context.Clienti.ToList();
            return View(clientList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Client model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Database.ExecuteSqlInterpolated($@"
                        INSERT INTO Clienti (Nume, Prenume, Adresa, Nr_telefon, Email)
                        VALUES ({model.Nume}, {model.Prenume}, {model.Adresa}, {model.Nr_telefon}, {model.Email});
                    ");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the record.");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var client = _context.Clienti.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpPost]
        public IActionResult Edit(int id, Client model)
        {
            if (id != model.ID_Client)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Database.ExecuteSqlInterpolated($@"
                        UPDATE Clienti
                        SET Nume = {model.Nume},
                            Prenume = {model.Prenume},
                            Adresa = {model.Adresa},
                            Nr_telefon = {model.Nr_telefon},
                            Email = {model.Email}
                        WHERE ID_Client = {id};
                    ");

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the record.");
                }
            }

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var client = _context.Clienti.FromSqlRaw("SELECT * FROM Clienti WHERE ID_Client = {0}", id).FirstOrDefault();
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var client = _context.Clienti.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _context.Database.ExecuteSqlRaw("DELETE FROM Clienti WHERE ID_Client = {0}", id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log or print exception details
                Console.WriteLine(ex.ToString());
                return RedirectToAction(nameof(Delete), new { id, error = true });
            }
        }
    }
}
