using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mysql_connect.Data;
using mysql_connect.Models;

namespace mysql_connect.Controllers
{
    public class ComandaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComandaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comanda
        public IActionResult Index()
        {
            var comandaList = _context.Comenzi.ToList();
            return View(comandaList);
        }

        public IActionResult ShowSearchForm()
        {
            return View();
        }

        public IActionResult ShowSearchResults(String SearchPhrase)
        {
            var sql = "SELECT co.* FROM Comenzi co " +
                      "JOIN Clienti cl ON cl.ID_Client = co.ID_Client " +
                      $"WHERE cl.Nume LIKE '%{SearchPhrase}%'";

            var sql4 = "SELECT co.* FROM Comenzi co " +
           "WHERE EXISTS ( " +
           "SELECT ID_Client FROM Clienti cl " +
           $"WHERE cl.ID_Client = co.ID_Client AND cl.Nr_telefon LIKE '%{SearchPhrase}%')";

            var sql5 = "SELECT co.* FROM Comenzi co " +
           "WHERE EXISTS ( " +
           "SELECT ID_Client FROM Clienti cl " +
           $"WHERE cl.ID_Client = co.ID_Client AND cl.Email LIKE '%{SearchPhrase}%')";

            var sql6 = "SELECT co.* FROM Comenzi co " +
           "WHERE EXISTS ( " +
           "SELECT ID_Client FROM Clienti cl " +
           $"WHERE cl.ID_Client = co.ID_Client AND cl.Adresa LIKE '%{SearchPhrase}%')";

            var sql7 = "SELECT co.* FROM Comenzi co " +
           "WHERE EXISTS ( " +
           "SELECT ID_Client FROM Clienti cl " +
           $"WHERE cl.ID_Client = co.ID_Client AND cl.Prenume LIKE '%{SearchPhrase}%')";


            // var sql3 = "SELECT co.* FROM Comenzi co " +
            //"WHERE EXISTS ( " +
            //"SELECT ID_Client FROM Clienti cl " +
            //$"WHERE cl.ID_Client = co.ID_Client AND cl.Nr_telefon LIKE '%{SearchPhrase}%')";


            var searchResults = _context.Comenzi.FromSqlRaw(sql).ToList();

            var searchResults1 = _context.Comenzi.FromSqlRaw(sql4).ToList();

            var searchResults2 = _context.Comenzi.FromSqlRaw(sql5).ToList();

            var searchResults3 = _context.Comenzi.FromSqlRaw(sql6).ToList();

            var searchResults4 = _context.Comenzi.FromSqlRaw(sql7).ToList();


            if (searchResults.Count != 0)
            {
                return View("Index", searchResults);
            }
            else if (searchResults1.Count != 0)
            {
                return View("Index", searchResults1);
            }
            else if (searchResults2.Count != 0)
            {
                return View("Index", searchResults2);
            }
            else if (searchResults3.Count != 0)
            {
                return View("Index", searchResults3);
            }
            else if (searchResults4.Count != 0)
            {
                return View("Index", searchResults4);
            }

            return View("Index", ShowSearchResults);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Comanda model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Database.ExecuteSqlInterpolated($@"
                        INSERT INTO Comenzi (ID_Client, Data_Comenzii, Detalii_Comanda)
                        VALUES ({model.ID_Client}, {model.Data_Comenzii}, {model.Detalii_Comanda});
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
            var comanda = _context.Comenzi.Find(id);
            if (comanda == null)
            {
                return NotFound();
            }

            return View(comanda);
        }

        [HttpPost]
        public IActionResult Edit(int id, Comanda model)
        {
            if (id != model.ID_Comanda)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Database.ExecuteSqlInterpolated($@"
                        UPDATE Comenzi
                        SET ID_Client = {model.ID_Client},
                            Data_Comenzii = {model.Data_Comenzii},
                            Detalii_Comanda = {model.Detalii_Comanda}
                        WHERE ID_Comanda = {id};
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
            var comanda = _context.Comenzi.FromSqlRaw("SELECT * FROM Comenzi WHERE ID_Comanda = {0}", id).FirstOrDefault();
            if (comanda == null)
            {
                return NotFound();
            }

            return View(comanda);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var comanda = _context.Comenzi.Find(id);
            if (comanda == null)
            {
                return NotFound();
            }

            return View(comanda);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _context.Database.ExecuteSqlRaw("DELETE FROM Comenzi WHERE ID_Comanda = {0}", id);
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
