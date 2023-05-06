using System.Reflection;
using TrainzLib.Models;

namespace TrainzMock
{
    public class VagonCrudMock : AbstractCrudMock<Vagon>
    {
        protected override PropertyInfo IdProperty => typeof(Vagon).GetProperty("NomVag");
    }
}
