using System;

namespace DVDLibraryManager
{
    public static class MemberMenuView
    {
      public static void Show()
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

}