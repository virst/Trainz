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
    public class VagonCrudDb : AbstractCrudDb<Vagon>
    {
        protected override DbSet<Vagon> DbSet => _context.Vagons;
        public VagonCrudDb(TrainzContext context) : base(context)
        {
        }
    }
}
