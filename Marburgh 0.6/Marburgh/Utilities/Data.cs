using System;
using System.IO;

public class Data
{
    public static void Load(Creature p)
    {
        
        Console.WriteLine("What is the name of the character you would like to load?");
        string charname = Console.ReadLine();
        if (!File.Exists($"{charname}.txt"))
        {
            Console.WriteLine("That file does not exist");
            Utilities.Keypress();
            Marburgh.Program.Welcome();
        }
        string readText = File.ReadAllText($"{charname}.txt");
        string[] line = readText.Split(',');
        if (line[20] == "true") Data.WinStats(line);
        string[] num = new string[] { line[2], line[5], line[6], line[7], line[8], line[9], line[10], line[11], line[12], line[13], line[14], line[15], line[16], line[17], line[18], line[19] };
        int[] number = Array.ConvertAll(num, s => int.Parse(s));
        p = new Hero(Create.Warrior, Create.f, Shop.WeaponList[0], Shop.ArmorList[1]);
        p.family.FirstName = line[0];
        for (int i = 0; i < Create.HeroList.Length; i++)
        {
            if (line[1] == Create.HeroList[i].cName) p.pClass = Create.HeroList[i];
        }

        for (int i = 0; i < Shop.ArmorList.Length; i++)
        {
            if (line[3] == Shop.WeaponList[i].name) p.Weapon = Shop.WeaponList[i];

            if (line[4] == Shop.ArmorList[i].name) p.Armor = Shop.ArmorList[i];
        }
        p.potions = number[0];
        p.gold = number[1];
        p.bankGold = number[2];
        p.damage = number[3];
        p.energy = number[4];
        p.maxEnergy = number[5];
        p.health = number[8];
        p.maxHealth = number[9];
        p.level = number[10];
        p.levelMax = number[11];
        p.magic = number[12];
        p.maxMagic = number[13];
        p.xp = number[14];
        Time.day = number[15];
        Console.WriteLine("Your data has been loaded");
        Utilities.Keypress();
        Marburgh.Program.GameTown();
    }    

    public static void Save(Creature p)
    {
        using (StreamWriter sw = File.CreateText($"{p.family.LastName}.txt"))
        {
            sw.WriteLine($"{p.family.LastName},{p.pClass.cName},{p.potions},{p.Weapon.name},{p.Armor.name},{p.gold},{p.bankGold},{p.damage},{p.energy},{p.maxEnergy},{p.health},{p.maxHealth},{p.level},{p.levelMax},{p.magic},{p.maxMagic},{p.xp},{Time.day},{p.win}");
        }
        Console.WriteLine("Your data has been saved");
        Utilities.Keypress();
    }

    public static void Win(Creature p)
    {
        Console.WriteLine("Thanks for playing my game!\nThat's all there is for now but if there's any interest whatsoever i'd love to add more to it.\nMonsters, bosses, dungeons, events, I've got a lot of ideas");
        Console.WriteLine("Your Name, days played, weapon, armor, and gold have been stored");
        if (!File.Exists("Winners.txt"))
        {
            using (StreamWriter sw = File.CreateText("Winners.txt"))
            {                   //0             1           2               3               4               5
                sw.WriteLine($"{p.family.LastName}, {p.pClass.cName}, day, {Time.day}, {p.gold + p.bankGold}, gold, {p.Weapon.name}, {p.Armor.name}, ---");
            }
        }
        else
        {
            using (StreamWriter sw = File.AppendText("Winners.txt"))
            {
                sw.WriteLine($"{p.family.LastName}, {p.pClass.cName}, day, {Time.day}, {p.gold + p.bankGold}, gold, {p.Weapon.name}, {p.Armor.name}, ---");
            }
        }
        Utilities.Keypress();
        p.win = true;
        Save(p);
        Environment.Exit(0);
    }

    public static void Winners()
    {
        if (!File.Exists("Winners.txt"))
        {
            Console.WriteLine("\n\nNo one has won yet!");
            Utilities.Keypress();
            Marburgh.Program.Welcome();
        }
        string readText = File.ReadAllText("Winners.txt");
        string[] line = readText.Split(',');
        for (int i = 0; i < line.Length; i++)
        {
            Console.WriteLine($"{line[i]}");
        }
        Utilities.Keypress();
        Marburgh.Program.Welcome();
    }

    public static void WinStats(string[] line)
    {
        Console.WriteLine("This character has won, and can no longer enter the game");
        Utilities.Keypress();
        Environment.Exit(0);
    }
}