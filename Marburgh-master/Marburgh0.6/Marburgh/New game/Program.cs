using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;

namespace New_game
{
    class Program
    {
        public static int day = 1;
        public static int deathDay = 3;
        public static int dayMax = 15;
        public static int attack; 
        public static string pName;
        public static Creature p;
        public static Monster mon;
        public static Shop shop;
        public static Equipment[] WeaponList = new Equipment[] { new Equipment("None", 0, 0), new Equipment("Dagger", 2, 50), new Equipment("Short Sword", 5, 400), new Equipment("Arming Sword", 8, 800), new Equipment("Long Sword", 12, 1200), new Equipment("Broad Sword", 15, 1500), new Equipment("Great Sword", 20, 2000) };
        public static Equipment[] ArmorList = new Equipment[] { new Equipment("None", 0, 0), new Equipment("Cloth Armor", 2, 50), new Equipment("Old Leather Armor", 5, 400), new Equipment("Leather Armor", 8, 800), new Equipment("Chain mail", 12, 50), new Equipment("Coat of plates", 15, 1500), new Equipment("Plate mail", 20, 2000) };
        public static pClass[] MonsterClassList = new pClass[] { new pClass("Slime", 7, 3, 0, 0), new pClass("Goblin", 5, 4, 0, 0), new pClass("Kobald", 6, 3, 0, 0),  new pClass("Skeleton", 8, 5, 0, 0), new pClass("Orc", 10, 6, 0, 0), new pClass("Minotaur", 20, 13, 0, 0), new pClass("Ogre", 25, 15, 0, 0) };
        public static pClass Boss = new pClass("Boss", 120, 45, 0, 0);
        public static pClass Warrior = new pClass("Warrior", 12, 4, 0, 0);
        public static pClass Rogue = new pClass("Rogue", 10, 5, 1, 1);
        public static pClass Mage = new pClass("Mage", 8, 3, 3, 3);
        public static pClass[] HeroList = new pClass[] { Warrior, Rogue, Mage };
        public static Random rand = new Random();
        public static Shop WeaponShop = new Shop("Billford's weapon emporium.", "Billford", "troll", "Greetings, What can I do for you", WeaponList);
        public static Shop ArmorShop = new Shop("Alya's armor shop.", "Alya", "elf", "Hey there! Looking to buy some armor?", ArmorList);

        //Game Info Functions
        static void Main(string[] args)
        {
            Welcome();
        }

        private static void AttackMonster()
        {
            Console.WriteLine($"You hit the {mon.name} for {attack} damage");
            mon.health -= attack;
        }

        private static void AttackPlayer()
        {
            Console.WriteLine($"The {mon.name} hits you for {mon.damage-p.Armor.effect} damage");
            p.health -= mon.damage-p.Armor.effect;
        }

        private static void AttackSelect()
        {
            Console.WriteLine("\n\nWhat would you like to do?");
            string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (choice == "a")
            {
                attack = p.damage + p.Weapon.effect;
                return;
            }
            else if (choice == "b" && p.pClass.cName == "Rogue" && p.energy > 0)
            {
                attack = p.damage + p.Weapon.effect * 3;
                p.energy--;
                return;
            }
            else if (choice == "f" && p.pClass.cName == "Mage" && p.energy > 0)
            {
                attack = p.damage + p.magic * 4;
                p.energy--;
                return;
            }
            else if (choice == "b" && p.pClass.cName == "Rogue" && p.energy < 1 || choice == "f" && p.pClass.cName == "Mage" && p.energy < 1)
            {
                Console.WriteLine("You don't have enough energy!");
                Keypress();
            }
            else if (choice == "c")
                CharacterSheet();
            else if (choice == "h")
                Heal();
            else if (choice == "r")
                GameDungeon();
            GameCombat();
        }

