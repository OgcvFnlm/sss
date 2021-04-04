using System;
using System.Windows.Forms;
using System.Drawing;

namespace UI.FrameWork
{
    #region 树形图查询、选择窗体的接口和基础类
    namespace TreeFrom {
        public class LoadTreeEventArgs
        {
            public BLL.TreeLogic TreeLogic;
            public ContextMenuStrip RightMenu;
            public EventArgs e;
            public object sender;
            public LoadTreeEventArgs(BLL.TreeLogic TreeLogic, ContextMenuStrip RightMenu, EventArgs e, object sender)
            {
                this.TreeLogic = TreeLogic;
                this.RightMenu = RightMenu;
                this.e = e;
                this.sender = sender;
            }
        }
        public class TreeConfirmEventArgs
        {
            public TreeNode[] SelectedNodes;
            public EventArgs e;
            public object sender;
            public TreeConfirmEventArgs(TreeNode[] SelectedNodes, EventArgs e, object sender)
            {
                this.SelectedNodes = SelectedNodes;
                this.e = e;
                this.sender = sender;
            }
        }
        public abstract class TreeForm:Form
        {
            public delegate void TreeConfirmEventHandler(TreeConfirmEventArgs e);
            public event TreeConfirmEventHandler TreeConfirmSubmit;

            private ImageList Imgs = new ImageList();
            public TreeView Tree = new TreeView();
            private ContextMenuStrip RightMenu;
            public BLL.TreeLogic TreeLogic;
            private CallTreeForm Caller;

            public TreeForm()
            {
                this.SuspendLayout();

                #region 初始化ImageList
                Imgs.Images.Add("NodeRoot", Properties.Resources.NodeRoot);
                Imgs.Images.Add("NodeRootS", Properties.Resources.NodeRootS);
                Imgs.Images.Add("NodeNoChild", Properties.Resources.NodeNoChild);
                Imgs.Images.Add("NodeNoChildS", Properties.Resources.NodeNoChildS);
                Imgs.Images.Add("NodeHasChild", Properties.Resources.NodeHasChild);
                Imgs.Images.Add("NodeHasChildS", Properties.Resources.NodeHasChildS);
                Imgs.Images.Add("AddRootNode", Properties.Resources.AddRootNode);
                Imgs.Images.Add("AddNodeChild", Properties.Resources.AddNodeChild);
                Imgs.Images.Add("DeleteNode", Properties.Resources.DeleteNode);
                Imgs.Images.Add("RenameNode", Properties.Resources.RenameNode);
                Imgs.Images.Add("BindDims", Properties.Resources.BindDims);
                Imgs.Images.Add("DeleteBind", Properties.Resources.DeleteBind);
                Imgs.Images.Add("ShowNodeInfo", Properties.Resources.ShowNodeInfo);
                Imgs.Images.Add("BindAccs", Properties.Resources.BindAccs);
                #endregion

                Tree.ImageList = Imgs;
                Tree.NodeMouseClick += NodeClick;

                this.MinimizeBox = false;
                this.MaximizeBox = false;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.Controls.Add(Tree);
                InitSize();
                this.ResumeLayout(false);
            }

            public abstract void InitSize();

            private void NodeClick(object sender, TreeNodeMouseClickEventArgs e)
            {
                if (e.Button == MouseButtons.Right && e.Node != null)
                {
                    Tree.SelectedNode = e.Node;
                    RightMenu.Show(MousePosition);
                }

            }
            private void BindRightMenu(ContextMenuStrip Menu)
            {
                RightMenu = Menu;
                RightMenu.ImageList = Imgs;
                foreach (ToolStripMenuItem item in RightMenu.Items)
                {
                    switch (item.Text)
                    {
                        case "新建辅助项目":
                        case "新增一级科目":
                            item.Image = Imgs.Images["AddRootNode"];
                            item.Click += TreeLogic.AddRootNode;
                            break;
                        case "新增子级项目":
                        case "新增子级科目":
                            item.Image = Imgs.Images["AddNodeChild"];
                            item.Click += TreeLogic.AddNodeChild;
                            break;
                        case "删除已选项目":
                        case "删除已选科目":
                            item.Image = Imgs.Images["DeleteNode"];
                            item.Click += TreeLogic.DeleteNode;
                            break;
                        case "修改项目名称":
                        case "修改科目名称":
                            item.Image = Imgs.Images["RenameNode"];
                            item.Click += TreeLogic.RenameNode;
                            break;
                        case "查看项目介绍":
                        case "查看科目介绍":
                            item.Image = Imgs.Images["ShowNodeInfo"];
                            item.Click += TreeLogic.ShowNodeInfo;
                            break;
                        case "绑定辅助核算项":
                            item.Image = Imgs.Images["BindDims"];
                            item.Click += TreeLogic.BindDims;
                            break;
                        case "解除辅助项绑定":
                            item.Image = Imgs.Images["DeleteBind"];
                            item.Click += TreeLogic.DeleteBind;
                            break;
                        case "查看已绑科目":
                            item.Image = Imgs.Images["BindAccs"];
                            item.Click += TreeLogic.BindAccs;
                            break;
                    }
                }
            }

