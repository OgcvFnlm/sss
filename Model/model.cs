using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;

namespace Model
{
    //public struct TreeData
    //{
    //    private List<string[]> DataArr;
    //    public string[] data
    //    {
    //        set
    //        {
    //            if (this.DataArr == null)
    //            {
    //                this.DataArr = new List<string[]>();
    //            }
    //            this.DataArr.Add(value);
    //        }
    //    }
    //    public int Count
    //    {
    //        get { return this.DataArr == null ? 0 : this.DataArr.Count; }
    //    } 
    //    public string[] this[int i]
    //    {
    //        get
    //        {
    //            if (i>=0 && i<= this.DataArr.Count - 1)
    //            {
    //                return this.DataArr[i];
    //            }
    //            else
    //            {
    //                return null;
    //            }
    //        }
    //    }

    //}
    public class TreeData
    {
        public string sql;
        public DataSet ds;
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
        public CashTreeData() : base(CashTreeSql) { }
    }
    public class DimTreeData : TreeData
    {
        private const string DimTreeSql = "select code,title,parent,NULL as tag from dimensions";
        public DimTreeData() : base(DimTreeSql) { }
    }
    public class AccTreeData : TreeData
    {
        private const string AccTreeSql = "select code,title,parent,dim_number as tag from accounts";
        public AccTreeData() : base(AccTreeSql) { }
    }
}
