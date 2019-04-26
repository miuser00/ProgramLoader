namespace ProgramLoader
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_ReadList = new System.Windows.Forms.Button();
            this.txt_AppPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_App = new System.Windows.Forms.DataGridView();
            this.btn_Run = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btn_OverNight = new System.Windows.Forms.Button();
            this.btn_Heatbeat = new System.Windows.Forms.Button();
            this.btn_TimerStop = new System.Windows.Forms.Button();
            this.btn_TimerRun = new System.Windows.Forms.Button();
            this.btn_Init = new System.Windows.Forms.Button();
            this.btn_StartMonitorThreads = new System.Windows.Forms.Button();
            this.btn_StopMonitorThreads = new System.Windows.Forms.Button();
            this.rtb_Message = new System.Windows.Forms.RichTextBox();
            this.notify_1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Execute = new System.Windows.Forms.Button();
            this.btn_Terminate = new System.Windows.Forms.Button();
            this.lab_RunStatus = new System.Windows.Forms.Label();
            this.tmr_Refresh = new System.Windows.Forms.Timer(this.components);
            this.tmr_Triger = new System.Windows.Forms.Timer(this.components);
            this.btn_Open = new System.Windows.Forms.Button();
            this.btn_TerminatedError = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_App)).BeginInit();
            this.groupBox9.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_ReadList);
            this.groupBox1.Controls.Add(this.txt_AppPath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dgv_App);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(784, 144);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "程序列表";
            // 
            // btn_ReadList
            // 
            this.btn_ReadList.Location = new System.Drawing.Point(158, 72);
            this.btn_ReadList.Name = "btn_ReadList";
            this.btn_ReadList.Size = new System.Drawing.Size(110, 31);
            this.btn_ReadList.TabIndex = 52;
            this.btn_ReadList.Text = "导入";
            this.btn_ReadList.UseVisualStyleBackColor = true;
            this.btn_ReadList.Visible = false;
            this.btn_ReadList.Click += new System.EventHandler(this.btn_ReadList_Click);
            // 
            // txt_AppPath
            // 
            this.txt_AppPath.Location = new System.Drawing.Point(96, 45);
            this.txt_AppPath.Name = "txt_AppPath";
            this.txt_AppPath.Size = new System.Drawing.Size(172, 21);
            this.txt_AppPath.TabIndex = 53;
            this.txt_AppPath.Text = "AutoApp.csv";
            this.txt_AppPath.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 54;
            this.label1.Text = "列表位置";
            this.label1.Visible = false;
            // 
            // dgv_App
            // 
            this.dgv_App.AllowUserToAddRows = false;
            this.dgv_App.AllowUserToDeleteRows = false;
            this.dgv_App.AllowUserToOrderColumns = true;
            this.dgv_App.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_App.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_App.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_App.Location = new System.Drawing.Point(3, 17);
            this.dgv_App.Name = "dgv_App";
            this.dgv_App.RowTemplate.Height = 23;
            this.dgv_App.Size = new System.Drawing.Size(778, 124);
            this.dgv_App.TabIndex = 0;
            // 
            // btn_Run
            // 
            this.btn_Run.Location = new System.Drawing.Point(657, 147);
            this.btn_Run.Name = "btn_Run";
            this.btn_Run.Size = new System.Drawing.Size(118, 74);
            this.btn_Run.TabIndex = 1;
            this.btn_Run.Text = "全部运行";
            this.btn_Run.UseVisualStyleBackColor = true;
            this.btn_Run.Click += new System.EventHandler(this.btn_Run_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.Location = new System.Drawing.Point(657, 230);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(118, 43);
            this.btn_Stop.TabIndex = 2;
            this.btn_Stop.Text = "全部停止";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btn_TerminatedError);
            this.groupBox9.Controls.Add(this.btn_OverNight);
            this.groupBox9.Controls.Add(this.btn_Heatbeat);
            this.groupBox9.Controls.Add(this.btn_TimerStop);
            this.groupBox9.Controls.Add(this.btn_TimerRun);
            this.groupBox9.Controls.Add(this.btn_Init);
            this.groupBox9.Controls.Add(this.btn_StartMonitorThreads);
            this.groupBox9.Controls.Add(this.btn_StopMonitorThreads);
            this.groupBox9.Controls.Add(this.rtb_Message);
            this.groupBox9.Location = new System.Drawing.Point(0, 147);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(526, 103);
            this.groupBox9.TabIndex = 49;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "信息";
            // 
            // btn_OverNight
            // 
            this.btn_OverNight.Location = new System.Drawing.Point(270, 77);
            this.btn_OverNight.Name = "btn_OverNight";
            this.btn_OverNight.Size = new System.Drawing.Size(118, 26);
            this.btn_OverNight.TabIndex = 64;
            this.btn_OverNight.Text = "隔夜初始化-刷新";
            this.btn_OverNight.UseVisualStyleBackColor = true;
            this.btn_OverNight.Visible = false;
            this.btn_OverNight.Click += new System.EventHandler(this.btn_OverNight_Click);
            // 
            // btn_Heatbeat
            // 
            this.btn_Heatbeat.Location = new System.Drawing.Point(270, 48);
            this.btn_Heatbeat.Name = "btn_Heatbeat";
            this.btn_Heatbeat.Size = new System.Drawing.Size(118, 26);
            this.btn_Heatbeat.TabIndex = 63;
            this.btn_Heatbeat.Text = "检测心跳-刷新";
            this.btn_Heatbeat.UseVisualStyleBackColor = true;
            this.btn_Heatbeat.Visible = false;
            this.btn_Heatbeat.Click += new System.EventHandler(this.btn_Heatbeat_Click);
            // 
            // btn_TimerStop
            // 
            this.btn_TimerStop.Location = new System.Drawing.Point(146, 77);
            this.btn_TimerStop.Name = "btn_TimerStop";
            this.btn_TimerStop.Size = new System.Drawing.Size(118, 26);
            this.btn_TimerStop.TabIndex = 62;
            this.btn_TimerStop.Text = "定时关闭-刷新";
            this.btn_TimerStop.UseVisualStyleBackColor = true;
            this.btn_TimerStop.Visible = false;
            this.btn_TimerStop.Click += new System.EventHandler(this.btn_TimerStop_Click);
            // 
            // btn_TimerRun
            // 
            this.btn_TimerRun.Location = new System.Drawing.Point(146, 48);
            this.btn_TimerRun.Name = "btn_TimerRun";
            this.btn_TimerRun.Size = new System.Drawing.Size(118, 26);
            this.btn_TimerRun.TabIndex = 61;
            this.btn_TimerRun.Text = "定时执行-刷新";
            this.btn_TimerRun.UseVisualStyleBackColor = true;
            this.btn_TimerRun.Visible = false;
            this.btn_TimerRun.Click += new System.EventHandler(this.btn_TimerRun_Click);
            // 
            // btn_Init
            // 
            this.btn_Init.Location = new System.Drawing.Point(22, 20);
            this.btn_Init.Name = "btn_Init";
            this.btn_Init.Size = new System.Drawing.Size(118, 26);
            this.btn_Init.TabIndex = 60;
            this.btn_Init.Text = "初始化";
            this.btn_Init.UseVisualStyleBackColor = true;
            this.btn_Init.Visible = false;
            this.btn_Init.Click += new System.EventHandler(this.btn_Init_Click);
            // 
            // btn_StartMonitorThreads
            // 
            this.btn_StartMonitorThreads.Location = new System.Drawing.Point(22, 48);
            this.btn_StartMonitorThreads.Name = "btn_StartMonitorThreads";
            this.btn_StartMonitorThreads.Size = new System.Drawing.Size(118, 26);
            this.btn_StartMonitorThreads.TabIndex = 58;
            this.btn_StartMonitorThreads.Text = "开始控制台监控";
            this.btn_StartMonitorThreads.UseVisualStyleBackColor = true;
            this.btn_StartMonitorThreads.Visible = false;
            this.btn_StartMonitorThreads.Click += new System.EventHandler(this.btn_StartMonitorThreads_Click);
            // 
            // btn_StopMonitorThreads
            // 
            this.btn_StopMonitorThreads.Location = new System.Drawing.Point(22, 77);
            this.btn_StopMonitorThreads.Name = "btn_StopMonitorThreads";
            this.btn_StopMonitorThreads.Size = new System.Drawing.Size(118, 26);
            this.btn_StopMonitorThreads.TabIndex = 59;
            this.btn_StopMonitorThreads.Text = "终止控制台监控";
            this.btn_StopMonitorThreads.UseVisualStyleBackColor = true;
            this.btn_StopMonitorThreads.Visible = false;
            this.btn_StopMonitorThreads.Click += new System.EventHandler(this.btn_StopMonitorThreads_Click);
            // 
            // rtb_Message
            // 
            this.rtb_Message.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_Message.Location = new System.Drawing.Point(3, 17);
            this.rtb_Message.Name = "rtb_Message";
            this.rtb_Message.Size = new System.Drawing.Size(520, 83);
            this.rtb_Message.TabIndex = 16;
            this.rtb_Message.Text = "";
            this.rtb_Message.TextChanged += new System.EventHandler(this.rtb_Message_TextChanged);
            // 
            // notify_1
            // 
            this.notify_1.ContextMenuStrip = this.contextMenuStrip1;
            this.notify_1.Icon = ((System.Drawing.Icon)(resources.GetObject("notify_1.Icon")));
            this.notify_1.Text = "Notify_Icon";
            this.notify_1.Visible = true;
            this.notify_1.Click += new System.EventHandler(this.notify_1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(97, 26);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(96, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // btn_Execute
            // 
            this.btn_Execute.Location = new System.Drawing.Point(533, 147);
            this.btn_Execute.Name = "btn_Execute";
            this.btn_Execute.Size = new System.Drawing.Size(118, 35);
            this.btn_Execute.TabIndex = 55;
            this.btn_Execute.Text = "执行选中-带参数";
            this.btn_Execute.UseVisualStyleBackColor = true;
            this.btn_Execute.Click += new System.EventHandler(this.btn_Execute_Click);
            // 
            // btn_Terminate
            // 
            this.btn_Terminate.Location = new System.Drawing.Point(533, 230);
            this.btn_Terminate.Name = "btn_Terminate";
            this.btn_Terminate.Size = new System.Drawing.Size(118, 43);
            this.btn_Terminate.TabIndex = 56;
            this.btn_Terminate.Text = "终止当前选中程序";
            this.btn_Terminate.UseVisualStyleBackColor = true;
            this.btn_Terminate.Click += new System.EventHandler(this.btn_Terminate_Click);
            // 
            // lab_RunStatus
            // 
            this.lab_RunStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lab_RunStatus.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_RunStatus.Location = new System.Drawing.Point(3, 253);
            this.lab_RunStatus.Name = "lab_RunStatus";
            this.lab_RunStatus.Size = new System.Drawing.Size(523, 20);
            this.lab_RunStatus.TabIndex = 57;
            this.lab_RunStatus.Text = "未知";
            this.lab_RunStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmr_Refresh
            // 
            this.tmr_Refresh.Interval = 1000;
            this.tmr_Refresh.Tick += new System.EventHandler(this.tmr_Refresh_Tick);
            // 
            // tmr_Triger
            // 
            this.tmr_Triger.Interval = 1000;
            this.tmr_Triger.Tick += new System.EventHandler(this.tmr_Triger_Tick);
            // 
            // btn_Open
            // 
            this.btn_Open.Location = new System.Drawing.Point(533, 186);
            this.btn_Open.Name = "btn_Open";
            this.btn_Open.Size = new System.Drawing.Size(118, 33);
            this.btn_Open.TabIndex = 58;
            this.btn_Open.Text = "执行选中-无参";
            this.btn_Open.UseVisualStyleBackColor = true;
            this.btn_Open.Click += new System.EventHandler(this.btn_Open_Click);
            // 
            // btn_TerminatedError
            // 
            this.btn_TerminatedError.Location = new System.Drawing.Point(394, 48);
            this.btn_TerminatedError.Name = "btn_TerminatedError";
            this.btn_TerminatedError.Size = new System.Drawing.Size(118, 26);
            this.btn_TerminatedError.TabIndex = 65;
            this.btn_TerminatedError.Text = "检测异常退出-刷新";
            this.btn_TerminatedError.UseVisualStyleBackColor = true;
            this.btn_TerminatedError.Visible = false;
            this.btn_TerminatedError.Click += new System.EventHandler(this.btn_TerminatedError_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(784, 281);
            this.Controls.Add(this.btn_Open);
            this.Controls.Add(this.lab_RunStatus);
            this.Controls.Add(this.btn_Terminate);
            this.Controls.Add(this.btn_Execute);
            this.Controls.Add(this.btn_Run);
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "ProgramLoader 0.41";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_App)).EndInit();
            this.groupBox9.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Run;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.RichTextBox rtb_Message;
        private System.Windows.Forms.NotifyIcon notify_1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.Button btn_ReadList;
        private System.Windows.Forms.TextBox txt_AppPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv_App;
        private System.Windows.Forms.Button btn_Execute;
        private System.Windows.Forms.Button btn_Terminate;
        private System.Windows.Forms.Label lab_RunStatus;
        private System.Windows.Forms.Timer tmr_Refresh;
        private System.Windows.Forms.Timer tmr_Triger;
        private System.Windows.Forms.Button btn_StartMonitorThreads;
        private System.Windows.Forms.Button btn_StopMonitorThreads;
        private System.Windows.Forms.Button btn_Init;
        private System.Windows.Forms.Button btn_TimerRun;
        private System.Windows.Forms.Button btn_Heatbeat;
        private System.Windows.Forms.Button btn_TimerStop;
        private System.Windows.Forms.Button btn_OverNight;
        private System.Windows.Forms.Button btn_Open;
        private System.Windows.Forms.Button btn_TerminatedError;
    }
}

