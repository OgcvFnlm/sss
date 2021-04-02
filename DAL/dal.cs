using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Data;

namespace DAL
{
    public class MyOdbc
    {
        private string OdbcConnStr = Properties.Resources.ConnStr;
        private string sql;
        private OdbcConnection conn;
        private OdbcDataAdapter da;
        private void QueryDatabase(DataSet ds)
        {
            //OdbcCommand comm;
            this.conn = new OdbcConnection(this.OdbcConnStr);
            this.conn.Open();
            //comm = new OdbcCommand(this.sql, this.conn);
            //this.dr = comm.ExecuteReader();
            this.da = new OdbcDataAdapter(this.sql, this.conn);
            this.da.Fill(ds);
        }
        public MyOdbc(string sql,DataSet ds)
        {
            this.sql = sql;
            this.QueryDatabase(ds);
        }
        public MyOdbc(string OdbcConnStr, string sql,DataSet ds)
        {
            this.OdbcConnStr = OdbcConnStr;
            this.sql = sql;
            this.QueryDatabase(ds);
        }
        //public string[] this[int j]
        //{
        //    get
        //    {
        //        if (this.dr.Read())
        //        {
        //            string[] arr = new string[this.dr.FieldCount];
        //            for (int i =0;i< this.dr.FieldCount - 1; i++)
        //            {
        //                arr[i] = dr[i].ToString();
        //            }
        //            return arr;
        //        }
        //        else
        //        {
        //            this.conn.Close();
        //            return null;
        //        }
        //    }
        //}
        //public void Fill(DataSet ds)
        //{
        //    this.da.Fill(ds);
        //}
    }
}
