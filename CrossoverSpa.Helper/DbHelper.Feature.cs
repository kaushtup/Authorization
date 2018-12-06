using CrossoverSpa.Entities;
using CrossoverSpa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossoverSpa.Helper
{
    public partial class DbHelper 
    {
        public async Task<FeatureViewModel> GetFeatureByIdAsync(int id)
        {
            return (await new Repository<Feature>(_context).FindByIdAsync(id)).ToVM();
        }

        //nested save....it does not requires transaction.
        public bool CreateFeature(string featureName)
        {

            var objFeature = new Feature
            {
                Name = featureName
            };

            try
            {
                new Repository<Feature>(_context).Add(objFeature);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public List<FeatureViewModel> GetFeatures()
        {
            var list = new List<FeatureViewModel>();

            new Repository<Feature>(_context).FindAll().ToList().ForEach(x => list.Add(x.ToVM()));

            return list;
        }

        public async Task<List<FeatureViewModel>> GetFeaturesAsync()
        {
            var list = new List<FeatureViewModel>();

            (await new Repository<Feature>(_context).FindAllAsync()).ToList().ForEach(x => list.Add(x.ToVM()));

            return list;
        }

    }
}
