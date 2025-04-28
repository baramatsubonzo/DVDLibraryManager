using System;

namespace DVDLibraryManager
{
    public class MovieCollection
    {
        private Movie[] movies;
        private int movieCount;

        public MovieCollection()
        {
            movies = new Movie[1000]; //max 1000
            movieCount = 0;
        }

        // TODO: Check later if this hash function matches the lecture
        private int HashFunction(string title)
        {
            int hash = 0;
            foreach (char c in title)
            {
                hash = (hash * 31 + c) % movies.Length;
            }
            return hash;
        }

        // Add a movie by Hash
        public void AddMovie(Movie movie)
        {
            int slot = HashFunction(movie.Title);
            if (movies[slot] == null)
            {
                movies[slot] = movie;
                movieCount++;
            }
            else if (movies[slot].Title == movie.Title)
            {
                // If the title is the same, add the number of copies
                movies[slot].AddCopies(movie.TotalCopies);
            }
            else
            {
                // Collision detected, but not handled yet
                Console.WriteLine($"Collision detected for title: {movie.Title} (not handled yet)");
            }
        }

        // Search for a movie by Hash
        public Movie FindMovie(string title)
        {
            int slot = HashFunction(title);
            if (movies[slot] != null && movies[slot].Title == title)
            {
                return movies[slot];
            }
            return null;
        }

        // Delete a movie by Hash
        public bool RemoveMovie(string title)
        {
            int slot = HashFunction(title);
            if (movies[slot] != null && movies[slot].Title == title)
            {
                movies[slot] = null;
                movieCount--;
                return true;
            }
            return false;
        }

        // Display the movie list for testing
        public void ListAllMovies()
        {
            for (int i = 0; i < movies.Length; i++)
            if (movies[i] != null)
            {
                Console.WriteLine(movies[i]);
            }
        }
    }
}
