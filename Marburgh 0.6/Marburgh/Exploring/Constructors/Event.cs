using System;

public class Event
{
    //Variables, self explanatory
    public string name;
    public string flavor;
    public int eventType;
    public int effect;
    public int success;
    public static Random rand = new Random();

    //Constructor
    public Event(string name, string flavor, int eventType, int effect, int success)
    {
        this.name = name;
        this.flavor = flavor;
        this.eventType = eventType;
        this.effect = effect;
        this.success = success;
    }
}