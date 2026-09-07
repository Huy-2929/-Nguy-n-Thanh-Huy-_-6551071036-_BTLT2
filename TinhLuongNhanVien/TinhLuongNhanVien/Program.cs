using System;

namespace TinhLuongNhanVien
{
	class NhanVien
	{
		// ==========================================
		// 1. Khai báo các field private
		// ==========================================
		private string _maNV;
		private string _hoTen;
		private decimal _luongCoBan;
		private int _soNgayLam;
		private int _soNgayNghiPhep;

		// ==========================================
		// 2. Constructor không tham số
		// ==========================================
		public NhanVien()
		{
			_maNV = "NV000";
			_hoTen = "Chưa có tên";
			_luongCoBan = 0;
			_soNgayLam = 0;
			_soNgayNghiPhep = 0;
		}

		// ==========================================
		// 3. Constructor chỉ nhận mã NV và họ tên
		// ==========================================
		public NhanVien(string maNV, string hoTen)
		{
			_maNV = maNV;
			_hoTen = hoTen;
			_luongCoBan = 5_000_000;
			_soNgayLam = 26;
			_soNgayNghiPhep = 0;
		}

		// ==========================================
		// 4. Constructor đầy đủ tham số
		// ==========================================
		public NhanVien(string maNV, string hoTen,
						decimal luongCoBan, int soNgayLam,
						int soNgayNghiPhep)
		{
			_maNV = maNV;
			HoTen = hoTen;
			LuongCoBan = luongCoBan;
			SoNgayLam = soNgayLam;
			_soNgayNghiPhep = soNgayNghiPhep;
		}

		// ==========================================
		// 5. Constructor có Optional Parameters
		// ==========================================
		public NhanVien(string maNV, string hoTen,
						decimal luong = 5_000_000,
						int soNgayLam = 26)
		{
			_maNV = maNV;
			HoTen = hoTen;
			LuongCoBan = luong;
			SoNgayLam = soNgayLam;
			_soNgayNghiPhep = 0;
		}

		// ==========================================
		// 6. Property HoTen
		// ==========================================
		public string HoTen
		{
			get { return _hoTen; }
			set { _hoTen = value; }
		}

		// ==========================================
		// 7. Property LuongCoBan
		// Validate >= 0
		// ==========================================
		public decimal LuongCoBan
		{
			get { return _luongCoBan; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(
						"Lương cơ bản không được nhỏ hơn 0!"
					);
				}

				_luongCoBan = value;
			}
		}

		// ==========================================
		// 8. Property SoNgayLam
		// Validate từ 0 đến 31
		// ==========================================
		public int SoNgayLam
		{
			get { return _soNgayLam; }
			set
			{
				if (value < 0 || value > 31)
				{
					throw new ArgumentException(
						"Số ngày làm phải từ 0 đến 31!"
					);
				}

				_soNgayLam = value;
			}
		}

		// ==========================================
		// 9. Property LuongThucNhan
		// Chỉ đọc, tính tự động
		// ==========================================
		public decimal LuongThucNhan
		{
			get
			{
				// BHXH = 8% lương cơ bản
				decimal khauTruBHXH = LuongCoBan * 0.08m;

				// Lương thực nhận
				return (LuongCoBan / 26 * SoNgayLam)
					   - khauTruBHXH;
			}
		}

		// ==========================================
		// 10. Overload TinhThuong()
		// Không có tham số -> thưởng = 0
		// ==========================================
		public decimal TinhThuong()
		{
			return 0;
		}

		// ==========================================
		// 11. Overload TinhThuong(decimal heSo)
		// ==========================================
		public decimal TinhThuong(decimal heSo)
		{
			return LuongCoBan * heSo;
		}

		// ==========================================
		// 12. Overload TinhThuong(decimal heSo, bool coPhucLoi)
		// ==========================================
		public decimal TinhThuong(decimal heSo, bool coPhucLoi)
		{
			decimal tienThuong = LuongCoBan * heSo;

			if (coPhucLoi)
			{
				tienThuong += 500_000;
			}

			return tienThuong;
		}

		// ==========================================
		// 13. Phương thức hiển thị thông tin
		// ==========================================
		public void HienThiThongTin()
		{
			Console.WriteLine("------------------------------------------");
			Console.WriteLine("          THÔNG TIN NHÂN VIÊN");
			Console.WriteLine("------------------------------------------");
			Console.WriteLine($"Mã NV          : {_maNV}");
			Console.WriteLine($"Họ tên         : {HoTen}");
			Console.WriteLine($"Lương cơ bản   : {LuongCoBan:N0} VNĐ");
			Console.WriteLine($"Số ngày làm    : {SoNgayLam}");
			Console.WriteLine($"Lương thực nhận: {LuongThucNhan:N0} VNĐ");
			Console.WriteLine("------------------------------------------");
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			// ==========================================
			// 1. Nhân viên dùng constructor không tham số
			// ==========================================
			NhanVien nv1 = new NhanVien();

			nv1.HoTen = "Nguyễn Văn A";
			nv1.LuongCoBan = 8_000_000;
			nv1.SoNgayLam = 26;

			// ==========================================
			// 2. Nhân viên dùng constructor mã NV + họ tên
			// ==========================================
			NhanVien nv2 = new NhanVien(
				"NV002",
				"Trần Văn B"
			);

			// ==========================================
			// 3. Dùng Named Arguments với
			//    Optional Parameters
			// ==========================================
			NhanVien nv3 = new NhanVien(
				maNV: "NV003",
				hoTen: "Lê Văn C",
				soNgayLam: 20
			);

			// ==========================================
			// Hiển thị thông tin
			// ==========================================
			Console.WriteLine("========== DANH SÁCH NHÂN VIÊN ==========");
			Console.WriteLine();

			nv1.HienThiThongTin();
			nv2.HienThiThongTin();
			nv3.HienThiThongTin();

			// ==========================================
			// Gọi 3 overload TinhThuong
			// ==========================================
			Console.WriteLine();
			Console.WriteLine("========== TÍNH THƯỞNG ==========");

			Console.WriteLine(
				$"Nhân viên {nv1.HoTen}"
			);

			Console.WriteLine(
				$"TinhThuong() = {nv1.TinhThuong():N0} VNĐ"
			);

			Console.WriteLine(
				$"TinhThuong(0.1m) = {nv1.TinhThuong(0.1m):N0} VNĐ"
			);

			Console.WriteLine(
				$"TinhThuong(0.1m, true) = " +
				$"{nv1.TinhThuong(0.1m, true):N0} VNĐ"
			);

			// ==========================================
			// So sánh tiền thưởng
			// ==========================================
			decimal thuong1 = nv1.TinhThuong();
			decimal thuong2 = nv1.TinhThuong(0.1m);
			decimal thuong3 = nv1.TinhThuong(0.1m, true);

			Console.WriteLine();
			Console.WriteLine("========== SO SÁNH ==========");
			Console.WriteLine($"Không có hệ số        : {thuong1:N0} VNĐ");
			Console.WriteLine($"Có hệ số 0.1          : {thuong2:N0} VNĐ");
			Console.WriteLine($"Hệ số 0.1 + phúc lợi  : {thuong3:N0} VNĐ");

			// ==========================================
			// Kết thúc
			// ==========================================
			Console.WriteLine();
			Console.WriteLine("Nhấn phím bất kỳ để kết thúc...");
			Console.ReadKey();
		}
	}
}