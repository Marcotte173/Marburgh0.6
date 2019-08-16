using System;
using System.Threading;

public class Tavern
{
    public static void Inn(Creature p)
    {
        Console.Clear();
        Utilities.ColourText(Colour.SPEAK, "You enter a bustling tavern. More flavor will be added soon describing the place and what you can do.");
        Console.WriteLine("\n\n[G]amble                    [L]ocal gossip");
        Console.WriteLine("[T]alk to the bartender     [R]eturn\n");
        Utilities.EmbedColourText(Colour.GOLD, "You have ", $"{p.gold} ", "gold\n");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (choice == "g")
            Gamble(p);
        else if (choice == "l")
            Gossip();
        else if (choice == "t")
            Bartender();
        else if (choice == "r")
            Marburgh.Program.GameTown();
        Inn(p);
    }

    private static void Bartender()
    {
        Console.Clear();
        Utilities.ColourText(Colour.SPEAK, "The Bartender will have more to say once dungeoning has been implemented");
        Utilities.Keypress();
    }

    private static void Gossip()
    {
        Console.Clear();
        Utilities.ColourText(Colour.SPEAK, "Word is this game's gonna be pretty cool when it gets finished");
        Utilities.Keypress();
    }

    private static void Gamble(Creature p)
    {   
        if (p.gold > 0)
        {
            Console.Clear();
            Console.WriteLine("You walk towards the tables at the back and find a dice game.\nA grisled old man challenges you to a game of dice\n\n");
            int wager;
            do
            {
                Utilities.EmbedColourText(Colour.GOLD, "\nYou have ",$"{p.gold}"," gold\nHow much would you like to wager?\n\n[0] Return\n");
                Console.ForegroundColor = Colour.GOLD;
            } while (!int.TryParse(Console.ReadLine(), out wager));
            Console.ForegroundColor = Colour.RESET;
            if (wager == 0) return;
            else if (wager > 150 * p.level)
            {
                Console.WriteLine("You can't gamble that much");
                Utilities.Keypress();
                return;
            }
            else if (p.gold >= wager)
            {
                Utilities.EmbedColourText(Colour.GOLD, "\nYou want to wager ",$"{wager}"," gold?\n\n[Y]es     [N]o");
                string wagerConfirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
                if (wagerConfirm != "y") return;
                else
                {
                    int playerRoll = Utilities.rand.Next(1, 7);
                    int opponentRoll = Utilities.rand.Next(1, 7);
                    p.gold -= wager;
                    Console.Clear();
                    Utilities.EmbedColourText(Colour.GOLD, "Confident, you set ",$"{wager}"," gold on the table");
                    Console.Write($"\nYou roll a die, it comes up.");
                    RollDice(playerRoll);
                    Console.Write($"\nYour opponent rolls a die, it comes up.");
                    RollDice(opponentRoll);
                    string report = (playerRoll == opponentRoll) ? "\n\nIt's a tie!\nYou take your money back" : (playerRoll > opponentRoll) ? "\n\nYou win!\nYou receive " + Colour.ECGOLD + wager*2 + Colour.RESET1 + " gold!": "\n\nYou lose!\n";
                    Console.WriteLine(report);
                    p.gold = (playerRoll == opponentRoll) ? p.gold + wager : (playerRoll > opponentRoll) ? p.gold + 2 * wager : p.gold;
                    Utilities.Keypress();
                    return;
                }
            }
        }
        Console.WriteLine("You don't have enough money!");
        Utilities.Keypress();             
    }

    public static void RollDice(int roll)
    {
        Thread.Sleep(300);
        Console.Write($".");
        Thread.Sleep(300);
        Console.Write($".");
        Thread.Sleep(300);
        Console.Write($"{roll}!");
        Thread.Sleep(400);
    }
}