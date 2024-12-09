using AgendaBlue.Domain.Enums;
using System.ComponentModel.DataAnnotations;


namespace AgendaBlue.DTO
{
    public class UpdateSchedulingDTO
    {
        [Required] 
        public long IdUser { get; set; }
        [Required]
        public long IdScheduling { get; set; }
     
        public DateTime HairCurtDate { get; set; }
      
        public string DesiredService { get; set; }
      
        public string Time { get; set; }
      public BarberEnum barberEnum { get; set; }
    }
}
