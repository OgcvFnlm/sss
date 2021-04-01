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
    public partial class dim_manage : Form
    {
        private string TreeRootParent;
        public dim_manage(string Parent)
        {
            InitializeComponent();
            TreeNodeCollection Nodes = this.treeView1.Nodes;
            this.TreeRootParent = Parent;
            this.ShowTree(this.TreeRootParent,ref Nodes);
        }
        private void ShowTree(string Parent,ref TreeNodeCollection Nodes)
        {
            TreeNode Node;
            TreeNodeCollection Nodes1;
            Model.TreeData Data =new Model.TreeData(Parent);
            BLL.dim_manage TreeData = new BLL.dim_manage(ref Data);
            if (Data.Count > 0)
            {
                string[] arr;
                for (int i = 0; i < Data.Count; i++){
                    arr = Data[i];
                    Node = Nodes.Add(arr[0], arr[1],Data.Parent == this.TreeRootParent ? 0 :2, Data.Parent==this.TreeRootParent ? 1 : 3);
                    Node.Tag = arr[2];
                    Nodes1 = Node.Nodes;
                    this.ShowTree(Node.Name,ref Nodes1);
                }
            }

        }

    }
}
