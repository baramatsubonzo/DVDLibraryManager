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

        // Find an available slot with linear probing
        private int FindSlot(string title)
        {
            int hash = HashFunction(title);
            int originalHash = hash;

            while (movies[hash] != null && movies[hash].Title != title)
            {
                hash = (hash + 1) % movies.Length;
                if (hash == originalHash)
                {
                    // If no available slot is found (hash table is full)
                    return -1;
                }
            }
            return hash;
        }

        // Add a movie by Hash
        public void AddMovie(Movie movie)
        {
            int slot = FindSlot(movie.Title);
            if (slot == -1)
            {
                Console.WriteLine("Movie collection is full!");
                return;
            }

            if (movies[slot] == null)
            {
                movies[slot] = movie;
                movieCount++;
            }
            else
            {
                // If the title is the same, add the number of copies
                movies[slot].AddCopies(movie.TotalCopies);
            }
        }

        // Search for a movie by Hash
        public Movie FindMovie(string title)
        {
            int slot = FindSlot(title);
            // Check if an available slot was found by `slot !=-1`
            if (slot != -1 && movies[slot] != null && movies[slot].Title == title)
            {
                return movies[slot];
            }
            return null;
        }

        // Delete a movie by Hash
        public bool RemoveMovie(string title)
        {
            int slot = FindSlot(title);
            // Check if an available slot was found by `slot !=1`
            if (slot !=-1 && movies[slot] != null && movies[slot].Title == title)
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
