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
    }
}
