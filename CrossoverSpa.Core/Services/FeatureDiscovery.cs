
using System.Collections.Generic;
using CrossoverSpa.Helper;
using CrossoverSpa.ViewModel;
using CrossoverSpa.ViewModels;
using Microsoft.AspNetCore.Http;


namespace CrossoverSpa.Core.Services
{
    public class FeatureDiscovery:IFeatureDiscovery
    {
        private readonly IDbHelper _dbHelper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMvcControllerDiscovery _mvcController;
        private readonly List<MvcControllerInfo> _mvcControllerInfo;
       
        public FeatureDiscovery(IDbHelper helper,IMvcControllerDiscovery mvcController,List<MvcControllerInfo> mvcControllerInfo, IHttpContextAccessor httpContext)
        { 
        _dbHelper = helper;
            _httpContext = httpContext;
            _mvcController = mvcController;
            _mvcControllerInfo = mvcControllerInfo;
        }

        public Dictionary<string,List<string>> GetFeatures()
        {
            var userFromDb =  _dbHelper.GetUsersAsync().Result;
            int roleId = 0;
            var userName = _httpContext.HttpContext.User.Identity.Name;
            foreach (var item in userFromDb)
            {
                if (item.Email.ToLower() == userName)
                {
                    roleId = item.RoleId;
                }

            }
            var rolefeature =  _dbHelper.GetRoleFeaturesAsync().Result;

            List<int> list = new List<int>();

            foreach (var item in rolefeature)
            {
                if (item.RoleId == roleId)
                {

                    list.Add(item.FeatureId);
                }

            }
            var featureIdList = new List<FeatureViewModel>();
            foreach (var item in list)
            {
                var feature =  _dbHelper.GetFeatureByIdAsync(item).Result;
                featureIdList.Add(feature);
            }
            var featuresFromDb = _dbHelper.GetFeaturesAsync().Result;
            Dictionary<string, List<string>> featureDictionary = new Dictionary<string, List<string>>();
            foreach (var itemfromDb in featuresFromDb)
            {
                foreach (var itemFromFeature in featureIdList)
                {

                  if (itemfromDb.Name == itemFromFeature.Name)
                    {
                        if (itemFromFeature.RouteUrl != "")
                        {
                            var stringToSplit = itemFromFeature.RouteUrl;
                            string[] words = stringToSplit.Split('/');
                            if (words[1] != null)
                            {
                                if (!featureDictionary.ContainsKey(words[0]))
                                    featureDictionary.Add(words[0], new List<string>());

                                featureDictionary[words[0]].Add(words[1]);
                            }
                            
                        }
                        
                        
                       

                    }
                   
                }
            }
            return featureDictionary;
        }
        public string IsUserLoggedIn()
        {

            return _httpContext.HttpContext.User.Identity.Name;
        }
    }
}
