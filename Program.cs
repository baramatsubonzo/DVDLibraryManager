using System;
using DVDLibraryManager;

class Program
{
    static MovieCollection movieCollection = new MovieCollection();
    static MemberCollection memberCollection = new MemberCollection();

    static void Main(string[] args)
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            ShowMainMenu();

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

    static void ShowMainMenu()
    {
        Console.WriteLine("==============================================");
        Console.WriteLine("COMMUNITY LIBRARY MOVIE DVD MANAGEMENT SYSTEM");
        Console.WriteLine("==============================================");
        Console.WriteLine();
        Console.WriteLine("Main Menu");
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("Select from the following:");
        Console.WriteLine();
        Console.WriteLine("1. Staff");
        Console.WriteLine("2. Member");
        Console.WriteLine("0. End the program");
        Console.WriteLine();
        Console.Write("Enter your choice ==> ");
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
                    foreach (var movie in allMovies)
                    {
                        Console.WriteLine(movie);
                    }
                    break;
                case "2":
                    Console.WriteLine("Display information about a movie selected.");
                    break;
                case "3":
                    Console.WriteLine("Borrow a movie DVD selected.");
                    break;
                case "4":
                    Console.WriteLine("Return a movie DVD selected.");
                    break;
                case "5":
                    Console.WriteLine("List current borrowing movies selected.");
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
}


// // MEMO: Operation test for Member class
// // TODO: Remove later
// // class Program
// // {
// //     static void Main(string[] args)
// //     {
// //         // === Beginning of test code ===
// //         Member member = new Member("John", "Doe", "0123456789", "1234");

// //         Console.WriteLine("Borrowing movies...");
// //         member.BorrowMovie("Inception");
// //         member.BorrowMovie("Interstellar");
// //         member.BorrowMovie("The Dark Knight");

// //         Console.WriteLine("\nCurrent Borrowed Movies:");
// //         foreach (string movie in member.GetCurrentBorrowedMovies())
// //         {
// //             Console.WriteLine(movie);
// //         }

// //         Console.WriteLine("\nReturning 'Interstellar'...");
// //         member.ReturnMovie("Interstellar");

// //         Console.WriteLine("\nCurrent Borrowed Movies After Return:");
// //         foreach (string movie in member.GetCurrentBorrowedMovies())
// //         {
// //             Console.WriteLine(movie);
// //         }

// //         Console.WriteLine("\nTest completed!");
// //         // === end of test code ===
// //     }
// // }

// TODO: Remove later, just test for movie collection
// class Program
// {
//     static void Main(string[] args)
//     {
//         MovieCollection movieCollection = new MovieCollection();

//         // テスト用Movieを作る
//         Movie movie1 = new Movie("Inception", "Sci-Fi", "M15+", 148, 5, 5);
//         Movie movie2 = new Movie("Interstellar", "Sci-Fi", "M15+", 169, 3, 3);
//         Movie movie3 = new Movie("The Dark Knight", "Action", "M15+", 152, 7, 7);

//         // 追加テスト
//         Console.WriteLine("=== Add Movie Test ===");
//         movieCollection.AddMovie(movie1);
//         movieCollection.AddMovie(movie2);
//         movieCollection.AddMovie(movie3);

//         Console.WriteLine("\n=== List All Movies ===");
//         movieCollection.ListAllMovies();

//         // 検索テスト
//         Console.WriteLine("\n=== Find Movie Test ===");
//         var foundMovie = movieCollection.FindMovie("Interstellar");
//         if (foundMovie != null)
//         {
//             Console.WriteLine("Found: " + foundMovie);
//         }
//         else
//         {
//             Console.WriteLine("Movie not found.");
//         }

//         // 削除テスト
//         Console.WriteLine("\n=== Remove Movie Test ===");
//         bool removed = movieCollection.RemoveMovie("Inception");
//         Console.WriteLine("Removed Inception? " + (removed ? "Yes" : "No"));

//         Console.WriteLine("\n=== List All Movies After Removal ===");
//         movieCollection.ListAllMovies();

//         Console.WriteLine("\nTest Completed!");
//     }
// }


// TODO: Remove later, just test for member collection
// class Program
// {
//     static void Main(string[] args)
//     {
//         // テスト用MemberCollection作成
//         MemberCollection memberCollection = new MemberCollection();

//         Console.WriteLine("=== Add Members ===");
//         memberCollection.AddMember(new Member("John", "Smith", "12345678", "password1"));
//         memberCollection.AddMember(new Member("Jane", "Doe", "87654321", "password2"));
//         Console.WriteLine("Added John Smith and Jane Doe!");

//         Console.WriteLine("\n=== Find Member ===");
//         Member found = memberCollection.FindMember("John", "Smith");
//         if (found != null)
//         {
//             Console.WriteLine($"Found: {found.FirstName} {found.LastName}");
//         }
//         else
//         {
//             Console.WriteLine("John Smith not found!");
//         }

//         Console.WriteLine("\n=== Remove Member ===");
//         bool removed = memberCollection.RemoveMember("John", "Smith");
//         Console.WriteLine($"Removed John Smith? {(removed ? "Yes" : "No")}");

//         Console.WriteLine("\n=== Try to Find Again ===");
//         found = memberCollection.FindMember("John", "Smith");
//         Console.WriteLine(found == null ? "John Smith not found (as expected)" : "Still found!");

//         Console.WriteLine("\n=== List All Members ===");
//         memberCollection.ListAllMembers();

//         Console.WriteLine("\n=== Test Completed! ===");
//     }
// }
