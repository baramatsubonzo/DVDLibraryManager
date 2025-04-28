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

        // Add a movie, simply append to the end
        public void AddMovie(Movie movie)
        {
            if (movieCount >= movies.Length)
            {
                Console.WriteLine("Movie collection is full");
                return;
            }

            movies[movieCount] = movie;
            movieCount++;
        }

        // Search for a movie by title
        public Movie FindMovie(string title)
        {
            for (int i = 0; i < movieCount; i++)
            {
                if (movies[i].Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    return movies[i];
                }
            }
            return null;
        }

        // Delete a movie by title
        public bool RemoveMovie(string title)
        {
            for (int i = 0; i < movieCount; i++)
            {
                if (movies[i].Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    // Linear shift after deletion
                    for (int j = i; j < movieCount - 1; j++)
                    {
                        movies[j] = movies[j + 1];
                    }
                    movies[movieCount - 1] = null;
                    movieCount--;
                    return true;
                }
            }
            return false;
        }

        // Display the movie list for testing
        public void ListAllMovies()
        {
            for (int i = 0; i < movieCount; i++)
            {
                Console.WriteLine(movies[i]);
            }
        }
    }
}
