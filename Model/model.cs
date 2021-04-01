using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public struct TreeData
    {
        private List<string[]> DataArr;
        public string[] data
        {
            set
            {
                if (this.DataArr == null)
                {
                    this.DataArr = new List<string[]>();
                }
                this.DataArr.Add(value);
            }
        }
        public int Count
        {
            get { return this.DataArr == null ? 0 : this.DataArr.Count; }
        } 
        public string[] this[int i]
        {
            get
            {
                if (i>=0 && i<= this.DataArr.Count - 1)
                {
                    return this.DataArr[i];
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
