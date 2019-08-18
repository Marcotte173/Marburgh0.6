using System;

class LevelMaster
{
    public static int[] xpRequired = new int[5] { 0, 25, 50, 85, 115 };

    public static void VisitLevelMaster(Creature p)
    {
        Console.Clear();
        if (p.level == p.levelMax) Utilities.ColourText(Colour.SPEAK, "The level master has left. He has taught you all he can.");
        else
        {
            Utilities.ColourText(Colour.SPEAK, "The Level Master is meditating. He looks up at you.\n\n'Are you here to level up?\n");
            Console.WriteLine("\n[Y]es    [N]o\n");
            string level = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (level == "y")
            {
                if (p.xp < xpRequired[p.level]) Utilities.ColourText(Colour.SPEAK, "\nHe looks at you thoughtfully.\n'Hmmm... You're not QUITE ready yet'\nCome back when you are more experienced");
                else LevelUp(p);
            }
            else
                Utilities.ColourText(Colour.SPEAK, "\nQuit wasting my time!");
        }
        Utilities.Keypress();
    }

    public static void LevelUp(Creature p)
    {
        p.xp -= xpRequired[p.level];
        p.level += 1;
        Utilities.EmbedColourText(Colour.XP, "Congrats! You are level ", $"{p.level}", "!");
        Utilities.EmbedColourText(Colour.HEALTH, "\nMax health increased by ", $"{p.pClass.startingHealth}", "!");
        Utilities.EmbedColourText(Colour.ENERGY, "Max energy increased by ", $"{p.pClass.startingEnergy}", "!");
        Utilities.EmbedColourText(Colour.BOSS, "Damage increased by ", $"{p.pClass.startingDamage}", "!");
        if (p.pClass.cName != "Warrior")
        {
            Utilities.EmbedColourText(Colour.SP, "Spellpower increased by ", $"{p.pClass.startingMagic}", "!");
            p.maxMagic += p.pClass.startingMagic;
        }
        p.maxEnergy += p.pClass.startingEnergy;
        p.maxHealth += p.pClass.startingHealth;
        p.health = p.maxHealth;
        p.energy = p.maxEnergy;
        p.damage += p.pClass.startingDamage;
    }
}