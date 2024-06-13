using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics.Contracts;

namespace PustakaCeria.Menu
{
    public class BukuBuku
    {
        public string Judul { get; set; }
        public string Pengarang { get; set; }
        public bool SedangDipinjam { get; set; }
    }

    public class Peminjaman
    {
        public string Peminjam { get; set; }
        public DateTime TanggalPinjam { get; set; }
        public string Judul { get; set; }
    }

    public class LibraryData
    {
        public List<BukuBuku> DaftarBuku { get; set; } = new List<BukuBuku>();
        public List<Peminjaman> DaftarPeminjaman { get; set; } = new List<Peminjaman>();
    }

    public class PeminjamanDanPengembalianBuku
    {
        private List<BukuBuku> daftarBuku;
        private List<Peminjaman> daftarPeminjaman;
        private readonly string filePath = "pinjambuku.json";

        public PeminjamanDanPengembalianBuku()
        {
            LoadData();
        }

        private void LoadData()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found, a new file will be created.");
                daftarBuku = new List<BukuBuku>();
                daftarPeminjaman = new List<Peminjaman>();
                SaveData();
            }
            else
            {
                try
                {
                    string json = File.ReadAllText(filePath);
                    var data = JsonConvert.DeserializeObject<LibraryData>(json) ?? new LibraryData();
                    daftarBuku = data.DaftarBuku;
                    daftarPeminjaman = data.DaftarPeminjaman;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to load data: {ex.Message}");
                    daftarBuku = new List<BukuBuku>();
                    daftarPeminjaman = new List<Peminjaman>();
                }
            }
        }

        private void SaveData()
        {
            try
            {
                var data = new LibraryData
                {
                    DaftarBuku = daftarBuku,
                    DaftarPeminjaman = daftarPeminjaman
                };
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save data: {ex.Message}");
            }
        }

        public void PinjamBuku(string judul, string pengarang, string peminjam)
        {
            Contract.Requires(!string.IsNullOrEmpty(judul), "Judul buku tidak boleh kosong.");
            Contract.Requires(!string.IsNullOrEmpty(pengarang), "Pengarang buku tidak boleh kosong.");
            Contract.Requires(!string.IsNullOrEmpty(peminjam), "Nama peminjam tidak boleh kosong.");

            var buku = daftarBuku.Find(b => b.Judul == judul && b.Pengarang == pengarang && !b.SedangDipinjam);
            Contract.Assert(buku != null, "Buku tidak tersedia untuk dipinjam.");

            buku.SedangDipinjam = true;
            daftarPeminjaman.Add(new Peminjaman { Peminjam = peminjam, TanggalPinjam = DateTime.Now, Judul = judul });
            SaveData();
            Console.WriteLine("Book successfully borrowed.");
        }

        public void KembalikanBuku(string judul, string pengarang, string peminjam)
        {
            Contract.Requires(!string.IsNullOrEmpty(judul), "Judul buku tidak boleh kosong.");
            Contract.Requires(!string.IsNullOrEmpty(pengarang), "Pengarang buku tidak boleh kosong.");
            Contract.Requires(!string.IsNullOrEmpty(peminjam), "Nama peminjam tidak boleh kosong.");

            var buku = daftarBuku.Find(b => b.Judul == judul && b.Pengarang == pengarang && b.SedangDipinjam);
            Contract.Assert(buku != null, "Buku tidak dapat dikembalikan karena belum dipinjam.");

            buku.SedangDipinjam = false;
            var peminjaman = daftarPeminjaman.FindLast(p => p.Judul == judul && p.Peminjam == peminjam);
            Contract.Assert(peminjaman != null, "Peminjaman tidak ditemukan.");

            daftarPeminjaman.Remove(peminjaman);
            SaveData();
            Console.WriteLine("Book successfully returned.");
        }

        public void TambahBuku(string judul, string pengarang)
        {
            Contract.Requires(!string.IsNullOrEmpty(judul), "Judul buku tidak boleh kosong.");
            Contract.Requires(!string.IsNullOrEmpty(pengarang), "Pengarang buku tidak boleh kosong.");

            daftarBuku.Add(new BukuBuku { Judul = judul, Pengarang = pengarang, SedangDipinjam = false });
            SaveData();
            Console.WriteLine("Book successfully added.");
        }

        public void TampilkanDaftarBuku()
        {
            Console.WriteLine("Daftar Buku:");
            foreach (var buku in daftarBuku)
            {
                Console.WriteLine($"Judul: {buku.Judul}, Pengarang: {buku.Pengarang}, Sedang Dipinjam: {buku.SedangDipinjam}");
            }
        }

        public void TampilkanMenuPeminjamanDanPengembalianBuku()
        {
            bool kembaliKeMenuUtama = false;
            while (!kembaliKeMenuUtama)
            {
                Console.WriteLine("===== Menu Peminjaman dan Pengembalian Buku =====");
                Console.WriteLine("1. Pinjam Buku");
                Console.WriteLine("2. Kembalikan Buku");
                Console.WriteLine("3. Tambah Buku");
                Console.WriteLine("4. Tampilkan Daftar Buku");
                Console.WriteLine("0. Kembali ke Menu Utama");
                Console.Write("Masukkan nomor menu yang dipilih: ");
                int pilihan = int.Parse(Console.ReadLine());

                switch (pilihan)
                {
                    case 1:
                        Console.Write("Masukkan judul buku: ");
                        string judulPinjam = Console.ReadLine();
                        Console.Write("Masukkan pengarang buku: ");
                        string pengarangPinjam = Console.ReadLine();
                        Console.Write("Masukkan nama peminjam: ");
                        string peminjamPinjam = Console.ReadLine();
                        PinjamBuku(judulPinjam, pengarangPinjam, peminjamPinjam);
                        break;
                    case 2:
                        Console.Write("Masukkan judul buku: ");
                        string judulKembali = Console.ReadLine();
                        Console.Write("Masukkan pengarang buku: ");
                        string pengarangKembali = Console.ReadLine();
                        Console.Write("Masukkan nama peminjam: ");
                        string peminjamKembali = Console.ReadLine();
                        KembalikanBuku(judulKembali, pengarangKembali, peminjamKembali);
                        break;
                    case 3:
                        Console.Write("Masukkan judul buku: ");
                        string judulTambah = Console.ReadLine();
                        Console.Write("Masukkan pengarang buku: ");
                        string pengarangTambah = Console.ReadLine();
                        TambahBuku(judulTambah, pengarangTambah);
                        break;
                    case 4:
                        TampilkanDaftarBuku();
                        break;
                    case 0:
                        kembaliKeMenuUtama = true;
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                        break;
                }

                if (!kembaliKeMenuUtama)
                {
                    Console.WriteLine("Tekan tombol apa pun untuk kembali ke menu peminjaman dan pengembalian.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
    }
}
