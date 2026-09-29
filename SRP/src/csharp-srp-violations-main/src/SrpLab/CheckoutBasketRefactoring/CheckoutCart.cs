using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.CheckoutBasketRefactoring
{
    public class CheckoutCart
    {
        private readonly List<(string Sku, decimal Price, int Qty)> _lines = new();

        public void AddLine(string sku, decimal price, int qty)
        {
            if (qty <= 0)
                throw new ArgumentOutOfRangeException(nameof(qty));

            _lines.Add((sku, price, qty));
        }

        public decimal SubTotal()
        {
            return _lines.Sum(l => l.Price * l.Qty);
        }

        public int LineCount => _lines.Count;

        public IReadOnlyList<string> Skus =>
            _lines.Select(l => l.Sku).ToList();
    }
}
