using FlightReservationSystem.Data;
using FlightReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Route = FlightReservationSystem.Models.Route;

namespace FlightReservationSystem.Controllers
{
    public class RouteController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RouteController(ApplicationDbContext context) 
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Route> RouteList = _context.routes; 
            return View(RouteList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Route route)
        {
            if (route.DepartureCity == route.ArrivalCity) 
            {
                ModelState.AddModelError("DepartureCity", "Departure city cannot be the same as Arrival city");
                return View(route);
            }
            var isExist=_context.routes.FirstOrDefault(r=>r.DepartureCity==route.DepartureCity);
            if (isExist!=null)
            {
                if (isExist.ArrivalCity == route.ArrivalCity)
                {
                    ModelState.AddModelError("ArrivalCity", "This Route already exist");
                    return View(route);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(route);
                _context.SaveChanges();
                return RedirectToAction("Index", "Route");
            }
            return View(route);
        }

        public IActionResult Update(int? Id)
        {
            if (Id == null || Id == null)
            {
                return NotFound();
            }
            var RouteFromDb = _context.routes.Find(Id);
            if (RouteFromDb == null)
            {
                return NotFound();
            }
            return View(RouteFromDb);
        }

        [HttpPost]
        public IActionResult Update(Route updatedRoute)
        {
            if (ModelState.IsValid)
            {
                _context.routes.Update(updatedRoute);
                _context.SaveChanges();
                return RedirectToAction("Index", "Route");
            }
            return View(updatedRoute);
        }

        public IActionResult Delete(int? Id)
        {
            var existingRoute = _context.routes.Find(Id);
            if (existingRoute == null)
            {
                NotFound();
            }
            _context.Remove(existingRoute);
            _context.SaveChanges();
            return RedirectToAction("Index", "Route");
        }
    }
}
