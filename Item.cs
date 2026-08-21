using System.Text.Json.Serialization;

namespace ConsoleApp1;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(Weapon), "weapon")]
[JsonDerivedType(typeof(Armor), "armor")]
[JsonDerivedType(typeof(HealthPotion), "potion")]
public class Item
{
    public string Name { get; set; }
    public int Price { get; set; }
    public int SellPrice => Price / 2;
}