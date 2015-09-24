using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthstoneCardStats
{
    public class CardStats
    {
        Card card;
        public int drawn;
        public int played;
        public int won;
        public int loss;
        public int damageDealt;
        public int damageRecieved;

        public string CardName {
            get { return card.Name; }
        }

        public double PlayedPercentage
        {
            get {
                return GetPercentage(played, drawn);
            }
        }

        public double WinPercentage
        {
            get {
                return GetPercentage(won, played);
            }
        }

        private double GetPercentage(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                return 0;
            }
            double percentage = Math.Round(((double)numerator / (double)denominator) * 100, 2);
            return percentage; 
        }

        public CardStats(string id)
        {
            card = Database.GetCardFromId(id);
            drawn = 0;
            played = 0;
            won = 0;
            loss = 0;
        }

        public void IncrementPlay(bool result)
        {
            played++;
            if (result)
            {
                won++;
            }
            else
            {
                loss++;
            }
        }

        public void IncrementDrawn()
        {
            drawn++;
        }

        public void IncrementDamage()
        {

        }
    }
}
