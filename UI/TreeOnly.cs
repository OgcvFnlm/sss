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
    public partial class TreeOnly : Form
    {
        public TreeOnly()
        {
            InitializeComponent();
        }
        public void loadTree()
        {
            this.ShowTree(null, this.treeView1.Nodes);
        }
        private void ShowTree(string ParentRoot,TreeNodeCollection Nodes)
        {
            TreeNode Node;
            TreeNodeCollection Nodes1;
            Model.TreeData TreeData;
            BLL.FillTreeData.FillData(ref TreeData,this.Name, ParentRoot);
            if (TreeData.Count > 0)
            {
                string[] arr;
                for (int i = 0; i < TreeData.Count; i++){
                    arr = TreeData[i];
                    Node = Nodes.Add(arr[0], arr[1], ParentRoot == null ? 0 :2, ParentRoot == null ? 1 : 3);
                    Node.Tag = arr[3];
                    Nodes1 = Node.Nodes;
                    this.ShowTree(Node.Name,Nodes1);
                }
            }
        }

        protected virtual void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip1.Show(MousePosition);
            }
        }



    }
}
