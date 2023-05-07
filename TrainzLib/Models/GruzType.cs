using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainzLib.Models
{
    [Table("gruz_type")]
    public class GruzType
    {
        [Column("id")]
        [DisplayName("Первыичный ключ")]
        public int Id { get; set; }

        [Column("nm")]
        [DisplayName("Наименование")]
        public string Name { get; set; } = string.Empty;

        public static IEnumerable<GruzType> GetBaseList()
        {
            int i = 0;
            yield return new GruzType { Id = ++i, Name = "Уголь" };
            yield return new GruzType { Id = ++i, Name = "СупеФасфа2" };
            yield return new GruzType { Id = ++i, Name = "Бревна" };
            yield return new GruzType { Id = ++i, Name = "Соки" };
            yield return new GruzType { Id = ++i, Name = "Зерно" };
            yield return new GruzType { Id = ++i, Name = "Азотная Кислота" };
            yield return new GruzType { Id = ++i, Name = "Нефть" };
        }
    }
}
