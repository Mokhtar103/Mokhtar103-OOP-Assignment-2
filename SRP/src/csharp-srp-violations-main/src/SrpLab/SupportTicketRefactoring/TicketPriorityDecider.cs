using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.SupportTicketRefactoring
{
    public class TicketPriorityDecider
    {
        public string CalculatePriority(SupportTicketInfo ticket)
        {
            var blob = (ticket.Subject + " " + ticket.Body)
                .ToLowerInvariant();

            if (blob.Contains("down") ||
                blob.Contains("outage") ||
                blob.Contains("cannot login"))
            {
                return "P1";
            }

            if (blob.Contains("urgent") ||
                blob.Contains("asap") ||
                blob.Contains("blocked"))
            {
                return "P2";
            }

            return "P3";
        }
    }
}
