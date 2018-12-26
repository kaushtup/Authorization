using CrossoverSpa.ViewModel;
using CrossoverSpa.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossoverSpa.Helper
{
    public interface IDbHelper: IDisposable
    {
        Task<List<UserViewModel>> GetUsersAsync();

        Task<UserViewModel> GetUserAsync(string email, string password);
        Task<bool> CreateRoleFeatureAsync(int roleId,int featureId);
        Task<bool> CreateUserAsync(string email,string password, int roleid);
        Task<List<RoleViewModel>> GetRolesAsync();
        //Task<bool> IsAddOrDeleteAsync(int id);
        //bool IsAddOrDelete(int id);
        Task<List<RoleFeatureViewModel>> GetRoleFeaturesAsync();
        List<FeatureViewModel> GetFeatures();
        bool CreateFeature(string featureName, string RouteUrl);
        List<RoleViewModel> GetRoles();

        Task<int> GetRoleFeatureId(int id);

        Task<RoleViewModel> GetRoleByIdAsync(int id);

        Task<FeatureViewModel> GetFeatureByIdAsync(int id);

        Task<List<FeatureViewModel>> GetFeaturesAsync();

        Task<bool> CreateRoleFeatureListAsync(int roleId, List<int> featureId);

        Task<bool> DeleteRoleFeatureByIdAsync(int id);
        List<UserViewModel> GetUsers();
        Task<bool> CreateRoleAsync(string role);




    }
}
