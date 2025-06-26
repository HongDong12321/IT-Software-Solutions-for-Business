using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 전북_1.View
{
    public partial class z_BF : Form
    {
        public static List<Form> fs = new List<Form>();
        public static int before = 1;
        public static Model.user user;
        public z_BF()
        {
            InitializeComponent();
            this.BackColor = Color.White;
            this.Font = new Font("맑은고딕", 15f);
            //this.Icon = new Icon("./datafiles/icon/icon.ico");
        }



        public static void Emsg(string s)
        {
            MessageBox.Show(s, "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Imsg(string s)
        {
            MessageBox.Show(s, "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void setDefault(Form f)
        {
            f.BackColor = Color.White;
            f.Font = new Font("맑은고딕", 15f);
            f.Icon = new Icon("datafiles/icon/icon.ico");
        }

        public static void setButton(params Button[] b)
        {
            foreach (var item in b)
            {
                item.ForeColor = Color.White;
                item.BackColor = Color.Purple;
            }
        }

        private void BF_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                for (int i = 0; i < before; i++)
                {
                    fs.Remove(fs[fs.Count() - 1]);
                }
                before = 1;
                fs[fs.Count() - 1].Visible = true;
            }
            catch (Exception)
            {
                Dispose();
            }
        }

        public static void addForm(Form b , Form a)
        {
            b.Visible = false;
            fs.Add(a);
            a.Visible = true;
        }
    }
}
