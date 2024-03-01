using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mysql_connect.Data;
using mysql_connect.Models;

namespace mysql_connect.Controllers
{
    public class SOController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SOController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SO
        public IActionResult Index()
        {
            var soList = _context.SOs.ToList();
            return View(soList);
        }

        public IActionResult ShowSearchForm()
        {
            return View();
        }

        public IActionResult ShowSearchResults(String SearchPhrase)
        {
            var sql = "SELECT s.* FROM SOs s " +
                     "JOIN Laptop l ON l.ID_SO = s.ID_SO " +
                     $"WHERE l.Stoc LIKE '%{SearchPhrase}%'";

            var searchResults = _context.SOs.FromSqlRaw(sql).ToList();

            return View("Index", searchResults);



        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Database.ExecuteSqlInterpolated($@"
                        INSERT INTO SOs (Sistem_operare)
                        VALUES ({model.Sistem_operare});
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
            var so = _context.SOs.Find(id);
            if (so == null)
            {
                return NotFound();
            }

            return View(so);
        }

        [HttpPost]
        public IActionResult Edit(int id, SO model)
        {
            if (id != model.ID_SO)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Database.ExecuteSqlInterpolated($@"
                        UPDATE SOs
                        SET Sistem_operare = {model.Sistem_operare}
                        WHERE ID_SO = {id};
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
            var so = _context.SOs.FromSqlRaw("SELECT * FROM SOs WHERE ID_SO = {0}", id).FirstOrDefault();
            if (so == null)
            {
                return NotFound();
            }

            return View(so);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var so = _context.SOs.Find(id);
            if (so == null)
            {
                return NotFound();
            }

            return View(so);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _context.Database.ExecuteSqlRaw("DELETE FROM SOs WHERE ID_SO = {0}", id);
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
