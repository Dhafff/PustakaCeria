using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

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

        public PeminjamanDanPengembalianBuku(string jsonPath)
        {
            string jsonData = File.ReadAllText(jsonPath);
            daftarBuku = JsonConvert.DeserializeObject<List<Buku>>(jsonData);
        }

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
         public void SimpanKeJSON(string jsonPath)
        {
            string jsonData = JsonConvert.SerializeObject(daftarBuku, Formatting.Indented);
            File.WriteAllText(jsonPath, jsonData);
        }

        public static void Main(string[] args)
        {
            PeminjamanDanPengambalianBuku perpustakaan = new PeminjamanDanPengambalianBuku(jsonPath);
            perpustakaan.PinjamBuku("Laskar Pelangi");
            perpustakaan.PinjamBuku("Bumi Manusia");
            perpustakaan.PinjamBuku("Dilan 1990");
            perpustakaan.PinjamBuku("5 cm");
            perpustakaan.PinjamBuku("Autobiografi Tan Malaka: Dari Penjara Ke Penjara");
            perpustakaan.PinjamBuku("Atomic Habits");
            perpustakaan.PinjamBuku("Soekarno: Biografi Singkat 1901 – 1970");
            perpustakaan.PinjamBuku("Rudy: Kisah Masa Muda Sang Visioner");
            perpustakaan.PinjamBuku("Autobiografi Mahatma Gandhi");

            perpustakaan.KembalikanBuku("Laskar Pelangi");
            perpustakaan.KembalikanBuku("Bumi Manusia");
            perpustakaan.KembalikanBuku("Dilan 1990");
            perpustakaan.KembalikanBuku("5 cm");
            perpustakaan.KembalikanBuku("Autobiografi Tan Malaka: Dari Penjara Ke Penjara");
            perpustakaan.KembalikanBuku("Atomic Habits");
            perpustakaan.KembalikanBuku("Soekarno: Biografi Singkat 1901 – 1970");
            perpustakaan.KembalikanBuku("Rudy: Kisah Masa Muda Sang Visioner");
            perpustakaan.KembalikanBuku("Autobiografi Mahatma Gandhi");

            perpustakaan.SimpanKeJSON(jsonPath);
            Console.ReadLine();
        }
        
    }
}
