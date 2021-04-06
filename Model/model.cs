using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data.Odbc;

namespace Model
{
    public class MyOdbc
    {
        private string OdbcConnStr = Properties.Resources.ConnStr;
        private OdbcConnection conn;
        public OdbcDataAdapter dap;
        public void QueryDatabase(DataSet dst, string sql)
        {
            conn = new OdbcConnection(OdbcConnStr);
            conn.Open();
            dap = new OdbcDataAdapter(sql, conn);
            dap.Fill(dst);
            conn.Close();
        }
    }
    public abstract class TreeData
    {
        public DataSet ds = new DataSet();
        public TreeData()
        {
            new MyOdbc().QueryDatabase(ds, Sql());
        }
        public abstract string Root();
        public abstract string Sql();
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
        public virtual IEnumerable<NodeInfo> NodeList(string parent)
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
        public virtual DataTable TableData(string FieldName,string RegStr)
        {
            var query = from row
                        in ds.Tables[0].AsEnumerable()
                        let matchs = new Regex(RegStr).Matches(row.Field<string>(FieldName))
                        where matchs.Count>0
                        orderby row.Field<string>("code")
                        select row;
            DataTable NewTable = query.Count<DataRow>() == 0 ? null : query.CopyToDataTable<DataRow>();
            if (NewTable != null)
            {
                NewTable.Columns.Remove("parent");
                NewTable.Columns.Remove("tag");
                NewTable.Columns["code"].ColumnName = "项目编码";
                NewTable.Columns["title"].ColumnName = "项目名称";
            }
            return NewTable;
        }
    }
    public class DimTreeData : TreeData
    {
        public override string Root()
        {
            return "root";
        }
        public override string Sql()
        {
            return "select code,title,parent,NULL as tag from dimensions";
        }

    }
    public class CashTreeData:DimTreeData
    {
        public override string Root()
        {
            return "cash";
        }
    }
    public class AccTreeData : TreeData
    {
        public override string Root()
        {
            return "root";
        }
        public override string Sql()
        {
            return "select code,title,parent,dim_number as tag from accounts";
        }
    }
    public class AddressBookData : TreeData
    {
        private DataSet HrDs;
        private string HrSql(string code)
        {
            return "SELECT basicinfo.code as 工号, basicinfo.title as 姓名, holdpost.title as 职务, basicinfo.phone as 联系方式, holdpost.pt_main as 职务备注 FROM organize INNER JOIN (basicinfo INNER JOIN holdpost ON basicinfo.code = holdpost.hr_number) ON organize.code = holdpost.organize_number WHERE organize.code ='" + code + "'";
        }
        public override string Root()
        {
            return "root";
        }
        public override string Sql()
        {
            return "select code,title,parent,NULL as tag from organize";
        }
        public override DataTable TableData(string code, string FieldName = null)
        {
            HrDs = new DataSet();
            new MyOdbc().QueryDatabase(HrDs, HrSql(code));
            return HrDs.Tables[0];
        }
    }
}
