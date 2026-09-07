using System;

namespace QuanLySachCoBan
{
	class Sach
	{
		// =========================
		// 1. KHAI BÁO FIELD
		// =========================
		private string _maSach;
		private string _tenSach;
		private string _tacGia;
		private int _namXuatBan;
		private double _giaBan;

		// =========================
		// 2. CONSTRUCTOR ĐẦY ĐỦ
		// =========================
		public Sach(string maSach, string tenSach, string tacGia,
					int namXuatBan, double giaBan)
		{
			_maSach = maSach;
			TenSach = tenSach;
			_tacGia = tacGia;
			NamXuatBan = namXuatBan;

			if (giaBan < 0)
				throw new ArgumentException("Giá bán không được âm!");

			_giaBan = giaBan;
		}

		// =========================
		// 3. CONSTRUCTOR KHÔNG THAM SỐ
		// =========================
		public Sach()
		{
			_maSach = "S000";
			_tenSach = "Chưa có tên";
			_tacGia = "Chưa xác định";
			_namXuatBan = 2020;
			_giaBan = 0;
		}

		// =========================
		// 4. PROPERTY
		// =========================

		// MaSach: chỉ đọc từ bên ngoài
		public string MaSach
		{
			get { return _maSach; }
		}

		// TenSach: đọc/ghi, không được rỗng
		public string TenSach
		{
			get { return _tenSach; }

			set
			{
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentException("Tên sách không được để trống!");

				_tenSach = value;
			}
		}

		// NamXuatBan: từ 1900 đến năm hiện tại
		public int NamXuatBan
		{
			get { return _namXuatBan; }

			set
			{
				int namHienTai = DateTime.Now.Year;

				if (value < 1900 || value > namHienTai)
					throw new ArgumentException(
						$"Năm xuất bản phải từ 1900 đến {namHienTai}!");

				_namXuatBan = value;
			}
		}

		// GiaBan: chỉ đọc từ bên ngoài
		public double GiaBan
		{
			get { return _giaBan; }
		}

		// =========================
		// 5. HIỂN THỊ THÔNG TIN
		// =========================
		public void HienThiThongTin()
		{
			Console.WriteLine("----------------------------------------");
			Console.WriteLine($"Mã sách       : {MaSach}");
			Console.WriteLine($"Tên sách      : {TenSach}");
			Console.WriteLine($"Tác giả       : {_tacGia}");
			Console.WriteLine($"Năm xuất bản  : {NamXuatBan}");
			Console.WriteLine($"Giá bán       : {GiaBan:N0} VNĐ");
			Console.WriteLine("----------------------------------------");
		}

		// =========================
		// 6. OVERRIDE TOSTRING
		// =========================
		public override string ToString()
		{
			return $"{MaSach} - {TenSach} - {_tacGia} - " +
				   $"{NamXuatBan} - {GiaBan:N0} VNĐ";
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;

			Console.WriteLine("========== QUẢN LÝ SÁCH ==========\n");

			// ==========================================
			// CÁCH 1: CONSTRUCTOR ĐẦY ĐỦ THAM SỐ
			// ==========================================
			Sach sach1 = new Sach(
				"S001",
				"Lập trình C#",
				"Nguyễn Văn A",
				2023,
				150000
			);

			// ==========================================
			// CÁCH 2: CONSTRUCTOR KHÔNG THAM SỐ
			// SAU ĐÓ GÁN PROPERTY
			// ==========================================
			Sach sach2 = new Sach();

			// MaSach chỉ đọc nên không thể:
			// sach2.MaSach = "S002";  // LỖI

			sach2.TenSach = "Lập trình hướng đối tượng";
			sach2.NamXuatBan = 2024;

			// GiaBan cũng chỉ đọc nên không thể:
			// sach2.GiaBan = 200000;  // LỖI

			// ==========================================
			// CÁCH 3: OBJECT INITIALIZER
			// ==========================================
			Sach sach3 = new Sach(
				"S003",
				"Cơ sở dữ liệu",
				"Trần Văn B",
				2022,
				180000
			)
			{
				TenSach = "Cơ sở dữ liệu nâng cao",
				NamXuatBan = 2025
			};

			// ==========================================
			// HIỂN THỊ 3 ĐỐI TƯỢNG
			// ==========================================
			Console.WriteLine("----- SÁCH 1 -----");
			sach1.HienThiThongTin();

			Console.WriteLine("\n----- SÁCH 2 -----");
			sach2.HienThiThongTin();

			Console.WriteLine("\n----- SÁCH 3 -----");
			sach3.HienThiThongTin();

			// ==========================================
			// TEST TOSTRING()
			// ==========================================
			Console.WriteLine("\n========== TOSTRING ==========");
			Console.WriteLine(sach1.ToString());
			Console.WriteLine(sach2.ToString());
			Console.WriteLine(sach3.ToString());

			// ==========================================
			// THỬ GÁN NĂM XUẤT BẢN KHÔNG HỢP LỆ
			// ==========================================
			Console.WriteLine("\n========== KIỂM TRA NGOẠI LỆ ==========");

			try
			{
				Console.WriteLine("Đang thử gán năm xuất bản = 1800...");

				sach1.NamXuatBan = 1800;
			}
			catch (ArgumentException ex)
			{
				Console.WriteLine("Lỗi: " + ex.Message);
			}

			// ==========================================
			// THỬ TÊN SÁCH RỖNG
			// ==========================================
			try
			{
				Console.WriteLine("\nĐang thử gán tên sách rỗng...");

				sach1.TenSach = "";
			}
			catch (ArgumentException ex)
			{
				Console.WriteLine("Lỗi: " + ex.Message);
			}
			Console.WriteLine("MSSV: 6551071036");
			Console.WriteLine("\nNhấn phím bất kỳ để kết thúc...");
			
			Console.ReadKey();
		}
	}
}