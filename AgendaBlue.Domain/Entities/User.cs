
using System.ComponentModel.DataAnnotations;

namespace AgendaBlue.Domain.Entities
{
    public class User
    {
        [Key]
        public long IdUser { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Email  { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }
        public bool BarberAdmin { get; set; }
    }
}
