using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using vroom.Data;
using vroom.Models;

namespace vroom.Controllers
{
    [Authorize(Roles ="Executive,Admin")]
    public class MakeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MakeController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [HttpGet]

        public IActionResult Index()
        {
            var make = _context.Makes.ToList();
            return View(make);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Make model)
        {
            if (ModelState.IsValid)
            {
            _context.Add(model);
            _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var make = _context.Makes.Find(id);
            if(make == null)
            {
                return NotFound();
            }
            else
            {
                _context.Remove(make);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var make = _context.Makes.Find(id);
            if (make == null)
            {
                return NotFound();
            }
           
            return View(make);
        }

        [HttpPost]
        public IActionResult Edit(Make make)
        {
            if (ModelState.IsValid)
            { 
            _context.Update(make);
            _context.SaveChanges();
            return RedirectToAction("Index");
            }
            return View(make);
        }

        //public IActionResult Edit(int id)
        //{

        //}
        [HttpGet]
        public IActionResult Bikes()
        {
            Make make = new Make()
            {
                Id = 1,
                Name = "harley davidson"
            };
            return View(make);
        }
    }
}
