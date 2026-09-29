using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.SubscriptionRefactoring
{
    public class InvoiceNumberGenerator
    {
        private static int _invoiceSeq = 1000;

        public string NextInvoiceNumber(DateOnly periodStart)
        {
            var n = ++_invoiceSeq;

            return $"INV-{periodStart:yyyyMM}-{n:D5}";
        }
    }
}
