using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PustakaCeria.Menu
{
    internal partial class StatistikPenggunaanBuku
    {

        public class BorrowingData
        {
            public required List<Buku> Books { get; set; }
        }

        public void AnalyzeBorrowingData(BorrowingData data)
        {
            // Calculate total borrow count
            int totalBorrowCount = data.Books.Sum(b => b.BorrowCount);

            // Calculate most popular genre
            var mostPopularGenre = data.Books
               .GroupBy(b => b.Genre)
               .OrderByDescending(g => g.Sum(b => b.BorrowCount))
               .FirstOrDefault();

            // Calculate most popular book
            var mostPopularBook = data.Books
               .OrderByDescending(b => b.BorrowCount)
               .FirstOrDefault();

            // Print statistics
            Console.WriteLine("Total borrow count: " + totalBorrowCount);
            if (mostPopularGenre!= null)
            {
                Console.WriteLine("Most popular genre: " + mostPopularGenre.Key + " with " + mostPopularGenre.Sum(b => b.BorrowCount) + " borrows");
            }
            else
            {
                Console.WriteLine("No books borrowed.");
            }

            if (mostPopularBook!= null)
            {
                Console.WriteLine("Most popular book: " + mostPopularBook.Judul + " with " + mostPopularBook.BorrowCount + " borrows");
            }
            else
            {
                Console.WriteLine("No books borrowed.");
            }
        }
          // Fungsi untuk mencetak statistik peminjaman buku
        public void PrintStatistics(BorrowingData data)
        {
            Console.WriteLine("===== Menu Statistik Penggunaan Buku =====");
            Console.WriteLine();
            // Panggil metode AnalyzeBorrowingData
            AnalyzeBorrowingData(data);
            Console.WriteLine("==========================================");
            Console.WriteLine();
            Console.WriteLine("Tekan tombol apa pun untuk kembali ke menu utama.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
   