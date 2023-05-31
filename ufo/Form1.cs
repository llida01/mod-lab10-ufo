using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ufo
{
    public partial class Form1 : Form
    {
        bool paint = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Chart_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            paint = true;
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (paint)
            {
                double dist1;
                int x1 = 10;
                int y1 = 10;
                int x2 = 500;
                int y2 = 200;
                int r = 8;
                int n = 5;
                Graphics g = e.Graphics;
                Brush b = new SolidBrush(Color.MidnightBlue);
                g.FillEllipse(b, x1 - 5, y1 - 5, r, r);
                g.FillEllipse(b, x2 - 5, y2 - 5, r, r);
                double x = x1;
                double y = y1;
                int step = 5;

                double angle = Math.Atan(Math.Abs(y2 - y1) / (double)Math.Abs(x2 - x1)) * 180 / Math.PI;
                double dist = Math.Sqrt((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1));
                double sin = Sin(angle, r);
                double cos = Cos(angle, r);
                Brush b1 = new SolidBrush(Color.Indigo);
                while (dist > n)
                {
                    Thread.Sleep(20);
                    x += step * cos;
                    y += step * sin;
                    g.FillEllipse(b1, (int)x - 5, (int)y - 5, 5, 5);
                    dist1 = dist;
                    dist = Math.Sqrt(Math.Pow(y2 - y, 2) + Math.Pow(x2 - x, 2));

                    if (dist1 < dist)
                    {
                        break;
                    }
                }
            }
        }

        private double Cos(double angle, int r)
        {
            double cos = 0;
            for (int i = 0; i < r; ++i)
            {
                if (i % 2 == 0)
                {
                    cos += Math.Pow(angle / 180 * Math.PI, 2 * i) / Factorial(2 * i);
                }
                else
                {
                    cos -= Math.Pow(angle / 180 * Math.PI, 2 * i) / Factorial(2 * i);
                }
            }
            return cos;
        }

        private double Sin(double angle, int r)
        {
            double sin = 0;
            for (int i = 0; i < r; ++i)
            {
                if (i % 2 == 0)
                {
                    sin += Math.Pow(angle / 180 * Math.PI, 2 * i + 1) / Factorial(2 * (i + 1) - 1);
                }
                else
                {
                    sin -= Math.Pow(angle / 180 * Math.PI, 2 * i + 1) / Factorial(2 * (i + 1) - 1);
                }
            }
            return sin;
        }

        static int Factorial(int n)
        {
            int answer = 1;
            for (int i = 1; i <= n; i++)
            {
                answer *= i;
            }
            return answer;
        }
    }
}
