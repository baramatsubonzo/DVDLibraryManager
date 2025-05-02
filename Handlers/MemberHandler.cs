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
                        HandleBorrowMovie();
                        break;
                    case "4":
                        HandleReturnMovie();
                        break;
                    case "5":
                        HandleListBorrowedMoviews();
                        break;
                    case "6":
                        Console.WriteLine("Display the top 3 movies rented by members selected.");
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

        private void HandleBorrowMovie()
        {
            Console.WriteLine("Borrow a movie DVD selected.");

            // Confirm whether member or not
            Console.Write("Enter your first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter your last name: ");
            string lastName = Console.ReadLine();

            Member member = memberCollection.FindMember(firstName, lastName);
            if (member == null)
            {
                Console.WriteLine("Member not found.");
                return;
            }

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
            else if (!member.BorrowMovie(movieTitle))
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

        private void HandleReturnMovie()
        {
            Console.WriteLine("Return a movie DVD selected.");
            Console.Write("Enter your first name: ");
            string returnFirstName = Console.ReadLine();
            Console.Write("Enter your last name: ");
            string returnLastName = Console.ReadLine();

            Member returnMember = memberCollection.FindMember(returnFirstName, returnLastName);
            if (returnMember == null)
            {
                Console.WriteLine("Member not found.");
                return;
            }

            Console.Write("Enter movie title to return: ");
            string returnMovieTitle = Console.ReadLine();

            if (!returnMember.ReturnMovie(returnMovieTitle))
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
        private void HandleListBorrowedMoviews()
        {
            Console.WriteLine("List current borrowing movies selected.");
            Console.WriteLine("=== List Your Borrowed Movies ===");

            Console.Write("Enter your first name: ");
            string memberFirstName = Console.ReadLine();
            Console.Write("Enter your last name: ");
            string memberLastName = Console.ReadLine();

            Member currentMember = memberCollection.FindMember(memberFirstName, memberLastName);

            if (currentMember != null)
            {
                string[] borrowed = currentMember.GetCurrentBorrowedMovies();

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
            else
            {
                Console.WriteLine("Member not found.");
            }
        }
    }
}