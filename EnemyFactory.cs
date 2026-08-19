namespace ConsoleApp1;

public static class EnemyFactory
{
    private static Random random = new Random();

    public static Hero CreateGoblin()
    {
        Hero goblin = new Hero("гоблин", 45, 10,45, 20);
        goblin.EquipArmor(new Armor { Name = "рваные шорты", Defense = 1 , Slots = ArmorType.Leggings});
        goblin.EquipArmor(new Armor { Name = "бандана", Defense = 1 , Slots = ArmorType.Helmet});
        goblin.EquipArmor(new Armor { Name = "тапки", Defense = 1 , Slots = ArmorType.Boots});
        goblin.EquipArmor(new Armor { Name = "майка алкашка", Defense = 1 , Slots = ArmorType.Chest});
        goblin.EquipWeapon(new Weapon { Name = "кинжал", BonusDamage = 6 });
        goblin.Items.Add(new HealthPotion { Name = "настойка из чая", HealAmount = 15 });
        goblin.GoldReward = 25;
        goblin.ExpReward = 30;
        return goblin;
    }

    public static Hero CreateRogue()
    {
        Hero rogue = new Hero("разбойник", 70, 15,70);
        rogue.EquipArmor(new Armor { Name = "лёгкие штаны", Defense = 2 , Slots = ArmorType.Leggings});
        rogue.EquipArmor(new Armor { Name = "капюшон", Defense = 1 , Slots = ArmorType.Helmet});
        rogue.EquipArmor(new Armor { Name = "кожаные сапоги", Defense = 2 , Slots = ArmorType.Boots});
        rogue.EquipArmor(new Armor { Name = "деревянный нагрудник", Defense = 3 , Slots = ArmorType.Chest});
        rogue.EquipWeapon(new Weapon { Name = "стальной меч", BonusDamage = 10 });
        rogue.Items.Add(new HealthPotion { Name = "среднее зелье", HealAmount = 35 });
        rogue.GoldReward = 15;
        rogue.ExpReward = 45;
        return rogue;
    }

    public static Hero CreateOrc()
    {
        Hero orc = new Hero("орк", 110, 18, 110,0);
        orc.EquipArmor(new Armor { Name = "тяжелые поножи", Defense = 7 , Slots = ArmorType.Leggings});
        orc.EquipArmor(new Armor { Name = "тяжелый шлем", Defense = 3 , Slots = ArmorType.Helmet});
        orc.EquipArmor(new Armor { Name = "тяжелые сапоги", Defense = 4 , Slots = ArmorType.Boots});
        orc.EquipArmor(new Armor { Name = "тяжелый нагрудник", Defense = 7 , Slots = ArmorType.Chest});
        orc.EquipWeapon(new Weapon { Name = "двуручный топор", BonusDamage = 15 });
        orc.Items.Add(new HealthPotion { Name = "большое зелье", HealAmount = 50 });
        orc.GoldReward = 30;
        orc.ExpReward = 75;
        return orc;
    }

    public static Hero GetRandomEnemy()
    {
        int roll = random.Next(1, 4); // Случайное число от 1 до 3
        
        switch (roll)
        {
            case 1: return CreateGoblin();
            case 2: return CreateRogue();
            case 3: return CreateOrc();
            default: return CreateGoblin();
        }
    }


}