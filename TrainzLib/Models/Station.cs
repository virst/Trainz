namespace TrainzLib.Models
{
    public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<Way> Ways { get; set; } = new List<Way>();
    }
}
