using TrainzLib.Models;

namespace TrainzApi.RepositoryMock
{
    public class StationCrudMock : AbstractCrudMock<Station>
    {
        protected override Func<Station, int> GetId => t => t.Id;
    }
}
