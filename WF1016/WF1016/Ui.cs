using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF1016
{
    class Ui
    {
        public void FillDrawing(Control control, Color color)
        {
            control.BackColor = color;
        }

        public void Label(Control control, bool b)
        {
            if (b)
            {
                control.Text = "서버 연결...";
                control.BackColor = Color.Blue;
            }
            else
            {
                control.Text = "서버 연결 해제...";
                control.BackColor = Color.Red;
            }
        }

        public void LogPrint(ListBox lb1, string msg)
        {
            msg += "(" + DateTime.Now.ToString() + ")";
            lb1.Items.Add(msg);
        }
    }
}
