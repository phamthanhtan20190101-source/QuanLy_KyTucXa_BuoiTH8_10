namespace QuanLy_KyTucXa.Forms
{
    partial class ChucNang
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            btnthemphong = new Button();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            btnthem = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnthemphong);
            groupBox1.Controls.Add(button5);
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(btnthem);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1195, 665);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Chức năng";
            // 
            // btnthemphong
            // 
            btnthemphong.Location = new Point(224, 159);
            btnthemphong.Name = "btnthemphong";
            btnthemphong.Size = new Size(180, 92);
            btnthemphong.TabIndex = 5;
            btnthemphong.Text = "Thêm Phòng";
            btnthemphong.UseVisualStyleBackColor = true;
            btnthemphong.Click += btnthemphong_Click;
            // 
            // button5
            // 
            button5.Location = new Point(449, 159);
            button5.Name = "button5";
            button5.Size = new Size(180, 92);
            button5.TabIndex = 4;
            button5.Text = "button5";
            button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(673, 159);
            button4.Name = "button4";
            button4.Size = new Size(180, 92);
            button4.TabIndex = 3;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(901, 159);
            button3.Name = "button3";
            button3.Size = new Size(180, 92);
            button3.TabIndex = 2;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(6, 323);
            button2.Name = "button2";
            button2.Size = new Size(180, 92);
            button2.TabIndex = 1;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            // 
            // btnthem
            // 
            btnthem.Location = new Point(6, 159);
            btnthem.Name = "btnthem";
            btnthem.Size = new Size(180, 92);
            btnthem.TabIndex = 0;
            btnthem.Text = "Thêm sinh viên";
            btnthem.UseVisualStyleBackColor = true;
            btnthem.Click += btnthem_Click;
            // 
            // ChucNang
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1219, 689);
            Controls.Add(groupBox1);
            Name = "ChucNang";
            Text = "ChucNang";
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button btnthemphong;
        private Button button5;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button btnthem;
    }
}