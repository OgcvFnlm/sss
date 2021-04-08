using System;
using System.Windows.Forms;
using System.Drawing;
namespace UI.FrameWork
{
    #region 树形图查询、选择窗体的接口和基础类
    public class LoadTreeEventArgs
    {
        public BLL.TreeLogic TreeLogic;
        public CallTreeForm CallTreeForm;
        public LoadTreeEventArgs(BLL.TreeLogic TreeLogic, CallTreeForm CallTreeForm)
        {
            this.TreeLogic = TreeLogic;
            this.CallTreeForm = CallTreeForm;
        }
    }
    public class TreeConfirmEventArgs
    {
        public object data;
        public TreeConfirmEventArgs(object data)
        {
            this.data = data;
        }
    }
    public abstract class TreeForm : Form
    {
        public delegate void TreeConfirmEventHandler(object sender, TreeConfirmEventArgs e);
        public event TreeConfirmEventHandler TreeConfirmSubmit;
        public void OnTreeConfirmSubmit(object sender, EventArgs e)
        {
            object SelectedItem = TreeLogic.GetSelectedItem(Table, Tree);
            TreeConfirmEventArgs E = new TreeConfirmEventArgs(SelectedItem);
            TreeConfirmSubmit(Caller, E);
        }
        public CallTreeForm CallTreeForm;
        public object Caller;
        public TreeView Tree;
        public BLL.TreeLogic TreeLogic;
        public Boolean TreeCheckBox;
        public ContextMenuStrip RightMenu;
        public void LoadTree(object sender, LoadTreeEventArgs e)
        {
            CallTreeForm = e.CallTreeForm;
            Caller = sender;
            Tree.CheckBoxes = TreeCheckBox;
            TreeLogic = e.TreeLogic;
            TreeLogic.GetTreeNodes(Tree.Nodes, null);
            TreeConfirmSubmit = CallTreeForm.TreeConfirm;
            RightMenu = LoadRightMenu();
        }
        public virtual ContextMenuStrip LoadRightMenu()
        {
            ContextMenuStrip Menu = new ContextMenuStrip();
            Menu.Items.Add("查看节点介绍", Imgs.Images["ShowNodeInfo"], TreeLogic.ShowNodeInfo);
            return Menu;
        }

