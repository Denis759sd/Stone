using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        
        private const double _g = 9.81;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _v0 = double.Parse(textBox1.Text);
            _angle = double.Parse(textBox2.Text) * Math.PI / 180;
            _h0 = double.Parse(textBox3.Text);

            _SinA = Math.Sin(_angle);
            _CosA = Math.Cos(_angle);
            
            double D = _v0 * _v0 * _SinA * _SinA + 2 * _g * _h0;
            
            _Time = (_v0 * _SinA + Math.Sqrt(D)) / _g;
            _L = _v0 * _CosA * _Time;
            _Hmax = D / (2 * _g);

            textBox4.Text = _L.ToString();
            textBox5.Text = _Time.ToString();
            textBox6.Text = _Hmax.ToString();
        }
    }
}