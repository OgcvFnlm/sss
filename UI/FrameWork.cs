using System;
using System.Windows.Forms;
using System.Drawing;

namespace UI.FrameWork
{
    #region 树形图查询、选择窗体的接口和基础类
    namespace TreeFrom {
        public interface TreeImgs
        {
            void InitImgs();
            void LoadImgsItem();
            void InitTree(Size TreeSize);
            void LoadTreeNode(CallTreeFrm sender, LoadTreeEventArg e);
        }
        public interface LoadTreeEventArg
        {
            BLL.TreeLogic TreeLogic { set; get; }
        }
        public interface TreeNodeSelectedEventArg
        {
            TreeNode[] SelectedNode { set; get; }
        }
        public interface CallTreeFrm
        {
            void TreeNodeSelected(TreeNodeSelectedEventArg e);
        }
        class TreeOnly : Form, TreeImgs
        {
            private ImageList Imgs;
            public virtual void InitImgs()
            {
                Imgs = new ImageList();
            }
            public virtual void LoadImgsItem()
            {
                Imgs.Images.AddRange(new Image[6]
                {
                   Properties.Resources.NodeRootN,
                   Properties.Resources.NodeRootS,
                   Properties.Resources.NodeNoChildN,
                   Properties.Resources.NodeNoChildS,
                   Properties.Resources.NodeHasChildN,
                   Properties.Resources.NodeHasChildS
                });
            }
            private TreeView Tree;
            public virtual void InitTree(Size TreeSize)
            {
                Tree = new TreeView();
                Tree.Location = new Point(0, 0);
                Tree.Size = TreeSize;
                Tree.ImageList = Imgs;
            }
            public TreeOnly(Size BaseClientSize, Size TreeSize)
            {
                this.SuspendLayout();
                base.ClientSize = BaseClientSize;
                InitImgs();
                LoadImgsItem();
                InitTree(TreeSize);
                this.Controls.Add(Tree);
                this.ResumeLayout(false);
            }

            private BLL.TreeLogic TreeLogic;
            private CallTreeFrm Caller;
            public delegate void TreeNodeSelectedEventHandler(TreeNodeSelectedEventArg e);
            public event TreeNodeSelectedEventHandler TreeNodeSelectedSubmit;
            public void LoadTreeNode(CallTreeFrm sender, LoadTreeEventArg e)
            {
                Caller = sender;
                TreeLogic = e.TreeLogic;
                TreeNodeSelectedSubmit = Caller.TreeNodeSelected;
                TreeLogic.GetTreeNodes(Tree.Nodes,null);
            }

        }
    }
    #endregion
}
