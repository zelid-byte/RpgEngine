using System.Runtime.CompilerServices;

namespace ConsoleApp1;

public class Hero

{
    private Random random = new Random();  
    public Weapon EquippedWeapon { get; set; }
    public int MaxHealth { get; set; }
    public int Damage { get; set; }
    private int health;
    public List<Item> Items { get; set; } = new List<Item>();
    public int DodgeChance { get; set; } 
    public int ExpReward { get; set; }
    public int GoldReward { get; set; }
    public int Lvl { get; private set; } = 1;
    public int CurrentExp { get; private set; } = 0;
    public int Gold { get; set; } = 0;
    
    public void UseItem(Item item)
    {
        if (item is Weapon weapon)
        {
            EquipWeapon(weapon);
        }
        else if (item is Armor armor)
        {
            EquipArmor(armor);
            
        }
        else if (item is HealthPotion potion)
        {
            if (health >= MaxHealth)
            {
                Console.WriteLine("Здоровье и так максимально! Зелье не потрачено.");
                return; 
            }

            int oldHealth = health;
            Health += potion.HealAmount; 
            int healedAmount = health - oldHealth;

            Console.WriteLine($"{Name} выпил {potion.Name} и восстановил {healedAmount} HP!");
            if (!potion.IsInfinite)
            {
                Items.Remove(potion);
            }
        }
    }
    public void EquipWeapon(Weapon weapon)
    {
        EquippedWeapon=weapon;
        Console.WriteLine($"Герой {Name} взял в руки {weapon.Name}");
    }

    Dictionary<ArmorType,Armor> EquippedArmor { get; set; } = new Dictionary<ArmorType, Armor>();
    public void EquipArmor(Armor armor)
    {
        EquippedArmor[armor.Slots] = armor;
        Console.WriteLine($"Герой {Name} одел на себя {armor.Name}");
    }

    public void ShowInfo()
    {
        System.Console.WriteLine($"У героя {Name} имеется {health} здоровья");
    }
    public string  Name{get; init;}
    public int Health
    {
        get{return health;}
        set
        {
            if(value <0)
            {
                System.Console.WriteLine("здоровье не может быть меньше нуля");
                health=0;
            }
            else if (value>MaxHealth)
            {
                System.Console.WriteLine($"на здоровье не может быть выше {MaxHealth}");
                health=MaxHealth;
            }
            else health=value;
        }
    }
    public Hero (string name, int maxHealth, int damage, int health, int dodgeChance=10)
    {this.Name=name; this.MaxHealth=maxHealth;  this.Damage=damage; this.Health=health;  this.DodgeChance=dodgeChance;}

    public void Attack(Hero target)
    {
        Random rnd = new Random();
        int Chance = rnd.Next(0, 100);
        if (Chance <=target.DodgeChance)
        {
            Console.WriteLine($"{target.Name} уклонился от атаки!");
        }
        else
        {


            int TotalDamage = 0;
            int weaponBonus = this.EquippedWeapon?.BonusDamage ?? 0;
            int targetDefense = 0;
            int DamageToHero = random.Next(this.Damage - 5, this.Damage + 5 + 1);

            foreach (var armor in target.EquippedArmor)
            {
                targetDefense += armor.Value.Defense;
            }

            TotalDamage = DamageToHero + weaponBonus - targetDefense;
            if (TotalDamage <= 0)
            {
                TotalDamage = 1;
            }

            int CriticalChance = random.Next(0, 100);
            if (CriticalChance <= 20)
            {
                TotalDamage *= 2;
                Console.WriteLine($"Герой {this.Name} наносит критический удар!");
            }

            target.Health = target.Health - TotalDamage;
            Console.WriteLine($"{this.Name} наносит {TotalDamage} урона герою {target.Name}");
        }
    }

    public bool IsAlive
    {
        get
        {
            if(Health<=0)
                return false;
            else
            {
                return true;    
            }
        }
    }

    public override string ToString()
    {
        string weaponInfo = EquippedWeapon != null
            ? $"{EquippedWeapon.Name} c уроном {EquippedWeapon.BonusDamage}"
            : "голые руки";
        string armorInfo = "";
        foreach (var item in EquippedArmor)
        {
                armorInfo += EquippedArmor != null
                ? $"{item.Value.Name} c защитой {item.Value.Defense} "
                : "без брони";
        }

        return $"Герой {Name} (Здоровье: {Health}, Оружие: {weaponInfo}, Броня: {armorInfo}, Базовый разброс: {Damage - 5}-{Damage + 5})";
    }

    public void MakeTurn(Hero opponent) 
    {
        if (this.Health < 35)
        {
            HealthPotion potionToUse = null;
            foreach (var item in this.Items)
            {
                if (item is HealthPotion healthPotion)
                {
                    potionToUse = healthPotion;
                    break;
                }
            }
            if (potionToUse != null)
            {
                UseItem(potionToUse);
                return; 
            }
        }

        int currentBonus = EquippedWeapon?.BonusDamage ?? 0;
        Weapon bestWeapon = null;
    
        foreach (var item in this.Items)
        {
            if (item is Weapon weapon && weapon.BonusDamage > currentBonus)
            {
                currentBonus = weapon.BonusDamage;
                bestWeapon = weapon;
            }
        }

        
        if (bestWeapon != null)
        {
            UseItem(bestWeapon);
            return; 
        }
        
        Attack(opponent);
    }

    public void AddReward(Hero opponent)
    {
        this.CurrentExp+=opponent.ExpReward;
        this.Gold+=opponent.GoldReward;
        Console.WriteLine($"{this.Name} получил +{opponent.ExpReward} опыта и +{opponent.GoldReward} золота с врага {opponent.Name} ");
        if (this.CurrentExp >= 100)
        {
            this.CurrentExp -= 100;
            Lvl++;
            this.MaxHealth+=15;
            this.Damage+=5;
            this.Health+=15;
            Console.WriteLine($"{this.Name} получил новый уровень! Теперь у него {Lvl} уровень и повышены характеристики (+5 к атаке, +15 к максимальному здоровью)!");
        }
    }

    public void ShowInventory()
    {
        if (Items.Count == 0)
        {
            Console.WriteLine("Инвентарь пуст!");
            return;
        }

        Console.WriteLine("--- Инвентарь ---");
        for (int i = 0; i < Items.Count; i++)
        {
            Console.WriteLine($"[{i + 1}] {Items[i].ToString()}");
        }
    }
};



