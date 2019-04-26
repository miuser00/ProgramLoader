using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Data.OleDb;
using System.Data;
using System.Collections;


namespace MCSV
{
    public class MCSV_OLD
    {

        public static void ExportToSvc(System.Data.DataTable dt, string strName)
            {
                string strPath = strName;

                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }
                //先打印标头
                StringBuilder strColu = new StringBuilder();
                StringBuilder strValue = new StringBuilder();
                int i = 0;

                try
                {
                    StreamWriter sw = new StreamWriter(new FileStream(strPath, FileMode.CreateNew), Encoding.GetEncoding("GB2312"));

                    for (i = 0; i <= dt.Columns.Count - 1; i++)
                    {
                        strColu.Append(dt.Columns[i].ColumnName);
                        strColu.Append(",");
                    }
                    strColu.Remove(strColu.Length - 1, 1);//移出掉最后一个,字符

                    sw.WriteLine(strColu);

                    foreach (DataRow dr in dt.Rows)
                    {
                        strValue.Remove(0, strValue.Length);//移出

                        for (i = 0; i <= dt.Columns.Count - 1; i++)
                        {
                            strValue.Append(dr[i].ToString());
                            strValue.Append(",");
                        }
                        strValue.Remove(strValue.Length - 1, 1);//移出掉最后一个,字符
                        sw.WriteLine(strValue);
                    }

                    sw.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        public static void OpenCSVFile(ref DataTable mycsvdt, string filepath)
        {
            string strpath = filepath; //csv文件的路径
            try
            {
                int intColCount = 0;
                bool blnFlag = true;

                DataColumn mydc;
                DataRow mydr;

                string strline;
                string[] aryline;
                StreamReader mysr = new StreamReader(strpath, System.Text.Encoding.Default);

                while ((strline = mysr.ReadLine()) != null)
                {
                    aryline = strline.Split(new char[] { ',' });

                    //给datatable加上列名
                    if (blnFlag)
                    {
                        blnFlag = false;
                        intColCount = aryline.Length;
                        int col = 0;
                        for (int i = 0; i < aryline.Length; i++)
                        {
                            col = i + 1;
                            mydc = new DataColumn(col.ToString());
                            mycsvdt.Columns.Add(mydc);
                        }
                    }

                    //填充数据并加入到datatable中
                    mydr = mycsvdt.NewRow();
                    for (int i = 0; i < intColCount; i++)
                    {
                        mydr[i] = aryline[i];
                    }
                    mycsvdt.Rows.Add(mydr);
                }

            }
            catch (Exception e)
            {

                throw (e);
            }
        }
        public static string OpenTDXExportFile(ref DataTable mycsvdt, string filepath,ref string NO,ref string Name,ref string Type,ref string other)
        {
            string strpath = filepath; //csv文件的路径
            try
            {
                int intColCount = 0;

                DataColumn mydc;
                DataRow mydr;

                string strline;
                string[] aryline;
                StreamReader mysr = new StreamReader(strpath, System.Text.Encoding.Default);

                //读入第一行，代码，品种，数据类型
                if ((strline = mysr.ReadLine()) == null) return "无数据";  //读入一行，如果读到文件尾，直接返回
                aryline = strline.Split(new char[] { ' ' });
                NO = aryline[0];
                Name = aryline[1];
                Type = aryline[2];
                other = aryline[3];

                //给datatable加上列名
                if ((strline = mysr.ReadLine()) == null) return "无数据";
                aryline = strline.Split(new char[] { '\t' });
                intColCount = aryline.Length;
                for (int i = 0; i < aryline.Length; i++)
                {
                    mydc = new DataColumn(aryline[i].Trim());
                    mycsvdt.Columns.Add(mydc);
                }

                while ((strline = mysr.ReadLine()) != null)
                {
                    aryline = strline.Split(new char[] { ',' });
                    if (aryline.Length != intColCount) break;
                    //填充数据并加入到datatable中
                    mydr = mycsvdt.NewRow();
                    for (int i = 0; i < intColCount; i++)
                    {
                        mydr[i] = aryline[i];
                    }
                     mycsvdt.Rows.Add(mydr);
                }

            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "读取成功";
        }
    }
    public class CSV
    {
        public static void Removemsg(ref DataTable tab)
        {
            for (int i = tab.Rows.Count - 1; i >= 0; i--)
            {
                string s_TableTitle = (String)(tab.Rows[i][0]);
                if ((s_TableTitle.Contains("TES system")) || (s_TableTitle.Contains("\\")) || (s_TableTitle.Contains("通达信")))
                {
                    tab.Rows.RemoveAt(i);
                }
                else
                {
                    break;
                }


            }
        }
        public static void OpenCSVFile(ref DataTable mycsvdt, string filepath)
        {
            string strpath = filepath; //csv文件的路径
            try
            {
                int intColCount = 0;
                bool blnFlag = true;

                DataColumn mydc;
                DataRow mydr;

                string strline;
                string[] aryline;
                FileStream fs = new FileStream(strpath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader mysr = new StreamReader(fs, System.Text.Encoding.Default);


                //StreamReader mysr = new StreamReader(strpath, System.Text.Encoding.Default);

                while ((strline = mysr.ReadLine()) != null)
                {
                    aryline = strline.Split(new char[] { ',' });

                    //给datatable加上列名
                    if (blnFlag)
                    {
                        blnFlag = false;
                        intColCount = aryline.Length;
                        int col = 0;
                        for (int i = 0; i < aryline.Length; i++)
                        {
                            col = i + 1;
                            mydc = new DataColumn(col.ToString());
                            mycsvdt.Columns.Add(mydc);
                        }
                    }

                    //填充数据并加入到datatable中
                    mydr = mycsvdt.NewRow();
                    for (int i = 0; i < intColCount; i++)
                    {
                        try
                        {
                            mydr[i] = aryline[i];
                        }
                        catch
                        {
                        }
                    }
                    mycsvdt.Rows.Add(mydr);
                }

            }
            catch (Exception e)
            {

                throw (e);
            }
        }
        public static void OpenCSVFileWithHeader(ref DataTable mycsvdt, string filepath)
        {
            string strpath = filepath; //csv文件的路径
            try
            {
                int intColCount = 0;
                bool blnFlag = true;

                DataColumn mydc;
                DataRow mydr;

                string strline;
                string[] aryline;
                FileStream fs = new FileStream(strpath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader mysr = new StreamReader(fs, System.Text.Encoding.Default);


                //StreamReader mysr = new StreamReader(strpath, System.Text.Encoding.Default);

                while ((strline = mysr.ReadLine()) != null)
                {
                    aryline = strline.Split(new char[] { ',' });

                    //给datatable加上列名
                    if (blnFlag)
                    {
                        blnFlag = false;
                        intColCount = aryline.Length;
                        int col = 0;
                        for (int i = 0; i < aryline.Length; i++)
                        {
                            col = i + 1;
                            mydc = new DataColumn(col.ToString());
                            mycsvdt.Columns.Add(aryline[i]);
                        }
                        continue;
                    }

                    //填充数据并加入到datatable中
                    mydr = mycsvdt.NewRow();
                    for (int i = 0; i < intColCount; i++)
                    {
                        try
                        {
                            mydr[i] = aryline[i];
                        }
                        catch { }
                    }
                    mycsvdt.Rows.Add(mydr);
                }

            }
            catch (Exception e)
            {

                throw (e);
            }
        }
        /// <summary>
        /// 将DataTable中数据写入到CSV文件中
        /// </summary>
        /// <param name="dt">提供保存数据的DataTable</param>
        /// <param name="msg">尾行数据</param>
        /// <param name="fileName">CSV的文件路径</param>
        public static void SaveCSV(DataTable dt, string msg, string fileName)
        {
            FileStream fs = new FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
            string data = "";
            #region dt写入
            //写出列名称
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                data += dt.Columns[i].ColumnName.ToString();
                if (i < dt.Columns.Count - 1)
                {
                    data += ",";
                }
            }
            sw.WriteLine(data);


            //写出各行数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                data = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    data += dt.Rows[i][j].ToString();
                    if (j < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);
            }
            #endregion
            #region msg写入
            data = msg;

            sw.WriteLine(data);


            #endregion
            sw.Close();
            fs.Close();
        }
    }
}