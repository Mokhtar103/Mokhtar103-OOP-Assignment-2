using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem
{
    public class Member : Person
    {
        private readonly List<Loan> _loans = new List<Loan>();

        public IReadOnlyList<Loan> Loans => _loans;

        public int MaxLoans { get; }
        public decimal DiscountPercentage { get; }

        protected Member(
            string personId,
            string fullName,
            string phone,
            int maxLoans,
            decimal discountPercentage)
            : base(personId, fullName, phone)
        {
            if (maxLoans <= 0)
                throw new ArgumentException("Maximum loans must be greater than zero.");

            if (discountPercentage < 0)
                throw new ArgumentException("Discount percentage cannot be negative.");

            MaxLoans = maxLoans;
            DiscountPercentage = discountPercentage;
        }

        public Loan Borrow(
            LibraryItem item,
            int loanId,
            DateTime borrowDate)
        {
            int activeLoans = 0;

            foreach (Loan loan in _loans)
            {
                if (loan.Status == LoanStatus.Borrowed)
                    activeLoans++;
            }

            if (activeLoans >= MaxLoans)
                throw new InvalidOperationException(
                    "Member cannot have more active loans than the maximum limit");

            item.ValidateCanBeBorrowed();

            Loan newLoan = new Loan(
                loanId,
                borrowDate,
                this,
                item);

            _loans.Add(newLoan);

            item.MarkAsBorrowed();

            return newLoan;
        }

    }
}
