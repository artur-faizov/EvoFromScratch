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
        
        public System.Windows.Forms.PictureBox PictureBox1;
        public System.Windows.Forms.Timer LifeTime;
        public System.Windows.Forms.Timer GrowTime;

        public List<LifeForm> Coloni = new List<LifeForm>();

        public MainForm(int _Width, int _Hight, int _LifeSpeed)
        {
            this.Width_ = _Width;
            this.Hight_ = _Hight;
            
            
            //Moving Timer
            this.LifeTime = new System.Windows.Forms.Timer();
            this.LifeTime.Enabled = true;
            this.LifeTime.Interval = (int) (2000/_LifeSpeed);
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
            this.PictureBox1.Size = new System.Drawing.Size(_Width, _Hight);
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
            if (Coloni.Count == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    this.Coloni.Add(new LifeForm(Coloni, Width_, Hight_));
                } 
            }

            Draw DrawObject = new Draw(PictureBox1);
            foreach (LifeForm lf in Coloni)
            {
                lf.Death(Coloni, lf);
                if (lf.IsAlive == true)
                {
                    DrawObject.DrawLf(lf);
                    lf.Move();
                } else {
                    DrawObject.DrawDeath(lf);    
                }
            }
        }

        public void GrowTime_Tick(Object sender, EventArgs e)
        {
            for (int i = 0; i < Coloni.Count; i++)
            {
                Coloni[i].Search(Coloni, Coloni[i]);
                if (Coloni[i].IsPregnant == true)
                {
                    Coloni[i].Born(Coloni, Coloni[i]);
                }
                Coloni[i].Growing(Coloni[i]);
                Coloni[i].RemoveBody(Coloni, Coloni[i]);
            }
        }

    }
}
