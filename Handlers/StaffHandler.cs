using System;
using System.Diagnostics;

namespace DVDLibraryManager
{
    public class StaffHandler
    {
        private MovieCollection movieCollection;
        private MemberCollection memberCollection;
        
        public StaffHandler(MovieCollection movieCollection, MemberCollection memberCollection)
        {
            this.movieCollection = movieCollection;
            this.memberCollection = memberCollection;
        }

        public void RunStaffMenu()
        {
            bool backToMain = false;
            while (!backToMain)
            {
                Console.Clear();
                StaffMenuView.Show();
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        HandleAddMovie();
                        break;
                    case "2":
                        HandleRemoveMovie();
                        break;
                    case "3":
                        HandleRegisterMember();
                        break;
                    case "4":
                        HandleRemoveMember();
                        break;
                    case "5":
                        HandleFindMemberPhone();
                        break;
                    case "6":
                        HandleFindMembersWithMovie();
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

        private void HandleAddMovie()
        {
            Console.WriteLine("Add DVDs to system selected.");

            // Title input
            string title = EnterMovieTitle();
            // Genre input
            Genre genre = SelectGenre();
            // Classification input
            Classification classification = SelectClassification();
            // Duration input
            int duration = EnterValidDuration();
            // Copies input
            int total_copies = EnterValidCopies();
            int available_copies = total_copies;

            Movie newMovie = new Movie(title, genre, classification, duration, available_copies, total_copies);
            movieCollection.AddMovie(newMovie);

            Console.WriteLine("Movie added successfully!");
            Console.WriteLine("=== Current Movies in Collection ===");
            movieCollection.GetAllMovies();
        }

        private void HandleRemoveMovie()
        {
          Console.WriteLine("Remove DVDs from system selected.");
          Console.Write("Enter movie title to remove: ");
          string titleToRemove = Console.ReadLine();

          bool isMovieRemoved = movieCollection.RemoveMovie(titleToRemove);

          if (isMovieRemoved)
          {
            Console.WriteLine("Movie removed successfully!");
          }
          else
          {
            Console.WriteLine("Movie not found or could not be removed.");
          }
        }

        private void HandleRegisterMember()
        {
          Console.WriteLine("Register a new member selected.");

          string firstName = EnterValidFirstName();
          string lastName = EnterValidLastName();
          string phoneNumber = EnterValidPhoneNumber();
          string password = EnterValidPassword();

          Member newMember = new Member(firstName, lastName, phoneNumber, password);

          bool added = memberCollection.AddMember(newMember);

          if (added)
          {
            Console.WriteLine("Member registered successfully!");
          }
          else
          {
            Console.WriteLine("Member already exists or collection is full.");
          }
          // Show the member
          memberCollection.ListAllMembers();
        }

        private void HandleRemoveMember()
        {
          Console.WriteLine("Remove a registered member selected.");
          Console.Write("Enter member's first name: ");
          string removeFirstName = Console.ReadLine();

          Console.Write("Enter member's last name: ");
          string removeLastName = Console.ReadLine();

          bool isMemberRemoved = memberCollection.RemoveMember(removeFirstName, removeLastName);

          if (isMemberRemoved)
          {
            Console.WriteLine("Member removed successfully!");
          }
          else
          {
            Console.WriteLine("Failed to remove member. They may not exist or are still borrowing DVDs.");
          }
          // Show the member
          memberCollection.ListAllMembers();
        }

        private void HandleFindMemberPhone()
        {
          Console.WriteLine("Find a member contact phone number selected.");
          Console.Write("Enter first name: ");
          string searchFirstName = Console.ReadLine();

          Console.Write("Enter last name: ");
          string searchLastName = Console.ReadLine();

          Member foundMember = memberCollection.FindMember(searchFirstName, searchLastName);

          if (foundMember != null)
          {
            Console.WriteLine($"Phone number for {searchFirstName} {searchLastName}: {foundMember.PhoneNumber}");
          }
          else
          {
            Console.WriteLine("Member not found.");
          }
        }

        private void HandleFindMembersWithMovie()
        {
          Console.WriteLine("Find members currently renting a movie selected.");
          Console.Write("Enter movie title to search: ");
          string searchTitle = Console.ReadLine();
          Member[] borrowingMembers = memberCollection.GetMembersWithMovie(searchTitle);

          Console.WriteLine($"\n=== Members Borrowing '{searchTitle}' ===");
          if (borrowingMembers.Length == 0)
          {
            Console.WriteLine("No members are currently borrowing this movie.");
          }
          else
          {
          foreach (var member in borrowingMembers)
            {
              Console.WriteLine($"Name: {member.FirstName} {member.LastName}, Phone: {member.PhoneNumber}");
            }
          }
        }

        private string EnterMovieTitle()
        {
            Console.WriteLine("Enter movie title: ");
            string title = Console.ReadLine()?.Trim();

            while (title == null || title == "")
            {
                Console.WriteLine("Title cannot be empty. Please enter a valid title.");
                Console.Write("Enter movie title: ");
                title = Console.ReadLine()?.Trim();
            }

            return title;
        }

        private Genre SelectGenre()
        {
            bool firstAttempt = true; // Display the full genre list only on the first attempt

            while (true)
            {
                // Display the full genre list only on the first attempt
                if (firstAttempt)
                {
                  Console.WriteLine("Select genre:");
                  Console.WriteLine("1. Drama");
                  Console.WriteLine("2. Adventure");
                  Console.WriteLine("3. Family");
                  Console.WriteLine("4. Action");
                  Console.WriteLine("5. SciFi");
                  Console.WriteLine("6. Comedy");
                  Console.WriteLine("7. Animated");
                  Console.WriteLine("8. Thriller");
                  Console.WriteLine("9. Other");
                  firstAttempt = false;
                }
                Console.Write("Enter number (1-9): ");

                if (int.TryParse(Console.ReadLine(), out int genreChoice) && genreChoice >= 1 && genreChoice <= 9)
                {
                    return genreChoice switch
                    {
                        1 => Genre.Drama,
                        2 => Genre.Adventure,
                        3 => Genre.Family,
                        4 => Genre.Action,
                        5 => Genre.SciFi,
                        6 => Genre.Comedy,
                        7 => Genre.Animated,
                        8 => Genre.Thriller,
                        9 => Genre.Other
                    };
                }
                else
                {
                    Console.WriteLine("Invalid genre selection. Please try again.");
                }
            }
        }

        private Classification SelectClassification()
        {
            bool firstAttempt = true; // Display the full genre list only on the first attempt
            while (true)
            {
                // Display the full genre list only on the first attempt
                if (firstAttempt)
                {
                  Console.WriteLine("Select classification:");
                  Console.WriteLine("1. G");
                  Console.WriteLine("2. PG");
                  Console.WriteLine("3. M15+");
                  Console.WriteLine("4. MA15+");
                  firstAttempt = false;
                }
                Console.Write("Enter number (1-4): ");

                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 4)
                {
                    return choice switch
                    {
                        1 => Classification.G,
                        2 => Classification.PG,
                        3 => Classification.M15Plus,
                        4 => Classification.MA15Plus
                    };
                }
                else
                {
                    Console.WriteLine("Invalid selection. Please enter a number between 1 and 4.");
                }
            }
        }

        private int EnterValidDuration()
        {
          int duration = 0;
          while (true)
          {
            Console.Write("Enter duration (in minutes): ");
            string input = Console.ReadLine();

            // According to assignment, negative value disallowd.
            // Tentative max duration is 600 minutes. No formal uppter limit.
            if (int.TryParse(input, out duration) && duration > 0 && duration <=600)
            {
              return duration;
            }
            else
            {
              Console.WriteLine("Invalid input. Please enter a positive number (max 600 minutes).");
            }
          }
        }

        private int EnterValidCopies()
        {
          int copies = 0;
          while (true)
          {
            Console.Write("Enter number of copies: ");
            string input = Console.ReadLine();

            // According to assignment, negative value disallowd.
            // Tentative max copies is 100. No formal uppter limit.
            if (int.TryParse(input, out copies) && copies > 0 && copies <= 100)
            {
              return copies;
            }
            else
            {
              Console.WriteLine("Invalid input. Please enter a positive number (max 100 copies).");
            }
          }
        }
      private string EnterValidFirstName()
      {
        while (true)
        {
            Console.Write("Enter first name: ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            Console.WriteLine("First name must not be empty.");
        }
      }
      private string EnterValidLastName()
      {
        while (true)
        {
            Console.Write("Enter last name: ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            Console.WriteLine("Last name must not be empty.");
        }
      }

      private string EnterValidPhoneNumber()
      {
        while (true)
        {
            Console.Write("Enter phone number (10 digits): ");
            string input = Console.ReadLine();
            if (input.Length == 10 && input.All(char.IsDigit))
            {
                return input;
            }
        Console.WriteLine("Phone number must be exactly 10 digits (numbers only).");
        }
      }
      private string EnterValidPassword()
      {
        while (true)
        {
            Console.Write("Enter password (4 digits): ");
            string input = Console.ReadLine();
            if (input.Length == 4 && input.All(char.IsDigit))
            {
                return input;
            }
        Console.WriteLine("Password must be exactly 4 digits (numbers only).");
        }
      }

    }
}