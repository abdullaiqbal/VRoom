using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using vroom.Data;
using vroom.Models;
using vroom.Models.ViewModel;
using Model = vroom.Models.Model;

namespace vroom.Controllers
{
    [Authorize(Roles = "Executive,Admin")]
    public class ModelController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ModelViewModel ModelVM { get; set; }
        public ModelController(ApplicationDbContext db)
        {
            _db = db;
      
            ModelVM = new ModelViewModel()
            {
                Makes = _db.Makes.ToList(),
                Model = new Models.Model()
            };
        }
        public IActionResult Index()
        {
            var model = _db.Models.Include(m => m.Make);
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(ModelVM);
        }

        [HttpPost, ActionName("Create")]
        public IActionResult PostCreate()
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(ModelVM);
            //}
            _db.Models.Add(ModelVM.Model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //HTTP Get Method
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ModelVM.Model = _db.Models.Include(m => m.Make).SingleOrDefault(m => m.Id == id);
            if (ModelVM.Model == null)
            {
                return NotFound();
            }

            return View(ModelVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(ModelVM);
            //}

            _db.Update(ModelVM.Model);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //[HttpPost, ActionName("Delete")]
        public IActionResult Delete(int Id)
        {
            //Model model = _db.Models.Find(id);
            Model model = _db.Models.Find(Id);
            if (model == null)
            {
                return NotFound();
            }
            _db.Models.Remove(model);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }



        [AllowAnonymous]
        [HttpGet("api/models")]
        public IEnumerable<Model> Models()
        {
            //var models = _db.Models.ToList();
            //return _mapper.Map<List<Model>, List<ModelResources>>(models);

            //var modelResources = models
            //    .Select(m => new ModelResources
            //    {
            //        Id = m.Id,
            //        Name = m.Name
            //    }).ToList();

            return _db.Models.ToList();
        }
    }
}
