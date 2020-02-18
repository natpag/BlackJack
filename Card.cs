using System;
using System.Collections.Generic;


namespace BlackJack
{
  public class Card
  {

    //rank
    public string Rank { get; set; }
    //suit
    public string Suit { get; set; }


    //method
    public string DisplayCard()
    {
      return $"{Rank}{Suit}";
    }

    public int GetCardValue()
    {
      if (Rank == "Ace")
      {
        return 11;
      }
      else if (Rank == "Queen" || Rank == "King" || Rank == "Jack")
      {
        return 10;
      }
      else
      {
        return int.Parse(Rank);
      }


    }
  }
}
