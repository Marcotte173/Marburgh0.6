public class DayEvent
{
    public string name;
    public bool check;
    public bool flavorCheck;
    public bool gameOver;
    public string flavor;
    public int day;
    public int week;
    public int month;
    public int year;
    public bool active;

    public DayEvent(string name, bool check, bool flavorCheck, string flavor, int day, int week, int month, int year, bool active, bool gameOver)
    {
        this.name = name;
        this.check = check;
        this.flavorCheck = flavorCheck;
        this.flavor = flavor;
        this.day = day;
        this.week = week;
        this.month = month;
        this.year = year;
        this.active = active;
        this.gameOver = gameOver;                
    }
}