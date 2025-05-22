using System;

namespace DVDLibraryManager
{
    public class MovieCollection
    {
        // To avoid null, I introduced an enum with a default value of Empty (0) for bucket state management.
        private enum BucketState { Empty, Occupied, Deleted }

        // Although only key-value pairs are required by the assignment,
        // this struct also includes a state field (Empty, Occupied, Deleted)
        // to improve maintainability and support future extensions.
        private struct Bucket
        {
            public string Key; // movie title
            public Movie Value; // movie object
            public BucketState State;
        }
        private Bucket[] table;
        private int movieCount;
        //max 1000
        public MovieCollection(int size = 1000)
        {
            // Default value of BucketState is Empty(0)
            table = new Bucket[size];
            movieCount = 0;
        }

        // TODO: Check later if this hash function matches the lecture
        private int HashFunction(string title)
        {
            int hash = 0;
            foreach (char c in title)
            {
                hash = (hash * 31 + c) % table.Length;
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
                if (table[hash].State == BucketState.Empty)
                    return -1;
                if (table[hash].State == BucketState.Occupied && table[hash].Key == title)
                    return hash;
                hash = (hash + 1) % table.Length;
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
                if (table[hash].State == BucketState.Empty || table[hash].State == BucketState.Deleted)
                    return hash;
                if (table[hash].State == BucketState.Occupied && table[hash].Key == title)
                    return hash;
                hash = (hash + 1) % table.Length;
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
            if (table[bucket].State == BucketState.Occupied)
            {
                table[bucket].Value.AddCopies(movie.TotalCopies);
            }
            else
            {
                table[bucket].Key = movie.Title;
                table[bucket].Value = movie;
                table[bucket].State = BucketState.Occupied;
                movieCount++;
            }
        }

        // Search for a movie
        public Movie FindMovie(string title)
        {
            int bucket = FindBucket(title);

            if (bucket != -1 && table[bucket].State == BucketState.Occupied)
            {
                return table[bucket].Value;
            }
            return null;
        }

        // Delete a movie. Set the Delete flag.
        public bool RemoveMovie(string title)
        {
            int bucket = FindBucket(title);

            if (bucket !=-1 && table[bucket].State == BucketState.Occupied)
            {
                table[bucket].Key = null;
                table[bucket].Value = null;
                table[bucket].State = BucketState.Deleted;
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
            for (int i = 0; i < table.Length; i++)
                if (table[i].State == BucketState.Occupied)
                    res[k++] = table[i].Value;
                Array.Sort(res, (a, b) => a.Title.CompareTo(b.Title));
            return res;
        }
    }
}
