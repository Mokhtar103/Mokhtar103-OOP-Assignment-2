using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.AppointmentDeskRefactoring
{
    public class AppointmentSmsFormatter
    {
        public string CreateReminder(
        DateTimeOffset slot,
        string clinicPhone)
        {
            return $"Reminder: appointment {slot:MMM dd HH:mm}. " +
                   $"Call {clinicPhone} to reschedule.";
        }
    }
}
