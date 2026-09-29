using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem
{
    public enum LoanStatus
    {
        Borrowed,
        Returned,
        Lost
    }
    public class Loan
    {
        public int LoanId { get; }

        public DateTime BorrowDate { get; }

        public DateTime DueDate
        {
            get
            {
                return BorrowDate.AddDays(Item.LoanPeriodDays);
            }
        }

        public Member Member { get; }

        public LibraryItem Item { get; }

        public LoanStatus Status { get; private set; }

        public DateTime? ReturnDate { get; private set; }

        public decimal LateFee
        {
            get
            {
                if (Status != LoanStatus.Returned)
                    return 0;

                int daysLate = (ReturnDate!.Value - DueDate).Days;

                if (daysLate <= 0)
                    return 0;

                decimal originalFee =
                    daysLate * Item.GetDailyLateFee();

                decimal discount =
                    originalFee * Member.DiscountPercentage / 100;

                return originalFee - discount;
            }
        }

        public Loan(int loanId, DateTime borrowDate, Member member, LibraryItem item)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            if (item == null)
                throw new ArgumentNullException(nameof(item));

            LoanId = loanId;
            BorrowDate = borrowDate;
            Member = member;
            Item = item;
            Status = LoanStatus.Borrowed;
        }

        public void Return(DateTime returnDate)
        {
            if (Status != LoanStatus.Borrowed)
                throw new InvalidOperationException("Only a borrowed loan can be returned");

            if (returnDate < BorrowDate)
                throw new ArgumentException("Return date cannot be earlier than borrow date");

            ReturnDate = returnDate;
            Status = LoanStatus.Returned;

            Item.MarkAsReturned();
        }

        public void MarkAsLost()
        {
            if (Status != LoanStatus.Borrowed)
                throw new InvalidOperationException("Only a borrowed loan can be marked as lost");

            Status = LoanStatus.Lost;
        }
    }
}
