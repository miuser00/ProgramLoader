using System;
using System.Collections.Generic;
using System.Text;

namespace TDXBind
{
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate int WNDENUMPROC(System.IntPtr param0, System.IntPtr param1);

    public partial class NativeMethods
    {

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "FindWindowA")]
        public static extern System.IntPtr FindWindowA([System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string lpClassName, [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string lpWindowName);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "EnumWindows")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "GetWindowTextW")]
        public static extern int GetWindowTextW([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd, [System.Runtime.InteropServices.OutAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)] System.Text.StringBuilder lpString, int nMaxCount);  
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "GetClassNameW")]
        public static extern int GetClassNameW([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd, [System.Runtime.InteropServices.OutAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)] System.Text.StringBuilder lpClassName, int nMaxCount);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "EnumChildWindows")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool EnumChildWindows([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWndParent, WNDENUMPROC lpEnumFunc, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "SendMessageW")]
        //[return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.SysInt)]
        public static extern int SendMessageW([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd, uint Msg, uint wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "SendMessageW")]
        //[return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.SysInt)]
        public static extern int SendMessageW2([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd, uint Msg, uint wParam, IntPtr lParam);

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "PostMessageW")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool PostMessageW([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd, uint Msg, uint wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "SetWindowTextW")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool SetWindowTextW([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd, [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)] string lpString);

        //-----------new added declaration
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "GetWindowThreadProcessId")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint dwProcessId); public const uint PROCESS_VM_OPERATION = 0x0008; public const uint PROCESS_VM_READ = 0x0010; public const uint PROCESS_VM_WRITE = 0x0020;
        [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "OpenProcess")]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId); public const uint MEM_COMMIT = 0x1000; public const uint MEM_RELEASE = 0x8000; public const uint MEM_RESERVE = 0x2000; public const uint PAGE_READWRITE = 4;
        [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "VirtualAllocEx")]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);
        [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "VirtualFreeEx")]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint dwFreeType);
        [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "CloseHandle")]
        public static extern bool CloseHandle(IntPtr handle);
        [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead);
        [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "ReadProcessMemory")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead);

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "GetDlgCtrlID")]
        public static extern int GetDlgCtrlID([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "GetParent")]
        public static extern System.IntPtr GetParent([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "GetWindowLongW")]
        public static extern int GetWindowLongW([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd, int nIndex);


        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "GetWindowRect")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool GetWindowRect([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd, [System.Runtime.InteropServices.OutAttribute()] out tagRECT lpRect);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "mouse_event")]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, uint dwExtraInfo);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "SetCursorPos")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool SetCursorPos(int X, int Y);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "SetActiveWindow")]
        public static extern System.IntPtr SetActiveWindow([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "SetWindowPos")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool SetWindowPos([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd, [System.Runtime.InteropServices.InAttribute()] System.IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "GetCursorPos")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool GetCursorPos([System.Runtime.InteropServices.OutAttribute()] out Point lpPoint);

        public const int WM_LBUTTONDOWN = 513;
        public const int WM_LBUTTONUP = 514;
        public const int WM_LBUTTONDBLCLK = 515;
        public const int WM_CHAR = 258;
        public const int WM_CLOSE = 16;

        public const int WM_SETTEXT = 12;
        public const uint LVM_FIRST = 0x1000;
        public const uint LVM_GETITEMCOUNT = LVM_FIRST + 4;
        public const uint LVM_GETITEMW = LVM_FIRST + 75;
        public const uint LVM_GETCOLUMNA =LVM_FIRST+ 25;
¡¡¡¡    public const uint LVM_GETCOLUMN =LVM_GETCOLUMNA;
        public const uint LVM_GETHEADER = LVM_FIRST + 31;
        public const uint HDM_GETITEMCOUNT = 0x1200;
        public const int CB_SETCURSEL = 334;
        public const int CB_GETCURSEL = 327;
        public const int NM_FIRST = 0;
        public const int NM_DBLCLK = (NM_FIRST - 3);
        public const uint LVM_SETITEMSTATE =LVM_FIRST + 43;
        public const int LVIS_SELECTED=0x0002;
        public const int LVIS_FOCUSED = 0x0001;
        public const int LVIS_ACTIVATING = 0x0020;
        public const int GWL_ID = -12;
        public const int WM_NOTIFY = 78;
        public const int WM_KEYDOWN = 256;
        public const int VK_F1=0x70;
        public const int VK_F2=0x71;
        public const int VK_F3=0x72;
        public const int VK_F4=0x73;
        public const int VK_F5=0x74;
        public const int WM_SYSKEYDOWN = 260;
        public const int WM_KEYUP = 257;
        public const int WM_GETTEXT = 13;
        public const int MOUSEEVENTF_LEFTDOWN = 2;
        public const int MOUSEEVENTF_LEFTUP = 4;
        public const int HWND_TOP = 0;
        public const int SWP_NOMOVE = 2;
        public const int SWP_NOSIZE = 1;
        public const int SWP_NOZORDER = 0x0004;
        public const int LVCF_TEXT = 0x4;

        public const int BN_CLICKED = 0xF5;
        public const int TV_FIRST = 0x1100;
        public const int TVGN_ROOT = 0x0;
        public const int TVGN_NEXT = 0x1;
        public const int TVGN_CHILD = 0x4;
        public const int TVGN_FIRSTVISIBLE = 0x5;
        public const int TVGN_NEXTVISIBLE = 0x6;
        public const int TVGN_CARET = 0x9;
        public const int TVM_SELECTITEM = (TV_FIRST + 11);
        public const int TVM_GETNEXTITEM = (TV_FIRST + 10);
        public const int TVM_GETITEM = (TV_FIRST + 12);
        public const int TVIF_TEXT = 0x0001;

        public const int GMEM_FIXED = 0x0000;
        public const int TVM_EXPAND = 4354;
        public const int TVE_EXPAND = 2;

        public struct LVITEM
        {
            public int mask;
            public int iItem;
            public int iSubItem;
            public int state;
            public int stateMask;
            public IntPtr pszText; // string    
            public int cchTextMax;
            public int iImage;
            public IntPtr lParam;
            public int iIndent;
            public int iGroupId;
            public int cColumns;
            public IntPtr puColumns;
        }
        public struct LVHEADER
        {
            public int mask;
            public int fmt;
            public int cx;
            public IntPtr pszText;
            public int cchTextMax;
            public int iSubItem;

            public int cxMin;
            public int cxDefault;
            public int cxIdeal;
        }
        public struct TVITEM
        {
            public int mask;
            public IntPtr hItem;
            public int state;
            public int stageMask;
            public IntPtr pszText;
            public int cchTextMax;
            public int iIMage;
            public int iSelectedImage;
            public int cChildren;
            public IntPtr lParam;

                
        }
        public const int LVIF_TEXT = 0x0001;

        public struct NMHDR
        {

            /// HWND->HWND__*
            public System.IntPtr hwndFrom;

            /// UINT_PTR->unsigned int
            public uint idFrom;

            /// UINT->unsigned int
            public int code;
        }
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct tagRECT
        {

            /// LONG->int
            public int left;

            /// LONG->int
            public int top;

            /// LONG->int
            public int right;

            /// LONG->int
            public int bottom;
        }
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct Point
        {

            /// LONG->int
            public int x;

            /// LONG->int
            public int y;
        }

    }

}