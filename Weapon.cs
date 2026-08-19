namespace ConsoleApp1;

public class Weapon : Item
{
    
    public int BonusDamage  { get; init; }
    public override string ToString()
    {
        return $"{Name} | c {BonusDamage} бонусного урона | цена продажи: {SellPrice} ";
    }
}