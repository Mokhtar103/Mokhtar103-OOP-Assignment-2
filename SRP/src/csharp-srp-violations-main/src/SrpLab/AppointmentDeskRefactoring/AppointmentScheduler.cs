using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.AppointmentDeskRefactoring
{
    public class AppointmentScheduler
    {
        private readonly ClinicSchedule _schedule;
        private readonly HashSet<DateTimeOffset> _booked = new();

        public AppointmentScheduler(ClinicSchedule schedule)
        {
            _schedule = schedule;
        }

        public DateTimeOffset? FindNextSlot(
            DateTimeOffset from,
            int searchHours)
        {
            var cursor = _schedule.Align(from);
            var end = from.AddHours(searchHours);

            while (cursor < end)
            {
                if (_schedule.IsWithinBusinessHours(cursor) &&
                    !_booked.Contains(cursor))
                {
                    return cursor;
                }

                cursor = cursor.AddMinutes(_schedule.SlotMinutes);
            }

            return null;
        }

        public bool TryBook(DateTimeOffset slot)
        {
            if (!_schedule.IsWithinBusinessHours(slot) ||
                _booked.Contains(slot))
            {
                return false;
            }

            _booked.Add(slot);
            return true;
        }
    }
}
