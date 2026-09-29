using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.CheckoutBasketRefactoring
{
    public class CheckoutTotalCalculator
    {
        private const decimal GiftWrapFee = 4.99m;

        public decimal Calculate(
            decimal subTotal,
            decimal discount,
            bool giftWrapEnabled)
        {
            var total = subTotal - discount;

            if (giftWrapEnabled)
                total += GiftWrapFee;

            return Math.Max(0m, total);
        }
    }
}
