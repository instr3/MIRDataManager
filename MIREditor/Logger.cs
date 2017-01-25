using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIREditor
{
    static class Logger
    {
        public static TextBox textBox;

        public static void Register(TextBox registerTextBox)
        {
            textBox = registerTextBox;
        }
        public static void Log(string text)
        {
            textBox.Text += string.Format("[{0:HH}:{0:mm}:{0:ss}.{0:fff}]", DateTime.Now) + text + Environment.NewLine;
            textBox.SelectionStart = textBox.TextLength;
            textBox.ScrollToCaret();
        }
        public static void Clear()
        {
            textBox.Text = "";
        }
    }
}
