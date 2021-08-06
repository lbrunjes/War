using System;

namespace Brunjes.War {
    public class Card{
        //♠	♥	♦	♣
       public  static readonly string[] Suits = new string[]{"Clubs", "Diamonds", "Hearts", "Spades"};
       public static readonly string[] CardNames = new string[]{"2","3","4","5","6","7","8","9","10","J","Q","K","A"};

       public int suit{get;set;}
       public int value{get;set;}

       public Card(int _suit, int _value){
           this.suit = _suit;
           this.value =_value;

           if(this.suit >= Suits.Length){
               throw new ArgumentOutOfRangeException("Suits must be smaller than the suit array");
           }
            if (this.value >= CardNames.Length)
            {
                throw new ArgumentOutOfRangeException("Cards must be smaller than the suit array");
            }
       }
       public override string ToString(){
           return $"{CardNames[value]} of {Suits[suit]}";
       }

    }
}