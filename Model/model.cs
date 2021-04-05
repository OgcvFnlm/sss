using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Model
{
    public abstract class TreeData
    {
        public string sql;
        public DataSet ds;
        public abstract string Root();
        public TreeData(string TreeSql)
        {
            ds = new DataSet();
            sql = TreeSql;
        }
        public class NodeInfo
        {
            public string code;
            public string title;
            public string parent;
            public string tag;
            public int children;
            public NodeInfo(string code,string title,string parent,string tag,int children)
            {
                this.code = code;
                this.title = title;
                this.parent = parent;
                this.tag = tag;
                this.children = children;
            }
        }
        public virtual IEnumerable<NodeInfo> NodeTable(string parent)
        {
            IEnumerable<NodeInfo> matches = from row
                                          in ds.Tables[0].AsEnumerable()
                                          where row.Field<string>("parent") == parent
                                          orderby row.Field<string>("code")
                                          select new NodeInfo(
                                              row.Field<string>("code"), 
                                              row.Field<string>("title"), 
                                              row.Field<string>("parent"), 
                                              row.Field<string>("tag"), 
                                              (from row1 in ds.Tables[0].AsEnumerable()
                                               where row1.Field<string>("parent") == row.Field<string>("code")
                                               select row1).Count()
                                          );
            return matches;
        }
    }
    public class CashTreeData:TreeData
    {
        private const string CashTreeSql = "select code,title,parent,NULL as tag from dimensions";
        private string _root = "cash";
        public CashTreeData() : base(CashTreeSql) { }
        public override string Root()
        {
            return _root;
        }
        public DataTable TableData(TreeNode Node)
        {
            IEnumerable<DataRow> matches = from row
                                            in ds.Tables[0].AsEnumerable()
                                            where row.Field<string>("parent") == "root"
                                            orderby row.Field<string>("code")
                                            select row;
            return matches;
        }
    }
    public class DimTreeData : TreeData
    {
        private const string DimTreeSql = "select code,title,parent,NULL as tag from dimensions";
        private string _root = "root";
        public DimTreeData() : base(DimTreeSql) { }
        public override string Root()
        {
            return _root;
        }

    }
    public class AccTreeData : TreeData
    {
        private const string AccTreeSql = "select code,title,parent,dim_number as tag from accounts";
        private string _root = "root";
        public AccTreeData() : base(AccTreeSql) { }
        public override string Root()
        {
            return _root;
        }
    }
}
