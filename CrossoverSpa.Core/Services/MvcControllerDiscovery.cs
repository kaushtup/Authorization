using CrossoverSpa.Core.Models;
using CrossoverSpa.Helper;
using CrossoverSpa.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CrossoverSpa.Core.Services
{
    public class MvcControllerDiscovery : IMvcControllerDiscovery
    {
        private List<MvcModuleInfo> _mvcModules;

      

        private List<MvcControllerInfo> _mvcControllers;
        private readonly IDbHelper _dbHelper;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public MvcControllerDiscovery(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider, IDbHelper _dbHelper)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
            this._dbHelper = _dbHelper;
        }

        public IEnumerable<MvcModuleInfo> GetControllers()
        {
            


            if (_mvcModules != null)
                return _mvcModules;

           _mvcControllers = new List<MvcControllerInfo>();
            _mvcModules = new List<MvcModuleInfo>();


            var features = _dbHelper.GetFeatures();

            var items = _actionDescriptorCollectionProvider
                .ActionDescriptors.Items
                .Where(descriptor => descriptor.GetType() == typeof(ControllerActionDescriptor))
                .Select(descriptor => (ControllerActionDescriptor)descriptor)
                .GroupBy(descriptor => descriptor.ControllerTypeInfo.FullName)
                .ToList();

       

            foreach (var actionDescriptors in items)
            {
                if (!actionDescriptors.Any())
                    continue;

                var actionDescriptor = actionDescriptors.First();
                var controllerTypeInfo = actionDescriptor.ControllerTypeInfo;

                var currentModule = new MvcModuleInfo
                {
                    Name = controllerTypeInfo.GetCustomAttribute<CustomAttribute>()?.ModuleName,
                    DisplayName = controllerTypeInfo.GetCustomAttribute<CustomAttribute>()?.ModuleName,
                };

                var currentController = new MvcControllerInfo
                {
                    AreaName = controllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue,
                    DisplayName =
                      controllerTypeInfo.GetCustomAttribute<CustomAttribute>()?.DisplayName,
                    Name = actionDescriptor.ControllerName,
                    Description = controllerTypeInfo.GetCustomAttribute<CustomAttribute>()?.Description,
                    ModuleName = controllerTypeInfo.GetCustomAttribute<CustomAttribute>()?.ModuleName,
                };

                

                var actions = new List<MvcActionInfo>();
                foreach (var descriptor in actionDescriptors.GroupBy
                                            (a => a.ActionName).Select(g => g.First()))
                {
                    var methodInfo = descriptor.MethodInfo;
                    actions.Add(new MvcActionInfo
                    {
                        ControllerId = currentController.Id,
                        Name = descriptor.ActionName, 
                        DisplayName =
                             methodInfo.GetCustomAttribute<CustomAttribute>()?.DisplayName,
                        Description = methodInfo.GetCustomAttribute<CustomAttribute>()?.Description
                    });

                    int _count = 0;
                    if (features.Count() != 0)
                    {
                        foreach (var item in features)
                        {
                            if (item.Name == methodInfo.GetCustomAttribute<CustomAttribute>()?.DisplayName)
                            {
                                _count = 1;
                            }
                        }

                        if (_count != 1)
                        {
                            _dbHelper.CreateFeature(methodInfo.GetCustomAttribute<CustomAttribute>()?.DisplayName);
                        }
                    }
                    else
                    {
                        _dbHelper.CreateFeature(methodInfo.GetCustomAttribute<CustomAttribute>()?.DisplayName);
                    }
                }
               
                currentController.Actions = actions;
                _mvcControllers.Add(currentController);
                

                var controller = new List<MvcControllerInfo>();

                controller.Add(new MvcControllerInfo
                {
                    
                    AreaName = currentController.AreaName,
                    DisplayName = currentController.DisplayName,
                    Name = currentController.Name,
                    Description = currentController.Description,
                    ModuleName = currentController.ModuleName,
                    Actions = currentController.Actions
                });

                int count = 0;
                if(_mvcModules.Count()!=0)
                {
                    foreach(var item in _mvcModules)
                    {
                        if(item.Name == currentModule.Name)
                        {
                            item.Controllers.AddRange(controller);
                            count = 1;
                        }
                    }
                    if (count != 1)
                    {
                        currentModule.Controllers = controller;
                        _mvcModules.Add(currentModule);
                    }
                }

               
                else
                {
                    currentModule.Controllers = controller;
                    _mvcModules.Add(currentModule);
                }
                
            }


            


            return _mvcModules;
        }
    }
}
