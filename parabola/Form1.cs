using System;
using System.Drawing;
using System.Windows.Forms;

namespace ParabolaApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "График параболы y = x^2";
            this.Width = 800;
            this.Height = 600;
            this.DoubleBuffered = true; // Для плавной отрисовки
            this.Paint += DrawParabola;
        }

        private void DrawParabola(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // Сглаживание

            // Настройки осей и параболы
            Pen axisPen = new Pen(Color.Black, 2);
            Pen parabolaPen = new Pen(Color.Red, 3);
            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;
            float scale = 50; // Пикселей на единицу

            // Ось X
            g.DrawLine(axisPen, 0, centerY, this.ClientSize.Width, centerY);
            // Ось Y
            g.DrawLine(axisPen, centerX, 0, centerX, this.ClientSize.Height);

            // Рисуем параболу y = x^2
            PointF[] points = new PointF[this.ClientSize.Width];
            for (int i = 0; i < this.ClientSize.Width; i++)
            {
                float x = (i - centerX) / scale;
                float y = x * x;
                points[i] = new PointF(i, centerY - y * scale);
            }
            g.DrawCurve(parabolaPen, points);

            // Подписи осей
            Font font = new Font("Arial", 10);
            g.DrawString("X", font, Brushes.Black, this.ClientSize.Width - 20, centerY - 20);
            g.DrawString("Y", font, Brushes.Black, centerX + 10, 10);
        }
    }
}