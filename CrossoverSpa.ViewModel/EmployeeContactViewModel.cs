using CrossoverSpa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossoverSpa.ViewModel
{
    public class EmployeeContactViewModel : BaseViewModel
    {
        public EmployeeViewModel Employee { get; set; }
        public int EmployeeId { get; set; }

        public ContactViewModel Contact { get; set; }
        public int ContactId { get; set; }
    }
}
