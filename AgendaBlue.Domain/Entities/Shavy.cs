


using System.ComponentModel.DataAnnotations;

namespace AgendaBlue.Domain.Entities
{
    public class Shavy
    {
        [Key]
        public long Id { get; set; }

        public string? BarberName { get; set; }
    }
}
