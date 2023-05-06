using System.Reflection;
using TrainzLib.Models;

namespace TrainzMock
{
    public class WayCrudMock : AbstractCrudMock<Way>
    {
        protected override PropertyInfo IdProperty => typeof(Way).GetProperty("Id");
    }
}
