using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mysql_connect.Data;



namespace mysql_connect.Controllers
{
    public class LaptopAccesoriiController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LaptopAccesoriiController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var LaptopAccesorii = _context.LaptopAccesoriu.ToList();
            return View(LaptopAccesorii);
        }
    }
}

