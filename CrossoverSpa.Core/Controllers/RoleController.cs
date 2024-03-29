﻿using CrossoverSpa.Core.Models;
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
    [Route("Role")]
   [CustomAttribute("Human Resource", "Role Controller", "This is a home controller.")]
    public class RoleController : Controller
    {
        private readonly IDbHelper _helper;

        private readonly IMvcControllerDiscovery _mvcControllerDiscovery;
        private readonly List<MvcControllerInfo> _mvcControllerInfos;

        public RoleController(IMvcControllerDiscovery mvcControllerDiscovery, IDbHelper helper,List<MvcControllerInfo> mvcControllerInfos)
        {
            _mvcControllerDiscovery = mvcControllerDiscovery;
            this._helper = helper;
            _mvcControllerInfos = mvcControllerInfos;
        }

        // GET: Role/Create

        [Route("CreateRole")]
        [CustomAttribute("", "Create Role", "This action is used to create the roles.")]
        public  IActionResult Create()
        {

            ViewData["Modules"] = _mvcControllerDiscovery.GetControllers();

            return View();
        }

       [Route("CreateRole")]
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Modules"] = _mvcControllerDiscovery.GetControllers();
                return View(viewModel);
            }
            await _helper.CreateRoleAsync(viewModel.Name);
            return RedirectToAction("Show");
        }

        [Route("Show")]
        [CustomAttribute("", "Show Roles", "This action is used to show Roles.")]
        public async Task<IActionResult> Show()
        {
            List<RoleViewModel> roleToView = new List<RoleViewModel>();
            var role = await _helper.GetRolesAsync();
            var roleFeature = await _helper.GetRoleFeaturesAsync();
            foreach (var roles in role)
            {
                bool status = false;
                RoleViewModel roleTemp = new RoleViewModel();
                foreach (var item in roleFeature)
                {
                    if (item.RoleId == roles.Id)
                    { 
                        status = true;
                    }                            
                }
                if (status == true)
                {                  
                    roleTemp.Id = roles.Id;
                    roleTemp.Name = roles.Name;
                    roleTemp.OldRole = true;
                    roleToView.Add(roleTemp);
                }
                else
                {                 
                    roleTemp.Id = roles.Id;
                    roleTemp.Name = roles.Name;
                    roleTemp.OldRole = false;
                    roleToView.Add(roleTemp);
                }
            }          
            return View(roleToView);
        }

        [Route("EditRole")]
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

               
            }           
            rolefeatureIdList.Role = role;
            rolefeatureIdList.Feature = featureIdList;
            ViewData["Modules"] = _mvcControllerDiscovery.GetControllers();
          
            return View(rolefeatureIdList);
        }

        [Route("AddFeatures")]
        [CustomAttribute("", "Add Features", "This action is used to add features to the roles.")]
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

        [Route("DeleteRole")]
        [CustomAttribute("", "Delete Features", "This action is used to delete features of the roles.")]
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

            await _helper.DeleteFeatureFromRoleByIdAsync(rolefeatureId);

            return RedirectToAction("Edit", new { id = roleId });
        }

        [Route("AssignRole")]
        [CustomAttribute("", "Assign features", "This action is used to assign features the roles.")]
        public async Task<IActionResult> AssignFeatures(int id)
        {
            ViewData["Modules"] = _mvcControllerDiscovery.GetControllers();
            var rolesFromDb = await _helper.GetRoleByIdAsync(id);

            return View(rolesFromDb);
        }
        [Route("AssignRole")]
        [CustomAttribute("", "", "This action is used to edit the roles.")]
        [HttpPost]
        public async Task<IActionResult> AssignFeatures(RoleViewModel viewModel)
        {
            if (viewModel.SelectedControllers != null && viewModel.SelectedControllers.Any())
            {
                var featuresList = new List<FeatureViewModel>();

                var features = await _helper.GetFeaturesAsync();

                foreach (var controller in viewModel.SelectedControllers)
                {
                    foreach (var action in controller.Actions)
                    {
                        var feature = new FeatureViewModel();
                        feature.Name = action.DisplayName;

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

                await _helper.CreateRoleFeatureListAsync(viewModel.Id, featureId);
            }
            return RedirectToAction("Show");
        }
        [Route("Delete")]
        [CustomAttribute("", "Delete", "This action is used to edit the roles.")]
        public async Task<IActionResult> Delete(int id)
        {
            
            var roleDelete = await _helper.DeleteRoleFeatureByIdAsync(id);
            if (!roleDelete)
                return RedirectToAction("show", new { errorMessage = "Delete Unsuccessful" });
            return RedirectToAction("show", new { errorMessage = "Delete successful" });

        }
    }
}