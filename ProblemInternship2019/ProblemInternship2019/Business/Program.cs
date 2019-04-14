using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProblemInternship2019.Model;

namespace ProblemInternship2019.Business
{
    class Program
    {
        static void Main(string[] args)
        {
            Card silver = new Card("silver", 0.2, 4500, new DateTime(2020, 5, 23, 0, 0, 0), 20000);
            Card gold = new Card("gold", 0.1, 3000, new DateTime(2018, 8, 15, 0, 0, 0), 25000);
            Card platinum = new Card("platinum", 0, 4000, new DateTime(2019, 3, 20, 0, 0, 0), 3000);

            List<Card> cards = new List<Card>();
            cards.Add(silver);
            cards.Add(gold);
            cards.Add(platinum);

            ATM atm1 = new ATM("ATM1", new DateTime(2019, 3, 19, 12, 0, 0), new DateTime(2019, 3, 19, 18, 0, 0), 5);
            ATM atm2 = new ATM("ATM2", new DateTime(2019, 3, 19, 10, 0, 0), new DateTime(2019, 3, 19, 17, 0, 0), 60);
            ATM atm3 = new ATM("ATM3", new DateTime(2019, 3, 19, 22, 0, 0), new DateTime(2019, 3, 19, 12, 0, 0), 30);
            ATM atm4 = new ATM("ATM4", new DateTime(2019, 3, 19, 17, 0, 0), new DateTime(2019, 3, 19, 1, 0, 0), 45);            

            List<ATM> atms = new List<ATM>();
            atms.Add(atm1);
            atms.Add(atm2);
            atms.Add(atm3);
            atms.Add(atm4);          

            Walk w1 = new Walk(atm1, atm2, 40);
            Walk w2 = new Walk(atm1, atm4, 45);
            Walk w3 = new Walk(atm2, atm3, 15);
            Walk w4 = new Walk(atm3, atm1, 40);
            Walk w5 = new Walk(atm3, atm4, 15);
            Walk w6 = new Walk(atm4, atm2, 30);
        

            List<Walk> walks = new List<Walk>();
            walks.Add(w1);
            walks.Add(w2);
            walks.Add(w3);
            walks.Add(w4);
            walks.Add(w5);
            walks.Add(w6);
           
            ModelLogic ml = new ModelLogic();

          
            DateTime startTime = new DateTime(2019, 3, 19, 11, 30, 0);
            Console.WriteLine("StartTime - Day: {0:d} Time: {1:g}", startTime.Date, startTime.TimeOfDay);
            DateTime finishTime = new DateTime(2019, 3, 19, 14, 0, 0);
            Console.WriteLine("FinishTime - Day: {0:d} Time: {1:g}", finishTime.Date, finishTime.TimeOfDay);

            int amountToExtract = 7500;
            Console.WriteLine("Amount to extract: "+ amountToExtract);
            int atmLimit = 5000;
            Console.WriteLine("ATM limit: " + atmLimit);



            List<ATM> availableATMs = ml.checkavailableATMs(atms, finishTime);                                   
                                                      
            List<Card> modifiedcards = ml.verifyExpirationDate(cards, startTime);            
            
            Console.WriteLine();        
            

            Dictionary<int, List<ATM>> routetime = ml.routeATMSwithTime(availableATMs, walks, startTime, amountToExtract, atmLimit);           

            List<ATM> getAtmsRoute = ml.getAtmsRoute(routetime);

            Console.WriteLine("The best route is:");

            foreach(ATM item in getAtmsRoute)
            {
                Console.WriteLine(item.getName());
            }

            Console.WriteLine();

            ml.moneyATM(modifiedcards, amountToExtract, atmLimit);     

            Console.ReadLine();

        }
    }
}
