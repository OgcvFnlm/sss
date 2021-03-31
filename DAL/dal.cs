using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace DAL
{
    public class MyOdbc
    {
        private string OdbcConnStr = "dsn=maranda";
        private string sql;
        private OdbcDataReader dr;
        private void QueryDatabase()
        {
            OdbcConnection conn;
            OdbcCommand comm;
            conn = new OdbcConnection(this.OdbcConnStr);
            conn.Open();
            comm = new OdbcCommand(this.sql, conn);
            this.dr = comm.ExecuteReader();
        }
        public MyOdbc(string sql)
        {
            this.sql = sql;
            this.QueryDatabase();
        }
        public MyOdbc(string OdbcConnStr, string sql)
        {
            this.OdbcConnStr = OdbcConnStr;
            this.sql = sql;
            this.QueryDatabase();
        }
        public string[] this[int j]
        {
            get
            {
                if (this.dr.Read())
                {
                    string[] arr = new string[this.dr.FieldCount];
                    for (int i =0;i< this.dr.FieldCount - 1; i++)
                    {
                        arr[i] = dr[i].ToString();
                    }
                    return arr;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
