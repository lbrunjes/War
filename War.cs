using System;
using System.Collections.Generic;
using System.Linq;

namespace Brunjes.War {

    public class War{
        public int NumCardsOnTie{get;set;}=3;
        int rounds =0;
        public List<Player> Players = new List<Player>{
        new Player("Crawford",0),
        new Player("Mary Anne",1),
        new Player("Rhiannon",2),
        new Player("Hunter",3)
        };

        public List<Card> Deck =new List<Card>();



        public War(){
           InitDeck();
           Shuffle();
           Shuffle();
        }

        protected void InitDeck(){

            for (var s = 0; s < Card.Suits.Length; s++)
            {
                for (int c = 0; c < Card.CardNames.Length; c++)
                {
                    Deck.Add(new Card(s, c));

                }
            }
        }
        protected void Shuffle(){
            Console.WriteLine("Shuffling...");

            Random rnd = new Random();
            Card curr = null;
            int index = 0;
            for( int i = Deck.Count -1; i>0;i--){
                curr = Deck[i];
                index = rnd.Next(0,i-1);
                Deck[i] = Deck[index];
                Deck[index] = curr;
            }
            
        }

        public void Start(){
            Deal();
            while(Players.Where(p => p.Deck.Count > 0 ).Count() > 1){
                Fight();
            }
            Program.DrawWinScreen(Players.Where(p => p.Deck.Count> 0 ).Select(p=>p.Name).First(), rounds);
            Console.Beep();
        }
        public void Deal(){
            Console.WriteLine("Dealing...");

            Card c;
            while (Deck.Count > 0)
            {
                c = Deck[0];
                Players[Deck.Count% Players.Count].Deck.Add(c);
                Deck.RemoveAt(0);
            }
            foreach(Player p in Players){
                Console.WriteLine($"{p.Name}: {p.Deck.Count} Cards");
            }
        }

        public Card  GetCard(int Player){
            if(Players[Player].Deck.Count>0){
                Card c = Players[Player].Deck[0];
                Players[Player].Deck.Remove(c);
                return c;
            }
            return null;
        }
         

        public void Fight(){
            rounds++;
            Console.WriteLine($"\n\n=== FIGHT #{rounds}===");

            Dictionary<int,Card> Cards;
            List<int> winners = Players.Where(p=>p.Deck.Count()>0).Select(p => p.Id).ToList();
            List<Card> Pending= new List<Card>();

            do{
                Cards = GetPlayedCards(winners.ToArray());
                winners = GetWinners(Cards);

                if(winners.Count == 1){
                    Players[winners[0]].Deck.AddRange(Pending);
                    Players[winners[0]].Deck.AddRange(Cards.Select(kvp=> kvp.Value));
                    Console.WriteLine($"\n =>{Players[winners[0]].Name.ToUpper()} WINS");
                   
                }
                else{
                    Pending.AddRange(Cards.Select(kvp=> kvp.Value));
                    Console.WriteLine($"{winners.Count} way TIE");

                    foreach(int i in winners){
                        int cardsToTake = Players[i].Deck.Count>NumCardsOnTie?NumCardsOnTie:Players[i].Deck.Count-1;
                        if(cardsToTake>0){
                            Pending.AddRange(Players[i].Deck.GetRange(0,cardsToTake));
                            Players[i].Deck.RemoveRange(0, cardsToTake);
                        }
                    }
                }
            }
            while (winners.Count !=1 );
            int cards = 0;
            foreach (var player in Players)
            {
                Console.Write($"{player.Name}:{player.Deck.Count} ");
                cards += player.Deck.Count;
               
            }
            Console.WriteLine();
            if(cards >52){
                throw new Exception("cards exploded");
            }
        }

        protected Dictionary<int, Card> GetPlayedCards(int[] playersToPlay){
            Dictionary<int, Card> cards = new Dictionary<int, Card>();
            foreach(int i in playersToPlay){

                var card = GetCard(i);
                if(card != null){
                    cards.Add(i, card);
                    Console.WriteLine($"{Players[i].Name}: {card}");
                }
            }
            return cards;
        }

        protected List<int> GetWinners(Dictionary<int, Card> Played){
            List<int> output = new List<int>();
            int max =-1;
            foreach(KeyValuePair<int, Card> kvp in Played){
                if(kvp.Value.value>max){
                    output.Clear();
                    output.Add(kvp.Key);
                    max = kvp.Value.value;
                }
                else{
                    if(max ==  kvp.Value.value){
                        output.Add(kvp.Key);
                    }
                }
            }

            return output;
        }


    }
}