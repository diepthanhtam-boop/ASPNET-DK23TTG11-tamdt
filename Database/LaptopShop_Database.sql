-- =============================================
-- Tạo Cơ Sở Dữ Liệu LaptopShop
-- Ngày tạo: 02/11/2024
-- Mô tả: CSDL quản lý bán laptop trực tuyến
-- =============================================

-- Tạo database
CREATE DATABASE LaptopShopDB
GO

USE LaptopShopDB
GO

-- =============================================
-- Bảng Người Dùng (Users)
-- =============================================
CREATE TABLE NguoiDung (
    Id NVARCHAR(450) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    Email NVARCHAR(256) NOT NULL UNIQUE,
    MatKhauMaHoa NVARCHAR(500) NOT NULL,
    VaiTro NVARCHAR(50) DEFAULT 'user',
    SoDienThoai NVARCHAR(20),
    DiaChi NVARCHAR(500),
    NgayTao DATETIME2 DEFAULT GETDATE()
)
GO

-- =============================================
-- Bảng Sản Phẩm (Products)
-- =============================================
CREATE TABLE SanPham (
    Id NVARCHAR(450) PRIMARY KEY,
    TenSanPham NVARCHAR(200) NOT NULL,
    ThuongHieu NVARCHAR(100) NOT NULL,
    Gia DECIMAL(18,2) NOT NULL,
    GiaGoc DECIMAL(18,2),
    HinhAnh NVARCHAR(500) NOT NULL,
    DanhSachHinhAnh NVARCHAR(MAX) NOT NULL,
    DanhMuc NVARCHAR(100) NOT NULL,
    CPU NVARCHAR(200) NOT NULL,
    RAM NVARCHAR(100) NOT NULL,
    LuuTru NVARCHAR(100) NOT NULL,
    CardDoHoa NVARCHAR(200) NOT NULL,
    ManHinh NVARCHAR(200) NOT NULL,
    MoTa NVARCHAR(MAX) NOT NULL,
    ThongSoKyThuat NVARCHAR(MAX) NOT NULL,
    SoLuongTon INT NOT NULL DEFAULT 0,
    DanhGia FLOAT DEFAULT 0,
    SoLuotDanhGia INT DEFAULT 0,
    NoiBat BIT DEFAULT 0
)
GO

-- =============================================
-- Bảng Đơn Hàng (Orders)
-- =============================================
CREATE TABLE DonHang (
    Id NVARCHAR(450) PRIMARY KEY,
    IdNguoiDung NVARCHAR(450) NOT NULL,
    DanhSachSanPham NVARCHAR(MAX) NOT NULL,
    TongTien DECIMAL(18,2) NOT NULL,
    TrangThai NVARCHAR(50) DEFAULT 'pending',
    NgayTao DATETIME2 DEFAULT GETDATE(),
    TenKhachHang NVARCHAR(100) NOT NULL,
    EmailKhachHang NVARCHAR(256) NOT NULL,
    SoDienThoaiKhachHang NVARCHAR(20) NOT NULL,
    DiaChiGiaoHang NVARCHAR(500) NOT NULL,
    PhuongThucThanhToan NVARCHAR(100) NOT NULL,
    
    FOREIGN KEY (IdNguoiDung) REFERENCES NguoiDung(Id)
)
GO

-- =============================================
-- Bảng Liên Hệ (ContactRequests)
-- =============================================
CREATE TABLE LienHe (
    Id NVARCHAR(450) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    Email NVARCHAR(256) NOT NULL,
    SoDienThoai NVARCHAR(20),
    TieuDe NVARCHAR(200) NOT NULL,
    NoiDung NVARCHAR(MAX) NOT NULL,
    IdSanPham NVARCHAR(450),
    TrangThai NVARCHAR(50) DEFAULT 'New',
    NgayTao DATETIME2 DEFAULT GETDATE(),
    
    FOREIGN KEY (IdSanPham) REFERENCES SanPham(Id)
)
GO

-- =============================================
-- Thêm dữ liệu mẫu
-- =============================================

-- Thêm tài khoản admin
INSERT INTO NguoiDung (Id, HoTen, Email, MatKhauMaHoa, VaiTro, SoDienThoai, DiaChi)
VALUES 
(NEWID(), N'Quản Trị Viên', 'admin@laptopstore.com', 'jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=', 'admin', '0123456789', N'123 Đường Admin, Quận 1, TP.HCM'),
(NEWID(), N'Trần Đức Tâm', 'tamdt@gmail.com', 'jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=', 'admin', '0987654321', N'456 Đường User, Quận 2, TP.HCM')
GO

