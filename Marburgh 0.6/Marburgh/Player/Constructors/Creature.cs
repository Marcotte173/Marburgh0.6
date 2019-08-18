using System.Collections.Generic;
public class Creature
{
    public List<Drop> Drops = new List<Drop> { };
    //stun[0] = stunned, stun[1] = frozen
    public int[] stun = new int[] { 0, 0 };
    public bool canAct;
    public int bleed;
    public int confused;
    public int casting;
    public int burning;
    public int shield;
    public int maxPotions;
    public int bleedDam;
    public int attack;
    public Family family;
    public int hasInvestment;
    public int invested;
    public bool craft;
    public int addDam;
    public int addHP;
    public pClass pClass;
    public int energy;
    public int maxEnergy;
    public int health;
    public int maxHealth;
    public int maxMagic;
    public int magic;
    public int damage;
    public int level;
    public int gold;
    public int xp;    
    public int potions;
    public int levelMax;
    public int bankGold;
    public Equipment Weapon;
    public Equipment Armor;
    public bool win;
    public Creature(pClass pClass) { }
}