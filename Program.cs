using System;
using DVDLibraryManager;

class Program
{
    static MovieCollection movieCollection = new MovieCollection();
    static MemberCollection memberCollection = new MemberCollection();

    static void Main(string[] args)
    {
        InitializeTestData(); // Test data for development

        LoginHandler loginHandler = new LoginHandler(memberCollection);
        StaffHandler staffHandler = new StaffHandler(movieCollection, memberCollection);
        MemberHandler memberHandler = new MemberHandler(movieCollection, memberCollection);

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
                    if (loginHandler.StaffLogin())
                    {
                        staffHandler.RunStaffMenu();
                    }
                    break;
                case "2":
                    Member loggedInMember = loginHandler.MemberLogin();
                    if (loggedInMember != null)
                    {
                        memberHandler.RunMemberMenu(loggedInMember);
                    }
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


    // Test case
    static void InitializeTestData()
    {
        // Register Movie
        movieCollection.AddMovie(new Movie("Titanic", Genre.Drama, Classification.M15Plus, 60, 5, 5));
        movieCollection.AddMovie(new Movie("Inception", Genre.SciFi, Classification.M15Plus, 90, 3, 3));

        // Register Member
        memberCollection.AddMember(new Member("Test", "User1", "0123456789", "0000"));
        memberCollection.AddMember(new Member("Test", "User2", "1234567890", "0000"));
        memberCollection.AddMember(new Member("hoge", "hoge", "0000000000", "0000"));
    }

}