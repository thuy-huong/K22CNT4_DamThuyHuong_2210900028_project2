create database K22CNT4_DamThuyHuong_2210900028_Project2
go
use K22CNT4_DamThuyHuong_2210900028_Project2
CREATE TABLE quanTri(
	maQuanTri INT IDENTITY(1,1) PRIMARY KEY ,
    tenQuanTri NVARCHAR(100) NOT NULL,
	email VARCHAR(255) UNIQUE NOT NULL,
    matKhau VARCHAR(255) NOT NULL,
	diaChi NVARCHAR(255),
	trangThai bit  DEFAULT 1,
)
CREATE TABLE banner(
	maBanner INT IDENTITY(1,1) PRIMARY KEY,
	hinhAnh NVARCHAR(255),
	trangThai bit DEFAULT 1
)

CREATE TABLE danhMuc(
    maDanhMuc INT IDENTITY(1,1) PRIMARY KEY ,
    tenDanhMuc NVARCHAR(100) NOT NULL,
    maDanhMucCha INT NULL,
	trangThai bit  DEFAULT 1,
    ngayTao  DATETIME DEFAULT CURRENT_TIMESTAMP,
    ngayCapNhat DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (maDanhMucCha) REFERENCES danhMuc(maDanhMuc)
)

CREATE TABLE sanPham (
    maSanPham INT PRIMARY KEY IDENTITY(1,1),
    tenSanPham NVARCHAR(255) NOT NULL,
    moTa NVARCHAR(255) NOT NULL,
    gia DECIMAL(10, 2) NOT NULL,
	hinhAnh NVARCHAR(255),
    mauSac NVARCHAR(50),
    soLuong INT NOT NULL,
    maDanhMuc INT,
	trangThai bit  DEFAULT 1,
    ngayTao DATETIME DEFAULT CURRENT_TIMESTAMP,
    ngayCapNhat DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (maDanhMuc) REFERENCES danhMuc(maDanhMuc)
)
CREATE TABLE khachHang (
    maKhachHang INT PRIMARY KEY IDENTITY(1,1),
    tenKhachHang NVARCHAR(255) NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    matKhau VARCHAR(255) NOT NULL,
    dienThoai VARCHAR(15),
    diaChi NVARCHAR(255),
	trangThai bit  DEFAULT 1,
    ngayTao DATETIME DEFAULT CURRENT_TIMESTAMP,
    ngayCapNhat DATETIME DEFAULT CURRENT_TIMESTAMP,
	hinhAnh NVARCHAR(255) DEFAULT N'DEFAULT_USER.jpg'
)

CREATE TABLE donHang (
    maDonHang INT PRIMARY KEY IDENTITY(1,1),
    maKhachHang INT,
	tenKhachHang nvarchar(255),
    tongTien DECIMAL(10, 2) NOT NULL,
    diaChiGiaoHang NVARCHAR(255),
	dienthoai varchar(12),
    phiVanChuyen DECIMAL(10, 2) DEFAULT 0.00,
    phuongThucThanhToan INT CHECK (phuongThucThanhToan IN (1, 2, 3)) DEFAULT 1,  -- 1:Tiền mặt khi giao hàng', 2' thẻ tín dụng', 3'PayPal
	trangThai INT CHECK (trangThai IN (1, 2, 3, 4)) DEFAULT 1,  -- 1: Chờ xử lý, 2: Đang xử lý, 3: Đã giao, 4: Đã hủy
    ngayTao DATETIME DEFAULT CURRENT_TIMESTAMP,
    ngayCapNhat DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (maKhachHang) REFERENCES khachHang(maKhachHang)
)

CREATE TABLE chiTietDonHang (
    maChiTiet INT PRIMARY KEY IDENTITY(1,1),
    maDonHang INT,
    maSanPham INT,
    soLuong INT NOT NULL,
    gia DECIMAL(10, 2) NOT NULL,
    ngayTao DATETIME DEFAULT CURRENT_TIMESTAMP,
    ngayCapNhat DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (maDonHang) REFERENCES donHang(maDonHang),
    FOREIGN KEY (maSanPham) REFERENCES sanPham(maSanPham)
)

CREATE TABLE danhGia (
    maManhGia INT PRIMARY KEY IDENTITY(1,1),
    maSanPham INT,
    maKhachHang INT,
    xepHang INT CHECK (xepHang BETWEEN 1 AND 5),
    binhLuan NVARCHAR(255),
    ngayTao DATETIME DEFAULT CURRENT_TIMESTAMP,
    ngayCapNhat DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (maSanPham) REFERENCES sanPham(maSanPham),
    FOREIGN KEY (maKhachHang) REFERENCES khachHang(maKhachHang)
)
INSERT INTO quanTri(tenQuanTri, email, matKhau, diaChi) VALUES(N'Thúy Hường', 'admin@gmail.com','admin123', N'Hà Nội')

