using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ufo
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            chart1.Visible = true;
            chart1.ChartAreas.Clear();
            chart1.Series.Clear();
            chart1.ChartAreas.Add("area1");
            chart1.Titles.Add("Зависимость точности от радиуса попадания");
            chart1.Series.Add("Зависимость точности от радиуса попадания");
            chart1.Series["Зависимость точности от радиуса попадания"].ChartType = SeriesChartType.Spline;
            chart1.Series["Зависимость точности от радиуса попадания"].Color = Color.DarkSlateGray;
            chart1.ChartAreas[0].AxisX.Title = "Радиус попадания";
            chart1.ChartAreas[0].AxisY.Title = "Точность";
            chart1.ChartAreas[0].AxisX.Minimum = 0;

            int step = 1;
            int x1 = 10;
            int y1 = 10;
            int x2 = 500;
            int y2 = 200;

            for (double i = 1; i <= 51; i = i + 10)
            {
                int n = N(x1, y1, x2, y2, step, i);
                chart1.Series["Зависимость точности от радиуса попадания"].Points.AddXY(i, n);
            }
        }

        int N(int x1, int y1, int x2, int y2, int step, double eps)
        {
            int n = 1;
            bool is_finish = false;
            double angle;
            double dist1;
            double x = x1;
            double y = y1;
            while ((n < 15) && (is_finish == false))
            {
                angle = Math.Atan(Math.Abs(y2 - y1) / (double)Math.Abs(x2 - x1)) * 180 / Math.PI;
                double dist = Math.Sqrt((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1));
                dist1 = Math.Sqrt(Math.Pow((x - x2), 2) + Math.Pow((y - y2), 2));
                while (true)
                {
                    if (dist <= dist1)
                    {
                        dist1 = Math.Sqrt(Math.Pow((x - x2), 2) + Math.Pow((y - y2), 2));
                        x += step * Cos(angle, n);
                        y += step * Sin(angle, n);
                        dist = Math.Sqrt(Math.Pow((x - x2), 2) + Math.Pow((y - y2), 2));
                        if (dist <= eps)
                        {
                            is_finish = true;
                            break;
                        }
                    }
                    else
                    {
                        n++;
                        break;
                    }
                }
            }
            return n;
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
