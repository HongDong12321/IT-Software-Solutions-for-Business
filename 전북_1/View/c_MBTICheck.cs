using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 전북_1.Model;
using 전북_1.ViewControl;

namespace 전북_1.View
{
    public partial class c_MBTICheck : z_BF
    {
        bool close = true;
        List<MBTICheckControl> mccs = new List<MBTICheckControl>();
        public c_MBTICheck()
        {
            InitializeComponent();
            setQuestion();
        }
        void setQuestion()
        {
            using (care db = new care())
            {
                try
                {
                    foreach (var item in db.question)
                    {
                        MBTICheckControl mcc = new MBTICheckControl()
                        {
                            title = item.qquestion,
                            result = 0,
                            type = item.qdigitsor,
                        };
                        Console.WriteLine(item.qquestion);
                        flowLayoutPanel1.Controls.Add(mcc);
                        mcc.Size = new Size(flowLayoutPanel1.Width - 30, flowLayoutPanel1.Height / 2);
                        mccs.Add(mcc);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                Panel p = new Panel();
                p.Size = new Size(flowLayoutPanel1.Width - 30, 100);
                Button b = new Button()
                {
                    BackColor = Color.Purple,
                    ForeColor = Color.White,
                    Text = "결과 확인 →"
                };
                b.Size = new Size(200, 50);
                p.Controls.Add(b);
                b.Location = new Point(p.Width / 2 - 100, 30);
                b.MouseClick += b_Click;
                flowLayoutPanel1.Controls.Add(p);
            }
        }

        private void b_Click(object sender, MouseEventArgs e)
        {
            foreach (var item in mccs)
            {
                if(item.result == 0)
                {
                    Emsg("먼저 답변을 모두 해야 결과를 확인하실 수 있습니다.");
                    return;
                }
            }
            if(MessageBox.Show("결과를 확인하시겠습니까?", "질문", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                close = false;
                addForm(this, new d_ResultTest() { mccs = this.mccs });

            }
        }

        private void c_MBTICheck_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (close == false) return;
            if(MessageBox.Show("테스트를 종료하시겠습니까?", "질문", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Dispose();
            }
            else
            {
                e.Cancel = true;
                return;
            }
        }
    }
}
