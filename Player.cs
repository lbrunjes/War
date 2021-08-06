using System.Collections.Generic;

namespace Brunjes.War {
public class Player{
    public int Id{get;set;}
    public string Name {get;set;}
    public List<Card> Deck = new List<Card>();
    public Player(string name, int id){
        Name= name;
        Id = id;
    }

 
}
}