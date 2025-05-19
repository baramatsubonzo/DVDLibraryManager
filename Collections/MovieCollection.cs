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

        // Find an available bucket with linear probing
        private int FindBucket(string title)
        {
            int hash = HashFunction(title);
            int originalHash = hash;

            while (movies[hash] != null && movies[hash].Title != title)
            {
                hash = (hash + 1) % movies.Length;
                if (hash == originalHash)
                {
                    // If no available bucket is found (hash table is full)
                    return -1;
                }
            }
            return hash;
        }

        // Add a movie by Hash
        public void AddMovie(Movie movie)
        {
            int bucket = FindBucket(movie.Title);
            if (bucket == -1)
            {
                Console.WriteLine("Movie collection is full!");
                return;
            }

            if (movies[bucket] == null)
            {
                movies[bucket] = movie;
                movieCount++;
            }
            else
            {
                // If the title is the same, add the number of copies
                movies[bucket].AddCopies(movie.TotalCopies);
            }
        }

        // Search for a movie by Hash
        public Movie FindMovie(string title)
        {
            int bucket = FindBucket(title);
            // Check if an available bucket was found by `bucket !=-1`
            if (bucket != -1 && movies[bucket] != null && movies[bucket].Title == title)
            {
                return movies[bucket];
            }
            return null;
        }

        // Delete a movie by Hash
        public bool RemoveMovie(string title)
        {
            int bucket = FindBucket(title);
            // Check if an available bucket was found by `bucket !=1`
            if (bucket !=-1 && movies[bucket] != null && movies[bucket].Title == title)
            {
                movies[bucket] = null;
                movieCount--;
                return true;
            }
            return false;
        }

        // Returns all movies stored in the collection, alphabetically by title.
        public Movie[] GetAllMovies()
        {
            int count = 0;

            // Count movies in the array without null
            for (int i = 0; i < movies.Length; i++)
            {
                if (movies[i] != null)
                {
                    count++;
                }
            }

            // Copy valid movies into new array
            Movie[] result = new Movie[count];
            int index = 0;
            for (int i = 0; i < movies.Length; i++)
            {
                if (movies[i] != null)
                {
                    result[index++] = movies[i];
                }
            }

            // Sort result alphabetically
            Array.Sort(result, (a, b) => a.Title.CompareTo(b.Title));

            return result;
        }
    }
}
