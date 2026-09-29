using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.SupportTicketRefactoring
{
    public class SupportCommunicationFormatter
    {
        private readonly TicketSlaPolicy _slaPolicy;

        public SupportCommunicationFormatter(TicketSlaPolicy slaPolicy)
        {
            _slaPolicy = slaPolicy;
        }

        public string DraftPublicReply(SupportTicketInfo ticket, string priority, string agentName)
        {
            var apology = priority == "P1"
                ? "We are treating this as a critical incident."
                : "Thanks for reaching out.";

            var deadline = _slaPolicy.SlaDeadline(ticket, priority);

            return $"Hi,\n" +
                   $"{apology}\n" +
                   $"Ticket {ticket.Id} is with {agentName}. " +
                   $"Next update before {deadline:u}.\n";
        }

        public string InternalEscalationBlurb(SupportTicketInfo ticket, string priority)
        {
            var deadline = _slaPolicy.SlaDeadline(ticket, priority);

            return $"ESCALATE {ticket.Id} " +
                   $"priority={priority} " +
                   $"breachAt={deadline:u} " +
                   $"keywords-scanned=yes";
        }
    }
}
