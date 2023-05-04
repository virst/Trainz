using TrainzLib.Models;

namespace TrainzApi.RepositoryMock
{
    public class WayCrudMock : AbstractCrudMock<Way>
    {
        protected override Func<Way, int> GetId => t => t.Id;
    }
}
