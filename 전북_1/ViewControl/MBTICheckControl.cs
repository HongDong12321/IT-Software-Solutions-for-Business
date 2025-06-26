using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 전북_1.ViewControl
{
    public partial class MBTICheckControl : UserControl
    {
        public string title { get { return label1.Text; } set { label1.Text = value; } }
        public int type{ get; set; }
        public int result { get; set; }
        List<Label> ls = new List<Label>();

        public MBTICheckControl()
        {
            InitializeComponent();
            this.Font = new Font("맑은 고딕", 9);  // 또는 폼 폰트 영향 제거
            setLabel();
            
        }

        void setLabel()
        {
            foreach (var item in tableLayoutPanel1.Controls)
            {
                if(item is Label l)
                {
                    if (!ls.Contains(l))
                    {
                        ls.Add(l);
                    }
                }
            }
            ls.Reverse();
            label1.Text = title;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Label l = sender as Label;
            foreach (var item in ls)
            {
                if (l.Name == item.Name)
                {
                    if (l.Name == label4.Name)
                    {
                        label2.ForeColor = Color.Green;
                    }else if (l.Name == label8.Name)
                    {
                        label3.ForeColor = Color.Green;
                    }
                    else
                    {
                        label2.ForeColor = Color.Black;
                        label3.ForeColor = Color.Black;
                    }
                    item.ForeColor = Color.Green;
                    item.Text = "●";
                }
                else
                {
                    item.ForeColor = Color.Black;
                    item.Text = "○";
                }
                for (int i = 0; i < ls.Count; i++)
                {
                    if (ls[i].Name == l.Name)
                    {
                        result = (i + 1);
                        Console.WriteLine(result);
                        break;
                    }

                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}
