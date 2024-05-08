using System;
using System.Collections.Generic;

namespace PustakaCeria.Menu
{
    public class RiwayatPeminjaman
    {
        private List<Peminjaman> riwayatPeminjaman = new List<Peminjaman>();

        public RiwayatPeminjaman()
        {
            TambahRiwayat();
        }

        private void TambahRiwayat()
        {
            // Membuat tabel peminjaman
            string[,] dataPeminjaman = new string[,]
            {
                { "John Doe", "Introduction to Programming", "2024-04-01" },
                { "Jane Smith", "Data Structures and Algorithms", "2024-04-05" },
                { "Alice Johnson", "Database Management Systems", "2024-04-10" }
            };

            // Menambahkan entri riwayat peminjaman dari tabel
            for (int i = 0; i < dataPeminjaman.GetLength(0); i++)
            {
                string namaPeminjam = dataPeminjaman[i, 0];
                string judulBuku = dataPeminjaman[i, 1];
                DateTime tanggalPeminjaman = DateTime.Parse(dataPeminjaman[i, 2]);

                riwayatPeminjaman.Add(new Peminjaman(namaPeminjam, judulBuku, tanggalPeminjaman));
            }
        }

        public void PrintRiwayat()
        {
            Console.WriteLine("Riwayat Peminjaman Buku:");
            foreach (var peminjaman in riwayatPeminjaman)
            {
                Console.WriteLine($"Peminjam: {peminjaman.NamaPeminjam}, Buku: {peminjaman.JudulBuku}, Tanggal Peminjaman: {peminjaman.TanggalPeminjaman.ToShortDateString()}");
            }
        }
    }

    internal class Peminjaman
    {
        public string NamaPeminjam { get; set; }
        public string JudulBuku { get; set; }
        public DateTime TanggalPeminjaman { get; set; }

        public Peminjaman(string namaPeminjam, string judulBuku, DateTime tanggalPeminjaman)
        {
            NamaPeminjam = namaPeminjam;
            JudulBuku = judulBuku;
            TanggalPeminjaman = tanggalPeminjaman;
        }
    }
}
