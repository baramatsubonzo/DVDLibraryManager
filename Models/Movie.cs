using System;

namespace DVDLibraryManager
{
    public enum Genre
        {
            Drama,
            Adventure,
            Family,
            Action,
            SciFi,
            Comedy,
            Animated,
            Thriller,
            Other
        }
    public enum Classification
    {
        G,
        PG,
        M15Plus,
        MA15Plus
    }
    public class Movie
    {
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public Classification Classification { get; set; } // G, PG, M15+, MA15+
        public int Duration { get; set; }

        // For Admin
        public int AvailableCopies { get; set; }
        public int TotalBorrowedCount { get; set; }

        public int TotalCopies { get; set; }

        public Movie(string title, Genre genre, Classification classification, int duration, int availableCopies, int copies)
        {
            ValidateInput(title, duration, availableCopies, copies);

            Title = title;
            Genre = genre;
            Classification = classification;
            Duration = duration;
            AvailableCopies = availableCopies;
            TotalCopies = copies;
            TotalBorrowedCount = 0; // Initial value
        }

        // Method to call when borrowed
        // When borrowing, need to know success or failure immediately
        public bool Borrow()
        {
            if (AvailableCopies > 0)
            {
                AvailableCopies--;
                TotalBorrowedCount++;
                return true;
            }
            return false;
        }

        // Method to call when returned
        public void Return()
        {
            // Avoid increasing stock beyond the original count due to bugs or mistakes.
            if (AvailableCopies < TotalCopies)
            {
                AvailableCopies++;
            }
        }

        public void AddCopies(int copies)
        {
            AvailableCopies += copies;
            TotalCopies += copies;
        }

        public bool RemoveCopies(int copies)
        {
            if (copies > AvailableCopies)
                return false;

            AvailableCopies -= copies;
            TotalCopies -= copies;
            return true;
        }

        // Method to display information
        public override string ToString()
        {
            return
                $"Title: {Title}, Genre: {Genre}, Classification: {Classification}, Duration: {Duration} minutes, Available Copies: {AvailableCopies}";
        }


        private void ValidateInput(string title, int duration, int availableCopies, int copies)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.");

            if (duration <= 0 || duration > 600)
                throw new ArgumentException("Duration must be between 1 and 600 minutes.");

            if (availableCopies < 0)
                throw new ArgumentException("Available copies cannot be negative.");

            if (copies <= 0)
                throw new ArgumentException("Total copies must be at least 1.");

            if (availableCopies > copies)
                throw new ArgumentException("Available copies cannot exceed total copies.");
        }
    }
}
