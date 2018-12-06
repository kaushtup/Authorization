using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossoverSpa.Entities
{
    public class Employee : EntityBase
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

        public int UserId { get; set; }


        public User User { get; set; }
    

        public User AddedBy { get; set; }


        [Required]
        public int AddedById { get; set; }

        
        public User ModifiedBy { get; set; }
        public int? ModifiedById { get; set; }
    }
}
