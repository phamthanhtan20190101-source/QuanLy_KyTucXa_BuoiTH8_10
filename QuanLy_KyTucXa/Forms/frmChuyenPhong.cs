using QuanLy_KyTucXa.Data;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QuanLy_KyTucXa.Forms
{
    public partial class frmChuyenPhong : Form
    {
        QLKTXDbContext context = new QLKTXDbContext();
        public string mssvCanChuyen = "";

        public frmChuyenPhong()
        {
            InitializeComponent();
        }

        private void frmChuyenPhong_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(mssvCanChuyen))
                {
                    txtMSSV.Text = mssvCanChuyen;
                    LoadThongTinHienTai();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message, "Lỗi");
            }
        }

        private void LoadThongTinHienTai()
        {
            string maSV = txtMSSV.Text.Trim();
            if (string.IsNullOrEmpty(maSV))
            {
                txtPhongCu.Text = "";
                txtHoTen.Text = "";
                cobPhongMoi.DataSource = null;
                return;
            }

            var sv = context.SinhViens.FirstOrDefault(s => s.MSSV == maSV);
            if (sv != null)
            {
                txtHoTen.Text = sv.HoTen;
                txtPhongCu.Text = string.IsNullOrEmpty(sv.MaPhong) ? "Chưa xếp phòng" : sv.MaPhong;

                string gioiTinhSV = sv.GioiTinh != null ? sv.GioiTinh.Trim().ToLower() : "";

                // LỌC DANH SÁCH PHÒNG THEO QUY ĐỊNH: A, C = Nam | B, D = Nữ
                var danhSachPhongPhuHop = context.Phongs
                    .AsEnumerable()
                    .Where(p => {
                        if (string.IsNullOrEmpty(p.MaPhong)) return false;
                        string toaMoi = p.MaPhong.Substring(0, 1).ToUpper();
                        bool hopLe = false;

                        if (gioiTinhSV == "nam") { if (toaMoi == "A" || toaMoi == "C") hopLe = true; }
                        else { if (toaMoi == "B" || toaMoi == "D") hopLe = true; }

                        // LoaiPhong là int (sức chứa)
                        return hopLe && p.SoLuongDangO < p.LoaiPhong;
                    })
                    .Select(p => p.MaPhong)
                    .ToList();

                cobPhongMoi.DataSource = danhSachPhongPhuHop;
            }
        }

        private void txtMSSV_TextChanged(object sender, EventArgs e)
        {
            LoadThongTinHienTai();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string maSV = txtMSSV.Text.Trim();
            string phongCu = txtPhongCu.Text;
            string phongMoi = cobPhongMoi.Text;

            if (string.IsNullOrEmpty(maSV) || string.IsNullOrEmpty(phongMoi))
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin!", "Thông báo");
                return;
            }

            try
            {
                // Truy vấn dữ liệu mới nhất từ Context
                var sinhVien = context.SinhViens.FirstOrDefault(s => s.MSSV == maSV);
                var objPhongMoi = context.Phongs.FirstOrDefault(p => p.MaPhong == phongMoi);

                if (sinhVien == null || objPhongMoi == null) return;

                // =============================================================
                // 1. KIỂM TRA NỢ (BẢN SỬA LỖI TRIỆT ĐỂ CHO TÂN)
                // =============================================================

                // Kiểm tra trực tiếp trên thuộc tính TienNo của SinhVien
                bool conNoTrongHoSo = sinhVien.TienNo > 0;

                // Kiểm tra chi tiết trong bảng TienNos
                // Dùng các từ khóa "Chưa" hoặc "Nợ" để bao quát hết các trạng thái chưa đóng tiền
                var cacKhoanNo = context.TienNos
                    .Where(t => t.MSSV == maSV)
                    .AsEnumerable()
                    .Where(t => t.TrangThai.Contains("Chưa") || t.TrangThai.Contains("chưa") ||
                                t.TrangThai.Contains("Nợ") || t.TrangThai.Contains("no") ||
                                t.SoTienNo > 0)
                    .ToList();

                if (conNoTrongHoSo || cacKhoanNo.Count > 0)
                {
                    decimal tongNo = conNoTrongHoSo ? sinhVien.TienNo : cacKhoanNo.Sum(t => t.SoTienNo);

                    MessageBox.Show($"LỖI: Không thể chuyển phòng!\nSinh viên {sinhVien.HoTen} còn nợ {tongNo:N0} VNĐ.\nYêu cầu thanh toán hết nợ trước khi thực hiện.",
                                    "Vi phạm ràng buộc tài chính", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return; // Chặn đứng thao tác
                }

                // =============================================================
                // 2. KIỂM TRA GIỚI TÍNH TÒA NHÀ
                // =============================================================
                string toaMoi = objPhongMoi.MaPhong.Substring(0, 1).ToUpper();
                string gt = sinhVien.GioiTinh.Trim().ToLower();
                bool hopLe = (gt == "nam" && (toaMoi == "A" || toaMoi == "C")) ||
                             (gt != "nam" && (toaMoi == "B" || toaMoi == "D"));

                if (!hopLe)
                {
                    MessageBox.Show($"Tòa {toaMoi} không khớp với giới tính {sinhVien.GioiTinh}!", "Lỗi");
                    return;
                }

                // 3. XÁC THỰC MẬT KHẨU
                frmXacNhanMatKhau frm = new frmXacNhanMatKhau();
                if (frm.ShowDialog() != DialogResult.OK) return;

                // THỰC HIỆN CẬP NHẬT TRONG SQL
                var objPhongCu = context.Phongs.FirstOrDefault(p => p.MaPhong == phongCu);
                if (objPhongCu != null) objPhongCu.SoLuongDangO -= 1;

                objPhongMoi.SoLuongDangO += 1;
                sinhVien.MaPhong = objPhongMoi.MaPhong;

                context.SaveChanges();
                MessageBox.Show("Chuyển phòng thành công!", "Thông báo");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}