using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemInternship2019.Model
{
    class Card
    {
        public String type;
        public double fee;
        public int withdrawLimit;
        public DateTime expirationDate;
        public int availableAmount;

        public Card(String type, double fee, int withdrawLimit, DateTime expirationDate, int availableAmount)
        {
            this.type = type;
            this.fee = fee;
            this.withdrawLimit = withdrawLimit;
            this.expirationDate = expirationDate;
            this.availableAmount = availableAmount;
        }

        public DateTime getExpirationDate()
        {
            return this.expirationDate;
        }

        public String getType()
        {
            return this.type;
        }

        public double getFee()
        {
            return this.fee;
        }

        public int getwithdrawLimit()
        {
            return this.withdrawLimit;
        }

        public void setwithdrawLimit(int limit)
        {
            this.withdrawLimit = limit;
        }

        public int getavailableAmount()
        {
            return this.availableAmount;
        }

        public void setavailableAmount(int newavailableAmount)
        {
            this.availableAmount = newavailableAmount;
        }
    }
}
