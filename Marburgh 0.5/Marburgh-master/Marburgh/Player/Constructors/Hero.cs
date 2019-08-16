using System.Collections.Generic;
public class Hero : Creature
{
    public Hero(pClass pClass, Family family, Equipment Weapon, Equipment Armor)
    : base(pClass)
    {
        List<Drop> Drops = new List<Drop> { };
        this.family = family;
        this.pClass = pClass;
        this.Weapon = Weapon;
        this.Armor = Armor;
        energy = maxEnergy = pClass.startingEnergy+1;
        health = maxHealth = pClass.startingHealth+4;
        magic = pClass.startingMagic;
        damage = pClass.startingDamage;
        level = 1;
        gold = 100;
        xp = 0;
        potions = 1;        
        levelMax = 5;
        bankGold = 0;
        invested = 0;
        hasInvestment = 0;
        craft = false;
        win = false;
        maxPotions = 1;
        if (pClass.cName == "Mage") maxPotions = 2;
    }
}