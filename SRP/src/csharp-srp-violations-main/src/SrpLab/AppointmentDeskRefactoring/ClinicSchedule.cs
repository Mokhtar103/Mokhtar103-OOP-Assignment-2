using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.AppointmentDeskRefactoring
{
    public class ClinicSchedule
    {
        public TimeOnly Open { get; }
        public TimeOnly Close { get; }
        public int SlotMinutes { get; }

        public ClinicSchedule(TimeOnly open, TimeOnly close, int slotMinutes)
        {
            Open = open;
            Close = close;
            SlotMinutes = slotMinutes;
        }

        public bool IsWithinBusinessHours(DateTimeOffset when)
        {
            if (when.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday)
                return false;

            var time = TimeOnly.FromDateTime(when.DateTime);

            return time >= Open &&
                   time.AddMinutes(SlotMinutes) <= Close;
        }

        public DateTimeOffset Align(DateTimeOffset from)
        {
            var minutes = from.Minute - (from.Minute % SlotMinutes);

            return new DateTimeOffset(
                from.Year,
                from.Month,
                from.Day,
                from.Hour,
                minutes,
                0,
                from.Offset);
        }
    }
}
