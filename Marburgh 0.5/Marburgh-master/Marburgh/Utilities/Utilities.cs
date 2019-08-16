using System;
using System.Threading;
using System.Collections.Generic;

public class Utilities
{
    public static int death;
    public static Random rand = new Random();

    public static void CharacterSheet(Creature p)
    {
        Console.Clear();
        Utilities.EmbedColourText(Colour.NAME, "Name: ", $"{p.family.FirstName} {p.family.LastName}", "");
        Utilities.EmbedColourText(Colour.CLASS, "Class: ", $"{p.pClass.cName}", "");
        if (p.level == p.levelMax) Utilities.ColourText(Colour.XP, "\nYOU ARE MAX LEVEL\n");
        else Utilities.EmbedColourText(Colour.XP, "\nLevel: ", $"{p.level}", "");
        if (p.xp >= LevelMaster.xpRequired[p.level]) Utilities.ColourText(Colour.XP, "YOU ARE ELIGIBLE FOR A LEVEL RAISE\n");
        else Utilities.EmbedColourText(Colour.XP, "Experience: ", $"{p.xp}/{LevelMaster.xpRequired[p.level]}", "");
        Utilities.EmbedColourText(Colour.GOLD, "\nGold: ", $"{p.gold}", "");
        Utilities.EmbedColourText(Colour.GOLD, "Gold In Bank: ", $"{p.bankGold}\n", "");
        Utilities.EmbedColourText(Colour.HEALTH, "Health: ", $"{p.health}/{p.maxHealth+p.Weapon.healthEffect+p.Armor.healthEffect}", "");
        Utilities.EmbedColourText(Colour.ENERGY, "Energy: ", $"{p.energy}/{p.maxEnergy}", "");
        Utilities.EmbedColourText(Colour.SP, "Spellpower: ", $"{p.magic}\n", "");
        Console.Write("Weapon: ");
        if (p.Weapon.frontEnhance > 0) Utilities.ColourText(Colour.magColour[(int)p.Weapon.frontEnhance], $"{p.Weapon.preName} ");
        Utilities.ColourText(Colour.ITEM, p.Weapon.name);
        Console.Write("\nArmor: ");
        if (p.Armor.frontEnhance > 0) Utilities.ColourText(Colour.magColour[(int)p.Armor.frontEnhance], $"{p.Armor.preName} ");
        Utilities.ColourText(Colour.ITEM, p.Armor.name);
        //Utilities.EmbedColourText(Colour.ITEM, "\nArmor: ", $"{p.Armor.name}\n", "");
        Utilities.EmbedColourText(Colour.BOSS, "\n\nDamage: ", $"{p.damage + p.Weapon.effect + p.Weapon.damageEffect}", "");
        Utilities.EmbedColourText(Colour.SPEAK, "Mitigation: ", $"{p.Armor.effect+p.Armor.effect}\n", "");
        Utilities.EmbedColourText(Colour.HEALTH, "Potions: ", $"{p.potions}", "");
        Utilities.EmbedColourText(Colour.RAREDROP, "\n\n", "[D]rops", "");
        Console.WriteLine("\nPress any key to continue");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (choice == "d")
        {
            if (p.Drops.Count > 0)
            {                
                Console.Clear();
                Utilities.ColourText(Colour.RAREDROP, "Drops\n\n");
                for (int i = 0; i < p.Drops.Count; i++)
                {
                    Console.WriteLine($"{p.Drops[i].name}: {p.Drops[i].amount.ToString()}");
                }                
                Utilities.Keypress();
            }
            else
            {
                Console.Clear();
                Utilities.ColourText(Colour.RAREDROP, "Drops\n\n");
                Utilities.ColourText(Colour.DROP, "No Drops");
                Utilities.Keypress();
            }
        }
    }

    public static void Death()
    {
        death++;
        Console.Clear();
        if (death > 2)
        {
            Console.Clear();
            Utilities.ColourText(Colour.MONSTER, $"You were the last of the {Create.p.family.LastName}s.\nYour bloodline ends here");
            Utilities.Keypress();
            GameOver();
        }
        Utilities.ColourText(Colour.MONSTER, "YOU DIED! Hopefully one of your family members can carry on for you");
        Utilities.Keypress();
        if (death == 1) Create.Character2Create();
        else Create.Character3Create();
    }    

    public static void DotDotDot()
    {
        Thread.Sleep(300);
        Console.Write(".");
        Thread.Sleep(300);
        Console.Write(".");
        Thread.Sleep(300);
        Console.Write(".\n");
        Thread.Sleep(300);
    }