        private static void CharacterSheet()
        {
            Console.Clear();
            Console.WriteLine($"Name: {p.pName}");
            Console.WriteLine($"Class: {p.pClass.cName}");
            if (p.level == p.levelMax) Console.WriteLine("\nYOU ARE MAX LEVEL");
            else Console.WriteLine($"\nLevel: {p.level}");
            if (p.xp >= p.xpRequired) Console.WriteLine("YOU ARE ELIGIBLE FOR A LEVEL RAISE");
            else Console.WriteLine($"Experience: {p.xp}/{p.xpRequired}");            
            Console.WriteLine($"\nGold: {p.gold}");
            Console.WriteLine($"Gold In Bank: {p.bankGold}\n");            
            Console.WriteLine($"Health: {p.health}/{p.maxHealth}");
            Console.WriteLine($"Energy: {p.energy}/{p.maxEnergy}");
            Console.WriteLine($"Spellpower: {p.magic}\n");
            Console.WriteLine($"Weapon: {p.Weapon.name}");
            Console.WriteLine($"Armor: {p.Armor.name}\n");
            Console.WriteLine($"Damage: {p.damage + p.Weapon.effect}");
            Console.WriteLine($"Mitigation: {p.Armor.effect}\n");
            Console.WriteLine($"Potions: {p.potions}");
            Keypress();
        }

        private static void CharacterCreate()
        {
            NameSelect();
            CharacterSelect();
            GameTown();
        }

        private static void CharacterSelectConfirm(Creature p)
        {
            Console.WriteLine($"\n\n\nSo {p.pName} the {p.pClass.cName}?\n\n[Y]es      [N]o\n");
            string confirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (confirm == "y") return;
            CharacterSelect();
        }

        private static void CharacterSelect()
        {
            Console.Clear();
            Console.WriteLine("Please select a class\n\n[W]arrior     [R]ogue         [M]age");
            string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (choice == "w") p = new Hero(pName, Warrior, WeaponList[0], ArmorList[1]);
            else if (choice == "r") p = new Hero(pName, Rogue, WeaponList[1], ArmorList[0]);
            else if (choice == "m") p = new Hero(pName, Mage, WeaponList[0], ArmorList[0]);
            else CharacterSelect();
            CharacterSelectConfirm(p);
        }

        private static void DayCheck()
        {
            if (day > dayMax)
                GameOver();
            Console.WriteLine($"\nThere are {dayMax - day} days left");
            Keypress();
        }

        private static void Death()
        {
            Console.Clear();
            Console.WriteLine("YOU DIED!");
            Console.WriteLine($"\n\nYou have been revived at your house. The ordeal took {deathDay} days.\nYou awake with full health and energy, but they took your gold for your troubles.\nYou lose all gold and experience");            
            day += deathDay;
            p.gold = 0;
            p.xp = 0;
            Refresh();
            DayCheck();
            Save();
            Keypress();
            GameTown();
        }

        private static void DungeonSearch()
        {
            p.fights--;
            Console.WriteLine("\n\nYou find...");
            int dungeonRoll = rand.Next(1, 6);
            if (dungeonRoll < 5) MonsterSummon();
            else ItemFind();
        }

        private static void Gamble()
        {
            Console.WriteLine("You walk into the tavern and find a dice game. A grisled old man asks if you'd like to play\n\n[Y]es     [N]o");
            string confirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (confirm == "y")
            {
                if (p.gold > 0)
                {
                    int wager;
                    do
                    {
                        Console.WriteLine($"\nYou have {p.gold} gold\nHow much would you like to wager?\n[0] Return\n");
                    } while (!int.TryParse(Console.ReadLine(), out wager));
                    if (p.gold >= wager)
                    {
                        Console.WriteLine($"\nYou want to wager {wager} gold?\n\n[Y]es     [N]o");
                        string wagerConfirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
                        if (wagerConfirm != "y") return;
                        else
                        {
                            int playerRoll = rand.Next(1, 7);
                            int opponentRoll = rand.Next(1, 7);
                            p.gold -= wager;
                            Console.Clear();
                            Console.WriteLine($"Confident, you set {wager} gold on the table");
                            Console.Write($"\nYou roll a die, it comes up.");
                            RollDice(playerRoll);
                            Console.Write($"\nYour opponent rolls a die, it comes up.");
                            RollDice(opponentRoll);
                            string report = (playerRoll == opponentRoll) ? "\n\nIt's a tie!\nYou take your money back" : (playerRoll > opponentRoll) ? "\n\nYou win!\nYou get double your wager back!" : "\n\nYou lose!\nDisgusted, you leave the tavern";
                            Console.WriteLine(report);
                            p.gold = (playerRoll == opponentRoll) ? p.gold+wager : (playerRoll > opponentRoll) ? p.gold + 2 * wager : p.gold;
                            Keypress();
                            return;
                        }
                    }
                }
                Console.WriteLine("You don't have enough money!");
                Keypress();
            }
        }

