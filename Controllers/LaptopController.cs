using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mysql_connect.Data;
using mysql_connect.Models;

namespace mysql_connect.Controllers
{
    public class LaptopController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LaptopController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Laptop
        public IActionResult Index()
        {
            var laptopList = _context.Laptop.ToList();
            return View(laptopList);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Laptop models)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Database.ExecuteSqlInterpolated($@"
                        INSERT INTO Laptop (ID_SO, Marca, Model, Pret, Caracteristici_Tehnice, Stoc)
                        VALUES ({models.ID_SO}, {models.Marca}, {models.Model}, {models.Pret}, {models.Caracteristici_Tehnice}, {models.Stoc});
                    ");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the record.");
                }
            }

            return View(models);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var laptop = _context.Laptop.Find(id);
            if (laptop == null)
            {
                return NotFound();
            }

            return View(laptop);
        }

        [HttpPost]
        public IActionResult Edit(int id, Laptop models)
        {
            if (id != models.ID_Laptop)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Database.ExecuteSqlInterpolated($@"
                        UPDATE Laptop
                        SET ID_SO = {models.ID_SO},
                            Marca = {models.Marca},
                            Model = {models.Model},
                            Pret = {models.Pret},
                            Caracteristici_Tehnice = {models.Caracteristici_Tehnice},
                            Stoc = {models.Stoc}
                        WHERE ID_Laptop = {id};
                    ");

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the record.");
                }
            }

            return View(models);
        }

        public IActionResult Details(int id)
        {
            var laptop = _context.Laptop.FromSqlRaw("SELECT * FROM Laptop WHERE ID_Laptop = {0}", id).FirstOrDefault();
            if (laptop == null)
            {
                return NotFound();
            }

            return View(laptop);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var laptop = _context.Laptop.Find(id);
            if (laptop == null)
            {
                return NotFound();
            }

            return View(laptop);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _context.Database.ExecuteSqlRaw("DELETE FROM Laptop WHERE ID_Laptop = {0}", id);
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
