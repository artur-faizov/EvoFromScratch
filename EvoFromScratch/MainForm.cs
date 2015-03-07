using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace EvoFromScratch
{
    public partial class MainForm : Form
    {
        public MainForm(Params _Par)
        {
            this.Construct(_Par);
        }

        public void LifeTime_Tick(Object sender, EventArgs e)
        {
            LifeForm lf = new LifeForm (Coloni, Par); 
                       
            this.NewColoni(Coloni, CurrentOrder, Par, Stat);           //create start coloni if coloni.count == 0
            CurrentOrder.UpdateOrder(Coloni); //<<

            lf.Growing(Coloni);                  //growing for everybody
            lf.Search(Coloni, CurrentOrder);     //search target to move
            lf.Move(Coloni);
            CurrentOrder.UpdateOrder(Coloni); //<<

            lf.Sex(Coloni);
            lf.Death(Coloni, Stat);
            DrawObject.DrawLf(Coloni, Par);
            lf.RemoveBody(Coloni);
            CurrentOrder.UpdateOrder(Coloni); //<<
            lf.Born(Coloni, Stat);
        }

        public void GrowTime_Tick(Object sender, EventArgs e)
        {
            Stat.UpdateUpTime();
            this.StatisticBox_UpTime_Data.Text = Stat.Uptime.Hours.ToString() + '.' + Stat.Uptime.Minutes.ToString() + '.' + Stat.Uptime.Seconds.ToString();
            this.StatisticBox_Iteration_Data.Text = Stat.Iteration.ToString();
            this.StatisticBox_ColoniCount_Data.Text = Stat.ColoniCount.ToString();
            this.StatisticBox_MaleCount_Data.Text = Stat.MaleCount.ToString();
            this.StatisticBox_FemaleCount_Data.Text = Stat.FemaleCount.ToString();
            this.StatisticBox_DeathCount_Data.Text = Stat.DeadLfCount.ToString();
        }

        public void NewColoni(List<LifeForm> Coloni, Order Order, Params _Par, Statistics Stat)
        {
            if (Coloni.Count == 0)
            {
                Stat.NewIteration();
                for (int i = 0; i < Par.StartColoniCount; i++)
                {
                    Coloni.Add(new LifeForm(Coloni, Par));
                    Stat.ColoniCount_plus(Coloni[i]);
                }

                Order.UpdateOrder(Coloni);

            }
        }

    }
}
