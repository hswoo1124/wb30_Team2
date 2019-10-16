using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF1016
{
    public partial class Form1 : Form
    {
        Ui ui = new Ui();
        Client client = new Client();
        Form2 f2 = new Form2();

        public Form1()
        {
            InitializeComponent();
            client.ParentInfo(this);
            ui.FillDrawing(panel1, Color.Empty);
        }

        private void 프로그램종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 서버연결ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (f2.ShowDialog() == DialogResult.OK)
            {
                int port = f2.Port;
                string ip = f2.Ip;
                string msg;
                if (client.Connect(ip, port) == true)
                {
                    msg = string.Format("[연결성공] {0}:{1}", client.Ip, client.Port);
                    ui.FillDrawing(panel1, Color.Green);
                    ui.Label(label1, true);
                    ui.LogPrint(listBox1, msg);
                }
                else
                {
                    msg = string.Format("[연결실패] {0}:{1}", client.Ip, client.Port);
                    ui.FillDrawing(panel1, Color.Red);
                    ui.Label(label1, false);
                    ui.LogPrint(listBox1, msg);
                }
            }
        }

        private void 서버연결해제ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg;
            msg = string.Format("[연결해제] {0}:{1}", client.Ip, client.Port);

            client.DisConnect();

            ui.FillDrawing(panel1, Color.Red);
            ui.Label(label1, false);
            ui.LogPrint(listBox1, msg);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string msg = textBox2.Text;

            client.SendMessage(name, msg);
        }

        public void ShortMessage(string str)
        {
            ui.LogPrint(listBox2, str);
        }
    }
}
