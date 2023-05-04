namespace TrainzLib.Models
{
    public class Way
    {
        public int Id { get; set; }
        public int Num { get; set; }
        public string? Description { get; set; }
        public int StationId { get; set; }
        public virtual Station Station { get; set; }
    }
}
