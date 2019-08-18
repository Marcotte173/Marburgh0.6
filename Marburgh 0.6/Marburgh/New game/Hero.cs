public class Hero : Creature
{
    public Hero(string name, pClass pClass, Equipment Weapon, Equipment Armor)
    : base(name, pClass)
    {
        pName = name;
        this.pClass = pClass;
        this.Weapon = Weapon;
        this.Armor = Armor;
        energy = maxEnergy = pClass.startingEnergy;
        health = maxHealth = pClass.startingHealth;
        magic = pClass.startingMagic;
        damage = pClass.startingDamage;
        level = 1;
        gold = 100;
        xp = 0;
        potions = 1;        
        levelMax = 5;
        bankGold = 0;
        fights = 10;
        fightsMax = 10;
        xpRequired = 25;
        win = false;
}
    public void SetName(string x) { pName = x; }
}