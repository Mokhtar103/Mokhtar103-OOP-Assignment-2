using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.CourseEnrollmentRefactoring
{
    public class CourseDetails
    {
        public string CourseCode { get; }
        public decimal Tuition { get; }

        public CourseDetails(string courseCode, decimal tuition)
        {
            CourseCode = courseCode;
            Tuition = tuition;
        }
    }
}
