using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLy_KyTucXa.Forms
{
    public partial class frmHuongDan : Form
    {
        public frmHuongDan()
        {
            InitializeComponent();
            SetupBrowser();
        }

        private void SetupBrowser()
        {
            this.Text = "Cẩm nang hướng dẫn sử dụng - Hệ thống Quản lý KTX";
            this.Size = new Size(1000, 800);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Đổi màu nền của Form để chống chớp trắng khi load
            this.BackColor = Color.FromArgb(15, 23, 42);

            WebBrowser browser = new WebBrowser();
            browser.Dock = DockStyle.Fill;
            browser.ScriptErrorsSuppressed = true;

            // Đã xóa biến CSS, thay bằng mã màu cứng (HEX) để tương thích 100% với WinForms
            string html = @"
            <!DOCTYPE html>
            <html lang='vi'>
            <head>
                <meta charset='utf-8' />
                <meta http-equiv='X-UA-Compatible' content='IE=edge' />
                <style>
                    body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; line-height: 1.8; color: #e2e8f0; background-color: #0f172a; margin: 0; padding: 40px; }
                    .wrapper { max-width: 900px; margin: auto; background: #1e293b; border-radius: 15px; box-shadow: 0 15px 35px rgba(0,0,0,0.5); overflow: hidden; border: 1px solid #334155; }
                    .header { background: #1e40af; color: #fff; padding: 40px 20px; text-align: center; }
                    .header h1 { margin: 0; font-size: 26px; text-transform: uppercase; letter-spacing: 2px; }
                    .content { padding: 40px; }
                    
                    .nav-links { text-align: center; margin-bottom: 35px; padding-bottom: 20px; border-bottom: 2px dashed #334155; }
                    .nav-links a { text-decoration: none; color: #60a5fa; font-weight: bold; font-size: 15px; transition: 0.3s; margin: 0 15px; }
                    .nav-links a:hover { color: #fff; }
                    
                    h2 { color: #60a5fa; border-left: 6px solid #3b82f6; padding-left: 15px; margin-top: 45px; font-size: 22px; background: #13203b; padding-top: 10px; padding-bottom: 10px; }
                    .feature-box { background: #0f172a; border: 1px solid #334155; border-radius: 10px; padding: 25px; margin-bottom: 25px; }
                    .feature-box h3 { margin-top: 0; font-size: 18px; color: #60a5fa; border-bottom: 1px solid #334155; padding-bottom: 10px; margin-bottom: 15px; }
                    
                    .badge { display: inline-block; padding: 4px 10px; border-radius: 20px; font-size: 11px; font-weight: bold; color: #fff; text-transform: uppercase; margin-left: 10px; vertical-align: middle; }
                    .badge-security { background: #e74c3c; }
                    .badge-auto { background: #27ae60; }
                    
                    ul { padding-left: 25px; }
                    li { margin-bottom: 12px; }
                    b { color: #60a5fa; }
                    .footer { text-align: center; padding: 25px; font-size: 13px; color: #94a3b8; background: #1e293b; border-top: 1px solid #334155; }
                </style>
            </head>
            <body>
                <div class='wrapper'>
                    <div class='header'>
                        <h1>HƯỚNG DẪN VẬN HÀNH HỆ THỐNG</h1>
                    </div>
                    <div class='content'>
                        <div class='nav-links'>
                            <a href='#system'>AN NINH</a> | 
                            <a href='#resident'>NỘI TRÚ</a> | 
                            <a href='#finance'>TÀI CHÍNH</a> | 
                            <a href='#report'>THỐNG KÊ</a>
                        </div>

                        <p style='text-align: center; font-style: italic; color: #94a3b8;'>Tài liệu này hướng dẫn chi tiết các quy trình nghiệp vụ dành cho Ban quản lý và Sinh viên nội trú.</p>

                        <h2 id='system'>1. QUẢN TRỊ & BẢO MẬT</h2>
                        <div class='feature-box'>
                            <h3>Kiểm soát truy cập <span class='badge badge-security'>Mã hóa SHA256</span></h3>
                            <p>Hệ thống áp dụng cơ chế phân quyền nghiêm ngặt nhằm bảo vệ dữ liệu:</p>
                            <ul>
                                <li><b>Tài khoản Quản lý:</b> Có quyền điều phối phòng ốc, phê duyệt đơn từ, quản lý dòng tiền và truy xuất nhật ký thao tác toàn hệ thống.</li>
                                <li><b>Tài khoản Sinh viên:</b> Được cấp quyền tự tra cứu hồ sơ cá nhân, xem biểu phí hàng tháng và tự động kết xuất đơn từ theo mẫu quy định.</li>
                            </ul>
                        </div>
                        <div class='feature-box'>
                            <h3>Xác thực mật khẩu cấp 2 <span class='badge badge-security'>Phòng vệ</span></h3>
                            <p>Nhằm tránh các sai sót dữ liệu không đáng có, hệ thống sẽ yêu cầu xác thực lại mật khẩu Admin trước khi thực hiện: <b>Xóa sinh viên, Xóa phòng, Thanh toán hóa đơn</b> hoặc <b>Điều chuyển nhân khẩu</b> giữa các tòa nhà.</p>
                        </div>

                        <h2 id='resident'>2. QUẢN LÝ LƯU TRÚ & PHÒNG Ở</h2>
                        <div class='feature-box'>
                            <h3>Điều phối phòng ở <span class='badge badge-auto'>Thông minh</span></h3>
                            <p>Quy trình chuyển phòng được tối ưu hóa bằng các thuật toán kiểm tra logic tự động:</p>
                            <ul>
                                <li><b>Lọc Giới tính:</b> Hệ thống chỉ hiển thị các phòng trống phù hợp với giới tính của sinh viên đang cần chuyển.</li>
                                <li><b>Kiểm tra Sức chứa:</b> Tự động khóa các phòng đã đạt ngưỡng tối đa (Phòng 4 người hoặc 6 người) để tránh tình trạng quá tải.</li>
                                <li><b>Cập nhật chỉ số:</b> Khi hoàn tất, số lượng người hiện tại của cả phòng đi và phòng đến sẽ được hệ thống cập nhật đồng bộ ngay lập tức.</li>
                            </ul>
                        </div>
                        <div class='feature-box'>
                            <h3>Quản lý Hạ tầng & Tiện ích</h3>
                            <p>Sử dụng công cụ <b>Nhập/Xuất Excel</b> để quản lý danh sách phòng ốc hàng loạt. Mọi thông tin về giá phòng sẽ được hệ thống tự động áp dụng khi bạn chọn loại hình lưu trú tương ứng (Tòa A, B, C, D).</p>
                        </div>

                        <h2 id='finance'>3. NGHIỆP VỤ TÀI CHÍNH</h2>
                        <div class='feature-box'>
                            <h3>Tự động hóa chu kỳ cước <span class='badge badge-auto'>Chốt nợ</span></h3>
                            <p>Hệ thống có khả năng nhận diện thời điểm sang tháng mới để tự động thực hiện các thao tác:</p>
                            <ul>
                                <li>Quét danh sách sinh viên đang nội trú tại các tòa nhà.</li>
                                <li>Tính toán: <b>Nợ mới = Nợ cũ + Giá phòng + Tiền điện nước định mức</b>.</li>
                                <li>Cập nhật trạng thái 'Chưa đóng tiền' cho toàn bộ danh sách sinh viên chưa hoàn tất nghĩa vụ tài chính.</li>
                            </ul>
                        </div>
                        <div class='feature-box'>
                            <h3>Thanh toán & Hóa đơn điện tử</h3>
                            <p>Khi thực hiện thu tiền, quản lý có thể chọn đóng một phần hoặc toàn bộ. Ngay sau khi bấm xác nhận, hệ thống sẽ kích hoạt <b>PdfHelper</b> để xuất hóa đơn thanh toán chính thức dưới dạng file PDF có đầy đủ mã hóa đơn và ngày giờ giao dịch.</p>
                        </div>

                        <h2 id='report'>4. GIÁM SÁT & THỐNG KÊ</h2>
                        <div class='feature-box'>
                            <h3>Nhật ký hoạt động (Audit Log)</h3>
                            <p>Mọi hành động nhạy cảm trên phần mềm đều được ghi lại vĩnh viễn trong nhật ký. Quản lý có thể lọc theo tháng để xem ai đã đăng nhập, ai đã thực hiện thay đổi dữ liệu, đảm bảo tính minh bạch và trách nhiệm giải trình.</p>
                        </div>
                        <div class='feature-box'>
                            <h3>Báo cáo Microsoft ReportViewer</h3>
                            <p>Cung cấp các biểu mẫu báo cáo thống kê đạt chuẩn in ấn cho: <b>Lịch sử dòng tiền, Danh sách nợ</b> và <b>Nhật ký hệ thống</b>. Hỗ trợ xuất dữ liệu ra PDF hoặc Excel phục vụ công tác báo cáo cho Ban Giám hiệu.</p>
                        </div>
                    </div>
                    <div class='footer'>
                        &copy; 2026 - PHẦN MỀM QUẢN LÝ KTX - NGƯỜI THỰC HIỆN: PHẠM THANH TÂN - DTH235761
                    </div>
                </div>
            </body>
            </html>";

            browser.DocumentText = html;
            this.Controls.Add(browser);
        }
    }
}