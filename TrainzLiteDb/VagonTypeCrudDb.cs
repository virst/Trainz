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
    public class VagonTypeCrudDb : AbstractCrudDb<VagonType>
    {
        protected override DbSet<VagonType> DbSet => _context.VagonTypes;
        public VagonTypeCrudDb(TrainzContext context) : base(context)
        {
        }
    }
}
