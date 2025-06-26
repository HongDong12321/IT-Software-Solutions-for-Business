using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 전북_1.Model;
using 전북_1.Properties;

namespace 전북_1.View
{
    public partial class b_Main : z_BF
    {
        // 영역 선택
        private Point[] point = new Point[4];
        // 클릭된 좌표 개수
        private int pN = 0;
        // 애니메이션 이미지
        PictureBox[] p = new PictureBox[3];
        Bitmap[] images = new Bitmap[3];

        bool setMove = true;
        bool rec_2 = false;
        bool rec_4 = false;

        bool one = false;
        bool two = false;

        // 캡차 테스트
        Bitmap capture = default;

        public b_Main()
        {
            InitializeComponent();
            this.Name = "메인";
            setImage();
            moveimage();
            panel2.Visible = false;
            
            using (care db = new care())
            {
                //var d = db.result.Where(x => x.uno == user.uno).Select(X => X.mno).FirstOrDefault();
                //var dd = db.mbti.Where(x => x.mno == d).Select(x => x.mname).FirstOrDefault();
                try
                {
                    var dd = db.result.Where(r => r.uno == user.uno).Join(db.mbti, r => r.mno, m => m.mno, (r, m) => m.mname).FirstOrDefault();
                    label2.Text = user.unick + "\n(" + dd + "";

                }
                catch (Exception)
                {
                    Console.WriteLine("유저값이 없습니다.");
                }

            }
        }

        private void setImage()
        {
            Image[] img = { Properties.Resources._11, Properties.Resources._21, Properties.Resources._31 };

            for (int i = 0; i < 3; i++)
            {
                images[i] = new Bitmap(img[i], this.bufferedPanel1.Width, this.bufferedPanel1.Height) { Tag = (i + 1) * this.bufferedPanel1.Width };

                Console.WriteLine("right :" + (int)images[i].Tag);
                //p[i] = new PictureBox()
                //{
                //    Size = new Size(panel1.Width, panel1.Height),
                //    SizeMode = PictureBoxSizeMode.StretchImage
                //};
                //switch (i)
                //{
                //    case 0: p[i].Image = Resources._11; break;
                //    case 1: p[i].Image = Resources._21; break;
                //    case 2: p[i].Image = Resources._31; break;
                //}
                //p[i].MouseClick += panel1_MouseClick; ;
                //p[i].Paint += panel1_Paint;
                //panel1.Controls.Add(p[i]);
                //panel1.Controls.SetChildIndex(p[i], 0);
            }

            //for (int i = 0; i < 3; i++)
            //{
            //    panel1.Controls.SetChildIndex(p[i], panel1.Controls.Count - 1);
            //    p[i].Location = new Point(i * panel1.Width, 0);
            //}

            //panel1.Controls.SetChildIndex(p[i], 0);
        }

