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
    }
}
