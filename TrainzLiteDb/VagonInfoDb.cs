using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainzLib.Models;
using TrainzLib.Repository;
using TrainzLiteDb.Data;

namespace TrainzLiteDb
{
    public class VagonInfoDb : AbstractCrudDb<VagonInfo>, IVagonInfoRepository
    {
        protected override DbSet<VagonInfo> DbSet => _context.VagonInfos;
        public VagonInfoDb(TrainzContext context) : base(context)
        {
        }

        private bool buffering = false;

        private void SaveChanges()
        {
            if (buffering) return;
            _context.SaveChanges();
        }

        public void StartBuffer()
        {
            buffering = true;
        }

        public int WayFirsNum(int wayId)
        {
            return VagonsOnWay(wayId).Min(t => t.OrderNum) ?? 0;
        }

        public int WayLastNum(int wayId)
        {
            return VagonsOnWay(wayId).Max(t => t.OrderNum) ?? 0;
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

        public IEnumerable<VagonInfo> VagonsOnWay(int wayId)
        {
            return DbSet.Where(t => t.WayId == wayId).OrderBy(t => t.OrderNum);
        }

        public VagonInfo InfoByWagon(int vagonNum)
        {
            return DbSet.FirstOrDefault(t => t.VagonId == vagonNum);
        }

        public void ClearBuffer()
        {
            buffering = false;
            SaveChanges();
        }

        public void ReOrderVagonsOnWay(int wayId)
        {
            var infoToReOrd = this.VagonsOnWay(wayId);
            int num = 0;
            foreach (var v in infoToReOrd)
            {
                v.OrderNum = ++num;               
            }
            SaveChanges();
        }
    }
}

