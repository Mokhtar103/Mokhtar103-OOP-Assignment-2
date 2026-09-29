using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.WarehouseRefactoring
{
    public class WarehouseOutputFormatter
    {
        private readonly WarehouseWalkingPlanner _walkingPlanner;
        private readonly InventoryAllocator _allocator;

        public WarehouseOutputFormatter(WarehouseWalkingPlanner walkingPlanner, InventoryAllocator allocator)
        {
            _walkingPlanner = walkingPlanner;
            _allocator = allocator;
        }

        public string PickerScript(WarehousePickList pickList)
        {
            var steps = _walkingPlanner
                .CreateWalkingOrder(pickList)
                .Select((step, index) =>
                    $"{index + 1}. Go aisle {step.Aisle} " +
                    $"bin {step.Bin}: pick {step.Qty} × {step.Sku}");

            var shortfalls = _allocator
                .Allocate(pickList)
                .Where(allocation =>
                {
                    var need = pickList.Lines
                        .First(line => line.Sku == allocation.Sku)
                        .QtyNeeded;

                    return allocation.Allocated < need;
                });

            var warning = shortfalls.Any()
                ? "SHORTAGES: " +
                  string.Join(", ", shortfalls.Select(s => s.Sku))
                : "SHORTAGES: none";

            return string.Join('\n', steps) +
                   "\n" +
                   warning;
        }

        public string WmsXmlBatch(WarehousePickList pickList, string batchId)
        {
            var parts = _allocator
                .Allocate(pickList)
                .Select(allocation =>
                    $"<line sku=\"{allocation.Sku}\" " +
                    $"qty=\"{allocation.Allocated}\" />");

            return $"<batch id=\"{batchId}\">" +
                   $"{string.Join("", parts)}" +
                   $"</batch>";
        }
    }
}
