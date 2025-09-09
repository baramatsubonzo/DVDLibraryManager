using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVDLibraryManager.Generators;

namespace DVDLibraryManager.Handlers
{
    public class TopMoviesExtractor
    {
        // Runs the empirical analysis for input sizes from 1000 to 20000
        public void RunTest()
        {
            Console.WriteLine("Running ExtractTop3 Empirical Test...");

            for (int size = 1000; size <= 20000; size += 1000)
            {
                var movies = MovieCollectionGenerator.GenerateRandomMovieList(size);
                int count = ExtractTop3CountOnly(movies);
                Console.WriteLine($"Input size: {size}, Comparison count: {count}");
            }
            // Pause to allow the user to view the results
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        // Extracts top 3 movies using heap and returns only the comparison count
        public int ExtractTop3CountOnly(List<Movie> movies)
        {
            int count = 0;
            int n = movies.Count;

            // Create array with dummy element at index 0 to simulate 1-based indexing
            var heap = new Movie[n + 1];
            for (int i = 0; i < n; i++)
                heap[i + 1] = movies[i];

            // Build Max-Heap
            for (int i = n / 2; i >= 1; i--)
                Heapify(heap, i, n, ref count);

            // Extract top 3 elements from the heap
            for (int i = 0; i < 3 && n > 0; i++)
            {
                Swap(heap, 1, n);
                n--;
                Heapify(heap, 1, n, ref count);
            }

            return count;
        }

        // Maintains the Max-Heap property and increments count on comparisons
        private void Heapify(Movie[] heap, int i, int heapSize, ref int count)
        {
            int left = 2 * i;
            int right = 2 * i + 1;
            int largest = i;

            if (left <= heapSize && ++count > 0 && heap[left].TotalBorrowedCount > heap[largest].TotalBorrowedCount)
                largest = left;

            if (right <= heapSize && ++count > 0 && heap[right].TotalBorrowedCount > heap[largest].TotalBorrowedCount)
                largest = right;

            if (largest != i)
            {
                Swap(heap, i, largest);
                Heapify(heap, largest, heapSize, ref count);
            }
        }

        // Swaps two elements in the heap
        private void Swap(Movie[] arr, int i, int j)
        {
            Movie temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}
