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
        // TODO: Check later if this hash function matches the lecture
        private int HashFunction(string firstName, string lastName)
        {
            string fullName = firstName + lastName;
            int hash = 0;
            foreach (char c in fullName)
            {
                hash = (hash * 31 + c) % members.Length;
            }
            return hash;
        }
        // Find an available slot with linear probing
        private int FindSlot(string firstName, string lastName)
        {
            int hash = HashFunction(firstName, lastName);
            int originalHash = hash;

            // Loop until an empty slot or a matching name is found
            while (members[hash] != null && (members[hash].FirstName != firstName || members[hash].LastName != lastName))
            {
                hash = (hash + 1) % members.Length;
                if (hash == originalHash)
                {
                    // If no available slot is found (hash table is full)
                    return -1;
                }
            }
            return hash;
        }

        public bool AddMember(Member member)
        {
            // Check if the collection is full
            if (memberCount >= members.Length)
            {
                return false;
            }

            // Find slot
            int slot = FindSlot(member.FirstName, member.LastName);
            if (slot == -1)
            {
                return false;
            }

            if (members[slot] == null)
            {
                // If the slot is empty, register the new member there
                members[slot] = member;
                memberCount++;
                return true;
            }
            else
            {
                // If Member with the same name already exists, return false
                return false;
            }
        }

        // Need to return the actual Member data
        public Member FindMember(string firstName, string lastName)
        {
            int slot = FindSlot(firstName, lastName);
            if (slot != -1 && members[slot] != null)
            {
                return members[slot];
            }
            return null;
        }

        // For login method
        public Member FindMemberByPassword(string firstName, string lastName, string password)
        {
            int slot = FindSlot(firstName, lastName);
            if (slot != -1 && members[slot] != null)
            {
                Member member = members[slot];
                if (member.Password == password)
                {
                    return member;
                }
            }
            return null;
        }

        //Only need to know if the deletion succeeded, so use Bool
        public bool RemoveMember(string firstName, string lastName)
        {
            int slot = FindSlot(firstName, lastName);
            if (slot != -1 && members[slot] != null)
            {
                // Deletion not allowed if the member has borrowed movies
                if (members[slot].HasBorrowedMovies())
                {
                    Console.WriteLine("This member cannot be removed because they are currently borrowing movies.");
                    return false;
                }
                members[slot] = null;
                memberCount--;
                return true;
            }
            return false;
        }

        public Member[] GetMembersWithMovie(string movieTitle)
        {
            int count = 0;
            // 1. Count matching members
            for (int i = 0; i < members.Length; i++)
            {
                if (members[i] != null && Array.Exists(members[i].GetCurrentBorrowedMovies(), title => title == movieTitle))
                {
                    count++;
                }
            }
            // 2. Collect matching members
            Member[] result = new Member[count];
            int index = 0;
            for (int i = 0; i < members.Length; i++)
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
            for (int i = 0; i < members.Length; i++)
            {
                if (members[i] != null)
                {
                    Console.WriteLine(members[i]);
                }
            }
        }
    }

}
