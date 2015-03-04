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
        public int TargetID;
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
        public int ViewDistance;
        public static Params Par;

        int Width;
        int Hight;

        static Random rnd = new Random();

        public LifeForm(List<LifeForm> _Coloni, Params _Par)
        {
            this.Width = _Par.Width;
            this.Hight = _Par.Hight;
            this.ID = _Coloni.Count;
            this.Age = 1;
            this.CurrentLocX = rnd.Next(0, Width);
            this.CurrentLocY = rnd.Next(0, Hight);
            this.TargetID = -1;
            this.MoveAngle = (float)(rnd.Next(0, 360) * Math.PI / 180);
            this.MoveStep = 20/rnd.Next(5, 10);
            this.BornTime = DateTime.Now;
            this.PregnantTime = DateTime.Now;
            this.IsPregnant = false;
            this.IsAlive = true;
            char[] _gender = new char[2] { 'M', 'F' };
            this.Gender = (char)_gender[rnd.Next(0,2)];
            this.ViewDistance = rnd.Next(50, 70);
            Par = _Par;
        }



        public void Move()
        {
            this.OldLocX = this.CurrentLocX;
            this.OldLocY = this.CurrentLocY;
            double NewX = this.CurrentLocX + this.MoveStep * Math.Sin(this.MoveAngle);
            double NewY = this.CurrentLocY + this.MoveStep * Math.Cos(this.MoveAngle);
            while ((NewX < 0) || (NewX > this.Width) || (NewY < 0) || (NewY > this.Hight))
            {
                this.MoveAngle = (float)(rnd.Next(0, 360) * Math.PI / 180);
                NewX = this.CurrentLocX + this.MoveStep * Math.Sin(this.MoveAngle);
                NewY = this.CurrentLocY + this.MoveStep * Math.Cos(this.MoveAngle);
            }
                this.CurrentLocX = (float)NewX;
                this.CurrentLocY = (float)NewY;
        }

        public void MoveAngleChangeToTarget(LifeForm Currentlf, LifeForm Targetlf)
        {
            double MoveAngleShouldBe;
            MoveAngleShouldBe =  Math.Atan2((Targetlf.CurrentLocX - Currentlf.CurrentLocX), (Targetlf.CurrentLocY - Currentlf.CurrentLocY));
            Currentlf.MoveAngle = (float)(MoveAngleShouldBe);
        }

        public int Born(List<LifeForm> _Coloni, LifeForm lf, Statistics Stat)
        {
            TimeSpan T = DateTime.Now - lf.PregnantTime;
            if ((T.TotalSeconds) >= Par.PregnantePeriod && lf.IsPregnant == true)
            {
                int ChildCount = rnd.Next(1, Par.BornAtOnce + 1);
                for (int i = 0; i < ChildCount; i++)
                {
                    _Coloni.Add(new LifeForm(_Coloni, Par));
                    _Coloni[_Coloni.Count - 1].CurrentLocX = lf.CurrentLocX;
                    _Coloni[_Coloni.Count - 1].CurrentLocY = lf.CurrentLocY;

                    //update coloni statistic
                    Stat.ColoniCount_plus(_Coloni[_Coloni.Count - 1]);
                }
                lf.IsPregnant = false;
                return ChildCount;
            }
            else return 0;
        }

        public void Search(List<LifeForm> Coloni, LifeForm lf, Order Order)
        {
            if (lf.TargetID >= Coloni.Count)
            {
                DropTarget(lf);
            }
            if (lf.TargetID > -1 && 
                lf.Distance(lf, Coloni[lf.TargetID]) < lf.ViewDistance &&
                Coloni[lf.TargetID].IsPregnant != true &&
                Coloni[lf.TargetID].IsAlive == true)
            {
                    lf.MoveAngleChangeToTarget(lf, Coloni[lf.TargetID]);
            }
            else if (lf.TargetID > -1)
            {
                    DropTarget(lf);
            }
            if (lf.TargetID < 0)
            {
                Order.GetTarget(lf, Par);
            }
            
/*            if (lf.Age > Par.ChildAge && 
                lf.IsPregnant != true && 
                lf.IsAlive == true &&
                lf.TargetID  == -1)
            {
                foreach (LifeForm lf2 in Coloni)
                {
                    if (lf2.Age > Par.ChildAge && 
                        lf.ID != lf2.ID && 
                        lf2.IsPregnant != true && 
                        lf2.IsAlive == true && 
                        lf.Gender != lf2.Gender)
                    {
                        if (lf.Distance(lf,lf2) < lf.ViewDistance)
                        {
                            lf.TargetID = lf2.ID;
                            lf.MoveAngleChangeToTarget(lf, Coloni[lf.TargetID]);
                            break;
                        }
                    }
                }
            } */  
        }

        public void Sex(List<LifeForm> Coloni, LifeForm lf)
        {
            if (lf.TargetID >= Coloni.Count) 
            {
                lf.DropTarget(lf);
            }
            if (lf.TargetID > -1)
            {
                
                if (lf.Distance(lf, Coloni[lf.TargetID]) < Par.SexDistance)
                {
                    if (lf.Gender == 'F')
                    {
                        lf.IsPregnant = true;
                        lf.PregnantTime = DateTime.Now;
                    }
                    else
                    {
                        Coloni[lf.TargetID].IsPregnant = true;
                        Coloni[lf.TargetID].PregnantTime = DateTime.Now;
                    }
                    lf.MoveAngle = (float)(rnd.Next(0, 360) * Math.PI / 180);
                    Coloni[lf.TargetID].MoveAngle = (float)(rnd.Next(0, 360) * Math.PI / 180);
                    DropTarget(Coloni[lf.TargetID]);
                    DropTarget(lf);
                }
            }
        }

        public void DropTarget (LifeForm lf)
        {
            lf.TargetID = -1;
            lf.MoveAngle = (float)(rnd.Next(0, 360) * Math.PI / 180);
        }

        public float Distance(LifeForm lf1, LifeForm lf2)
        {
            float x = Math.Abs (lf1.CurrentLocX - lf2.CurrentLocX);
            float y = Math.Abs (lf1.CurrentLocY - lf2.CurrentLocY);
            float Distance = (float)(Math.Sqrt(x*x + y*y));
            return(Distance);
        }

        public void Growing(LifeForm lf)
        {
            TimeSpan T = DateTime.Now - lf.BornTime;
            lf.Age = (int)T.TotalSeconds;
        }

        public void Death (LifeForm lf, Statistics Stat)
        {
            TimeSpan T = DateTime.Now - lf.BornTime;
            if ((T.TotalSeconds) > Par.LifePeriod)
            {
                if (lf.IsAlive == true) { Stat.ColoniCount_minus(lf); }
                lf.IsAlive = false;
                
            }
        }

        public void RemoveBody(List<LifeForm> _Coloni, LifeForm lf)
        {
            if (lf.IsAlive == false) { _Coloni.Remove(lf); }
        }
    }
}
