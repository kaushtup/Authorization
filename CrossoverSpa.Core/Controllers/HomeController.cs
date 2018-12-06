using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CrossoverSpa.Core.Models;
using CrossoverSpa.Helper;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CrossoverSpa.Core.Controllers
{
    //[Route("dashboard")]
    [CustomAttribute("Human Resource","Home Controller","This is a home controller.")]
    public class HomeController : Controller 
    {
        private readonly IDbHelper _helper;

        public HomeController(IDbHelper helper)
        {
            this._helper = helper;
        }

        [CustomAttribute("","Show Users","This action is used to show users.")]
        public async Task<IActionResult> Indexnew()
        {
            var user = await _helper.GetUsersAsync();
            return View(user);
        }

        //[Authorize(Policy = "Admins")]
        //[Route("aboutus")]
        [CustomAttribute("", "About Us", "This contains the description of the site.")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }
        
        //[Authorize(Policy = "Employees")]
        [CustomAttribute("", "Contacts", "This contains contact informations.")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [CustomAttribute("", "Privacy", "This is a privacy page.")]
        public IActionResult Privacy()
        {
            return View();
        }

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
