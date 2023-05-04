using TrainzLib.Models;
using TrainzLib.Repository;

namespace TrainzApi.RepositoryMock
{
    public class VagonInfoMock : AbstractCrudMock<VagonInfo>, IVagonInfoRepository
    {
        protected override Func<VagonInfo, int> GetId => t => t.Id;

        public IEnumerable<VagonInfo> VagonsOnWay(int wayId)
        {
            return GetAll().Where(t => t.WayId == wayId);
        }

        public int WayLastNum(int wayId)
        {
            return VagonsOnWay(wayId).Max(t => t.OrderNum) ?? 0;
        }
    }
}