        private static void GameOver()
        {
            Console.Clear();
            Console.WriteLine("Time has run out!");
            Console.WriteLine("\n\nYou tried.\nYou failed but you tried.\nAnd in the end, is that not the real victory?\nThe answer is no.\n\nGoodbye!");
            Keypress();
            Environment.Exit(0);
        }
                
        private static void Heal()
        {
            if (p.health == p.maxHealth)
                Console.WriteLine("You don't need healing!");
            else if (p.potions < 1)
                Console.WriteLine("You don't have enough potions!");
            else
            {
                p.health = p.maxHealth;
                p.potions -= 1;
                Console.WriteLine("You heal to full health");
            }
            Keypress();
        }

        private static void Help()
        {
            Console.WriteLine("\n\nThis is a very simple dungeon crawler. Descend the dungeon to the final level and defeat the boss to win!");
            Console.WriteLine("WEAPONS,ARMOR,ITEMS\nUse the shops to outfit your character with better gear and healing potions");
            Keypress();
        }

        private static void House()
        {
            Console.WriteLine($"Would you like to return home to sleep?\nDoing so will end the day and restore your health, energy and dungeon fights\n\n[Y]es    [N]o\n\n");
            string confirm = Console.ReadKey(true).KeyChar.ToString();
            if (confirm == "y")
            {
                Refresh();
                day ++ ;
                Console.WriteLine($"\nYou wake up the following day feeling refreshed!\n");
                DayCheck();
            }
            GameTown();
        }
                
        private static void ItemFind()
        {
            int rewardRoll;
            Thread.Sleep(300);
            int itemRoll = rand.Next(1, 8);
            if (itemRoll == 1 || itemRoll == 2)
            {
                rewardRoll = rand.Next(45, 89);
                Console.WriteLine($"Gold! You gain {200 * p.level + rewardRoll} gold!");
                p.gold += 200 * p.level + rewardRoll;
            }
            else if (itemRoll == 3 || itemRoll == 4)
            {
                rewardRoll = rand.Next(5, 9);
                Console.WriteLine($"A book! Reading it gives you {rewardRoll * p.level} experience!");
                p.xp += rewardRoll * p.level;
            }
            else if (itemRoll == 5 || itemRoll == 6)
            {
                if ((p.pClass.cName == "Warrior" && p.potions < 1) || (p.pClass.cName == "Rogue" && p.potions < 1) || (p.pClass.cName == "Mage" && p.potions < 2))
                {
                    Console.WriteLine($"A potion! You gain 1 potion!");
                    p.potions++;
                }
                else Console.WriteLine("Nothing..... Well, that's disapointing");
            }
            else if (itemRoll == 7)
            {
                int equipRoll = rand.Next(1, 3);
                if (equipRoll == 1)
                {
                    if (p.Weapon.effect >= WeaponList[p.level / 2].effect)
                        Console.WriteLine($"An old broken weapon.\nCursing your almost good luck, you move on");
                    else
                    {
                        Console.WriteLine($"A {WeaponList[p.level / 2].name}!\nExcited, you equip it and move on");
                        p.Weapon = WeaponList[p.level / 2];
                    }
                }
                else
                {
                    if (p.Armor.effect >= ArmorList[p.level / 2].effect)
                        Console.WriteLine($"An old broken set of armor.\nCursing your almost good luck, you move on");
                    else
                    {
                        Console.WriteLine($"A {ArmorList[p.level / 2].name}!\nExcited, you equip it and move on");
                        p.Armor = ArmorList[p.level / 2];
                    }
                }
            }
            Keypress();
        }

