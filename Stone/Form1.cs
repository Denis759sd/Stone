using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;

namespace Stone
{
    public partial class Form1 : Form
    {
        private double _v0;
        private double _angle;
        private double _h0;

        private double _L;
        private double _Time;
        private double _Hmax;

        private double _SinA;
        private double _CosA;

        private double D;
        private double t;
        private double dt;
        private double x;
        private double y;
        private double kx;
        private double ky;

        private int X1;
        private int Y1;
        private int X2;
        private int Y2;
        
        private const double _g = 9.81;
        
        Graphics graphics1;
        Graphics graphics2;
        Bitmap bitmap1;
        Bitmap bitmap2;
        
        Pen pen1 = new Pen(Color.Brown, 2);
        Pen pen2 = new Pen(Color.Blue, 2);
        Pen pen3 = new Pen(Color.Red, 1.2f);

        SolidBrush brush = new SolidBrush(Color.Yellow);

        private bool flag;


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            flag = true;
            
            _v0 = double.Parse(textBox1.Text);
            _angle = double.Parse(textBox2.Text) * Math.PI / 180;
            _h0 = double.Parse(textBox3.Text);

            _SinA = Math.Sin(_angle);
            _CosA = Math.Cos(_angle);
            
            D = _v0 * _v0 * _SinA * _SinA + 2 * _g * _h0;
            
            _Time = (_v0 * _SinA + Math.Sqrt(D)) / _g;
            _L = _v0 * _CosA * _Time;
            _Hmax = D / (2 * _g);

            textBox4.Text = _L.ToString();
            textBox5.Text = _Time.ToString();
            textBox6.Text = _Hmax.ToString();
            
            MakeGraph();
        }

        private void MakeGraph()
        {
            graphics1.Clear(pictureBox1.BackColor);

            for (int i = 0; i < 10; i++)
            {
                graphics1.DrawLine(pen3, i * bitmap1.Width / 10, 0, i * bitmap1.Width /10, bitmap1.Height);
                graphics1.DrawLine(pen3, 0 ,i * bitmap1.Height / 10, bitmap1.Width, i * bitmap1.Height / 10);
            }

            pictureBox1.Image = bitmap1;

            dt = _Time / 1000;
            kx = bitmap1.Width / _L;
            ky = bitmap1.Height / _Hmax;
            X1 = 0;
            Y1 = (int)(bitmap1.Height - ky * _h0);
            t = 0;
            timer1.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics1 = Graphics.FromImage(bitmap1);
            bitmap2 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics2 = Graphics.FromImage(bitmap2);
        }

        private void timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            t += dt;
            x = _v0 * _CosA * t;
            y = _h0 + _v0 * _SinA * t - _g * t * t / 2;
            X2 = (int)(kx * x);
            Y2 = (int)(bitmap1.Height - ky * y);
            
            graphics1.DrawLine(pen2, X1, Y1, X2,Y2);
            graphics2.DrawImage(bitmap1,0,0);
            graphics2.DrawEllipse(pen1, X2 - 5, Y2-5, 11,11);
            graphics2.FillEllipse(brush, X2 - 5, Y2-5, 11,11);

            pictureBox1.Image = bitmap2;
            X1 = X2;
            Y1 = Y2;
            if (t >= _Time)
            {
                timer1.Enabled = false;
                MessageBox.Show("Движение закончено.", "Важное сообщение!!!", MessageBoxButtons.OK);
                button1.Enabled = true;
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            if (!flag) return;
            label7.Visible = true;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (!flag) return;
            label7.Visible = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label7.Left = pictureBox1.Left + e.X + 5;
            label7.Top = pictureBox1.Top + e.Y + 5;
        }
    }
}