    public static void EmbedColourText(ConsoleColor colour, string text1, string text2, string text3)
    {
        Console.Write(text1);
        ColourText(colour, text2);
        Console.Write(text3);
        Console.WriteLine();
    }

    public static void EmbedColourText(ConsoleColor colour1, ConsoleColor colour2, string text1, string text2, string text3, string text4, string text5)
    {
        Console.Write(text1);
        ColourText(colour1, text2);
        Console.Write(text3);
        ColourText(colour2, text4);
        Console.Write(text5);
        Console.WriteLine();
    }

    public static void EmbedColourText(ConsoleColor colour1, ConsoleColor colour2, ConsoleColor colour3, string text1, string text2, string text3, string text4, string text5, string text6, string text7)
    {
        Console.Write(text1);
        ColourText(colour1, text2);
        Console.Write(text3);
        ColourText(colour2, text4);
        Console.Write(text5);
        ColourText(colour3, text6);
        Console.Write(text7);
        Console.WriteLine();
    }    

    public static void EmbedColourText(ConsoleColor colour1, ConsoleColor colour2, ConsoleColor colour3, ConsoleColor colour4, string text1, string text2, string text3, string text4, string text5, string text6, string text7, string text8, string text9)
    {
        Console.Write(text1);
        ColourText(colour1, text2);
        Console.Write(text3);
        ColourText(colour2, text4);
        Console.Write(text5);
        ColourText(colour3, text6);
        Console.Write(text7);
        ColourText(colour4, text8);
        Console.Write(text9);
        Console.WriteLine();
    }

    public static void EmbedColourText(ConsoleColor colour1, ConsoleColor colour2, ConsoleColor colour3, ConsoleColor colour4, ConsoleColor colour5, string text1, string text2, string text3, string text4, string text5, string text6, string text7, string text8, string text9, string text10, string text11)
    {
        Console.Write(text1);
        ColourText(colour1, text2);
        Console.Write(text3);
        ColourText(colour2, text4);
        Console.Write(text5);
        ColourText(colour3, text6);
        Console.Write(text7);
        ColourText(colour4, text8);
        Console.Write(text9);
        ColourText(colour5, text10);
        Console.Write(text11);
        Console.WriteLine();
    }

    public static void Equip(Equipment Item)
    {
        if (Item.isWeapon)
        {
            Create.p.Weapon = new Equipment("",0,0,0,0,0,0,0,0,"","",true);
            Create.p.Weapon = Item;
        }
        else
        {
            Create.p.Armor = new Equipment("", 0, 0, 0, 0, 0, 0, 0, 0, "", "", false);
            Create.p.Armor = Item;
        }
    }

    public static void GameOver()
    {
        Console.Clear();
        Utilities.ColourText(Colour.BOSS, "You tried.\nYou failed, but you tried.\n\nAnd in the end, is that not the real victory?\nThe answer is no.\n\nGoodbye!");
        Utilities.Keypress();
        Environment.Exit(0);
    }
    
    public static void ColourText(ConsoleColor colour, String text)
    {
        Console.ForegroundColor = colour;
        Console.Write(text);
        Console.ForegroundColor = Colour.RESET;
    }

    public static void Heal(Creature p)
    {
        if (p.health == p.maxHealth) Utilities.ColourText(Colour.SPEAK, "You don't need healing!");
        else if (p.potions < 1) Utilities.ColourText(Colour.SPEAK, "You don't have enough potions!");
        else
        {
            p.health = p.maxHealth;
            p.potions -= 1;
            Utilities.ColourText(Colour.HEALTH, "You heal to full health");
        }
        Utilities.Keypress();
    }

    public static void Help()
    {
        Console.WriteLine("\n\nThis game has changed a great deal from its last iteration.");
        Console.WriteLine("Help file coming soon");
        Utilities.Keypress();
    }    

    public static void Keypress()
    {
        Console.WriteLine("\n\nPress any key to continue");
        Console.ReadKey(true);
    }     

    public static void Refresh(Creature p)
    {
        p.health = p.maxHealth;
        p.energy = p.maxEnergy;
        Explore.canExplore = true;        
        Data.Save(p);
    }

    public static void Quit()
    {
        Console.WriteLine("Are you sure you want to quit?\n\n[Y]es      [N]o\n");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (choice == "y")
            Environment.Exit(0);
    }
}