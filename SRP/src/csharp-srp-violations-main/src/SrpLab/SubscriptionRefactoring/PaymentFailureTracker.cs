using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.SubscriptionRefactoring
{
    public class PaymentFailureTracker
    {
        public int FailedPayments { get; private set; }

        public void RegisterFailedPayment()
        {
            FailedPayments++;
        }
    }
}
