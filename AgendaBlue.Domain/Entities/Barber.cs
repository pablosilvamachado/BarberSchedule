
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaBlue.Domain.Entities
{
    public class Barber
    {
        [Key]
        public long IdBarber { get; set; }
        public long IdSchedulling { get; set; }
        [Required]
        public string Name { get; set; }

        [ForeignKey("IdSchedulling")]
        public virtual Scheduling scheduling { get; set; }
    }
}
