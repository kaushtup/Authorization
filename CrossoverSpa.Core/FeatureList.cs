using CrossoverSpa.Helper;
using CrossoverSpa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossoverSpa.Core
{
    public class FeatureList
    {
        private readonly DbHelper _dbHelper;

        public FeatureList()
        {
        }

        
        public List<FeatureViewModel> Features(int roleId)
        {
            var roleFeatures = _dbHelper.GetRoleFeaturesAsync().Result;
            List<int> list = new List<int>();
            foreach (var item in roleFeatures)
            {
                if (item.RoleId == roleId)
                {
                    list.Add(item.FeatureId);
                }
            }

            var featureList = new List<FeatureViewModel>();
            foreach (var item in list)
            {
                var feature = _dbHelper.GetFeatureByIdAsync(item).Result;
                featureList.Add(feature);
            }
            return featureList;
        }
    }
    
}
