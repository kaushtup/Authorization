using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CrossoverSpa.Core.Models;
using CrossoverSpa.Helper;
using System.Threading.Tasks;
using System.ComponentModel;
using CrossoverSpa.ViewModels;
using System.Collections.Generic;

namespace CrossoverSpa.Core.Controllers
{
    [Route("dashboard")]    
    [CustomAttribute("User Department", "Home ","This is a home controller.")]
    public class HomeController : Controller 
    {
        private readonly IDbHelper _helper;

        public HomeController(IDbHelper helper)
        {
            this._helper = helper;
        }

        [Route("Index")]
        [CustomAttribute("","Show Users","This action is used to show users.")]
        public async Task<IActionResult> Indexnew()
        {
            var user = await _helper.GetUsersAsync();
            var roles = await _helper.GetRolesAsync();
           var userToView = new List<UserViewModel>();
            foreach (var item in user)
            {
                var users = new UserViewModel();
                foreach (var item2 in roles)
                {
                    if (item2.Id == item.RoleId)
                    {
                        users.Id = item.Id;
                        users.Email = item.Email;
                        users.RoleId = item.RoleId;
                        users.RoleName = item2.Name;
                    }
                       
                }
                userToView.Add(users);

            }
            
            return View(userToView);
        }

        //[Authorize(Policy = "Admins")]
        [Route("aboutus")]
        [CustomAttribute("", "About Us", "This contains the description of the site.")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        [Route("Contactus")]
        [CustomAttribute("", "Contacts", "This contains contact informations.")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [Route("Privacy")]
        [CustomAttribute("", "Privacy", "This is a privacy page.")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [CustomAttribute("", "Error", "Any error occured is displayed here.")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        protected override void Dispose(bool disposing)
        {
            _helper.Dispose();
            base.Dispose(disposing);
        }
    }
}
