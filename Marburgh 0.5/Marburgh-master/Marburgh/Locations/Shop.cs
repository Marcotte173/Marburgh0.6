using System;
using System.Collections.Generic;

public class Shop
{
    public string name;
    public string shopkeepName;
    public string shopkeepRace;
    public string shopkeepGreeting;
    public Equipment[] ItemList;
    public static Equipment[] WeaponList = new Equipment[] 
    {
        new Equipment("None", 0, 0,0,0,0,0,0,0,"","",true),              new Equipment("Dagger", 2, 50,0,0,0,0,0,0,"","",true),            new Equipment("Battered Sword", 4, 200,0,0,0,0,0,0,"","",true),  new Equipment("Short Sword", 6, 400,0,0,0,0,0,0,"","",true),
        new Equipment("Arming Sword", 8, 800,0,0,0,0,0,0,"","",true),    new Equipment("Roman Sword", 10, 1000,0,0,0,0,0,0,"","",true),    new Equipment("Long Sword", 12, 1200,0,0,0,0,0,0,"","",true),    new Equipment("Steel Sword", 14, 1600,0,0,0,0,0,0,"","",true),
        new Equipment("Broad Sword", 16, 2000,0,0,0,0,0,0,"","",true),   new Equipment("Great Sword", 20, 2500,0,0,0,0,0,0,"","",true),    new Equipment("Blue Sword", 22, 3000,0,0,0,0,0,0,"","",true),    new Equipment("Black Sword", 25, 3500,0,0,0,0,0,0,"","",true),
        new Equipment("Red Sword", 30, 4500,0,0,0,0,0,0,"","",true),     new Equipment("Purple Sword", 35, 5500,0,0,0,0,0,0,"","",true),   new Equipment("White Sword", 40, 6500,0,0,0,0,0,0,"","",true),   new Equipment("Crystal Sword", 50, 7500,0,0,0,0,0,0,"","",true)
    };
    public static Equipment[] ArmorList = new Equipment[] 
    {
        new Equipment("None", 0, 0,0,0,0,0,0,0,"","",false),              new Equipment("Cloth Armor", 2, 50,0,0,0,0,0,0,"","",false),      new Equipment("Battered Armor", 4, 200,0,0,0,0,0,0,"","",false), new Equipment("Soldier's Armor", 6, 400,0,0,0,0,0,0,"","",false),
        new Equipment("Leather Armor", 8, 1000,0,0,0,0,0,0,"","",false),  new Equipment("Roman Armor", 10, 3000,0,0,0,0,0,0,"","",false),   new Equipment("Chain mail", 12, 1500,0,0,0,0,0,0,"","",false),   new Equipment("Heavy mail", 14, 3000,0,0,0,0,0,0,"","",false),
        new Equipment("Coat of plates", 16, 2000,0,0,0,0,0,0,"","",false),new Equipment("Plate mail", 20, 2500,0,0,0,0,0,0,"","",false),    new Equipment("Blue mail", 22, 3000,0,0,0,0,0,0,"","",false),    new Equipment("Black mail", 25, 3500,0,0,0,0,0,0,"","",false),
        new Equipment("Red mail", 32, 4500,0,0,0,0,0,0,"","",false),      new Equipment("Purple Mail", 40, 5500,0,0,0,0,0,0,"","",false),   new Equipment("White Armor", 56, 6500,0,0,0,0,0,0,"","",false),  new Equipment("Crystal Armor", 70, 7500,0,0,0,0,0,0,"","",false)
    };

    public static Shop WeaponShop = new Shop("Billford's weapon emporium.", "Billford", "troll", "Greetings, What can I do for you", WeaponList);
    public static Shop ArmorShop = new Shop("Alya's armor shop.", "Alya", "elf", "Hey there! Looking to buy some armor?", ArmorList);

    public Shop(string name,string shopkeepName, string shopkeepRace, string shopkeepGreeting, Equipment[] ItemList)
    {
        this.name = name;
        this.shopkeepName = shopkeepName;
        this.shopkeepRace = shopkeepRace;
        this.shopkeepGreeting = shopkeepGreeting;
        this.ItemList = ItemList;
    }

