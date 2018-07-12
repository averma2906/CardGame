using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    public  static class HandEvaluator
    {
        public enum HandCompareResult
        {
            Player1Wins, Player2Wins, Tie
        }

        public static HandCompareResult Compare(List<PlayingCard> player1Cards, List<PlayingCard> player2Cards, List<PlayingCard> deckCards)
        {
            var player1 = new  List<PlayingCard>(deckCards);
            player1.AddRange(player1Cards);

            var player2 = new  List<PlayingCard>(deckCards);
            player2.AddRange(player2Cards);

            return Compare(player1, player2);
        }

        public static HandCompareResult Compare(List<PlayingCard> player1Cards, List<PlayingCard> player2Cards)
        {
            PokerHand hand1 = new PokerHand();
            List<PlayingCard> bestCards1 = null;

            HandEval(player1Cards, ref hand1, ref bestCards1);

            PokerHand hand2 = new PokerHand();
            List<PlayingCard> bestCards2 = null;

            HandEval(player2Cards, ref hand2, ref bestCards2);

            Debug.LogFormat("{0} vs {1}", hand1, hand2);

            if (hand1 > hand2)
            {
                return HandCompareResult.Player1Wins;
            }
            if (hand1 < hand2)
            {
                return HandCompareResult.Player2Wins;
            }
            return CompareBests(bestCards1, bestCards2);
        }

        private static HandCompareResult CompareBests(List<PlayingCard> bestCards1, List<PlayingCard> bestCards2)
        {
            for (int i = 0; i < 5; i++)
            {
                if (bestCards1[i].NominalValue > bestCards2[i].NominalValue) return HandCompareResult.Player1Wins;
                if (bestCards1[i].NominalValue < bestCards2[i].NominalValue) return HandCompareResult.Player2Wins;
            }
            return HandCompareResult.Tie;
        }

        private static void HandEval(List<PlayingCard> cards, ref PokerHand hand, ref List<PlayingCard> bestCards)
        {
            if (IsRoyalFlush(cards, ref hand, ref bestCards)) return;
            if (IsStraightFlush(cards, ref hand, ref bestCards)) return;
            if (IsFourOfAKind(cards, ref hand, ref bestCards)) return;
            if (IsFullHouse(cards, ref hand, ref bestCards)) return;
            if (IsFlush(cards, ref hand, ref bestCards)) return;
            if (IsStraight(cards, ref hand, ref bestCards)) return;
            if (IsThreeOfKind(cards, ref hand, ref bestCards)) return;
            if (IsTwoPair(cards, ref hand, ref bestCards)) return;
            if (IsPair(cards, ref hand, ref bestCards)) return;
            IsHighCard(cards, ref hand, ref bestCards);
        }

        private static void IsHighCard(List<PlayingCard> cards, ref PokerHand hand, ref List<PlayingCard> bestCards)
        {
            hand = PokerHand.HighCard;

            bestCards = new List<PlayingCard>(cards.OrderByDescending(card => card.NominalValue).Take(5));
        }

        private static bool IsPair(List<PlayingCard> cards, ref PokerHand hand, ref List<PlayingCard> bestCards)
        {
            var pair = cards.GroupBy(card => card.NominalValue).FirstOrDefault(group => @group.Count() == 2);
            if (pair != null)
            {
                hand = PokerHand.Pair;

                var bests = new List<PlayingCard>(pair);
                bests.AddRange(cards.Where(card => !bests.Contains(card)).OrderByDescending(card => card.NominalValue).Take(3));
                bestCards = bests;

                return true;
            }

            return false;
        }

        private static bool IsTwoPair(List<PlayingCard> cards, ref PokerHand hand, ref List<PlayingCard> bestCards)
        {
            var pairGroups = cards.GroupBy(card => card.NominalValue).Where(group => @group.Count() == 2).OrderByDescending(group => @group.Key);

            if (pairGroups.Count() >= 2)
            {
                var highPair = pairGroups.First();
                var lowPair = pairGroups.Skip(1).First();

                hand = PokerHand.TwoPair;

                var bests = new List<PlayingCard>();
                bests.AddRange(highPair);
                bests.AddRange(lowPair);

                bests.Add(cards.Where(card=>!bests.Contains(card)).OrderByDescending(card => card.NominalValue).First());
                bestCards = bests;

                return true;
            }
            return false;
        }

        private static bool IsThreeOfKind(List<PlayingCard> cards, ref PokerHand hand, ref List<PlayingCard> bestCards)
        {
            var triplet = cards.GroupBy(card => card.NominalValue).FirstOrDefault(group => @group.Count() == 3);

            if (triplet != null)
            {
                hand = PokerHand.ThreeOfKind;

                var bests = new List<PlayingCard>(triplet);
                bestCards.AddRange(cards.Where(card => !bests.Contains(card)).OrderByDescending(card => card.NominalValue).Take(2));

                return true;
            }
            return false;
        }

        private static bool IsStraight(List<PlayingCard> cards, ref PokerHand hand, ref List<PlayingCard> bestCards)
        {
            var highCard = cards.FirstOrDefault(card =>
                (cards.Any(card2 => (card2.NominalValue == card.NominalValue - 1)) &&
                 cards.Any(card2 => (card2.NominalValue == card.NominalValue - 2)) &&
                 cards.Any(card2 => (card2.NominalValue == card.NominalValue - 3)) &&
                 cards.Any(card2 => (card2.NominalValue == card.NominalValue - 4)))
                ||
                //special case when Ace is considered as the lowest card in straight
                ((card.NominalValue == CardNominalValue.Five) &&
                 (cards.Any(card2 => (card2.NominalValue == CardNominalValue.Four)) &&
                  cards.Any(card2 => (card2.NominalValue == CardNominalValue.Three)) &&
                  cards.Any(card2 => (card2.NominalValue == CardNominalValue.Two)) &&
                  cards.Any(card2 => (card2.NominalValue == CardNominalValue.Ace)))
                ));

            if (highCard == null) return false;

            hand = PokerHand.Straight;

            bestCards = (highCard.NominalValue == CardNominalValue.Five)
                ? new List<PlayingCard>
                {
                    highCard,
                    cards.First(card => card.NominalValue == CardNominalValue.Four),
                    cards.First(card => card.NominalValue == CardNominalValue.Three),
                    cards.First(card => card.NominalValue == CardNominalValue.Two),
                    cards.First(card => card.NominalValue == CardNominalValue.Ace),
                }
                : new List<PlayingCard>
                {
                    highCard,
                    cards.First(card => card.NominalValue == highCard.NominalValue - 1),
                    cards.First(card => card.NominalValue == highCard.NominalValue - 2),
                    cards.First(card => card.NominalValue == highCard.NominalValue - 3),
                    cards.First(card => card.NominalValue == highCard.NominalValue - 4)
                };

            return true;
        }

        private static bool IsFlush(List<PlayingCard> cards, ref PokerHand hand, ref List<PlayingCard> bestCards)
        {
            var allSameSuite = cards.GroupBy(card => card.Suit).FirstOrDefault(group => group.Count() >= 5);

            if (allSameSuite != null)
            {
                hand = PokerHand.Flush;

                bestCards = new List<PlayingCard>(allSameSuite.OrderByDescending(card => card.NominalValue).Take(5));

                return true;
            }

            return false;

        }

        private static bool IsFullHouse(List<PlayingCard> cards, ref PokerHand hand, ref List<PlayingCard> bestCards)
        {
            var groupsOfTriples = cards.GroupBy(card => card.NominalValue).Where(group => @group.Count() == 3).OrderByDescending(group => @group.Key);

            if (groupsOfTriples.Count() == 1)
            {
                var triplet = groupsOfTriples.First().ToList();

                var pair = cards.Where(card => !triplet.Contains(card)).GroupBy(card => card.NominalValue).Where(group => @group.Count() == 2).OrderByDescending(group => @group.Key).FirstOrDefault();

                if (pair == null) return false;

                hand = PokerHand.FullHouse;

                var bests = new List<PlayingCard>();
                bests.AddRange(triplet);
                bests.AddRange(pair);

                bestCards = bests;

                return true;
            }

            if (groupsOfTriples.Count() == 2)
            {
                var triplet = groupsOfTriples.First();
                var pair = groupsOfTriples.Last().Take(2);

                var bests = new List<PlayingCard>();
                bests.AddRange(triplet);
                bests.AddRange(pair);

                bestCards = bests;

                return true;
            }

            return false;
        }

        private static bool IsFourOfAKind(List<PlayingCard> cards, ref PokerHand hand, ref List<PlayingCard> bestCards)
        {
            var four = cards.GroupBy(card => card.NominalValue).FirstOrDefault(group => @group.Count() == 4);

            if (four != null)
            {

                hand = PokerHand.FourOfKind;

                var bests = new List<PlayingCard>(four.ToList());

                bests.Add(cards.Where(card => !bests.Contains(card)).OrderByDescending(card => card.NominalValue).First());

                bestCards = bests;

                return true;
            }

            return false;
        }

        private static bool IsStraightFlush(List<PlayingCard> cards, ref PokerHand hand, ref List<PlayingCard> bestCards)
        {
            var highCard = cards.FirstOrDefault(card =>
                (cards.Any(card2 => ((card2.Suit == card.Suit) && (card2.NominalValue == card.NominalValue - 1))) &&
                 cards.Any(card2 => ((card2.Suit == card.Suit) && (card2.NominalValue == card.NominalValue - 2))) &&
                 cards.Any(card2 => ((card2.Suit == card.Suit) && (card2.NominalValue == card.NominalValue - 3))) &&
                 cards.Any(card2 => ((card2.Suit == card.Suit) && (card2.NominalValue == card.NominalValue - 4))))
                ||
                //special case when Ace is considered as the lowest card in straight
                ((card.NominalValue == CardNominalValue.Five) &&
                 (cards.Any(card2 => (card2.Suit == card.Suit) && (card2.NominalValue == CardNominalValue.Four)) &&
                  cards.Any(card2 => (card2.Suit == card.Suit) && (card2.NominalValue == CardNominalValue.Three)) &&
                  cards.Any(card2 => (card2.Suit == card.Suit) && (card2.NominalValue == CardNominalValue.Two)) &&
                  cards.Any(card2 => (card2.Suit == card.Suit) && (card2.NominalValue == CardNominalValue.Ace))))
                );

            if (highCard != null)
            {

                hand = PokerHand.StraightFlush;

                bestCards = (highCard.NominalValue == CardNominalValue.Five)
                    ? new List<PlayingCard>
                    {
                        highCard,
                        cards.First(card => card.Suit == highCard.Suit && card.NominalValue == CardNominalValue.Four),
                        cards.First(card => card.Suit == highCard.Suit && card.NominalValue == CardNominalValue.Three),
                        cards.First(card => card.Suit == highCard.Suit && card.NominalValue == CardNominalValue.Two),
                        cards.First(card => card.Suit == highCard.Suit && card.NominalValue == CardNominalValue.Ace),
                    }
                    : new List<PlayingCard>
                    {
                        highCard,
                        cards.First(card => card.Suit == highCard.Suit && card.NominalValue == highCard.NominalValue - 1),
                        cards.First(card => card.Suit == highCard.Suit && card.NominalValue == highCard.NominalValue - 2),
                        cards.First(card => card.Suit == highCard.Suit && card.NominalValue == highCard.NominalValue - 3),
                        cards.First(card => card.Suit == highCard.Suit && card.NominalValue == highCard.NominalValue - 4)
                    };

                return true;
            }

            return false;
        }

        private static bool IsRoyalFlush(List<PlayingCard> cards, ref PokerHand hand, ref List<PlayingCard> bestCards)
        {
            var ace = cards.FirstOrDefault(
                card =>
                    ((card.NominalValue == CardNominalValue.Ace)
                     && (cards.Any(card2 => (card2.Suit == card.Suit) && (card2.NominalValue == CardNominalValue.King))
                         &&
                         cards.Any(card2 => (card2.Suit == card.Suit) && (card2.NominalValue == CardNominalValue.Queen))
                         &&
                         cards.Any(card2 => (card2.Suit == card.Suit) && (card2.NominalValue == CardNominalValue.Jack))
                         &&
                         cards.Any(card2 => (card2.Suit == card.Suit) && (card2.NominalValue == CardNominalValue.Ten)))));

            if (ace != null)
            {
                hand = PokerHand.RoyalFlush;

                bestCards = new List<PlayingCard>
                {
                    ace,
                    cards.First(card => card.Suit == ace.Suit && card.NominalValue == CardNominalValue.King),
                    cards.First(card => card.Suit == ace.Suit && card.NominalValue == CardNominalValue.Queen),
                    cards.First(card => card.Suit == ace.Suit && card.NominalValue == CardNominalValue.Jack),
                    cards.First(card => card.Suit == ace.Suit && card.NominalValue == CardNominalValue.Ten)
                };

                return true;
            }
            return false;
        }

    }
