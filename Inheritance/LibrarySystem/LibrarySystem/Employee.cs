using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem
{
    public class Employee : Person
    {
        public DateTime HireDate { get; }

        public decimal MonthlySalary { get; private set; }

        private decimal ResponsibilityAllowance { get; }

        protected Employee(
            string personId,
            string fullName,
            string phone,
            DateTime hireDate,
            decimal monthlySalary,
            decimal responsibilityAllowance)
            : base(personId, fullName, phone)
        {
            if (monthlySalary < 0)
                throw new ArgumentException("Monthly salary cannot be negative.");

            HireDate = hireDate;
            MonthlySalary = monthlySalary;
            ResponsibilityAllowance = responsibilityAllowance;
        }

        public void GiveRaise(decimal percentage)
        {
            if (percentage <= 0)
                throw new ArgumentException(
                    "Raise percentage must be greater than zero.");

            MonthlySalary += MonthlySalary * percentage / 100;
        }

        public decimal GetMonthlyPay()
        {
            return MonthlySalary + ResponsibilityAllowance;
        }
    }
}
