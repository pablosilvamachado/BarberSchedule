
using System.ComponentModel.DataAnnotations;

namespace AgendaBlue.Domain.Entities
{
    public class Service
    {
        [Key]
        public long Id { get; set; }

        public string? NameServices { get; set; }
    }
}
