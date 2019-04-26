namespace LoadTester
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txt_arg = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_con = new System.Windows.Forms.TextBox();
            this.btn_Send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "arg";
            // 
            // txt_arg
            // 
            this.txt_arg.Location = new System.Drawing.Point(90, 17);
            this.txt_arg.Name = "txt_arg";
            this.txt_arg.Size = new System.Drawing.Size(334, 21);
            this.txt_arg.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "console";
            // 
            // txt_con
            // 
            this.txt_con.Location = new System.Drawing.Point(90, 62);
            this.txt_con.Name = "txt_con";
            this.txt_con.Size = new System.Drawing.Size(334, 21);
            this.txt_con.TabIndex = 3;
            // 
            // btn_Send
            // 
            this.btn_Send.Location = new System.Drawing.Point(358, 121);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(122, 45);
            this.btn_Send.TabIndex = 4;
            this.btn_Send.Text = "send";
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 321);
            this.Controls.Add(this.btn_Send);
            this.Controls.Add(this.txt_con);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_arg);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_arg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_con;
        private System.Windows.Forms.Button btn_Send;
    }
}

