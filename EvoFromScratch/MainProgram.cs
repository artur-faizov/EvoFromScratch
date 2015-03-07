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
            Params Par = new Params();
            Par.Width = 1000;
            Par.Hight = 700;

            Par.LifeSpeed = 100;
            Par.LifePeriod = 10;

            Par.StartColoniCount = 40;
            Par.ChildAge = 2;
            Par.PregnantePeriod = 1;
            Par.SexDistance = 2;
            Par.BornAtOnce = 2;
            

            Application.Run(new MainForm(Par));
        }
    }
}
