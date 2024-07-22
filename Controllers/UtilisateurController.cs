using Microsoft.AspNetCore.Mvc;
using SopalS.Models;
using SopalS.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Scripting;

namespace SopalS.Controllers
{
    public class UtilisateurController : Controller
    {
        private readonly DataContext _dataContext;

        

        public UtilisateurController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Utilisateur model)
        {
            if (ModelState.IsValid)
            {
                // Hash the password before saving (use a proper hashing method in a real application)
                model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

                _dataContext.Utilisateur.Add(model);
                await _dataContext.SaveChangesAsync();

                return RedirectToAction("Login", "Utilisateur");
            }

            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string lastName, string password)
        {
            var user = await _dataContext.Utilisateur.SingleOrDefaultAsync(u => u.LastName == lastName);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                // Set authentication cookie or token here
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Nom de famille ou mot de passe incorrect.");
            return View();
        }
    }
}