        public ImageList Imgs;
        public DataGridView Table;
        public Button ConfirmButton;
        public TreeForm(string Caption, Boolean TreeCheckBox)
        {
            this.Text = Caption;
            this.SuspendLayout();
            #region 初始化ImageList
            Imgs = new ImageList();
            Imgs.Images.Add("NodeRoot", Properties.Resources.NodeRoot);
            Imgs.Images.Add("NodeRootS", Properties.Resources.NodeRootS);
            Imgs.Images.Add("NodeNoChild", Properties.Resources.NodeNoChild);
            Imgs.Images.Add("NodeNoChildS", Properties.Resources.NodeNoChildS);
            Imgs.Images.Add("NodeHasChild", Properties.Resources.NodeHasChild);
            Imgs.Images.Add("NodeHasChildS", Properties.Resources.NodeHasChildS);
            Imgs.Images.Add("AddRootNode", Properties.Resources.AddRootNode);
            Imgs.Images.Add("AddNodeChild", Properties.Resources.AddNodeChild);
            Imgs.Images.Add("DeleteNode", Properties.Resources.DeleteNode);
            Imgs.Images.Add("RenameNode", Properties.Resources.RenameNode);
            Imgs.Images.Add("BindDims", Properties.Resources.BindDims);
            Imgs.Images.Add("DeleteBind", Properties.Resources.DeleteBind);
            Imgs.Images.Add("ShowNodeInfo", Properties.Resources.ShowNodeInfo);
            Imgs.Images.Add("ShowBindAcc", Properties.Resources.ShowBindAcc);
            #endregion
            #region 初始化树形图
            Tree = new TreeView();
            Tree.ImageList = Imgs;
            this.TreeCheckBox = TreeCheckBox;
            Tree.Location = new Point(0, 0);
            Tree.Size = new Size(230, 320);
            Tree.BorderStyle = BorderStyle.Fixed3D;
            Tree.NodeMouseClick += OnNodeClick;
            #endregion
            #region 初始化确认按钮
            ConfirmButton = new Button
            {
                Size = new Size(100, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Orange,
                Text = "点击确认选择",
                ForeColor = Color.White
            };
            ConfirmButton.FlatAppearance.BorderSize = 0;
            ConfirmButton.MouseClick += OnTreeConfirmSubmit;
            #endregion
            ClientSize = new Size(230, 320);
            this.BackColor = Color.White;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Controls.Add(Tree);
            this.ResumeLayout(false);
        }
        public virtual void OnNodeClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.Node != null)
            {
                Tree.SelectedNode = e.Node;
                RightMenu.Show(MousePosition);
            }

        }
    }
    public abstract class CallTreeForm : Form
    {
        public delegate void LoadTreeEventHandler(object sender, LoadTreeEventArgs e);
        public event LoadTreeEventHandler LoadTreeSubmit;
        public void OnLoadTreeSubmit(object sender, LoadTreeEventArgs e)
        {
            LoadTreeSubmit = TreeForm.LoadTree;
            LoadTreeSubmit(sender, e);
            TreeForm.ShowDialog();
        }
        public abstract void TreeConfirm(object sender, TreeConfirmEventArgs e);

        public TreeForm TreeForm;
    }
    public class TreeOnlyQuery : TreeForm {
        public TreeOnlyQuery(string Caption, Boolean TreeCheckBox = false) : base(Caption, TreeCheckBox) { }
    }
    public class TreeOnlyConfirm : TreeOnlyQuery
    {
        public TreeOnlyConfirm(string Caption, Boolean TreeCheckBox) : base(Caption, TreeCheckBox)
        {
            Tree.Size = new Size(230, 270);
            ConfirmButton.Size = new Size(100, 30);
            ConfirmButton.Location = new Point(65, 280);
            this.Controls.Add(ConfirmButton);
        }
    }
    public class TreeTableQuery : TreeOnlyQuery
    {
        public TreeTableQuery(string Caption) : base(Caption, false)
        {
            ClientSize = new Size(400, 320);
            Tree.Size = new Size(150, 320);
            Table = new DataGridView
            {
                Size = new Size(250, 320),
                Location = new Point(150, 0),
                BackgroundColor = Color.White,
                ReadOnly = true,
                BorderStyle = BorderStyle.Fixed3D,
                AllowUserToAddRows = false
            };
            this.Controls.Add(Table);
        }
        public override void OnNodeClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Node != null)
            {
                Tree.SelectedNode = e.Node;
                Table.DataSource = TreeLogic.GetTableSource(e.Node);
                Table.AutoResizeColumns();
            } else if (e.Button == MouseButtons.Right && e.Node != null)
            {
                Tree.SelectedNode = e.Node;
                RightMenu.Show(MousePosition);
            }
        }
        public override ContextMenuStrip LoadRightMenu()
        {
            return TreeLogic.GetRightMenu(Imgs);
        }
    }
    public class TreeTableConfirm : TreeTableQuery
    {
        public TreeTableConfirm(string Caption) : base(Caption)
        {

            Table.Size = new Size(250, 270);
            ConfirmButton.Location = new Point(225, 280);
            this.Controls.Add(ConfirmButton);
        }
    }
    #endregion
    #region 登录窗体
    public abstract class LoginSelectForm : Form
    {
        public PictureBox Picture = new PictureBox
        {
            Image = Properties.Resources.LoginPicture,
            Location = new Point(5, 5),
            Size = new Size(140, 230),
            BorderStyle = BorderStyle.Fixed3D
        };
        public Label TitleLabel = new Label
        {
            Location = new Point(150, 25),
            Size = new Size(210, 24)
        };
        public Panel Panel = new Panel
        {
            Location = new Point(150, 52),
            Size = new Size(210, 128),
            BorderStyle =BorderStyle.Fixed3D,
            BackColor = Color.WhiteSmoke
        };
        public Label Label1 = new Label
        {
            Location = new Point(10, 30),
            Size = new Size(45, 12),
        };
        public Label Label2 = new Label
        {
            Location = new Point(10, 63),
            Size = new Size(45, 12),
        };
        public Button ConfirmButton = new Button
        {
            Location = new Point(200,195),
            Size = new Size(100, 30),
            Text = "确认提交",
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.Orange,
            ForeColor = Color.White
        };
        public LoginSelectForm()
        {
            SuspendLayout();
            ClientSize = new Size(365,240);
            MinimizeBox = false;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = Color.LightGray;
            Controls.Add(Picture);
            Controls.Add(TitleLabel);
            Controls.Add(Panel);
            Panel.Controls.Add(Label1);
            Panel.Controls.Add(Label2);
            Controls.Add(ConfirmButton);
            ConfirmButton.MouseClick += ConfirmButtonClick;
            ResumeLayout(false);
        }
        public abstract void ConfirmButtonClick(object sender, EventArgs e);
    }
    public class FirstLogin : LoginSelectForm
    {
        private TextBox UserID = new TextBox
        {
            Location = new Point(55, 24),
            Size = new Size(140, 20),
        };
        private TextBox UserPW = new TextBox
        {
            Location = new Point(55, 58),
            Size = new Size(140, 20),
            PasswordChar = char.Parse("*")
        };
        private CheckBox ShowPW = new CheckBox
        {
            Location = new Point(55, 90),
            Size = new Size(140, 20),
            Text = "显示密码",
            Checked = false,
    };
        public FirstLogin() 
        {
            Text = "登录验证";
            TitleLabel.Text = "欢迎使用本系统，请输入账号与密码：";
            Label1.Text = "账号：";
            Panel.Controls.Add(UserID);
            Label2.Text = "密码：";
            Panel.Controls.Add(UserPW);
            Panel.Controls.Add(ShowPW);
            ShowPW.CheckedChanged += ChangeShowPW;
        }
        private void ChangeShowPW(object sender, EventArgs e)
        {
            if (ShowPW.Checked)
            {
                UserPW.PasswordChar = new char(); 
            }
            else
            {
                UserPW.PasswordChar = char.Parse("*");

            }
        }
        public override void ConfirmButtonClick(object sender, EventArgs e)
        {
            new SelectAccBook().ShowDialog();
        }
    }
    public class SelectAccBook : LoginSelectForm
    {
        private ComboBox Company = new ComboBox
        {
            Location = new Point(55, 24),
            Size = new Size(140, 20),
        };
        private ComboBox AccBook = new ComboBox
        {
            Location = new Point(55, 68),
            Size = new Size(140, 20),
        };
        public SelectAccBook()
        {
            Text = "选择账簿";
            TitleLabel.Text = "欢迎使用本系统，请选择机构和账簿：";
            Label1.Text = "机构：";
            Panel.Controls.Add(Company);
            Label2.Text = "账簿：";
            Label2.Location = new Point(10, 73);
            Panel.Controls.Add(AccBook);
        }
        public override void ConfirmButtonClick(object sender, EventArgs e)
        {

        }
    }
    #endregion
}
