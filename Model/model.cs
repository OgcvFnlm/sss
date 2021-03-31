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
        public int Count
        {
            get { return this.DataArr == null ? 0 : this.DataArr.Count; }
        } 
        public void Add(string code,string name,string tag)
        {
            this.DataArr = new List<string[]> ();
            this.DataArr.Add(new string[3] { code, name, tag });
        }
        private string _Parent;
        public string Parent
        {
            get { return this._Parent; }
            set { this._Parent = value; }
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
