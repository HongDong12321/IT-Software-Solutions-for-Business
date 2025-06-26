using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 전북_1.View
{
    public partial class Test : Form
    {
        Image[] images = new Image[3];

        public Test()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawImages(g);
        }

        private void DrawImages(Graphics g)
        {
            for (int j = 0; j < images.Length; j++)
            {
                int right = (int)images[j].Tag;
                g.DrawImage(images[j], right - this.bufferedPanel1.Width, 0, this.bufferedPanel1.Width, this.bufferedPanel1.Height);
            }
        }

        private void Test_Load(object sender, EventArgs e)
        {
            Image[] img = { Properties.Resources._11, Properties.Resources._21, Properties.Resources._31 };

            for (int i = 0; i < 3; i++)
            {
                images[i] = new Bitmap(img[i], this.bufferedPanel1.Width, this.bufferedPanel1.Height) { Tag = (i + 1) * this.bufferedPanel1.Width };
            }

            moveimage();
        }

        private async void moveimage()
        {
            while (true)
            {
                for (int i = 0; i < 3; i++)
                {
                    int right = (int)images[i].Tag;
                    right -= 4;
                    images[i].Tag = right;

                    if (right <= 0)
                    {
                        right = this.bufferedPanel1.Width * 3;
                        images[i].Tag = right;
                        await Task.Delay(1000);
                    }
                    this.bufferedPanel1.Invalidate();
                    await Task.Delay(3);

                }
            }
        }
    }

    public class BufferedPanel : Panel
    {
        public BufferedPanel()
        {
            DoubleBuffered = true;
        }
    }
}
