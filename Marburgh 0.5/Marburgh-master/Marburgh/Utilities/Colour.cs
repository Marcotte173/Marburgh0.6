using System;
using System.Runtime.InteropServices;

public class Colour
{
    public static ConsoleColor[] magColour = new ConsoleColor[] { ConsoleColor.White, ConsoleColor.DarkRed, ConsoleColor.Green, ConsoleColor.DarkYellow, ConsoleColor.Yellow };
    
    //ESCAPE CODES, REQUIRED FOR FOR LOOPS
    public const string BOLD = "\u001B[1m";
    public const string GRAY = "\u001b[30;1m";
    public const string WHITE = "\u001B[37m";
    public const string RED = "\u001B[31m";
    public const string MAGENTA = "\u001b[35m";
    public const string BLUE = "\u001b[34m";
    public const string ECITEM = "\u001b[32m";
    public const string ECGOLD = "\u001b[33m";
    public const string CYAN = "\u001b[36m";
    public const string RESET1 = "\u001B[1m\u001B[37m";

    const int STD_OUTPUT_HANDLE = -11;
    const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 4;
    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr GetStdHandle(int nStdHandle);
    [DllImport("kernel32.dll")]
    static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);
    [DllImport("kernel32.dll")]
    static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

    //COLOURS
    public const ConsoleColor XP = ConsoleColor.Yellow;
    public const ConsoleColor SP = ConsoleColor.DarkMagenta;
    public const ConsoleColor ITEM = ConsoleColor.DarkGreen;
    public const ConsoleColor MONSTER = ConsoleColor.Red;
    public const ConsoleColor RESET = ConsoleColor.White;
    public const ConsoleColor CLASS = ConsoleColor.Magenta;
    public const ConsoleColor HEALTH = ConsoleColor.Green;
    public const ConsoleColor GOLD = ConsoleColor.DarkYellow;
    public const ConsoleColor BOSS = ConsoleColor.DarkRed;
    public const ConsoleColor NAME = ConsoleColor.DarkCyan;
    public const ConsoleColor TIME = ConsoleColor.DarkBlue;
    public const ConsoleColor ENERGY = ConsoleColor.Cyan;
    public const ConsoleColor RAREDROP = ConsoleColor.Blue;
    public const ConsoleColor DROP = ConsoleColor.DarkGray;
    public const ConsoleColor SPEAK = ConsoleColor.Gray;

    public static void SetupConsole()
    {
        IntPtr handle = GetStdHandle(STD_OUTPUT_HANDLE);
        uint mode;
        GetConsoleMode(handle, out mode);
        mode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING;
        SetConsoleMode(handle, mode);
    }
}