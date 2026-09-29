using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.SubscriptionRefactoring
{
    public class BillingOutputFormatter
    {
        private readonly SubscriptionProrationCalculator _prorationCalculator;
        private readonly InvoiceNumberGenerator _invoiceNumberGenerator;

        public BillingOutputFormatter(
            SubscriptionProrationCalculator prorationCalculator,
            InvoiceNumberGenerator invoiceNumberGenerator)
        {
            _prorationCalculator = prorationCalculator;
            _invoiceNumberGenerator = invoiceNumberGenerator;
        }

        public string DunningEmail(
            Subscription subscription,
            PaymentFailureTracker paymentFailures,
            string customerName,
            DateOnly asOf)
        {
            var amount = _prorationCalculator.Prorate(subscription, subscription.PeriodStart);

            var invoice = _invoiceNumberGenerator.NextInvoiceNumber( subscription.PeriodStart);

            var severity = paymentFailures.FailedPayments switch
            {
                <= 1 => "friendly reminder",
                2 => "second notice",
                _ => "final notice before suspension"
            };

            return $"Subject: {severity} {invoice}\n" +
                   $"Hi {customerName},\n" +
                   $"Balance {amount:C} as of {asOf:o} " +
                   $"({paymentFailures.FailedPayments} failures).\n";
        }

        public string LedgerJournalLine(Subscription subscription, DateOnly activeFrom)
        {
            var invoice = _invoiceNumberGenerator.NextInvoiceNumber(subscription.PeriodStart);

            var amount = _prorationCalculator.Prorate(subscription, activeFrom);

            return $"{subscription.CustomerId}," +
                   $"{invoice}," +
                   $"{amount:0.00},AR-SUB";
        }
    }
}
