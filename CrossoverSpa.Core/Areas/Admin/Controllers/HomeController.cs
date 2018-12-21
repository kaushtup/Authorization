using CrossoverSpa.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace CrossoverSpa.Core.Areas.Admin.Controllers
{

    [CustomAttribute("Human Resource", "Home Controller", "This is a admin home controller.")]
    public class HomeController : Controller
    {
        [CustomAttribute("", "Admin Site View", "This is a admin site.")]
        public ActionResult Index()
        {
            return View();
        }
    }
}   