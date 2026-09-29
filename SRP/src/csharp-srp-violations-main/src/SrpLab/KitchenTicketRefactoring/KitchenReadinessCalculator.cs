using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.KitchenTicketRefactoring
{
    public class KitchenReadinessCalculator
    {
        private readonly AllergenDetector _allergenDetector;

        public KitchenReadinessCalculator(AllergenDetector allergenDetector)
        {
            _allergenDetector = allergenDetector;
        }

        public int EstimatedReadyMinutes(KitchenOrder order, int openStations)
        {
            if (openStations <= 0)
                openStations = 1;

            var sequential = order.Items.Sum(i => i.PrepMinutes);

            var parallel = (int)Math.Ceiling(
                sequential / (double)openStations
            );

            if (_allergenDetector.Detect(order).Count > 0)
                parallel += 3;

            var longest = order.Items.Count == 0
                ? 0
                : order.Items.Max(i => i.PrepMinutes);

            return Math.Max(parallel, longest);
        }
    }
}
