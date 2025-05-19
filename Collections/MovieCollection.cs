using System;

namespace DVDLibraryManager
{
    public class MovieCollection
    {
        /* ---------- 内部構造 ---------- */
        private enum SlotState { Empty, Occupied, Deleted }

        private class Bucket
        {
            public string   Key;    // タイトル
            public Movie    Value;  // Movie オブジェクト
            public SlotState State;
        }

        private readonly Bucket[] table;
        private int movieCount;

        public MovieCollection(int capacity = 1000)
        {
            table = new Bucket[capacity];
            for (int i = 0; i < capacity; i++)
                table[i] = new Bucket { State = SlotState.Empty };
        }

        /* ---------- ハッシュ関数 ---------- */
        private int Hash(string title)
        {
            int h = 0;
            foreach (char c in title)
                h = (h * 31 + c) % table.Length;
            return h;
        }

        /* ---------- バケット探索（線形） ---------- */
        private int Find(string title, bool forInsert)
        {
            int start = Hash(title), idx = start;

            do
            {
                Bucket b = table[idx];

                if (b.State == SlotState.Empty)
                    return forInsert ? idx : -1;

                if (b.State == SlotState.Occupied && b.Key == title)
                    return idx;

                if (b.State == SlotState.Deleted && forInsert)
                    return idx;

                idx = (idx + 1) % table.Length;
            }
            while (idx != start);

            return -1; // 満杯
        }

        /* ---------- 追加 ---------- */
        public void AddMovie(Movie m)
        {
            int idx = Find(m.Title, true);
            if (idx == -1) { Console.WriteLine("Table full"); return; }

            Bucket b = table[idx];

            if (b.State == SlotState.Occupied)
            {
                b.Value.AddCopies(m.TotalCopies); // 既にある → コピー数追加
            }
            else
            {
                table[idx] = new Bucket
                {
                    Key   = m.Title,
                    Value = m,
                    State = SlotState.Occupied
                };
                movieCount++;
            }
        }

        /* ---------- 検索 ---------- */
        public Movie FindMovie(string title)
        {
            int idx = Find(title, false);
            return idx == -1 ? null : table[idx].Value;
        }

        /* ---------- 削除 ---------- */
        public bool RemoveMovie(string title)
        {
            int idx = Find(title, false);
            if (idx == -1) return false;

            table[idx].Value = null;
            table[idx].State = SlotState.Deleted;
            movieCount--;
            return true;
        }

        /* ---------- 一覧取得 ---------- */
        public Movie[] GetAllMovies()
        {
            Movie[] res = new Movie[movieCount];
            int k = 0;
            foreach (var b in table)
                if (b.State == SlotState.Occupied)
                    res[k++] = b.Value;

            Array.Sort(res, (a, b) => a.Title.CompareTo(b.Title));
            return res;
        }

        public int Count => movieCount;
    }
}
