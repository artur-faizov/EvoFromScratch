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
            Par.LifeSpeed = 25;

            Par.StartColoniCount = 10;
            Par.ChildAge = 5;
            Par.PregnantePeriod = 2;
            Par.LifePeriod = 50;
            Par.SexDistance = 11;
            Par.BornAtOnce = 2;
            

            Application.Run(new MainForm(Par));
        }
    }
}
