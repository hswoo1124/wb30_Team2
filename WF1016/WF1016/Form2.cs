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
    public partial class Form2 : Form
    {
        public string Ip   { get; private set; }
        public int Port    { get; private set; }

        public Form2()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {//취소
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        private void Button2_Click(object sender, EventArgs e)
        {//확인
            Port = int.Parse(textBox1.Text);
            Ip = textBox2.Text;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
