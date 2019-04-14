using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemInternship2019.Model
{
    class ATM
    {
        public String name;
        public DateTime openingTime;
        public DateTime closingTime;
        public int duration;

        public ATM(String name, DateTime openingTime, DateTime closingTime, int durationStartPointtoAtm)
        {
            this.name = name;
            this.openingTime = openingTime;
            this.closingTime = closingTime;
            this.duration = durationStartPointtoAtm;

        }

        public String getName()
        {
            return this.name;
        }

        public DateTime getOpeningTime()
        {
            return this.openingTime;
        }

        public DateTime getClosingTime()
        {
            return this.closingTime;
        }

        public int getDuration()
        {
            return this.duration;
        }
        
    }
}
