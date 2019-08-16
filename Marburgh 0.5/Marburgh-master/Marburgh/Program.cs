using System;

namespace Marburgh
{
    class Program
    {
        //STARTS GAME
        static void Main(string[] args)
        {
            Welcome();
        }

        //GAME FUNCTIONS    
        public static void Welcome()
        {
            Colour.SetupConsole();
            Console.Clear();
            Utilities.ColourText(Colour.TIME,"/'\\_/`\\             ( )                       ( )    \n");
            Utilities.ColourText(Colour.XP,"|     |   _ _  _ __ | |_    _   _  _ __   __  | |__  \n");
            Utilities.ColourText(Colour.BOSS,"| (_) | /'_` )( '__)| '_`\\ ( ) ( )( '__)/'_ `\\|  _ `\\ \n");
            Utilities.ColourText(Colour.DROP,"| | | |( (_| || |   | |_) )| (_) || |  ( (_) || | | |\n");
            Utilities.ColourText(Colour.RAREDROP,"(_) (_)`\\__,_)(_)   (_,__/'`\\___/'(_)  `\\__  |(_) (_)\n");
            Utilities.ColourText(Colour.NAME,"                                       ( )_) |       \n");
            Utilities.ColourText(Colour.GOLD,"                                        \\___/'       \n");
            Utilities.ColourText(Colour.CLASS,"");
            Utilities.EmbedColourText(Colour.HEALTH,"\n\n","[N]","ew Game       ");//[L]oad game);
            string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (choice == "n") Create.Character1Create();
            //else if (choice == "l") Data.Load(p);
            else Welcome();
        }
        
        public static void GameTown()
        {            
            Dungeon d = Dungeon.DungeonList[0];
            Console.Clear();
            Utilities.ColourText(Colour.SPEAK, "You are in the town of Marburgh. It is a small town, but is clearly growing. Who knows what will be here in a month?\n\n");
            Console.WriteLine("[W]eapon shop            [A]rmor shop            [I]tem shop          [D]ungeon");
            Console.WriteLine("[T]avern                 [Y]our house            [B]ank                         ");
            Console.WriteLine("[C]haracter              [H]eal                  [L]evel Master       [O]ther Places");
            Console.WriteLine("[S]ave                   [?]Help                                      [Q]uit             ");
            Utilities.ColourText(Colour.SPEAK, "\n\nWhat would you like to do?\n\n");
            Utilities.EmbedColourText(Colour.ENERGY, Colour.ENERGY, Colour.ENERGY, Colour.ENERGY, "It is day ",$"{Time.day}",", the ",$"{Time.weeks[Time.week]}", " week of ",$"{Time.months[Time.month]}",", ",$"{Time.year}","\n\n");
            string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (choice == "w")Shop.GameShop(Shop.WeaponShop, Create.p);
            else if (choice == "a")Shop.GameShop(Shop.ArmorShop, Create.p);
            //else if (choice == "s") Data.Save(p);
            else if (choice == "d") Explore.DungeonCheck(d, Create.p);
            else if (choice == "h") Utilities.Heal(Create.p);
            else if (choice == "i") Shop.ItemShop(Create.p);
            else if (choice == "?") Utilities.Help();
            else if (choice == "c") Utilities.CharacterSheet(Create.p);
            else if (choice == "l") LevelMaster.VisitLevelMaster(Create.p);
            else if (choice == "t") Tavern.Inn(Create.p);
            else if (choice == "y") House.YourHouse(Create.p);
            else if (choice == "b") Bank.GameBank(Create.p);
            else if (choice == "q") Utilities.Quit();
            else if (choice == "x") Utilities.Death();
            else if (choice == "o") OtherPlaces.Other(Create.p);
            GameTown();
        }        
    }
}