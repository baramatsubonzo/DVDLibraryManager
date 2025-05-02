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


    // TODO: Remove later
    static void InitializeTestData()
    {
        // 映画登録
        movieCollection.AddMovie(new Movie("Titanic", Genre.Drama, Classification.M15Plus, 60, 5, 5));
        movieCollection.AddMovie(new Movie("Inception", Genre.SciFi, Classification.M15Plus, 90, 3, 3));

        // 会員登録
        memberCollection.AddMember(new Member("Test", "User1", "000111222", "password"));
        memberCollection.AddMember(new Member("Test", "User2", "000111333", "password"));
    }

}