    public static void GameShop(Shop shop,Creature p)
    {
        Console.Clear();
        Console.WriteLine($"You walk into {shop.name}");
        Utilities.EmbedColourText(Colour.NAME, Colour.CLASS, $"", $"{shop.shopkeepName} ", "the ", $"{shop.shopkeepRace}", " comes over to greet you");
        Utilities.ColourText(Colour.SPEAK, $"'{shop.shopkeepGreeting}'\n\n");
        Console.WriteLine("[B]uy        [S]ell");
        Console.WriteLine($"[C]haracter  [R]eturn\n\n");
        Utilities.EmbedColourText(Colour.GOLD, "You have ", $"{p.gold}", " gold\n\n");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (choice == "b")
        {
            Console.WriteLine("Great! What would you like to buy?\n");
            for (int i = 1; i < shop.ItemList.Length; i++)
            {
                Utilities.ColourText(Colour.RESET, "");
                Console.WriteLine(String.Format("{0,-4} {1,-6} {2,-15} {3,-20}{4,-23}{5,-24}", $"[{ i}]", Colour.ECITEM, $" {shop.ItemList[i].name}", Colour.ECGOLD, $"{ shop.ItemList[i].price}", Colour.RESET1));
            }
            Console.WriteLine("\n[0] Return");
            int buyChoice;
            do
            {

            } while (!int.TryParse(Console.ReadLine(), out buyChoice));
            if (buyChoice > 0 && buyChoice < shop.ItemList.Length)
            {
                if (p.gold < shop.ItemList[buyChoice].price) Console.WriteLine("\n\n'Sorry, you don't Have enough Gold'");
                else
                {
                    Console.WriteLine($"\n\nWould you like to buy {shop.ItemList[buyChoice].name}?\n\n[Y]es      [N]o\n\n");
                    string confirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
                    if (confirm == "y")
                    {
                        if (p.Weapon.name != "None" && shop.ItemList[1].name == "Dagger")
                            ShopBuy(p.Weapon, shop.ItemList[buyChoice],p, shop);
                        else if (p.Armor.name != "None" && shop.ItemList[1].name == "Cloth Armor")
                            ShopBuy(p.Armor, shop.ItemList[buyChoice],p, shop);
                        else
                        {
                            Console.WriteLine($"\n\nSmiling, {shop.shopkeepName} takes your money and gives you your {shop.ItemList[buyChoice].name}");
                            p.gold -= shop.ItemList[buyChoice].price;
                            if (shop.shopkeepName == "Billford") Utilities.Equip(shop.ItemList[buyChoice]);
                            else if (shop.shopkeepName == "Alya") Utilities.Equip(shop.ItemList[buyChoice]);
                        }
                    }
                }
            }
            Utilities.Keypress();
        }
        else if (choice == "s")
        {
            if (p.Weapon.name == "None" && p.Armor.name == "None") Console.WriteLine("You have nothing to Sell!");
            else
            {
                Console.WriteLine($"\n\nWhat would you like to Sell?\n\n");
                List<Equipment> EquipmentList = new List<Equipment> { };
                if (p.Weapon.name != "None") EquipmentList.Add(p.Weapon);
                if (p.Armor.name != "None") EquipmentList.Add(p.Armor);
                for (int i = 0; i < EquipmentList.Count; i++)
                {
                    Utilities.ColourText(Colour.RESET, "");
                    Console.WriteLine(String.Format("{0,-4} {1,-6} {2,-15} {3,-20}{4,-23}{5,-24}", $"[{ i + 1}]", Colour.ECITEM, $" {EquipmentList[i].name}", Colour.ECGOLD, $"{EquipmentList[i].price / 2}", Colour.RESET1));
                }
                Console.WriteLine("\n[0] Return");
                int sellChoice;
                do
                {

                } while (!int.TryParse(Console.ReadKey(true).KeyChar.ToString().ToLower(), out sellChoice));
                if (sellChoice > 0 && sellChoice <= shop.ItemList.Length)
                {
                    Console.WriteLine($"\n\nWould you Like to sell {EquipmentList[sellChoice - 1].name}? I'll give you {EquipmentList[sellChoice - 1].price / 2} for it\n\n[Y]es      [N]o\n\n");
                    string confirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
                    if (confirm == "y")
                    {
                        Console.WriteLine($"\n\n'Great!' {shop.shopkeepName} takes your {EquipmentList[sellChoice - 1].name} and gives you {EquipmentList[sellChoice - 1].price / 2} gold");
                        p.gold += EquipmentList[sellChoice - 1].price / 2;
                        if (p.Weapon == EquipmentList[sellChoice - 1]) Utilities.Equip(WeaponList[0]);
                        if (p.Armor == EquipmentList[sellChoice - 1]) Utilities.Equip(ArmorList[0]);
                    }
                }
            }
            Utilities.Keypress();
        }
        else if (choice == "c") Utilities.CharacterSheet(p);
        else if (choice == "r") Marburgh.Program.GameTown();
        GameShop(shop, p);
    }

