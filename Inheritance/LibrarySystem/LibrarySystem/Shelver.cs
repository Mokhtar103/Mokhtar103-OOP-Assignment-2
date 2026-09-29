using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem
{
    public class Shelver : Employee
    {
        public string Section { get; private set; }

        public Shelver(
            string personId,
            string fullName,
            string phone,
            DateTime hireDate,
            decimal monthlySalary,
            string section)
            : base(
                personId,
                fullName,
                phone,
                hireDate,
                monthlySalary,
                0)
        {
            if (string.IsNullOrEmpty(section))
                throw new ArgumentException("Section cannot be null or empty.");

            Section = section;
        }

        public void Reassign(string newSection)
        {
            if (string.IsNullOrEmpty(newSection))
                throw new ArgumentException(
                    "Section cannot be null or empty.");

            Section = newSection;
        }
    }
}
