using TrainzLib.Models;

namespace TrainzApi.RepositoryMock
{
    public class VagonCrudMock : AbstractCrudMock<Vagon>
    {
        protected override Func<Vagon, int> GetId => t => t.NomVag;
    }
}
