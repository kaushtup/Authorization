using CrossoverSpa.Entities;
using CrossoverSpa.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace CrossoverSpa.Helper
{
    public partial class DbHelper
    {
        public async Task<bool> IsUsernameExistsAsync(string email)
        {
            return (await new Repository<User>(_context).FindAsync(x => x.Email == email)).FirstOrDefault() != null;
        }

        public async Task<UserViewModel> GetUserAsync(string email, string password)
        {
            return (await new Repository<User>(_context).FindAsync(x => x.Email == email && x.Password == password)).FirstOrDefault().ToVM();
        }
    }
}
