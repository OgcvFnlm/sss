using System;
using System.Windows.Forms;
using System.Collections.Generic;
namespace BLL
{
    public abstract class TreeLogic
    {
        private Model.TreeData TreeData;
        private DAL.MyOdbc MyDal = new DAL.MyOdbc();
        public TreeLogic(Model.TreeData TreeData)
        {
            this.TreeData = TreeData;
            MyDal.QueryDatabase(TreeData.ds, TreeData.sql);
        }
        public string CheckSelectedNode(TreeNode Node)
        {
            return Node == null ? "请先选择节点！" : Node.Parent != null ? "请选择根节点！" : null;
        }
        public void GetTreeNodes(TreeNodeCollection Nodes,string parent)
        {
            TreeNode Node;
            IEnumerable<Model.TreeData.NodeInfo> NodeTable = TreeData.NodeTable(parent ?? TreeData.Root());
            foreach (Model.TreeData.NodeInfo item in NodeTable)
            {
                Node = Nodes.Add(item.code, item.title, item.parent == TreeData.Root() ? 0 : item.children == 0 ? 2 : 4, item.parent == TreeData.Root() ? 1 : item.children == 0 ? 3 : 5);
                GetTreeNodes(Node.Nodes, item.code);
            }
        }
        public void AddRootNode(object sender, EventArgs e) { Console.WriteLine("AddRootNode"); }
        public void AddNodeChild(object sender, EventArgs e) { Console.WriteLine("AddNodeChild"); }
        public void DeleteNode(object sender, EventArgs e) { Console.WriteLine("DeleteNode"); }
        public void RenameNode(object sender, EventArgs e) { Console.WriteLine("RenameNode"); }
        public void BindDims(object sender, EventArgs e) { Console.WriteLine("BindDims"); }
        public void DeleteBind(object sender, EventArgs e) { Console.WriteLine("DeleteBind"); }
        public void ShowBindAcc(object sender, EventArgs e) { Console.WriteLine("ShowBindAcc"); }
        public void ShowNodeInfo(object sender, EventArgs e) { Console.WriteLine("ShowNodeInfo"); }
        public void BindAccs(object sender, EventArgs e) { Console.WriteLine("BindAccs"); }

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
    }
}
