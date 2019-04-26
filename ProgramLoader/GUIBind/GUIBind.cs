using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.Management;

namespace TDXBind
{
    public partial class GUIBind : Form
    {
        public GUIBind()
        {
            InitializeComponent();

        }

        private void btn_GetWindowList_Click(object sender, EventArgs e)
        {
            AccessWin aw = new AccessWin();
            AccessWin.WindowsTree wt = new AccessWin.WindowsTree();

            IntPtr pt = aw.GetWindowHandlebyTitle(txt_APPTitle.Text);
            txt_handle.Text = pt.ToString("X");  //display the handle in textbox
            //IntPtr vHandle = new IntPtr(Convert.ToInt32(txt_hndle.Text, 16));
            if (aw != null)
            {
                DataTable dt = new DataTable();
                dt = aw.ListWindow(pt).toDatatable();
                //dgv_list.DataSource = dt;
                AccessWin.WindowsTree wt2 = new AccessWin.WindowsTree();
                wt2.GetfromDataTable(dt);
                DataTable dt2 = wt2.toDatatable();
                dgv_list.DataSource = dt2;
            }
        }

        private void btn_click_Click(object sender, EventArgs e)
        {
            AccessWin aw = new AccessWin();
            aw.ClickButton(new IntPtr(Convert.ToInt32(txt_SelectedCell.Text, 16)));
        }

        private void btn_doubleclick_Click(object sender, EventArgs e)
        {
            AccessWin aw = new AccessWin();
            aw.DoubleClickButton(new IntPtr(Convert.ToInt32(txt_SelectedCell.Text, 16)));
        }

        private void btn_inputTXT_Click(object sender, EventArgs e)
        {
            AccessWin aw = new AccessWin();
            aw.SetText(new IntPtr(Convert.ToInt32(txt_SelectedCell.Text, 16)), txt_inputTXT.Text);
        }

        private void btn_changeselect_Click(object sender, EventArgs e)
        {
            AccessWin aw = new AccessWin();
            aw.SelectComboBoxIndex(new IntPtr(Convert.ToInt32(txt_SelectedCell.Text, 16)), Convert.ToInt16(txt_index.Text));
        }

        private void btn_sendKey_Click(object sender, EventArgs e)
        {
            AccessWin aw = new AccessWin();
            aw.SendKey(new IntPtr(Convert.ToInt32(txt_SelectedCell.Text, 16)), txt_KeyCode.Text);
        }

        private void btn_GetTXT_Click(object sender, EventArgs e)
        {
            AccessWin aw = new AccessWin();
            IntPtr vHandle = new IntPtr(Convert.ToInt32(txt_SelectedCell.Text, 16));
            txt_GetTXT.Text = aw.GetText(vHandle);
        }

        private void btn_GetSel_Click(object sender, EventArgs e)
        {
            AccessWin aw = new AccessWin();
            txt_select.Text = Convert.ToString(aw.GetComboBox(new IntPtr(Convert.ToInt32(txt_handle.Text, 16))));
        }



        private void btn_GetHandleByChild_Click(object sender, EventArgs e)
        {
            AccessWin aw = new AccessWin();
            IntPtr pt = new IntPtr(Convert.ToInt32(txt_SelectedCell.Text, 16));
            IntPtr parentHandle = NativeMethods.GetParent(pt);
            txtParentHandle1.Text = parentHandle.ToString("X");
        }

        private void txt_FindTitleFromList_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (DataGridViewRow dr in dgv_list.Rows)
            {
                if (dr.Cells[2].Value == null)
                {
                    MessageBox.Show("没找到");
                    break;
                }
                if (dr.Cells[2].Value.ToString() == (txt_title.Text))
                {
                    dgv_list.FirstDisplayedScrollingRowIndex = i;
                    break;
                }
                i++;

            }
        }

