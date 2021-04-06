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
        public virtual void AddRootNode(object sender, EventArgs e) { Console.WriteLine("AddRootNode"); }
        public virtual void AddNodeChild(object sender, EventArgs e) { Console.WriteLine("AddNodeChild"); }
        public virtual void DeleteNode(object sender, EventArgs e) { Console.WriteLine("DeleteNode"); }
        public virtual void RenameNode(object sender, EventArgs e) { Console.WriteLine("RenameNode"); }
        public virtual void BindDims(object sender, EventArgs e) { Console.WriteLine("BindDims"); }
        public virtual void DeleteBind(object sender, EventArgs e) { Console.WriteLine("DeleteBind"); }
        public virtual void ShowBindAcc(object sender, EventArgs e) { Console.WriteLine("ShowBindAcc"); }
        public virtual void ShowNodeInfo(object sender, EventArgs e) { Console.WriteLine("ShowNodeInfo"); }
        public virtual void BindAccs(object sender, EventArgs e) { Console.WriteLine("BindAccs"); }

    }
    public class CashTreeLogic : TreeLogic
    {
        public CashTreeLogic() : base( new Model.CashTreeData()) { }
    }
    public class DimTreeLogic : TreeLogic
    {
        public DimTreeLogic() : base( new Model.DimTreeData()) { }
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
    }
    public class AddressBookLogic : TreeLogic
    {
        public AddressBookLogic() : base(new Model.AddressBookData()) { }
        public override DataTable GetTableSource(TreeNode Node)
        {
            return TreeData.TableData(Node.Name,null);
        }
    }
}
