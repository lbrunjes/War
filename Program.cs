using System;
using System.Linq;

namespace Brunjes.War 
{
    class Program
    {
        public const char KEY_EXIT = 'x';
        public const char KEY_WAR = 'w';
        public const char KEY_CARDS_1 = '1';
        public const char KEY_CARDS_2 = '2';
        public const char KEY_CARDS_3 = '3';
        public const char KEY_CARDS_4 = '4';
        public const char KEY_PLAYERS = 'p';
        public static  War Exitsting = new War();
        static void Main(string[] args)
        {

            DrawSplashScreen();
            DrawMainMenu();
            var x = Console.ReadLine();
            Console.WriteLine(x);
            
        }

        public static void DrawSplashScreen(){
            Console.WriteLine(@"
        Brunjes Relaxation Team
                Presents
                
  ----------------------------------

   \\\   /^\   ///   /^\    |||D))
    \\\ //^\\ ///   //^\\   |||\\\
     \\V// \\V//   ///=\\\  ||| \\\

  ----------------------------------

");


            
        }

        public static void DrawMainMenu(){
            Console.WriteLine(String.Join( " vs ", Exitsting.Players.Select(p=>p.Name).ToArray()));
            Console.WriteLine("Press 1-4 to set number of face down cards in ties.\nPress W to start\nPress P to edit players\nPress X to exit");
            char key = (char)Console.Read();
           
                switch(key){
                    
                    case KEY_WAR:
                        Console.WriteLine("BEGINING WAR");
                        Exitsting.Start();

                        break;
                    case KEY_CARDS_1:
                        Console.WriteLine("Setting face down cards to 1");
                        Exitsting.NumCardsOnTie = 1;
                        DrawMainMenu();
                        break;

                    case KEY_CARDS_2:
                        Console.WriteLine("Setting face down cards to 2");
                        Exitsting.NumCardsOnTie = 2;
                        DrawMainMenu();
                        break;

                    case KEY_CARDS_3:
                        Console.WriteLine("Setting face down cards to 3");
                        Exitsting.NumCardsOnTie = 3;
                        DrawMainMenu();
                        break;
                    case KEY_CARDS_4:
                        Console.WriteLine("Setting face down cards to 4");
                        Exitsting.NumCardsOnTie = 4;
                        DrawMainMenu();
                        break;
                    case KEY_EXIT:
                        break;
                    case KEY_PLAYERS:
                        DrawPlayerMenu();
                        break;
                        
                    default:
                    DrawMainMenu();
                    break;
                }
        }

        public static void DrawWinScreen(string winner, int rounds){
            DrawSplashScreen();
            Console.WriteLine(@"WAR OVER");                               
            Console.WriteLine(@""+winner);                               
            Console.WriteLine(@"WON IN "+rounds+" Tricks");
            Console.Beep();
            Console.Beep();




        }
        public static void DrawPlayerMenu(){
            Console.WriteLine("PLayers:");
            foreach(Player p in Exitsting.Players){
                Console.WriteLine($" * ({p.Id}) {p.Name} ");
            }
            Console.WriteLine("Enter Id to remove or a name to add.");
            Console.WriteLine("X To go back");
            string item = (string)Console.ReadLine().Trim();
            if(item == KEY_EXIT.ToString()){
                DrawMainMenu();
            }
            else{
                if(item.StartsWith("0") || item.StartsWith("1") ||item.StartsWith("2") ||item.StartsWith("3") ||item.StartsWith("4") ||item.StartsWith("5") ||item.StartsWith("6") ||item.StartsWith("7") ||item.StartsWith("8") ||item.StartsWith("9")  ){
                    Console.WriteLine($"removing {item}");
                    Exitsting.Players.Remove(Exitsting.Players.Where(p => p.Id.ToString() == item).First());
                    for(int i=0; i < Exitsting.Players.Count;i++){
                        Exitsting.Players[i].Id = i;
                    }
                    DrawPlayerMenu();

                }
                else{
                    Console.WriteLine("Adding user");
                    if(!String.IsNullOrEmpty(item))
                        Exitsting.Players.Add( new Player(item, Exitsting.Players.Count()));
                    DrawPlayerMenu();

                }
            }
        }
    }
}
