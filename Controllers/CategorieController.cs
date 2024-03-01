using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mysql_connect.Data;
using mysql_connect.Models;

namespace mysql_connect.Controllers
{
    public class CategorieController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategorieController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categorie
        public IActionResult Index()
        {
            var categorieList = _context.Cateogrii.ToList();
            return View(categorieList);
        }

        public IActionResult ShowSearchForm()
        {
            return View();
        }

        public IActionResult ShowSearchResults(String SearchPhrase)
        {
            var sql = "SELECT c.* FROM Cateogrii c " +
                      "JOIN Accesorii a ON c.ID_Categorie = a.ID_Categorie " +
                      $"WHERE a.Tip LIKE '%{SearchPhrase}%'";

            var sql1 = "SELECT c.* FROM Cateogrii c " +
                      "JOIN Accesorii a ON c.ID_Categorie = a.ID_Categorie " +
                      $"WHERE a.Stoc LIKE '%{SearchPhrase}%'";

            var sql2 = "SELECT c.* FROM Cateogrii c " +
          "JOIN Accesorii a ON c.ID_Categorie = a.ID_Categorie " +
          $"WHERE a.Pret LIKE '%{SearchPhrase}%'";

            var sql3 = "SELECT c.* FROM Cateogrii c " +
          "JOIN Accesorii a ON c.ID_Categorie = a.ID_Categorie " +
          $"WHERE a.Nume_Accesoriu LIKE '%{SearchPhrase}%'";




            var searchResults = _context.Cateogrii.FromSqlRaw(sql).ToList();

            var searchResults1 = _context.Cateogrii.FromSqlRaw(sql1).ToList();

            var searchResults2 = _context.Cateogrii.FromSqlRaw(sql2).ToList();

            var searchResults3 = _context.Cateogrii.FromSqlRaw(sql3).ToList();

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
            return View("Index", ShowSearchResults);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Categorie model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Database.ExecuteSqlInterpolated($@"
                        INSERT INTO Cateogrii (Accesorii_Componente)
                        VALUES ({model.Accesorii_Componente});
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
            var categorie = _context.Cateogrii.Find(id);
            if (categorie == null)
            {
                return NotFound();
            }

            return View(categorie);
        }

        [HttpPost]
        public IActionResult Edit(int id, Categorie model)
        {
            if (id != model.ID_Categorie)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Database.ExecuteSqlInterpolated($@"
                        UPDATE Cateogrii
                        SET Accesorii_Componente = {model.Accesorii_Componente}
                        WHERE ID_Categorie = {id};
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
            var categorie = _context.Cateogrii.FromSqlRaw("SELECT * FROM Cateogrii WHERE ID_Categorie = {0}", id).FirstOrDefault();
            if (categorie == null)
            {
                return NotFound();
            }

            return View(categorie);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var categorie = _context.Cateogrii.Find(id);
            if (categorie == null)
            {
                return NotFound();
            }

            return View(categorie);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _context.Database.ExecuteSqlRaw("DELETE FROM Cateogrii WHERE ID_Categorie = {0}", id);
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
