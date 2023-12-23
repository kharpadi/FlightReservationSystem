using FlightReservationSystem.Data;
using FlightReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationSystem.Controllers
{
    public class PlaneController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PlaneController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Plane> PlaneList = _context.planes;
            return View(PlaneList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Plane plane)
        {
            _context.planes.Add(plane);
            _context.SaveChanges();
            return RedirectToAction("Index","Plane");
        }

        public IActionResult Update(int? Id)
        {
            if (Id == null || Id == null)
            {
                return NotFound();
            }
            var PlaneFromDb = _context.planes.Find(Id);
            if (PlaneFromDb == null)
            {
                return NotFound();
            }
            return View(PlaneFromDb);
        }

        [HttpPost]
        public IActionResult Update(Plane updatedPlane) 
        {
            if(ModelState.IsValid)
            {
                _context.planes.Update(updatedPlane);
                _context.SaveChanges();
                return RedirectToAction("Index", "Plane");
            }
            return View(updatedPlane);
        }

        public IActionResult Delete(int? Id)
        {
            var existingPlane = _context.planes.Find(Id);
            if (existingPlane == null)
            {
                NotFound();
            }
            _context.planes.Remove(existingPlane);
            _context.SaveChanges();
            return RedirectToAction("Index", "Plane");
        }
    }
}
