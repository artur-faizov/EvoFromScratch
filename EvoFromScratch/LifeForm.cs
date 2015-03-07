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



        public void Move(List<LifeForm> Coloni)
        {
            foreach (LifeForm lf in Coloni)
            {
                if (lf.IsAlive == true)
                {
                    lf.OldLocX = lf.CurrentLocX;
                    lf.OldLocY = lf.CurrentLocY;
                    double NewX = lf.CurrentLocX + lf.MoveStep * Math.Sin(lf.MoveAngle);
                    double NewY = lf.CurrentLocY + lf.MoveStep * Math.Cos(lf.MoveAngle);
                    while ((NewX < 0) || (NewX > lf.Width) || (NewY < 0) || (NewY > lf.Hight))
                    {
                        lf.MoveAngle = (float)(rnd.Next(0, 360) * Math.PI / 180);
                        NewX = lf.CurrentLocX + lf.MoveStep * Math.Sin(lf.MoveAngle);
                        NewY = lf.CurrentLocY + lf.MoveStep * Math.Cos(lf.MoveAngle);
                    }
                    lf.CurrentLocX = (float)NewX;
                    lf.CurrentLocY = (float)NewY;
                }
            }
        }

        public void MoveAngleChangeToTarget(LifeForm Currentlf, List<LifeForm> Coloni)
        {
            double MoveAngleShouldBe;
            LifeForm Targetlf;
            if (Currentlf.TargetID > -1)
            {
                Targetlf = Coloni[Currentlf.TargetID];
            }
            else
            { return; }
            MoveAngleShouldBe =  Math.Atan2((Targetlf.CurrentLocX - Currentlf.CurrentLocX), (Targetlf.CurrentLocY - Currentlf.CurrentLocY));
            Currentlf.MoveAngle = (float)(MoveAngleShouldBe);
        }

        public void Born(List<LifeForm> _Coloni, Statistics Stat)
        {
            for (int i = 0; i < _Coloni.Count; i++)
            {
                LifeForm lf = _Coloni[i];
                TimeSpan T = DateTime.Now - lf.PregnantTime;
                if ((T.TotalSeconds) >= Par.PregnantePeriod && lf.IsPregnant == true)
                {
                    int ChildCount = rnd.Next(1, Par.BornAtOnce + 1);
                    for (int ii = 0; ii < ChildCount; ii++)
                    {
                        _Coloni.Add(new LifeForm(_Coloni, Par));
                        _Coloni[_Coloni.Count - 1].CurrentLocX = lf.CurrentLocX;
                        _Coloni[_Coloni.Count - 1].CurrentLocY = lf.CurrentLocY;

                        //update coloni statistic
                        Stat.ColoniCount_plus(_Coloni[_Coloni.Count - 1]);
                    }
                    lf.IsPregnant = false;
                    //return ChildCount;
                }
                //else return 0;
            }
        }

        public void Search(List<LifeForm> Coloni, Order Order)
        {
            foreach (LifeForm lf in Coloni)
            {
                Order.CheckTarget(lf, Coloni, Par);
                Order.GetTarget(lf, Coloni, Par);
                lf.MoveAngleChangeToTarget(lf, Coloni);
            }
        }

        public void Sex(List<LifeForm> Coloni)
        {
            foreach (LifeForm lf in Coloni)
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
                        //lf.MoveAngle = (float)(rnd.Next(0, 360) * Math.PI / 180);
                        //Coloni[lf.TargetID].MoveAngle = (float)(rnd.Next(0, 360) * Math.PI / 180);
                        DropTarget(Coloni[lf.TargetID]);
                        DropTarget(lf);
                    }
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

        public void Growing(List<LifeForm> Coloni)
        {
            foreach (LifeForm lf in Coloni)
            {
                TimeSpan T = DateTime.Now - lf.BornTime;
                lf.Age = (int)T.TotalSeconds;
            }
        }

        public void Death (List<LifeForm> Coloni, Statistics Stat)
        {
            foreach (LifeForm lf in Coloni)
            {
                TimeSpan T = DateTime.Now - lf.BornTime;
                if ((T.TotalSeconds) > Par.LifePeriod)
                {
                    if (lf.IsAlive == true) { Stat.ColoniCount_minus(lf); }
                    lf.IsAlive = false;

                }
            }
        }

        public void RemoveBody(List<LifeForm> _Coloni)
        {
            

            List<LifeForm> ToDeleteList = new List<LifeForm>();
            List<int> NewIDlist = new List<int>();
            int DeleteCounter = 0;
            for (int i = 0; i < _Coloni.Count; i++)
            {

                if (_Coloni[i].IsAlive == false)
                {
                    DeleteCounter++;
                    ToDeleteList.Add(_Coloni[i]);
                }
                NewIDlist.Add ( i - DeleteCounter);
            }

            if (DeleteCounter > 0)
            {
                List<LifeForm> I = _Coloni;

                foreach (LifeForm lf in ToDeleteList)
                {
                    _Coloni.Remove(lf);
                }

                for (int i = 0; i < _Coloni.Count; i++)
                {
                    if (_Coloni[i].TargetID > -1)
                    {
                        //if (_Coloni[i].TargetID >= _Coloni.Count) 
                        //{ 
                        //    int x  = _Coloni[i].TargetID;
                        //    _Coloni[i].TargetID = NewIDlist[_Coloni[i].TargetID];

                        //}
                        //if (NewIDlist[_Coloni[i].TargetID] == -1)
                        //{
                            //DropTarget(_Coloni[i]);
                        //}
                        //else
                        //{
                        _Coloni[i].ID = NewIDlist[i];    
                        _Coloni[i].TargetID = NewIDlist[_Coloni[i].TargetID];
                        //}
                        if (_Coloni[i].TargetID >= _Coloni.Count)
                        { int Achtung; }
                    }
                }
                List<int> M = NewIDlist;
                List<LifeForm> Y = ToDeleteList;
                List<LifeForm> C = _Coloni;
            }

            //for (int i = 0; i < _Coloni.Count; i++)
            //{
            //    if (_Coloni[i].IsAlive == false)
            //    {
            //        _Coloni.Remove(_Coloni[i]);
            //        i--;
            //    }
            //}
        }
    }
}
