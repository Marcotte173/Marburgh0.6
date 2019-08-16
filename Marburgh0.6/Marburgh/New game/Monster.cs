public class Monster: Creature
{
    public string name;
    public string taunt;
    public int rewardGold;
    public int rewardXP;
    public string summonName;
    public Monster(string name, string taunt, pClass pClass, int rewardGold, int rewardXP, string summonName)
    : base(name, pClass)
    {
        this.name = name;
        this.taunt = taunt;
        this.pClass = pClass;
        gold = rewardGold;
        xp = rewardXP;
        energy = pClass.startingEnergy;
        health = pClass.startingHealth;
        magic = pClass.startingMagic;
        damage = pClass.startingDamage;
        this.summonName = summonName;
    }
}