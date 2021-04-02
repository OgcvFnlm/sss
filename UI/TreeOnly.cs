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
            this.imageList1.Images.AddRange(new Image[6]
            {
               Properties.Resources.NodeRootN,
               Properties.Resources.NodeRootS,
               Properties.Resources.NodeNoChildN,
               Properties.Resources.NodeNoChildS,
               Properties.Resources.NodeHasChildN,
               Properties.Resources.NodeHasChildS
            });
        }
        public void loadTree(object sender, login.ShowTreeWindowEventArgs e)
        {
            this.Text = e.caption;
            this.Name = e.name;
            this.ShowTree(null, this.treeView1.Nodes);
        }
        private void ShowTree(string ParentRoot,TreeNodeCollection Nodes)
        {
            //TreeNode Node;
            //TreeNodeCollection Nodes1;
            Model.TreeData TreeData = new Model.TreeData(1);
            BLL.FillTreeData.FillData(ref TreeData,this.Name, ParentRoot);
            //if (TreeData.Count > 0)
            //{
            //    string[] arr;
            //    for (int i = 0; i < TreeData.Count; i++){
            //        arr = TreeData[i];
            //        Node = Nodes.Add(arr[0], arr[1], ParentRoot == null ? 0 :2, ParentRoot == null ? 1 : 4);
            //        Node.Tag = arr[3];
            //        Nodes1 = Node.Nodes;
            //        this.ShowTree(Node.Name,Nodes1);
            //    }
            //}
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
