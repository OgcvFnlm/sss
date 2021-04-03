using System.Windows.Forms;
using System.Collections.Generic;
namespace BLL
{
    public abstract class TreeLogic
    {
        private string root;
        private Model.TreeData TreeData;
        private DAL.MyOdbc MyDal = new DAL.MyOdbc();
        public TreeLogic(string root, Model.TreeData TreeData)
        {
            this.root = root;
            this.TreeData = TreeData;
            MyDal.QueryDatabase(TreeData.ds, TreeData.sql);
        }
        public void GetTreeNodes(TreeNodeCollection Nodes,string parent)
        {
            TreeNode Node;
            IEnumerable<Model.TreeData.NodeInfo> NodeTable = TreeData.NodeTable(parent ?? root);
            foreach (Model.TreeData.NodeInfo item in NodeTable)
            {
                Node = Nodes.Add(item.code, item.title, item.parent == root ? 0 : item.children == 0 ? 2 : 4, item.parent == root ? 1 : item.children == 0 ? 3 : 5);
                GetTreeNodes(Node.Nodes, item.code);
            }
        }
    }
    public class CashTreeLogic : TreeLogic
    {
        public CashTreeLogic() : base("cash", new Model.CashTreeData()) { }
    }
    public class DimTreeLogic : TreeLogic
    {
        public DimTreeLogic() : base("root", new Model.DimTreeData()) { }
    }
    public class AccTreeLogic : TreeLogic
    {
        public AccTreeLogic() : base("root", new Model.AccTreeData()) { }
    }
}
