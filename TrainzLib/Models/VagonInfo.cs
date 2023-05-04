namespace TrainzLib.Models
{
    public class VagonInfo
    {
        public int Id { get; set; }
        public int VagonId { get; set; }
        public virtual Vagon Vagon { get; set; }

        public int? OrderNum { get; set; }
        public int GruzTypeId { get; set; }
        public virtual GruzType GruzType { get; set; }

        public int? WayId { get; set; }
        public virtual Way? Way { get; set; }
    }
}
