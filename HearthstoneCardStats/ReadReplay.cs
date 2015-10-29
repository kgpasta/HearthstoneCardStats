using Hearthstone_Deck_Tracker;
using System;
using System.IO;
using System.Linq;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using Hearthstone_Deck_Tracker.Stats;
using Hearthstone_Deck_Tracker.Replay;
using Hearthstone_Deck_Tracker.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HearthstoneCardStats
{
	public class ReadReplay
	{
        public static Dictionary<String,CardStats> LoadFile() {
            Deck deck = DeckList.Instance.ActiveDeck;
            if (deck == null)
            {
                return null;
            }
            var games = deck.DeckStats.Games;

            Dictionary<String, CardStats> cardStatTable = new Dictionary<String, CardStats>();

            foreach (GameStats game in games)
            {
                if (game.ReplayFile != null)
                {
                    List<ReplayKeyPoint> replay = ReplayReader.LoadReplay(game.ReplayFile);
                    bool gameResult = false;

                    if (replay.LastOrDefault().Type == KeyPointType.Victory)
                    {
                        gameResult = true;
                    }

                    Board board = new Board();

                    foreach (ReplayKeyPoint point in replay)
                    {
                        if (point.Player == ActivePlayer.Player)
                        {
                            //Get card from table
                            string currentId = point.GetCardId();
                            if (currentId != null)
                            {
                                if (!cardStatTable.ContainsKey(currentId))
                                {
                                    cardStatTable.Add(currentId, new CardStats(currentId));
                                }
                                CardStats cardStats;
                                cardStatTable.TryGetValue(currentId, out cardStats);

                                //Entity
                                Entity entity = point.Data.FirstOrDefault(x => x.Id == point.Id);

                                //Increment card depending on event
                                if (point.Type == KeyPointType.Play || point.Type == KeyPointType.PlaySpell)
                                {
                                    cardStats.IncrementPlay(gameResult);
                                    if (entity.IsMinion)
                                    {
                                        board.AddMinionToBoard(point.Id, point.Turn);
                                    }     
                                }
                                else if (point.Type == KeyPointType.Draw)
                                {
                                    cardStats.IncrementDrawn();
                                }
                                else if (point.Type == KeyPointType.Attack)
                                {
                                    int cardIndexAtk = point.Data[0].GetTag(GAME_TAG.PROPOSED_ATTACKER);
                                    int cardIndexDef = point.Data[0].GetTag(GAME_TAG.PROPOSED_DEFENDER);
                                    if (cardIndexAtk < point.Data.Length && cardIndexDef < point.Data.Length)
                                    {
                                        int attack = point.Data.FirstOrDefault(x => x.Id == cardIndexAtk).Attack;
                                        int damage = point.Data.FirstOrDefault(x => x.Id == cardIndexDef).Attack;

                                        cardStats.IncrementDamage(attack, damage);
                                    }

                                }
                                else if (point.Type == KeyPointType.PlayToHand || point.Type == KeyPointType.PlayToDeck)
                                {
                                    cardStats.CalculateTurnsOnBoard(board.GetTurnPlayed(point.Id), point.Turn);
                                    if (entity.IsMinion)
                                    {
                                        board.RemoveMinionFromBoard(point.Id);
                                    }
                                }
                                else if(point.Type == KeyPointType.Death)
                                {
                                	cardStats.CalculateTurnsOnBoard(board.GetTurnPlayed(point.Id), point.Turn);
                                    if (entity.IsMinion)
                                    {
                                        board.RemoveMinionFromBoard(point.Id);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return cardStatTable;
        }

	}
}
