using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EvoFromScratch
{
    public class LifeForm
    {
        public int ID;
        public float OldLocX;
        public float OldLocY;
        public float CurrentLocX;
        public float CurrentLocY;
        public float MoveAngle;
        public float MoveStep;
        public int Age;
        public bool IsPregnant;
        public bool IsAlive;
        DateTime BornTime;
        DateTime PregnantTime;
        public char Gender;


        int Width;
        int Hight;

        static Random rnd = new Random();

        public LifeForm(List<LifeForm> _Coloni, int Width, int Hight)
        {
            this.Width = Width;
            this.Hight = Hight;
            this.ID = _Coloni.Count + 1;
            this.Age = 1;
            this.CurrentLocX = rnd.Next(0, Width);
            this.CurrentLocY = rnd.Next(0, Hight);
            this.MoveAngle = (float)(rnd.Next(0, 360) * Math.PI / 180);
            this.MoveStep = 50/rnd.Next(5, 10);
            this.BornTime = DateTime.Now;
            this.PregnantTime = DateTime.Now;
            this.IsPregnant = false;
            this.IsAlive = true;
            char[] _gender = new char[2] { 'M', 'F' };
            this.Gender = _gender[rnd.Next(0,2)];
        }

        public void Move()
        {
            this.OldLocX = this.CurrentLocX;
            this.OldLocY = this.CurrentLocY;
            double NewX = this.CurrentLocX + this.MoveStep * Math.Sin(this.MoveAngle);
            double NewY = this.CurrentLocY + this.MoveStep * Math.Cos(this.MoveAngle);
            Random _rnd = new Random();
            while ((NewX < 0) || (NewX > this.Width) || (NewY < 0) || (NewY > this.Hight))
            {
                this.MoveAngle = (float)(_rnd.Next(0, 360) * Math.PI / 180);
                NewX = this.CurrentLocX + this.MoveStep * Math.Sin(this.MoveAngle);
                NewY = this.CurrentLocY + this.MoveStep * Math.Cos(this.MoveAngle);
            }
                this.CurrentLocX = (float)NewX;
                this.CurrentLocY = (float)NewY;
        }

        public void Born(List<LifeForm> _Coloni, LifeForm lf)
        {
            TimeSpan T = DateTime.Now - lf.PregnantTime;
            if ((T.TotalSeconds) >= 1)
            {
                Random X = new Random();
                int ChildCount = X.Next(1, 2);
                for (int i = 0; i < ChildCount; i++)
                {
                    _Coloni.Add(new LifeForm(_Coloni, lf.Width, lf.Hight));
                    _Coloni[_Coloni.Count - 1].CurrentLocX = lf.CurrentLocX;
                    _Coloni[_Coloni.Count - 1].CurrentLocY = lf.CurrentLocY;
                }
            }   
        }

        public void Search(List<LifeForm> _Coloni, LifeForm lf)
        {
            if (lf.Age > 15 && lf.IsPregnant != true && lf.IsAlive == true)
            {
                foreach (LifeForm lf2 in _Coloni)
                {
                    if (lf2.Age > 15 && lf.ID != lf2.ID && lf2.IsPregnant != true 
                        && lf2.IsAlive == true && lf.Gender != lf2.Gender)
                    {
                        if (Math.Abs(lf.CurrentLocX - lf2.CurrentLocX) < 50 &&
                            Math.Abs(lf.CurrentLocY - lf2.CurrentLocY) < 50)
                        {
                            if (lf.Gender == 'F')
                            {
                                lf.IsPregnant = true;
                                lf.PregnantTime = DateTime.Now;
                            }
                            else
                            {
                                lf2.IsPregnant = true;
                                lf2.PregnantTime = DateTime.Now;
                            }
                        }
                    }
                }
            }  
        }

        public void Growing(LifeForm lf)
        {
            TimeSpan T = DateTime.Now - lf.BornTime;
            lf.Age = (int)T.TotalSeconds;
        }

        /*public void Death2(List<LifeForm> _Coloni, LifeForm lf)
        {
            if (lf.Age > 100)
            {
                _Coloni.Remove(lf);
            }
        }*/

        public void Death (List<LifeForm> _Coloni, LifeForm lf)
        {
            TimeSpan T = DateTime.Now - lf.BornTime;
            if ((T.TotalSeconds) > 30)
            {
                lf.IsAlive = false;
            }
        }

        public void RemoveBody(List<LifeForm> _Coloni, LifeForm lf)
        {
            if (lf.IsAlive == false) { _Coloni.Remove(lf); }
        }
    }
}
