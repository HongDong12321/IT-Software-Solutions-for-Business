using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 솔루셩
{
    public partial class Form1 : Form
    {

        capturePanel capturePanel;
        List<Point> points = new List<Point>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("캡쳐를 시작하겠습니다.","정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
            points.Clear();
            if(capturePanel != null)
            {
                this.Controls.Remove(capturePanel);
                capturePanel.Dispose();
            }

            capturePanel = new capturePanel()
            {
                Dock = DockStyle.Fill,
            };  
            this.Controls.Add(capturePanel);
            capturePanel.BringToFront();

            capturePanel.MouseClick += capturePanel_MmouseClick;
            capturePanel.Paint += capturePanel_Paint;
        }

        private void capturePanel_MmouseClick(object sender , MouseEventArgs e)
        {
            points.Add(e.Location);
            capturePanel.Invalidate();
            capturePanel.Refresh();

            if(points.Count == 4)
            {
                captureImg();
                capturePanel.MouseClick -= capturePanel_MmouseClick;
            }
        }
        private void capturePanel_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.Red);
            Brush b = Brushes.Red;
            foreach (var item in points)
            {
                e.Graphics.FillEllipse(b, item.X - 3, item.Y - 3, 5,5);
            }
            if(points.Count == 4)
            {
                e.Graphics.DrawPolygon(p, points.ToArray());
            }
            
        }
        private void captureImg()
        {
            int minX = points.Min(x=> x.X);
            int maxX = points.Max(x=> x.X);
            int minY = points.Min(y=> y.Y);
            int maxY = points.Max(y=> y.Y);

            Rectangle bounds = new Rectangle(minX, minY, maxX - minX, maxY - minY);
            
            Point screenPoint = capturePanel.PointToScreen(bounds.Location);

            Bitmap fullScreen = new Bitmap(bounds.Width , bounds.Height);
            using (Graphics g = Graphics.FromImage(fullScreen))
            {
                g.CopyFromScreen(screenPoint, Point.Empty, bounds.Size);
            }

            Bitmap clipped = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics g = Graphics.FromImage(clipped))
            {
                g.Clear(Color.Transparent);
                Point[] localPoint =  points.ConvertAll (p => new Point(p.X - bounds.X,  p.Y -bounds.Y) ).ToArray();
                using (GraphicsPath  path = new GraphicsPath())
                {
                    path.AddPolygon(localPoint);
                    g.SetClip(path);
                    g.DrawImage(fullScreen, Point.Empty);
                }
            }

            Form f = new Form();
            PictureBox p = new PictureBox();
            f.Controls.Add(p);
            p.Image = clipped;

            this.Controls.Remove(capturePanel);
            capturePanel.Dispose();
        }
    }
    class capturePanel : Panel
    {
        protected override void OnPaintBackground(PaintEventArgs e)
        {

        }
    }
}