-- Thêm sản phẩm mẫu
INSERT INTO SanPham (Id, TenSanPham, ThuongHieu, Gia, GiaGoc, HinhAnh, DanhSachHinhAnh, DanhMuc, CPU, RAM, LuuTru, CardDoHoa, ManHinh, MoTa, ThongSoKyThuat, SoLuongTon, DanhGia, SoLuotDanhGia, NoiBat)
VALUES 
(NEWID(), N'MacBook Pro 14" M3 Pro', 'Apple', 62000000, 67000000, 'https://images.pexels.com/photos/18105/pexels-photo.jpg', 
'["https://images.pexels.com/photos/18105/pexels-photo.jpg","https://images.pexels.com/photos/1029757/pexels-photo-1029757.jpeg"]',
N'Professional', 'Apple M3 Pro 11-core CPU', '18GB Unified Memory', '512GB SSD', '14-core GPU', N'14.2" Liquid Retina XDR (3024×1964)',
N'Siêu phẩm MacBook Pro với chip M3 Pro mạnh mẽ, màn hình Liquid Retina XDR tuyệt đẹp và thời lượng pin ấn tượng.',
'{"Processor":"Apple M3 Pro 11-core CPU","Memory":"18GB Unified Memory","Storage":"512GB SSD","Graphics":"14-core GPU","Display":"14.2 Liquid Retina XDR","Battery":"Up to 18 hours","Weight":"1.6kg","OS":"macOS Sonoma","Warranty":"1 year"}',
15, 4.9, 247, 1),

(NEWID(), N'Dell XPS 13 Plus', 'Dell', 42000000, 47000000, 'https://images.pexels.com/photos/1029757/pexels-photo-1029757.jpeg',
'["https://images.pexels.com/photos/1029757/pexels-photo-1029757.jpeg","https://images.pexels.com/photos/18105/pexels-photo.jpg"]',
N'Ultrabook', 'Intel Core i7-1360P', '16GB LPDDR5', '512GB NVMe SSD', 'Intel Iris Xe Graphics', N'13.4" 3.5K OLED Touch (3456×2160)',
N'Dell XPS 13 Plus với thiết kế tinh tế, màn hình OLED sống động và hiệu suất mạnh mẽ.',
'{"Processor":"Intel Core i7-1360P (up to 5.0GHz)","Memory":"16GB LPDDR5-5200MHz","Storage":"512GB M.2 PCIe NVMe SSD","Graphics":"Intel Iris Xe Graphics","Display":"13.4 3.5K OLED Touch","Battery":"Up to 12 hours","Weight":"1.24kg","OS":"Windows 11 Pro","Warranty":"1 year Premium Support"}',
23, 4.7, 189, 1),

(NEWID(), N'ASUS ROG Strix G16', 'ASUS', 49000000, 49000000, 'https://images.pexels.com/photos/325876/pexels-photo-325876.jpeg',
'["https://images.pexels.com/photos/325876/pexels-photo-325876.jpeg","https://images.pexels.com/photos/1029757/pexels-photo-1029757.jpeg"]',
N'Gaming', 'Intel Core i7-13650HX', '16GB DDR5', '1TB NVMe SSD', 'NVIDIA RTX 4060 8GB', N'16" FHD 165Hz IPS',
N'Laptop gaming mạnh mẽ với RTX 4060, màn hình 165Hz mượt mà và hệ thống tản nhiệt hiệu quả.',
'{"Processor":"Intel Core i7-13650HX (up to 4.9GHz)","Memory":"16GB DDR5-4800MHz","Storage":"1TB M.2 NVMe PCIe 4.0 SSD","Graphics":"NVIDIA GeForce RTX 4060 8GB GDDR6","Display":"16 FHD (1920×1080) 165Hz IPS","Battery":"Up to 8 hours","Weight":"2.5kg","OS":"Windows 11 Home","Warranty":"2 years International"}',
18, 4.6, 156, 0)
GO

-- =============================================
-- Tạo Index để tối ưu hiệu suất
-- =============================================

-- Index cho bảng NguoiDung
CREATE INDEX IX_NguoiDung_Email ON NguoiDung(Email)
CREATE INDEX IX_NguoiDung_VaiTro ON NguoiDung(VaiTro)
GO

-- Index cho bảng SanPham
CREATE INDEX IX_SanPham_ThuongHieu ON SanPham(ThuongHieu)
CREATE INDEX IX_SanPham_DanhMuc ON SanPham(DanhMuc)
CREATE INDEX IX_SanPham_Gia ON SanPham(Gia)
CREATE INDEX IX_SanPham_NoiBat ON SanPham(NoiBat)
GO

-- Index cho bảng DonHang
CREATE INDEX IX_DonHang_IdNguoiDung ON DonHang(IdNguoiDung)
CREATE INDEX IX_DonHang_TrangThai ON DonHang(TrangThai)
CREATE INDEX IX_DonHang_NgayTao ON DonHang(NgayTao)
GO

