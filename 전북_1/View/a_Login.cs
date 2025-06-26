using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 전북_1.Model;

namespace 전북_1.View
{
    public partial class 로그인 : z_BF
    {
        int[] check = new int[4];
        List<PictureBox> ps = new List<PictureBox>();
        Queue<Tuple<PictureBox, int>> pq = new Queue<Tuple<PictureBox, int>>();

        public 로그인()
        {
            InitializeComponent();
            setClear();
            for (int i = 0; i < 4; i++)
            {
                check[i] = 0;
            }
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            pictureBox7.Visible = false;
            pictureBox8.Visible = false;
            setPicture(this);
            setButton(button1);
            this.Text = "로그인";
        }

        void setClear()
        {
            ps.Clear();
            pq.Clear();
        }

        void setPicture(Form f)
        {
            foreach (var item in f.Controls)
            {
                if (item is PictureBox p)
                {

                    if (!ps.Contains(p))
                    {
                        ps.Add(p);
                    }
                }
            }
            ps.Reverse();
            return;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;
            int i = 3;

            foreach (var item in ps)
            {
                i++;

                if (p.Name == item.Name)
                {
                    if (ps[i].Visible == false)
                    {
                        if (pq.Count < 2)
                        {
                            pq.Enqueue(Tuple.Create(ps[i], (i - 3)));
                            ps[i].Visible = true;
                        }
                        else
                        {
                            var del = pq.Dequeue();
                            del.Item1.Visible = false;
                            pq.Enqueue(Tuple.Create(ps[i], (i - 3)));
                            ps[i].Visible = true;
                        }
                    }
                    else
                    {
                        Queue<Tuple<PictureBox, int>> newQueue = new Queue<Tuple<PictureBox, int>>();

                        while (pq.Count > 0)
                        {
                            var temp = pq.Dequeue();
                            PictureBox pTemp = temp.Item1;
                            int pN = temp.Item2;
                            if (pTemp == ps[i] || pTemp.Name == ps[i].Name)
                            {
                                pTemp.Visible = false;
                            }
                            else
                            {
                                newQueue.Enqueue(Tuple.Create(pTemp, pN));
                                Console.WriteLine(pN);
                            }
                        }
                        pq = newQueue;

                    }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (care db = new care())
            {
                if (string.IsNullOrEmpty(textBox1.Text)) //공백 처리
                {
                    Emsg("빈칸이 있습니다.");
                    return;
                }

                List<int> checklist = new List<int>(); //비밀번호 이미지 순서, 값 확인용 리스트
                foreach (var item in pq)
                {
                    checklist.Add(item.Item2);
                }

                if (pq.Count == 0)
                {
                    checklist.Add(0);
                    checklist.Add(0);
                }

                if (textBox1.Text == "admin") // 관리자 조건
                {
                    foreach (var item in checklist)
                    {
                        if (item != 0) //하나라도 선택되어 있을 시 경고
                        {
                            Emsg("비밀번호 이미지를 선택해주세요.");
                            return;
                        }
                    }
                    Imsg("관리자님 환영합니다.");
                    return;
                }
                else
                {
                    int i = 0;
                    foreach (var item in db.user)
                    {
                        if (item.uid == textBox1.Text)
                        {
                            foreach (var item1 in item.upw.Split(','))
                            {
                                if (int.Parse(item1) != checklist[i])
                                {
                                    Emsg("비밀번호 이미지를 확인해주세요.");
                                    return;
                                }
                                i++;
                            }
                            user = item;
                            Imsg(user.unick + " 회원님 환영합니다.");
                            addForm(this, new b_Main());
                            return;
                        }
                    }
                    Emsg("일치하는 아이디가 없습니다.");
                }
            }
        }

        private void 로그인_Load(object sender, EventArgs e)
        {
            fs.Add(this);
        }
    }
}
