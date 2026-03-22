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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLy_KyTucXa.Forms
{
    public partial class CapNhatDongTien : Form
    {
        QLKTXDbContext context = new QLKTXDbContext();
        public CapNhatDongTien()
        {
            InitializeComponent();
        }

        private void CapNhatDongTien_Load(object sender, EventArgs e)
        {
            txtmssv.ReadOnly = true;
            txthoten.ReadOnly = true;
            txtlop.ReadOnly = true;
            txtsdt.ReadOnly = true;
            txtTienNo.ReadOnly = true; // Tiền nợ thường do hệ thống tính, không nên nhập tay

            var listPhong = context.Phongs.Select(p => p.MaPhong).ToList();
            listPhong.Insert(0, "Tất cả các phòng");
            cobtimkiem.DataSource = listPhong;

            // Tải dữ liệu lên lưới
            LoadData();

        }
        private void LoadData()
        {
            try
            {
                // Ngăn DataGridView tự tạo thêm cột thừa
                dataGridView.AutoGenerateColumns = false;

                // Nối cột trên giao diện với cột trong Database (Thuộc tính DataPropertyName)
                dataGridView.Columns["MSSV"].DataPropertyName = "MSSV";
                dataGridView.Columns["HoTen"].DataPropertyName = "HoTen";
                dataGridView.Columns["Lop"].DataPropertyName = "Lop";
                dataGridView.Columns["SDT"].DataPropertyName = "SDT";
                dataGridView.Columns["NgayVao"].DataPropertyName = "NgayVao";
                dataGridView.Columns["MaPhong"].DataPropertyName = "MaPhong";

                // Lấy danh sách sinh viên từ CSDL và sắp xếp theo tên
                var listSV = context.SinhViens.OrderBy(sv => sv.HoTen).ToList();

                // Đổ dữ liệu vào lưới
                dataGridView.DataSource = listSV;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.RowIndex < 0 || dataGridView.Rows[e.RowIndex].IsNewRow)
                return;

            DataGridViewRow row = dataGridView.Rows[e.RowIndex];

            // 1. Gán thông tin cơ bản
            txtmssv.Text = row.Cells["MSSV"].Value?.ToString();
            txthoten.Text = row.Cells["HoTen"].Value?.ToString();
            txtlop.Text = row.Cells["Lop"].Value?.ToString();
            txtsdt.Text = row.Cells["SDT"].Value?.ToString();

            // 2. Lấy Mã Phòng của sinh viên đang được click
            string maPhong = row.Cells["MaPhong"].Value?.ToString();

            // 3. Truy vấn tìm phòng và tính tiền nợ
            if (!string.IsNullOrEmpty(maPhong))
            {
                // Tìm phòng trong CSDL
                var phong = context.Phongs.FirstOrDefault(p => p.MaPhong == maPhong);

                if (phong != null)
                {
                    // Tính tổng tiền = Giá + Tiền điện nước
                    decimal tienNo = phong.Gia + phong.TienDienNuoc;

                    // Gán vào Textbox. Hàm ToString("N0") giúp thêm dấu phẩy ngăn cách hàng nghìn (VD: 1,500,000)
                    txtTienNo.Text = tienNo.ToString("N0");
                }
                else
                {
                    txtTienNo.Text = "0"; // Nếu không tìm thấy phòng (hiếm khi xảy ra)
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txttimkiem.Text.Trim();
            bool timNguoiNo = checkSVno.Checked; // Lấy trạng thái của Checkbox mới

            // 1. KIỂM TRA TEXTBOX CÓ TRỐNG HAY KHÔNG
            if (string.IsNullOrEmpty(tuKhoa))
            {
                if (timNguoiNo)
                {
                    // Nếu TextBox trống NHƯNG có tick "Sinh viên nợ" -> Vẫn cho phép hiện danh sách nợ
                    var listNo = context.SinhViens
                        .Where(sv => sv.Phong != null && (sv.Phong.Gia + sv.Phong.TienDienNuoc) > 0)
                        .OrderBy(sv => sv.HoTen).ToList();
                    dataGridView.DataSource = listNo;
                }
                else
                {
                    // Nếu TextBox trống VÀ KHÔNG tick gì cả -> Hiện cảnh báo bắt buộc nhập
                    MessageBox.Show("Vui lòng nhập MSSV hoặc Tên sinh viên để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txttimkiem.Focus();
                }
                return; // Dừng lại, không chạy code tìm kiếm bên dưới nữa
            }

            // 2. NẾU TEXTBOX CÓ CHỮ -> Tìm kiếm theo MSSV hoặc Tên
            var query = context.SinhViens.Where(sv => sv.MSSV.Contains(tuKhoa) || sv.HoTen.Contains(tuKhoa));

            // Nếu lúc này đang tick "Sinh viên nợ" thì kết hợp tìm "Những người tên đó MÀ còn nợ tiền"
            if (timNguoiNo)
            {
                query = query.Where(sv => sv.Phong != null && (sv.Phong.Gia + sv.Phong.TienDienNuoc) > 0);
            }

            var ketQua = query.OrderBy(sv => sv.HoTen).ToList();
            dataGridView.DataSource = ketQua;

            // Nếu tìm không ra ai
            if (ketQua.Count == 0)
            {
                MessageBox.Show("Không tìm thấy sinh viên nào khớp với điều kiện!", "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cobtimkiem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobtimkiem.SelectedItem == null) return;

            string phongChon = cobtimkiem.Text;

            if (phongChon == "Tất cả các phòng")
            {
                LoadData(); // Nếu chọn "Tất cả" thì gọi lại hàm load toàn bộ
            }
            else
            {
                // Lọc ra sinh viên của phòng được chọn
                var listTheoPhong = context.SinhViens
                    .Where(sv => sv.MaPhong == phongChon)
                    .OrderBy(sv => sv.HoTen).ToList();
                dataGridView.DataSource = listTheoPhong;
            }


        }

        private void checkSVno_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSVno.Checked)
            {
                // Khi tick vào -> Hiện tất cả sinh viên còn nợ tiền
                var listNo = context.SinhViens
                    .Where(sv => sv.Phong != null && (sv.Phong.Gia + sv.Phong.TienDienNuoc) > 0)
                    .OrderBy(sv => sv.HoTen).ToList();
                dataGridView.DataSource = listNo;
            }
            else
            {
                // Khi bỏ tick -> Load lại toàn bộ danh sách ban đầu
                LoadData();
            }
        }
    }
}
