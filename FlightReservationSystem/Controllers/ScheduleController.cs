using FlightReservationSystem.Data;
using FlightReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationSystem.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ScheduleController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Schedule> ScheduleList = _context.schedules.Include(p=>p.Plane).Include(r=>r.Route);
            return View(ScheduleList);
        }

        public IActionResult Create()
        {
            
            // Plane verilerini al ve ViewBag'e ekle
            ViewBag.PlaneList= _context.planes.ToList();

            // Route verilerini al ve ViewBag'e ekle
            ViewBag.RouteList = _context.routes.ToList();
            return View ();
        }


        [HttpPost]  
        public IActionResult Create(Schedule schedule)
        {
            // Plane verilerini al ve ViewBag'e ekle
            ViewBag.PlaneList = _context.planes.ToList();

            // Route verilerini al ve ViewBag'e ekle
            ViewBag.RouteList = _context.routes.ToList();
            var isExisting = _context.schedules.FirstOrDefault(s=>s.Route == schedule.Route);
            if (isExisting!=null && isExisting.DepartureTime==schedule.DepartureTime) 
            {
                ModelState.AddModelError("Schedule", "There cannot be more than one flight to the same route at the same time");
                return View(schedule);
            }
            var existingPlane = _context.schedules.FirstOrDefault(s => s.Plane == schedule.Plane);
            if (existingPlane!=null && existingPlane.DepartureTime==schedule.DepartureTime) 
            {
                ModelState.AddModelError("Plane", "A plane cannot make more than one flight at the same time");
                return View(schedule);
            }
            if(ModelState.IsValid)
            {
                _context.schedules.Add(schedule);
                _context.SaveChanges();
                return RedirectToAction("Index","Schedule");
            }
            return View(schedule);
        }

        public IActionResult Update(int? Id)
        {
            if (Id == null || Id == null)
            {
                return NotFound();
            }
            var ScheduleFromDb = _context.schedules.Find(Id);
            if (ScheduleFromDb == null)
            {
                return NotFound();
            }
            return View(ScheduleFromDb);
        }

        [HttpPost]
        public IActionResult Update(Schedule updatedSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.routes.Update(updatedSchedule);
                _context.SaveChanges();
                return RedirectToAction("Index", "Route");
            }
            return View(updatedRoute);
        }
    }
}
