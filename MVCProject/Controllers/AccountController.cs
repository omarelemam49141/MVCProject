using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Repos;
using MVCProject.ViewModels;
using System.Security.Claims;

namespace MVCProject.Controllers
{
    public class AccountController : Controller
    {
        private IInstructorRepo instRepo;
        private IEmployeeRepo empRepo;
        private IStudentRepo stdRepo;

        public AccountController(IInstructorRepo _instRepo, IStudentRepo _stdRepo, IEmployeeRepo _empRepo) 
        {
            instRepo = _instRepo;
            empRepo = _empRepo;
            stdRepo = _stdRepo;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public async void signInToken(string email, string role)
        {
            Claim c1 = new Claim(ClaimTypes.Email, email);
            Claim c2 = new Claim(ClaimTypes.Role, role);

            ClaimsIdentity ci = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            ci.AddClaim(c1);
            ci.AddClaim(c2);

            ClaimsPrincipal cp = new ClaimsPrincipal(ci);

            await HttpContext.SignInAsync(cp);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Login(LoggedInUser loggedInUser)
        {
            if (ModelState.IsValid)
            {
                if (loggedInUser.Email.ToLower() == "admin@gmail.com" && loggedInUser.Password == "123456") 
                {
                    signInToken(loggedInUser.Email, "admin");
                    return RedirectToAction("index", "admin");
                }
                else if(stdRepo.GetStudentByEmailAndPassword(loggedInUser.Email, loggedInUser.Password)!=null)
                {
                    signInToken(loggedInUser.Email, "student");
                    return RedirectToAction("index", "student");
                }
                else if(empRepo.GetEmployeeByEmailAndPassword(loggedInUser.Email, loggedInUser.Password) != null)
                {
                    signInToken(loggedInUser.Email, "employee");
                    return RedirectToAction("index", "employee");
                }
                else if (instRepo.GetInstructorByEmailAndPassword(loggedInUser.Email, loggedInUser.Password) != null)
                {
                    signInToken(loggedInUser.Email, "instructor");
                    return RedirectToAction("index", "instructor");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Email or password");
                    return View(loggedInUser);
                }
            }
            else
                return View(loggedInUser);
            return View();
        }
    }
}
