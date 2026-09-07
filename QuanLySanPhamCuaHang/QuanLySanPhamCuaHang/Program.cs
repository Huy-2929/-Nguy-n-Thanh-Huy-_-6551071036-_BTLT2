using System;
using System.Collections.Generic;

namespace QuanLySanPhamCuaHang
{
	// ==========================================
	// CLASS SANPHAM - BASE CLASS
	// ==========================================
	class SanPham
	{
		private string _maSP;
		private string _tenSP;
		private decimal _gia;
		private int _soLuongTon;

		// Property
		public string MaSP
		{
			get { return _maSP; }
			set { _maSP = value; }
		}

		public string TenSP
		{
			get { return _tenSP; }
			set { _tenSP = value; }
		}

		public decimal Gia
		{
			get { return _gia; }
			set { _gia = value; }
		}

		public int SoLuongTon
		{
			get { return _soLuongTon; }
			set { _soLuongTon = value; }
		}

		// Constructor đầy đủ
		public SanPham(string maSP, string tenSP, decimal gia, int soLuongTon)
		{
			_maSP = maSP;
			_tenSP = tenSP;
			_gia = gia;
			_soLuongTon = soLuongTon;
		}

		// Constructor không tham số
		public SanPham()
		{
			_maSP = "";
			_tenSP = "";
			_gia = 0;
			_soLuongTon = 0;
		}

		// Phương thức virtual
		public virtual decimal TinhGiaBan()
		{
			return _gia;
		}

		// Phương thức virtual
		public virtual string MoTa()
		{
			return $"Mã SP: {_maSP}, Tên: {_tenSP}, Giá: {_gia:N0} VNĐ";
		}
	}


	// ==========================================
	// CLASS SANPHAMTHUCPHAM - KẾ THỪA SANPHAM
	// ==========================================
	class SanPhamThucPham : SanPham
	{
		private DateTime _ngayHetHan;
		private int _nhietDoBAoquan;

		// Property
		public DateTime NgayHetHan
		{
			get { return _ngayHetHan; }
			set { _ngayHetHan = value; }
		}

		public int NhietDoBaoQuan
		{
			get { return _nhietDoBAoquan; }
			set { _nhietDoBAoquan = value; }
		}

		// Constructor gọi base
		public SanPhamThucPham(
			string maSP,
			string tenSP,
			decimal gia,
			int soLuongTon,
			DateTime ngayHetHan,
			int nhietDoBaoQuan)
			: base(maSP, tenSP, gia, soLuongTon)
		{
			_ngayHetHan = ngayHetHan;
			_nhietDoBAoquan = nhietDoBaoQuan;
		}

		// Override TinhGiaBan
		public override decimal TinhGiaBan()
		{
			// Số ngày còn lại đến ngày hết hạn
			double soNgayConLai = (NgayHetHan - DateTime.Now).TotalDays;

			// Nếu còn 3 ngày hoặc ít hơn thì giảm 30%
			if (soNgayConLai <= 3 && soNgayConLai >= 0)
			{
				return Gia * 0.7m;
			}

			return Gia;
		}

		// Override MoTa
		public override string MoTa()
		{
			return $"[THỰC PHẨM] {MaSP} - {TenSP} | " +
				   $"Giá gốc: {Gia:N0} VNĐ | " +
				   $"Giá bán: {TinhGiaBan():N0} VNĐ | " +
				   $"Tồn: {SoLuongTon} | " +
				   $"Hết hạn: {NgayHetHan:dd/MM/yyyy} | " +
				   $"Nhiệt độ bảo quản: {NhietDoBaoQuan}°C";
		}
	}


	// ==========================================
	// CLASS SANPHAMDIENTU - KẾ THỪA SANPHAM
	// ==========================================
	class SanPhamDienTu : SanPham
	{
		private int _baoHanhThang;
		private string _hangSanXuat;

		// Property
		public int BaoHanhThang
		{
			get { return _baoHanhThang; }
			set { _baoHanhThang = value; }
		}

		public string HangSanXuat
		{
			get { return _hangSanXuat; }
			set { _hangSanXuat = value; }
		}

