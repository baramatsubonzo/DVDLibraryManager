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
    public class Movie
    {
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public string Classification { get; set; } // G, PG, M15+, MA15+
        public int Duration { get; set; }

        // For Admin
        public int AvailableCopies { get; set; }
        public int TotalBorrowedCount { get; set; }

        public int TotalCopies { get; set; }

        public Movie(string title, Genre genre, string classification, int duration, int availableCopies, int copies)
        {
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

        // Todo: Extend it when starting to create the staff menu
        public void AddCopies(int copies)
        {
            AvailableCopies += copies;
            TotalCopies += copies;
        }
        // public RemoveCopies(int copies)

        // Method to display information
        public override string ToString()
        {
            return
                $"Title: {Title}, Genre: {Genre}, Classification: {Classification}, Duration: {Duration} minutes, Available Copies: {AvailableCopies}";
        }
    }
}
