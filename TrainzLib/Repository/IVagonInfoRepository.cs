using TrainzLib.Models;

namespace TrainzLib.Repository
{
    public interface IVagonInfoRepository : ICrudRepository<VagonInfo>
    {
        void StartBuffer();
        int WayLastNum(int wayId);
        int WayFirsNum(int wayId);
        bool IsFirstOnWay(int vagonId);
        bool IsLastOnWay(int vagonId);
        IEnumerable<VagonInfo> VagonsOnWay(int wayId);
        VagonInfo InfoByWagon(int vagonNum);
        void ClearBuffer();
    }
}
