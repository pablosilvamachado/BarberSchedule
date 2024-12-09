using AgendaBlue.Domain.Enums;
namespace AgendaBlue.DTO
{
    public class SchedulingDTO
    {
        public int IdUser { get; set; }
        public DateTime HairCurtDate { get; set; }
        public string DesiredService { get; set; }
        public string Time { get; set; }
        public BarberEnum barberEnum { get; set; }
    }
}
