using System.ComponentModel.DataAnnotations;

namespace CrossoverSpa.Entities
{
    public class Role : EntityBase
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
