using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProblemInternship2019.Model;

namespace ProblemInternship2019.Business
{
    class ModelLogic
    {
        public ModelLogic()
        {

        }

        public List<Card> verifyExpirationDate(List<Card> cards, DateTime date)
        {
            List<Card> modifiedcards = new List<Card>();

            foreach (Card item in cards)
            {
                int comparison = DateTime.Compare(item.getExpirationDate(), date);
                if (comparison > 0)
                    modifiedcards.Add(item);
            }

            return modifiedcards;
        }

        public int sumfor1ATM (int sum, int atmMax)
        {
            int money_extract = 0;

            if (sum < atmMax)
                money_extract = sum;
            else
                money_extract = atmMax;

            return money_extract;
        }

        public List<Card> cardsuse1ATM (List<Card> cards, int sum)
        {

            var cardsByFee = cards.OrderBy(f => f.fee);
            int money = 0;

            List<Card> cardsmodify = new List<Card>();

            var firstElement = cardsByFee.First();

            foreach(Card item in cardsByFee)
            {                
                if (money == sum)
                    break;
                if(item.getavailableAmount() > 0 && item.getwithdrawLimit() > 0)
                {
                    if(sum - money <= item.getavailableAmount())
                    {
                        if(sum - money <= item.getwithdrawLimit())
                        {
                            Console.WriteLine("Extract from "+ item.getType() + " card, amount of " + (sum - money));
                            item.setavailableAmount(item.getavailableAmount() - (sum - money));
                            item.setwithdrawLimit(item.getwithdrawLimit() - (sum - money));
                            money = money + (sum - money);
                                                     
                        }
                        else
                        {
                            if(item.getwithdrawLimit() < sum - money)
                            {
                                Console.WriteLine("Extract from " + item.getType() + " card, amount of " + item.getwithdrawLimit());
                                item.setavailableAmount(item.getavailableAmount() - item.getwithdrawLimit());
                                money = money + item.getwithdrawLimit();
                                item.setwithdrawLimit(0);
                            }
                            else
                            {
                                money = money + (sum - money);
                            }
                            
                            
                        }

                    }
                    else
                    {
                        if(item.getavailableAmount() <= item.getwithdrawLimit())
                        {
                            Console.WriteLine("Extract from " + item.getType() + " card, amount of " + item.getavailableAmount());
                            item.setwithdrawLimit(item.getwithdrawLimit() - item.getavailableAmount());
                            money = money + item.getavailableAmount();
                            item.setavailableAmount(0);                  
                            
                            
                        }
                        else
                        {
                            if(item.getwithdrawLimit() < sum - money)
                            {
                                Console.WriteLine("A scos de pe cardul " + item.getType() + " suma de " + item.getwithdrawLimit());
                                item.setavailableAmount(item.getavailableAmount() - item.getwithdrawLimit());
                                money = money + item.getwithdrawLimit();
                                item.setwithdrawLimit(0);
                            }
                            else
                            {
                                money = money + (sum - money);
                            }

                            
                            
                        }
                    }

                    cardsmodify.Add(item);
                }                
            }

            return cardsmodify;            
        }                               

        public List<ATM> checkavailableATMs(List<ATM> atms, DateTime hour)
        {
            List<ATM> availableATMs = new List<ATM>();

            foreach(ATM item in atms)
            {
                int comparison1 = TimeSpan.Compare(item.getOpeningTime().TimeOfDay, hour.TimeOfDay);
                int comparison2 = TimeSpan.Compare(hour.TimeOfDay, item.getClosingTime().TimeOfDay);               

                if (comparison1 < 0 && comparison2 < 0)
                    availableATMs.Add(item);              
                    
            }

            return availableATMs;
        }

        public int numberATMsneeded(int sum, int atmMax)
        {
            int a = 0;
            int number = 0;
            while(a < sum)
            {
                a = a + atmMax;
                number++;
            }

            return number;

        }

        public void moneyATM(List<Card> cards, int sum, int atmMax)
        {
            int numberAtm = this.numberATMsneeded(sum, atmMax);

            
            for (int i = 0; i < numberAtm; i++)
            {
                if(sum > 0)
                {
                    int money = this.sumfor1ATM(sum, atmMax);
                    List<Card> updatedCards = this.cardsuse1ATM(cards, money);
                    Console.WriteLine("Extracts from an ATM");
                    Console.WriteLine();
                    sum = sum - atmMax;
                }
            }        

        }
        

        public List<List<ATM>> combo(List<ATM> atms)
        {        

            int count = (int) Math.Pow(2, atms.Count) - 1;

            List<List<ATM>> combo = new List<List<ATM>>();

            for (int i = 1; i<= count + 1; i++)
            {
                combo.Add(new List<ATM>());
                for (int j = 0; j < atms.Count; j++)
                {
                    if ((i >> j) % 2 != 0)
                        combo.Last().Add(atms[j]);
                }
            }

            return combo;    
        }

        public List<List<ATM>> allcomb(List<List<ATM>> atms, int nr)
        {
            List<List<ATM>> newATMs = new List<List<ATM>>();
            foreach (List<ATM> item_atms in atms)
            {
                if(item_atms.Count == nr)
                {
                    foreach(ATM item in item_atms)
                    {                                              
                        for (int i = 0; i < item_atms.Count; i++)
                        {
                            if(item != item_atms[i])
                            {
                                List<ATM> aux = new List<ATM>();
                                aux.Add(item);
                                aux.Add(item_atms[i]);
                                newATMs.Add(aux);
                            }
                           
                        }
                    }
                }
            }
            return newATMs;
            
        }


        public Dictionary<int, List<ATM>> routeATMSwithTime(List<ATM> atms, List<Walk> walks, DateTime startTime, int sum, int atmMax)
        {
            int numberAtmsneeded = this.numberATMsneeded(sum, atmMax);

            Dictionary<int, List<ATM>> routetime = new Dictionary<int, List<ATM>>();

            List<List<ATM>> combo = this.combo(atms);
            List<List<ATM>> allcombo = this.allcomb(combo, numberAtmsneeded);

            foreach(List<ATM> list in allcombo)
            {
                DateTime currentTime = startTime;
                int time = 0;
                for(int i = 0; i< list.Count - 1; i++)
                {
                    if(i == 0)
                    {
                        time = time + list[i].getDuration();
                        currentTime.AddMinutes(list[i].getDuration());
                    }                          
                   

                    int comparation = TimeSpan.Compare(list[i].getOpeningTime().TimeOfDay, currentTime.TimeOfDay);
                    if(comparation > 0)
                    {
                        TimeSpan dif = list[i].getOpeningTime().TimeOfDay.Subtract(currentTime.TimeOfDay);
                        time = time + (int)dif.TotalMinutes;
                        currentTime.AddMinutes((int)dif.TotalMinutes);
                    }

                    foreach (Walk item_walk in walks)
                    {
                        if (item_walk.getFrom() == list[i] && item_walk.getTo() == list[i + 1] || item_walk.getTo() == list[i] && item_walk.getFrom() == list[i + 1])
                        {
                            time = time + item_walk.getDuration();
                            currentTime.AddMinutes(item_walk.getDuration());
                        }
                    }
                }

                routetime[time] = list;
            }

            return routetime;

        }
       
        
        public List<ATM> getAtmsRoute(Dictionary<int, List<ATM>> routetime)
        {
            List<ATM> atms = new List<ATM>();

            var orderTime = routetime.OrderBy(key => key.Key);

            var sort = orderTime.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);

            atms = sort.First().Value;

            Console.WriteLine("Min time is: " + sort.First().Key + " minutes");

            return atms;
        }
    }
}
