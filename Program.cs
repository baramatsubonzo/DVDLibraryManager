using System;
using DVDLibraryManager;

class Program
{
    static MovieCollection movieCollection = new MovieCollection();
    static MemberCollection memberCollection = new MemberCollection();

    static void Main(string[] args)
    {
        // TODO: Remove later
        InitializeTestData(); // Test data for development

        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            MainMenuView.Show();

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    ShowStaffMenuLoop();
                    break;
                case "2":
                    ShowMemberMenuLoop();
                    break;
                case "0":
                    Console.WriteLine("Exiting the program...");
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }

    static void ShowStaffMenuLoop()
    {
        bool backToMain = false;
        while (!backToMain)
        {
            Console.Clear();
            ShowStaffMenu();
            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
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
                    break;
                case "2":
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
                    break;
                case "3":
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
                    break;
                case "4":
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
                    break;
                case "5":
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

                    break;
                case "6":
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

    static void ShowStaffMenu()
    {
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine("Staff Menu");
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine();
        Console.WriteLine("1. Add DVDs to system");
        Console.WriteLine("2. Remove DVDs from system");
        Console.WriteLine("3. Register a new member to system");
        Console.WriteLine("4. Remove a registered member from system");
        Console.WriteLine("5. Find a member contact phone number, given the member's name");
        Console.WriteLine("6. Find members who are currently renting a particular movie");
        Console.WriteLine("0. Return to main menu");
        Console.WriteLine();
        Console.Write("Enter your choice ==> ");
    }

    static void ShowMemberMenuLoop()
    {
        bool backToMain = false;
        while (!backToMain)
        {
            Console.Clear();
            ShowMemberMenu();
            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Browse all the movies selected.");
                    Console.WriteLine("=== Movie List ===");
                    Movie[] allMovies = movieCollection.GetAllMovies();
                    foreach (var movieItem in allMovies)
                    {
                        Console.WriteLine(movieItem);
                    }
                    break;
                case "2":
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

                    break;
                case "3":
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
                        break;
                    }

                    // Confirm the movie
                    Console.Write("Enter movie title to borrow: ");
                    string movieTitle = Console.ReadLine();

                    Movie movie = movieCollection.FindMovie(movieTitle);
                    if (movie == null)
                    {
                        Console.WriteLine("Movie not found.");
                        break;
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

                    break;
                case "4":
                    Console.WriteLine("Return a movie DVD selected.");
                    Console.Write("Enter your first name: ");
                    string returnFirstName = Console.ReadLine();
                    Console.Write("Enter your last name: ");
                    string returnLastName = Console.ReadLine();

                    Member returnMember = memberCollection.FindMember(returnFirstName, returnLastName);
                    if (returnMember == null)
                    {
                        Console.WriteLine("Member not found.");
                        break;
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

                    break;

                case "5":
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

    static void ShowMemberMenu()
    {
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine("Member Menu");
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine();
        Console.WriteLine("1. Browse all the movies");
        Console.WriteLine("2. Display all the information about a movie, given the title of the movie");
        Console.WriteLine("3. Borrow a movie DVD");
        Console.WriteLine("4. Return a movie DVD");
        Console.WriteLine("5. List current borrowing movies");
        Console.WriteLine("6. Display the top 3 movies rented by the members");
        Console.WriteLine("0. Return to main menu");
        Console.WriteLine();
        Console.Write("Enter your choice ==> ");
    }

    // TODO: Remove later
    static void InitializeTestData()
    {
        // 映画登録
        movieCollection.AddMovie(new Movie("Titanic", "Romance", "M15+", 60, 5, 5));
        movieCollection.AddMovie(new Movie("Inception", "Sci-Fi", "M15+", 90, 3, 3));

        // 会員登録
        memberCollection.AddMember(new Member("Test", "User1", "000111222", "password"));
        memberCollection.AddMember(new Member("Test", "User2", "000111333", "password"));
    }

}