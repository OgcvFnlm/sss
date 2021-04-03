using System.Data.Odbc;
using System.Data;

namespace DAL
{
    public class MyOdbc
    {
        private string OdbcConnStr = Properties.Resources.ConnStr;
        private OdbcConnection conn;
        public OdbcDataAdapter dap;
        public void QueryDatabase(DataSet dst,string sql)
        {
            conn = new OdbcConnection(OdbcConnStr);
            conn.Open();
            dap = new OdbcDataAdapter(sql, conn);
            dap.Fill(dst);
        }
    }
}
