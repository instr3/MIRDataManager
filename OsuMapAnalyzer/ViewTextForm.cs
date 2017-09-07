using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OsuMapAnalyzer
{
    public partial class ViewTextForm : Form
    {
        public ViewTextForm(string inputText)
        {
            InitializeComponent();
            textBox.Text = inputText;
        }
    }
}