        private static void ItemShop()
        {
            Console.Clear();
            Console.WriteLine("You eneter a dingy shop. A fat little man walks up to you quickly.\n'Hello there! Are you here to buy potions?\n\n[Y]es      [N]o\n");
            string confirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (confirm =="y")
            {
                int amount = (p.pClass.cName == "Mage") ? 2 : 1;
                int buymax = ((amount - p.potions == 1 && p.gold / 100 >= 1) || (amount - p.potions == 2 && p.gold / 100 == 1)) ? 1:(amount - p.potions == 2 && p.gold / 100 >= 2) ? 2 : 0;
                int buyChoice;
                do
                {
                    Console.WriteLine($"'Excellent! how many would you like to buy? They are 200 gold apiece and you can have {amount} max'\n\nYou can buy {buymax} potions\n[0] Return\n");
                } while (!int.TryParse(Console.ReadLine(), out buyChoice));
                if (buyChoice == 0) return;
                else if(buyChoice>buymax) Console.WriteLine("You have too many potions!");
                else if (p.gold< buyChoice *200) Console.WriteLine("'I'm sorry, it doesn't look like you can afford that'");
                else
                {
                    Console.WriteLine("'A pleasure doing business with you!'");
                    Console.WriteLine($"You give the man {buyChoice *200} gold and receive {buyChoice} potions");
                    p.gold -= buyChoice * 200;
                    p.potions += buyChoice;
                }
                Keypress();
            }
        }

        private static void Keypress()
        {
            Console.WriteLine("\n\nPress any key to continue");
            Console.ReadKey(true);
        }

        private static void LevelMaster()
        {
            if (p.level == p.levelMax) Console.WriteLine("YOU ARE MAX LEVEL");
            else
            {
                if (p.xp < p.xpRequired) Console.WriteLine("Come back when you are more experienced");
                else
                {
                    Console.WriteLine("Congrats! You have gained a level!");
                    p.maxEnergy += p.pClass.startingEnergy;
                    p.maxHealth += p.pClass.startingHealth;
                    p.health = p.maxHealth;
                    p.energy = p.maxEnergy;
                    p.xp -= p.xpRequired;
                    p.level += 1;
                    p.xpRequired = p.level * 20 + 5;
                    p.damage += p.pClass.startingDamage;
                }
            }
            Keypress();
        }

        private static void Load()
        {
            Console.WriteLine("What is the name of the character you would like to load?");
            string charname = Console.ReadLine();
            string readText = File.ReadAllText($"{charname}.txt");
            string[] line = readText.Split(',');
            if (line.Length == 2) WinStats(line);
            string[] num = new string[] { line[2], line[5], line[6], line[7], line[8], line[9], line[10], line[11], line[12], line[13], line[14], line[15], line[16], line[17], line[18], line[19], line[20] };
            int[] number = Array.ConvertAll(num, s => int.Parse(s));
            p = new Hero(pName, Warrior, WeaponList[0], ArmorList[1]);
            p.pName = line[0];
            for (int i = 0; i < HeroList.Length; i++)
            {
                if (line[1] == HeroList[i].cName) p.pClass = HeroList[i];
            }
            
            for (int i = 0; i < ArmorList.Length; i++)
            {
                if(line[3] == WeaponList[i].name) p.Weapon = WeaponList[i];

                if(line[4] == ArmorList[i].name) p.Armor = ArmorList[i];
            }
            p.potions = number[0];
            p.gold = number[1];
            p.bankGold = number[2];
            p.damage = number[3];
            p.energy = number[4];
            p.maxEnergy = number[5];
            p.fights = number[6];
            p.fightsMax = number[7];
            p.health = number[8];
            p.maxHealth = number[9];
            p.level = number[10];
            p.levelMax = number[11];
            p.magic = number[12];
            p.maxMagic = number[13];
            p.xp = number[14];
            p.xpRequired = number[15];
            day = number[16];
            Console.WriteLine("Your data has been loaded");
            Keypress();
            GameTown();
        }

