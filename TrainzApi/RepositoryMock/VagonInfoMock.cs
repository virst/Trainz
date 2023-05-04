using TrainzLib.Models;
using TrainzLib.Repository;

namespace TrainzApi.RepositoryMock
{
    public class VagonInfoMock : AbstractCrudMock<VagonInfo>, IVagonInfoRepository
    {
        protected override Func<VagonInfo, int> GetId => t => t.Id;

        public void ClearBuffer()
        {

        }

        public VagonInfo? InfoByWagon(int vagonNum)
        {
            return mockList.FirstOrDefault(t => t.VagonId == vagonNum);
        }

        public bool IsFirstOnWay(int vagonId)
        {
            return InfoByWagon(vagonId)?.OrderNum == 1;
        }

        public bool IsLastOnWay(int vagonId)
        {
            var vi = InfoByWagon(vagonId);
            if (vi == null)
                return false;

            return WayLastNum(vi.WayId ?? 0) == vi.OrderNum;

        }

        public void StartBuffer()
        {

        }

        public IEnumerable<VagonInfo> VagonsOnWay(int wayId)
        {
            return GetAll().Where(t => t.WayId == wayId);
        }

        public int WayFirsNum(int wayId)
        {
            return VagonsOnWay(wayId).Min(t => t.OrderNum) ?? 0;
        }

        public int WayLastNum(int wayId)
        {
            return VagonsOnWay(wayId).Max(t => t.OrderNum) ?? 0;
        }
    }
}