        private async void moveimage()
        {
            //if (setMove == false) return;
            //while (setMove)
            //{
            //    for (int i = 0; i < 3; i++)
            //    {
            //        p[i].Left -= 4;
            //        if (p[i].Right <= 0)
            //        {
            //            p[i].Location = new Point(panel1.Width * 2, 0);
            //            await Task.Delay(1000);
            //        }
            //        await Task.Delay(3);

            //    }
            //}
            if (setMove == false) return;
            while (setMove)
            {
                for (int i = 0; i < 3; i++)
                {
                    int right = (int)images[i].Tag;
                    right -= 4;
                    images[i].Tag = right;

                    if (right <= 0)
                    {
                        right = bufferedPanel1.Width * 3;
                        images[i].Tag = right;
                        await Task.Delay(1000);
                    }
                    this.bufferedPanel1.Invalidate();
                    await Task.Delay(3);

                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Imsg("자르기를 시작합니다.");
            Array.Clear(point, 0, point.Length);
            setMove = false;

            capture = new Bitmap(this.bufferedPanel1.Width, this.bufferedPanel1.Height);
            this.bufferedPanel1.DrawToBitmap(capture, this.bufferedPanel1.ClientRectangle);
                
            if (bufferedPanel1.Cursor == Cursors.Default)
            {
                bufferedPanel1.Cursor = Cursors.Hand;
            }
        }
        private void bufferedPanel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawImages(g);
            Draw(g);
        }

        private void bufferedPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (bufferedPanel1.Cursor == Cursors.Hand)
            {
                //point[pN] = ((Control)sender).PointToScreen(e.Location);
                //point[pN] = panel1.PointToScreen(e.Location);
                point[pN] = this.bufferedPanel1.PointToClient((sender as Control).PointToScreen(e.Location));
                pN++;
                if (pN == 4)
                {
                    rec_4 = true;
                    pN = 0;
                    foreach (var item in point)
                    {
                        Console.WriteLine(item.X + " " + item.Y);
                    }
                }
            }
            else if (bufferedPanel1.Cursor == Cursors.Cross)
            {
                //point[pN] = ((Control)sender).PointToScreen(e.Location);
                //point[pN] = panel1.PointToScreen(e.Location);
                point[pN] = e.Location;
                pN += 2;
                if (pN == 4)
                {
                    point[1] = new Point(point[0].X, point[2].Y);
                    point[3] = new Point(point[2].X, point[0].Y);
                    rec_2 = true;
                    pN = 0;
                }
            }
            bufferedPanel1.Refresh();
            bufferedPanel1.Invalidate();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Imsg("캡쳐를 시작합니다.");
            Array.Clear(point, 0, point.Length);
            capture = new Bitmap(this.bufferedPanel1.Width, this.bufferedPanel1.Height);
            this.bufferedPanel1.DrawToBitmap(capture, this.bufferedPanel1.ClientRectangle);

            setMove = false;
            if (bufferedPanel1.Cursor == Cursors.Default)
            {   
                bufferedPanel1.Cursor = Cursors.Cross;
            }
        }



       

      
        private void DrawImages(Graphics g)
        {
            for (int j = 0; j < images.Length; j++)
            {
                int right = (int)images[j].Tag;
                g.DrawImage(images[j], right - this.bufferedPanel1.Width, 0, this.bufferedPanel1.Width, this.bufferedPanel1.Height);
            }
        }

        private void Draw(Graphics g)
        {

            Pen p = new Pen(Color.Red);
            Brush b = Brushes.Red;
            //Point[] screenPoint = point.Select(pp => panel1.PointToScreen(pp)).ToArray();

            foreach (var item in point)
            {
                if (item.X != 0 && item.Y != 0)
                {
                    g.FillEllipse(b, item.X - 3, item.Y - 3, 5, 5);
                }
            }
            if (rec_2)
            {
                g.DrawPolygon(p, point);
                one = rec_2;
                saveImg();
            }
            if (rec_4)
            {
                g.DrawPolygon(p, point);
                two = rec_4;
                saveImg();
            }

        }
        int i = 0;


        private async void saveImg()
        {
            await Task.Delay(1000);
            int minX = point.Min(p => p.X);
            int minY = point.Min(p => p.Y);
            int maxX = point.Max(p => p.X);
            int maxY = point.Max(p => p.Y);



            int width = maxX - minX;
            int height = maxY - minY;

            if (width == 0 && height == 0) return;

            Console.WriteLine(width);
            Console.WriteLine(height);

            //Bitmap fullBmp = new Bitmap(width, height);
            //using (Graphics g = Graphics.FromImage(fullBmp))
            //{
            //    g.CopyFromScreen(minX, minY, 0, 0, new Size(width, height));
            //}

            Bitmap clippedBmp = capture.Clone() as Bitmap;
            //Bitmap clippedBmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(clippedBmp))
            {
                //Point[] localPoints = point.Select(p => new Point(p.X - minX, p.Y - minY)).ToArray();
                GraphicsPath path = new GraphicsPath();
                path.AddPolygon(point.ToArray());
                g.Clear(Color.Transparent);
                g.SetClip(path);
                g.DrawImage(capture, 0, 0);
            }

            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string savePath = Path.Combine(desktop, "PictureBox_캡처" + (i++) + ".png");
            clippedBmp.Save(savePath, System.Drawing.Imaging.ImageFormat.Png);

            bufferedPanel1.Cursor = Cursors.Default;
            for (int i = 0; i < 4; i++)
            {
                point[i] = new Point(0, 0);
            }
            bufferedPanel1.Refresh();
            setMove = true;
            moveimage();
            capture.Dispose();
            clippedBmp.Dispose();
            rec_4 = false;
            rec_2 = false;
            if (one && two)
            {
                pictureBox2.Image = Resources.menu2;
            }
        }

        private void b_Main_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (one && two)
            {
                if (panel2.Visible)
                {
                    panel2.Visible = false;
                }
                else
                {
                    panel2.Visible = true;
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Dispose();
        }

    }


}
