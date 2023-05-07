using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainzLib.Models
{
    [Table("ways")]
    public class Way
    {
        [Column("id")]
        [DisplayName("Первыичный ключ")]
        public int Id { get; set; }
        [Column("num")]
        [DisplayName("Номер пути")]
        public int Num { get; set; }

        [Column("descr")]
        [DisplayName("Текстовое описание")]
        public string? Description { get; set; }

        [Column("station_id")]
        [DisplayName("Код станции")]
        public int StationId { get; set; }

        [Column("station_id")]
        [DisplayName("Код станции")]
        public virtual Station Station { get; set; }
    }
}
