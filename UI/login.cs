using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class login : Form
    {
        public delegate void ShowTreeWindowEventHandler(Object sender, ShowTreeWindowEventArgs e);
        public event ShowTreeWindowEventHandler ShowTreeWindowClick;
        public class ShowTreeWindowEventArgs : EventArgs
        {
            public string name { get; }
            public string caption { get; }
            public ShowTreeWindowEventArgs(string name,string caption)
            {
                this.name = name;
                this.caption = caption;
            }
        }
        private void OnShowTreeWindow(TreeOnly frm,ShowTreeWindowEventArgs e)
        {
            if (ShowTreeWindowClick != null)
            {
                ShowTreeWindowClick(this, e);
                frm.ShowDialog();
            }
        }
        public login()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
   
            TreeOnly frm = new TreeOnly();
            this.ShowTreeWindowClick = frm.loadTree;
            OnShowTreeWindow(frm, new ShowTreeWindowEventArgs("cash", "现金流项目选择器"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TreeOnly frm = new TreeOnly();
            this.ShowTreeWindowClick = frm.loadTree;
            OnShowTreeWindow(frm, new ShowTreeWindowEventArgs("dims", "辅助核算项目选择器"));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TreeOnly frm = new TreeOnly();
            this.ShowTreeWindowClick = frm.loadTree;
            OnShowTreeWindow(frm, new ShowTreeWindowEventArgs("accs", "会计科目选择器"));
        }
    }
}
