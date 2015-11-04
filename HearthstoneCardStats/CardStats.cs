using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

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
        public int turnsOnBoard;

        public string CardName {
            get { return card.Name; }
        }

        public double PlayedPercentage
        {
            get {
                return GetDecimal(played, drawn) * 100;
            }
        }

        public double WinPercentage
        {
            get {
                return GetDecimal(won, played) * 100;
            }
        }

        public double AverageDamageDealt
        {
            get
            {
                return GetDecimal(damageDealt, played);
            }
        }

        public double AverageDamageRecieved
        {
            get
            {
                return GetDecimal(damageRecieved, played);
            }
        }
        
        public double AverageTurnsOnBoard
        {
            get
            {
                return GetDecimal(turnsOnBoard, played);
            }
        }

        public string CardImage
        {
            get
            {
                return "../Images/" + card.CardFileName + ".png";
            }
        }

        public string Name
        {
            get 
            {
                return card.Name;
            }
        }

        private double GetDecimal(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                return 0;
            }
            double dec = Math.Round(((double)numerator / (double)denominator), 2);
            return dec; 
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

        public void IncrementDamage(int attack, int damage)
        {
            damageDealt += attack;
            damageRecieved += damage;

        }
        
        public void CalculateTurnsOnBoard(int turnPlayed, int turnDied)
        {
            turnsOnBoard += turnDied - turnPlayed;
        }
    }
}
