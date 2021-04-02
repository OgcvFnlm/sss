using System;
using System.Windows.Forms;
using System.Drawing;

namespace UI.FrameWork
{
    interface Itree
    {
        void InitTree();
        void LoadRootNode(object sender, LoadTreeEventArgs e);
        void LoadChildNode(TreeNode Node);
    }
    interface Iimage
    {
        void AddImg(Image[] imgs);
    }
    interface LoadTreeEventArgs
    {
        string Name { set; get; }
        string Caption { set; get; }
    }
    //class TreeOnly : Form, Itree, Iimage
    //{
    //    private TreeView Tree { get; set; }
    //    public TreeOnly()
    //    {
    //        if (this.Tree == null){ this.InitTree(); }
    //    }
    //    public virtual void InitTree()
    //    {
    //        Tree = new TreeView();
        //    Tree.Location = new Point(0, 0);
        //    Tree.Size = new Size(230, 320);
        //}
        //public void LoadRootNode(object sender, LoadTreeEventArgs e)
        //{
        //    this.Text = e.Caption;
        //    this.Name = e.Name;
        //    Model.TreeData TreeData;
        //    this.LoadChildNode();
        //}
        //public void LoadChildNode(TreeNode Node)
        //{

        //}


    //}
}
