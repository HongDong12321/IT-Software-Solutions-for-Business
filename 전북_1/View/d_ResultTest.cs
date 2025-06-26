using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 전북_1.ViewControl;

namespace 전북_1.View
{
    public partial class d_ResultTest : z_BF
    {
        public List<MBTICheckControl> mccs { get; set; }
        int[] check = new int[4];
        List<TrackBar> tb = new List<TrackBar>();
        List<Button> bs = new List<Button>();
        int[] n = new int[] { 100, 85, 36, 16 }; //테스트

        public d_ResultTest()
        {
            InitializeComponent();
            before = 2;
            this.Size = new Size(623, 499);
        }

        void setButton(Form f)
        {
            foreach (var item in f.Controls)
            {
                if (item is Button b)
                {
                    if (!bs.Contains(b))
                    {
                        bs.Add(b);
                    }
                }
            }
            bs.Reverse();
        }

        void setBar(Form f)
        {
            foreach (var item in f.Controls)
            {
                if (item is TrackBar t)
                {
                    if (!tb.Contains(t))
                    {
                        tb.Add(t);
                    }
                }
            }
            tb.Reverse();

            for (int i = 0; i < check.Length; i++)
            {
                check[i] = check[i] * 100 / 25;
            }

            for (int i = 0; i < 4; i++)
            {
                ToolTip t = new ToolTip();
                tb[i].Value = n[i];
                t.SetToolTip(tb[i], tb[i].Value + "%");
            }

        }

        private void d_ResultTest_Activated(object sender, EventArgs e)
        {
            setBar(this);
            setButton(this);
        }


        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {

                tb[i].Value = n[i];

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (this.Size == new Size(623, 871))
            {
                setMovePanel(false);
            }
            else
            {
                setMovePanel(true);
            }

            foreach (var i in bs)
            {
                i.BackColor = Color.White;
                i.ForeColor = Color.Purple;
                if (b.Name == i.Name)
                {
                    i.BackColor = Color.Purple;
                    i.ForeColor = Color.White;
                }
            }
        }

        bool setMovePanelFlag = false;

        private async void setMovePanel(bool bb)
        {
            if (setMovePanelFlag) return;
            setMovePanelFlag = true;
            if (bb)
            {
                while (this.Height != 871)
                {
                    this.Height += 1;
                    await Task.Delay(1);
                }
            }
            else
            {
                while (this.Height != 499)
                {
                    this.Height -= 1;
                    await Task.Delay(1);
                }
            }
            setMovePanelFlag = false;
        }
    }
}
