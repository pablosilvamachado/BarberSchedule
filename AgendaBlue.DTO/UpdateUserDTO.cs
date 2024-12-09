
using System.ComponentModel.DataAnnotations;


namespace AgendaBlue.DTO
{
    public class UpdateUserDTO
    {
        [Required]
        public int IdUser { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Phone { get; set; }
        public bool IsBarberAdmin { get; set; }
    }
}