        private static void WinStats(string[] line)
        {
            Console.WriteLine("/n");
            Console.WriteLine("/n");
            Console.WriteLine("/n");
            Console.Write(line[0]);
            Console.Write(line[1]);            
            Keypress();
            Environment.Exit(0);
        }

        private static void MonsterSummon()
        {
            Thread.Sleep(300);
            Monster[] MonsterList = new Monster[] { new Monster("Slime", "*Squish*", MonsterClassList[0], 35, 4, " Slime"), new Monster("Goblin", "Scatter!", MonsterClassList[1], 45, 3, " Goblin"), new Monster("Kobald", "Leave my candles alone!", MonsterClassList[2], 35, 3, " Kobald"), new Monster("Skeleton", "*Creak*", MonsterClassList[3], 40, 4, " Skeleton"), new Monster("Orc", "You're dead!", MonsterClassList[4], 49, 5, "n Orc"), new Monster("Ogre", "RAAAWR", MonsterClassList[6], 110, 10, " Ogre"), new Monster("Minotaur", "*Snort*", MonsterClassList[5], 100, 8, " Minotaur") };
            int monsterChoice = rand.Next(0, 5);
            int monsterChoice2 = rand.Next(0, 7);
            if (p.level<3) mon = MonsterList[monsterChoice];
            else mon = MonsterList[monsterChoice2];
            Console.WriteLine($"A{mon.summonName}!");
            mon.health *= p.level;
            mon.health -= p.level;
            if (p.level > 1) mon.damage *= p.level / 2 + p.level;
            Keypress();
            GameCombat();
        }

        private static void NameSelect()
        {
            Console.Clear();
            Console.WriteLine("What is your name?");
            pName = Console.ReadLine();
            Console.WriteLine($"\n\n\n{pName}, is that correct?\n\n[Y]es      [N]o\n");
            string confirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (confirm == "y") return;
            else NameSelect();
        }

        private static void Reward()
        {
            if (mon.name == "Marcotte") Win();
            Console.WriteLine($"\n\nYou kill the {mon.name}!");
            Console.WriteLine($"\nYou find {mon.gold} gold!");
            Console.WriteLine($"You earn {mon.xp} experience!");
            p.gold += mon.gold;
            p.xp += mon.xp;
            Keypress();
            GameDungeon();
        }

        private static void Refresh()
        {
            p.health = p.maxHealth;
            p.energy = p.maxEnergy;
            p.fights = p.fightsMax;
        }

        private static void RollDice(int roll)
        {
            Thread.Sleep(300);
            Console.Write($".");
            Thread.Sleep(300);
            Console.Write($".");
            Thread.Sleep(300);
            Console.Write($"{roll}!");
            Thread.Sleep(400);
        }

        private static void Save()
        {
            FileStream filestream = new FileStream($"{p.pName}.txt", FileMode.Create);
            var streamwriter = new StreamWriter(filestream);
            streamwriter.AutoFlush = true;
            Console.SetOut(streamwriter);
            Console.SetError(streamwriter);
            Console.WriteLine($"{p.pName},{p.pClass.cName},{p.potions},{p.Weapon.name},{p.Armor.name},{p.gold},{p.bankGold},{p.damage},{p.energy},{p.maxEnergy},{p.fights},{p.fightsMax},{p.health},{p.maxHealth},{p.level},{p.levelMax},{p.magic},{p.maxMagic},{p.xp},{p.xpRequired},{day},{p.win}");
            StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
            Console.WriteLine("\nYour character has been saved!");
            Keypress();
        }

        private static void ShopBuy(Equipment equip, Equipment playerEquip)
        {
            Console.WriteLine($"I see you have a {equip.name}. Would you like to sell it?\n\n[Y]es      [N]o\n");
            string confirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (confirm == "y")
            {
                Console.WriteLine($"{ shop.shopkeepName} takes your {equip.name} and gives you {equip.price / 2} gold");
                p.gold += equip.price / 2;
                Console.WriteLine($"Smiling, {shop.shopkeepName} takes your money and gives you your {playerEquip.name }");
                p.gold -= playerEquip.price;
                if (shop.shopkeepName == "Billford") p.Weapon = playerEquip;
                else if (shop.shopkeepName == "Alya") p.Armor = playerEquip;
            }
        }                       

