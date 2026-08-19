namespace ConsoleApp1;

public class Armor : Item
{
    public ArmorType Slots { get; init; }
    public int Defense  { get; init; }
    public override string ToString()
    {
        return $"{Name} (броня: {Slots}) | защита : {Defense} | цена продажи: {SellPrice}";
    }
}