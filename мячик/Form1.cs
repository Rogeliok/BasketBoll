using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;

namespace мячик
{
    public partial class Form1 : Form
    {
        string stfolder;
        public Form1()
        {
            InitializeComponent();
            stfolder = @"C:\D\unversity\Programming\расчет фортран графики";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //preparation
            //IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
            int n, j;
            double H, L;

            //H = double.Parse(textBox2.Text, formatter);
            Console.WriteLine(textBox2.Text);
            //L = double.Parse(textBox3.Text, formatter);
            H = double.Parse(textBox2.Text);
            //L = double.Parse(textBox3.Text);
            //n = int.Parse(textBox9.Text);
            //H = Convert.ToDouble(textBox2.Text);
            L = Convert.ToDouble(textBox3.Text);
            n = Convert.ToInt32(textBox9.Text);
            string st;
            st = textBox1.Text + " , " + textBox4.Text + " , " + textBox5.Text + " , " + textBox8.Text + " , " + textBox7.Text + " , " + textBox6.Text + " , " + textBox9.Text;
            StreamWriter info_for_ft;
            info_for_ft = new StreamWriter(stfolder + "\\in.txt");
            info_for_ft.Write(st);
            info_for_ft.Write("\n");
            info_for_ft.Write("hight , Velosity , phi , alpha , mass , Time , n");
            info_for_ft.Close();

            //System.Diagnostics.ProcessStartInfo ft = new System.Diagnostics.ProcessStartInfo(stfolder + "\\Pen.exe");
            //ft.WorkingDirectory = stfolder;
            using (Process pr = new Process())
            {
                pr.StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = stfolder,
                    FileName = stfolder + "\\Pen.exe"
                };
                //pr.StartInfo = ft;
                pr.Start();
                pr.WaitForExit();
            }

            //System.Diagnostics.ProcessStartInfo ft = new System.Diagnostics.ProcessStartInfo(stfolder + "\\Pen.exe");
            //ft.WorkingDirectory = stfolder;
            //System.Diagnostics.Process.Start(ft);
            //System.Threading.Thread.Sleep(5000);

            // рисовалка
            double mx, my;
            Bitmap btmp;
            btmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = btmp;
            Graphics gr = Graphics.FromImage(pictureBox1.Image);
            Pen pn = new Pen(Color.Green, 2);
            SolidBrush pn1 = new SolidBrush(Color.Red);

            double[] x, y;
            x = new double[2 * n];
            y = new double[2 * n];
            
            string st1;
            StreamReader info_from_ft;
            info_from_ft = new StreamReader(stfolder + "\\out.txt");
            j = n;
            for (int i = 0; !info_from_ft.EndOfStream; i++)
            {
                st1 = info_from_ft.ReadLine();
                textBox10.Text = "ok1";
                x[i] = double.Parse(st1.Substring((st1.IndexOf(";") + 3), 16));
                y[i] = double.Parse(st1.Substring(st1.IndexOf(",") + 3));
                //x[i] = double.Parse(st1.Substring((st1.IndexOf(";") + 3), 16), formatter);
                //y[i] = double.Parse(st1.Substring(st1.IndexOf(",") + 3), formatter);
                /*if (i % 2 == 0) 
                {
                    x[i] = double.Parse(st1.Trim());
                    textBox10.Text = "ok2";
                }
                else 
                {
                    y[i] = Convert.ToDouble(st1.Trim());
                    textBox10.Text = "ok3";
                }*/
                if (y[i] < 0.0)
                {
                    j = i;
                    break;
                }
            }
            this.Text = "ok4";
            info_from_ft.Close();
            info_from_ft.Dispose();

            StreamWriter info;
            info = new StreamWriter(stfolder + "\\in(a).txt");
            for (int i = 0; i < j; i++)
            {
                st = x[i].ToString() + " " + y[i].ToString();
                info.Write(st);
                info.Write("\n");
            }
            info.Close();
            info.Dispose();

            mx = pictureBox1.Width / (x.Max() - x.Min());
            my = pictureBox1.Height / (y.Max() - y.Min());

            for (int i = 0; i < j - 1; i++) {
                gr.DrawLine(pn, Convert.ToInt32((x[i] - x.Min()) * mx), Convert.ToInt32((y.Max() - y[i]) * my), Convert.ToInt32((x[i + 1] - x.Min()) * mx), Convert.ToInt32((y.Max() - y[i + 1]) * my));
            }
            //gr.FillEllipse(pn1, Convert.ToInt32((L - x.Min()) * mx), Convert.ToInt32((y.Max() - H) * my), 50, 50);
            gr.FillEllipse(pn1, Convert.ToInt32(L / pictureBox1.Width), Convert.ToInt32((H - y.Min()) * my), 50, 50);
        }

    }
}
