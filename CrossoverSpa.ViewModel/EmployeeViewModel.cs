using CrossoverSpa.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CrossoverSpa.ViewModel
{
    public class EmployeeViewModel : BaseViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Designation { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public DateTime AddedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int AddedById { get; set; }

        public int? ModifiedById { get; set; }

        public int UserId { get; set; }


        public UserViewModel User { get; set; }
      

        public UserViewModel AddedBy { get; set; }

        public UserViewModel ModifiedBy { get; set; }
       
     }
}
