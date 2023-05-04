using TrainzLib.Models;

namespace TrainzApi.RepositoryMock
{
    public class GruzCrudMock : AbstractCrudMock<GruzType>
    {
        protected override Func<GruzType, int> GetId => t => t.Id;
    }
}
