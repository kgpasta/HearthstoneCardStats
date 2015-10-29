using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthstoneCardStats
{
    class Board
    {
        Dictionary<int, int> minions;

        public Board()
        {
            minions = new Dictionary<int, int>();
        }

        public void AddMinionToBoard(int minion, int turn){
            minions.Add(minion,turn);
        }

        public int GetTurnPlayed(int minion)
        {
            int turn = -1;
            minions.TryGetValue(minion, out turn);
            if (turn > -1)
            {
                return turn;
            }
            return 0;
        }

        public void RemoveMinionFromBoard(int minion)
        {
            minions.Remove(minion);
        }
    }
}
