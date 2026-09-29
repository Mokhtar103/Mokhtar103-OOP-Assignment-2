using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.CheckoutBasketRefactoring
{
    public class GiftMessageCardFormatter
    {
        public string Create(
        string fromName,
        IReadOnlyList<string> skus,
        decimal total)
        {
            var items = string.Join(", ", skus);

            return $"Dear friend,\n" +
                   $"A gift from {fromName} awaits ({items}).\n" +
                   $"Total surprise value: {total:C}\n";
        }
    }
}