        private void btn_FindClassFromList_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (DataGridViewRow dr in dgv_list.Rows)
            {
                if (dr.Cells[3].Value == null)
                {
                    MessageBox.Show("没找到");
                    break;
                }
                if (dr.Cells[3].Value.ToString() == (txt_class.Text))
                {
                    dgv_list.FirstDisplayedScrollingRowIndex = i;
                    break;
                }
                i++;

            }
        }

        private void btn_FindHWNDFromList_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (DataGridViewRow dr in dgv_list.Rows)
            {
                if (dr.Cells[1].Value == null)
                {
                    MessageBox.Show("没找到");
                    break;
                }
                if (Convert.ToInt32(dr.Cells[1].Value.ToString(),16) == Convert.ToInt32((txt_HWND.Text),16))
                {
                    dgv_list.FirstDisplayedScrollingRowIndex = i;
                    break;
                }
                i++;

            }
        }

        private void dgv_list_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_SelectedCell.Text = dgv_list.SelectedCells[0].Value.ToString();
        }

        private void btn_GetSubWnd_Click(object sender, EventArgs e)
        {
            AccessWin aw = new AccessWin();
            AccessWin.WindowsTree wt = new AccessWin.WindowsTree();
            IntPtr pt = new IntPtr(Convert.ToInt32(txt_SelectedCell.Text, 16));
            txt_handle.Text = pt.ToString("X");  //display the handle in textbox
            //IntPtr vHandle = new IntPtr(Convert.ToInt32(txt_ListHandle.Text, 16));

            dgv_child.DataSource = aw.ListWindow(pt).toDatatable();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dgv_child_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_SelectedCell.Text = dgv_child.SelectedCells[0].Value.ToString();
        }

        private void btn_GetGridContent_Click(object sender, EventArgs e)
        {
            AccessWin aw = new AccessWin();
            IntPtr vHandle = new IntPtr(Convert.ToInt32(txt_SelectedCell.Text, 16));
            DataTable dt = aw.GetListViewContent(vHandle);
            dgv_Listview.DataSource = dt;
        }

        private void btn_GetTreeViewCount_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            IntPtr vHandle = new IntPtr(Convert.ToInt32(txt_SelectedCell.Text, 16));
            ExternalCode.GetTreeViewText(vHandle, dt);
            dgv_TreeView.DataSource = dt;

        }

        private void btn_selectNode_Click(object sender, EventArgs e)
        {
            IntPtr vRootHandle = new IntPtr(Convert.ToInt32(txt_SelectedCell.Text, 16));
            string s_handle = dgv_TreeView.SelectedCells[0].OwningRow.Cells["节点句柄"].Value.ToString();
            int handle = Convert.ToInt32(s_handle, 16);
            IntPtr noodptr = new IntPtr(handle);
            NativeMethods.SendMessageW(vRootHandle, NativeMethods.TVM_SELECTITEM, NativeMethods.TVGN_CARET, noodptr.ToInt32());
            NativeMethods.SendMessageW(vRootHandle, NativeMethods.TVM_EXPAND, NativeMethods.TVE_EXPAND, noodptr.ToInt32());
        }

        private void dgv_TreeView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //txt_SelectedCell.Text = dgv_TreeView.SelectedCells[0].Value.ToString();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int x, y;
            x = System.Windows.Forms.Cursor.Position.X;
            y = System.Windows.Forms.Cursor.Position.Y;
            txt_X.Text = x.ToString();
            txt_Y.Text = y.ToString();
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }


        private void btn_ClickPos_Click(object sender, EventArgs e)
        {
            AccessWin aw = new AccessWin();
            //0，0的话单击当前位置
            aw.ClickLocation(Convert.ToInt32(txt_posX.Text), Convert.ToInt32(txt_posY.Text));
        }

        private void btn_FilterTitle_Click(object sender, EventArgs e)
        {
            AccessWin aw = new AccessWin();
            AccessWin.WindowsTree wt = new AccessWin.WindowsTree();
            IntPtr pt = aw.GetWindowHandlebyTitle(txt_APPTitle.Text);
            txt_handle.Text = pt.ToString("X");  //display the handle in textbox
            //IntPtr vHandle = new IntPtr(Convert.ToInt32(txt_ListHandle.Text, 16));
            wt = aw.ListWindow(pt);
            try
            {
                DataTable dt2 = wt.FiltedbyTitle(txt_FilterTitle.Text).toDatatable();
                if (dt2 != null)
                { dgv_list.DataSource = dt2; }
            }
            catch
            { }
        }

        private void btn_FilterClassName_Click(object sender, EventArgs e)
        {
            AccessWin aw = new AccessWin();
            AccessWin.WindowsTree wt = new AccessWin.WindowsTree();
            IntPtr pt = aw.GetWindowHandlebyTitle(txt_APPTitle.Text);
            txt_handle.Text = pt.ToString("X");  //display the handle in textbox
            //IntPtr vHandle = new IntPtr(Convert.ToInt32(txt_ListHandle.Text, 16));
            wt = aw.ListWindow(pt);
            try
            {
                dgv_list.DataSource = wt.FiltedbyClassName(txt_FilterClassName.Text).toDatatable();
            }
            catch
            { }
        }

        private void btn_FilterHWND_Click(object sender, EventArgs e)
        {
            AccessWin aw = new AccessWin();
            AccessWin.WindowsTree wt = new AccessWin.WindowsTree();
            IntPtr pt = aw.GetWindowHandlebyTitle(txt_APPTitle.Text);
            txt_handle.Text = pt.ToString("X");  //display the handle in textbox
            //IntPtr vHandle = new IntPtr(Convert.ToInt32(txt_ListHandle.Text, 16));
            wt = aw.ListWindow(pt);
            try
            {
                dgv_list.DataSource = wt.FiltedbyHWND(Convert.ToInt32(txt_FilterHWND.Text, 16)).toDatatable();
            }
            catch
            { }
        }

        private void btn_GetBrotherWindowTree_Click(object sender, EventArgs e)
        {
            AccessWin aw = new AccessWin();
            AccessWin.WindowsTree wt = new AccessWin.WindowsTree();
            IntPtr pt = new IntPtr(Convert.ToInt32(txt_SelectedCell.Text, 16));
            txt_handle.Text = pt.ToString("X");  //display the handle in textbox
            //IntPtr vHandle = new IntPtr(Convert.ToInt32(txt_ListHandle.Text, 16));

            dgv_child.DataSource = aw.ListBrotherWindow(pt).toDatatable();
        }

        private void btn_ClickPOSs_Click(object sender, EventArgs e)
        {
            DataTable dt_Pos = new DataTable("POS");
            dt_Pos.Columns.Add("X");
            dt_Pos.Columns.Add("Y");
            dt_Pos.Rows.Add(100, 100);
            dt_Pos.Rows.Add(100, 200);
            dt_Pos.Rows.Add(100, 300);
            AccessWin aw = new AccessWin();
            aw.ClickPOSList(dt_Pos, 100);

        }

        private void btn_SendKeyGlobal_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SendKeys.Send(txt_KeyCode.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                AccessWin aw = new AccessWin();

                aw.ClickLocation(Convert.ToInt32(txt_posX.Text), Convert.ToInt32(txt_posY.Text));
                System.Windows.Forms.SendKeys.Send(txt_keytoPos.Text);
                System.Windows.Forms.SendKeys.Send("{ENTER}");
            }
            catch
            { }
        }

        private void btn_ActiveWindow_Click(object sender, EventArgs e)
        {
            IntPtr pt = new IntPtr(Convert.ToInt32(txt_SelectedCell.Text, 16));
            ExternalCode.SetActiveWindow(pt);
            ExternalCode.SetForegroundWindow(pt);
        }

        private void btn_CloseMessageBox_Click(object sender, EventArgs e)
        {
            IntPtr pt;
            try
            {
                 pt = new IntPtr(Convert.ToInt32(txt_SelectedCell.Text, 16));
            }catch
            {
                pt = new IntPtr(0);
            }
            AccessWin aw = new AccessWin();
            string s = "";
            aw.CloseMessageBox(ref s, pt);
            txt_Message.Text = s;
        }

        private void btn_Execute_Click(object sender, EventArgs e)
        {
            Process p=Process.Start(txt_ExecutePath.Text);
            Thread.Sleep(1000);
            txt_PID.Text = p.Id.ToString();
            txt_ExecuteHandle.Text = p.MainWindowHandle.ToInt32().ToString("X");

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Process p = Process.GetProcessById(Convert.ToInt32(txt_PID.Text));
                p.Kill();
                
            }
            catch
            { }
        }

        private void btn_MoveWindow_Click(object sender, EventArgs e)
        {
            IntPtr ptr = new IntPtr(Convert.ToInt32(txt_SelectedCell.Text, 16));
            ExternalCode.MoveWindow(ptr, Convert.ToInt32(txt_WindowX.Text), Convert.ToInt32(txt_WindowY.Text), Convert.ToInt32(txt_WindowWidth.Text), Convert.ToInt32(txt_WindowHeight.Text), true);
        }

        private void btn_GetWindowPos_Click(object sender, EventArgs e)
        {
            IntPtr ptr = new IntPtr(Convert.ToInt32(txt_SelectedCell.Text, 16));
            ExternalCode.RECT rect = new ExternalCode.RECT();
            ExternalCode.GetWindowRect(ptr, ref rect);

            txt_WindowX.Text = rect.Left.ToString();
            txt_WindowY.Text  = rect.Top.ToString();

            txt_WindowHeight.Text = (rect.Bottom - rect.Top).ToString();
            txt_WindowWidth.Text = (rect.Right - rect.Left).ToString();


        }

        private void txt_WindowHeight_TextChanged(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void txt_WindowWidth_TextChanged(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label111_Click(object sender, EventArgs e)
        {

        }

        private void txt_WindowY_TextChanged(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void txt_WindowX_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_ListViewItem_Click(object sender, EventArgs e)
        {
            AccessWin aw = new AccessWin();
            IntPtr vHandle = new IntPtr(Convert.ToInt32(txt_SelectedCell.Text, 16));
            aw.CheckListViewItem(vHandle,dgv_Listview.SelectedCells[0].OwningRow.Index);
            IntPtr vHandle2 = new IntPtr(Convert.ToInt32(txt_handle.Text, 16));

            //NativeMethods.SetActiveWindow(vHandle);
            //ExternalCode.keybd_event(0x0d, 0x1c, 0, 0);
            //ExternalCode.keybd_event(0x0d, 0x1c, 2, 0);


            //IntPtr handle = new IntPtr(Convert.ToInt32(txt_SelectedCell.Text, 16));
            //int keycode = Convert.ToInt32(txt_KeyDownCode.Text, 16);
            //NativeMethods.SendMessageW(handle, NativeMethods.WM_KEYDOWN, (uint)keycode, 0x1c0001);
        }

        private void btn_SendSpace_Click(object sender, EventArgs e)
        {
            try
            {
                AccessWin aw = new AccessWin();
                IntPtr handle = new IntPtr(Convert.ToInt32(txt_SelectedCell.Text, 16));
                NativeMethods.SendMessageW(handle, NativeMethods.WM_KEYDOWN, 0x20, 0);

            }
            catch

            { }
        }

        private void btn_KeyDown_Click(object sender, EventArgs e)
        {
            AccessWin aw = new AccessWin();
            IntPtr handle = new IntPtr(Convert.ToInt32(txt_SelectedCell.Text, 16));
            int keycode = Convert.ToInt32(txt_KeyDownCode.Text, 16);
            NativeMethods.SendMessageW(handle, NativeMethods.WM_KEYDOWN, (uint)keycode, 0x1c0001);
        }
    }
}
