using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EvoFromScratch
{
    class MainProgram
    {
        static public void Main()
        {
            int Width = 300;
            int Hight = 300;
            int LifeSpeed = 25;
            
            Application.Run(new MainForm(Width, Hight, LifeSpeed));
        }
    }
}
