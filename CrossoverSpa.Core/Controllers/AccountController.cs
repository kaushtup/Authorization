using CrossoverSpa.Core;
using CrossoverSpa.Core.Models;
using CrossoverSpa.Helper;
using CrossoverSpa.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CrossoverSpa.App.Web.Controllers
{
    [Route("/")]
    [Route("Account")]
    [CustomAttribute("Security", "Account ", "This is a account controller.")]
    public class AccountController : Controller
    {
        private readonly IDbHelper _dbHelper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        //readonly Disposable disposable;

        public AccountController(IHttpContextAccessor httpContextAccessor, IDbHelper helper)
        {
            this._dbHelper = helper;

            _httpContextAccessor = httpContextAccessor;

            //_dbhttphelper = httpHelper;
        }
       
        [Route("")]
        [Route("login")]
        [CustomAttribute("", "Login", "This is a login page.")]
        public ActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [Route("login")]
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
           
                
                //if (user.RoleId == 1)
                //{
                //    identity.AddClaim(new Claim(ClaimTypes.Role, "Admins"));
                //}
                //else if (user.RoleId == 2)
                //{
                //    identity.AddClaim(new Claim(ClaimTypes.Role, "Employees"));
                //}

                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = false });



                //if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                //{
                //    var n = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email);

                //    var l = _httpContextAccessor.HttpContext.User.Identity.Name;

                //  //HttpContext.Session.SetString("a", "n");
                //}


                ViewData["UserIdentity"] = HttpContext.User.Identity.Name;
                    return RedirectToAction("Index", "Profile");
                
            }


            return RedirectToAction("Login");
        }

        [Route("Signup")]
        [CustomAttribute("", "SignUp", "This is a signup page.")]
        [HttpGet]
        public async Task<ActionResult> SignUp()
        {
            ViewData["RoleId"] = new SelectList(await _dbHelper.GetRolesAsync(), "Id", "Name");
            return View();
        }

        [Route("Signup")]
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

            return RedirectToAction("Login");
        }

        [Route("Logout")]
        [CustomAttribute("", "LogOut", "This is a Logout function.")]
        public async Task<IActionResult> Logout()
        {
            //_dbhttphelper.Dispose();

            //DisposeClass.RegisterDispose(_dbhttphelper, _dbhttphelper.HttpContext);
          await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

      
       

        protected override void Dispose(bool disposing)
        {
            _dbHelper.Dispose();
            base.Dispose(disposing);
        }
    }

    public static class DisposeClass
    {
        public static T RegisterDispose<T>(this T disposable, HttpContext context) where T : IDisposable
        {
            context.Response.RegisterForDispose(disposable);
            return disposable;
        }
    }
}