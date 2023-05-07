using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainzLib.Models
{
    [Table("vagons")]
    public class Vagon
    {
        [Key]
        [Column("nom_vag")]
        [DisplayName("Номер вагона")]
        public int NomVag { get; set; }

        [Column("vag_type")]
        [DisplayName("Тип вагона")]
        public int VagTypeId { get; set; }

        [Column("vag_type")]
        [DisplayName("Тип вагона")]
        public virtual VagonType VagType { get; set; }

        [Column("weight")]
        [DisplayName("Вес тары")]
        public float VagWeight { get; set; }

        [Column("capacity")]
        [DisplayName("Грузоподъемность")]
        public float VagСapacity { get; set; }

    }
}
