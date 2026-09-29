using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.AppointmentDeskRefactoring
{
    public class AppointmentCalendarFormatter
    {
        public string ToIcs(
       DateTimeOffset slot,
       int slotMinutes,
       string patientName,
       string clinician)
        {
            var uid = Guid.NewGuid();
            var end = slot.AddMinutes(slotMinutes);

            return "BEGIN:VCALENDAR\nVERSION:2.0\nBEGIN:VEVENT\n" +
                   $"UID:{uid}\nDTSTART:{slot:yyyyMMdd'T'HHmmss'Z'}\n" +
                   $"DTEND:{end:yyyyMMdd'T'HHmmss'Z'}\n" +
                   $"SUMMARY:Visit {patientName} / {clinician}\n" +
                   "END:VEVENT\nEND:VCALENDAR\n";
        }
    }
}
