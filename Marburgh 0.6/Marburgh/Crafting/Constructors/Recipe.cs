public enum EnhancementType { Damage, Health, Gold, XP }

public class Recipe
{
    public EnhancementType type;
    public string name;
    public int ingredient1Amount;
    public int ingredient2Amount;
    public int ingredient3Amount;
    public int ingredient4Amount;
    public Drop ingredient1;
    public Drop ingredient2;
    public Drop ingredient3;
    public Drop ingredient4;
    public int succesChance;
    public int effect;
    public bool prefix;

    public Recipe(string name, int ingredient1Amount, int ingredient2Amount,  int ingredient3Amount , int ingredient4Amount,  Drop ingredient1, Drop ingredient2,Drop ingredient3,Drop ingredient4, int succesChance, EnhancementType type, int effect, bool prefix)
    {
        this.effect = effect;
        this.type = type;
        this.name = name;
        this.ingredient1Amount =ingredient1Amount;
        this.ingredient2Amount =ingredient2Amount;
        this.ingredient3Amount =ingredient3Amount;
        this.ingredient4Amount =ingredient4Amount;
        this.ingredient1 =  ingredient1;
        this.ingredient2 =  ingredient2;
        this.ingredient3 =  ingredient3;
        this.ingredient4 =  ingredient4;
        this.succesChance =  succesChance;
        this.prefix = prefix;
    }
}

