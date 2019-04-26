using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

//Common reference
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;


namespace ProgramLoader
{
    public partial class Form1 : Form
    {
        DataTable dt_App=null;
        int i_MessageCount = 0;
        //每个运行的进程配备的变量
        Process[] ps; //进程
        string[] s_ConsoleOutput_buff; //控制台缓冲
        string[] s_ConsoleOutput; //控制台返回信息
        StreamReader[] srs; //控制台读取的stream
        Thread[] ts; //刷新控制台的进程

        DateTime dt_last; //上次触发的时间
        DateTime dt_Now; //本次触发的时间

        bool[] b_Beating;

        string arg;
        public Form1()
        {
            InitializeComponent();
        }
        public Form1(string[] args)
        {
            InitializeComponent();
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                    arg = arg + args[i];
            }
        }
        private void notify_1_Click(object sender, EventArgs e)
        {
            this.Visible = (!this.Visible);
        }
        public void SendMsg(string strRsv)
        {

            string s_output = rtb_Message.Text;
            i_MessageCount++;
            s_output = strRsv + "\n" + s_output;
            s_output = "(" + i_MessageCount.ToString() + ") " + System.DateTime.Now.ToString() + " " + Environment.NewLine + s_output;
            if (s_output.Length > 2000)
            {
                s_output = s_output.Substring(0, 2000);
            }
            rtb_Message.Text = s_output;
        }
        public void ShowException(Exception ee)
        {
            SendMsg("发生异常：" + ee.Message + "\n" + "异常具体信息为：" + "\n" + ee.StackTrace);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //初始化
            btn_Init_Click(sender, e);

            if (arg=="autostart")
            {
                btn_Run_Click(sender, e);
                this.Visible = false;
            }

        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            notify_1.Dispose();
            btn_Stop_Click(sender, e);
            Environment.Exit(0);
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            //终止触发
            tmr_Triger.Enabled = false;
            //终止进程监控
            btn_StopMonitorThreads_Click(sender, e);
            //终止所有进程
            for (int i=0;i<dt_App.Rows.Count;i++)
            {
                stopProcess(i);
            }
            lab_RunStatus.Text = "已停止";
            lab_RunStatus.BackColor = Color.Gray;
        }

        private void btn_ReadList_Click(object sender, EventArgs e)
        {
            dt_App = new DataTable();
            MCSV.CSV.OpenCSVFileWithHeader(ref dt_App, txt_AppPath.Text);
            dgv_App.DataSource = dt_App;
        }

        private void btn_Execute_Click(object sender, EventArgs e)
        {
            if (dgv_App.SelectedCells.Count > 0)
            {
                int i_appindex = dgv_App.SelectedCells[0].OwningRow.Index;
                startProcess(i_appindex);
            }

        }

        void t_RefreshProcessInfo(object processNo)
        {
            for (;;)
            { 
                int i = (int)processNo;
                if (ps[i] != null)
                {
                    srs[i] = ps[i].StandardOutput;
                    char[] bs = new char[1];


                    int ret = srs[i].Read(bs, 0, 1);
                    if (ret > 0)
                    {
                        if (bs[0] == '\r')
                        {
                            s_ConsoleOutput[i] = s_ConsoleOutput_buff[i];
                            s_ConsoleOutput_buff[i] = ""; //新的一行
                            b_Beating[i] = true;
                            Thread.Sleep(1);
                        }
                        s_ConsoleOutput_buff[i] = s_ConsoleOutput_buff[i] + new string(bs);
                    }else
                    {
                        Thread.Sleep(10);
                    }
                }else
                {
                    Thread.Sleep(10);
                }

            }
        }
        private void tmr_Refresh_Tick(object sender, EventArgs e)
        {

            //刷新执行状态
            for (int i = 0; i < dt_App.Rows.Count; i++)
            {
                if (ps[i] != null)
                {
                    bool isExited = ps[i].HasExited;

                    if (isExited == true)
                    {
                        dt_App.Rows[i]["运行时间"] = "";

                        if (dt_App.Rows[i]["状态"].ToString() != "执行完毕")
                        {
                            dt_App.Rows[i]["状态"] = "执行完毕";
                            SendMsg("程序 " + dt_App.Rows[i]["程序名"] + " 已经终止");
                        }
                    }
                    else
                    {
                        if (dt_App.Rows[i]["状态"].ToString() != "执行中")
                        {
                            dt_App.Rows[i]["状态"] = "执行中";
                            SendMsg("程序 " + dt_App.Rows[i]["程序名"] + " 已经启动");
                        }
                    }
                    //刷新控制台输出
                    if (dt_App.Rows[i]["输出"].ToString() != s_ConsoleOutput[i]) dt_App.Rows[i]["输出"] = s_ConsoleOutput[i];
                    //刷新剩余心跳

                }
            }
        }

