using System;
using System.Collections.Generic;

namespace BlackJack
{
  public class BlackJackGame
  {
    //method in the class that contains the logic that is going to run the game...
    public void StartGame()
    {
      string userInput;

      bool gameOver = false;
      while (!gameOver)
      {
        Console.Clear();

        Console.WriteLine("╔════════════════════════════════════════════╗");
        Console.WriteLine("    Welcome to BlackJack! Let's deal...");
        Console.WriteLine("╚════════════════════════════════════════════╝");

        //INSERT GAME HERE
        var deck = GetShuffledDeck();

        var playerHand = new List<Card>();
        var dealerHand = new List<Card>();

        //DEAL PLAYER HAND
        playerHand.Add(deck[0]);
        playerHand.Add(deck[1]);
        deck.RemoveAt(0);
        deck.RemoveAt(0);

        Console.WriteLine($"\nYou've been dealt the {playerHand[0].DisplayCard()} and the {playerHand[1].DisplayCard()}");

        //DEAL DEALER HAND
        dealerHand.Add(deck[0]);
        dealerHand.Add(deck[1]);
        deck.RemoveAt(0);
        deck.RemoveAt(0);

        Console.WriteLine($"\nThe Dealer has been dealt the {dealerHand[0].DisplayCard()} and a facedown card...");

        var playerTotal = GetHandTotal(playerHand);
        var dealerTotal = GetHandTotal(dealerHand);

        Console.WriteLine($"\nYour current total is {playerTotal}");
        if (playerTotal != 21)
        {
          //BEGIN THE PLAYER HIT LOOP
          bool playerStand = false;
          while (!playerStand)
          {
            Console.WriteLine("\n|* * * * * * * * * * * * * * * * * * * * * * * * *|");
            Console.WriteLine("    Would you like to hit or stand?    ");
            Console.WriteLine("|* * * * * * * * * * * * * * * * * * * * * * * * *|");


            userInput = Console.ReadLine().ToLower();

            if (userInput != "hit" && userInput != "stand")
            {
              Console.WriteLine("\nThat is not a valid response, please try again...");
            }
            else if (userInput == "hit")
            {
              playerHand.Add(deck[0]);
              deck.RemoveAt(0);

              Console.WriteLine($"\nYour next card is {playerHand[playerHand.Count - 1].DisplayCard()}");
              playerTotal = GetHandTotal(playerHand);

              if (playerTotal >= 21)
              {
                break; // breaks you out of the loop!
              }
            }
            else if (userInput == "stand")
            {
              playerStand = true;
            }

            Console.WriteLine($"\nYour current total is {playerTotal}");
          }
        }

        //BEGIN DEALER'S HAND CHECK

        if (playerTotal <= 21)
        {
          Console.WriteLine("Let's see what's in the dealer's hand ...");

          Console.WriteLine($"\nThe dealer has a {dealerHand[0].DisplayCard()} and a {dealerHand[1].DisplayCard()}");
          Console.WriteLine($"Dealer's total is {dealerTotal}");

          while (dealerTotal < 17)
          {
            Console.WriteLine("The dealer hits....");

            dealerHand.Add(deck[0]);
            deck.RemoveAt(0);

            Console.WriteLine($"\nThe dealer's next card is {dealerHand[dealerHand.Count - 1].DisplayCard()}");

            dealerTotal = GetHandTotal(dealerHand);

            Console.WriteLine($"Dealer's total is {dealerTotal}");
          }
        }

        //COMPARE SCORES

        Console.WriteLine($"\nYour total is {playerTotal} and the Dealer's total is {dealerTotal}");

        if (playerTotal > 21)
        {
          Console.WriteLine("You Bust!");
        }
        else if (dealerTotal > 21)
        {
          Console.WriteLine("Dealer busts! You win!");
        }
        else if (playerTotal > dealerTotal)
        {
          Console.WriteLine("You win!");
        }
        else if (dealerTotal > playerTotal)
        {
          Console.WriteLine("You lose, Dealer wins...");
        }
        else
        {
          Console.WriteLine("Push! You Tied!");
        }


        Console.WriteLine("\nWould you like to play again? Yes or No?");

        userInput = Console.ReadLine();

        if (userInput.ToLower() == "no")
        {
          gameOver = true;
        }
      }
    }

    private List<Card> GetShuffledDeck()
    {
      //found instrustions for UniCode here (https://www.youtube.com/watch?v=eGKQhr1HxzU)
      Console.OutputEncoding = System.Text.Encoding.UTF8;
      var deck = new List<Card>();
      var suits = new string[] { "\u2663", "\u2666", "\u2665", "\u2660" };
      var ranks = new string[] { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

      for (var i = 0; i < suits.Length; i++)
      {
        for (var j = 0; j < ranks.Length; j++)
        {
          var card = new Card();
          card.Rank = ranks[j];
          card.Suit = suits[i];
          deck.Add(card);
        }
      }

      //fisher-yates algorithm 
      for (var i = deck.Count - 1; i >= 1; i--)
      {
        var j = new Random().Next(i);
        var temp = (deck[j]);
        deck[j] = (deck[i]);
        deck[i] = (temp);
      }

      return deck;

    }

    // int is being returned, GetHandTotal is the name of method, we're passing in a list of cards,
    // through the cardHand variable
    private int GetHandTotal(List<Card> cardHand)
    {
      int totalCardValue = 0;

      for (var i = 0; i < cardHand.Count; i++)
      {
        totalCardValue += cardHand[i].GetCardValue();
      }

      return totalCardValue;
    }
  }
}
