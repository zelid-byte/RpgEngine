namespace ConsoleApp1;

public class Store
{
    private List<Item> AllItmes = new List<Item>() ;
    private List<Item> Stock = new List<Item>();

   private void InitMasterCatalog()
{
    // --- ОРУЖИЕ ---
    AllItmes.Add(new Weapon { Name = "Деревянная дубина", BonusDamage = 5, Price = 15 });
    AllItmes.Add(new Weapon { Name = "Железный короткий меч", BonusDamage = 12, Price = 45 });
    AllItmes.Add(new Weapon { Name = "Стальной двуручник", BonusDamage = 22, Price = 90 });
    AllItmes.Add(new Weapon { Name = "Катана «Шёпот ветра»", BonusDamage = 30, Price = 150 });
    AllItmes.Add(new Weapon { Name = "Огненный секира", BonusDamage = 45, Price = 260 });

    // --- БРОНЯ (Разные слоты) ---
    // Шлемы
    AllItmes.Add(new Armor { Name = "Кожаный капюшон", Defense = 2, Slots = ArmorType.Helmet, Price = 20 });
    AllItmes.Add(new Armor { Name = "Шлем стражника", Defense = 5, Slots = ArmorType.Helmet, Price = 60 });
    AllItmes.Add(new Armor { Name = "Драконий шлем", Defense = 10, Slots = ArmorType.Helmet, Price = 140 });

    // Нагрудники
    AllItmes.Add(new Armor { Name = "Стеганый кушак", Defense = 4, Slots = ArmorType.Chest, Price = 35 });
    AllItmes.Add(new Armor { Name = "Стальная кираса", Defense = 9, Slots = ArmorType.Chest, Price = 100 });
    AllItmes.Add(new Armor { Name = "Панцирь титана", Defense = 16, Slots = ArmorType.Chest, Price = 220 });

    // Поножи
    AllItmes.Add(new Armor { Name = "Кожаные штаны", Defense = 2, Slots = ArmorType.Leggings, Price = 20 });
    AllItmes.Add(new Armor { Name = "Железные поножи", Defense = 6, Slots = ArmorType.Leggings, Price = 70 });

    // Сапоги
    AllItmes.Add(new Armor { Name = "Старые сапоги", Defense = 1, Slots = ArmorType.Boots, Price = 10 });
    AllItmes.Add(new Armor { Name = "Кованые ботинки", Defense = 4, Slots = ArmorType.Boots, Price = 45 });

    // --- ЗЕЛЬЯ ---
    AllItmes.Add(new HealthPotion { Name = "Малое зелье лечения", HealAmount = 25, IsInfinite = false, Price = 25 });
    AllItmes.Add(new HealthPotion { Name = "Большое зелье лечения", HealAmount = 60, IsInfinite = false, Price = 55 });
    AllItmes.Add(new HealthPotion { Name = "Эликсир полной регенерации", HealAmount = 100, IsInfinite = false, Price = 110 });
    AllItmes.Add(new HealthPotion { Name = "Бездонная склянка", HealAmount = 15, IsInfinite = true, Price = 350 });
}

    public Store()
    {
        InitMasterCatalog();
        GenerateStore();
    }
    public void GenerateStore()
    {
        Stock.Clear();
        Random rnd = new Random();
        int itemCount = rnd.Next(3, 6);
        for (int i = 0; i < itemCount; i++)
        {
            int randomItem = rnd.Next(AllItmes.Count);
            Stock.Add(AllItmes[randomItem]);
        }
    }
    public void DisplayStore()
    {
        if (Stock.Count == 0)
        {
            Console.WriteLine("Витрина магазина пуста!");
            return;
        }

        Console.WriteLine("--- ВИТРИНА МАГАЗИНА ---");
        for (int i = 0; i < Stock.Count; i++)
        {
            Console.WriteLine($"[{i + 1}] {Stock[i]} | Цена: {Stock[i].Price} золота");
        }
    }
    public void BuyItem(Hero hero, int choiceOfItem)
    {
        Item item = Stock[choiceOfItem-1];
        if (!Stock.Contains(item))
        {
            Console.WriteLine("Такого товара нет в магазине!");
            return;
        }

        if (hero.Gold < item.Price)
        {
            Console.WriteLine($"Не хватает золота! Нужно: {item.Price} з., а у вас: {hero.Gold} з.");
            return;
        }

        hero.Gold -= item.Price;        
        hero.Items.Add(item);        
        Stock.Remove(item);            
        Console.WriteLine($"\n✅ Вы успешно купили {item.Name} за {item.Price} золота!");
        Console.WriteLine($"Остаток золота: {hero.Gold}");
        Console.ReadKey(true);
        Console.WriteLine("нажмите на левую кнопку чтобы продолжить");
    }

    public void SellItem(Hero hero, int choiceOfItem)
    {
       Item item = hero.Items[choiceOfItem-1];
        if (!hero.Items.Contains(item))
        {
            Console.WriteLine("У вас нет этого предмета в инвентаре!");
            return;
        }

        hero.Items.Remove(item); 
        hero.Gold += item.SellPrice;   
        Stock.Add(item);                

        Console.WriteLine($"\n💰 Вы продали {item.Name} за {item.SellPrice} золота!");
        Console.WriteLine($"Ваше золото: {hero.Gold}");
        Console.ReadKey(true);
        Console.WriteLine("нажмите на левую кнопку чтобы продолжить");
    }
}