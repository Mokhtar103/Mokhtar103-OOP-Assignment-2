using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.WarehouseRefactoring
{
    public class WarehousePickList
    {
       private readonly List<(
       string Sku,
       string Aisle,
       int Bin,
       int QtyNeeded,
       int QtyOnHand)> _lines = new();

        public IReadOnlyList<(
            string Sku,
            string Aisle,
            int Bin,
            int QtyNeeded,
            int QtyOnHand)> Lines => _lines;

        public void AddNeed(
            string sku,
            string aisle,
            int bin,
            int qtyNeeded,
            int qtyOnHand)
        {
            _lines.Add((sku, aisle, bin, qtyNeeded, qtyOnHand));
        }
    }
}
