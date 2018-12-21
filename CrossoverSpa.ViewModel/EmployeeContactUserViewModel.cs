using CrossoverSpa.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossoverSpa.ViewModel
{
    public class EmployeeContactUserViewModel : BaseViewModel
    {
        public EmployeeViewModel Employee { get; set; }
        public int EmployeeId { get; set; }

        public ContactViewModel Contact { get; set; }
        public int ContactId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public int RoleId { get; set; }

    }
}