        private void btn_Terminate_Click(object sender, EventArgs e)
        {
            if (dgv_App.SelectedCells.Count > 0)
            {
                int i_appindex = dgv_App.SelectedCells[0].OwningRow.Index;
                stopProcess(i_appindex);
            }
        }

        private void btn_Run_Click(object sender, EventArgs e)
        {
            //立即启动非定时执行程序
            for (int i = 0; i < dt_App.Rows.Count;i++)
            {
                bool isTimerRun = false;
                if (dt_App.Columns.Contains("定时执行"))
                {
                    if (dt_App.Rows[i]["定时执行"].ToString().Contains("是"))
                    {
                        isTimerRun = true;
                    }
                }
                if (isTimerRun == false)
                {
                    startProcess(i);
                }
            }
            //启动控制台监控
            btn_StartMonitorThreads_Click(sender, e);
            //触发器使能
            tmr_Triger.Enabled = true;

            lab_RunStatus.Text = "运行中";
            lab_RunStatus.BackColor = Color.Green;
        }

        private void btn_StartMonitorThreads_Click(object sender, EventArgs e)
        {
            for (int i=0;i<dt_App.Rows.Count;i++)
            {
                ts[i] = new Thread(t_RefreshProcessInfo);
                ts[i].Start((object)i);
            }
            tmr_Refresh.Enabled = true;
            SendMsg("监控线程及监控Timer启动");
        }

