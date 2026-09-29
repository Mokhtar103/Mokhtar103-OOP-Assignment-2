namespace LibrarySystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Library System ===");
            Console.WriteLine();

            // can't be compiled

            //Person person = new Person("P1", "Ahmed", "01000000000");
            //Member member = new Member("M1", "Ahmed", "01000000000", 3, 0);
            //Staff staff = new Staff("S1", "Ahmed", "01000000000",
            //    DateTime.Today, 5000, 0);
            //LibraryItem item = new LibraryItem("I1", "Item", 10, 7, 1);

            StudentMember student = new StudentMember("M001", "Ahmed", "01000000000");

            // no compile
            //student.FullName = "New Name"; 

            // no compile
            // student.Loans.Add(null); 

            Book book = new Book("B001", "Clean Code", 10);

            // can't be compiled
            //book.IsOnLoan = true;



          

            HeadLibrarian head =
                new HeadLibrarian(
                    "S003",
                    "Head Librarian",
                    "01033333333",
                    new DateTime(2025, 1, 1),
                    10000);

            Book withdrawnBook = new Book("B002", "Design Patterns", 10);

            head.Withdraw(withdrawnBook);

            try
            {
                student.Borrow(withdrawnBook, 1, new DateTime(2026, 9, 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Withdrawn item: " + ex.Message);
            }

            Console.WriteLine();

            // Item Already on loan case

            Book borrowedBook = new Book("B003", "C# in Depth", 10);

            StudentMember anotherStudent = new StudentMember("M002", "Mohamed", "01011111111");

            Loan firstLoan = student.Borrow(borrowedBook, 2, new DateTime(2026, 9, 1));

            try
            {
                anotherStudent.Borrow(borrowedBook, 3, new DateTime(2026, 9, 2));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Already on loan: " + ex.Message);
            }

            Console.WriteLine();

            // max 3 loans case

            //Book book1 = new Book("B004", "Book 1", 10);
            //Book book2 = new Book("B005", "Book 2", 10);
            //Book book3 = new Book("B006", "Book 3", 10);
            //Book book4 = new Book("B007", "Book 4", 10);

            //student.Borrow(book1, 4, new DateTime(2026, 9, 1));

            //student.Borrow(book2, 5, new DateTime(2026, 9, 1));

            //try
            //{
            //    student.Borrow(book3, 6, new DateTime(2026, 9, 1));
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Student 4th loan: " + ex.Message);
            //}

            //Console.WriteLine();

            // Staff list

            Librarian librarian =
                new Librarian(
                    "S001",
                    "Ali",
                    "01022222222",
                    new DateTime(2026, 1, 1),
                    6000);

            Shelver shelver =
                new Shelver(
                    "S002",
                    "Omar",
                    "01044444444",
                    new DateTime(2026, 2, 1),
                    5000,
                    "History");

          

            List<Employee> staffMembers = new List<Employee>();

            staffMembers.Add(librarian);
            staffMembers.Add(head);
            staffMembers.Add(shelver);

            foreach (Employee staffMember in staffMembers)
            {
                Console.WriteLine("Staff monthly pay: " + staffMember.GetMonthlyPay());
            }

            Console.WriteLine();

            // Library Items List

            List<LibraryItem> items = new List<LibraryItem>();

            Book polymorphicBook = new Book("B008", "Book", 10);

            DVD dvd = new DVD("D001", "DVD", 10);

            Magazine magazine = new Magazine("M001", "Magazine", 10);

            items.Add(polymorphicBook);
            items.Add(dvd);
            items.Add(magazine);

            foreach (LibraryItem libraryItem in items)
            {
                Console.WriteLine(
                    "Loan period: " +
                    libraryItem.LoanPeriodDays +
                    " days, Daily late fee: " +
                    libraryItem.GetDailyLateFee());
            }

            Console.WriteLine();

            // Premium member returning dvd 5 days late case

            PremiumMember premium =
                new PremiumMember("M003", "Premium User", "01055555555", 20);

            DVD premiumDvd =
                new DVD("D002", "Premium DVD", 10);

            DateTime borrowDate = new DateTime(2026, 9, 1);

            Loan premiumLoan = premium.Borrow(premiumDvd, 10, borrowDate);

            DateTime returnDate = new DateTime(2026, 9, 13);

            librarian.ProcessReturn(premiumLoan, returnDate);

            Console.WriteLine("Premium DVD due date: " + premiumLoan.DueDate);

            Console.WriteLine("Premium DVD late fee: " + premiumLoan.LateFee);

            Console.WriteLine("Premium reading points: " + premium.ReadingPoints);

            Console.WriteLine();

            // Return same loan twice case

            Book returnBook = new Book("B009", "Return Test", 10);

            Loan returnLoan = student.Borrow(returnBook, 20, new DateTime(2026, 9, 1));

            librarian.ProcessReturn(returnLoan, new DateTime(2026, 9, 5));

            try
            {
                librarian.ProcessReturn(returnLoan, new DateTime(2026, 9, 6));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Return twice: " + ex.Message);
            }

            Console.WriteLine();

            // Mark returned as lost case

            try
            {
                librarian.MarkAsLost(returnLoan);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Returned loan marked as lost: " + ex.Message);
            }

            Console.WriteLine();

            // Invalid raise case

            try
            {
                librarian.GiveRaise(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid raise: " + ex.Message);
            }

            // Invalid late fee case

            try
            {
                head.ChangeLateFee(polymorphicBook,0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid late fee: " + ex.Message);
            }
        }
    }
}
