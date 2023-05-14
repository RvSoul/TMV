namespace TMV.PrintServer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.ipaddress = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(415, 33);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 24);
            this.button1.TabIndex = 0;
            this.button1.Text = "打印";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ipaddress
            // 
            this.ipaddress.AutoSize = true;
            this.ipaddress.Location = new System.Drawing.Point(28, 40);
            this.ipaddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ipaddress.Name = "ipaddress";
            this.ipaddress.Size = new System.Drawing.Size(43, 17);
            this.ipaddress.TabIndex = 1;
            this.ipaddress.Text = "IP地址";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(71, 38);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(97, 23);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "127.0.0.1";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(220, 38);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(59, 23);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "5288";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(187, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "端口";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(4, 80);
            this.textBox3.Margin = new System.Windows.Forms.Padding(2);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(498, 228);
            this.textBox3.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(509, 319);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ipaddress);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private Label ipaddress;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label1;
        private TextBox textBox3;
    }
}