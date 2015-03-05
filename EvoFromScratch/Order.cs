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
            this.OrderedColoniX = OriginColoni.OrderBy(lf => lf.CurrentLocX).ToList();
            this.OrderedColoniY = OriginColoni.OrderBy(lf => lf.CurrentLocY).ToList();
        }

        public void ShowOrder ()
        {
            string ListOfElements = "";
            for (int i = 0; i < OrderedColoniX.Count; i++)
            {
                ListOfElements += i + " :" + OrderedColoniX[i].CurrentLocX + '\n';
            }
            MessageBox.Show(ListOfElements);
        }


        public bool CheckTarget(LifeForm lf1, List<LifeForm> Coloni,Params Par)
        {
            if (lf1.Age > Par.ChildAge &&
                lf1.IsPregnant != true &&
                lf1.IsAlive == true &&
                lf1.TargetID != -1 &&
                lf1.TargetID < Coloni.Count)
            {
                if (lf1.Distance (lf1, Coloni[lf1.TargetID]) < lf1.ViewDistance)
                { return true; }
                else
                { return false ;}
            }
            else
            { return false; }
        }
        public bool CheckAtibility(LifeForm lf1, List<LifeForm> Coloni, Params Par)
        {
            if (lf1.Age > Par.ChildAge &&
                lf1.IsPregnant != true &&
                lf1.IsAlive == true &&
                lf1.TargetID == -1)
            { return true; }
            else if (lf1.TargetID > -1 && lf1.Distance(lf1, Coloni[lf1.TargetID]) > lf1.ViewDistance)
            { lf1.TargetID = -1; return true; }
            else
            { return false; }
        }
        public bool CheckCompatibility (LifeForm lf1, LifeForm lf2, Params Par)
        {
            if (/*lf1.Age > Par.ChildAge &&
                lf1.IsPregnant != true &&
                lf1.IsAlive == true &&
                lf1.TargetID == -1 &&*/
                lf2.Age > Par.ChildAge &&
                lf1.ID != lf2.ID &&
                lf2.IsPregnant != true &&
                lf2.IsAlive == true &&
                lf1.Gender != lf2.Gender)
                    {return true;}
            else
                    {return false;}    
        }
        public bool ChekNeibours(LifeForm lf1,int i_InOrderX, int i_InOrderY, int Delta, Params Par)
        {
            List<LifeForm> NeibourList = new List<LifeForm>();

            if (i_InOrderX - Delta > 0)
            { NeibourList.Add(OrderedColoniX[i_InOrderX - Delta]); }
            if (i_InOrderX + Delta < OrderedColoniX.Count)
            { NeibourList.Add(OrderedColoniX[i_InOrderX + Delta]); }
            if (i_InOrderY - Delta > 0)
            { NeibourList.Add(OrderedColoniY[i_InOrderY - Delta]); }
            if (i_InOrderY + Delta < OrderedColoniY.Count)
            { NeibourList.Add(OrderedColoniY[i_InOrderY + Delta]); }

            float distance = -1;

            foreach (LifeForm lf in NeibourList)
            {
                if (CheckCompatibility (lf1, lf, Par) == true)
                {
                    if (distance == -1)
                    {
                        distance = lf1.Distance(lf1,lf);
                        if (distance < lf1.ViewDistance)
                            { lf1.TargetID = lf.ID; }
                    } else if (lf1.Distance(lf1,lf) < distance)
                    {
                        distance = lf1.Distance(lf1,lf);
                        if (distance < lf1.ViewDistance)
                            { lf1.TargetID = lf.ID; }
                    }
                }
            }
            if (distance == -1) {return false;}
            else {return true;}

        }
        public void GetTarget (LifeForm lf, List<LifeForm> Coloni, Params Par)
        {
            if (CheckAtibility(lf, Coloni, Par) == true)
            {

                int i_InOrderX = -1;
                int i_InOrderY = -1;

                for (int i = 0; i < OrderedColoniX.Count; i++)
                {
                    if (lf.ID == OrderedColoniX[i].ID) { i_InOrderX = i; }
                    if (lf.ID == OrderedColoniY[i].ID) { i_InOrderY = i; }
                }

                int delta = 1;
                while (ChekNeibours(lf, i_InOrderX, i_InOrderY, delta, Par) != true) 
                { 
                    delta++;
                    if (delta > 1) { break; }
                }
            }
        }
    }
}
