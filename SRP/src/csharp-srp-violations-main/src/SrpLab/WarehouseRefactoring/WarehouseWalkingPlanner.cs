using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.WarehouseRefactoring
{
    public class WarehouseWalkingPlanner
    {

        public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> CreateWalkingOrder(WarehousePickList pickList)
        {
            return pickList.Lines
                .OrderBy(line => line.Aisle)
                .ThenBy(line => line.Bin)
                .Select(line => (
                    line.Aisle,
                    line.Bin,
                    line.Sku,
                    Math.Min(line.QtyNeeded, line.QtyOnHand)))
                .Where(x => x.Item4 > 0)
                .ToList();
        }
    }
}