            public void OnTreeConfirmSubmit(TreeConfirmEventArgs e)
            {
                if (TreeConfirmSubmit != null)
                {
                    TreeConfirmSubmit(e);
                }
            }
            public void LoadTree(CallTreeForm sender, LoadTreeEventArgs e)
            {
                Caller = sender;
                TreeLogic = e.TreeLogic;
                TreeConfirmSubmit = Caller.TreeConfirm;
                TreeLogic.GetTreeNodes(Tree.Nodes, null);
                BindRightMenu(e.RightMenu);
            }
        }
        public abstract class CallTreeForm:Form
        {
            public delegate void LoadTreeEventHandler(CallTreeForm sender, LoadTreeEventArgs e);
            public event LoadTreeEventHandler LoadTreeSubmit;

            public abstract void TreeConfirm(TreeConfirmEventArgs e);

            public void OnLoadTreeSubmit(CallTreeForm sender, LoadTreeEventArgs e)
            {
                if (LoadTreeSubmit != null)
                {
                    LoadTreeSubmit(sender, e);
                }
            }
        }
        public class TreeOnlyQuery : TreeForm { 
            public TreeOnlyQuery(string Caption)
            {
                this.Text = Caption;
            }
            public override void InitSize()
            {
                base.ClientSize = new Size(230, 320);
                base.Tree.Location = new Point(0, 0);
                base.Tree.Size = new Size(230, 320);
            }
        }
        public class TreeOnlyConfirm : TreeForm
        {
            private Button ConfirmButton;
            public TreeOnlyConfirm(string Caption)
            {
                this.Text = Caption;
                ConfirmButton = new Button { Size = new Size(230, 30) ,
                                             Location = new Point(0, 290),
                                             Text = "点击确认选择",
                                             Font = new Font("宋体",11)};
                ConfirmButton.MouseClick += ConfirmSelected;
                this.Controls.Add(ConfirmButton);
            }
            public override void InitSize()
            {
                base.ClientSize = new Size(230, 320);
                base.Tree.Location = new Point(0, 0);
                base.Tree.Size = new Size(230, 290);
            }
            private void ConfirmSelected(object sender, EventArgs e)
            {
                string ErrStr = TreeLogic.CheckSelectedNode(Tree.SelectedNode);
                if (ErrStr == null)
                {
                    base.OnTreeConfirmSubmit(new TreeConfirmEventArgs(new TreeNode[1]{ base.Tree.SelectedNode },e,sender));

                }
                else
                {
                    MessageBox.Show(this,ErrStr,"错误");
                }
            }
        }
        public class TreeTableQuery : TreeForm
        {
            private DataGridView Table;
            public TreeTableQuery(string Caption)
            {
                this.Text = Caption;
                Table = new DataGridView();
                //{
                //    Size = new Size(230, 30),
                //    Location = new Point(0, 290),
                //    Text = "点击确认选择",
                //    Font = new Font("宋体", 11)
                //};
                ConfirmButton.MouseClick += ConfirmSelected;
                this.Controls.Add(ConfirmButton);
            }
            public override void InitSize()
            {
                base.ClientSize = new Size(230, 320);
                base.Tree.Location = new Point(0, 0);
                base.Tree.Size = new Size(230, 320);
            }
        }
    }
    #endregion
}
