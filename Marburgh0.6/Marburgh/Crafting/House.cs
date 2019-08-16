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
        new Recipe(magPrefix[1],0,3,0,0, Dungeon.drop[0], Dungeon.drop[1], Dungeon.drop[2],Dungeon.drop[3],80, EnhancementType.Damage,3,true),
        new Recipe(magPrefix[2],0,1,1,1, Dungeon.drop[0], Dungeon.drop[1], Dungeon.drop[2],Dungeon.drop[3],80, EnhancementType.Health,10,true),
        new Recipe(magPrefix[3],0,2,3,2, Dungeon.drop[0], Dungeon.drop[1], Dungeon.drop[2],Dungeon.drop[3],80, EnhancementType.Gold,15,true),
        new Recipe(magPrefix[4],0,4,1,0, Dungeon.drop[0], Dungeon.drop[1], Dungeon.drop[2],Dungeon.drop[3],80, EnhancementType.XP,5,true),
        new Recipe(magSuffix[1],0,2,0,2, Dungeon.drop[0], Dungeon.drop[1], Dungeon.drop[2],Dungeon.drop[3],80, EnhancementType.Damage,3,false),
        new Recipe(magSuffix[2],0,1,0,1, Dungeon.drop[0], Dungeon.drop[1], Dungeon.drop[2],Dungeon.drop[3],80, EnhancementType.Health,10,false),
        new Recipe(magSuffix[3],0,2,3,2, Dungeon.drop[0], Dungeon.drop[1], Dungeon.drop[2],Dungeon.drop[3],80, EnhancementType.Gold,15,false),
        new Recipe(magSuffix[4],0,4,1,0, Dungeon.drop[0], Dungeon.drop[1], Dungeon.drop[2],Dungeon.drop[3],80, EnhancementType.XP,5,false)
    };
    //When you gain access to an enhancement, it goes on this list
    public static List<Recipe> CraftMachineEnhancements = new List<Recipe>
    {
        Enhancements[0], Enhancements[1]
    };
    //When you go to your house
    public static void YourHouse(Creature p)
    {
        Console.Clear();
        Utilities.ColourText(Colour.SPEAK, "You are in your house. It's not big, but it's clean and cozy. In the corner you see your bed.");
        if (p.craft == true) Utilities.EmbedColourText(Colour.SPEAK, Colour.RAREDROP, Colour.SPEAK, "\n", "In the center of the main room you have set up your ", "", "crafting machine", "", ". \nNow you just have to figure out how it works", "");
        Utilities.ColourText(Colour.HEALTH, "\n\n[B]");
        Console.Write("ed");
        //If the crafting machine is available, you see it
        if (p.craft == true) Utilities.EmbedColourText(Colour.RAREDROP, "                  [1]","Crafting machine","");
        Console.WriteLine("\n[C]haracter            [R]eturn to town\n");
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
        if (choice == "1" && p.craft == true) Craft(p);        
        if (choice == "x")
        {
            for (int i = 0; i < p.Drops.Count; i++)
            {
                if (p.Drops[i] == Dungeon.drop[1]) p.Drops[i].amount++;
            }
            if (!p.Drops.Contains(Dungeon.drop[1])) p.Drops.Add(Dungeon.drop[1]);
        }
        if (choice == "c") Utilities.CharacterSheet(p);
        YourHouse(p);
    }
    //Here you choose, Boss weapons or Enhancements
    private static void Craft(Creature p)
    {
        Console.Clear();
        Console.WriteLine("It appears this machine can craft enhancements for your weapons and armor," +
            " as well as exotic weapons from fallen bosses\n");
        Utilities.EmbedColourText(Colour.ITEM, Colour.ENHANCEMENT, "", "[B]", "oss weapons", "            [E]", "nhancements\n\n[R]eturn to your house\n");
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
        if (p.Armor.name == "None" && p.Weapon.name == "None")
        {
            Utilities.EmbedColourText(Colour.ENHANCEMENT, "", "You don't have any equipment to enhance!", "");
            Utilities.Keypress();
        }
        else
        {
            if (p.Armor.name != "None" && p.Weapon.name != "None")
            {
                Console.Write(Colour.ENHANCEMENT + "Enhance " + Colour.RESET + "your " + Colour.ITEM + "[W]" + Colour.RESET + "eapon or your " + Colour.ITEM + "[A]" + Colour.RESET + "rmor?\n\n");
                string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
                if (choice == "w" && p.Weapon.name != "None") EnhanceConfirm(p, p.Weapon);
                if (choice == "a" && p.Armor.name != "None") EnhanceConfirm(p, p.Armor);
            }
            else if (p.Armor.name != "None" && p.Weapon.name == "None") EnhanceConfirm(p, p.Armor);
            else if (p.Armor.name == "None" && p.Weapon.name != "None") EnhanceConfirm(p, p.Weapon);
        }
    }

    public static void EnhanceConfirm(Creature p, Equipment item)
    {
        Utilities.EmbedColourText(Colour.ENHANCEMENT, Colour.ITEM, "", "\n\nEnhance", " your ", $"{item.name}", "?\n\n[Y]es        [N]o\n\n");
        string confirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (confirm == "y") ItemChange(p, item);
    }

    private static void ItemChange(Creature p, Equipment item)
    {
        int choice;
        do
        {
            Console.Clear();
            Utilities.ColourText(Colour.ENHANCEMENT, "Available Enhancements\n");
            for (int i = 0; i < CraftMachineEnhancements.Count; i++)
            {                
                //List each enhancement, under it the items required to craft
                if (item.isWeapon == false && CraftMachineEnhancements[i].name == "Deadly") Utilities.EmbedColourText(Colour.MITIGATION, $"\n[{i + 1}] ", $"Hardened", "");
                else Utilities.EmbedColourText(Colour.magColour[(int)CraftMachineEnhancements[i].type + 1], $"\n[{i + 1}] ", $"{CraftMachineEnhancements[i].name}", "");
                if (CraftMachineEnhancements[i].ingredient1Amount > 0) Utilities.ColourText(Colour.DROP, $"{CraftMachineEnhancements[i].ingredient1Amount} {CraftMachineEnhancements[i].ingredient1.name}\n");
                if (CraftMachineEnhancements[i].ingredient2Amount > 0) Utilities.ColourText(Colour.DROP, $"{CraftMachineEnhancements[i].ingredient2Amount} {CraftMachineEnhancements[i].ingredient2.name}\n");
                if (CraftMachineEnhancements[i].ingredient3Amount > 0) Utilities.ColourText(Colour.DROP, $"{CraftMachineEnhancements[i].ingredient3Amount} {CraftMachineEnhancements[i].ingredient3.name}\n");
                if (CraftMachineEnhancements[i].ingredient4Amount > 0) Utilities.ColourText(Colour.DROP, $"{CraftMachineEnhancements[i].ingredient4Amount} {CraftMachineEnhancements[i].ingredient4.name}\n");
            }
            Console.WriteLine("\n\n[0] Return \n");
            //Select your enhancement
        } while (!int.TryParse(Console.ReadLine(), out choice));
        if (choice > 0 && choice < CraftMachineEnhancements.Count)
        {
            if (IngredientCheck(p, CraftMachineEnhancements[choice - 1]))
            {
                Console.Clear();
                Console.Write("\n");
                Utilities.DotDotDotSL();
                string weapOrArm = (item.isWeapon == true) ? "weapon" : "armor";
                string col = (item.isWeapon == true) ? Colour.MONSTER : Colour.MITIGATION;
                Utilities.EmbedColourText(col, Colour.ITEM, " Success! \n\nYou managed to ", "enhance", " your ", weapOrArm, "\n\n");
                //Add the prefix to the item info
                if (item.isWeapon == false && CraftMachineEnhancements[choice - 1].name == "Deadly")
                {
                    item.preName = "Hardened";
                    item.frontEnhance = 5;
                }
                else
                {
                    item.preName = CraftMachineEnhancements[choice - 1].name;
                    item.frontEnhance = choice;
                }
                //Actually affect the item
                if (CraftMachineEnhancements[choice - 1].type == EnhancementType.Damage)
                {
                    string damOrMit = (item.isWeapon == true) ? "Damage" : "Mitigation";
                    Utilities.EmbedColourText(col, col, "", damOrMit, " increased by ", $"{CraftMachineEnhancements[choice - 1].effect}", "");
                    item.damageEffect += CraftMachineEnhancements[choice - 1].effect;
                    item.goldEffect = item.xpEffect = item.healthEffect = 0;
                    p.health = (p.health > p.maxHealth) ? p.maxHealth : p.health;
                }
                if (CraftMachineEnhancements[choice - 1].type == EnhancementType.Health)
                {
                    Utilities.EmbedColourText(Colour.magColour[(int)CraftMachineEnhancements[choice].type], Colour.magColour[(int)CraftMachineEnhancements[choice].type], "", "Health", " increased by ", $"{CraftMachineEnhancements[choice - 1].effect}", "");
                    item.healthEffect += CraftMachineEnhancements[choice - 1].effect;
                    p.health += CraftMachineEnhancements[choice - 1].effect;
                    item.damageEffect = item.xpEffect = item.goldEffect = 0;
                }
                if (CraftMachineEnhancements[choice - 1].type == EnhancementType.Gold)
                {
                    Utilities.EmbedColourText(Colour.magColour[(int)CraftMachineEnhancements[choice].type], Colour.magColour[(int)CraftMachineEnhancements[choice].type], "", "Gold drop", " increased by ", $"{CraftMachineEnhancements[choice - 1].effect}", "");
                    item.goldEffect = CraftMachineEnhancements[choice - 1].effect;
                    item.damageEffect = item.xpEffect = item.healthEffect = 0;
                    p.health = (p.health > p.maxHealth) ? p.maxHealth : p.health;
                }
                if (CraftMachineEnhancements[choice - 1].type == EnhancementType.XP)
                {
                    Utilities.EmbedColourText(Colour.magColour[(int)CraftMachineEnhancements[choice].type], Colour.magColour[(int)CraftMachineEnhancements[choice].type], "", "Experience gained", "increased by ", $"{CraftMachineEnhancements[choice - 1].effect}", "");
                    item.xpEffect = CraftMachineEnhancements[choice - 1].effect;
                    item.damageEffect = item.goldEffect = item.healthEffect = 0;
                    p.health = (p.health > p.maxHealth) ? p.maxHealth : p.health;
                }

            }
            else Console.WriteLine("\nYou do not have the ingredients required");
            Utilities.Keypress();
        }            
    }

    public static bool IngredientCheck(Creature p, Recipe rec)
    {
        string[] ingredientsNeeded = new string[] { rec.ingredient1.name, rec.ingredient2.name, rec.ingredient3.name, rec.ingredient4.name };
        int[] ingredientAmountNeeded = new int[] { rec.ingredient1Amount, rec.ingredient2Amount, rec.ingredient3Amount, rec.ingredient4Amount };
        bool[] haveIngredients = new bool[] { false, false, false, false };
        for (int i = 0; i < 4; i++)
        {
            if (ingredientAmountNeeded[i] == 0) haveIngredients[i] = true;
            if (haveIngredients[i] == false)
            {
                for (int x = 0; x < p.Drops.Count; x++)
                {
                    if (p.Drops.Count == 0) break;
                    if (ingredientsNeeded[i] == p.Drops[x].name && ingredientAmountNeeded[i] <= p.Drops[x].amount)
                    {
                        p.Drops[x].amount -= ingredientAmountNeeded[i];
                        haveIngredients[i] = true;
                        break;
                    }
                }
                if (haveIngredients[i] == false) return false;
            }            
        }        
        return true;
    }
}