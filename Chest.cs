namespace ConsoleApp1;



public class Chest
{
    private List<Item> rewards = new List<Item>();

    public Chest()
    {
        Random rnd = new Random();

        Weapon legendaryWeapon = new Weapon 
        { 
            Name = "Клинок Дракона", 
            BonusDamage = rnd.Next(30, 41) 
        };

        Armor legendaryArmor = new Armor 
        { 
            Name = "Доспех Древних", 
            Defense = rnd.Next(12, 18), 
            Slots = ArmorType.Chest 
        };

        HealthPotion infinitePotion = new HealthPotion 
        { 
            Name = "Бездонный Флакон", 
            HealAmount = 10, 
            IsInfinite = true 
        };
        
        rewards.Add(legendaryWeapon);
        rewards.Add(legendaryArmor);
        rewards.Add(infinitePotion);

        for (int i = rewards.Count - 1; i > 0; i--)
        {
            int j = rnd.Next(i + 1);
            Item temp = rewards[i];
            rewards[i] = rewards[j];
            rewards[j] = temp;
        }
    }

    public Item Open(int choice)
    {
        int index = choice - 1;
        if (index < 0 || index >= rewards.Count)
        {
            index = 0;
        }

        return rewards[index];
    }
}