        private static void Quit()
        {
            Console.WriteLine("Are you sure you want to quit?\n\n[Y]es      [N]o\n");
            string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (choice == "y")
                Environment.Exit(0);
        }

        private static void Win()
        {
            Console.WriteLine("Thanks for playing my game!\nThat's all there is for now but if there's any interest whatsoever i'd love to add more to it.\nMonsters, bosses, dungeons, events, I've got a lot of ideas");
            Console.WriteLine("Your Name, days played, weapon, armor, and gold have been stored");
            FileStream filestream = new FileStream($"{p.pName}.txt", FileMode.Create);
            var streamwriter = new StreamWriter(filestream);
            streamwriter.AutoFlush = true;
            Console.SetOut(streamwriter);
            Console.SetError(streamwriter);
            Console.WriteLine($"{p.pName}, has won the game on day {day} with {p.gold+p.bankGold} gold.");
            StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
            Keypress();
            Environment.Exit(0);
        }

        private static void Welcome()
        {
            Console.WriteLine("                     _                         _     ");
            Console.WriteLine("/'\\_/`\\             ( )                       ( )    ");
            Console.WriteLine("|     |   _ _  _ __ | |_    _   _  _ __   __  | |__  ");
            Console.WriteLine("| (_) | /'_` )( '__)| '_`\\ ( ) ( )( '__)/'_ `\\|  _ `\\ ");
            Console.WriteLine("| | | |( (_| || |   | |_) )| (_) || |  ( (_) || | | |");
            Console.WriteLine("(_) (_)`\\__,_)(_)   (_,__/'`\\___/'(_)  `\\__  |(_) (_)");
            Console.WriteLine("                                       ( )_) |       ");
            Console.WriteLine("                                        \\___/'       ");
            Console.WriteLine("");
            Console.WriteLine("[N]ew Game       [L]oad game\n");
            string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (choice == "n") CharacterCreate();
            else if (choice == "l") Load();
            else Welcome();
        }

        //GAME FUNCTIONS

        private static void GameBank()
        {
            Console.Clear();
            Console.WriteLine("You enter a small but busy bank. One teller appears to be free. You walk up to him");
            Console.WriteLine("\n'Hello. How may I be of service?'");
            Console.WriteLine("\n\n(D)eposit        (W)ithdraw          (R)eturn to town\n\n");
            Console.WriteLine($"Gold:{p.gold}         Gold in bank:{p.bankGold}\n");
            string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (choice == "d")
            {
                if (p.gold > 0)
                {
                    int deposit;
                    do
                    {
                        Console.WriteLine("'Excellent! How much would you like to deposit?'\n[0] Return \n");
                    } while (!int.TryParse(Console.ReadLine(), out deposit));
                    if (p.gold >= deposit)
                    {
                        p.gold -= deposit;
                        p.bankGold += deposit;
                        Console.WriteLine($"You deposit {deposit} gold.");
                    }
                    else Console.WriteLine("'You don't have enough money!'");
                }
                else Console.WriteLine("'You don't have any money!'");
                Keypress();
            }
            else if (choice == "w")
            {
                if (p.bankGold > 0)
                {
                    int withdraw;
                    do
                    {
                        Console.WriteLine("'Excellent! How much would you like to withdraw?'\n[0] Return "); Console.ResetColor();
                    } while (!int.TryParse(Console.ReadLine(), out withdraw));
                    if (p.bankGold >= withdraw)
                    {
                        p.gold += withdraw;
                        p.bankGold -= withdraw;
                        Console.WriteLine($"You withdraw {withdraw} gold.");
                    }
                    else Console.WriteLine("'You don't have enough money in the bank!'");
                }
                else Console.WriteLine("'You don't have any money in the bank!'");
                Keypress();
            }
            else if (choice == "r")
                GameTown();
            GameBank();
        }

