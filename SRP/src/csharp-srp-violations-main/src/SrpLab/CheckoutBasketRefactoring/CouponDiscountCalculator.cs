using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.CheckoutBasketRefactoring
{
    public class CouponDiscountCalculator
    {
        private string? _couponRaw;

        public void ApplyCouponText(string? couponText)
        {
            _couponRaw = couponText;
        }

        public decimal CalculateDiscount(decimal subTotal)
        {
            if (string.IsNullOrWhiteSpace(_couponRaw))
                return 0m;

            var t = _couponRaw.Trim().ToUpperInvariant();

            if (t.StartsWith("SAVE") &&
                int.TryParse(t[4..], out var pct) &&
                pct is > 0 and <= 50)
            {
                return Math.Round(subTotal * pct / 100m, 2);
            }

            if (t.Contains("FREESHIP"))
                return 0m;

            if (t == "WELCOME10")
                return Math.Min(10m, subTotal);

            return 0m;
        }
    }
}
