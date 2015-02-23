using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace EvoFromScratch
{
    public class Draw
    {
        Pen PregnantePen;
        Pen MalePen;
        Pen FemalePen;
        Pen ChildMalePen;
        Pen ChildFemalePen;
        Pen ErasPenGrow;
        Pen ErasPenChiled;
        Pen DeathPen;
        System.Windows.Forms.PictureBox PictureBox_;
        
        public Draw(System.Windows.Forms.PictureBox _PictureBox)
        {
            this.PregnantePen = new Pen(Color.Green, 2);
            this.MalePen = new Pen(Color.Blue, 2);
            this.FemalePen = new Pen(Color.Peru, 2);
            this.ChildMalePen = new Pen(Color.Blue, 1);
            this.ChildFemalePen = new Pen(Color.Peru, 1);
            this.ErasPenChiled = new Pen(Color.White, 1);
            this.ErasPenGrow = new Pen(Color.White, 2);
            this.DeathPen = new Pen(Color.Black, 2);
            this.PictureBox_ = _PictureBox;
        }
            
        public void DrawLf (LifeForm _lf)
        {
            Graphics g = Graphics.FromHwnd(PictureBox_.Handle);
            if (_lf.Age < 15 && _lf.Gender == 'M')
            {
                g.DrawRectangle(ErasPenChiled, _lf.OldLocX, _lf.OldLocY, 2, 2);
                g.DrawRectangle(ChildMalePen, _lf.CurrentLocX, _lf.CurrentLocY, 2, 2);
            }
            else if (_lf.Age < 15 && _lf.Gender == 'F')
            {
                    g.DrawRectangle(ErasPenChiled, _lf.OldLocX, _lf.OldLocY, 2, 2);
                    g.DrawRectangle(ChildFemalePen, _lf.CurrentLocX, _lf.CurrentLocY, 2, 2);
            } 
            else if (_lf.Gender == 'M')
            {
                    g.DrawRectangle(ErasPenGrow, _lf.OldLocX, _lf.OldLocY, 2, 2);
                    g.DrawRectangle(MalePen, _lf.CurrentLocX, _lf.CurrentLocY, 2, 2);
            }
            else if (_lf.Gender == 'F')
            {
                if (_lf.IsPregnant == true)
                {
                    g.DrawRectangle(ErasPenGrow, _lf.OldLocX, _lf.OldLocY, 2, 2);
                    g.DrawRectangle(PregnantePen, _lf.CurrentLocX, _lf.CurrentLocY, 2, 2);
                }
                else
                {
                    g.DrawRectangle(ErasPenGrow, _lf.OldLocX, _lf.OldLocY, 2, 2);
                    g.DrawRectangle(FemalePen, _lf.CurrentLocX, _lf.CurrentLocY, 2, 2);
                }
            }
        }

        public void DrawDeath(LifeForm _lf)
        {
            Graphics g = Graphics.FromHwnd(PictureBox_.Handle);
            g.DrawRectangle(ErasPenGrow, _lf.OldLocX, _lf.OldLocY, 2, 2);
            g.DrawRectangle(DeathPen, _lf.CurrentLocX, _lf.CurrentLocY, 2, 2);
        }

    }
}
