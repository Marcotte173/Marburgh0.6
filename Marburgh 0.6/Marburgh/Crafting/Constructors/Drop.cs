public class Drop
{
    //Variables, self explanatory
    public string name;
    public int amount;
    public int dropChance;
    public int rare;

    //Constructor
    public Drop(string name, int amount, int dropChance, int rare)
    {
        this.rare = rare;
        this.name = name;
        this.amount = amount;
        this.dropChance = dropChance;
    }
}