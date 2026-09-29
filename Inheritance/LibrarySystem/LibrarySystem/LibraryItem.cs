using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem
{
    public class LibraryItem
    {
        public string CatalogNumber { get; }
        public string Title { get; }

        public decimal BaseLateFee { get; private set; }

        public bool IsWithdrawn { get; private set; }

        public bool IsOnLoan { get; private set; }

        public int LoanPeriodDays { get; }

        private decimal LateFeeMultiplier { get; }

        protected LibraryItem(string catalogNumber, string title, decimal baseLateFee, int loanPeriodDays, decimal lateFeeMultiplier)
        {
            if (string.IsNullOrEmpty(catalogNumber))
                throw new ArgumentException(
                    "Catalog number cannot be null or empty.");

            if (string.IsNullOrEmpty(title))
                throw new ArgumentException(
                    "Title cannot be null or empty.");

            if (baseLateFee <= 0)
                throw new ArgumentException(
                    "Base late fee must be greater than zero.");

            if (loanPeriodDays <= 0)
                throw new ArgumentException(
                    "Loan period must be greater than zero.");

            if (lateFeeMultiplier <= 0)
                throw new ArgumentException(
                    "Late fee multiplier must be greater than zero.");

            CatalogNumber = catalogNumber;
            Title = title;
            BaseLateFee = baseLateFee;
            LoanPeriodDays = loanPeriodDays;
            LateFeeMultiplier = lateFeeMultiplier;
        }

        public decimal GetDailyLateFee()
        {
            return BaseLateFee * LateFeeMultiplier;
        }

        public void ChangePricing(decimal newLateFee)
        {
            if (newLateFee <= 0)
                throw new ArgumentException(
                    "New late fee must be greater than zero.");

            BaseLateFee = newLateFee;
        }

        public void Withdraw()
        {
            if (IsWithdrawn)
                throw new InvalidOperationException(
                    "Item is already withdrawn.");

            IsWithdrawn = true;
        }

        public void Restore()
        {
            if (!IsWithdrawn)
                throw new InvalidOperationException(
                    "Item is not withdrawn.");

            IsWithdrawn = false;
        }

        internal void ValidateCanBeBorrowed()
        {
            if (IsWithdrawn)
                throw new InvalidOperationException(
                    "Withdrawn item cannot be borrowed.");

            if (IsOnLoan)
                throw new InvalidOperationException(
                    "Item is already on loan.");
        }

        internal void MarkAsBorrowed()
        {
            IsOnLoan = true;
        }

        internal void MarkAsReturned()
        {
            IsOnLoan = false;
        }
    }
}
