using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem
{
    public class HeadLibrarian : Employee
    {
        public HeadLibrarian(
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
           400)
        {
        }

        public void ChangeLateFee(
            LibraryItem item,
            decimal newLateFee)
        {
            item.ChangePricing(newLateFee);
        }

        public void Withdraw(LibraryItem item)
        {
            item.Withdraw();
        }

        public void Restore(LibraryItem item)
        {
            item.Restore();
        }
    }
}
