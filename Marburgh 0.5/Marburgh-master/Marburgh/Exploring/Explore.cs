using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Explore
{
    //Variables, self explanatory
    public static bool canExplore = true;
    public static bool fought;
    public static int dungeonRoomTally;
    public static List<Monster> opponentList = new List<Monster> { };
    public static List<Event> EventDisplay = new List<Event> { };    

    //Can you go exploring?
    public static void DungeonCheck(Dungeon d, Creature p)
    {
        //If no, go home
        if (canExplore == false)
        {
            Utilities.ColourText(Colour.SPEAK, "You are exhausted. Go to bed and come back tomorrow");
            Utilities.Keypress();
            Marburgh.Program.GameTown();
        }
        //Warning so you don't use it then leave right away
        Utilities.ColourText(Colour.SPEAK, "You may only go exploring once a day. Would you like to go now?");
        Console.WriteLine("\n\n[Y]es      [N]o");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (choice == "y")
        {
            //Disallow going back later, send to dungeon
            canExplore = false;
            dungeonRoomTally = 0;
            GameDungeon(d, p);
        }
        Marburgh.Program.GameTown();
    }

    //Dungeon main screen
    public static void GameDungeon(Dungeon d, Creature p)
    {
        Console.Clear();
        Utilities.ColourText(Colour.SPEAK, d.flavor);
        Console.WriteLine("\n\n[E]xplore      [H]eal");
        Console.WriteLine("[C]haracter    [R]eturn");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (choice== "h") Utilities.Heal(p);
        else if (choice == "r") Marburgh.Program.GameTown();
        else if (choice == "c") Utilities.CharacterSheet(p);
        else if (choice == "e") 
        {
            fought = false;
            dungeonRoomTally++;
            ExploreNextRoom(d, p);
        }
        GameDungeon(d,p);
    }

    private static void ExploreNextRoom(Dungeon d, Creature p)
    {
        opponentList = new List<Monster> { };
        Console.Clear();
        //Check if you hit the boss room
        if (dungeonRoomTally == d.rooms && d.boss.IsAlive == true)//When you hit last room and boss is alive. FIGHT!
        {
            opponentList.Add(d.boss.MonsterCopy());
            Combat.GameCombat(p, opponentList[0]);            
        }
        else if (dungeonRoomTally == d.rooms && d.boss.IsAlive == false) //This is when you hit last room but boss is dead. You get treasure but less.
        {
            int goldFound = Utilities.rand.Next(280, 450);
            //Pays off less according to diminishing returns
            double GoldGain = goldFound * d.diminishingReturns;
            int GoldGainInt = Convert.ToInt32(GoldGain);
            Console.Clear();
            Console.WriteLine($"You make it to the {d.boss.name}'s lair. He is long dead but you manage to find some treasure you didn't notice in your last pass");
            Console.WriteLine($"You gain {GoldGainInt} gold");
            p.gold += GoldGainInt;
            //Sets new diminihing returns, 33 % less than last time
            d.diminishingReturns -= 0.33;
            //20% less chance to summon a monster
            d.monsterSummon -= 20;
            Utilities.Keypress();
            Marburgh.Program.GameTown();
        }
        //If you're not on last room, you find a room
        int roomRoll = Utilities.rand.Next(0, d.roomOptions.Count());
        Console.WriteLine("You find " + d.roomOptions[roomRoll].flavor);
        //Check for monsters
        MonsterSummon(d.roomOptions[roomRoll], d, p);
        //Get a chance to explore
        ExploreRoomSearch(d.roomOptions[roomRoll], d, p);
        //If you chose not to explore it's back to main dungeon screen, this second chance at baddies is risk for searching
        if (fought == false)
        {
            //You may have been heard
            Console.Clear();
            Console.Write("You hear shuffling in the distance, were you heard?");
            Utilities.DotDotDot();
            MonsterSummon(d.roomOptions[roomRoll], d, p);
            //if you STILL haven't fought it's cause you didn't summon either time, lucky 
            if (fought == false) Console.WriteLine("Phew! You got luckey!");
        }
        Utilities.Keypress();
        GameDungeon(d,p);
    }

    private static void MonsterSummon(Room room, Dungeon d, Creature p)
    {
        //Did monsters get summoned?
        int x = Utilities.rand.Next(1, 101);
        //If yes,
        int monsterChance = room.modifier + d.monsterSummon;
        if (x <= monsterChance && fought == false)//Make monsters, fight them        
        {
            fought = true;
            //Make monsters, fight them
            Console.Write("\nYour presence has been noted by");
            Utilities.DotDotDot();
            //How many monsters?
            int numroll = Utilities.rand.Next(1, 4);
            for (int i = 0; i < numroll; i++)
            {
                //Which specific monsters?
                int monsterSelect = Utilities.rand.Next(0, d.bestiary.Length);
                opponentList.Add(d.bestiary[monsterSelect].MonsterCopy());
            }
            for (int i = 0; i < opponentList.Count; i++)
            {
                if (opponentList[i].name.FirstOrDefault() == 'A' || opponentList[i].name.FirstOrDefault() == 'E' || opponentList[i].name.FirstOrDefault() == 'I' ||
                opponentList[i].name.FirstOrDefault() == 'O' || opponentList[i].name.FirstOrDefault() == 'U') Utilities.EmbedColourText(Colour.MONSTER, "An", $"{opponentList[i].name}", "");
                else Utilities.EmbedColourText(Colour.MONSTER, "A ", $"{opponentList[i].name}", "");
            }
            Utilities.Keypress();
            Combat.GameCombat(p, opponentList[0]);
        }  
    }

    private static void ExploreRoomSearch(Room room, Dungeon d, Creature p)
    {
        //Search or move on. Search can get stuff but risks a fight if you didn't have one yet.
        Console.Clear();
        Console.WriteLine("You appear to be alone... for now");
        Console.WriteLine("\n[S]earch the room        [M]ove on");
        Console.WriteLine("\nWhat would you like to do?");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (choice == "m") GameDungeon(d, p);
        if (choice == "s")
        {
            //Make a list for events, only add if successful
            EventDisplay = new List<Event> { };
            Console.Write("\nYou find");
            Utilities.DotDotDot();
            for (int i = 0; i < room.EventArray.Length; i++)
            {
                //if successful, add event to list
                int EventSuccessRoll = Utilities.rand.Next(1, 101);
                if (EventSuccessRoll <= room.EventArray[i].success)
                {
                    EventDisplay.Add(room.EventArray[i]);
                }
            }
            //Tell us what we won!
            string a = (EventDisplay.Count == 3) ? $"{EventDisplay[0].flavor},{EventDisplay[1].flavor} and {EventDisplay[2].flavor}" : (EventDisplay.Count == 2) ? $"{EventDisplay[0].flavor} and {EventDisplay[1].flavor}" : (EventDisplay.Count == 1) ? $"{EventDisplay[0].flavor}" : "Nothing!";
            Console.WriteLine(a);
            Console.WriteLine("");
            // Now give it to me!
            for (int i = 0; i < EventDisplay.Count; i++)
            {
                Thread.Sleep(500);
                if (EventDisplay[i].flavor == "Some gold")
                {
                    int goldAdd = Utilities.rand.Next(-3, room.modifier);
                    p.gold += room.EventArray[0].effect + (goldAdd * 12);
                    Console.WriteLine($"You gain {room.EventArray[0].effect + (goldAdd * 12)} gold");
                    Thread.Sleep(500);
                }
                if (EventDisplay[i].flavor == "A health potion")
                {
                    if (p.potions == p.maxPotions)
                        Console.WriteLine($"Somebody already drank the potion\nIt's just an empty bottle!\nOh well...");
                    else
                    {
                        p.potions += room.EventArray[0].effect;
                        Console.Write($"You gain {room.EventArray[0].effect} health potion");
                        if (room.EventArray[0].effect > 1) Console.Write("s");
                    }
                    Thread.Sleep(500);
                }
                if (EventDisplay[i].flavor == "An old book")
                {
                    int XPAdd = Utilities.rand.Next(-3, room.modifier);
                    p.xp += room.EventArray[0].effect + XPAdd;
                    Console.WriteLine($"Reading the book, you gain insight into the dungeon and its inhabitants");
                    Console.WriteLine($"You gain {room.EventArray[0].effect + XPAdd} experience");
                    Thread.Sleep(500);
                }
            }
            Utilities.Keypress();
            for (int i = 0; i < EventDisplay.Count; i++)
            {
                if (EventDisplay[i].flavor == "Some equipment")
                {
                    int itemRoll = Utilities.rand.Next(1, 3);
                    if (itemRoll == 1)
                    {
                        if (Shop.WeaponList[d.lootTier].effect > p.Weapon.effect)
                        {//If it's better than what you have, you get it
                            Console.WriteLine($"You find a {Shop.WeaponList[d.lootTier].name}! Excited, you equip it");
                            Utilities.Equip(Shop.WeaponList[d.lootTier]);
                        }//If not, well, sucks          
                        else Console.WriteLine($"You find a broken old weapon. It is of no use to anyone");

                    }
                    if (itemRoll == 2)
                    {
                        if (Shop.ArmorList[d.lootTier].effect > p.Armor.effect)
                        {//If it's better than what you have, you get it
                            Console.WriteLine($"You find a {Shop.ArmorList[d.lootTier].name}! Excited, you equip it");
                            Utilities.Equip(Shop.ArmorList[d.lootTier]);
                        }//If not, well, sucks 
                        else Console.WriteLine($"You find a broken old set of armour. It is of no use to anyone");
                    }
                    Thread.Sleep(500);
                }
                Utilities.Keypress();
            }            
        }
    }
}