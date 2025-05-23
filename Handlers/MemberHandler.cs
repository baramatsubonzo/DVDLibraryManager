using System;

namespace DVDLibraryManager
{
    public class MemberHandler
    {
        private MovieCollection movieCollection;
        private MemberCollection memberCollection;

        public MemberHandler(MovieCollection movieCollection, MemberCollection memberCollection)
        {
            this.movieCollection = movieCollection;
            this.memberCollection = memberCollection;
        }

        public void RunMemberMenu(Member loggedInMember)
        {
            bool backToMain = false;
            while (!backToMain)
            {
                Console.Clear();
                Console.WriteLine($"Welcome, {loggedInMember.FirstName}!");
                MemberMenuView.Show();
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        HandleBrowseAllMovies();
                        break;
                    case "2":
                        HandleDisplayMovieInfo();
                        break;
                    case "3":
                        HandleBorrowMovie(loggedInMember);
                        break;
                    case "4":
                        HandleReturnMovie(loggedInMember);
                        break;
                    case "5":
                        HandleListBorrowedMovies(loggedInMember);
                        break;
                    case "6":
                        HandleDisplayTop3Movies();
                        break;
                    case "0":
                        backToMain = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }

                if (!backToMain)
                {
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                }
            }
        }

        private void HandleBrowseAllMovies()
        {
            Console.WriteLine("Browse all the movies selected.");
            Console.WriteLine("=== Movie List ===");
            Movie[] allMovies = movieCollection.GetAllMovies();
            foreach (var movieItem in allMovies)
            {
                Console.WriteLine(movieItem);
            }
        }

        private void HandleDisplayMovieInfo()
        {
            Console.WriteLine("Display information about a movie selected.");
            Console.Write("Enter movie title: ");
            string searchTitle = Console.ReadLine();

            Movie foundMovie = movieCollection.FindMovie(searchTitle);
            if (foundMovie != null)
            {
                Console.WriteLine("=== Movie Information ===");
                Console.WriteLine(foundMovie);
            }
            else
            {
                Console.WriteLine("Movie not found.");
            }
        }

        private void HandleBorrowMovie(Member loggedInMember)
        {
            Console.WriteLine("Borrow a movie DVD selected.");

            // Confirm the movie
            Console.Write("Enter movie title to borrow: ");
            string movieTitle = Console.ReadLine();

            Movie movie = movieCollection.FindMovie(movieTitle);
            if (movie == null)
            {
                Console.WriteLine("Movie not found.");
                return;
            }

            // Add movie borrowing feature with validations
            if (!movie.Borrow())
            {
                Console.WriteLine("This movie is currently unavailable.");
            }
            else if (!loggedInMember.BorrowMovie(movieTitle))
            {
                Console.WriteLine("You have reached your borrowing limit or already borrowed this movie.");
                // restore stock
                movie.Return();
            }
            else
            {
                Console.WriteLine($"Successfully borrowed: {movie.Title}");
            }
        }

        private void HandleReturnMovie(Member loggedInMember)
        {
            Console.WriteLine("Return a movie DVD selected.");

            Console.Write("Enter movie title to return: ");
            string returnMovieTitle = Console.ReadLine();

            if (!loggedInMember.ReturnMovie(returnMovieTitle))
            {
                Console.WriteLine("You have not borrowed this movie.");
            }
            else
            {
                Movie returnMovie = movieCollection.FindMovie(returnMovieTitle);
                if (returnMovie != null)
                {
                    returnMovie.Return();
                    Console.WriteLine($"You have successfully returned: {returnMovie.Title}");
                }
                else
                {
                    Console.WriteLine("Movie not found in collection.");
                }
            }
        }
        private void HandleListBorrowedMovies(Member loggedInMember)
        {
            Console.WriteLine("List current borrowing movies selected.");
            Console.WriteLine("=== List Your Borrowed Movies ===");

            string[] borrowed = loggedInMember.GetCurrentBorrowedMovies();

            if (borrowed.Length == 0)
            {
                Console.WriteLine("You are not currently borrowing any movies.");
            }
            else
            {
                Console.WriteLine("Your borrowed movies:");
                foreach (var title in borrowed)
                {
                    Console.WriteLine($"- {title}");
                }
            }
        }

        private void HandleDisplayTop3Movies()
        {
            Console.WriteLine("=== Top 3 Most Frequently Borrowed Movies ===");
            movieCollection.DisplayTop3Movies();
        }
    }
}