INSERT INTO danhMuc(tenDanhMuc) VALUES (N'LIP')
INSERT INTO danhMuc(tenDanhMuc) VALUES (N'EYE')
INSERT INTO danhMuc(tenDanhMuc) VALUES (N'FACE')
INSERT INTO danhMuc(tenDanhMuc, maDanhMucCha) VALUES (N'Merzy Cyber Mellow Tint', 1)
INSERT INTO danhMuc(tenDanhMuc, maDanhMucCha) VALUES (N'Merzy Academia', 1)
INSERT INTO danhMuc(tenDanhMuc, maDanhMucCha) VALUES (N'Soft Touch Lip Tint', 1)
INSERT INTO danhMuc(tenDanhMuc, maDanhMucCha) VALUES (N'The First Velvet Tint', 1)
INSERT INTO danhMuc(tenDanhMuc, maDanhMucCha) VALUES (N'Bite The Beat Mellow Tint', 1)
INSERT INTO danhMuc(tenDanhMuc, maDanhMucCha) VALUES (N'Contact Lens', 2)
INSERT INTO danhMuc(tenDanhMuc, maDanhMucCha) VALUES (N'Mascara', 2)
INSERT INTO danhMuc(tenDanhMuc, maDanhMucCha) VALUES (N'Eyeshadow', 2)
INSERT INTO danhMuc(tenDanhMuc, maDanhMucCha) VALUES (N'Eyebrow', 2)
INSERT INTO danhMuc(tenDanhMuc, maDanhMucCha) VALUES (N'Foundation', 3)
INSERT INTO danhMuc(tenDanhMuc, maDanhMucCha) VALUES (N'Cushion', 3)
INSERT INTO danhMuc(tenDanhMuc, maDanhMucCha) VALUES (N'Blush', 3)
INSERT INTO danhMuc(tenDanhMuc, maDanhMucCha) VALUES (N'Watery Dew Tint', 1)

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac, hinhAnh)
VALUES (N'Son Kem Lì Merzy Cyber Mellow Tint #CM1 Hidden Section', 
	N'Chất son kem lì được cải tiến siêu mịn phủ đầy đôi môi quyến rũ, bền màu nhiều giờ mà vẫn không gây cảm giác khô nứt.', 
	196000, 20, 4, N'Cam nâu đào', 'cm1.webp')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac, hinhAnh)
VALUES (N'Son Kem Lì Merzy Cyber Mellow Tint #CM2 Narrative Rose', 
	N'Chất son kem lì được cải tiến siêu mịn phủ đầy đôi môi quyến rũ, bền màu nhiều giờ mà vẫn không gây cảm giác khô nứt.', 
	196000, 20, 4, N'Hồng nâu', 'cm2.webp')


INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac, hinhAnh)
VALUES (N'Son Kem Lì Merzy Cyber Mellow Tint #CM3 Metallic Mauve', 
	N'Chất son kem lì được cải tiến siêu mịn phủ đầy đôi môi quyến rũ, bền màu nhiều giờ mà vẫn không gây cảm giác khô nứt.', 
	196000, 20, 4, N'Hồng tro', 'cm3.webp')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac, hinhAnh)
VALUES (N'Son Kem Lì Merzy Cyber Mellow Tint #CM4 Tempting Red', 
	N'Chất son kem lì được cải tiến siêu mịn phủ đầy đôi môi quyến rũ, bền màu nhiều giờ mà vẫn không gây cảm giác khô nứt.', 
	196000, 20, 4, N'Đỏ cánh hồng', 'cm4.webp')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac, hinhAnh)
VALUES (N'Son Kem Lì Merzy Cyber Mellow Tint #CM5 Bad Rule', 
	N'Chất son kem lì được cải tiến siêu mịn phủ đầy đôi môi quyến rũ, bền màu nhiều giờ mà vẫn không gây cảm giác khô nứt.', 
	196000, 20, 4, N'Đỏ cam gạch', 'cm5.webp')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac, hinhAnh)
VALUES (N'Son Kem Lì Merzy Cyber Mellow Tint #CM6 Brick Emotion', 
	N'Chất son kem lì được cải tiến siêu mịn phủ đầy đôi môi quyến rũ, bền màu nhiều giờ mà vẫn không gây cảm giác khô nứt.', 
	196000, 20, 4, N'Cam nâu đất', 'cm6.webp')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac)
VALUES (N'Son Kem Lì Merzy Cyber Mellow Tint #CM6 Brick Emotion', 
	N'Chất son kem lì được cải tiến siêu mịn phủ đầy đôi môi quyến rũ, bền màu nhiều giờ mà vẫn không gây cảm giác khô nứt.', 
	196000, 20, 4, N'Cam nâu đất')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac)
