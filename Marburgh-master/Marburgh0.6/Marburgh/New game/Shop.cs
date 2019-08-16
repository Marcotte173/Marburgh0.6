public class Shop
{
    public string name;
    public string shopkeepName;
    public string shopkeepRace;
    public string shopkeepGreeting;
    public Equipment[] ItemList;

    public Shop(string name,string shopkeepName, string shopkeepRace, string shopkeepGreeting, Equipment[] ItemList)
    {
        this.name = name;
        this.shopkeepName = shopkeepName;
        this.shopkeepRace = shopkeepRace;
        this.shopkeepGreeting = shopkeepGreeting;
        this.ItemList = ItemList;
    }
}