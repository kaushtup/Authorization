using CrossoverSpa.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossoverSpa.ViewModel
{
    public class ContactViewModel : BaseViewModel
    {
        [Required]
        public string Address { get; set; }

        [Required]
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Mobile3 { get; set; }

        [Required]
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }

        public string Email1 { get; set; }
        public string Email2 { get; set; }
    }
}
