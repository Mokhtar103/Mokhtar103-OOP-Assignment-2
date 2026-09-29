using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.SubscriptionRefactoring
{
    public class SubscriptionProrationCalculator
    {
        public decimal Prorate(Subscription subscription, DateOnly activeFrom)
        {
            if (activeFrom <= subscription.PeriodStart)
                return subscription.MonthlyPrice;

            if (activeFrom >= subscription.PeriodEnd)
                return 0m;

            var totalDays = subscription.PeriodEnd.DayNumber - subscription.PeriodStart.DayNumber;

            if (totalDays <= 0)
                return subscription.MonthlyPrice;

            var used = subscription.PeriodEnd.DayNumber - activeFrom.DayNumber;

            return Math.Round(subscription.MonthlyPrice * used / totalDays, 2);
        }
    }
}
