using System;
using DVDLibraryManager;

class Program
{
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
                    // TODO: Add Feature
                    // Later I will remove
                    // === Beginning of test code === 
                    // Movie testMovie = new Movie("Hoge", "SF", "M15+", 148, 3, 3);

                    // Console.WriteLine("作ったばかりのMovie:");
                    // Console.WriteLine(testMovie);

                    // bool borrowResult = testMovie.Borrow();
                    // Console.WriteLine("\nBorrowした後:");
                    // Console.WriteLine(testMovie);
                    // Console.WriteLine("Borrow成功した？ " + borrowResult);

                    // testMovie.Return();
                    // Console.WriteLine("\nReturnした後:");
                    // Console.WriteLine(testMovie);
                    // === End of test code === 
                    break;
                case "2":
                    Console.WriteLine("Remove DVDs from system selected.");
                    break;
                case "3":
                    Console.WriteLine("Register a new member selected.");
                    break;
                case "4":
                    Console.WriteLine("Remove a registered member selected.");
                    break;
                case "5":
                    Console.WriteLine("Find a member contact phone number selected.");
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
