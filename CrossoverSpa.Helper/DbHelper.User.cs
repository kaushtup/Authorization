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
        public UserViewModel GetUserById(int id)
        {
            return new Repository<User>(_context).FindById(id).ToVM();
        }

        public async Task<UserViewModel> GetUserByIdAsync(int id)
        {
            return (await new Repository<User>(_context).FindByIdAsync(id)).ToVM();
        }

        public List<UserViewModel> GetUsers()
        {
            var list = new List<UserViewModel>();

            new Repository<User>(_context).FindAll().ToList().ForEach(x => list.Add(x.ToVM()));

            return list;
        }

        public async Task<List<UserViewModel>> GetUsersAsync()
        {
            var list = new List<UserViewModel>();

            (await new Repository<User>(_context).FindAllAsync()).ToList().ForEach(x => list.Add(x.ToVM()));

            return list;
        }

        public async Task<int> GetUserId(int id)
        {
            return (await new Repository<User>(_context).FindAsync(x => x.Id == id)).FirstOrDefault().Id;
        }

        public async Task<bool> CreateUserAsync(string email, string password, int roleid)
        {
            var objUser = new User
            {
                Email = email,
                Password = password,
                RoleId = roleid
            };

            try
            {
                await new Repository<User>(_context).AddAsync(objUser);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

}
