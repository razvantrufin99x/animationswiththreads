using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace animationswiththreads
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private delegate void SafeCallDelegate(string text);
        private Thread thread2 = null;

        private delegate void SafeCallDelegate2(object o);
        private Thread thread3 = null;
        int mod = 0;

        private delegate void SafeCallDelegate3(object o);
        private Thread thread4 = null;
        int mod2 = 0;

        private Thread thread5 = null;
        private Thread thread6 = null;

        private void WriteTextSafe(string text)
        {
            if (textBox1.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteTextSafe);
                textBox1.Invoke(d, new object[] { text });
            }
            else
            {
                int i=0;
                textBox1.Text = text;
                while ( i< 100){
                    textBox1.Text += i.ToString();
                    i++;
                   // Thread.Sleep(20);
                    Refresh();
                }
            }
        }

        private void SetText()
        {
            WriteTextSafe("This text was set safely.");
        }


        private void WriteTextSafe2(object oo)
        {
            if (button3.InvokeRequired)
            {
                var d = new SafeCallDelegate2(WriteTextSafe2);
                button3.Invoke(d, new object[] { oo });
            }
            else
            {
                if (mod == 0)
                {
                    while (button3.Left < 600)
                    {
                        button3.Left += 10;
                        Refresh();
                        if (button3.Left >= 600) { mod = 1; }
                    };
                }
                else
                {
                    while (button3.Left > 100)
                    {
                        button3.Left -= 10;
                        Refresh();
                        if (button3.Left <= 100) { mod = 0; }
                    };
                }
            }
        }

        private void SetText2()
        {
            WriteTextSafe2(this.button3);
        }


        private void WriteTextSafe3(object oo)
        {
            if (button5.InvokeRequired)
            {
                var d = new SafeCallDelegate3(WriteTextSafe3);
                button5.Invoke(d, new object[] { oo });
            }
            else
            {
                if (mod2 == 0)
                {
                    while (button5.Top < 600)
                    {
                        button5.Top += 10;
                        Thread.Sleep(1);
                        Refresh();
                        if (button5.Top >= 600) { mod2 = 1; }
                    };
                }
                else
                {
                    while (button5.Top > 100)
                    {
                        button5.Top -= 10;
                        Thread.Sleep(1);
                        Refresh();
                        if (button5.Top <= 100) { mod2 = 0; }
                    };
                }
            }
        }

        private void SetText3()
        {
            WriteTextSafe3(this.button4);
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            thread2 = new Thread(new ThreadStart(SetText));
            thread2.Start();
            Thread.Sleep(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            thread3 = new Thread(new ThreadStart(SetText2));
            thread3.Start();
            Thread.Sleep(1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            thread4 = new Thread(new ThreadStart(SetText3));
            thread4.Start();
            Thread.Sleep(1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            thread3 = new Thread(new ThreadStart(SetText3));
           // thread3.Start();
            //Thread.Sleep(1);
            thread4 = new Thread(new ThreadStart(SetText3));
            //thread4.Start();
            //Thread.Sleep(1);

            thread5 = new Thread(new ThreadStart(SetText2));
            //thread5.Start();
            //Thread.Sleep(1);
            thread6 = new Thread(new ThreadStart(SetText2));
            //thread6.Start();
            //Thread.Sleep(1);


            thread3.Start();
            thread4.Start();
            thread5.Start();
            thread6.Start();
        }

      
    }
}
