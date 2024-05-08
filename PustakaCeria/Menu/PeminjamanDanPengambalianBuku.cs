namespace PustakaCeria.Menu
{
    public class Buku
    {
        public required string Judul { get; set; }
        public bool Dipinjam { get; set; }
    }
    public class PeminjamanDanPengambalianBuku
    {
        private List<Buku> daftarBuku = new List<Buku>();

        public void TambahBuku(string judul)
        {
            daftarBuku.Add(new Buku { Judul = judul, Dipinjam = false });
        }

        public void PinjamBuku(string judul)
        {
            Buku buku = CariBuku(judul);
            if (buku != null)
            {
                if (!buku.Dipinjam)
                {
                    buku.Dipinjam = true;
                    Console.WriteLine($"Buku \"{judul}\" berhasil dipinjam.");
                }
                else
                {
                    Console.WriteLine($"Maaf, buku \"{judul}\" sudah dipinjam.");
                }
            }
            else
            {
                Console.WriteLine($"Maaf, buku \"{judul}\" tidak ditemukan.");
            }
        }

        public void KembalikanBuku(string judul)
        {
            Buku buku = CariBuku(judul);
            if (buku != null)
            {
                if (buku.Dipinjam)
                {
                    buku.Dipinjam = false;
                    Console.WriteLine($"Buku \"{judul}\" berhasil dikembalikan.");
                }
                else
                {
                    Console.WriteLine($"Maaf, buku \"{judul}\" tidak sedang dipinjam.");
                }
            }
            else
            {
                Console.WriteLine($"Maaf, buku \"{judul}\" tidak ditemukan.");
            }
        }

        private Buku CariBuku(string judul)
        {
            return daftarBuku.Find(b => b.Judul == judul);
        }

        public static void Main(string[] args)
        {
            PeminjamanDanPengambalianBuku perpustakaan = new PeminjamanDanPengambalianBuku();
            perpustakaan.TambahBuku("Harry Potter");
            perpustakaan.TambahBuku("Lord of the Rings");

            perpustakaan.PinjamBuku("Harry Potter");
            perpustakaan.PinjamBuku("Lord of the Rings");
            perpustakaan.PinjamBuku("Lord of the Rings");

            perpustakaan.KembalikanBuku("Harry Potter");
            perpustakaan.KembalikanBuku("Lord of the Rings");
            perpustakaan.KembalikanBuku("Harry Potter");

            Console.ReadLine();
        }
    }
}
