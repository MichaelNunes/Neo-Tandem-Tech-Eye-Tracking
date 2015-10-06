using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spinningprogbar
{
    public class shower
    {
        internal Form1 m = new Form1();
        public void showBox()
        {
            
            m.ShowDialog();
        }

        public void endBox()
        {
            m.Close();
        }
    }
}
