using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIRDataManager
{
    internal class ListViewColumnComparer : IComparer
    {
        private int col;
        private bool rev;
        public int Column
        {
            get { return col; }
        }
        public bool Reverse
        {
            get { return rev; }
        }
        public ListViewColumnComparer()
        {
            col = 0;
            rev = false;
        }
        public ListViewColumnComparer(int column, bool reverse)
        {
            col = column;
            rev = reverse;
        }
        public int RawCompare(object x, object y)
        {
            return string.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
        }
        public int Compare(object x, object y)
        {
            return rev ? -RawCompare(x, y) : RawCompare(x, y);
        }
    }
}
