using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.SupportTicketRefactoring
{
    public class SupportTicketInfo
    {
        public string Id { get; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
        public DateTimeOffset OpenedAt { get; }

        public SupportTicketInfo(
            string id,
            string subject,
            string body,
            DateTimeOffset openedAt)
        {
            Id = id;
            Subject = subject;
            Body = body;
            OpenedAt = openedAt;
        }

        public void AppendCustomerMessage(string text)
        {
            Body += "\n---\n" + text;
        }
    }
}
