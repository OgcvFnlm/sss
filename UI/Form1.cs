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
    public partial class Form1 : Form
    {
        private string _TreeRootParent = "root";
        public Form1()
        {
            InitializeComponent();
            this.ShowTree(this._TreeRootParent);
        }
        public string TreeRootParent
        {
            get { return this._TreeRootParent; }
            set { this._TreeRootParent = value; }
        }
        private void ShowTree(string Parent)
        {
            TreeNode Node;
            Model.TreeData Data;
            Data.Parent = Parent;
            BLL.dim_manage TreeData = new BLL.dim_manage(ref Data);
            if (Data.Count > 0)
            {
                string[] arr;
                for (int i = 0; i < Data.Count; i++){
                    arr = Data[i];
                    Node = this.treeView1.Nodes.Add(arr[0], arr[1]);
                    Node.Tag = arr[2];
                    this.ShowTree(Node.Name,ref Node);
                }
            }

        }
        private void ShowTree(string Parent,ref TreeNode Node)
        {
            Model.TreeData Data;
            Data.Parent = Parent;
            BLL.dim_manage TreeData = new BLL.dim_manage(ref Data);
            if (Data.Count > 0)
            {
                string[] arr;
                for (int i = 0; i < Data.Count; i++)
                {
                    arr = Data[i];
                    Node = Node.Nodes.Add(arr[0], arr[1]);
                    Node.Tag = arr[2];
                    this.ShowTree(Node.Name, ref Node);
                }
            }

        }

    }
}
