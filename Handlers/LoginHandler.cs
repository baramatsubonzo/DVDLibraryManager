using System;

namespace DVDLibraryManager
{
  public class LoginHandler
  {
      private MemberCollection memberCollection;

      public LoginHandler(MemberCollection memberCollection)
      {
          this.memberCollection = memberCollection;
      }

      // Staff login check. Returns true if match.
      public bool StaffLogin()
      {
          Console.Write("Username: ");
          string username = Console.ReadLine();
          Console.Write("Password: ");
          string password = Console.ReadLine();

          // Hard-coded staff login according to assignment requirements
          if (username == "staff" && password == "today123")
          {
              Console.WriteLine("Staff login successful! Press Enter to continue.");
              Console.ReadLine();
              return true;
          }
          else
          {
              Console.WriteLine("Invalid staff. Press Enter to return.");
              Console.ReadLine();
              return false;
          }
      }

      // Member login check. Returns the Member object if match
      public Member MemberLogin()
      {
          Console.Write("First name: ");
          string firstName = Console.ReadLine();
          Console.Write("Last name: ");
          string lastName = Console.ReadLine();
          Console.Write("Password: ");
          string password = Console.ReadLine();

          Member member = memberCollection.FindMemberByPassword(firstName, lastName, password);
          if (member != null)
          {
              Console.WriteLine($"Welcome, {firstName}! Press Enter to continue.");
              Console.ReadLine();
              return member;
          }
          else
          {
              Console.WriteLine("Invalid member. Press Enter to return.");
              Console.ReadLine();
              return null;
          }
      }
  }
}