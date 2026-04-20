# REST API Weather Forecast

## Author

* Nama  : Adrian Rizqynaya Putra
* NIM   : 242410102075
* Kelas : PAA A

---

## Deskripsi Project

Project ini merupakan implementasi REST API Sistem Informasi Prediksi Cuaca yang digunakan untuk mengelola data:

1.User
2. Admin

API ini mendukung operasi CRUD (Create, Read, Update, Delete) dan dirancang untuk kelola data pengguna Prediksi Cuaca.

---

## Teknologi yang Digunakan

1. Bahasa Pemrograman: C#
2. Framework: ASP.NET Core Web API
3. Database: PostgreSQL
4. Library: Npgsql (PostgreSQL Driver), JwtBearer, OpenAPI
5. Tools: Visual Studio, Swagger (OpenAPI)

---

## Cara Instalasi dan Menjalankan Project

### 1. Clone Repository

```bash
git clone https://github.com/rizqyny/restapicrudauth
cd restapicrudauth
```

### 2. Buka Project

Buka file solution (.sln) menggunakan Visual Studio.

### 3. Konfigurasi Database

Edit file `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=nama_db;Username=postgres;Password=123"
}
```

### 4. Jalankan Project

Tekan F5 atau klik Start pada Visual Studio.
API akan berjalan dan dapat diakses melalui:

```
https://localhost:xxxx/swagger/index.html
```

---

## Cara Import Database

1. Buka pgAdmin atau PostgreSQL
2. Buat database baru
3. Buka Query Tool
4. Jalankan file `weatherforecast.sql`

File tersebut sudah berisi:

1. Struktur tabel (DDL)
2. Relasi foreign key
3. Sample data minimal 5 baris

---

## Daftar Endpoint API

### Anonymous

| Method | Endpoint         | Keterangan                 |
| ------ | ---------------- | -------------------------- |
| GET    | /api/person      | Melihat list nama person   |
| POST   | /api/Auth/login  | Login                      |

### User

| Method | Endpoint         | Keterangan           |
| ------ | ---------------- | ------------------   |
| GET    | /WeatherForecast | Akses Prediksi Cuaca |


### Admin

| Method | Endpoint                   | Keterangan                   |
| ------ | -------------------------- | ---------------------------- |
| GET    | /api/person/{id}           | Melihat Detail Person        |
| POST   | /api/person                | Tambah Data Person           |
| PUT    | /api/person/{id}           | Ubah Data Person             |
| DELETE | /api/person/{id}/delete    | Hapus Data Person            |

---

## Format Response API

### Success

```json
{
  "status": "success",
  "data": {},
  "meta": {}
}
```

### Error

```json
{
  "status": "error",
  "message": "Pesan error"
}
```

---

## Link Video Presentasi

https://youtu.be/h4ZbnLDsDR0
