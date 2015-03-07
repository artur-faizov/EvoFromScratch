using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EvoFromScratch
{
    public class Order
    {
        public List<LifeForm> OrderedColoniX;
        public List<LifeForm> OrderedColoniY;
        public Order(List<LifeForm> OriginColoni)
        {
            OrderedColoniX = OriginColoni.OrderBy(lf => lf.CurrentLocX).ToList();
            OrderedColoniY = OriginColoni.OrderBy(lf => lf.CurrentLocY).ToList();
        }

        public void UpdateOrder(List<LifeForm> OriginColoni)
        {
            this.OrderedColoniX.RemoveRange(0, this.OrderedColoniX.Count);
            this.OrderedColoniX = OriginColoni.OrderBy(lf => lf.CurrentLocX).ToList();
            this.OrderedColoniY.RemoveRange(0, this.OrderedColoniY.Count);
            this.OrderedColoniY = OriginColoni.OrderBy(lf => lf.CurrentLocY).ToList();
        }

        //public void ShowOrder ()
        //{
        //    string ListOfElements = "";
        //    for (int i = 0; i < OrderedColoniX.Count; i++)
        //    {
        //        ListOfElements += i + " :" + OrderedColoniX[i].CurrentLocX + '\n';
        //    }
        //    MessageBox.Show(ListOfElements);
        //}


        public bool CheckDistance(LifeForm lf1, List<LifeForm> Coloni,Params Par)
        {
            if (lf1.TargetID > -1 )
            {
                if (lf1.Distance (lf1, Coloni[lf1.TargetID]) < lf1.ViewDistance)
                { return true; }
                else
                { lf1.TargetID = -1;  return false; }
            }
            else
            { return false; }
        }
        public bool CheckNeedNewTarget(LifeForm lf1, Params Par)
        {
            if (lf1.Age > Par.ChildAge &&
                lf1.IsPregnant != true &&
                lf1.IsAlive == true &&
                lf1.TargetID == -1 )
            { return true; }
            else
            { return false; }
        }

        public bool CheckNeedNewAngle(LifeForm lf1, Params Par)
        {
            if (lf1.Age > Par.ChildAge &&
                lf1.IsPregnant != true &&
                lf1.IsAlive == true &&
                lf1.TargetID > -1)
            { return true; }
            else
            { return false; }
        }

        public bool CheckCompatibility (LifeForm lf1, LifeForm lf2, Params Par)
        {
            if (lf1.Age > Par.ChildAge &&
                lf1.IsPregnant != true &&
                lf1.IsAlive == true &&
                lf1.TargetID == -1 &&
                lf2.Age > Par.ChildAge &&
                lf1.ID != lf2.ID &&
                lf2.IsPregnant != true &&
                lf2.IsAlive == true &&
                lf1.Gender != lf2.Gender)
                    {return true;}
            else
                    {return false;}    
        }
        
        public void GetTarget (LifeForm lf, List<LifeForm> Coloni, Params Par)
        {         
            if (CheckNeedNewTarget(lf, Par) == true)
            {
                int i_InOrderX = -1;
                int i_InOrderY = -1;

                for (int i = 0; i < OrderedColoniX.Count; i++)
                {
                    if (lf.ID == OrderedColoniX[i].ID) { i_InOrderX = i; }
                    if (lf.ID == OrderedColoniY[i].ID) { i_InOrderY = i; }
                    if (i_InOrderX > -1 && i_InOrderY > -1) { break; }
                }

                GetTargetFromNeibours(lf, i_InOrderX, i_InOrderY, 1, Par);

                if (lf.TargetID >= OrderedColoniX.Count)
                {
                    int ahtung;
                }
            }
        }
        public bool GetTargetFromNeibours(LifeForm lf1, int i_InOrderX, int i_InOrderY, int Delta, Params Par)
        {
            List<LifeForm> NeibourList = new List<LifeForm>();

            if (i_InOrderX - Delta > -1)
            {  NeibourList.Add(OrderedColoniX[i_InOrderX - Delta]); }
            if (i_InOrderX + Delta < OrderedColoniX.Count)
            { NeibourList.Add(OrderedColoniX[i_InOrderX + Delta]); }
            if (i_InOrderY - Delta > -1)
            { NeibourList.Add(OrderedColoniY[i_InOrderY - Delta]); }
            if (i_InOrderY + Delta < OrderedColoniY.Count)
            { NeibourList.Add(OrderedColoniY[i_InOrderY + Delta]); }

            float distance = -1;

            foreach (LifeForm lf2 in NeibourList)
            {
                if (CheckCompatibility(lf1, lf2, Par) == true)
                {
                    if (lf1.Distance(lf1, lf2) < lf1.ViewDistance)
                    {
                        if (distance == -1)
                        { distance = lf1.Distance(lf1, lf2); lf1.TargetID = lf2.ID; }
                        else if (distance > lf1.Distance(lf1, lf2))
                        { distance = lf1.Distance(lf1, lf2); lf1.TargetID = lf2.ID; }
                    }
                }
            }
            if (distance == -1) { return false; }
            else { return true; }

        }
    }
}
