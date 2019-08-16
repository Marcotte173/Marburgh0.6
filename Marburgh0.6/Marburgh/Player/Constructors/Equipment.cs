public class Equipment
{
    public string name;
    public int effect;
    public int price;
    public int goldEffect;
    public int xpEffect;
    public int healthEffect;
    public int frontEnhance;
    public int backEnhance;
    public string preName;
    public string sufName;
    public int damageEffect;
    public bool isWeapon;

    public Equipment(string name, int effect, int price, int damageEffect,int healthEffect, int goldEffect, int xpEffect, int frontEnhance,int backEnhance,string preName, string sufName,bool isWeapon)
    {
        this.isWeapon = isWeapon;
        this.damageEffect = damageEffect;
        this.preName = preName;
        this.sufName = sufName;
        this.name = name;
        this.effect = effect;
        this.price = price;
        this.goldEffect = goldEffect;
        this.frontEnhance = frontEnhance;
        this.backEnhance = backEnhance;
    }
}