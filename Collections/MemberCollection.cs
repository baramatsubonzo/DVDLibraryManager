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
    }

}
