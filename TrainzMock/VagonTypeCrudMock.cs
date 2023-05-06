using System.Reflection;
using TrainzLib.Models;

namespace TrainzMock
{
    public class VagonTypeCrudMock : AbstractCrudMock<VagonType>
    {
        protected override PropertyInfo IdProperty => typeof(VagonType).GetProperty("Id");
    }
    
}
