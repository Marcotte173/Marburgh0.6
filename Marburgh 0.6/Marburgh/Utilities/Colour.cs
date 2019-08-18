using System;
using System.Runtime.InteropServices;

public class Colour
{
    public static string[] magColour = new string[] { RESET, MONSTER, HEALTH, GOLD, XP, MITIGATION };
    public static string[] rdColour = new string[] { DROP, RAREDROP };
    
    //ESCAPE CODES FOR COLOUR
    public const string BOLD = "\u001B[1m";
    public const string NAME = "\u001b[38;5;14m";
    public const string CLASS = "\u001b[38;5;5m";
    public const string XP = "\u001b[38;5;11m";
    public const string GRAY = "\u001b[30;1m";
    public const string GOLD = "\u001b[38;5;136m";
    public const string HEALTH = "\u001b[38;5;118m";
    public const string ITEM = "\u001b[38;5;2m";
    public const string ENERGY = "\u001b[38;5;33m";
    public const string MONSTER = "\u001b[38;5;160m";
    public const string BOSS = "\u001b[38;5;124m";
    public const string MITIGATION = "\u001b[38;5;241m";
    public const string SPEAK = "\u001b[38;5;230m";
    public const string TIME = "\u001b[38;5;80m";
    public const string SP = "\u001b[38;5;164m";
    public const string DROP = "\u001b[38;5;58m";
    public const string RAREDROP = "\u001b[38;5;127m";
    public const string ENHANCEMENT = "\u001b[38;5;170m";
    public const string RESET = "\u001B[1m\u001B[37m";

    const int STD_OUTPUT_HANDLE = -11;
    const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 4;
    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr GetStdHandle(int nStdHandle);
    [DllImport("kernel32.dll")]
    static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);
    [DllImport("kernel32.dll")]
    static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);
    
    public static void SetupConsole()
    {
        IntPtr handle = GetStdHandle(STD_OUTPUT_HANDLE);
        uint mode;
        GetConsoleMode(handle, out mode);
        mode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING;
        SetConsoleMode(handle, mode);
    }
}