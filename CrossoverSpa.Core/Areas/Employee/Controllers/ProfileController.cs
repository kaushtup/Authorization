
using CrossoverSpa.Core.Models;
using CrossoverSpa.Helper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace CrossoverSpa.Core.Areas.Employee.Controllers
{

    [CustomAttribute("Sales Department", "Home Controller", "This is a employee home controller.")]
    public class ProfileController : Controller
    {

        [CustomAttribute("", "Employee Site View", "This is a employee home controller.")]
        public ActionResult Index()
        {
            return View();
        }
       
    }
}