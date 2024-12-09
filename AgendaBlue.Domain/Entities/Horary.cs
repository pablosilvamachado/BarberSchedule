


using System.ComponentModel.DataAnnotations;

namespace AgendaBlue.Domain.Entities
{
    public class Horary
    {
        [Key] 
        public long Id { get; set; }
        public string? horary { get; set; }
        public bool? AvailableTime { get; set; }
        public DateTime AvailableDate { get; set; }
    }
}
