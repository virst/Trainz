using System.Reflection;
using TrainzLib.Models;
using TrainzLib.Repository;

namespace TrainzMock
{
    public class VagonInfoMock : AbstractCrudMock<VagonInfo>, IVagonInfoRepository
    {
        protected override PropertyInfo IdProperty => typeof(VagonInfo).GetProperty("Id");

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

        public void ReOrderVagonsOnWay(int wayId)
        {
            var infoToReOrd = this.VagonsOnWay(wayId).OrderBy(t => t.OrderNum);
            int num = 0;
            foreach (var v in infoToReOrd)
            {
                v.OrderNum = ++num;
                this.Update(v);
            }
        }

        public void StartBuffer()
        {

        }

        public IEnumerable<VagonInfo> VagonsOnWay(int wayId)
        {
            return GetAll().Where(t => t.WayId == wayId).OrderBy(t=>t.OrderNum);
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
