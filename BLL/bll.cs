using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FillTreeData
    {
        public static void FillData(ref Model.TreeData TreeData,string FrmName,string parent)
        {
            string sql =null;
            switch (FrmName)
            {
                case "accs":
                    sql = "select code,title,parent,dim_number as tag from accounts where parent = '" + (parent == null ? "root" : parent) + "'";
                    break;
                case "dims":
                    sql = "select code,title,parent,NULL as tag from dimensions where parent = '" + (parent == null ? "root" : parent) + "'";
                    break;
                case "cash":
                    sql = "select code,title,parent,NULL as tag from dimensions where parent = '" + (parent == null ? "cash" : parent ) + "'";
                    break;
            }
            if(sql != null)
            {
                int i = 0;
                string[] arr;
                DAL.MyOdbc DataReader = new DAL.MyOdbc(sql);
                arr = DataReader[i];
                while (arr != null)
                {
                    TreeData.data =arr;
                    i++;
                    arr = DataReader[i];

                }
            }
        }
    }
}
