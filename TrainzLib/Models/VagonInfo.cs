using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainzLib.Models
{
    [Table("vagon_info")]
    public class VagonInfo
    {
        [Column("id")]
        [DisplayName("Первыичный ключ")]
        public int Id { get; set; }

        [Column("vagon_id")]
        [DisplayName("Номер вагона")]
        public int VagonId { get; set; }

        [Column("vagon_id")]
        [DisplayName("Номер вагона")]
        public virtual Vagon Vagon { get; set; }

        [Column("ord")]
        [DisplayName("Порядок сортировки")]
        public int? OrderNum { get; set; }

        [Column("gruz_type_id")]
        [DisplayName("Код груза")]
        public int GruzTypeId { get; set; }

        [Column("gruz_type_id")]
        [DisplayName("Код груза")]
        public virtual GruzType GruzType { get; set; }

        [Column("way_id")]
        [DisplayName("Номер пути")]
        public int? WayId { get; set; }

        [Column("way_id")]
        [DisplayName("Номер пути")]
        public virtual Way? Way { get; set; }
    }
}
