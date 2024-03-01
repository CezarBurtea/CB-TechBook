using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using mysql_connect.Data;
using mysql_connect.Models;

namespace mysql_connect.Controllers
{
    public class AccesoriuComenziController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccesoriuComenziController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var accesoriuComenziList = _context.AccesoriuComenzi.ToList();
            return View(accesoriuComenziList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            // You may want to pass necessary data to the view (e.g., dropdown values) if needed
            return View();
        }

        [HttpPost]
        public IActionResult Create(AccesoriuComenzi model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.AccesoriuComenzi.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the record.");
                }
            }

            return View(model);
        }
    }
}