		// Constructor
		public SanPhamDienTu(
			string maSP,
			string tenSP,
			decimal gia,
			int soLuongTon,
			int baoHanhThang,
			string hangSanXuat)
			: base(maSP, tenSP, gia, soLuongTon)
		{
			_baoHanhThang = baoHanhThang;
			_hangSanXuat = hangSanXuat;
		}

		// Override TinhGiaBan
		public override decimal TinhGiaBan()
		{
			// Nếu bảo hành > 12 tháng thì cộng thêm 10%
			if (BaoHanhThang > 12)
			{
				return Gia * 1.10m;
			}

			return Gia;
		}

		// Override MoTa
		public override string MoTa()
		{
			return $"[ĐIỆN TỬ] {MaSP} - {TenSP} | " +
				   $"Giá gốc: {Gia:N0} VNĐ | " +
				   $"Giá bán: {TinhGiaBan():N0} VNĐ | " +
				   $"Tồn: {SoLuongTon} | " +
				   $"Hãng: {HangSanXuat} | " +
				   $"Bảo hành: {BaoHanhThang} tháng";
		}
	}


	// ==========================================
	// PROGRAM
	// ==========================================
	class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;

			// ==========================================
			// TẠO LIST<SANPHAM>
			// ==========================================
			List<SanPham> danhSach = new List<SanPham>();

			// ==========================================
			// OBJECT INITIALIZER - SẢN PHẨM THƯỜNG
			// ==========================================
			danhSach.Add(new SanPham
			{
				MaSP = "SP001",
				TenSP = "Bút bi",
				Gia = 5000,
				SoLuongTon = 100
			});

			// ==========================================
			// OBJECT INITIALIZER - THỰC PHẨM
			// Còn 2 ngày hết hạn -> giảm 30%
			// ==========================================
			danhSach.Add(new SanPhamThucPham(
				"TP001",
				"Sữa tươi",
				30000,
				50,
				DateTime.Now.AddDays(2),
				4
			));

			// ==========================================
			// OBJECT INITIALIZER - THỰC PHẨM
			// Còn 10 ngày -> không giảm
			// ==========================================
			danhSach.Add(new SanPhamThucPham(
				"TP002",
				"Bánh mì",
				20000,
				30,
				DateTime.Now.AddDays(10),
				5
			));

			// ==========================================
			// OBJECT INITIALIZER - ĐIỆN TỬ
			// Bảo hành 24 tháng -> cộng 10%
			// ==========================================
			danhSach.Add(new SanPhamDienTu(
				"DT001",
				"Laptop",
				20000000,
				10,
				24,
				"Dell"
			));

			// ==========================================
			// OBJECT INITIALIZER - ĐIỆN TỬ
			// Bảo hành 12 tháng -> không cộng
			// ==========================================
			danhSach.Add(new SanPhamDienTu(
				"DT002",
				"Chuột máy tính",
				500000,
				20,
				12,
				"Logitech"
			));


			// ==========================================
			// HIỂN THỊ DANH SÁCH
			// ==========================================
			Console.WriteLine("==============================================");
			Console.WriteLine("       QUẢN LÝ SẢN PHẨM CỬA HÀNG");
			Console.WriteLine("==============================================");

			decimal tongGiaTriKho = 0;

			foreach (SanPham sp in danhSach)
			{
				Console.WriteLine("\n" + sp.MoTa());

				// Đa hình runtime:
				// Mỗi đối tượng sẽ gọi TinhGiaBan()
				// theo đúng class thực tế của nó.
				decimal giaBan = sp.TinhGiaBan();

				Console.WriteLine($"Giá bán thực tế: {giaBan:N0} VNĐ");

				// Tính tổng giá trị kho
				tongGiaTriKho += giaBan * sp.SoLuongTon;
			}

			// ==========================================
			// TỔNG GIÁ TRỊ KHO
			// ==========================================
			Console.WriteLine("\n==============================================");
			Console.WriteLine($"TỔNG GIÁ TRỊ KHO: {tongGiaTriKho:N0} VNĐ");
			Console.WriteLine("==============================================");
			Console.WriteLine("MSSV: 6551071036");

			Console.WriteLine("\nNhấn phím bất kỳ để kết thúc...");
			Console.ReadKey();
		}
	}
}