-- Index cho bảng LienHe
CREATE INDEX IX_LienHe_TrangThai ON LienHe(TrangThai)
CREATE INDEX IX_LienHe_NgayTao ON LienHe(NgayTao)
GO

-- =============================================
-- Tạo View để báo cáo
-- =============================================

-- View thống kê tổng quan
CREATE VIEW V_ThongKeTongQuan AS
SELECT 
    (SELECT COUNT(*) FROM SanPham) AS TongSoSanPham,
    (SELECT COUNT(*) FROM NguoiDung WHERE VaiTro = 'user') AS TongSoKhachHang,
    (SELECT COUNT(*) FROM DonHang) AS TongSoDonHang,
    (SELECT ISNULL(SUM(TongTien), 0) FROM DonHang WHERE TrangThai = 'delivered') AS TongDoanhThu,
    (SELECT COUNT(*) FROM LienHe WHERE TrangThai = 'New') AS LienHeChuaXuLy
GO

-- View sản phẩm bán chạy
CREATE VIEW V_SanPhamBanChay AS
SELECT TOP 10
    sp.Id,
    sp.TenSanPham,
    sp.ThuongHieu,
    sp.Gia,
    COUNT(dh.Id) AS SoLuongDaBan,
    SUM(dh.TongTien) AS DoanhThu
FROM SanPham sp
LEFT JOIN DonHang dh ON dh.DanhSachSanPham LIKE '%' + sp.Id + '%'
WHERE dh.TrangThai = 'delivered'
GROUP BY sp.Id, sp.TenSanPham, sp.ThuongHieu, sp.Gia
ORDER BY SoLuongDaBan DESC
GO

-- =============================================
-- Stored Procedures
-- =============================================

-- Procedure tìm kiếm sản phẩm
CREATE PROCEDURE SP_TimKiemSanPham
    @TuKhoa NVARCHAR(200) = '',
    @ThuongHieu NVARCHAR(100) = '',
    @DanhMuc NVARCHAR(100) = '',
    @GiaMin DECIMAL(18,2) = 0,
    @GiaMax DECIMAL(18,2) = 999999999
AS
BEGIN
    SELECT * FROM SanPham
    WHERE (@TuKhoa = '' OR TenSanPham LIKE N'%' + @TuKhoa + '%')
      AND (@ThuongHieu = '' OR ThuongHieu = @ThuongHieu)
      AND (@DanhMuc = '' OR DanhMuc = @DanhMuc)
      AND Gia BETWEEN @GiaMin AND @GiaMax
      AND SoLuongTon > 0
    ORDER BY NoiBat DESC, DanhGia DESC
END
GO

-- Procedure cập nhật trạng thái đơn hàng
CREATE PROCEDURE SP_CapNhatTrangThaiDonHang
    @IdDonHang NVARCHAR(450),
    @TrangThaiMoi NVARCHAR(50)
AS
BEGIN
    UPDATE DonHang 
    SET TrangThai = @TrangThaiMoi
    WHERE Id = @IdDonHang
    
    SELECT @@ROWCOUNT AS SoDongCapNhat
END
GO

-- =============================================
-- Triggers
-- =============================================

-- Trigger cập nhật số lượng tồn kho khi có đơn hàng mới
CREATE TRIGGER TR_CapNhatTonKho
ON DonHang
AFTER INSERT
AS
BEGIN
    -- Logic cập nhật tồn kho sẽ được xử lý trong application
    PRINT N'Đơn hàng mới đã được tạo'
END
GO

-- =============================================
-- Phân quyền
-- =============================================

-- Tạo role cho admin
CREATE ROLE AdminRole
GO

-- Tạo role cho user
CREATE ROLE UserRole
GO

-- Cấp quyền cho AdminRole
GRANT SELECT, INSERT, UPDATE, DELETE ON SanPham TO AdminRole
GRANT SELECT, INSERT, UPDATE, DELETE ON DonHang TO AdminRole
GRANT SELECT, INSERT, UPDATE, DELETE ON NguoiDung TO AdminRole
GRANT SELECT, INSERT, UPDATE, DELETE ON LienHe TO AdminRole
GO

-- Cấp quyền cho UserRole
GRANT SELECT ON SanPham TO UserRole
GRANT SELECT, INSERT ON DonHang TO UserRole
GRANT SELECT, UPDATE ON NguoiDung TO UserRole
GRANT SELECT, INSERT ON LienHe TO UserRole
GO

PRINT N'Tạo cơ sở dữ liệu LaptopShop thành công!'
PRINT N'- Bảng: NguoiDung, SanPham, DonHang, LienHe'
PRINT N'- View: V_ThongKeTongQuan, V_SanPhamBanChay'
PRINT N'- Stored Procedures: SP_TimKiemSanPham, SP_CapNhatTrangThaiDonHang'
PRINT N'- Dữ liệu mẫu đã được thêm'
GO