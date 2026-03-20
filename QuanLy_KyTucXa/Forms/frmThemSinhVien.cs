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
    public partial class frmThemSinhVien : Form
    {
        QLKTXDbContext context = new QLKTXDbContext();

        // Các biến trạng thái
        bool xuLyThem = false;
        string currentMSSV = "";

        public frmThemSinhVien()
        {
            InitializeComponent();
        }

        private void BatTatChucNang(bool giaTri)
        {
            btnLuu.Enabled = giaTri;
            btnHuyBo.Enabled = giaTri;

            txtmssv.Enabled = giaTri;
            txthoten.Enabled = giaTri;
            txtlop.Enabled = giaTri;
            txtsdt.Enabled = giaTri;
            txtquequan.Enabled = giaTri;
            cobgioitinh.Enabled = giaTri;
            cobmaphong.Enabled = giaTri;
            datengaysinh.Enabled = giaTri;
            datengayvao.Enabled = giaTri;

            btnThem.Enabled = !giaTri;
            btnSua.Enabled = !giaTri;
            btnXoa.Enabled = !giaTri;
        }

        private void frmThemSinhVien_Load(object sender, EventArgs e)
        {
            LoadComboboxPhong(); // Chỉ cần tải danh sách phòng 1 lần khi mở Form
            ResetForm();         // Gọi hàm Reset để đưa form về trạng thái ban đầu
        }

        // Tạo riêng hàm ResetForm để load lại lưới và khóa nút mà không cần load lại ComboBox
        private void ResetForm()
        {
            BatTatChucNang(false);
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dataGridView.AutoGenerateColumns = false;
                List<SinhVien> listSV = context.SinhViens.OrderBy(sv => sv.HoTen).ToList();
                dataGridView.DataSource = listSV;

                if (dataGridView.Columns["Phong"] != null)
                    dataGridView.Columns["Phong"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void LoadComboboxPhong(string gioiTinh = "")
        {
            var query = context.Phongs.AsQueryable();

            // Dùng MaToaNha để lọc vì dữ liệu cột này lưu chính xác là "A", "B", "C", "D"
            if (gioiTinh == "Nam")
            {
                query = query.Where(p => p.ToaNha.MaToaNha == "A" || p.ToaNha.MaToaNha == "C");
            }
            else if (gioiTinh == "Nữ")
            {
                query = query.Where(p => p.ToaNha.MaToaNha == "B" || p.ToaNha.MaToaNha == "D");
            }

            var listPhong = query
                .Select(p => new
                {
                    p.MaPhong,
                    TenToaNha = p.ToaNha.TenToaNha
                })
                .ToList();

            // Xóa bộ nhớ đệm cũ trước khi đổ dữ liệu mới
            cobmaphong.DataSource = null;

            // Đổ dữ liệu đã lọc ra
            cobmaphong.DataSource = listPhong;
            cobmaphong.DisplayMember = "MaPhong";
            cobmaphong.ValueMember = "MaPhong";
            cobmaphong.SelectedIndex = -1;
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // [ĐÃ SỬA] Chặn ngay lập tức nếu click vào tiêu đề hoặc dòng trống dưới cùng
            if (e.RowIndex < 0 || dataGridView.Rows[e.RowIndex].IsNewRow)
                return;

            DataGridViewRow row = dataGridView.Rows[e.RowIndex];

            txtmssv.Text = row.Cells["MSSV"].Value?.ToString();
            currentMSSV = txtmssv.Text;

            txthoten.Text = row.Cells["HoTen"].Value?.ToString();
            txtlop.Text = row.Cells["Lop"].Value?.ToString();
            txtsdt.Text = row.Cells["SDT"].Value?.ToString();
            txtquequan.Text = row.Cells["QueQuan"].Value?.ToString();
            cobgioitinh.Text = row.Cells["GioiTinh"].Value?.ToString();

            // [ĐÃ SỬA] Kiểm tra kỹ DBNull.Value để tránh lỗi khi ngày tháng trong SQL bị rỗng
            if (row.Cells["NgaySinh"].Value != null && row.Cells["NgaySinh"].Value != DBNull.Value)
                datengaysinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);

            if (row.Cells["NgayVao"].Value != null && row.Cells["NgayVao"].Value != DBNull.Value)
                datengayvao.Value = Convert.ToDateTime(row.Cells["NgayVao"].Value);

            cobmaphong.SelectedValue = row.Cells["MaPhong"].Value?.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            xuLyThem = true;
            BatTatChucNang(true);

            txtmssv.Clear();
            txthoten.Clear();
            txtlop.Clear();
            txtsdt.Clear();
            txtquequan.Clear();
            cobgioitinh.SelectedIndex = -1;
            cobmaphong.SelectedIndex = -1;
            datengaysinh.Value = DateTime.Now;
            datengayvao.Value = DateTime.Now;
            txtmssv.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtmssv.Text))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa!", "Thông báo");
                return;
            }

            xuLyThem = false;
            BatTatChucNang(true);
            txtmssv.Enabled = false; // Không cho sửa khóa chính
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMSSV))
            {
                MessageBox.Show("Vui lòng chọn sinh viên để xóa!", "Cảnh báo");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên " + txthoten.Text + "?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    var sv = context.SinhViens.Find(currentMSSV);
                    if (sv != null)
                    {
                        context.SinhViens.Remove(sv);
                        context.SaveChanges();

                        MessageBox.Show("Xóa thành công!");
                        ResetForm(); // [ĐÃ SỬA] Dùng hàm ResetForm thay vì Load
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa (Có thể SV này đang có dữ liệu liên quan): " + ex.Message);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtmssv.Text) || string.IsNullOrWhiteSpace(txthoten.Text))
            {
                MessageBox.Show("Vui lòng nhập MSSV và Họ Tên!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (xuLyThem)
                {
                    var checkID = context.SinhViens.Find(txtmssv.Text);
                    if (checkID != null)
                    {
                        MessageBox.Show("Mã số sinh viên này đã tồn tại!", "Trùng mã");
                        return;
                    }

                    SinhVien sv = new SinhVien();
                    sv.MSSV = txtmssv.Text;
                    sv.HoTen = txthoten.Text;
                    sv.Lop = txtlop.Text;
                    sv.SDT = txtsdt.Text;
                    sv.QueQuan = txtquequan.Text;
                    sv.GioiTinh = cobgioitinh.Text;
                    sv.NgaySinh = datengaysinh.Value;
                    sv.NgayVao = datengayvao.Value;
                    sv.MaPhong = cobmaphong.SelectedValue?.ToString();

                    context.SinhViens.Add(sv);
                    context.SaveChanges();
                    MessageBox.Show("Thêm mới thành công!");
                }
                else
                {
                    var sv = context.SinhViens.Find(currentMSSV);
                    if (sv != null)
                    {
                        sv.HoTen = txthoten.Text;
                        sv.Lop = txtlop.Text;
                        sv.SDT = txtsdt.Text;
                        sv.QueQuan = txtquequan.Text;
                        sv.GioiTinh = cobgioitinh.Text;
                        sv.NgaySinh = datengaysinh.Value;
                        sv.NgayVao = datengayvao.Value;
                        sv.MaPhong = cobmaphong.SelectedValue?.ToString();

                        context.SinhViens.Update(sv);
                        context.SaveChanges();
                        MessageBox.Show("Cập nhật thông tin thành công!");
                    }
                }

                ResetForm(); // [ĐÃ SỬA] Dùng hàm ResetForm
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            ResetForm(); // [ĐÃ SỬA] Dùng hàm ResetForm
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void cobgioitinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            string gioiTinh = cobgioitinh.Text;

            // Gọi hàm load phòng để nó tự động lọc lại theo Nam/Nữ
            LoadComboboxPhong(gioiTinh);
        }
    }
}