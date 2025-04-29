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
    }

}
