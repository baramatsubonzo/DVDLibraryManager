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

        // Division method
        private int HashFunction(string title)
        {
            int num = 0;
            foreach (char c in title)
                // In C#, adding a char to an int automatically converts the char to its ASCII (Unicode) code.
                num += c;
            return num % table.Length;
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
            int firstEmptyOrDeleted = -1;  // Record the first empty or deleted bucket found

            do
            {
                 // If a movie with the same title already exists, return its bucket
                if (table[hash].State == BucketState.Occupied && table[hash].Key == title)
                    return hash;
                // If an empty or deleted bucket is found
                if (firstEmptyOrDeleted == -1 &&
                    (table[hash].State == BucketState.Empty || table[hash].State == BucketState.Deleted))
                    firstEmptyOrDeleted = hash;

                hash = (hash + 1) % table.Length;
            }
            while (hash != originalHash);

            // 同じタイトルの映画が見つからなかった場合、最初に見つかった空または削除済みのバケットを返す
            return firstEmptyOrDeleted;
        }

        // Add a movie by Hash
        public void AddMovie(Movie movie)
        {
            // First, look for existing titles.
            int existingBucket = FindBucket(movie.Title);
            if (existingBucket != -1 && table[existingBucket].State == BucketState.Occupied)
            {
                // If it exists, increase the copy count.
                table[existingBucket].Value.AddCopies(movie.TotalCopies);
                return;
            }

            // If there is no existing title, find an empty or deleted bucket.
            int insertBucket = FindInsertBucket(movie.Title);
            if (insertBucket == -1)
            {
                Console.WriteLine("Movie collection is full!");
                return;
            }
            table[insertBucket].Key = movie.Title;
            table[insertBucket].Value = movie;
            table[insertBucket].State = BucketState.Occupied;
            movieCount++;
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
