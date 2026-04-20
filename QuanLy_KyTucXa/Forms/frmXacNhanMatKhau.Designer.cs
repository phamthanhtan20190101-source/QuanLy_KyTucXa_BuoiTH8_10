namespace QuanLy_KyTucXa.Forms
{
    partial class frmXacNhanMatKhau
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            label1 = new Label();
            txtMatKhau = new TextBox();
            btnXacNhan = new Button();
            btnHuy = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.Location = new Point(57, 50);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(382, 30);
            label1.TabIndex = 0;
            label1.Text = "Vui lòng nhập lại mật khẩu để tiếp tục:";
            // 
            // txtMatKhau
            // 
            txtMatKhau.Font = new Font("Segoe UI", 14F);
            txtMatKhau.Location = new Point(57, 108);
            txtMatKhau.Margin = new Padding(4, 5, 4, 5);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.PasswordChar = '*';
            txtMatKhau.Size = new Size(455, 45);
            txtMatKhau.TabIndex = 1;
            // 
            // btnXacNhan
            // 
            btnXacNhan.BackColor = Color.ForestGreen;
            btnXacNhan.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnXacNhan.ForeColor = Color.White;
            btnXacNhan.Location = new Point(57, 192);
            btnXacNhan.Margin = new Padding(4, 5, 4, 5);
            btnXacNhan.Name = "btnXacNhan";
            btnXacNhan.Size = new Size(214, 75);
            btnXacNhan.TabIndex = 2;
            btnXacNhan.Text = "XÁC NHẬN";
            btnXacNhan.UseVisualStyleBackColor = false;
            btnXacNhan.Click += btnXacNhan_Click;
            // 
            // btnHuy
            // 
            btnHuy.BackColor = Color.IndianRed;
            btnHuy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnHuy.ForeColor = Color.White;
            btnHuy.Location = new Point(300, 192);
            btnHuy.Margin = new Padding(4, 5, 4, 5);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(214, 75);
            btnHuy.TabIndex = 3;
            btnHuy.Text = "HỦY BỎ";
            btnHuy.UseVisualStyleBackColor = false;
            btnHuy.Click += btnHuy_Click;
            // 
            // frmXacNhanMatKhau
            // 
            AcceptButton = btnXacNhan;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnHuy;
            ClientSize = new Size(571, 333);
            Controls.Add(btnHuy);
            Controls.Add(btnXacNhan);
            Controls.Add(txtMatKhau);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmXacNhanMatKhau";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Xác nhận bảo mật";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Button btnHuy;
    }
}