using System;

namespace DVDLibraryManager
{
    public class MovieCollection
    {
        // To avoid null, I introduced an enum with a default value of Empty (0) for bucket state management.
        private enum BucketState { Empty, Occupied, Deleted }
        private Movie[] movies;
        private BucketState[] states;
        private int movieCount;
        //max 1000
        public MovieCollection(int size = 1000)
        {
            movies = new Movie[size];
            states = new BucketState[size];
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

            do
            {
                if (states[hash] == BucketState.Empty)
                    return -1;
                if (states[hash] == BucketState.Occupied && movies[hash].Title == title)
                    return hash;
                hash = (hash + 1) % movies.Length;
            }
            while (hash != originalHash);
            return -1;
        }

        // For insersion
        private int FindInsertBucket(string title)
        {
            int hash = HashFunction(title);
            int originalHash = hash;
            do
            {
                // Todo: Collision Check later
                if (states[hash] == BucketState.Empty || states[hash] == BucketState.Deleted)
                    return hash;
                if (states[hash] == BucketState.Occupied && movies[hash].Title == title)
                    return hash;
                hash = (hash + 1) % movies.Length;
            }
            while (hash != originalHash);
            return -1;
        }

        // Add a movie by Hash
        public void AddMovie(Movie movie)
        {
            int bucket = FindInsertBucket(movie.Title);
            if (bucket == -1)
            {
                Console.WriteLine("Movie collection is full!");
                return;
            }
            // If it exists, increase the copy count
            if (states[bucket] == BucketState.Occupied)
            {
                movies[bucket].AddCopies(movie.TotalCopies);
            }
            else
            {
                movies[bucket] = movie;
                states[bucket] = BucketState.Occupied;
                movieCount++;
            }
        }

        // Search for a movie
        public Movie FindMovie(string title)
        {
            int bucket = FindBucket(title);

            if (bucket != -1 && states[bucket] == BucketState.Occupied)
            {
                return movies[bucket];
            }
            return null;
        }

        // Delete a movie. Set the Delete flag.
        public bool RemoveMovie(string title)
        {
            int bucket = FindBucket(title);

            if (bucket !=-1 && states[bucket] == BucketState.Occupied)
            {
                movies[bucket] = null;
                states[bucket] = BucketState.Deleted;
                movieCount--;
                return true;
            }
            return false;
        }

        // Returns all movies stored in the collection, alphabetically by title.
        public Movie[] GetAllMovies()
        {
            Movie[] res = new Movie[movieCount];
            int k = 0;
            for (int i = 0; i < movies.Length; i++)
                if (states[i] == BucketState.Occupied)
                    res[k++] = movies[i];
                Array.Sort(res, (a, b) => a.Title.CompareTo(b.Title));
            return res;
        }
    }
}
