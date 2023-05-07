using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainzLib.Models;
using TrainzLiteDb.Data;

namespace TrainzLiteDb
{
    public class StationCrudDb : AbstractCrudDb<Station>
    {
        protected override DbSet<Station> DbSet => _context.Stations;
        public StationCrudDb(TrainzContext context) : base(context)
        {
        }
    }
}
