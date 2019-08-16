using System;
using System.Collections.Generic;

public class Family
{
    public static string LN;
    public string sibling1, sibling2;
    public int siblings = 2;
    public string firstDead, secondDead;
    public string FirstName, LastName;
    public static List<string> potentialNames = new List<string>
        {
            "Angela","Amy", "Anna","Alison","Adam","Alex","Anthony","Alistair","Alfred","Alexa",
            "Beth", "Bonnie", "Bailey","Beuford", "Barbara","Bernard", "Bill", "Bryce", "Belle","Barrie",
            "Carol", "Candice", "Cindy","Cole", "Charles","Chris","Chance", "Caleb","Caddie","Caitlyn",
            "Donna", "Deborah","Doug",  "Don", "Dwight",
            "Elaine", "Emily","Edward",
            "Farrah","Fred","Frank",
            "Gina", "George",
            "Heather",  "Harold","Hank",
            "Isabelle", "Ian",
            "Jolene",  "Jake", "James",
            "Katherine", "Karl",
            "Laura","Lewis","Larry",
            "Mary",  "Matt","Martin","Melvin","Maebel", "Maddox","Meagen","Magda","Marvin","Mitch"
        };
    public static List<string> FamilyNames = new List<string> { };

    public static void FamilyCreate()
    {
        FamilyNameSelect();
        SiblingGenerate();
    }

    public static void FamilyNameSelect()
    {
        Console.Clear();
        Console.WriteLine("What is your family name?\n");
        Console.ForegroundColor = Colour.NAME;
        LN = Console.ReadLine();
        Console.ForegroundColor = Colour.RESET;
        Utilities.EmbedColourText(Colour.NAME, "\nIs ", LN, " correct?\n\n\n[Y]es   [N]o");
        string confirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (confirm == "y") return;
        else FamilyNameSelect();
    }

    public static void SiblingGenerate()
    {
        if (FamilyNames.Count < 3)
        {
            int roll = Utilities.rand.Next(0, potentialNames.Count - 1);
            FamilyNames.Add(potentialNames[roll]);
            potentialNames.Remove(potentialNames[roll]);
        }
        else return;        
        SiblingGenerate();
    }

    public static void FamilyAssignment()
    {
        Console.Clear();
        Utilities.EmbedColourText(Colour.NAME, Colour.NAME, "Your name is", $" {Create.p.family.FirstName} {Create.p.family.LastName}", ".\n\nYour Mother, ", $"Helen {Create.p.family.LastName} ", "was an adventurer. She was recently killed by an Orc.\nYou never knew your father.\n");
        if (Utilities.death == 0) Utilities.EmbedColourText(Colour.NAME, Colour.NAME, "You are the eldest child.\nYour siblings,", $" {Create.p.family.sibling1}", " and", $" {Create.p.family.sibling2}", " look up to you now to take care of them.");
        else if (Utilities.death == 1) Utilities.EmbedColourText(Colour.NAME, "You are the eldest surviving child.\nYour sibling,", $" {Create.p.family.sibling1}", " looks up to you now to put food on the table the only way you know how - Adventuring.");
        else Utilities.EmbedColourText(Colour.NAME, "You are the only survivng", $" {Create.p.family.LastName}", ".\nIt is all up to you now.");
        Utilities.Keypress();
    }
    
    public Family(string lName, string firstBorn, string secondBorn, string thirdBorn)
    {
        LastName = lName;
        firstDead = FirstName = firstBorn;
        secondDead = sibling1 = secondBorn;
        sibling2 = thirdBorn;
    }
}