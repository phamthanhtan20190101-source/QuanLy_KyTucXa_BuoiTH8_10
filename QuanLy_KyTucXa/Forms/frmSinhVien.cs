using QuanLy_KyTucXa.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLy_KyTucXa.Forms
{
    public partial class frmSinhVien : Form
    {
        QLKTXDbContext context = new QLKTXDbContext();
        private string mssvDangNhap;
        public frmSinhVien()
        {
            InitializeComponent();

        }

        private void frmSinhVien_Load(object sender, EventArgs e)
        {
            LoadThongTinSinhVien();
        }

        private void LoadThongTinSinhVien()
        {
            try
            {
                // 1. Lấy MSSV từ người dùng đang đăng nhập
                mssvDangNhap = frmMain.MaNguoiDungHienTai;
                if (string.IsNullOrEmpty(mssvDangNhap)) return;

                // 2. Truy vấn thông tin sinh viên
                var sv = context.SinhViens.FirstOrDefault(s => s.MSSV.Trim() == mssvDangNhap.Trim());

                if (sv != null)
                {
                    // Hiển thị thông tin cá nhân
                    txtmssv.Text = sv.MSSV;
                    txthoten.Text = sv.HoTen;
                    txtlop.Text = sv.Lop;
                    txtsdt.Text = sv.SDT;
                    txtquequan.Text = sv.QueQuan;
                    txtgioitinh.Text = sv.GioiTinh;
                    txtmaphong.Text = sv.MaPhong;
                    txtTienNo.Text = sv.TienNo.ToString("N0") + " VNĐ";
                    txtngaysinh.Text = sv.NgaySinh.ToString("dd/MM/yyyy");
                    txtngayvao.Text = sv.NgayVao.ToString("dd/MM/yyyy");

                    // --- PHẦN CẬP NHẬT: THAY MÃ GIAO DỊCH BẰNG MSSV ---
                    var lichSu = context.LichSuDongTiens
                        .Where(l => l.MSSV.Trim() == mssvDangNhap.Trim())
                        .OrderByDescending(l => l.NgayDong)
                        .Select(l => new {
                            MSSV = l.MSSV, // Đã đổi l.MaThanhToan thành l.MSSV
                            Tháng = l.ThangDongTien,
                            Năm = l.NamDongTien,
                            Số_Tiền = l.SoTien,
                            Ngày_Đóng = l.NgayDong
                        })
                        .ToList();

                    dgvLichSuDongTien.DataSource = null;
                    dgvLichSuDongTien.DataSource = lichSu;

                    // Định dạng lại giao diện bảng
                    if (dgvLichSuDongTien.Columns.Count > 0)
                    {
                        dgvLichSuDongTien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dgvLichSuDongTien.Columns["Số_Tiền"].DefaultCellStyle.Format = "N0";
                        dgvLichSuDongTien.Columns["Ngày_Đóng"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                        dgvLichSuDongTien.Columns["Số_Tiền"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // Hàm dùng chung để lấy thông tin Tòa nhà, Quản lý và gọi in PDF
        private void TaoDonTuDong(string tenLoaiDon)
        {
            try
            {
                string mssv = frmMain.MaNguoiDungHienTai;
                var sv = context.SinhViens.FirstOrDefault(s => s.MSSV == mssv);

                if (sv == null || string.IsNullOrEmpty(sv.MaPhong))
                {
                    MessageBox.Show("Bạn chưa được xếp phòng nên không thể tạo đơn này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Truy vấn logic: Sinh viên -> Phòng -> Tòa Nhà
                var phong = context.Phongs.FirstOrDefault(p => p.MaPhong == sv.MaPhong);
                string tenToaNha = "Không xác định";

                
                string tenQuanLy = "...................................";

                if (phong != null)
                {
                    var toaNha = context.ToaNhas.FirstOrDefault(t => t.MaToaNha == phong.MaToaNha);
                    if (toaNha != null)
                    {
                        tenToaNha = toaNha.MaToaNha;
                       
                    }
                }

                // Gọi hàm bên PdfHelper để xuất file
                PdfHelper.XuatDonPDF(tenLoaiDon, sv, tenToaNha, tenQuanLy);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDonXinChuyenPhong_Click(object sender, EventArgs e)
        {
            TaoDonTuDong("CHUYỂN PHÒNG");
        }

        private void btnXinRoiKTX_Click(object sender, EventArgs e)
        {
            TaoDonTuDong("RỜI KÝ TÚC XÁ");
        }
    }
}
