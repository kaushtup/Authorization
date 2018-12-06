using CrossoverSpa.Core.Models;
using CrossoverSpa.Helper;
using CrossoverSpa.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CrossoverSpa.App.Web.Controllers
{
    [CustomAttribute("Sales Department", "Account Controller", "This is a account controller.")]
    public class AccountController : Controller
    {
        private readonly IDbHelper _dbHelper;

        public AccountController(IDbHelper helper)
        {
            this._dbHelper = helper;
           
        }

        [CustomAttribute("", "Login", "This is a login page.")]
        public ActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ErrorMessage = "Enter all the required fields in corrent format.";
                return View(model);
            }

            var user = await _dbHelper.GetUserAsync(model.Email, model.Password);
            if (user == null)
            {
                model.ErrorMessage = "Invalid username or password.";
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Email, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.Email, model.Email));

                if (user.RoleId == 1)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Admins"));
                }
                else if (user.RoleId == 2)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Employees"));
                }

                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = false });
                //return RedirectToAction("Indexnew","Home");
                if (user.RoleId == 1)
                {
                    
                     //HttpContext.Session.Set("UserId", "1");
                    return RedirectToAction("Index", "Home", new { Area = "Admin" });
                    
                }
                else
                {
                    return RedirectToAction("Index", "Profile", new { Area = "Employee" });
                }
            }
            return RedirectToAction("Login");
        }


        [CustomAttribute("", "SignUp", "This is a signup page.")]
        [HttpGet]
        public async Task<ActionResult> SignUp()
        {
            ViewData["RoleId"] = new SelectList(await _dbHelper.GetRolesAsync(), "Id", "Name");
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> SignUp(UserViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                viewmodel.ErrorMessage = "Error Data";
                return View(viewmodel);
            }

            await _dbHelper.CreateUserAsync(viewmodel.Email,
                   viewmodel.Password,
                   viewmodel.RoleId
                   );

            return RedirectToAction("SignUp");
        }




        protected override void Dispose(bool disposing)
        {
            _dbHelper.Dispose();
            base.Dispose(disposing);
        }
    }
}