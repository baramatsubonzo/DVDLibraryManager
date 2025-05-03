using System;

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

            Console.WriteLine("Enter movie title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Enter genre: ");
            string genre = Console.ReadLine();

            Console.Write("Enter classification (G, PG, M15+, MA15+): ");
            string classification = Console.ReadLine();

            Console.Write("Enter duration (in minutes): ");
            int duration = int.Parse(Console.ReadLine());

            Console.Write("Enter number of copies: ");
            int total_copies = int.Parse(Console.ReadLine());
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

          Console.Write("Enter first name: ");
          string firstName = Console.ReadLine();

          Console.Write("Enter last name: ");
          string lastName = Console.ReadLine();

          Console.Write("Enter phone number: ");
          string phoneNumber = Console.ReadLine();

          Console.Write("Set password: ");
          string password = Console.ReadLine();

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
          // TODO: Later separate logic and display.
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
          // TODO: Later separate logic and display.
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
    }
}