        private void btn_StopMonitorThreads_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dt_App.Rows.Count; i++)
            {
                if (ts[i] != null)
                {
                    ts[i].Abort();
                }
            }
            tmr_Refresh.Enabled = false;
            SendMsg("监控线程及监控Timer停止");
        }

        private void tmr_Triger_Tick(object sender, EventArgs e)
        {
            dt_Now = DateTime.Now;
            btn_TimerRun_Click(sender, e);
            btn_TimerStop_Click(sender, e);
            btn_Heatbeat_Click(sender, e);
            btn_TerminatedError_Click(sender, e);
            btn_OverNight_Click(sender, e);
            dt_last = dt_Now;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            notify_1.Dispose();
            btn_Stop_Click(sender, e);
            Environment.Exit(0);
        }

        private void btn_Init_Click(object sender, EventArgs e)
        {
            //获取当前工作区宽度和高度（工作区不包含状态栏）
            int ScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int ScreenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            //计算窗体显示的坐标值，可以根据需要微调几个像素
            int x = ScreenWidth - this.Width + 5;
            int y = ScreenHeight - this.Height + 5;

            this.Location = new Point(x, y);

            //读取程序列表
            try
            {
                btn_ReadList_Click(sender, e);
                ps = new Process[dt_App.Rows.Count];
                b_Beating = new bool[dt_App.Rows.Count];
                s_ConsoleOutput = new string[dt_App.Rows.Count];
                s_ConsoleOutput_buff = new string[dt_App.Rows.Count];
                srs = new StreamReader[dt_App.Rows.Count];
                ts = new Thread[dt_App.Rows.Count];
            }
            catch
            {
                SendMsg("加载应用程序列表时发生错误.");
            }

            for (int i = 0; i < dt_App.Rows.Count; i++)
            {
                dt_App.Rows[i]["状态"] = "等待执行";
            }
            SendMsg("初始化完成");

            //初始化计时器
            dt_Now = DateTime.Now;
            dt_last = DateTime.Now;


        }

        private void btn_TimerRun_Click(object sender, EventArgs e)
        {
            //触发定时执行
            for (int i = 0; i < dt_App.Rows.Count; i++)
            {
                //判断是否需要定时执行
                bool isTimerRun = false;
                if (dt_App.Columns.Contains("定时执行"))
                {
                    if (dt_App.Rows[i]["定时执行"].ToString().Contains("是"))
                    {
                        isTimerRun = true;
                    }
                }
                if (dt_App.Columns.Contains("周末不执行"))
                {
                    if (dt_App.Rows[i]["周末不执行"].ToString().Contains("是"))
                    {
                        if ((DateTime.Now.DayOfWeek==DayOfWeek.Saturday)|| (DateTime.Now.DayOfWeek == DayOfWeek.Sunday))
                        isTimerRun = false;
                    }
                }
                if (isTimerRun)
                {
                   if (Convert.ToDateTime(dt_Now.ToShortTimeString()) == Convert.ToDateTime(dt_App.Rows[i]["执行时间"].ToString()))
                        {
                        if (dt_App.Rows[i]["状态"].ToString() == "等待执行")
                        {
                            dt_App.Rows[i]["状态"] = "启动中";
                            SendMsg(dt_App.Rows[i]["程序名"].ToString() + " 已触发");
                            startProcess(i);
                        }
                    }
                }
            }
        }

        private void btn_TimerStop_Click(object sender, EventArgs e)
        {

            //触发定时关闭
            for (int i = 0; i < dt_App.Rows.Count; i++)
            {
                //判断是否需要定时关闭
                bool isTimerStop = false;
                if (dt_App.Columns.Contains("定时关闭"))
                {
                    if (dt_App.Rows[i]["定时关闭"].ToString().Contains("是"))
                    {
                        isTimerStop = true;
                    }
                }
                //刷新运行时间
                double runtime = 0; //分钟
                double terminatetime = 0; //最大运行时间
                if (dt_App.Rows[i]["状态"].ToString() == "执行中")
                {
                    double elapsed = (dt_Now - dt_last).TotalMilliseconds / 60000;
                    if (dt_App.Rows[i]["运行时间"].ToString() != "")
                    {
                        runtime = Convert.ToDouble(dt_App.Rows[i]["运行时间"]);
                    }
                    runtime = runtime + elapsed;
                    dt_App.Rows[i]["运行时间"] = runtime.ToString("F6");

                    if (dt_App.Rows[i]["关闭时间"].ToString() != "")
                    {
                        terminatetime = Convert.ToDouble(dt_App.Rows[i]["关闭时间"]);
                    }
                }
                //判断是否运行超时
                if (isTimerStop)
                {
                    if (runtime > terminatetime)
                    {
                        if (dt_App.Rows[i]["状态"].ToString() == "执行中")
                        {
                            dt_App.Rows[i]["状态"] = "超时终止";
                            SendMsg(dt_App.Rows[i]["程序名"].ToString() + " 超时终止");
                            stopProcess(i);
                            //重置运行时间

                        }
                    }
                }
            }
        }

        private void rtb_Message_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_OverNight_Click(object sender, EventArgs e)
        {
            //日起变更刷新状态
            if (Convert.ToDateTime(dt_Now.ToLongTimeString()) == Convert.ToDateTime("00:00:00"))
            {
                SendMsg("日期变更为" + dt_Now.ToShortDateString()+"正在重新初始化...");
                for (int i = 0; i < dt_App.Rows.Count; i++)
                {
                    if (ps[i] != null)
                    {
                        if (ps[i].HasExited == true)
                        {
                            ps[i] = null;
                            dt_App.Rows[i]["状态"] = "等待执行";
                        }
                    }
                }
                SendMsg("初始化完成");
            }
            Application.DoEvents();
        }

        private void btn_Heatbeat_Click(object sender, EventArgs e)
        {
            //触发定时关闭
            for (int i = 0; i < dt_App.Rows.Count; i++)
            {
                //判断是否需要检测心跳
                bool isDetectHeartbeat = false;
                if (dt_App.Columns.Contains("监测心跳"))
                {
                    if (dt_App.Rows[i]["监测心跳"].ToString().Contains("是"))
                    {
                        isDetectHeartbeat = true;
                    }
                }

                //判断是否运行超时
                if (isDetectHeartbeat)
                {
                    //刷新心跳时间
                    double heartbeat = 0; //m
                    double maxheatbeat = 1; //最大心跳间隔
                    if (dt_App.Rows[i]["状态"].ToString() == "执行中")
                    {
                        double elapsed = (dt_Now - dt_last).TotalMilliseconds / 60000;
                        if (dt_App.Rows[i]["心跳"].ToString() != "")
                        {
                            heartbeat = Convert.ToDouble(dt_App.Rows[i]["心跳"]);
                        }
                        heartbeat = heartbeat + elapsed;
                        dt_App.Rows[i]["心跳"] = heartbeat.ToString("F6");
                    }
                    else
                    {
                        dt_App.Rows[i]["心跳"] = "";
                    }

                    if (heartbeat > maxheatbeat)
                    {
                        if (dt_App.Rows[i]["状态"].ToString() == "执行中")
                        {
                            dt_App.Rows[i]["状态"] = "心跳超时";
                            SendMsg(dt_App.Rows[i]["程序名"].ToString() + " 心跳超时");
                            //重启程序
                            stopProcess(i);
                            Thread.Sleep(5000);
                            startProcess(i);
                            //重置心跳
                            dt_App.Rows[i]["心跳"] = "0";
                        }
                    }
                    if (b_Beating[i])
                    {
                        //重置心跳
                        dt_App.Rows[i]["心跳"] = "0";
                        b_Beating[i] = false;
                    }
                    if (dt_App.Rows[i]["输出"].ToString().Contains("请求错误重启"))
                    {
                        SendMsg("程序 "+ dt_App.Rows[i]["程序名"].ToString()+" 发生内部错误，并要求重启");
                        //重启程序
                        stopProcess(i);
                        Thread.Sleep(5000);
                        startProcess(i);
                    }
                }
            }
            dt_last = dt_Now;
        }

        private void btn_Open_Click(object sender, EventArgs e)
        {
            if (dgv_App.SelectedCells.Count > 0)
            {
                int i_appindex = dgv_App.SelectedCells[0].OwningRow.Index;
                startProcess(i_appindex,false);
            }
        }
        public void startProcess(int i_appindex, bool b_WithArg = true)
        {
            if (i_appindex > dt_App.Rows.Count - 1) return;
            string s_apppath = dt_App.Rows[i_appindex]["路径"].ToString();
            string s_appname = dt_App.Rows[i_appindex]["程序名"].ToString();
            string s_arg = dt_App.Rows[i_appindex]["参数"].ToString();

            ProcessStartInfo startInfo = new ProcessStartInfo();

            //StartParameter
            startInfo.FileName = s_apppath + "\\" + s_appname;
            if (b_WithArg == true)
            {
                startInfo.Arguments = s_arg;
            }
            startInfo.WorkingDirectory = s_apppath;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;


            if ((ps[i_appindex] == null) || (ps[i_appindex].HasExited == true))
            {


                ps[i_appindex] = Process.Start(startInfo);

                int posX = 0, posY = 0;
                int width = 0, height = 0;
                bool isPlaceWindow = false;
                bool isResizeWindow = false;

                if (dt_App.Columns.Contains("摆放窗体"))
                {
                    if (dt_App.Rows[i_appindex]["摆放窗体"].ToString() == "是")
                    {
                        isPlaceWindow = true;
                        if (dt_App.Rows[i_appindex]["窗体位置"].ToString() != "")
                        {
                            string s_pos = dt_App.Rows[i_appindex]["窗体位置"].ToString();
                            string s_posX = s_pos.Substring(0, s_pos.IndexOf("|"));
                            string s_posY = s_pos.Substring(s_pos.IndexOf("|") + 1, s_pos.Length - s_pos.IndexOf("|") - 1);
                            int.TryParse(s_posX, out posX);
                            int.TryParse(s_posY, out posY);
                        }
                    }
                }
                if (dt_App.Columns.Contains("重置窗体"))
                {
                    if (dt_App.Rows[i_appindex]["重置窗体"].ToString() == "是")
                    {
                        isResizeWindow = true;
                        if (dt_App.Rows[i_appindex]["窗体尺寸"].ToString() != "")
                        {
                            string s_size = dt_App.Rows[i_appindex]["窗体尺寸"].ToString();
                            string s_sizeX = s_size.Substring(0, s_size.IndexOf("|"));
                            string s_sizeY = s_size.Substring(s_size.IndexOf("|") + 1, s_size.Length - s_size.IndexOf("|") - 1);
                            int.TryParse(s_sizeX, out width);
                            int.TryParse(s_sizeY, out height);
                        }
                    }

                }
                for (;;)
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Reset();
                    sw.Restart();

                    uint flag = TDXBind.NativeMethods.SWP_NOZORDER;
                    if (isPlaceWindow == false) flag = flag | TDXBind.NativeMethods.SWP_NOMOVE;
                    if (isResizeWindow == false) flag = flag | TDXBind.NativeMethods.SWP_NOSIZE;
                    if (ps[i_appindex].MainWindowHandle != null)
                    {
                        Thread.Sleep(5000);
                        IntPtr m_handle = ps[i_appindex].MainWindowHandle;
                        TDXBind.NativeMethods.SetWindowPos(m_handle, IntPtr.Zero, posX, posY, width, height, flag);
                        SendMsg("重置窗体完成");
                        break;
                    }

                    if (sw.ElapsedMilliseconds > 30000)
                    {
                        SendMsg("启动程序超时");
                        break;
                    }
                }
            }
            else
            {
                SendMsg("程序不能重复启动");
            }
        }
        public void stopProcess(int i_appindex)
        {
            try
            {
                if (ps[i_appindex].HasExited == false) ps[i_appindex].Kill();
            }
            catch { }
        }

        private void btn_TerminatedError_Click(object sender, EventArgs e)
        {
            //触发定时关闭
            for (int i = 0; i < dt_App.Rows.Count; i++)
            {
                //判断是否需要检测心跳
                bool isTerminatedError = false;
                if (dt_App.Columns.Contains("监测关闭"))
                {
                    if (dt_App.Rows[i]["监测关闭"].ToString().Contains("是"))
                    {
                        isTerminatedError = true;
                    }
                }

                //判断是否运行超时
                if (isTerminatedError)
                {
                    if (dt_App.Rows[i]["状态"].ToString().Contains("执行完毕"))
                    {
                        SendMsg("程序 " + dt_App.Rows[i]["程序名"].ToString() + "异常终止 正在重启");
                        //重启程序
                        stopProcess(i);
                        Thread.Sleep(5000);
                        startProcess(i);
                    }
                }
            }
            dt_last = dt_Now;
        }
    }
}
