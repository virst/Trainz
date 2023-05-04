namespace TrainzLib.Models
{
    public class GruzType
    {
        public int Id { get; set; }
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
