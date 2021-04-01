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
        private string _Parent;
        public string Parent
        {
            get { return this._Parent; }
            set { this._Parent = value; }
        }
        public TreeData( string Parent)
        {
            this.DataArr = new List<string[]>();
            this._Parent = Parent;
        }
        public int Count
        {
            get { return this.DataArr == this.DataArr.Count; }
        } 
        public void Add(string code,string name,string tag)
        {
            this.DataArr.Add(new string[3] { code, name, tag });
        }
        public string[] this[int i]
        {
            get
            {
                if (i>=0 && i<= this.Count - 1)
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
