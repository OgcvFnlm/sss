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
        public login()
        {
            InitializeComponent();
        }
        public class TreeSelectFrm : TreeOnly
        {

            public TreeSelectFrm(string Name)
            {
                base.Name = Name;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            TreeOnly frm = new TreeSelectFrm("cash");
            frm.loadTree();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TreeOnly frm = new TreeSelectFrm("dims");
            frm.loadTree();
            frm.ShowDialog();
        }
    }
}