    public static void ShopBuy(Equipment equip, Equipment playerEquip, Creature p, Shop shop)
    {
        Console.WriteLine($"I see you have a {equip.name}. Would you like to sell it?\n\n[Y]es      [N]o\n");
        string confirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (confirm == "y")
        {
            Console.WriteLine($"{ shop.shopkeepName} takes your {equip.name} and gives you {equip.price / 2} gold");
            p.gold += equip.price / 2;
            Console.WriteLine($"Smiling, {shop.shopkeepName} takes your money and gives you your {playerEquip.name }");
            p.gold -= playerEquip.price;
            if (shop.shopkeepName == "Billford") Utilities.Equip(playerEquip);
            else if (shop.shopkeepName == "Alya") Utilities.Equip(playerEquip);
        }
    }

    public static void ItemShop(Creature p)
    {
        Console.Clear();
        Utilities.EmbedColourText(Colour.NAME, Colour.CLASS, "You enter a cluttered shop. There are bubbling potions everywhere.", "\n\nElya", " the ", "elf ", "looks at you and smiles.\n");
        Utilities.ColourText(Colour.SPEAK, "'Well, hello there!. Are you here to buy something? Check back frequently, I'm always getting new items!;");
        Utilities.EmbedColourText(Colour.HEALTH, Colour.RAREDROP, "\n\n", "[P]otions", "    ", "[C]rafting Machine", "");
        Console.WriteLine("\n             [R]eturn to town");
        Utilities.EmbedColourText(Colour.GOLD, "\nYou have ", $"{p.gold}", " gold:\n\n");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (choice == "r")
            Marburgh.Program.GameTown();
        if (choice == "c")
        {
            Utilities.EmbedColourText(Colour.SPEAK, Colour.RAREDROP, Colour.SPEAK, Colour.GOLD, Colour.SPEAK, "", "Ah yes... A very rare thing indeed. \nA", "", " crafting machine", "", " can allow you to use the scraps of " +
                "monsters to build your own items, including weapons and armor!\nAll yours for the reasonable price of ", "", " 1000", "", " gold\nWould you like to buy it?", "");
            Console.WriteLine("\n(Y)es     (N)o");
            string confirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (confirm == "y")
            {
                if (p.gold >= 1000)
                {
                    Utilities.ColourText(Colour.SPEAK, "\n\nWonderful!");
                    Utilities.EmbedColourText(Colour.SPEAK, Colour.RAREDROP, "\n", "Elya takes your money and gives you the ", "", "crafting machine.", "");
                    p.gold -= 1000;
                    p.craft = true;
                    Utilities.Keypress();
                    ItemShop(p);
                }
            }
            Utilities.ColourText(Colour.SPEAK, "\n\nCome back when you're serious!");
        }
        if (choice == "p")
        {
            int amount = (p.pClass.cName == "Mage") ? 2 : 1;
            int buymax = (amount - p.potions == 1 && p.gold / 200 >= 1) || (amount - p.potions == 2 && p.gold / 200 >= 1 && p.gold / 200 < 2) ? 1 : (amount - p.potions == 2 && p.gold / 200 >= 2) ? 2 : 0;
            int buyChoice;
            do
            {
                Utilities.ColourText(Colour.SPEAK, "'Excellent! how many would you like to buy? They are 200 gold apiece and you can carry a maximum of ");
                Utilities.EmbedColourText(Colour.GOLD, Colour.SPEAK, "", $"{amount}", "", ".'", "");
                Utilities.EmbedColourText(Colour.SPEAK, Colour.HEALTH, Colour.SPEAK, "\n\n", "You can buy ", "", $"{buymax} ", "", "potions\n", "\n[0] Return\n");
                Console.ForegroundColor = Colour.HEALTH;
            } while (!int.TryParse(Console.ReadLine(), out buyChoice));
            Console.ForegroundColor = Colour.RESET;
            if (buyChoice == 0) ItemShop(p);
            else if (buyChoice > buymax) Utilities.ColourText(Colour.SPEAK, "\nYou have too many potions!");
            else if (p.gold < buyChoice * 200) Utilities.ColourText(Colour.SPEAK, "\n'I'm sorry, it doesn't look like you can afford that'");
            else
            {
                Utilities.ColourText(Colour.SPEAK, "'A pleasure doing business with you!'");
                Utilities.EmbedColourText(Colour.GOLD, Colour.HEALTH, "You give the elf ", $"{buyChoice * 200}", " gold and receive ", $"{buyChoice}", " potions");
                p.gold -= buyChoice * 200;
                p.potions += buyChoice;
            }
        }
        Utilities.Keypress();
        ItemShop(p);
    }
}