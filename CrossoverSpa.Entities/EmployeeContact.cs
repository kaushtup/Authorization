using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossoverSpa.Entities
{
    public class EmployeeContact : EntityBase
    {
        
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }

        
        public Contact Contact{ get; set; }
        public int ContactId { get; set; }

      
    }
}
