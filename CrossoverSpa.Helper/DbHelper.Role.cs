using CrossoverSpa.Entities;
using CrossoverSpa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossoverSpa.Helper
{
    public partial class DbHelper
    {
        public async Task<RoleViewModel> GetRoleByIdAsync(int id)
        {
            return (await new Repository<Role>(_context).FindByIdAsync(id)).ToVM();
        }



        public List<RoleViewModel> GetRoles()
        {
            var list = new List<RoleViewModel>();

            new Repository<Role>(_context).FindAll().ToList().ForEach(x => list.Add(x.ToVM()));

            return list;
        }

        public async Task<List<RoleViewModel>> GetRolesAsync()
        {
            var list = new List<RoleViewModel>();

            (await new Repository<Role>(_context).FindAllAsync()).ToList().ForEach(x => list.Add(x.ToVM()));

            return list;
        }
        public async Task<bool>CreateRoleAsync(string role)
        {

            var objRole = new Role
            {
                Name = role,

            };

            try
            {
                await new Repository<Role>(_context).AddAsync(objRole);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
       



    }
}
