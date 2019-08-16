public class pClass
{    
    public string cName;
    public int startingEnergy;    
    public int startingHealth;    
    public int startingMagic;    
    public int startingDamage;    

    public pClass(string name, int startingHealth, int startingDamage, int startingEnergy, int startingMagic )
    {
        this.startingEnergy = startingEnergy;        
        this.startingHealth = startingHealth;        
        this.startingMagic = startingMagic;        
        this.startingDamage = startingDamage;
        cName = name;
    }    
}