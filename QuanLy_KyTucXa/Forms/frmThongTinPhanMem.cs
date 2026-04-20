using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLy_KyTucXa.Forms
{
    public partial class frmThongTinPhanMem : Form
    {
        public frmThongTinPhanMem()
        {
            InitializeComponent();
            ThietKeGiaoDien();
        }

        private void ThietKeGiaoDien()
        {
            this.Text = "Thông tin phần mềm";
            this.Size = new Size(600, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Đổi màu nền của Form cho khớp với màu Web
            this.BackColor = Color.FromArgb(15, 23, 42);

            WebBrowser browser = new WebBrowser();
            browser.Dock = DockStyle.Fill;
            browser.ScriptErrorsSuppressed = true;
            browser.ScrollBarsEnabled = false;

            // Mã HTML/CSS đã cập nhật đúng tên Phạm Thanh Tân
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
                        padding: 30px; 
                        display: flex; 
                        justify-content: center; 
                        align-items: center; 
                        height: 100vh;
                    }
                    .profile-card { 
                        background: #1e293b; 
                        width: 100%; 
                        max-width: 500px; 
                        border-radius: 20px; 
                        box-shadow: 0 20px 40px rgba(0,0,0,0.6); 
                        border: 1px solid #334155;
                        text-align: center;
                        padding: 40px 30px;
                        box-sizing: border-box;
                    }
                    .logo-circle {
                        width: 100px;
                        height: 100px;
                        background: linear-gradient(135deg, #3b82f6, #1e40af);
                        border-radius: 50%;
                        margin: 0 auto 20px auto;
                        display: flex;
                        justify-content: center;
                        align-items: center;
                        color: white;
                        font-size: 32px;
                        font-weight: bold;
                        box-shadow: 0 10px 20px rgba(59, 130, 246, 0.4);
                        border: 4px solid #0f172a;
                    }
                    h1 { margin: 0; color: #fff; font-size: 24px; text-transform: uppercase; letter-spacing: 1.5px; }
                    .version { color: #60a5fa; font-weight: bold; font-size: 14px; margin-top: 5px; margin-bottom: 25px; }
                    
                    hr { border: 0; height: 1px; background: #334155; margin: 25px 0; }
                    
                    .info-section { text-align: left; margin-bottom: 20px; }
                    .info-section h3 { color: #94a3b8; font-size: 12px; text-transform: uppercase; letter-spacing: 2px; margin-bottom: 15px; }
                    .info-row { display: flex; justify-content: space-between; margin-bottom: 12px; font-size: 15px; }
                    .info-label { color: #cbd5e1; }
                    .info-value { color: #fff; font-weight: bold; }
                    .info-value.highlight { color: #3b82f6; }

                    .tech-tags { display: flex; flex-wrap: wrap; gap: 10px; justify-content: center; margin-top: 15px; }
                    .tag { background: #0f172a; color: #60a5fa; padding: 6px 15px; border-radius: 20px; font-size: 13px; font-weight: bold; border: 1px solid #334155; }
                </style>
            </head>
            <body>
                <div class='profile-card'>
                    <div class='logo-circle'>KTX</div>
                    <h1>Hệ Thống Quản Lý</h1>
                    <div class='version'>PHIÊN BẢN 1.0.0 (BẢN HOÀN THIỆN)</div>
                    
                    <hr>
                    
                    <div class='info-section'>
                        <h3>Thông tin đồ án</h3>
                        <div class='info-row'><span class='info-label'>Sinh viên thực hiện:</span> <span class='info-value highlight'>Phạm Thanh Tân</span></div>
                        <div class='info-row'><span class='info-label'>Mã số sinh viên:</span> <span class='info-value'>DTH235761</span></div>
                        <div class='info-row'><span class='info-label'>Lớp:</span> <span class='info-value'>DH24TH3</span></div>
                        <div class='info-row'><span class='info-label'>Giảng viên HD:</span> <span class='info-value'>TS. Huỳnh Lý Thanh Nhàn</span></div>
                        <div class='info-row'><span class='info-label'>Đơn vị:</span> <span class='info-value'>Đại học An Giang (AGU)</span></div>
                    </div>

                    <hr>
                    
                    <div class='info-section' style='text-align: center;'>
                        <h3>Công nghệ phát triển</h3>
                        <div class='tech-tags'>
                            <span class='tag'>C# WinForms</span>
                            <span class='tag'>.NET 8</span>
                            <span class='tag'>EF Core</span>
                            <span class='tag'>SQL Server</span>
                            <span class='tag'>ClosedXML</span>
                        </div>
                    </div>
                </div>
            </body>
            </html>";

            browser.DocumentText = html;
            this.Controls.Add(browser);
        }
    }
}