using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class House
{    
    //Prefixes and suffixes of enhancements
    public static string[] magPrefix = new string[] { "", "Deadly", "Vibrant", "Affluent", "Knowledgeable" };
    public static string[] magSuffix = new string[] { "", "of death", "of life", "of wealth", "of wisdom" };
    //List of enhancements in the game
    public static List<Recipe> Enhancements = new List<Recipe>

    {
        new Recipe(magPrefix[1],0,2,0,2, Dungeon.drop[0], Dungeon.drop[1], Dungeon.drop[2],Dungeon.drop[3],80, EnhancementType.Damage,3),
        new Recipe(magPrefix[2],0,1,0,1, Dungeon.drop[0], Dungeon.drop[1], Dungeon.drop[2],Dungeon.drop[3],80, EnhancementType.Health,10),
        new Recipe(magPrefix[3],0,2,3,2, Dungeon.drop[0], Dungeon.drop[1], Dungeon.drop[2],Dungeon.drop[3],80, EnhancementType.Gold,15),
        new Recipe(magPrefix[4],0,4,1,0, Dungeon.drop[0], Dungeon.drop[1], Dungeon.drop[2],Dungeon.drop[3],80, EnhancementType.XP,5),
        new Recipe(magSuffix[1],0,2,0,2, Dungeon.drop[0], Dungeon.drop[1], Dungeon.drop[2],Dungeon.drop[3],80, EnhancementType.Damage,3),
        new Recipe(magSuffix[2],0,1,0,1, Dungeon.drop[0], Dungeon.drop[1], Dungeon.drop[2],Dungeon.drop[3],80, EnhancementType.Health,10),
        new Recipe(magSuffix[3],0,2,3,2, Dungeon.drop[0], Dungeon.drop[1], Dungeon.drop[2],Dungeon.drop[3],80, EnhancementType.Gold,15),
        new Recipe(magSuffix[4],0,4,1,0, Dungeon.drop[0], Dungeon.drop[1], Dungeon.drop[2],Dungeon.drop[3],80, EnhancementType.XP,5)
    };
    //When you gain access to an enhancement, it goes on this list
    public static List<Recipe> CraftMachineEnhancements = new List<Recipe>
    {
        Enhancements[0], Enhancements[1]
    };
    //When you go to your house
    public static void YourHouse(Creature p)
    {
        p.craft = true;
        Console.Clear();
        Utilities.ColourText(Colour.SPEAK, "You are in your house. It's not big, but it's clean and cozy. In the corner you see your bed.");
        if (p.craft == true) Utilities.EmbedColourText(Colour.SPEAK, Colour.RAREDROP, Colour.SPEAK, "\n", "In the center of the main room you have set up your ", "", "crafting machine", "", ". \nNow you just have to figure out how it works", "");
        Utilities.ColourText(Colour.HEALTH, "\n\n[B]");
        Console.Write("ed");
        //If the crafting machine is available, you see it
        if (p.craft == true) Utilities.EmbedColourText(Colour.RAREDROP, "                  ","[C]","rafting machine");
        Console.WriteLine("\n[R]eturn to town\n");
        Utilities.EmbedColourText(Colour.ENERGY, Colour.ENERGY, Colour.ENERGY, Colour.ENERGY, "It is day ", $"{Time.day}", ", the ", $"{Time.weeks[Time.week]}", " week of ", $"{Time.months[Time.month]}", ", ", $"{Time.year}", "\n\n");
        Console.WriteLine("\n\nWhat would you like to do?\n\n");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (choice == "r") Marburgh.Program.GameTown();
        else if (choice == "b")
        {
            Console.Clear();
            Console.WriteLine("You sleep until morning.");
            Time.DayChange(1, p);
        }
        //If you hit c and it's available, you get to choose how to craft
        if (choice == "c" && p.craft == true) Craft(p); 
        YourHouse(p);
    }
    //Here you choose, Boss weapons or Enhancements
    private static void Craft(Creature p)
    {
        Console.Clear();
        Console.WriteLine("It appears this machine can craft enhancements for your weapons and armor," +
            " as well as exotic weapons from fallen bosses\n");
        Utilities.EmbedColourText(Colour.ITEM,Colour.RAREDROP,"","[B]", "oss weapons" ,"            [E]","nhancements\n\n[R]eturn to your house\n");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (choice == "r") YourHouse(p);
        if (choice == "b") BossWeapon(p);
        if (choice == "e") Enhancement(p);
    }

    private static void BossWeapon(Creature p)
    {
        
    }

    public static void Enhancement(Creature p)
    {
        Console.WriteLine("Would you like to enhance your [W]eapon or you [A]rmor?\n\n");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();

        if (choice == "w")
        {
            Console.WriteLine($"Would you like to enhance your {p.Weapon.name}?\n\n[Y]es        [N]o\n\n");
            string confirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (confirm =="y") ItemChange(p, p.Weapon);
        }
        if (choice == "a")
        {
            Console.WriteLine($"Would you like to enhance your {p.Armor.name}?\n\n[Y]es        [N]o\n\n");
            string confirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (confirm == "y") ItemChange(p, p.Armor);
        }

    }

    private static void ItemChange(Creature p, Equipment item)
    {
        int choice;
        do
        {
            Console.Clear();
            for (int i = 0; i < CraftMachineEnhancements.Count; i++)
            {
                //List each enhancement, under it the items required to craft
                if (item.isWeapon == false && CraftMachineEnhancements[i].name == "Deadly") Utilities.EmbedColourText(Colour.SPEAK, $"\n[{i + 1}] ", $"Hardened", "");
                else Utilities.EmbedColourText(Colour.magColour[(int)CraftMachineEnhancements[i].type + 1], $"\n[{i + 1}] ", $"{CraftMachineEnhancements[i].name}", "");
                if (CraftMachineEnhancements[i].ingredient1Amount > 0) Utilities.ColourText(Colour.DROP, $"{CraftMachineEnhancements[i].ingredient1Amount} {CraftMachineEnhancements[i].ingredient1.name}\n");
                if (CraftMachineEnhancements[i].ingredient2Amount > 0) Utilities.ColourText(Colour.DROP, $"{CraftMachineEnhancements[i].ingredient2Amount} {CraftMachineEnhancements[i].ingredient2.name}\n");
                if (CraftMachineEnhancements[i].ingredient3Amount > 0) Utilities.ColourText(Colour.DROP, $"{CraftMachineEnhancements[i].ingredient3Amount} {CraftMachineEnhancements[i].ingredient3.name}\n");
                if (CraftMachineEnhancements[i].ingredient4Amount > 0) Utilities.ColourText(Colour.DROP, $"{CraftMachineEnhancements[i].ingredient4Amount} {CraftMachineEnhancements[i].ingredient4.name}\n");
            }
            Console.WriteLine("\n\n[0] Return \n");
            //Select your enhancement
        } while (!int.TryParse(Console.ReadLine(), out choice));
        //bool ing1 = false, ing2= false, ing3= false, ing4= false;
        //if (CraftMachineEnhancements[choice - 1].ingredient1Amount < 0) ing1 = true;
        //else;
        //if (CraftMachineEnhancements[choice - 1].ingredient1Amount < 0) ing2 = true;
        //else;
        //if (CraftMachineEnhancements[choice - 1].ingredient1Amount < 0) ing3 = true;
        //else;
        //if (CraftMachineEnhancements[choice - 1].ingredient1Amount < 0) ing4 = true;
        //else;
        //if (ing1 && ing2 && ing3 && ing4) 

        //If what you selected is an option...
        if (choice > 0 && choice < CraftMachineEnhancements.Count)
        {            
            //Add the prefix to the item info
            item.preName = CraftMachineEnhancements[choice - 1].name;
            //Change the colour
            item.frontEnhance = choice;
            //Actually affect the item
            if (CraftMachineEnhancements[choice - 1].type == EnhancementType.Damage)
            {
                item.damageEffect += CraftMachineEnhancements[choice - 1].effect;
                item.goldEffect = item.xpEffect = item.healthEffect = 0;
                p.health = (p.health > p.maxHealth) ? p.maxHealth : p.health;
            }
            if (CraftMachineEnhancements[choice - 1].type == EnhancementType.Health)
            {
                item.healthEffect += CraftMachineEnhancements[choice - 1].effect;
                p.health += CraftMachineEnhancements[choice - 1].effect;
                item.damageEffect = item.xpEffect = item.goldEffect = 0;
            }
            if (CraftMachineEnhancements[choice - 1].type == EnhancementType.Gold)
            {
                item.goldEffect = CraftMachineEnhancements[choice - 1].effect;
                item.damageEffect = item.xpEffect = item.healthEffect = 0;
                p.health = (p.health > p.maxHealth) ? p.maxHealth : p.health;
            }
            if (CraftMachineEnhancements[choice - 1].type == EnhancementType.XP)
            {
                item.xpEffect = CraftMachineEnhancements[choice - 1].effect;
                item.damageEffect = item.goldEffect = item.healthEffect = 0;
                p.health = (p.health > p.maxHealth) ? p.maxHealth : p.health;
            }
        }
        Utilities.Keypress();
    }
}