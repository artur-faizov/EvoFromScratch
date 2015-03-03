using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvoFromScratch
{
    public class Statistics
    {
        public int Iteration;

        public int ColoniCount;
        public int MaleCount;
        public int FemaleCount;
        public int DeadLfCount;

        public DateTime StartTime;
        public TimeSpan Uptime;


        public void UpdateUpTime ()
        {
             this.Uptime = DateTime.Now -this.StartTime;
        }

        public Statistics()
        {
            this.Iteration = 0;
            this.ColoniCount = 0;
            this.MaleCount = 0;
            this.FemaleCount = 0;
            this.DeadLfCount = 0;
            this.StartTime = DateTime.Now;
        }

        public void NewIteration()
        {
            this.Iteration++;
            this.ColoniCount = 0;
            this.MaleCount = 0;
            this.FemaleCount = 0;
            this.DeadLfCount = 0;
        }
        
        public void ColoniCount_plus(LifeForm lf)
        {
            this.ColoniCount++;
            if (lf.Gender == 'M') { this.MaleCount++; }
            else { this.FemaleCount++; }
        }

        public void ColoniCount_minus(LifeForm lf)
        {
            this.ColoniCount--;
            if (lf.Gender == 'M') 
                { this.MaleCount--; }
            else 
                { this.FemaleCount--; }
            this.DeadLfCount++;
        }
    }
}
