using System.ComponentModel.DataAnnotations;

namespace CrossoverSpa.Entities
{
    public class User : EntityBase
    {
        [Required]
        [MaxLength(255)]
        //public string Username { get; set; }
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }
        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
