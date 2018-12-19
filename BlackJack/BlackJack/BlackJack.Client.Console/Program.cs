namespace BlackJack.Client.Console
{
    using System;
    using BlackJack.Core;
    using System.Collections.Generic;
    using System.Threading;
    
    class Program
    {

        static Deck deck;
        static List<Card> userHand;
        static List<Card> dealerHand;

        static void Main(string[] args)
        {
            Console.Title = "♠♥♣♦ Blackjack";
            Console.WriteLine("Welcome to the game of Blackjack");
           
            deck = new Deck();
            deck.Shuffle();
            
            {
                DealHand();
                Console.ReadKey(true);
            }           
        }

        static void DealHand()
        {
            if (deck.GetAmountOfRemainingCrads() < 20)
            {
                deck.Initialize();
                deck.Shuffle();
            }

            Console.WriteLine("Remaining Cards: {0}", deck.GetAmountOfRemainingCrads());
            Console.WriteLine("Press any key to start!");
            string input = Console.ReadLine().Replace(" ", "");
            
            userHand = new List<Card>();
            userHand.Add(deck.DrawACard());
            userHand.Add(deck.DrawACard());

            foreach (Card card in userHand)
            {
                if (card.Face == Face.Ace)
                {
                    card.Value += 10;
                    break;
                }
            }

            Console.WriteLine("Player");
            Console.WriteLine("Card 1: {0} of {1}", userHand[0].Face, userHand[0].Suit);
            Console.WriteLine("Card 2: {0} of {1}", userHand[1].Face, userHand[1].Suit);
            Console.WriteLine("Total: {0}\n", userHand[0].Value + userHand[1].Value);

            dealerHand = new List<Card>();
            dealerHand.Add(deck.DrawACard());
            dealerHand.Add(deck.DrawACard());

            foreach (Card card in dealerHand)
            {
                if (card.Face == Face.Ace)
                {
                    card.Value += 10;
                    break;
                }
            }

            Console.WriteLine("Delaer");
            Console.WriteLine("Card 1: {0} of {1}", dealerHand[0].Face, dealerHand[1].Suit);
            Console.WriteLine("Card 2: Hole Card");
            Console.WriteLine("Total: {0}\n", dealerHand[0].Value);

            {
               
                if (userHand[0].Value + userHand[1].Value == 21)
                {
                    Console.WriteLine("Blackjack, You Won!");
                    return;
                }

                do
                {
                    Console.WriteLine("Please choose a valid option: (S)tand or (H)it");
                    ConsoleKeyInfo userOption = Console.ReadKey(true);
                    while (userOption.Key != ConsoleKey.H && userOption.Key != ConsoleKey.S)
                    {
                        Console.WriteLine("Illegal key. Please choose a valid option: (S)tand or (H)it");
                        userOption = Console.ReadKey(true);
                    }
                    Console.WriteLine();

                    switch (userOption.Key)
                    {
                        case ConsoleKey.H:
                            userHand.Add(deck.DrawACard());
                            Console.WriteLine("Hitted {0} of {1}", userHand[userHand.Count - 1].Face, userHand[userHand.Count - 1].Suit);
                            int totalCardsValue = 0;
                            foreach (Card card in userHand)
                            {
                                totalCardsValue += card.Value;
                            }
                            Console.WriteLine("Total cards value now: {0}\n", totalCardsValue);
                            if (totalCardsValue > 21)
                            {
                                Console.Write("Busted!\nDealer wins!");
                                Thread.Sleep(3000);
                                return;
                            }
                            else if (totalCardsValue == 21)
                            {
                                Console.WriteLine("Great job! I think you might want to stand from now on...\n");
                                Thread.Sleep(3000);
                                continue;
                            }
                            else
                            {
                                continue;
                            }
                            
                        case ConsoleKey.S:

                            Console.WriteLine("Delaer");
                            Console.WriteLine("Card 1: {0} of {1}", dealerHand[0].Face, dealerHand[1].Suit);
                            Console.WriteLine("Card 2: {0} of {1}", dealerHand[1].Face, dealerHand[1].Suit);

                            int dealerCardsValue = 0;
                            foreach (Card card in dealerHand)
                            {
                                dealerCardsValue += card.Value;
                            }

                            while (dealerCardsValue < 17)
                            {
                                Thread.Sleep(3000);
                                dealerHand.Add(deck.DrawACard());
                                dealerCardsValue = 0;
                                foreach (Card card in dealerHand)
                                {
                                    dealerCardsValue += card.Value;
                                }
                                Console.WriteLine("Card {0}: {1} of {2}", dealerHand.Count, dealerHand[dealerHand.Count - 1].Face, dealerHand[dealerHand.Count - 1].Suit);
                            }
                            dealerCardsValue = 0;
                            foreach (Card card in dealerHand)
                            {
                                dealerCardsValue += card.Value;
                            }
                            Console.WriteLine("Total: {0}\n", dealerCardsValue);

                            if (dealerCardsValue > 21)
                            {
                                Console.WriteLine("Dealer bust! You win!");
                                return;
                            }
                            else
                            {
                                int playerCardValue = 0;
                                foreach (Card card in userHand)
                                {
                                    playerCardValue += card.Value;
                                }

                                if (dealerCardsValue > playerCardValue)
                                {
                                    Console.WriteLine("Dealer has {0} and player has {1}, dealer wins!", dealerCardsValue, playerCardValue);
                                    return;
                                }
                                else
                                {
                                    Console.WriteLine("Player has {0} and dealer has {1}, player wins!", playerCardValue, dealerCardsValue);

                                    return;
                                }
                            }
                            default:
                            break;
                    }
                    Console.ReadLine();
                }
                while (true);
            }
        }
    }
}
