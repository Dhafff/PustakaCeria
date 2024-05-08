using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace PustakaCeria.Menu
{
    public class ManajemenStokBuku
    {
        private string filePath = "../../../Buku.json";        

        public List<ManajemenBukuLibraries.Buku> LihatBuku()
        {
            return LoadBuku();
        }

        public void TambahBuku(ManajemenBukuLibraries.Buku buku)
        {
            Contract.Requires<ArgumentNullException>(buku != null, "Buku tidak boleh kosong");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(buku.Judul), "Judul buku tidak boleh kosong");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(buku.Penulis), "Penulis buku tidak boleh kosong");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(buku.Genre), "Genre buku tidak boleh kosong");

            List<ManajemenBukuLibraries.Buku> bukus = LoadBuku();
            bukus.Add(buku);
            SaveBuku(bukus);

            Contract.Ensures(LihatBuku().Contains(buku), "Buku harus ditambahkan");
        }

        private ManajemenBukuLibraries.Buku InputBuku()
        {
            Console.Write("Judul: ");
            string judul = Console.ReadLine();
            Console.Write("Penulis: ");
            string penulis = Console.ReadLine();
            Console.Write("Genre: ");
            string genre = Console.ReadLine();

            return new ManajemenBukuLibraries.Buku { Judul = judul, Penulis = penulis, Genre = genre };
        }

        public void HapusBuku(string judul)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(judul), "Judul buku tidak boleh kosong");

            List<ManajemenBukuLibraries.Buku> bukus = LoadBuku();
            ManajemenBukuLibraries.Buku bukuToRemove = bukus.Find(b => b.Judul.Equals(judul));
            if (bukuToRemove != null)
            {
                bukus.Remove(bukuToRemove);
                SaveBuku(bukus);
            }
            else
            {
                Console.WriteLine("Buku tidak ditemukan.");
                Console.WriteLine();
            }
        }

        public void EditBuku(string judul, ManajemenBukuLibraries.Buku newBukuData)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(judul), "Judul buku tidak boleh kosong");
            Contract.Requires<ArgumentNullException>(newBukuData != null, "Data buku baru tidak boleh kosong");

            List<ManajemenBukuLibraries.Buku> bukus = LoadBuku();
            ManajemenBukuLibraries.Buku bukuToEdit = bukus.Find(b => b.Judul.Equals(judul));
            if (bukuToEdit != null)
            {
                bukuToEdit.Penulis = newBukuData.Penulis;
                bukuToEdit.Genre = newBukuData.Genre;
                SaveBuku(bukus);
            }
            else
            {
                Console.WriteLine("Buku tidak ditemukan.");
                Console.WriteLine();
            }
        }

        private List<ManajemenBukuLibraries.Buku> LoadBuku()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                List<ManajemenBukuLibraries.Buku> bukus = JsonConvert.DeserializeObject<List<ManajemenBukuLibraries.Buku>>(json);
                for (int i = 0; i < bukus.Count; i++)
                {
                    bukus[i].Id = i + 1;
                }
                return bukus;
            }
            else
            {
                Console.WriteLine("File Buku.json tidak ditemukan.");
                Console.WriteLine();
                return new List<ManajemenBukuLibraries.Buku>();
            }
        }

        private void SaveBuku(List<ManajemenBukuLibraries.Buku> bukus)
        {
            string json = JsonConvert.SerializeObject(bukus, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public void TampilkanMenu()
        {
            bool exitMenu = false;

            while (!exitMenu)
            {
                Console.WriteLine("===== Menu Manejemen Stok Buku =====");
                Console.WriteLine("1. Lihat Daftar Buku");
                Console.WriteLine("2. Tambah Buku Baru");
                Console.WriteLine("3. Hapus Buku");
                Console.WriteLine("4. Edit Informasi Buku");
                Console.WriteLine("0. Kembali ke Menu Utama");
                Console.WriteLine("=====================================");
                Console.WriteLine();

                int pilihMenu = BacaInputMenu("Masukkan nomor menu yang dipilih: ");
                switch (pilihMenu)
                {
                    case 1:
                        // Lihat buku
                        Console.WriteLine();
                        List<ManajemenBukuLibraries.Buku> daftarBuku = LihatBuku();
                        if (daftarBuku.Count > 0)
                        {
                            foreach (var buku in daftarBuku)
                            {
                                Console.WriteLine($"No: {buku.Id}");
                                Console.WriteLine($"Judul: {buku.Judul}");
                                Console.WriteLine($"Penulis: {buku.Penulis}");
                                Console.WriteLine($"Genre: {buku.Genre}");
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Daftar buku kosong.");
                            Console.WriteLine();
                        }
                        break;
                    case 2:
                        // Menambah buku baru
                        Console.WriteLine();
                        Console.WriteLine("Masukkan informasi buku baru");
                        ManajemenBukuLibraries.Buku bukuBaru = InputBuku();
                        TambahBuku(bukuBaru);
                        Console.WriteLine("Buku berhasil ditambahkan.");
                        break;
                    case 3:
                        // Menghapus buku
                        Console.WriteLine();
                        Console.Write("Masukkan judul buku yang akan dihapus: ");
                        string judulBukuDihapus = Console.ReadLine();
                        HapusBuku(judulBukuDihapus);
                        break;
                    case 4:
                        // Mengedit informasi buku
                        Console.WriteLine();
                        Console.Write("Masukkan judul buku yang akan diedit: ");
                        string judulBukuDiubah = Console.ReadLine();
                        Console.WriteLine("Masukkan informasi baru untuk buku");
                        ManajemenBukuLibraries.Buku dataBukuBaru = InputBuku();
                        EditBuku(judulBukuDiubah, dataBukuBaru);
                        Console.WriteLine("Informasi buku berhasil diperbarui.");
                        break;
                    case 0:
                        exitMenu = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                        Console.WriteLine();
                        break;
                }
            }
        }

        private int BacaInputMenu(string message)
        {
            int input;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Input tidak valid. Mohon masukkan angka yang tersedia.");
                    Console.WriteLine();
                }
            }
            return input;
        }
    }
}
