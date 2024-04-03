using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
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

        public async void signInToken(int id, string name, string role)
        {
            Claim c1 = new Claim(ClaimTypes.NameIdentifier, id.ToString());
            Claim c2 = new Claim(ClaimTypes.Name, name);
            Claim c3 = new Claim(ClaimTypes.Role, role);

            ClaimsIdentity ci = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            ci.AddClaim(c1);
            ci.AddClaim(c2);
            ci.AddClaim(c3);

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
                    signInToken(0, "admin", "admin");
                    return RedirectToAction("index", "admin");
                }

                Student std = stdRepo.GetStudentByEmailAndPassword(loggedInUser.Email, loggedInUser.Password);
                if (std != null)
                {
                    signInToken(std.Id, std.Name, "student");
                    return RedirectToAction("index", "student", new { id = std.Id });
                }

                Employee emp = empRepo.GetEmployeeByEmailAndPassword(loggedInUser.Email, loggedInUser.Password);
                if (emp != null)
                {
                    signInToken(emp.Id, emp.Name, "employee");
                    return RedirectToAction("index", "employee", new { id = emp.Id });
                }

                Instructor inst = instRepo.GetInstructorByEmailAndPassword(loggedInUser.Email, loggedInUser.Password);


                if (inst != null)
                {
                    //check if the instructor is a supervisor then set his rule to a supervisor
                    Track track = instRepo.GetSuperVisorTrack(inst.Id);
                    if (track != null)
                    {
                        signInToken(inst.Id, inst.Name, "supervisor");
                    }
                    else
                    {
                        signInToken(inst.Id, inst.Name, "instructor");
                    }
                    return RedirectToAction("index", "instructor", new { id = inst.Id });
                }

                ModelState.AddModelError("", "Invalid Email or password");
                return View(loggedInUser);
            }
            else
                return View(loggedInUser);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("login");
        }

        [HttpGet]
        public IActionResult Signup()
        {

            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Signup(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }
            stdRepo.RegisterStudent(student);
            signInToken(student.Id, student.Name, "student");
            return RedirectToAction("index", "student", new { id = student.Id });
        }
    }
}
