using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.CourseEnrollmentRefactoring
{
    public class TuitionInvoiceFormatter
    {
        public string Create(
        string courseCode,
        string studentEmail,
        decimal tuition,
        CourseEnrollment enrollment)
        {
            if (!enrollment.IsSeated(studentEmail))
                return $"{courseCode},WAITLIST,0.00";

            var vat = Math.Round(tuition * 0.14m, 2);

            return $"{courseCode},TUITION,{tuition:0.00}," +
                   $"VAT,{vat:0.00},TOTAL,{(tuition + vat):0.00}";
        }
    }
}
