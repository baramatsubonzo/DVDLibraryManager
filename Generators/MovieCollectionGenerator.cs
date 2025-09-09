using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDLibraryManager.Generators
{
    public static class MovieCollectionGenerator
    {
        // Generates a list of movies with random data for testing
        public static List<Movie> GenerateRandomMovieList(int n)
        {
            Random rand = new Random();
            List<Movie> list = new List<Movie>();

            for (int i = 0; i < n; i++)
            {
                var movie = new Movie(
                    title: $"Movie_{i + 1}",
                    genre: (Genre)rand.Next(Enum.GetValues(typeof(Genre)).Length),
                    classification: (Classification)rand.Next(Enum.GetValues(typeof(Classification)).Length),
                    duration: rand.Next(60, 181),
                    copies: rand.Next(1, 11),
                    availableCopies: rand.Next(1, 11)
                );
                movie.TotalBorrowedCount = rand.Next(0, 101);
                list.Add(movie);
            }

            return list;
        }
    }
}
