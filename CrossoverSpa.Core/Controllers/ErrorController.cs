using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossoverSpa.Core.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CrossoverSpa.Core.Controllers
{
    [Route("Error")]
    [CustomAttribute("Errors", "Account Controller", "This is a account controller.")]
    public class ErrorController : Controller
    {
        // GET: /<controller>/
        [Route("Index")]
        [CustomAttribute("", "Index Controller", "This is a account controller.")]
        public IActionResult Index()
        {
            return View();
        }
        [CustomAttribute("", "Unauthorizes", "This is a account controller.")]
        [Route("UnAuthorized")]
        public IActionResult UnAuthorized()
        {
            return View();
        }
    }
}
