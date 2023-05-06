using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainzLib.Models;

namespace TrainzLib
{
    public static class StaticPrint
    {
        public static string ToPrintString(this IEnumerable<VagonInfo> vis)
        {
            var sb = new StringBuilder();

            var ways = vis.Select(x => x.WayId).Distinct().OrderBy(t => t);

            foreach (var way in ways)
            {
                sb.AppendLine($"w{way} " + string.Join("", vis.Where(t => t.WayId == way).OrderBy(t => t.OrderNum).Select(t => $"-{t.VagonId}-")));
            }

            return sb.ToString();
        }
    }
}
