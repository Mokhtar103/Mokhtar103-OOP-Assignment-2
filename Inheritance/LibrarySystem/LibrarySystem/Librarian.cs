using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem
{
    public class Librarian : Employee
    {
        public Librarian(
       string personId,
       string fullName,
       string phone,
       DateTime hireDate,
       decimal monthlySalary)
       : base(
           personId,
           fullName,
           phone,
           hireDate,
           monthlySalary,
           0)
        {
        }

        public void ProcessReturn(Loan loan, DateTime returnDate)
        {
            loan.Return(returnDate);
        }

        public void MarkAsLost(Loan loan)
        {
            loan.MarkAsLost();
        }
    }
}
