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
    public class WayCrudDb : AbstractCrudDb<Way>
    {
        protected override DbSet<Way> DbSet => _context.Ways;
        public WayCrudDb(TrainzContext context) : base(context)
        {
        }
    }
}