        private static void GameCombat()
        {
            Console.Clear();
            int playerHitRoll = rand.Next(1, 101);
            int monsterHitRoll = rand.Next(1, 101);
            int damageRoll = rand.Next(1 * (p.level / 2), 3 * (p.level / 2));
            if (p.level == 5) Console.WriteLine($"You are facing {mon.name}");
            else Console.WriteLine($"You are facing a {mon.name}");
            Console.Write($"'{mon.taunt}'");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine($"You                                 {mon.name}");
            Console.WriteLine($"Health: {p.health}/{p.maxHealth}    Health: {mon.health}");
            Console.WriteLine($"Energy: {p.energy}/{p.maxEnergy}    Energy: {mon.energy}");
            Console.WriteLine("");
            Console.WriteLine("[A]ttack     [H]eal      [R]un");
            if (p.pClass.cName == "Mage")
                Console.WriteLine("[F]ireball");
            else if (p.pClass.cName == "Mage")
                Console.WriteLine("[B]ackstab");
            AttackSelect();            
            attack += damageRoll;
            Console.WriteLine("");
            if (playerHitRoll < 81) AttackMonster();            
            else Console.WriteLine($"You miss the {mon.name}!");
            if (mon.health < 1) Reward();
            Thread.Sleep(400);
            if (monsterHitRoll < 81) AttackPlayer();
            else Console.WriteLine($"The {mon.name} misses you!");
            Keypress();
            if (p.health < 1) Death();
            if (p.health > 0 && mon.health > 0) GameCombat();
        }

        private static void GameDungeon()
        {
            Console.Clear();
            if (p.level == p.levelMax)
            {
                Console.WriteLine($"You are on the lowest dungeon level. It looks... different. There is treasure everywhere!\nBut there is also Marcotte");
                Console.WriteLine($"Are you ready for your final showdown?");
                string confirm = Console.ReadKey(true).KeyChar.ToString();
                if (confirm == "y")
                {
                    mon = new Monster("Marcotte", "End of the line", Boss, 0, 0, " Marcotte");
                    GameCombat();
                }
                else GameTown();
            }
            Console.WriteLine($"You are in dungeon level {p.level}");
            if (p.fights < 1)
            {
                Console.WriteLine("You are exhausted. Go get some rest");
                Keypress();
                GameTown();
            }                
            Console.WriteLine($"\n[L]ook for a monster    [H]eal");
            Console.WriteLine($"[C]haracter             [R]eturn to town");
            Console.WriteLine($"\nWhat would you like to do?");
            Console.WriteLine($"You have {p.fights} fights left");
            if (p.xp >= p.xpRequired) Console.WriteLine("\nYOU ARE ELIGIBLE FOR A LEVEL RAISE");
            string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (choice == "l")
                DungeonSearch();
            else if (choice == "c")
                CharacterSheet();
            else if (choice == "h")
                Heal();
            else if (choice == "r")
                GameTown();
            GameDungeon();
        }

