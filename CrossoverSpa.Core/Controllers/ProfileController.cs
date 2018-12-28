
using CrossoverSpa.Core.Models;
using CrossoverSpa.Core.Services;
using CrossoverSpa.Helper;
using CrossoverSpa.ViewModel;
using CrossoverSpa.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace CrossoverSpa.Core.Controllers
{
    [Route("Profile")]
    [CustomAttribute("User Department", "Profile ", "This is a employee home controller.")]
    public class ProfileController : Controller
    {
        private readonly IDbHelper _dbHelper;
        private readonly IMvcControllerDiscovery _mvcController;
        private readonly List<MvcControllerInfo> _mvcControllerInfo;
        private readonly IFeatureDiscovery _featureDiscovery;
        public ProfileController(IDbHelper dbHelper, IMvcControllerDiscovery mvcController,List<MvcControllerInfo> mvcControllerInfo,IFeatureDiscovery featureDiscovery)
        {
            _dbHelper = dbHelper;
            _mvcController = mvcController;
            _mvcControllerInfo = mvcControllerInfo;
            _featureDiscovery = featureDiscovery;
        }
       

        [Route("Index")]
        [CustomAttribute("", "UserFeature", "This is a employee home controller.")]
        public  IActionResult Index()
        {
           

              return View();
        }
       
    }
}