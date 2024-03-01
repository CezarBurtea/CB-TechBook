using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mysql_connect.Data;
using mysql_connect.Models;

namespace mysql_connect.Controllers
{
    public class AccesoriuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccesoriuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var accesoriuList = _context.Accesorii.ToList();
            return View(accesoriuList);
        }

        


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Accesoriu model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Database.ExecuteSqlInterpolated($@"
                INSERT INTO Accesorii (ID_Categorie, Tip, Nume_Accesoriu, Pret, Stoc, ImageUrl)
                VALUES ({model.ID_Categorie}, {model.Tip}, {model.Nume_Accesoriu}, {model.Pret}, {model.Stoc}, {model.ImageUrl});
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
            var accesoriu = _context.Accesorii.Find(id);
            if (accesoriu == null)
            {
                return NotFound();
            }

            return View(accesoriu);
        }

        [HttpPost]
        public IActionResult Edit(int id, Accesoriu model)
        {
            if (id != model.ID_Accesoriu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Assuming _context is an instance of DbContext
                    _context.Database.ExecuteSqlInterpolated($@"
                UPDATE Accesorii
                SET ID_Categorie = {model.ID_Categorie},
                    Tip = {model.Tip},
                    Nume_Accesoriu = {model.Nume_Accesoriu},
                    Pret = {model.Pret},
                    Stoc = {model.Stoc},
                    ImageUrl = {model.ImageUrl}
                WHERE ID_Accesoriu = {id};
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
            var accesoriu = _context.Accesorii.FromSqlRaw("SELECT * FROM Accesorii WHERE ID_Accesoriu = {0}", id).FirstOrDefault();
            if (accesoriu == null)
            {
                return NotFound();
            }

            return View(accesoriu);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var accesoriu = _context.Accesorii.Find(id);
            if (accesoriu == null)
            {
                return NotFound();
            }

            return View(accesoriu);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _context.Database.ExecuteSqlRaw("DELETE FROM Accesorii WHERE ID_Accesoriu = {0}", id);
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
