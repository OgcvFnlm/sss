﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace UI
{
    public partial class login : FrameWork.TreeFrom.CallTreeForm
    {

        public login()
        {
            InitializeComponent();

        }
        public override void TreeConfirm(FrameWork.TreeFrom.TreeConfirmEventArgs e)
        {
            Console.WriteLine("ss");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ContextMenuStrip Menu = new ContextMenuStrip();
            Menu.Items.Add("新增子级项目");
            Menu.Items.Add("删除已选项目");
            Menu.Items.Add("修改项目名称");
            Menu.Items.Add("查看项目介绍");
            OnLoadTreeSubmit(
                this,
                new FrameWork.TreeFrom.TreeOnlyConfirm("现金流项目选择器", true),
                new FrameWork.TreeFrom.LoadTreeEventArgs(new BLL.CashTreeLogic(), Menu, e, sender)
            );
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ContextMenuStrip Menu = new ContextMenuStrip();
            //    Menu.Items.Add("新建辅助项目");
            //    Menu.Items.Add("新增子级项目");
            //    Menu.Items.Add("删除已选项目");
            //    Menu.Items.Add("修改项目名称");
            //    Menu.Items.Add("查看已绑科目");
            //    Menu.Items.Add("查看项目介绍");
            //    FrameWork.TreeFrom.TreeOnly frm = new SelectTree("辅助核算项目选择器");
            //FrameWork.TreeFrom.TreeTableQuery frm = new FrameWork.TreeFrom.TreeTableQuery("辅助核算项目选择器");
            //FrameWork.TreeFrom.TreeTableConfirm frm = new FrameWork.TreeFrom.TreeTableConfirm("辅助核算项目选择器");
            FrameWork.TreeFrom.TreeOnlyConfirm frm = new FrameWork.TreeFrom.TreeOnlyConfirm("辅助核算项目选择器",false);
            LoadTreeSubmit += frm.LoadTree;
            OnLoadTreeSubmit(this, new FrameWork.TreeFrom.LoadTreeEventArgs(new BLL.DimTreeLogic(), Menu, e, sender));
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ContextMenuStrip Menu = new ContextMenuStrip();
            //    Menu.Items.Add("新增一级科目");
            //    Menu.Items.Add("新增子级科目");
            //    Menu.Items.Add("删除已选科目");
            //    Menu.Items.Add("修改科目名称");
            //    Menu.Items.Add("绑定辅助核算项");
            //    Menu.Items.Add("解除辅助项绑定");
            //    Menu.Items.Add("查看科目介绍");
            FrameWork.TreeFrom.TreeTableQuery frm = new FrameWork.TreeFrom.TreeTableQuery("会计科目选择器");
            LoadTreeSubmit += frm.LoadTree;
            OnLoadTreeSubmit(this, new FrameWork.TreeFrom.LoadTreeEventArgs(new BLL.AddressBookLogic(), Menu, e, sender));
            frm.ShowDialog();
        }
    }
}
