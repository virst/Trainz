using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainzLib.Models
{
    [Table("vagon_type")]
    public class VagonType
    {
        [Column("id")]
        [DisplayName("Первыичный ключ")]
        public int Id { get; set; }

        [Column("nm")]
        [DisplayName("Наименование")]
        public string Name { get; set; } = string.Empty;

        public static IEnumerable<VagonType> GetBaseList()
        {
            int i = 0;
            yield return new VagonType { Id = ++i, Name = "Пассажирский Купе" };
            yield return new VagonType { Id = ++i, Name = "Пассажирский Плацкарт" };
            yield return new VagonType { Id = ++i, Name = "Платформа" };
            yield return new VagonType { Id = ++i, Name = "Полувагон" };
            yield return new VagonType { Id = ++i, Name = "Хоппер" };
            yield return new VagonType { Id = ++i, Name = "Цистерна" };
            yield return new VagonType { Id = ++i, Name = "Крытый вагон" };
        }
    }
}
