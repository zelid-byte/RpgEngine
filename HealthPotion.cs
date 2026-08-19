namespace ConsoleApp1;

public class HealthPotion : Item
{
    public int HealAmount { get; init; }
    public bool IsInfinite { get; set; }=false;

    public override string ToString()
    {
        return $"{Name} | исцеляет {HealAmount} ед. | цена продажи: {SellPrice}";
    }
}