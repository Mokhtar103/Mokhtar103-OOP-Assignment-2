using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.KitchenTicketRefactoring
{
    public class ThermalTicketFormatter
    {
        private readonly AllergenDetector _allergenDetector;
        private readonly KitchenReadinessCalculator _readinessCalculator;

        public ThermalTicketFormatter(
            AllergenDetector allergenDetector,
            KitchenReadinessCalculator readinessCalculator)
        {
            _allergenDetector = allergenDetector;
            _readinessCalculator = readinessCalculator;
        }

        public string Render(KitchenOrder order, int orderNumber)
        {
            var width = 32;
            var line = new string('=', width);

            var body = string.Join(
                '\n',
                order.Items.Select(i =>
                    $"* {i.Item.ToUpperInvariant()} ({i.PrepMinutes}m)")
            );

            var allergens = _allergenDetector.Detect(order);

            var allergyLine = allergens.Count == 0
                ? "ALLERGENS: none"
                : "ALLERGENS: " + string.Join(",", allergens);

            return $"{line}\n" +
                   $"ORDER #{orderNumber}\n" +
                   $"ETA {_readinessCalculator.EstimatedReadyMinutes(order, 2)} MIN\n" +
                   $"{body}\n" +
                   $"{allergyLine}\n" +
                   $"{line}\n";
        }

        public string ExpoLaneHint(KitchenOrder order)
        {
            var allergens = _allergenDetector.Detect(order);

            if (allergens.Count > 0)
                return "LANE-ALLERGY";

            return _readinessCalculator.EstimatedReadyMinutes(order, 2) > 20
                ? "LANE-SLOW"
                : "LANE-FAST";
        }
    }
}
