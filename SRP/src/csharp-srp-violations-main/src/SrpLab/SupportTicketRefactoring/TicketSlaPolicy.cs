using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.SupportTicketRefactoring
{
    public class TicketSlaPolicy
    {
        public DateTimeOffset SlaDeadline(SupportTicketInfo ticket, string priority)
        {
            var hours = priority switch
            {
                "P1" => 4,
                "P2" => 24,
                _ => 72
            };

            return ticket.OpenedAt.AddHours(hours);
        }

        public bool IsBreached(SupportTicketInfo ticket, string priority, DateTimeOffset now)
        {
            return now > SlaDeadline(ticket, priority);
        }
    }
}
