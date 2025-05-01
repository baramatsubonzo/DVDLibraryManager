using System;

namespace DVDLibraryManager
{
    public static class MainMenuView
    {
        public static void Show()
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
    }

}