using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIREditor
{
    public partial class TextInputForm : Form
    {
        public TextInputForm(string initText,string captainText="")
        {
            InitializeComponent();
            textBox.Text = initText;
            if (captainText == "")
                Text = "输入文本";
            else
                Text = captainText;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Tag = textBox.Text;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Tag = null;
            Close();
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar=='\r')
            {
                buttonOK_Click(sender, e);
            }
        }
    }
}
