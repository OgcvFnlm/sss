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
    public partial class login : Form, FrameWork.TreeFrom.CallTreeFrm
    {
        public delegate void LoadTreeEventHandler(FrameWork.TreeFrom.CallTreeFrm sender, FrameWork.TreeFrom.LoadTreeEventArg e);
        public event LoadTreeEventHandler LoadTreeClick;
        public class LoadTreeEventArgs : EventArgs, FrameWork.TreeFrom.LoadTreeEventArg
        {
            public BLL.TreeLogic TreeLogic { set; get; }
            public LoadTreeEventArgs(BLL.TreeLogic TL)
            {
                TreeLogic = TL;
            }
        }
        public void TreeNodeSelected(FrameWork.TreeFrom.TreeNodeSelectedEventArg e)
        {

        }
        private void OnLoadTree(FrameWork.TreeFrom.CallTreeFrm frm, FrameWork.TreeFrom.LoadTreeEventArg e)
        {
            if (LoadTreeClick != null)
            {
                LoadTreeClick(this, e);
            }
        }
        public login()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            FrameWork.TreeFrom.TreeOnly frm = new FrameWork.TreeFrom.TreeOnly(new Size(230,320),new Size(230,320));
            LoadTreeClick = frm.LoadTreeNode;
            OnLoadTree(this, new LoadTreeEventArgs(new BLL.CashTreeLogic()));
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrameWork.TreeFrom.TreeOnly frm = new FrameWork.TreeFrom.TreeOnly(new Size(230, 320), new Size(230, 320));
            LoadTreeClick = frm.LoadTreeNode;
            OnLoadTree(this, new LoadTreeEventArgs(new BLL.DimTreeLogic()));
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrameWork.TreeFrom.TreeOnly frm = new FrameWork.TreeFrom.TreeOnly(new Size(230, 320), new Size(230, 320));
            LoadTreeClick = frm.LoadTreeNode;
            OnLoadTree(this, new LoadTreeEventArgs(new BLL.AccTreeLogic()));
            frm.ShowDialog();
        }
    }
}
