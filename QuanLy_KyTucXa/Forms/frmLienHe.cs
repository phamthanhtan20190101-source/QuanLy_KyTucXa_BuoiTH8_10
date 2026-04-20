using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLy_KyTucXa.Forms
{
    public partial class frmLienHe : Form
    {
        public frmLienHe()
        {
            InitializeComponent();
            ThietKeGiaoDien();
        }

        private void ThietKeGiaoDien()
        {
            this.Text = "Liên hệ hỗ trợ";
            this.Size = new Size(480, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // Khóa thay đổi kích thước
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Đổi màu nền của Form để chống chớp trắng
            this.BackColor = Color.FromArgb(15, 23, 42);

            WebBrowser browser = new WebBrowser();
            browser.Dock = DockStyle.Fill;
            browser.ScriptErrorsSuppressed = true;
            browser.ScrollBarsEnabled = false; // Tắt thanh cuộn

            // Mã HTML/CSS thiết kế Card Liên Hệ
            string html = @"
            <!DOCTYPE html>
            <html lang='vi'>
            <head>
                <meta charset='utf-8' />
                <meta http-equiv='X-UA-Compatible' content='IE=edge' />
                <style>
                    body { 
                        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; 
                        background-color: #0f172a; 
                        margin: 0; 
                        padding: 0; 
                        display: flex; 
                        justify-content: center; 
                        align-items: center; 
                        height: 100vh;
                    }
                    .support-card { 
                        background: #1e293b; 
                        width: 90%; 
                        max-width: 400px; 
                        border-radius: 16px; 
                        padding: 40px 30px; 
                        box-shadow: 0 15px 35px rgba(0,0,0,0.5); 
                        border: 1px solid #334155; 
                        text-align: center; 
                        box-sizing: border-box;
                    }
                    .icon-wrapper { 
                        width: 80px; 
                        height: 80px; 
                        background: linear-gradient(135deg, #3b82f6, #1e40af); 
                        border-radius: 50%; 
                        display: flex; 
                        align-items: center; 
                        justify-content: center; 
                        margin: 0 auto 20px; 
                        font-size: 36px; 
                        box-shadow: 0 10px 20px rgba(59,130,246,0.3); 
                        border: 4px solid #0f172a;
                    }
                    h2 { margin: 0 0 5px 0; color: #fff; font-size: 22px; text-transform: uppercase; letter-spacing: 1px; }
                    .subtitle { color: #94a3b8; font-size: 14px; margin-bottom: 30px; }
                    
                    .contact-item { 
                        background: #0f172a; 
                        border: 1px solid #334155; 
                        border-radius: 10px; 
                        padding: 15px; 
                        margin-bottom: 15px; 
                        display: flex; 
                        align-items: center; 
                        text-align: left; 
                        transition: transform 0.3s, border-color 0.3s; 
                    }
                    .contact-item:hover { border-color: #3b82f6; transform: translateY(-3px); }
                    
                    .icon { font-size: 24px; margin-right: 15px; width: 30px; text-align: center; }
                    .text-group { display: flex; flex-direction: column; }
                    .label { font-size: 12px; color: #94a3b8; text-transform: uppercase; font-weight: bold; }
                    .value { font-size: 15px; color: #e2e8f0; font-weight: 500; margin-top: 3px; }
                    .highlight { color: #60a5fa; font-weight: bold; }
                    
                    .status-text { 
                        color: #10b981; 
                        font-weight: bold; 
                        font-size: 13px; 
                        display: flex; 
                        align-items: center; 
                        justify-content: center; 
                        margin-top: 25px; 
                        background: rgba(16, 185, 129, 0.1);
                        padding: 8px 15px;
                        border-radius: 20px;
                        display: inline-flex;
                    }
                    .status-dot { 
                        display: inline-block; 
                        width: 10px; 
                        height: 10px; 
                        background: #10b981; 
                        border-radius: 50%; 
                        margin-right: 8px; 
                        box-shadow: 0 0 8px #10b981;
                    }
                </style>
            </head>
            <body>
                <div class='support-card'>
                    <div class='icon-wrapper'>🎧</div>
                    <h2>Hỗ Trợ Kỹ Thuật</h2>
                    <div class='subtitle'>Hệ thống Quản lý Ký túc xá</div>

                    <div class='contact-item'>
                        <div class='icon'>👤</div>
                        <div class='text-group'>
                            <span class='label'>Chuyên viên hỗ trợ</span>
                            <span class='value highlight'>Phạm Thanh Tân</span>
                        </div>
                    </div>

                    <div class='contact-item'>
                        <div class='icon'>📧</div>
                        <div class='text-group'>
                            <span class='label'>Email tiếp nhận</span>
                            <span class='value'>tan_dth235761@student.agu.edu.vn</span>
                        </div>
                    </div>

                    <div class='contact-item'>
                        <div class='icon'>📞</div>
                        <div class='text-group'>
                            <span class='label'>Hotline xử lý sự cố</span>
                            <span class='value'>0342 027 581</span>
                        </div>
                    </div>

                    <div class='status-text'>
                        <span class='status-dot'></span> Đang trực tuyến - Sẵn sàng hỗ trợ
                    </div>
                </div>
            </body>
            </html>";

            browser.DocumentText = html;
            this.Controls.Add(browser);
        }
    }
}