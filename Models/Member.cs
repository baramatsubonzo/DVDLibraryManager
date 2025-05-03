using System;

namespace DVDLibraryManager
{
    public class Member
    {
        // Basic Info
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        // List of up to 5 borrowed movies
        private string[] borrowedMovies;
        private int borrowedCount;
        public Member(string firstName, string lastName, string phoneNumber, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Password = password;
            borrowedMovies = new string[5]; // List of up to 5 borrowed movies
            borrowedCount = 0;
        }

        // Borrow movies
        public bool BorrowMovie(string movieTitle)
        {
            if (borrowedCount >= 5)
            {
                return false; // up to 5 borrowed movies
            }

            for (int i = 0; i < borrowedCount; i++)
            {
                if (borrowedMovies[i] == movieTitle)
                {
                    return false; // Cannot borrow the same movie twice
                }
            }

            borrowedMovies[borrowedCount] = movieTitle;
            borrowedCount++;
            return true;
        }

        // Retrun　movies
        public bool ReturnMovie(string movieTitle)
        {
            for (int i = 0; i < borrowedCount; i++)
            {
                if (borrowedMovies[i] == movieTitle)
                {
                  // If found, shift the following elements forward
                    for (int j = i; j < borrowedCount - 1; j++)
                    {
                        borrowedMovies[j] = borrowedMovies[j + 1];
                    }
                    borrowedMovies[borrowedCount - 1] = null;
                    borrowedCount--;
                    return true;
                }
            }
            return false; // If not found
        }

        // Returns the list of currently borrowed movies
        public string[] GetCurrentBorrowedMovies()
        {
            string[] currentBorrowed = new string[borrowedCount];
            for (int i = 0; i < borrowedCount; i++)
            {
                currentBorrowed[i] = borrowedMovies[i];
            }
            return currentBorrowed;
        }

        public bool HasBorrowedMovies()
        {
            return borrowedCount > 0;
        }

        // Display member information
        public override string ToString()
        {
            return $"Name: {FirstName} {LastName}, Phone: {PhoneNumber}, Borrowed Movies Count: {borrowedCount}";
        }

        // Provides a public interface to retrieve all validation error messages.
        // Individual error checks are kept private to encapsulate internal logic.
        public string[] ReturnMemberErrors()
        {
            string[] errors = new string[3];
            int index = 0;

            var nameError = ReturnNameError();
            if (nameError != null) errors[index++] = nameError;

            var passwordError = ReturnPasswordError();
            if (passwordError != null) errors[index++] = passwordError;

            var phoneError = ReturnPhoneNumberError();
            if (phoneError != null) errors[index++] = phoneError;

            if (index == 0)
            {
                return new string[0];
            }

            string[] result = new string[index];
            Array.Copy(errors, result, index);
            return result;
        }


        // Validation
        public string ReturnNameError()
        {
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
            {
                return "First and last name must not be empty.";
            }
            return null;
        }

        // According assignment: a four-digit password is set for the member through the staff member.
        public string ReturnPasswordError()
        {
            if (string.IsNullOrWhiteSpace(Password)|| Password.Length != 4)
            {
                return "Password must be exactly 4 digits.";
            }

            foreach (char c in Password)
            {
                if (!char.IsDigit(c))
                {
                    return "Password must contain only digits.";
                }
            }

            return null;
        }

        // No specific regulation for phone numbers, but typically set to 10 digits.
        private string ReturnPhoneNumberError()
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber) || PhoneNumber.Length != 10)
            {
                return "Phone number must be exactly 10 digits.";
            }

            foreach (char c in PhoneNumber)
            {
                if (!char.IsDigit(c))
                {
                    return "Phone number must contain only digits.";
                }
            }

            return null;
        }

    }
}
