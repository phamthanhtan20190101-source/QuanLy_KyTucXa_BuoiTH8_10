using QuanLy_KyTucXa.Data;
using System;
using System.Linq;
using System.Windows.Forms;

namespace QuanLy_KyTucXa.Forms
{
    public partial class frmXacNhanMatKhau : Form
    {
        public frmXacNhanMatKhau()
        {
            InitializeComponent();
            // Thiết lập nút Enter mặc định là Xong, nút Esc là Hủy
            this.AcceptButton = btnXacNhan;
            this.CancelButton = btnHuy;
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var context = new QLKTXDbContext())
            {
                // Lấy tài khoản đang đăng nhập từ hệ thống
                string tenUser = frmMain.MaNguoiDungHienTai;

                // Băm mật khẩu người dùng vừa gõ vào
                string mkDaBam = MaHoaHelper.HashPassword(txtMatKhau.Text);

                // Kiểm tra xem khớp trong DB không
                var tk = context.TaiKhoans.FirstOrDefault(t => t.TenTaiKhoan == tenUser && t.MatKhau == mkDaBam);

                if (tk != null)
                {
                    // Nếu ĐÚNG -> Trả về kết quả OK và tự đóng Form
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Mật khẩu không chính xác. Thao tác bị từ chối!", "Lỗi bảo mật", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMatKhau.Clear();
                    txtMatKhau.Focus();
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            // Nếu bấm hủy -> Trả về Cancel
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}