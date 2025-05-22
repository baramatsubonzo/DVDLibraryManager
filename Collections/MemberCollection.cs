using System;

namespace DVDLibraryManager
{
    public class MemberCollection
    {
        private const int MAX_MEMBERS = 1000; // Set to 1000 to ensure enough member slots
        private Member[] members;
        private int memberCount;

        public MemberCollection()
        {
            members = new Member[MAX_MEMBERS];
            memberCount = 0;
        }

        public bool AddMember(Member member)
        {
            // Check if the collection is full
            if (memberCount >= members.Length)
            {
                Console.WriteLine("Member collection is full. Cannot add new member.");
                return false;
            }

            // Linear search
            // Check if member with the same name already exists.
            for (int i = 0; i < memberCount; i++)
            {
                if (members[i].FirstName == member.FirstName && members[i].LastName == member.LastName)
                {
                    Console.WriteLine("Member with the same name already exists.");
                    return false; // Member already exists
                }
            }

            members[memberCount] = member;
            memberCount++;
            return true;
        }

        // Need to return the actual Member data
        public Member FindMember(string firstName, string lastName)
        {
            // Linear search for the member
            for (int i = 0; i < memberCount; i++)
            {
                if (members[i].FirstName == firstName && members[i].LastName == lastName)
                {
                    return members[i]; // Member found
                }
            }
            return null; // Member not found
        }


        // For login method
        public Member FindMemberByPassword(string firstName, string lastName, string password)
        {
            Member member = FindMember(firstName, lastName);
            if (member != null)
            {
                if (member.Password == password)
                {
                    return member;
                }
            }
            return null; // Member not found or password incorrect
        }


        //Only need to know if the deletion succeeded, so use Bool
        public bool RemoveMember(string firstName, string lastName)
        {
            int foundIndex = -1;
            // Linear search for the member's index
            for (int i = 0; i < memberCount; i++)
            {
                if (members[i].FirstName == firstName && members[i].LastName == lastName)
                {
                    foundIndex = i;
                    break;
                }
            }

            if (foundIndex != -1)
            {
                if (members[foundIndex].HasBorrowedMovies())
                {
                    Console.WriteLine($"Member {firstName} {lastName} is currently borrowing movies and cannot be removed.");
                    return false;
                }

                // Remove member by replacing with the last member and decrementing memberCount.
                members[foundIndex] = members[memberCount - 1];
                members[memberCount - 1] = null; // Clear the last element's original position
                memberCount--;
                return true;
            }
            return false; // Member not found
        }

        public Member[] GetMembersWithMovie(string movieTitle)
        {
            int count = 0;
            // 1. Count matching members (iterate up to memberCount)
            for (int i = 0; i < memberCount; i++)
            {
                if (members[i] != null && Array.Exists(members[i].GetCurrentBorrowedMovies(), title => title == movieTitle))
                {
                    count++;
                }
            }

            Member[] result = new Member[count];
            if (count == 0)
            {
                return result; // Return empty array if no members found
            }

            int index = 0;
            // 2. Collect matching members
            for (int i = 0; i < memberCount; i++)
            {
                if (members[i] != null && Array.Exists(members[i].GetCurrentBorrowedMovies(), title => title == movieTitle))
                {
                    result[index++] = members[i];
                }
            }
            return result;
        }

        public void ListAllMembers()
        {
            Console.WriteLine("\n--- Registered Members ---");
            if (memberCount == 0)
            {
                Console.WriteLine("No members registered in the system.");
                return;
            }
            for (int i = 0; i < memberCount; i++)
            {
                Console.WriteLine(members[i].ToString());
            }
            Console.WriteLine("--------------------------");
        }
    }

}
