public class Boss:Monster
{
    //Is the Boss Alive?
    public bool IsAlive = true;

    //Constructor
    public Boss(string name, string taunt, pClass pClass, int xp, int gold, int addHP, int addDam, Drop drop, bool IsAlive)
    : base(name, taunt, pClass, xp, gold, addHP,addDam, drop)
    {
        this.IsAlive = IsAlive;
    }
}