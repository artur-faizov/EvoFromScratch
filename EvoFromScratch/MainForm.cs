using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace EvoFromScratch
{
    public class MainForm : Form
    {
        public int Width_;
        public int Hight_;
        public int LifeSpeed;

        public int StatisticLocY;
        static Params Par;
        
        public System.Windows.Forms.PictureBox PictureBox1;
        public System.Windows.Forms.Timer LifeTime;
        public System.Windows.Forms.Timer GrowTime;

        public List<LifeForm> Coloni = new List<LifeForm>();

        public TextBox StatisticBox_UpTime_Data;
        public TextBox StatisticBox_Iteration_Data;
        public TextBox StatisticBox_ColoniCount_Data;
        public TextBox StatisticBox_MaleCount_Data;
        public TextBox StatisticBox_FemaleCount_Data;
        public TextBox StatisticBox_DeathCount_Data;

        public Statistics Stat;
       
        public MainForm(Params _Par)
        {
            Par = _Par;
            this.Width_ = Par.Width;
            this.Hight_ = Par.Hight;
            this.StatisticLocY = 5;
            
            //Moving Timer
            this.LifeTime = new System.Windows.Forms.Timer();
            this.LifeTime.Enabled = true;
            this.LifeTime.Interval = (int) (2000/Par.LifeSpeed);
            this.LifeTime.Tick += new System.EventHandler(this.LifeTime_Tick);

            //Growing Timer
            this.GrowTime = new System.Windows.Forms.Timer();
            this.GrowTime.Enabled = true;
            this.GrowTime.Interval = 100;
            this.GrowTime.Tick += new System.EventHandler(this.GrowTime_Tick);

            // pictureBox1
            // 
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.PictureBox1.Location = new System.Drawing.Point(5, 5);
            this.PictureBox1.Name = "pictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(Par.Width, Par.Hight);

            this.Name = "Evolution project";
            this.Text = "Evolution project";
            this.ClientSize = new System.Drawing.Size(Width_ + 10, Hight_ + 10);
            this.Controls.Add(this.PictureBox1);


            FormatTextBox f = new FormatTextBox(this);
            StatisticBox_UpTime_Data = f.format("Uptime:");
            StatisticBox_Iteration_Data = f.format("Iteration:");
            StatisticBox_ColoniCount_Data = f.format("ColoniCount:");
            StatisticBox_MaleCount_Data = f.format("MaleCount:");
            StatisticBox_FemaleCount_Data = f.format("FemaleCount:");
            StatisticBox_DeathCount_Data = f.format("DeathCount:");

            this.ActiveControl = this.PictureBox1;

            //Statistics Class object
            this.Stat = new Statistics();   
        }

        public void LifeTime_Tick(Object sender, EventArgs e)
        {
            //start coloni
            if (Coloni.Count == 0)
            {
                Stat.NewIteration();
                for (int i = 0; i < Par.StartColoniCount; i++)
                {
                    this.Coloni.Add(new LifeForm(Coloni, Par));
                    Stat.ColoniCount_plus(Coloni[i]);
                } 
            }

            Draw DrawObject = new Draw(PictureBox1);

            foreach (LifeForm lf in Coloni)
            {
                lf.Search(Coloni, lf);
                lf.Sex(Coloni,lf);
            }

            foreach (LifeForm lf in Coloni)
            {
                lf.Move();
                lf.Growing(lf);
                lf.Death(lf, Stat);
                DrawObject.DrawLf(lf, Par);
            }
            
            for (int i = 0; i < Coloni.Count; i++)
            {

                if (Coloni[i].IsPregnant == true)
                {
                    Coloni[i].Born(Coloni, Coloni[i], Stat);
                }
                Coloni[i].RemoveBody(Coloni, Coloni[i]);
            }

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

    }
}
