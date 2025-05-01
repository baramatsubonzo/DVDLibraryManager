using System;

namespace DVDLibraryManager
{
    public static class StaffMenuView
    {
      public static void Show()
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
    }

}