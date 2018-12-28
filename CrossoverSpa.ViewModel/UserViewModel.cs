using System.ComponentModel.DataAnnotations;

namespace CrossoverSpa.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        [Required]
        public string Email { get; set; }

       
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        //[DataType(DataType.Password)]
        //[Compare("Password")]
        //public string ConfirmPassword { get; set; }
        public string RoleName { get; set; }

        public int RoleId { get; set; }

        public RoleViewModel Role { get; set; }
       
    }
}
