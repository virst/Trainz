using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainzLib.Models
{
    [Table("stations")]
    public class Station
    {
        [Column("id")]
        [DisplayName("Первыичный ключ")]
        public int Id { get; set; }

        [Column("nm")]
        [DisplayName("Наименование")]
        public string Name { get; set; } = string.Empty;

        [Column("desct")]
        [DisplayName("Описание")]
        public string? Description { get; set; }
        public List<Way> Ways { get; set; } = new List<Way>();
    }
}
