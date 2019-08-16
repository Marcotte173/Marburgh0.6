using System;
using System.Threading;

public class Combat
{
    //No current function. Here as a seed for when I want monsters to drop gear. Works very well

    //public static void DropAdd()
    //{
    //    for (int i = 0; i < Utilities.Drops.Count; i++)
    //    {
    //        if (Utilities.Drops[i] == drop[1]) Utilities.Drops[i].amount++;
    //    }
    //    if (!Utilities.Drops.Contains(drop[1])) Utilities.Drops.Add(drop[1]);
    //}

    private static void AttackMonster(Creature p, Monster mon)
    {
        Console.WriteLine($"You hit the {mon.name} for {p.attack} damage");
        mon.health -= p.attack;
    }

    private static void AttackPlayer(Creature p, Monster mon)
    {
        if (p.shield)
        {
            Console.WriteLine($"The {mon.name} hits you but your shield absorbs the damage");
            p.shield = false;
        }
        else
        {
            Console.WriteLine($"The {mon.name} hits you for {mon.damage - p.Armor.effect} damage");
            p.health -= mon.damage - p.Armor.effect;
        }
    }

    private static void AttackSelect(Creature p, Monster mon)
    {
        Console.WriteLine("\n\nWhat would you like to do?");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (choice == "1")
        {
            p.attack = p.damage + p.Weapon.effect;
            return;
        }
        else if (choice == "2" && p.energy > 0 && p.pClass.cName == "Mage")
        {
            p.attack = p.damage + p.magic * 4;
            p.energy--;
            return;
        }
        else if (choice == "2" && p.energy > 0 && p.pClass.cName == "Rogue")
        {
            p.attack = p.damage + p.Weapon.effect * 3;
            p.energy--;
            return;
        }
        else if (p.level > 2 && choice == "3" && p.energy > 1 && p.pClass.cName == "Mage")
        {
            p.shield = true;
            p.energy -= 2;
            Utilities.Keypress();
            GameCombat(p, mon);
        }
        else if (p.level > 2 && choice == "3" && p.energy > 1 && p.pClass.cName == "Rogue")
        {
            p.attack = p.damage + p.Weapon.effect * 2;
            p.energy -= 2;
            p.bleedDam = p.attack / 2;
            Console.WriteLine($"You rend the {mon} causing {p.bleedDam} damage");
            p.bleed = true;
            return;
        }
        else if ((choice == "2" && p.energy < 1) || (p.level > 2 && choice == "3" && p.energy < 2))
        {
            Console.WriteLine("You don't have enough energy!");
            Utilities.Keypress();
        }
        else if (choice == "c") Utilities.CharacterSheet(p);
        else if (choice == "4") Utilities.Heal(p);
        else if (choice == "5" && p.pClass.cName == "Rogue") Run();
        else if (choice == "5" && p.pClass.cName != "Rogue")
        {
            int runChance = Utilities.rand.Next(1, 100);
            if (runChance < 80) Run();
            Console.WriteLine($"You try to run, but the {mon.name} knocks you over!");
            return;
        }
        GameCombat(p, mon);
    }

    public static void GameCombat(Creature p, Monster mon)
    {
        Console.Clear();
        int playerHitRoll = Utilities.rand.Next(1, 101);
        int monsterHitRoll = Utilities.rand.Next(1, 101);
        int damageRoll = Utilities.rand.Next(1 * (p.level / 2), 3 * (p.level / 2));
        if (p.level == 5) Console.WriteLine($"You are facing {mon.name}");
        else Console.WriteLine($"You are facing a {mon.name}");
        Console.Write($"'{mon.taunt}'");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine(String.Format("{0,-20} {1,-30}", "You", $"{mon.name}"));
        Console.WriteLine(String.Format("{0,-20} {1,-30}", $"Health: {p.health}/{p.maxHealth}", $"Health: { mon.health}"));
        Console.WriteLine($"Energy: {p.energy}/{p.maxEnergy}");
        Console.WriteLine("");
        string shieldText = (p.pClass.cName != "Mage") ? "" : (p.shield) ? "Your Shield is on" : "Your shield is off";
        Console.WriteLine(shieldText);
        Console.WriteLine("");
        Console.WriteLine("[1]Attack");
        if (p.pClass.cName == "Mage")
        {
            Console.WriteLine("[2]Fireball");
            if (p.level > 2) Console.WriteLine("[3]Shield");
        }
        else if (p.pClass.cName == "Rogue")
        {
            Console.WriteLine("[2]Backstab");
            if (p.level > 2) Console.WriteLine("[3]Rend");
        }
        Console.WriteLine("[4]Heal\n[5]Run\n\n[C]haracter");
        AttackSelect(p, mon);
        p.attack += damageRoll;
        Console.WriteLine("");
        if (playerHitRoll < 81) AttackMonster(p, mon);
        else Console.WriteLine($"You miss the {mon.name}!");
        if (p.bleed)
        {
            Console.WriteLine($"The {mon} bleeds for {p.bleedDam} damage");
            mon.health -= p.bleedDam;
        }
        if (mon.health < 1) Reward(p, mon);
        Thread.Sleep(400);
        if (monsterHitRoll < 81) AttackPlayer(p, mon);
        else Console.WriteLine($"The {mon.name} misses you!");
        Utilities.Keypress();
        if (p.health < 1) Utilities.Death();
        if (p.health > 0 && mon.health > 0) GameCombat(p, mon);
    }

    private static void Run()
    {
        Console.WriteLine("\nYou scamper away, as fast as your legs can carry you.\nYou eventually see daylight and return to town.\n\nHopefully no one saw that.");
        Utilities.Keypress();
        Marburgh.Program.GameTown();
    }

    private static void Reward(Creature p, Monster mon)
    {
        int randGold = Utilities.rand.Next(-10, 15);
        if (mon.name == "Marcotte") Data.Win(p);
        Console.WriteLine($"\n\nYou kill the {mon.name}!");
        Console.WriteLine($"\nYou find {(mon.gold + randGold) * p.level} gold!");
        Console.WriteLine($"You earn {mon.xp} experience!");
        p.gold += (mon.gold + randGold) * p.level;
        p.xp += mon.xp;
        Utilities.Keypress();
        Marburgh.Program.GameTown();
    }
}