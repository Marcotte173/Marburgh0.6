public class Room
{
    //Variables, self explanatory
    public string name;
    public string flavor;
    public int modifier;
    public Event[] EventArray;

    //Constructor
    public Room(string name, string flavor, int modifier, Event[] EventArray)
    {
        this.name = name;
        this.flavor = flavor;
        this.modifier = modifier;
        this.EventArray = EventArray;
    }
}