using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIREditor
{
    public static class Logger
    {
        public static TextBox textBox;

        public static void Register(TextBox registerTextBox)
        {
            textBox = registerTextBox;
        }
        public static void Log(string text)
        {
            text= string.Format("[{0:HH}:{0:mm}:{0:ss}.{0:fff}]", DateTime.Now) + text + Environment.NewLine;
            if (textBox is null)
            {
                Console.Write(text);
            }
            else
            {
                textBox.Text += text;
                textBox.SelectionStart = textBox.TextLength;
                textBox.ScrollToCaret();
            }
        }
        public static void Clear()
        {
            if (textBox is null)
                Console.Clear();
            else
                textBox.Text = "";
        }
    }
}
