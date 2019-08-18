using System;
using System.Collections.Generic;

public class Combat{

    public static int round;
    public static void GameCombat(Creature p, List<Monster> Monster)
    {
        CombatUpdate(p,Monster);
        CombatUI(p,Monster);
        CombatAction(p,Monster);
        Utilities.Keypress();
    }

    public static void CombatUpdate(Creature p, List<Monster> Monster)
    {
        p.canAct = true;
        for (int i = 0; i < p.stun.Length; i++)
        {
            if (p.stun[i] > 0) p.canAct = false;
            p.stun[i]--;
        }
        if (p.bleed > 0) p.bleed--;
        if (p.casting > 0) p.casting--;
        if (p.burning > 0) p.burning--;
        if (p.shield > 0) p.shield--;

        foreach (Monster mon in Monster)
        {
            mon.canAct = true;
            for (int i = 0; i < mon.stun.Length; i++)
            {
                if (mon.stun[i] > 0) mon.canAct = false;
                mon.stun[i]--;
            }
            if (mon.bleed > 0) mon.bleed--;
            if (mon.casting > 0) mon.casting--;
            if (mon.burning > 0) mon.burning--;
            if (mon.shield > 0) mon.shield--;
            if (mon.confused > 0) mon.confused--;
        }         
    }
    
                
    public static void CombatUI(Creature p, List<Monster> Monster)
    {
        Console.Clear();
        Console.WriteLine($"Combat round {round}\n");
        if (Monster.Count == 1)
        {
            Utilities.CenterText(Monster[0].name);
            Utilities.CenterText(Monster[0].health.ToString());
        }
        else if (Monster.Count == 2)
        {
            Utilities.CenterText(Monster[0].name, Monster[1].name);
            Utilities.CenterText(Monster[0].health.ToString(), Monster[1].health.ToString()); 
        }
        else if (Monster.Count == 3)
        {
            Utilities.CenterText(Monster[0].name, Monster[1].name, Monster[2].name);
            Utilities.CenterText(Monster[0].health.ToString(), Monster[1].health.ToString(), Monster[2].health.ToString());
        }
    }

    public static void CombatAction(Creature p, List<Monster> Monster)
    {
        
    }

    

    
}