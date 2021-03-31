using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class dim_manage
    {
        public dim_manage(ref Model.TreeData Data)
        {
            string[] arr;
            int i = 0;
            string sql = "select * from dimensions where parent = '" + Data.Parent + "'";
            DAL.MyOdbc DataReader = new DAL.MyOdbc(sql);
            arr = DataReader[i];
            while (arr != null)
            {
                Data.Add(arr[0], arr[1], arr[2]);
                i = i + 1;
                arr = DataReader[i];
            }
            
        }
    }
}
