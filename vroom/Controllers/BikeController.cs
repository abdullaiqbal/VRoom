using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vroom.Data;
using vroom.Models.ViewModel;
using Microsoft.Extensions.Hosting.Internal;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using vroom.Models;

namespace vroom.Controllers
{
    
    public class BikeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _hostingEnvironment;
        [BindProperty]
        public BikeViewModel BikeVM { get; set; }
        public BikeController(ApplicationDbContext db, IHostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            BikeVM = new BikeViewModel()
            {
                Makes = _db.Makes.ToList(),
                Models = _db.Models.ToList(),
                Bike = new Models.Bike()
            };
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            var Bikes = _db.Bikes.Include(m => m.Make).Include(m => m.Model);
                        
            return View(Bikes.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(BikeVM);
        }

        [HttpPost, ActionName("Create")]
        public IActionResult PostCreate()
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(BikeVM);
            //}
            
            
            UploadImageIfAvailable();
            _db.Bikes.Add(BikeVM.Bike);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        private void UploadImageIfAvailable()
        {
            //Get BikeID we have saved in database            
            //var BikeID = BikeVM.Bike.Id;


            //Get wwrootPath to save the file on server
            string wwrootPath = _hostingEnvironment.WebRootPath;

            //Get the Uploaded files
            var files = HttpContext.Request.Form.Files;

            //Get the reference of DBSet for the bike we have saved in our database
            //var SavedBike = _db.Bikes.Find(BikeID);


            //Upload the file on server and save the path in database if user have submitted file
            if (files.Count != 0)
            {
                string fileName = Guid.NewGuid().ToString();
                //Extract the extension of submitted file
                var Extension = Path.GetExtension(files[0].FileName);

                //Create the relative image path to be saved in database table 
                //var RelativeImagePath = Image.BikeImagePath + BikeID + Extension;
                var ImagePath = @"Images\Bikes\";
                //var RelativeImagePath = Image.BikeImagePath + BikeID + Extension;
                var RelativeImagePath = ImagePath + fileName + Extension;

                //Create absolute image path to upload the physical file on server
                var AbsImagePath = Path.Combine(wwrootPath, RelativeImagePath);


                //Upload the file on server using Absolute Path
                using (var filestream = new FileStream(AbsImagePath, FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }

                //Set the path in database
                //SavedBike.ImagePath = Convert.ToString(RelativeImagePath);
                BikeVM.Bike.ImagePath = RelativeImagePath;
            }
        }


    }
}
