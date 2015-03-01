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
        static Params Par;
        
        public System.Windows.Forms.PictureBox PictureBox1;
        public System.Windows.Forms.Timer LifeTime;
        public System.Windows.Forms.Timer GrowTime;

        public List<LifeForm> Coloni = new List<LifeForm>();
       
        public MainForm(Params _Par)
        {
            Par = _Par;
            this.Width_ = Par.Width;
            this.Hight_ = Par.Hight;
            
            
            //Moving Timer
            this.LifeTime = new System.Windows.Forms.Timer();
            this.LifeTime.Enabled = true;
            this.LifeTime.Interval = (int) (2000/Par.LifeSpeed);
            this.LifeTime.Tick += new System.EventHandler(this.LifeTime_Tick);

            //Growing Timer
            this.GrowTime = new System.Windows.Forms.Timer();
            this.GrowTime.Enabled = true;
            this.GrowTime.Interval = 1000;
            this.GrowTime.Tick += new System.EventHandler(this.GrowTime_Tick);

            // pictureBox1
            // 
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.PictureBox1.Location = new System.Drawing.Point(15, 15);
            this.PictureBox1.Name = "pictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(Par.Width, Par.Hight);
            //this.PictureBox1.TabIndex = 0;
            //this.PictureBox1.TabStop = false;
            // 
            // Form1
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(Width_ + 30 , Hight_ + 30);
            this.Controls.Add(this.PictureBox1);
            this.Name = "Evolution project";
            this.Text = "Evolution project";
            
        }

        public void LifeTime_Tick(Object sender, EventArgs e)
        {
            //start coloni
            if (Coloni.Count == 0)
            {
                for (int i = 0; i < Par.StartColoniCount; i++)
                {
                    this.Coloni.Add(new LifeForm(Coloni, Par));
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
                lf.Death(lf);
                DrawObject.DrawLf(lf, Par);
            }
            
            for (int i = 0; i < Coloni.Count; i++)
            {

                if (Coloni[i].IsPregnant == true)
                {
                    Coloni[i].Born(Coloni, Coloni[i]);
                }
                Coloni[i].RemoveBody(Coloni, Coloni[i]);
            }



        }

        public void GrowTime_Tick(Object sender, EventArgs e)
        {


        }

    }
}
