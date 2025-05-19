using System;

namespace DVDLibraryManager
{
    public class MovieCollection
    {
        private Movie[] movies;
        private int movieCount;
        private const int EMPTY = -1;  // 空のスロットのマーカー
        private const int DELETED = -2; // 削除されたスロットのマーカー

        public MovieCollection()
        {
            movies = new Movie[1000]; //max 1000
            movieCount = 0;
            // 初期化時にすべてのスロットをEMPTYに設定
            for (int i = 0; i < movies.Length; i++)
            {
                movies[i] = new Movie(EMPTY.ToString(), Genre.Other, Classification.G, 0, 0, 0);
            }
        }

        // TODO: Check later if this hash function matches the lecture
        private int HashFunction(string title)
        {
            int hash = 0;
            foreach (char c in title)
            {
                hash = (hash * 31 + c) % movies.Length;
            }
            return hash;
        }

        // 二次探査法を使用して利用可能なスロットを見つける
        private int FindSlot(string title)
        {
            int bucket = HashFunction(title);
            int i = 0;
            int offset = 0;
            
            while (i < movies.Length && 
                   movies[(bucket + offset) % movies.Length].Title != EMPTY.ToString() && 
                   movies[(bucket + offset) % movies.Length].Title != DELETED.ToString() && 
                   movies[(bucket + offset) % movies.Length].Title != title)
            {
                i++;
                offset = i * i; // 二次探査法
            }
            
            if (i >= movies.Length) {
                return -1; // テーブルが満杯
            }
            
            return (bucket + offset) % movies.Length;
        }

        // 映画を検索するためのスロットを見つける
        private int FindMovieSlot(string title)
        {
            int bucket = HashFunction(title);
            int i = 0;
            int offset = 0;
            
            while (i < movies.Length && 
                   movies[(bucket + offset) % movies.Length].Title != EMPTY.ToString() && 
                   movies[(bucket + offset) % movies.Length].Title != title)
            {
                i++;
                offset = i * i; // 二次探査法
            }
            
            if (i >= movies.Length || movies[(bucket + offset) % movies.Length].Title == EMPTY.ToString()) {
                return -1; // 見つからない
            }
            
            return (bucket + offset) % movies.Length;
        }

        // 映画をハッシュテーブルに追加
        public void AddMovie(Movie movie)
        {
            if (movieCount >= movies.Length) {
                Console.WriteLine("Movie collection is full!");
                return;
            }

            int slot = FindSlot(movie.Title);
            if (slot == -1)
            {
                Console.WriteLine("Failed to find an available slot!");
                return;
            }

            if (movies[slot].Title == EMPTY.ToString() || movies[slot].Title == DELETED.ToString())
            {
                movies[slot] = movie;
                movieCount++;
            }
            else
            {
                // タイトルが同じ場合、コピー数を追加
                movies[slot].AddCopies(movie.TotalCopies);
            }
        }

        // ハッシュを使用して映画を検索
        public Movie FindMovie(string title)
        {
            int slot = FindMovieSlot(title);
            if (slot != -1)
            {
                return movies[slot];
            }
            return null;
        }

        // ハッシュを使用して映画を削除
        public bool RemoveMovie(string title)
        {
            int slot = FindMovieSlot(title);
            if (slot != -1)
            {
                movies[slot] = new Movie(DELETED.ToString(), Genre.Other, Classification.G, 0, 0, 0); // 削除マーカーとしてDELETEDを使用
                movieCount--;
                return true;
            }
            return false;
        }

        // コレクションに保存されているすべての映画をタイトルのアルファベット順に返します
        public Movie[] GetAllMovies()
        {
            int count = 0;

            // 配列内の有効な映画をカウント
            for (int i = 0; i < movies.Length; i++)
            {
                if (movies[i].Title != EMPTY.ToString() && movies[i].Title != DELETED.ToString())
                {
                    count++;
                }
            }

            // 有効な映画を新しい配列にコピー
            Movie[] result = new Movie[count];
            int index = 0;
            for (int i = 0; i < movies.Length; i++)
            {
                if (movies[i].Title != EMPTY.ToString() && movies[i].Title != DELETED.ToString())
                {
                    result[index++] = movies[i];
                }
            }

            // 結果をアルファベット順にソート
            Array.Sort(result, (a, b) => a.Title.CompareTo(b.Title));

            return result;
        }
    }
}
