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
    public class GruzCrudDb : AbstractCrudDb<GruzType>
    {
        protected override DbSet<GruzType> DbSet => _context.GruzTypes;
        public GruzCrudDb(TrainzContext context) : base(context)
        {
        }      
    }
}