VALUES (N'Son Kem Lì Merzy Bite The Beat Mellow Tint #M1', 
	N'Son kem Merzy Bite The Beat Mellow Tint (Ver City) thiết kế vỏ son có màu sắc đặc trưng được thể hiện giống với màu son bên trong. 
	Khả năng lên màu cực kỳ chuẩn, bền màu, không lem trôi.', 
	139000, 20, 8, N'Đỏ nâu trầm')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac)
VALUES (N'Son Kem Lì Merzy Bite The Beat Mellow Tint #M2', 
	N'Son kem Merzy Bite The Beat Mellow Tint (Ver City) thiết kế vỏ son có màu sắc đặc trưng được thể hiện giống với màu son bên trong. 
	Khả năng lên màu cực kỳ chuẩn, bền màu, không lem trôi.', 
	99000, 20, 8, N'Đỏ nâu gạch')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac)
VALUES (N'Son Kem Lì Merzy Bite The Beat Mellow Tint #M3', 
	N'Son kem Merzy Bite The Beat Mellow Tint (Ver City) thiết kế vỏ son có màu sắc đặc trưng được thể hiện giống với màu son bên trong. 
	Khả năng lên màu cực kỳ chuẩn, bền màu, không lem trôi.', 
	159000, 20, 8, N'Cam đất')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac)
VALUES (N'Son Kem Lì Merzy Bite The Beat Mellow Tint #M4', 
	N'Son kem Merzy Bite The Beat Mellow Tint (Ver City) thiết kế vỏ son có màu sắc đặc trưng được thể hiện giống với màu son bên trong. 
	Khả năng lên màu cực kỳ chuẩn, bền màu, không lem trôi.', 
	129000, 20, 8, N'Nâu mận')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac)
VALUES (N'Son Kem Lì Merzy Bite The Beat Mellow Tint #M5', 
	N'Son kem Merzy Bite The Beat Mellow Tint (Ver City) thiết kế vỏ son có màu sắc đặc trưng được thể hiện giống với màu son bên trong. 
	Khả năng lên màu cực kỳ chuẩn, bền màu, không lem trôi.', 
	139000, 20, 8, N'Cam cháy')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac)
VALUES (N'Son Kem Lì Merzy Bite The Beat Mellow Tint #M6', 
	N'Son kem Merzy Bite The Beat Mellow Tint (Ver City) thiết kế vỏ son có màu sắc đặc trưng được thể hiện giống với màu son bên trong. 
	Khả năng lên màu cực kỳ chuẩn, bền màu, không lem trôi.', 
	159000, 20, 8, N'Đỏ ruby ánh nâu')


INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac)
VALUES (N'(Blue Dream Edition) Son Kem Lì Merzy The First Velvet Tint #V6', 
	N'Phiên bản Blue Dream ảo diệu với thiết kế thời thương, hiệu ứng xanh dương - tím hologram độc đáo.
	Màu son đỏ huyền thoại, phù hợp với mọi màu da với lớp tint cuối chuẩn màu son đầu.
	Khả năng lên màu cực kỳ chuẩn, bền màu, không lem trôi.', 
	179000, 20, 7, N'Đỏ gạch')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac)
VALUES (N'(Ver.Siren) Son Kem Lì Merzy The First Velvet Tint #V6', 
	N'Phiên bản Siren độc đáo được lấy cảm hứng từ vẻ đẹp của mỹ nhân ngư với thiết kế mới nữ tính, kiêu sa.
	Màu son đỏ huyền thoại, phù hợp với mọi màu da với lớp tint cuối chuẩn màu son đầu.', 
	169000, 20, 7, N'Đỏ gạch')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac)
VALUES (N' (Ver.Green) Son Kem Lì Merzy The First Velvet Tint #V6', 
	N'Son kem Merzy The First Velvet Tint với phiên bản giới hạn Green Edition nổi bật hoàn toàn mới.
	Màu son đỏ huyền thoại, phù hợp với mọi màu da với lớp tint cuối chuẩn màu son đầu.', 
	169000, 20, 7, N'Đỏ gạch')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac)
VALUES (N'(Ver.Siren Holiday) Son Kem Lì Merzy The First Velvet Tint #V6', 
	N'Son kem Merzy The First Velvet Tint với phiên bản giới hạn Limited Edition - V6 Siren Holiday nổi bật với thiết kế bắt mắt, sang trọng.
	Màu son đỏ huyền thoại, phù hợp với mọi màu da với lớp tint cuối chuẩn màu son đầu.', 
	169000, 20, 7, N'Đỏ gạch')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac)
