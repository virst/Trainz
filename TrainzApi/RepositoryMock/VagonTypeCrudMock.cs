using TrainzLib.Models;

namespace TrainzApi.RepositoryMock
{
    public class VagonTypeCrudMock : AbstractCrudMock<VagonType>
    {
        protected override Func<VagonType, int> GetId => t => t.Id;
    }
    
}
