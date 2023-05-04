namespace TrainzLib.Models
{
    public class Vagon
    {
        public int NomVag { get; set; }
        public int VagTypeId { get; set; }
        public virtual VagonType VagType { get; set; }
        public float VagWeight { get; set; }
        public float VagСapacity { get; set; }

    }
}