VALUES (N'(Ver Red) Son Kem Lì Merzy The First Velvet Tint #V16', 
	N'Son kem Merzy The First Velvet Tint Season 3 (Ver Red) thiết kế mang phong cách gây ấn tượng với gam màu đỏ bắt mắt	.
	Kết cấu nhung mịn, chuẩn màu ngay từ lần lướt cọ đầu tiên.', 
	159000, 20, 7, N'Đỏ gạch')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac)
VALUES (N'(Ver Red) Son Kem Lì Merzy The First Velvet Tint #V16', 
	N'Son kem Merzy The First Velvet Tint Season 3 (Ver Red) thiết kế mang phong cách gây ấn tượng với gam màu đỏ bắt mắt	.
	Kết cấu nhung mịn, chuẩn màu ngay từ lần lướt cọ đầu tiên.', 
	159000, 20, 7, N'Đỏ gạch')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac)
VALUES (N'Son Tint Bóng Merzy The Watery Dew Tint #WD01', 
	N' Son Tint Bóng Merzy The Watery Dew Tint với kết cấu son tint bóng có độ ẩm cao như một lớp màng nước phủ trên bề mặt và độ bám màu tốt.
	Không gây bết dính và nặng môi để lại một lớp màng phủ sương ẩm nhẹ.', 
	169000, 20, 16, N'Cam tươi rạng rỡ')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac)
VALUES (N'Son Tint Bóng Merzy The Watery Dew Tint #WD02', 
	N' Son Tint Bóng Merzy The Watery Dew Tint với kết cấu son tint bóng có độ ẩm cao như một lớp màng nước phủ trên bề mặt và độ bám màu tốt.
	Không gây bết dính và nặng môi để lại một lớp màng phủ sương ẩm nhẹ.', 
	169000, 20, 16, N'Hồng đất MLBB')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac)
VALUES (N' Son Tint Bóng Merzy The Watery Dew Tint #WD03', 
	N' Son Tint Bóng Merzy The Watery Dew Tint với kết cấu son tint bóng có độ ẩm cao như một lớp màng nước phủ trên bề mặt và độ bám màu tốt.
	Không gây bết dính và nặng môi để lại một lớp màng phủ sương ẩm nhẹ.', 
	169000, 20, 16, N'Hồng lạnh')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac)
VALUES (N'Son Tint Bóng Merzy The Watery Dew Tint #WD04', 
	N' Son Tint Bóng Merzy The Watery Dew Tint với kết cấu son tint bóng có độ ẩm cao như một lớp màng nước phủ trên bề mặt và độ bám màu tốt.
	Không gây bết dính và nặng môi để lại một lớp màng phủ sương ẩm nhẹ.', 
	169000, 20, 16, N'Đỏ tươi ánh cam')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac)
VALUES (N'Son Tint Bóng Merzy The Watery Dew Tint #WD05', 
	N' Son Tint Bóng Merzy The Watery Dew Tint với kết cấu son tint bóng có độ ẩm cao như một lớp màng nước phủ trên bề mặt và độ bám màu tốt.
	Không gây bết dính và nặng môi để lại một lớp màng phủ sương ẩm nhẹ.', 
	169000, 20, 16, N'Đỏ cherry')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac)
VALUES (N'Son Tint Bóng Merzy The Watery Dew Tint #WD06', 
	N'Son Tint Bóng Merzy The Watery Dew Tint với kết cấu son tint bóng có độ ẩm cao như một lớp màng nước phủ trên bề mặt và độ bám màu tốt.
	Không gây bết dính và nặng môi để lại một lớp màng phủ sương ẩm nhẹ.', 
	169000, 20, 16, N'Cam beige ánh nâu')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac)
VALUES (N'Son Tint Bóng Merzy The Watery Dew Tint #WD16', 
	N'Son Tint Bóng Merzy The Watery Dew Tint với kết cấu son tint bóng có độ ẩm cao như một lớp màng nước phủ trên bề mặt và độ bám màu tốt.
	Không gây bết dính và nặng môi để lại một lớp màng phủ sương ẩm nhẹ.', 
	169000, 20, 16, N'Đỏ nâu vampire')

INSERT INTO sanPham(tenSanPham,moTa, gia, soLuong, maDanhMuc, mauSac)
VALUES (N'Son Tint Bóng Merzy The Watery Dew Tint #WD22', 
	N'Son Tint Bóng Merzy The Watery Dew Tint với kết cấu son tint bóng có độ ẩm cao như một lớp màng nước phủ trên bề mặt và độ bám màu tốt.
	Không gây bết dính và nặng môi để lại một lớp màng phủ sương ẩm nhẹ.', 
	169000, 20, 16, N'Đỏ cam trà')