


using AgendaBlue.Domain.Entities;

namespace AgendaBlue.DTO
{
    public class ListResultAllSchedulingDTO
    {
        public List<Scheduling> schedulings { get; set; }
        public int Count { get; set; }
    }
}
