````
# 📂 FTP Client Sederhana (VB.NET)

Selamat datang di dokumentasi resmi **Proyek FTP Client Sederhana**. Aplikasi ini adalah sebuah klien FTP desktop yang fungsional, dibangun menggunakan **Visual Basic .NET (VB.NET)** dan **Windows Forms**. Proyek ini dirancang sebagai sarana edukasi dan fondasi untuk memahami cara kerja komunikasi dengan server FTP, dengan antarmuka yang terinspirasi dari aplikasi populer seperti FileZilla.

---

> ✨ _"Aku tidak berilmu; yang berilmu hanyalah DIA. Jika tampak ilmu dariku, itu hanyalah pantulan dari Cahaya-Nya."_

---

## 📜 Daftar Isi

- [✨ Fitur Utama](#-fitur-utama)
- [🚀 Teknologi yang Digunakan](#-teknologi-yang-digunakan)
- [🚀 Panduan Instalasi](#-panduan-instalasi)
- [📁 Struktur Folder & File](#-struktur-folder--file)
- [📖 Panduan Penggunaan](#-panduan-penggunaan)
- [🧠 Konsep Teknis Utama](#-konsep-teknis-utama)
- [📣 Panduan Kontribusi](#-panduan-kontribusi)

---

## ✨ Fitur Utama

Aplikasi ini dilengkapi dengan fitur-fitur esensial seperti:

- 🌐 **Koneksi FTP Standar**  
  Terhubung ke server FTP dengan Host, Port, Username, dan Password.

- ↔️ **Tampilan Dua Panel**  
  Menampilkan file lokal dan file server secara berdampingan.

- 📁 **Navigasi Direktori**  
  Jelajahi folder dan sub-folder di lokal & server, termasuk naik ke folder induk.

- 🔼 **Upload File**  
  Unggah file dari lokal ke server.

- 🔽 **Download File**  
  Unduh file dari server ke lokal.

- 🗑️ **Manajemen File & Folder**  
  Hapus file/folder dan buat folder baru di server.

- ✍️ **Edit File Langsung**  
  Edit file teks di server secara otomatis (download → edit → upload).

- 📊 **Indikator Proses**  
  Progress bar untuk menunjukkan status upload/download.

- 📜 **Log Aktivitas**  
  Panel log untuk melacak perintah dan status koneksi.

---

## 🚀 Teknologi yang Digunakan

- **Bahasa:** Visual Basic .NET (VB.NET)  
- **UI:** Windows Forms (WinForms)  
- **Framework:** .NET Framework 4.x  
- **FTP Class:** `System.Net.FtpWebRequest`  
- **IDE:** Microsoft Visual Studio

---

## 🚀 Panduan Instalasi

### 🔧 Prasyarat

- **Microsoft Visual Studio** (2017 atau lebih baru)
- **.NET Framework 4.7.2** atau lebih tinggi

### 📦 Langkah Instalasi

```bash
git clone https://github.com/Alghifari888/FtpClient
````

1. Buka file `.sln` di Visual Studio
2. Build > Build Solution (atau tekan `F6`)
3. Jalankan aplikasi (F5) atau buka `.exe` dari `bin/Debug/`

---

## 📁 Struktur Folder & File

```
VbFtpClient/
├── My Project/
│   ├── Application.myapp
│   ├── AssemblyInfo.vb
│   ├── Resources.resx
│   └── Settings.settings
├── bin/
│   └── Debug/
│       └── VbFtpClient.exe
├── obj/
├── App.config
├── Form1.vb
├── Form1.Designer.vb
├── frmEditor.vb
├── frmEditor.Designer.vb
└── VbFtpClient.vbproj
```

* **Form1.vb:** UI utama, koneksi & transfer file
* **frmEditor.vb:** Editor teks langsung dari server

---

## 📖 Panduan Penggunaan

1. Jalankan aplikasi (`F5` atau file `.exe`)
2. Masukkan detail koneksi:

   * **Host:** `ftp.domain.com`
   * **Port:** `21`
   * **Username / Password**
3. Klik `Connect`, lalu gunakan fitur:

| Aksi        | Cara                                   |
| ----------- | -------------------------------------- |
| Upload      | Pilih file lokal → klik `Upload >>`    |
| Download    | Pilih file server → klik `<< Download` |
| Edit File   | Pilih file teks → klik `Edit File`     |
| Buat Folder | Klik `New Folder`                      |
| Hapus       | Pilih item → klik `Delete`             |

---

## 🧠 Konsep Teknis Utama

### 1. Pemrograman Asinkron (`Async/Await`)

Memastikan UI tetap responsif selama proses FTP berjalan.

### 2. Kelas `FtpWebRequest`

Digunakan untuk:

| Fungsi      | Method                 |
| ----------- | ---------------------- |
| List        | `ListDirectoryDetails` |
| Upload      | `UploadFile`           |
| Download    | `DownloadFile`         |
| Hapus File  | `DeleteFile`           |
| Buat Folder | `MakeDirectory`        |

### 3. Safe UI Update (`InvokeRequired`)

Gunakan `Invoke()` saat update kontrol UI dari thread berbeda.

---

## 📣 Panduan Kontribusi

Kami menyambut kontribusi! Ikuti langkah berikut:

### A. Kolaborator Langsung

```bash
git clone https://github.com/NAMA_USER/NAMA_REPO.git
git checkout -b fitur/nama-fitur-baru
git add .
git commit -m "feat: Menambahkan fitur baru"
git push origin fitur/nama-fitur-baru
```

Buat Pull Request ke branch `main`.

---

### B. Kontributor Luar (Fork)

```bash
# Fork dari GitHub, lalu
git clone https://github.com/USERNAME_ANDA/NAMA_REPO.git
git checkout -b fix/nama-perbaikan
```

Setelah selesai, push dan buat **Pull Request** dari fork Anda ke repo asli.

---

## ✅ Pedoman Kontribusi

* Gunakan gaya kode yang konsisten
* Format commit:

  * `feat:` untuk fitur baru
  * `fix:` untuk perbaikan
  * `docs:` untuk dokumentasi
  * `style:` untuk formatting
  * `refactor:` untuk perombakan logika tanpa fitur baru
* 1 PR per fitur/bug untuk review yang lebih mudah

---

## 🙏 Terima Kasih

Terima kasih atas minat dan kontribusi Anda! Mari belajar dan berkembang bersama komunitas open source 🇮🇩🚀

```