        private static void GameShop()
        {
            Console.Clear();
            Console.WriteLine($"You walk into {shop.name}");
            Console.WriteLine($"{shop.shopkeepName} the {shop.shopkeepRace} comes over to greet you");
            Console.WriteLine($"'{shop.shopkeepGreeting}'\n\n");
            Console.WriteLine("[1] Buy      [2] Sell");
            Console.WriteLine("[C]haracter  [R]eturn\n\n");
            string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (choice == "1")
            {
                Console.WriteLine("Great! What would you like to buy?\n");
                for (int i = 1; i < shop.ItemList.Length; i++)
                {
                    Console.WriteLine($"[{i}] {shop.ItemList[i].name}  {shop.ItemList[i].price}");
                }
                Console.WriteLine("[0] Return");
                int buyChoice;
                do
                {

                } while (!int.TryParse(Console.ReadKey(true).KeyChar.ToString().ToLower(), out buyChoice));
                if (buyChoice > 0 && buyChoice < shop.ItemList.Length)
                {
                    if (p.gold < shop.ItemList[buyChoice].price) Console.WriteLine("\n\n'Sorry, you don't Have enough Gold'");
                    else
                    {
                        Console.WriteLine($"\n\nWould you like to buy {shop.ItemList[buyChoice].name}?\n\n[Y]es      [N]o\n\n");
                        string confirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
                        if (confirm == "y")
                        {
                            if (p.Weapon.name != "None" && shop.ItemList[1].name == "Dagger")
                                ShopBuy(p.Weapon, shop.ItemList[buyChoice]);
                            else if (p.Armor.name != "None" && shop.ItemList[1].name == "Cloth Armor")
                                ShopBuy(p.Armor, shop.ItemList[buyChoice]);
                            else
                            {
                                Console.WriteLine($"\n\nSmiling, {shop.shopkeepName} takes your money and gives you your {shop.ItemList[buyChoice].name}");
                                p.gold -= shop.ItemList[buyChoice].price;
                                if (shop.shopkeepName == "Billford") p.Weapon = shop.ItemList[buyChoice];
                                else if (shop.shopkeepName == "Alya") p.Armor = shop.ItemList[buyChoice];
                            }
                        }
                    }
                }
                Keypress();
            }
            else if (choice == "2")
            {
                if (p.Weapon.name == "None" && p.Armor.name == "None") Console.WriteLine("You have nothing to Sell!");
                else
                {
                    Console.WriteLine($"\n\nWhat would you like to Sell?\n\n");
                    List<Equipment> EquipmentList = new List<Equipment> { };
                    if (p.Weapon.name != "None")
                        EquipmentList.Add(p.Weapon);
                    if (p.Armor.name != "None")
                        EquipmentList.Add(p.Armor);
                    for (int i = 0; i < EquipmentList.Count; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {EquipmentList[i].name}  {EquipmentList[i].price / 2}");
                    }
                    Console.WriteLine("[0] Return");
                    int sellChoice;
                    do
                    {

                    } while (!int.TryParse(Console.ReadKey(true).KeyChar.ToString().ToLower(), out sellChoice));
                    if (sellChoice > 0 && sellChoice <= shop.ItemList.Length)
                    {
                        Console.WriteLine($"\n\nWould you Like to sell {EquipmentList[sellChoice - 1].name}? I'll give you {EquipmentList[sellChoice - 1].price / 2} for it\n\n[Y]es      [N]o\n\n");
                        string confirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
                        if (confirm == "y")
                        {
                            Console.WriteLine($"\n\n'Great!' {shop.shopkeepName} takes your {EquipmentList[sellChoice - 1].name} and gives you {EquipmentList[sellChoice - 1].price / 2} gold");
                            p.gold += EquipmentList[sellChoice - 1].price / 2;
                            if (p.Weapon == EquipmentList[sellChoice - 1]) p.Weapon = WeaponList[0];
                            if (p.Armor == EquipmentList[sellChoice - 1]) p.Armor = ArmorList[0];
                        }
                    }
                }
                Keypress();
            }
            else if (choice == "c") CharacterSheet();
            else if (choice == "r") GameTown();
            GameShop();
        }

        private static void GameTown()
        {            
            Console.Clear();
            Console.WriteLine("You are in the town of Marburgh. It is a small town, but is clearly growing. Who knows what will be here in a month?\n\n" +
                              "[W]eapon shop            [A}rmor shop            [I]tem shop          [D]ungeon");
            Console.WriteLine("[G]amble at the Tavern   [Y]our house            [B]ank               [V]isit level master ");
            Console.WriteLine("[C]haracter              [H]eal                  [Q]uit               [?]Help\n[S]ave");
            Console.WriteLine($"\n\nWhat would you like to do?\nIt is day {day}\n\n");
            string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (choice == "w")
            {
                shop = WeaponShop;
                GameShop();
            }
            if (choice == "s")
            {
                Save();
            }
            else if (choice == "a")
            {
                shop = ArmorShop;
                GameShop();
            }
            else if (choice == "d")
                GameDungeon();
            else if (choice == "h")
                Heal();
            else if (choice == "i")
                ItemShop();
            else if (choice == "?")
                Help();
            else if (choice == "c")
                CharacterSheet();
            else if (choice == "v")
                LevelMaster();
            else if (choice == "g")
                Gamble();
            else if (choice == "y")
                House();
            else if (choice == "b")
                GameBank();
            else if (choice == "q")
                Quit();    
            GameTown();
        }        
    }       
}