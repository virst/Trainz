using System.Reflection;
using TrainzLib.Models;

namespace TrainzMock
{
    public class GruzCrudMock : AbstractCrudMock<GruzType>
    {
        protected override PropertyInfo IdProperty => typeof(GruzType).GetProperty("Id");
    }
}
