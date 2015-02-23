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
            int Width = 1000;
            int Hight = 700;
            int LifeSpeed = 50;
            
            Application.Run(new MainForm(Width, Hight, LifeSpeed));
        }
    }
}
