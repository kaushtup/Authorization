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
       


        public async Task<List<RoleFeatureViewModel>> GetRoleFeaturesAsync()
        {
            var list = new List<RoleFeatureViewModel>();

            (await new Repository<RoleFeature>(_context).FindAllAsync()).ToList().ForEach(x => list.Add(x.ToVM()));

            return list;
        }


        public async Task<bool> CreateRoleFeatureListAsync(int roleId, List<int> featureId)
        {
            using (var dbTran = _context.Database.BeginTransaction())
            {
                try
                {
                    //var objRole = new Role
                    //{
                    //    Name = roleName

                    //};

                    //var user = await new Repository<Role>(_context).AddAsync(objRole);

                    foreach (var item in featureId)
                    {
                        //var objFeature = new Feature
                        //{
                        //    Email = item.Email,
                        //};

                        //var feature = await new Repository<Feature>(_context).AddAsync(objFeature);

                        var objRoleFeature = new RoleFeature
                        {
                            RoleId = roleId,
                            FeatureId = item
                        };
                        await new Repository<RoleFeature>(_context).AddAsync(objRoleFeature);
                    }
                    dbTran.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    dbTran.Rollback();
                    return false;
                }
            }

        }

        public async Task<bool> CreateRoleFeatureAsync(int roleId, int featureId)
        {
            try
            {

                var objRoleFeature = new RoleFeature
                {
                    RoleId = roleId,
                    FeatureId = featureId
                };
                await new Repository<RoleFeature>(_context).AddAsync(objRoleFeature);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
           

        }

        public async Task<int> GetRoleFeatureId(int id)
        {
            return (await new Repository<RoleFeature>(_context).FindAsync(x => x.RoleId == id)).FirstOrDefault().Id;
        }
        public async Task<bool> DeleteFeatureFromRoleByIdAsync(int id)
        {
            if (id < 1)
            {
                return false;
            }
            try
            {
                await new Repository<RoleFeature>(_context).RemoveByIdAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
       
        }

        public async Task<bool> DeleteRoleFeatureByIdAsync(int id)
        {
            if (id < 1)
            {
                return false;
            }
            using (var dbTran=_context.Database.BeginTransaction())
            {
                try
                {
                    var roleidForUser = await new Repository<User>(_context).FindAllAsync();
         
                    var roleidForFeature = await new Repository<RoleFeature>(_context).FindAllAsync();
                   
                    foreach (var item in roleidForFeature)
                    {
                        if (item.RoleId == id)
                        {
                           
                            if (item.Id != 0)
                            {
                                await new Repository<RoleFeature>(_context).RemoveByIdAsync(item.Id);
                            }
                        }
                           
                    }
                    foreach (var item in roleidForUser)
                    {
                        if (item.RoleId == id)
                        {
                            if (item.Id != 0)
                            {
                                await new Repository<User>(_context).RemoveByIdAsync(item.Id);
                            }
                        }
                          
                       
                    }
                    
                   
                    await new Repository<Role>(_context).RemoveByIdAsync(id);
                    dbTran.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    dbTran.Rollback();
                    return false;
                }
            }
              


        }

        


    }
}
