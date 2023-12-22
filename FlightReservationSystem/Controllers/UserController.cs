using FlightReservationSystem.Data;
using FlightReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User newUser)
        {
            var existingUser = _context.users.FirstOrDefault(u => u.Email == newUser.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "This email is already registered.");
                return View("SignUp", newUser);
            }
            if (ModelState.IsValid)
            {
                _context.Add(newUser);
                _context.SaveChanges();
                return RedirectToAction("SignIn", "User");
            }
            return View(newUser);
        }

        public IActionResult Index()
        {
            IEnumerable<User> UserList = _context.users;
            return View(UserList);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(User user)
        {
            var existingUser = _context.users.FirstOrDefault(u => u.Email == user.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "This email is already registered.");
                return View("Create", user);
            }
            if (ModelState.IsValid)
            {
                _context.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index", "User");
            }
            return View(user);
        }


        public IActionResult Update(int? Id)
        {
            if (Id == null || Id == null)
            {
                return NotFound();
            }
            var UserFromDb = _context.users.Find(Id);
            if (UserFromDb == null)
            {
                return NotFound();
            }
            return View(UserFromDb);
        }

        [HttpPost]
        public IActionResult Update(User updatedUser)
        {
            // Kullanıcının mevcut bilgilerini al
            var existingUser = _context.users.FirstOrDefault(u => u.Id == updatedUser.Id);

            if (existingUser == null)
            {
                // Kullanıcı bulunamadı
                return NotFound();
            }

            // Eğer e-posta adresi değiştiyse kontrol et
            if (existingUser.Email != updatedUser.Email)
            {
                // Yeni e-posta adresi başka bir kullanıcıya ait mi kontrol et
                var isEmailTaken = _context.users.Any(u => u.Email == updatedUser.Email);

                if (isEmailTaken)
                {
                    // Yeni e-posta adresi başka bir kullanıcıya aitse güncelleme işlemini reddet
                    ModelState.AddModelError("Email", "This email is already taken by another user.");
                    return View("Update", updatedUser);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Update(updatedUser);
                _context.SaveChanges();
                return RedirectToAction("Index", "User");
            }
            return View(updatedUser);

        }
        public IActionResult Delete(int? Id)
        {
            var existingUser= _context.users.Find(Id);
            if (existingUser == null)
            {
                NotFound();
            }
            _context.Remove(existingUser);
            _context.SaveChanges();
            return RedirectToAction("Index","User");
        }


    } 
}
