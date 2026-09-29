using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.CourseEnrollmentRefactoring
{
    public class CourseWelcomePacketFormatter
    {
        public string Create(
       string courseCode,
       string studentEmail,
       string studentName,
       CourseEnrollment enrollment)
        {
            var status = enrollment.IsSeated(studentEmail)
                ? "confirmed seat"
                : $"waitlist #{enrollment.WaitlistPosition(studentEmail)}";

            return $"# Welcome to {courseCode}\n" +
                   $"Hi {studentName},\n" +
                   $"Your status: **{status}**.\n" +
                   $"Bring a laptop. Discord onboarding link: " +
                   $"https://example.invalid/{courseCode.ToLowerInvariant()}\n";
        }
    }
}
