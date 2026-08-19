namespace ConsoleApp1;

public class Item
{
    public string Name { get; set; }
    public int Price { get; set; }
    public int SellPrice => Price/2;
}