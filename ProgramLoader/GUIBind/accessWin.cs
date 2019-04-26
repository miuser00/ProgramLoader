using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace TDXBind
{


    public class AccessWin
    {
        IntPtr ptr_LastClosedHWND;
        Stopwatch sw = new Stopwatch();
        public AccessWin() //构造函数
        {
            ptr_LastClosedHWND = IntPtr.Zero;
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Restart();

        }
        public static void Idle(int millisecond)
        {
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();
            for (;;)
            {
                if (sw.ElapsedMilliseconds > millisecond) break;
                Application.DoEvents();
            }
        }
        public class WindowsTree //一组获得的窗体组
        {
            public WindowInfo[] winfos;
            public int WindowCount;

            public WindowsTree()
            {
                WindowCount = 0;
                winfos = new WindowInfo[10000];
            }
            public WindowsTree FiltedbyTitle(string title)
            {
                WindowsTree wt = new WindowsTree();
                int isort = 0;
                for (int i=0;i<this.WindowCount;i++)
                {
                    if (this.winfos[i].title == title)
                    {
                        wt.winfos[isort].hwnd = this.winfos[i].hwnd;
                        wt.winfos[isort].title = this.winfos[i].title;
                        wt.winfos[isort].classname = this.winfos[i].classname;
                        isort++;
                    }                    
                }
                wt.WindowCount = isort;
                if (wt.WindowCount > 0)
                {
                    return wt;
                }else
                {
                    return null;
                }
            }
            public WindowsTree FiltedbyClassName(string classname)
            {
                WindowsTree wt = new WindowsTree();
                int isort = 0;
                for (int i = 0; i < this.WindowCount; i++)
                {
                    if (this.winfos[i].classname == classname)
                    {
                        wt.winfos[isort].hwnd = this.winfos[i].hwnd;
                        wt.winfos[isort].title = this.winfos[i].title;
                        wt.winfos[isort].classname = this.winfos[i].classname;
                        isort++;
                    }
                }
                wt.WindowCount = isort;
                if (wt.WindowCount > 0)
                {
                    return wt;
                }
                else
                {
                    return null;
                }
            }
            public WindowsTree FiltedbyHWND(int HWND)
            {
                WindowsTree wt = new WindowsTree();
                int isort = 0;
                for (int i = 0; i < this.WindowCount; i++)
                {
                    if (this.winfos[i].hwnd.ToInt32() == HWND)
                    {
                        wt.winfos[isort].hwnd = this.winfos[i].hwnd;
                        wt.winfos[isort].title = this.winfos[i].title;
                        wt.winfos[isort].classname = this.winfos[i].classname;
                        isort++;
                    }
                }
                wt.WindowCount = isort;
                if (wt.WindowCount > 0)
                {
                    return wt;
                }
                else
                {
                    return null;
                }
            }
            public DataTable toDatatable(WindowsTree wt)
            {
                if (wt != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Clear();
                    dt.Rows.Clear();
                    dt.Columns.Add("序号");
                    dt.Columns.Add("句柄");
                    dt.Columns.Add("标题");
                    dt.Columns.Add("类名");
                    for (int i = 0; i < wt.WindowCount; i++)
                    {
                        dt.Rows.Add(new string[] { (i + 1).ToString("D3"), wt.winfos[i].hwnd.ToString("X"), wt.winfos[i].title, wt.winfos[i].classname });
                    }
                    return dt;
                }
                else
                {
                    return null;
                }
            }
            public DataTable toDatatable()
            {
                if (this != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Clear();
                    dt.Rows.Clear();
                    dt.Columns.Add("序号");
                    dt.Columns.Add("句柄");
                    dt.Columns.Add("标题");
                    dt.Columns.Add("类名");
                    for (int i = 0; i < this.WindowCount; i++)
                    {
                        dt.Rows.Add(new string[] { (i + 1).ToString("D3"), this.winfos[i].hwnd.ToString("X"), this.winfos[i].title, this.winfos[i].classname });
                    }
                    return dt;
                }
                else
                {
                    return null;
                }
            }

            public bool GetfromDataTable(DataTable dt)
            {
                if (dt == null) return false;
                try
                {
                    int ColumnCount = dt.Columns.Count;
                    int RowCount = dt.Rows.Count;
                    this.WindowCount = RowCount;
                    for (int i = 0; i < RowCount; i++)
                    {
                        this.winfos[i].classname = dt.Rows[i]["类名"].ToString();
                        this.winfos[i].title = dt.Rows[i]["标题"].ToString();
                        int int_HWND = Convert.ToInt32(dt.Rows[i]["句柄"].ToString(), 16);
                        this.winfos[i].hwnd = new IntPtr(int_HWND);
                    }
                }
                catch
                {
                    return false;
                }
                return true;
            }

        }
        public struct WindowInfo
        {
            public IntPtr hwnd;
            public String title;
            public String classname;
        }

        private WindowsTree tmp;  //for internal communication only
        public WindowsTree windowsTree;


        //---------------------------------------------------------------GUI Operation ----------------------------------------------------
        //根据标题获取句柄
        public IntPtr GetWindowHandlebyTitle(String Title)
        {
            bool flg_success = false;
            string tmp;
            AccessWin aw = new AccessWin();
            aw.windowsTree = aw.ListWindow((IntPtr)0);
            int i;
            for (i = 0; i < aw.windowsTree.WindowCount; i++)
            {
                tmp = "";
                if (aw.windowsTree.winfos[i].title.Length >= Title.Length)
                {
                    tmp = aw.windowsTree.winfos[i].title.Substring(0, Title.Length); //prevent string too short to be cut  
                    if (Title == tmp)
                    {
                        break;
                    }
                }
            }
            if (i >= aw.windowsTree.WindowCount)   //can't find Top window
            {
                flg_success = false;
                if (flg_success == false)
                {
                    return new IntPtr(-1);
                }
            }
            int currWindowNO = i;
            IntPtr currWnd = aw.windowsTree.winfos[currWindowNO].hwnd;
            return currWnd;
        }
        //根据类名获取句柄
        public IntPtr GetWindowHandlebyClass(String Class)
        {
            bool flg_success = false;
            string tmp;
            AccessWin aw = new AccessWin();
            aw.windowsTree = aw.ListWindow((IntPtr)0);
            int i;
            for (i = 0; i < aw.windowsTree.WindowCount; i++)
            {
                tmp = "";
                if (aw.windowsTree.winfos[i].classname.Length >= Class.Length)
                {
                    tmp = aw.windowsTree.winfos[i].classname.Substring(0, Class.Length); //prevent string too short to be cut  
                    if (Class == tmp)
                    {
                        break;
                    }
                }
            }
            if (i >= aw.windowsTree.WindowCount)   //can't find Top window
            {
                flg_success = false;
                if (flg_success == false)
                {
                    return new IntPtr(-1);
                }
            }
            int currWindowNO = i;
            IntPtr currWnd = aw.windowsTree.winfos[currWindowNO].hwnd;
            return currWnd;
        }

        //根据句柄获取子控件序列，含子窗口
        public WindowsTree ListWindow(IntPtr hwnd)
        {
            tmp = new WindowsTree();
            tmp.WindowCount = 0;
            NativeMethods.EnumChildWindows(hwnd, new WNDENUMPROC(GerProcessInfo_back), 0);
            this.windowsTree = tmp;
            return tmp;
        }
        /// <summary>
        /// ListWindow的回调函数
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="lpararm"></param>
        /// <returns></returns>
        int GerProcessInfo_back(IntPtr handle, IntPtr lpararm)  //callback function used by listwindow
        {
            StringBuilder st = new StringBuilder(256, 256);
            tmp.winfos[tmp.WindowCount].hwnd = handle;
            NativeMethods.GetWindowTextW(handle, st, 256);
            tmp.winfos[tmp.WindowCount].title = st.ToString();
            NativeMethods.GetClassNameW(handle, st, 256);
            tmp.winfos[tmp.WindowCount].classname = st.ToString();
            tmp.WindowCount++;

            return 1;
        }
        public WindowsTree ListBrotherWindow(IntPtr hwnd)
        {
            IntPtr parentHandle = NativeMethods.GetParent(hwnd);
            return ListWindow(parentHandle);
        }
        //给控件发送一个字符串
        public void SendKey(IntPtr handle, string Input)
        {

            byte[] ch = (ASCIIEncoding.ASCII.GetBytes(Input));
            for (int i = 0; i < ch.Length; i++)
            {
                NativeMethods.SendMessageW(handle,NativeMethods.WM_CHAR, (uint)ch[i], 0);
            }
        }
        //设置文本框字符
        public void SetText(IntPtr handle, String content)
        {
            int ptr;
            System.Runtime.InteropServices.GCHandle gch;
            gch = System.Runtime.InteropServices.GCHandle.Alloc(content, System.Runtime.InteropServices.GCHandleType.Pinned);
            ptr = gch.AddrOfPinnedObject().ToInt32();
            NativeMethods.SendMessageW(handle, NativeMethods.WM_SETTEXT, 0, ptr);
            gch.Free();
        }
        
        /// <summary>
        ///设置文本框字符
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public String GetText(IntPtr handle)
        {

            int maxLength = 100;
            IntPtr buffer = Marshal.AllocHGlobal((maxLength + 1) * 2);
            NativeMethods.SendMessageW2(handle, NativeMethods.WM_GETTEXT, (uint)maxLength, buffer);
            string w = Marshal.PtrToStringUni(buffer);
            Marshal.FreeHGlobal(buffer);
            return w;


        }
        //鼠标点击指定位置
        public int ClickLocation(int posX, int posY)
        {
            NativeMethods.SetCursorPos(posX, posY);
            Idle(10);
            NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            return 1;
        }

        public int ClickPOSList(DataTable dt,int delay)
        {
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClickLocation(Convert.ToInt32(dt.Rows[i][0]), Convert.ToInt32(dt.Rows[i][1]));
                    ClickLocation(Convert.ToInt32(dt.Rows[i][0]), Convert.ToInt32(dt.Rows[i][1]));
                    Idle(delay);
                }
                return 1;
            }catch (Exception ee)
            {
                Console.Write(ee.ToString());
                return 0;
            }
        }
        /// <summary>
        /// /单击一个按钮
        /// </summary>
        /// <param name="handle"></param>
        public void ClickButton(IntPtr handle)
        {
            NativeMethods.PostMessageW(handle, NativeMethods.WM_LBUTTONDOWN, 0, 0);
            NativeMethods.PostMessageW(handle, NativeMethods.WM_LBUTTONUP, 0, 0);

        }

         public void DoubleClickButton(IntPtr handle)
        {
            NativeMethods.PostMessageW(handle, NativeMethods.WM_LBUTTONDBLCLK, 0, 0);
            NativeMethods.PostMessageW(handle, NativeMethods.WM_LBUTTONUP, 0, 0);

        }
        /// <summary>
        /// 设置Combox选中的索引号
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="ind"></param>
        public void SelectComboBoxIndex(IntPtr handle, int ind)
        {
            NativeMethods.PostMessageW(handle, NativeMethods.CB_SETCURSEL, (uint)ind, 0);
        }
        /// <summary>
        /// 获得Combox选中的索引号
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public int GetComboBox(IntPtr handle)
        {

            int index = NativeMethods.SendMessageW(handle, NativeMethods.CB_GETCURSEL, 0, 0);
            return index;
        }
        /// <summary>
        /// 获取指定句柄的listview内容
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public DataTable GetListViewContent(IntPtr handle)
        {
            AccessWin aw = new AccessWin();
            int vItemCount;
            int ColumnCount;

            if (handle == IntPtr.Zero) return null;
            vItemCount = NativeMethods.SendMessageW(handle, NativeMethods.LVM_GETITEMCOUNT, 0, 0);
            IntPtr handleHeader = new IntPtr(NativeMethods.SendMessageW(handle, NativeMethods.LVM_GETHEADER, 0, 0));
            ColumnCount = NativeMethods.SendMessageW(handleHeader, NativeMethods.HDM_GETITEMCOUNT, 0, 0);
            uint vProcessId;
            NativeMethods.GetWindowThreadProcessId(handle, out vProcessId);
            IntPtr vProcess = NativeMethods.OpenProcess(NativeMethods.PROCESS_VM_OPERATION | NativeMethods.PROCESS_VM_READ | NativeMethods.PROCESS_VM_WRITE, false, vProcessId);
            IntPtr vPointer = NativeMethods.VirtualAllocEx(vProcess, IntPtr.Zero, 4096, NativeMethods.MEM_RESERVE | NativeMethods.MEM_COMMIT, NativeMethods.PAGE_READWRITE);
            int ttt = Marshal.SizeOf(typeof(NativeMethods.NMHDR));
            DataTable dt = new DataTable();
            try
            {

                for (int j = 0; j < ColumnCount; j++)
                {
                    byte[] vBuffer = new byte[256];
                    NativeMethods.LVHEADER[] vHeader = new NativeMethods.LVHEADER[1];
                    vHeader[0] = new NativeMethods.LVHEADER();
                    vHeader[0].mask = NativeMethods.LVCF_TEXT;
                    vHeader[0].cchTextMax = 256;
                    vHeader[0].iSubItem = 0;
                    vHeader[0].pszText = (IntPtr)((int)vPointer + Marshal.SizeOf(typeof(NativeMethods.LVHEADER)));
                    uint vNumberOfBytesRead = 0;
                    NativeMethods.WriteProcessMemory(vProcess, vPointer, Marshal.UnsafeAddrOfPinnedArrayElement(vHeader, 0), Marshal.SizeOf(typeof(NativeMethods.LVHEADER)), ref vNumberOfBytesRead);
                    NativeMethods.SendMessageW(handle, NativeMethods.LVM_GETCOLUMN, (uint)j, vPointer.ToInt32());
                    NativeMethods.ReadProcessMemory(vProcess, (IntPtr)((int)vPointer + Marshal.SizeOf(typeof(NativeMethods.LVHEADER))), Marshal.UnsafeAddrOfPinnedArrayElement(vBuffer, 0), vBuffer.Length, ref vNumberOfBytesRead);
                    string vText = Marshal.PtrToStringAnsi(Marshal.UnsafeAddrOfPinnedArrayElement(vBuffer, 0));
                    dt.Columns.Add(vText);
                }

                for (int j = 0; j < ColumnCount; j++)
                {

                }

                for (int i = 0; i < vItemCount; i++)
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < ColumnCount; j++)
                    {
                        byte[] vBuffer = new byte[256];

                        NativeMethods.LVITEM[] vItem = new NativeMethods.LVITEM[1];
                        vItem[0] = new NativeMethods.LVITEM();
                        vItem[0].mask = NativeMethods.LVIF_TEXT;
                        vItem[0].iItem = i;
                        vItem[0].iSubItem = j;
                        vItem[0].cchTextMax = vBuffer.Length;
                        vItem[0].pszText = (IntPtr)((int)vPointer + Marshal.SizeOf(typeof(NativeMethods.LVITEM)));
                        uint vNumberOfBytesRead = 0;
                        NativeMethods.WriteProcessMemory(vProcess, vPointer, Marshal.UnsafeAddrOfPinnedArrayElement(vItem, 0), Marshal.SizeOf(typeof(NativeMethods.LVITEM)), ref vNumberOfBytesRead);
                        NativeMethods.SendMessageW(handle, NativeMethods.LVM_GETITEMW, (uint)i, vPointer.ToInt32());
                        NativeMethods.ReadProcessMemory(vProcess, (IntPtr)((int)vPointer + Marshal.SizeOf(typeof(NativeMethods.LVITEM))), Marshal.UnsafeAddrOfPinnedArrayElement(vBuffer, 0), vBuffer.Length, ref vNumberOfBytesRead);
                        string vText = Marshal.PtrToStringUni(Marshal.UnsafeAddrOfPinnedArrayElement(vBuffer, 0));
                        dr[j] = vText;
                    }
                    dt.Rows.Add(dr);
                    
                }
            }
            finally
            {
                NativeMethods.VirtualFreeEx(vProcess, vPointer, 0, NativeMethods.MEM_RELEASE);
                NativeMethods.CloseHandle(vProcess);
               
            }
            return dt;
        }
        public void CheckListViewItem(IntPtr handle, int index)
        {
            int vItemCount;
            int ColumnCount;
            IntPtr vHandle = handle;
            uint vNumberOfBytesRead = 0;
            if (vHandle == IntPtr.Zero) return;

            //选中Listview的某个Item 测试已工作
            vItemCount = NativeMethods.SendMessageW(vHandle, NativeMethods.LVM_GETITEMCOUNT, 0, 0);
            IntPtr handleHeader = new IntPtr(NativeMethods.SendMessageW(vHandle, NativeMethods.LVM_GETHEADER, 0, 0));
            ColumnCount = NativeMethods.SendMessageW(handleHeader, NativeMethods.HDM_GETITEMCOUNT, 0, 0);
            uint vProcessId;
            NativeMethods.GetWindowThreadProcessId(vHandle, out vProcessId);
            IntPtr vProcess = NativeMethods.OpenProcess(NativeMethods.PROCESS_VM_OPERATION | NativeMethods.PROCESS_VM_READ | NativeMethods.PROCESS_VM_WRITE, false, vProcessId);
            IntPtr vPointer = NativeMethods.VirtualAllocEx(vProcess, IntPtr.Zero, 4096, NativeMethods.MEM_RESERVE | NativeMethods.MEM_COMMIT, NativeMethods.PAGE_READWRITE);

            NativeMethods.LVITEM[] vItem = new NativeMethods.LVITEM[1];
            //vItem[0].state = NativeMethods.LVIS_SELECTED | NativeMethods.LVIS_FOCUSED | NativeMethods.LVIS_ACTIVATING;
            vItem[0].state = 0;
            vItem[0].stateMask = NativeMethods.LVIS_SELECTED | NativeMethods.LVIS_FOCUSED;

            NativeMethods.WriteProcessMemory(vProcess, vPointer, Marshal.UnsafeAddrOfPinnedArrayElement(vItem, 0), Marshal.SizeOf(typeof(NativeMethods.LVITEM)), ref vNumberOfBytesRead);

            for (int i = 0; i < vItemCount; i++)
            {
                NativeMethods.SendMessageW(handle, NativeMethods.LVM_SETITEMSTATE, (uint)i, vPointer.ToInt32());
            }
            NativeMethods.VirtualFreeEx(vProcess, vPointer, 0, NativeMethods.MEM_RELEASE);

            vItem[0].state = NativeMethods.LVIS_SELECTED | NativeMethods.LVIS_FOCUSED | NativeMethods.LVIS_ACTIVATING;
            vItem[0].stateMask = NativeMethods.LVIS_SELECTED| NativeMethods.LVIS_FOCUSED | NativeMethods.LVIS_ACTIVATING;
            vPointer = NativeMethods.VirtualAllocEx(vProcess, IntPtr.Zero, 4096, NativeMethods.MEM_RESERVE | NativeMethods.MEM_COMMIT, NativeMethods.PAGE_READWRITE);
            NativeMethods.WriteProcessMemory(vProcess, vPointer, Marshal.UnsafeAddrOfPinnedArrayElement(vItem, 0), Marshal.SizeOf(typeof(NativeMethods.LVITEM)), ref vNumberOfBytesRead);
            NativeMethods.SendMessageW(handle, NativeMethods.LVM_SETITEMSTATE, (uint)index, vPointer.ToInt32());


        }

        public bool CloseMessageBox(ref string message,IntPtr handle)
        {
            try {
                WindowsTree wt = ListWindow(handle);
                for(int i=0;i<wt.WindowCount;i++)
                {
                    WindowInfo info = wt.winfos[i];
                    //记录提示信息
                    if (info.title == "提示")
                    {
                        if ((sw.ElapsedMilliseconds > 20000) || (ptr_LastClosedHWND != info.hwnd)) //据上次关闭大于3S，或者要关闭的窗体是新窗体 （3S钟内不重复关闭一个窗体）
                        {
                            sw.Reset();
                            sw.Restart();
                            ptr_LastClosedHWND = info.hwnd;
                            AccessWin aw = new AccessWin();
                            WindowsTree wtsub = aw.ListWindow(info.hwnd);
                            WindowsTree wtfilter = wtsub.FiltedbyClassName("Static");
                            for (int j = 0; j < wtfilter.WindowCount; j++)
                            {
                                WindowInfo infosub = wtfilter.winfos[j];
                                message = message + infosub.title + " ";
                                message = message.Trim();
                            }

                            //关闭窗口
                            WindowsTree wtfilter2 = wtsub.FiltedbyClassName("Button");
                            ClickButton(wtfilter2.winfos[0].hwnd);
                            ClickButton(wtfilter2.winfos[0].hwnd);
                            return true;
                        }else
                        {
                            int iii = 0;
                            iii++;
                        }
                    } 

                }
            }
            catch (Exception ee)
            {
                Console.Write(ee.StackTrace);
                return false;
            }
            return false;

        }
        public bool CloseSystemTreeBox( string title, ref string message, IntPtr handle)
        {
            try
            {
                WindowsTree wt = ListWindow(handle);
                for (int i = 0; i < wt.WindowCount; i++)
                {
                    WindowInfo info = wt.winfos[i];
                    //记录提示信息
                    if (info.title == title)
                    {
                        AccessWin aw = new AccessWin();
                        WindowsTree wtsub = aw.ListWindow(info.hwnd);
                        WindowsTree wtfilter = wtsub.FiltedbyClassName("SysListView32");

                        DataTable dt = aw.GetListViewContent(wtfilter.winfos[0].hwnd);
                        for (int j=0;j<dt.Rows.Count;j++)
                        {
                            for (int k=0;k<dt.Columns.Count;k++)
                            {
                                message = message + dt.Rows[j][k].ToString()+"/";
                            }
                            
                        }

                        //关闭窗口
                        WindowsTree wtfilter2 = wtsub.FiltedbyClassName("Button");
                        ClickButton(wtfilter2.winfos[0].hwnd);
                        ClickButton(wtfilter2.winfos[0].hwnd);
                        return true;
                    }

                }
            }
            catch (Exception ee)
            {
                Console.Write(ee.StackTrace);
                return false;
            }
            return false;

        }
        public string FindText(IntPtr handle) //查找所有子控件包含的文本
        {
            //string message = "";
            //try
            //{
            //    WindowsTree wt = ListWindow(handle);
            //    for (int i = 0; i < wt.WindowCount; i++)
            //    {
            //        //记录提示信息
            //        WindowsTree wtfilter = wt.FiltedbyClassName("Static");
            //        for (int j = 0; j < wtfilter.WindowCount; j++)
            //        {
            //            WindowInfo infosub = wtfilter.winfos[j];
            //            message = message + infosub.title + " ";
            //            message = message.Trim();
            //        }
            //    }
            //}
            StringBuilder message = new StringBuilder("");
            string s_message = "";
            try
            {
                WindowsTree wt = ListWindow(handle);
                //记录提示信息
                WindowsTree wtfilter = wt.FiltedbyClassName("Static");
                if (wtfilter == null) return"";
                for (int j = 0; j < wtfilter.WindowCount; j++)
                {
                    WindowInfo infosub = wtfilter.winfos[j];
                    message.Append(infosub.title + " ");
                    s_message = message.ToString().Trim(); ;
                }
            }
            catch (Exception ee)
            {
                Console.Write(ee.StackTrace);
                return "";
            }
            return s_message;
        }
        //---------------------------------------------------------------二级函数 ----------------------------------------------------

    }
    public class ExternalCode //网上爬来的代码
    {
        public const int TV_FIRST = 0x1100;
        public const int TVM_GETCOUNT = TV_FIRST + 5;
        public const int TVM_GETNEXTITEM = TV_FIRST + 10;
        public const int TVM_GETITEMA = TV_FIRST + 12;
        public const int TVM_GETITEMW = TV_FIRST + 62;

        public const int TVGN_ROOT = 0x0000;
        public const int TVGN_NEXT = 0x0001;
        public const int TVGN_PREVIOUS = 0x0002;
        public const int TVGN_PARENT = 0x0003;
        public const int TVGN_CHILD = 0x0004;
        public const int TVGN_FIRSTVISIBLE = 0x0005;
        public const int TVGN_NEXTVISIBLE = 0x0006;
        public const int TVGN_PREVIOUSVISIBLE = 0x0007;
        public const int TVGN_DROPHILITE = 0x0008;
        public const int TVGN_CARET = 0x0009;
        public const int TVGN_LASTVISIBLE = 0x000A;

        public const int TVIF_TEXT = 0x0001;
        public const int TVIF_IMAGE = 0x0002;
        public const int TVIF_PARAM = 0x0004;
        public const int TVIF_STATE = 0x0008;
        public const int TVIF_HANDLE = 0x0010;
        public const int TVIF_SELECTEDIMAGE = 0x0020;
        public const int TVIF_CHILDREN = 0x0040;
        public const int TVIF_INTEGRAL = 0x0080;
        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd,
            uint Msg, int wParam, int lParam);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess,
            bool bInheritHandle, uint dwProcessId);
        public const uint MEM_COMMIT = 0x1000;
        public const uint MEM_RELEASE = 0x8000;

        public const uint MEM_RESERVE = 0x2000;
        public const uint PAGE_READWRITE = 4;

        public const uint PROCESS_VM_OPERATION = 0x0008;
        public const uint PROCESS_VM_READ = 0x0010;
        public const uint PROCESS_VM_WRITE = 0x0020;

        [DllImport("kernel32.dll")]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
            uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll")]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress,
           uint dwSize, uint dwFreeType);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
           IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
           IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd,
            out uint dwProcessId);

        [StructLayout(LayoutKind.Sequential)]
        public struct TVITEM
        {
            public int mask;
            public IntPtr hItem;
            public int state;
            public int stateMask;
            public IntPtr pszText;
            public int cchTextMax;
            public int iImage;
            public int iSelectedImage;
            public int cChildren;
            public IntPtr lParam;
            public IntPtr HTreeItem;
        }

        public static uint TreeView_GetCount(IntPtr hwnd)
        {
            return (uint)SendMessage(hwnd, TVM_GETCOUNT, 0, 0);
        }

        public static IntPtr TreeView_GetNextItem(IntPtr hwnd, IntPtr hitem, int code)
        {
            return (IntPtr)SendMessage(hwnd, TVM_GETNEXTITEM, code, (int)hitem);
        }

        public static IntPtr TreeView_GetRoot(IntPtr hwnd)
        {
            return TreeView_GetNextItem(hwnd, IntPtr.Zero, TVGN_ROOT);
        }

        public static IntPtr TreeView_GetChild(IntPtr hwnd, IntPtr hitem)
        {
            return TreeView_GetNextItem(hwnd, hitem, TVGN_CHILD);
        }

        public static IntPtr TreeView_GetNextSibling(IntPtr hwnd, IntPtr hitem)
        {
            return TreeView_GetNextItem(hwnd, hitem, TVGN_NEXT);
        }

        public static IntPtr TreeView_GetParent(IntPtr hwnd, IntPtr hitem)
        {
            return TreeView_GetNextItem(hwnd, hitem, TVGN_PARENT);
        }

        public static IntPtr TreeNodeGetNext(IntPtr AHandle, IntPtr ATreeItem)
        {
            if (AHandle == IntPtr.Zero || ATreeItem == IntPtr.Zero) return IntPtr.Zero;
            IntPtr result = TreeView_GetChild(AHandle, ATreeItem);
            if (result == IntPtr.Zero)
                result = TreeView_GetNextSibling(AHandle, ATreeItem);

            IntPtr vParentID = ATreeItem;
            while (result == IntPtr.Zero && vParentID != IntPtr.Zero)
            {
                vParentID = TreeView_GetParent(AHandle, vParentID);
                result = TreeView_GetNextSibling(AHandle, vParentID);
            }
            return result;
        }

        public static bool GetTreeViewText(IntPtr AHandle, DataTable dt_NodeInfo)
        {
            if (dt_NodeInfo == null) return false;
            dt_NodeInfo.Columns.Add("索引");
            dt_NodeInfo.Columns.Add("节点名称");
            dt_NodeInfo.Columns.Add("节点句柄");
            uint vProcessId;
            GetWindowThreadProcessId(AHandle, out vProcessId);

            IntPtr vProcess = OpenProcess(PROCESS_VM_OPERATION | PROCESS_VM_READ |
                PROCESS_VM_WRITE, false, vProcessId);
            IntPtr vPointer = VirtualAllocEx(vProcess, IntPtr.Zero, 4096,
                MEM_RESERVE | MEM_COMMIT, PAGE_READWRITE);
            try
            {
                uint vItemCount = TreeView_GetCount(AHandle);
                IntPtr vTreeItem = TreeView_GetRoot(AHandle);
                Console.WriteLine(vItemCount);
                for (int i = 0; i < vItemCount; i++)
                {
                    byte[] vBuffer = new byte[256];
                    TVITEM[] vItem = new TVITEM[1];
                    vItem[0] = new TVITEM();
                    vItem[0].mask = TVIF_TEXT;
                    vItem[0].hItem = vTreeItem;
                    vItem[0].pszText = (IntPtr)((int)vPointer + Marshal.SizeOf(typeof(TVITEM)));
                    vItem[0].cchTextMax = vBuffer.Length;
                    uint vNumberOfBytesRead = 0;
                    WriteProcessMemory(vProcess, vPointer, Marshal.UnsafeAddrOfPinnedArrayElement(vItem, 0), Marshal.SizeOf(typeof(TVITEM)), ref vNumberOfBytesRead);
                    SendMessage(AHandle, TVM_GETITEMA, 0, (int)vPointer);
                    ReadProcessMemory(vProcess, (IntPtr)((int)vPointer + Marshal.SizeOf(typeof(TVITEM))), Marshal.UnsafeAddrOfPinnedArrayElement(vBuffer, 0), vBuffer.Length, ref vNumberOfBytesRead);
                    //Console.WriteLine(Marshal.PtrToStringAnsi(Marshal.UnsafeAddrOfPinnedArrayElement(vBuffer, 0)));
                    string s_itemname = Marshal.PtrToStringAnsi(Marshal.UnsafeAddrOfPinnedArrayElement(vBuffer, 0));
                    DataRow dr = dt_NodeInfo.NewRow();
                    dr["索引"] = i;
                    dr["节点名称"] = s_itemname;
                    dr["节点句柄"] = Convert.ToString(vTreeItem.ToInt32(), 16);
                    dt_NodeInfo.Rows.Add(dr);
                    vTreeItem = TreeNodeGetNext(AHandle, vTreeItem);
                }
            }
            finally
            {
                VirtualFreeEx(vProcess, vPointer, 0, MEM_RELEASE);
                CloseHandle(vProcess);
            }
            return true;
        }
        [DllImport("user32.dll")]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "MoveWindow")]
        public static extern bool MoveWindow(System.IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left; //最左坐标
            public int Top; //最上坐标
            public int Right; //最右坐标
            public int Bottom; //最下坐标
        }
        [DllImport("user32.dll")]
        public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);
        [DllImport("user32.dll", EntryPoint = "keybd_event")]

        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);//虚拟键值  // 一般为0  //这里是整数类型 0 为按下，2为释放  //这里是整数类型 一般情况下设成为0 
    }

}
