using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
namespace BLL
{
    public abstract class TreeLogic
    {
        public Model.TreeData TreeData;
        public TreeLogic(Model.TreeData TreeData)
        {
            this.TreeData = TreeData;
        }
        public virtual DataTable GetTableSource(TreeNode Node)
        {
            return TreeData.TableData("code","^"+ Node.Name + @"\S{2}$");
        }
        public virtual ContextMenuStrip GetRightMenu(ImageList Imgs)
        {
            return new ContextMenuStrip();
        }
        public virtual void GetTreeNodes(TreeNodeCollection Nodes,string parent)
        {
            TreeNode Node;
            IEnumerable<Model.TreeData.NodeInfo> NodeList = TreeData.NodeList(parent ?? TreeData.Root());
            foreach (Model.TreeData.NodeInfo item in NodeList)
            {
                Node = Nodes.Add(item.code, item.title, item.parent == TreeData.Root() ? 0 : item.children == 0 ? 2 : 4, item.parent == TreeData.Root() ? 1 : item.children == 0 ? 3 : 5);
                GetTreeNodes(Node.Nodes, item.code);
            }
        }
        public virtual void GetCheckedNodes(TreeNodeCollection Nodes, List<TreeNode> CheckedNodes)
        {
            foreach (TreeNode item in Nodes)
            {
                if (item.Checked)
                {
                    CheckedNodes.Add(item);
                }
                GetCheckedNodes(item.Nodes, CheckedNodes);
            }
        }
        public object GetSelectedItem(DataGridView Table,TreeView Tree)
        {
            if (Table != null)
            {
                return Table.CurrentRow;
            }
            else if (Tree.CheckBoxes)
            {
                List<TreeNode> CheckedNodes = new List<TreeNode>();
                GetCheckedNodes(Tree.Nodes, CheckedNodes);
                return CheckedNodes.Count == 0 ? null : CheckedNodes;
            }
            else
            {
                return Tree.SelectedNode ?? null;
            }
        }
        public abstract void ShowNodeInfo(object sender, EventArgs e);
    }
    public class CashTreeLogic : TreeLogic
    {
        public CashTreeLogic() : base( new Model.CashTreeData()) { }
        public override ContextMenuStrip GetRightMenu(ImageList Imgs)
        {
            ContextMenuStrip Menu = new ContextMenuStrip();
            Menu.Items.Add("新增子级项目", Imgs.Images["AddNodeChild"], AddNodeChild);
            Menu.Items.Add(new ToolStripSeparator());
            Menu.Items.Add("删除已选项目", Imgs.Images["DeleteNode"], DeleteNode);
            Menu.Items.Add("修改项目名称", Imgs.Images["RenameNode"], RenameNode);
            Menu.Items.Add(new ToolStripSeparator());
            Menu.Items.Add("查看项目介绍", Imgs.Images["ShowNodeInfo"], ShowNodeInfo);
            return Menu;
        }
        public void AddNodeChild(object sender, EventArgs e) { Console.WriteLine("AddNodeChild"); }
        public void DeleteNode(object sender, EventArgs e) { Console.WriteLine("DeleteNode"); }
        public void RenameNode(object sender, EventArgs e) { Console.WriteLine("RenameNode"); }
        public override void ShowNodeInfo(object sender, EventArgs e)
        {

        }
    }
    public class DimTreeLogic : TreeLogic
    {
        public DimTreeLogic() : base( new Model.DimTreeData()) { }
        public override ContextMenuStrip GetRightMenu(ImageList Imgs)
        {
            ContextMenuStrip Menu = new ContextMenuStrip();
            Menu.Items.Add("新建辅助项目", Imgs.Images["AddRootNode"], AddRootNode);
            Menu.Items.Add("新增子级项目", Imgs.Images["AddNodeChild"], AddNodeChild);
            Menu.Items.Add(new ToolStripSeparator()); 
            Menu.Items.Add("删除已选项目", Imgs.Images["DeleteNode"], DeleteNode);
            Menu.Items.Add("修改项目名称", Imgs.Images["RenameNode"], RenameNode);
            Menu.Items.Add(new ToolStripSeparator()); 
            Menu.Items.Add("查看已绑科目", Imgs.Images["ShowBindAcc"], ShowBindAcc);
            Menu.Items.Add("查看项目介绍", Imgs.Images["ShowNodeInfo"], ShowNodeInfo);
            return Menu;
        }
        public void AddRootNode(object sender, EventArgs e) { Console.WriteLine("AddRootNode"); }
        public void AddNodeChild(object sender, EventArgs e) { Console.WriteLine("AddNodeChild"); }
        public void DeleteNode(object sender, EventArgs e) { Console.WriteLine("DeleteNode"); }
        public void RenameNode(object sender, EventArgs e) { Console.WriteLine("RenameNode"); }
        public void ShowBindAcc(object sender, EventArgs e) { Console.WriteLine("ShowBindAcc"); }
        public override void ShowNodeInfo(object sender, EventArgs e) { Console.WriteLine("ShowNodeInfo"); }
    }
    public class AccTreeLogic : TreeLogic
    {
        public AccTreeLogic() : base( new Model.AccTreeData()) { }
        public override DataTable GetTableSource(TreeNode Node)
        {
            if (Node.Parent == null)
            {
                return TreeData.TableData("parent", "^" + Node.Name + "$");
            }
            else
            {
                return TreeData.TableData("code", "^" + Node.Name + @"\S{2}$");
            }
        }
        public override ContextMenuStrip GetRightMenu(ImageList Imgs)
        {
            ContextMenuStrip Menu = new ContextMenuStrip();
            Menu.Items.Add("新增一级科目", Imgs.Images["AddRootNode"], AddRootNode);
            Menu.Items.Add("新增子级科目", Imgs.Images["AddNodeChild"], AddNodeChild);
            Menu.Items.Add(new ToolStripSeparator());
            Menu.Items.Add("删除已选科目", Imgs.Images["DeleteNode"], DeleteNode);
            Menu.Items.Add("修改科目名称", Imgs.Images["RenameNode"], RenameNode);
            Menu.Items.Add(new ToolStripSeparator());
            Menu.Items.Add("绑定辅助核算项", Imgs.Images["BindDims"], BindDims);
            Menu.Items.Add("解除辅助项绑定", Imgs.Images["DeleteBind"], DeleteBind);
            Menu.Items.Add(new ToolStripSeparator());
            Menu.Items.Add("查看项目介绍", Imgs.Images["ShowNodeInfo"], ShowNodeInfo);
            return Menu;
        }
        public void AddRootNode(object sender, EventArgs e) { Console.WriteLine("AddRootNode"); }
        public void AddNodeChild(object sender, EventArgs e) { Console.WriteLine("AddNodeChild"); }
        public void DeleteNode(object sender, EventArgs e) { Console.WriteLine("DeleteNode"); }
        public void RenameNode(object sender, EventArgs e) { Console.WriteLine("RenameNode"); }
        public void BindDims(object sender, EventArgs e) { Console.WriteLine("BindDims"); }
        public void DeleteBind(object sender, EventArgs e) { Console.WriteLine("DeleteBind"); }
        public override void ShowNodeInfo(object sender, EventArgs e) { Console.WriteLine("ShowNodeInfo"); }
    }
    public class AddressBookLogic : TreeLogic
    {
        public AddressBookLogic() : base(new Model.AddressBookData()) { }
        public override DataTable GetTableSource(TreeNode Node)
        {
            return TreeData.TableData(Node.Name,null);
        }
        public override void ShowNodeInfo(object sender, EventArgs e) { Console.WriteLine("ShowNodeInfo"); }
    }
}
