using System.Reflection;
using TrainzLib.Models;

namespace TrainzMock
{
    public class StationCrudMock : AbstractCrudMock<Station>
    {
        protected override PropertyInfo IdProperty => typeof(Station).GetProperty("Id");
    }
}
