using CrossoverSpa.Core.Models;
using CrossoverSpa.Core.Services;
using CrossoverSpa.Helper;
using CrossoverSpa.ViewModel;
using CrossoverSpa.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossoverSpa.Core.Controllers
{
   [CustomAttribute("Human Resource", "Role Controller", "This is a home controller.")]
    public class RoleController : Controller
    {
        private readonly IDbHelper _helper;

        private readonly IMvcControllerDiscovery _mvcControllerDiscovery;

        public RoleController(IMvcControllerDiscovery mvcControllerDiscovery, IDbHelper helper)
        {
            _mvcControllerDiscovery = mvcControllerDiscovery;
            this._helper = helper;
        }

        // GET: Role/Create

        [CustomAttribute("", "Create Role", "This action is used to create the roles.")]
        public async Task<IActionResult> Create()
        {
            ViewData["Modules"] = _mvcControllerDiscovery.GetControllers();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Modules"] = _mvcControllerDiscovery.GetControllers();
                return View(viewModel);
            }

          if (viewModel.SelectedControllers != null && viewModel.SelectedControllers.Any())
            {
                var featuresList = new List<FeatureViewModel>();

                var features = await _helper.GetFeaturesAsync();

                foreach (var controller in viewModel.SelectedControllers)
                {
                    foreach (var action in controller.Actions)
                    {
                        var feature = new FeatureViewModel();
                        feature.Name = action.Name;

                        featuresList.Add(feature);
                    }
                }

                List<int> featureId = new List<int>();
                foreach (var item in features)
                {
                    foreach (var newitem in featuresList)
                    {
                        if (item.Name == newitem.Name)
                        {
                            featureId.Add(item.Id);
                        }
                    }
                }

                await _helper.CreateRoleFeatureListAsync(viewModel.Name, featureId);
            }
            return RedirectToAction("Create");
        }

        [CustomAttribute("", "Show Roles", "This action is used to show Roles.")]
        public async Task<IActionResult> Show()
        {
            var role = await _helper.GetRolesAsync();
            return View(role);
        }

        [CustomAttribute("", "Edit Role", "This action is used to edit the roles.")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var rolefeature = await _helper.GetRoleFeaturesAsync();

            List<int> list = new List<int>();

            foreach (var item in rolefeature)
            {
                if(item.RoleId == id)
                {
                    
                    list.Add(item.FeatureId);
                }
                  
            }
            var featureIdList = new List<FeatureViewModel>();

            var rolefeatureIdList = new RoleFeatureListViewModel();

            var role = await _helper.GetRoleByIdAsync(id);

            foreach (var item in list)
            {
                var feature = await _helper.GetFeatureByIdAsync(item);

                featureIdList.Add(feature);

                //rolefeatureIdList.Add(new RoleFeatureViewModel
                //  {
                //  Feature = feature,
                //  Role = role
                //  });
            }
            rolefeatureIdList.RoleId = id;
            rolefeatureIdList.Feature = featureIdList;

            ViewData["Modules"] = _mvcControllerDiscovery.GetControllers();
            //return View(featureIdList);
            return View(rolefeatureIdList);
        }

  
        [HttpGet]
        public async Task<IActionResult> AddRole(string name, int roleId)
        {
            var feature = await _helper.GetFeaturesAsync();
            int featureId=0;
            
            foreach (var item in feature)
            {
                if(item.Name.Equals(name))
                {
                    featureId = item.Id;
                }
            }

        
            await _helper.CreateRoleFeatureAsync(
                 roleId,
                 featureId
                );

            return RedirectToAction("Edit",new { id=roleId});
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRole(string name, int roleId)
        {


            var feature = await _helper.GetFeaturesAsync();
            int featureId = 0;

            foreach (var item in feature)
            {
                if (item.Name.Equals(name))
                {
                    featureId = item.Id;
                }
            }


            var rolefeature = await _helper.GetRoleFeaturesAsync();
            int rolefeatureId = 0;
            foreach (var item in rolefeature)
            {
                if (item.RoleId == roleId && item.FeatureId == featureId)
                {
                    rolefeatureId = item.Id;
                }
            }

            await _helper.DeleteRoleFeatureByIdAsync(rolefeatureId);

            return RedirectToAction("Edit", new { id = roleId });
        }

    }
}