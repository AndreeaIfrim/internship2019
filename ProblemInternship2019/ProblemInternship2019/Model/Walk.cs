using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemInternship2019.Model
{
    class Walk
    {
        public ATM from;
        public ATM to;
        public int duration;        

        public Walk(ATM from, ATM to, int duration)
        {
            this.from = from;
            this.to = to;
            this.duration = duration;            
        }

        public ATM getFrom()
        {
            return this.from;
        }
        public ATM getTo()
        {
            return this.to;
        }
        public int getDuration()
        {
            return this.duration;
        }
    }
}
