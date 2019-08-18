using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class OtherPlaces
{
    public static void Other(Creature p)
    {
        Console.Clear();
        Utilities.ColourText(Colour.SPEAK, "Welcome to the still expanding portion of this game.\nAs Marburgh grows, both inside and out, this is where you will find new places to visit and thing to do.\n");
        Utilities.ColourText(Colour.SPEAK, "For now tho, you can visit your family graveyard\n\n");
        Console.WriteLine("[G]raveyard\n[R]eturn to town\n\nWhat you you like to do?");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (choice == "r") Marburgh.Program.GameTown();
        if (choice == "g") Graveyard(p);
        Other(p);
    }

    public static void Graveyard(Creature p)
    {
        Console.Clear();
        if (Utilities.death == 0) Console.WriteLine("As you arrive at the family plot you see one grave, your mother's. \nIt is still fresh, the memories still vivid.");
        else if (Utilities.death == 1) Utilities.EmbedColourText(Colour.NAME, "As you arrive at the family plot you see two graves.\nThe newer one belongs to ", $"{p.family.sibling2}", ". \nNext to it is your Mother's grave.");
        else Utilities.EmbedColourText(Colour.NAME, Colour.NAME, "You arrive at the graveyard to visit the only family you've ever know. \nThe fresh graves of ", $"{p.family.sibling2} ", "and ", $"{p.family.sibling1}", " lay next to your Mother.\nAt least they can be together.");
        Utilities.Keypress();
    }
}
