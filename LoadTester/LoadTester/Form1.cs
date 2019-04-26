using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadTester
{
    public partial class Form1 : Form
    {
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
                for (int i=0;i<args.Length;i++)
                arg = arg+args[i];
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            txt_arg.Text = arg;
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            Console.WriteLine(txt_con.Text);
        }
    }
}
