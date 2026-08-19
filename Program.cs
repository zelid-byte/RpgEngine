using ConsoleApp1;

Hero warrior = new("Воин", 100, 15,100) {Gold=150};
HealthPotion healthPotion = new HealthPotion{Name="зелье исцеления", HealAmount = 30};
warrior.Items.Add(healthPotion);

Store store = new Store();

bool InGame = true;

while (InGame)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Clear();
    Console.WriteLine("========================================");
    Console.WriteLine($"         ГОРОД — ХАБ (Золото: {warrior.Gold} з.)");
    Console.WriteLine("========================================");
    Console.WriteLine("1. Сходить в Подземелье (Данж)");
    Console.WriteLine("2. Зайти в Лавку Торговца (Магазин)");
    Console.WriteLine("3. Персонаж и Инвентарь");
    Console.WriteLine("0. Выйти из игры");
    Console.WriteLine("========================================");
    Console.Write("Выбери действие: ");
    Console.ForegroundColor = ConsoleColor.White;

    int choice = int.Parse(Console.ReadLine());
    
    switch (choice)
    {
        case 1:
            StartDange();
            break;
        case 2:
            ShowStore();
            break;
        case 3:
            Console.WriteLine(warrior.ToString());
            warrior.ShowInventory();
            WaitKey();
            break;
        case 0:
            Console.WriteLine("вы ходите из игры!");
            WaitKey();
            InGame = false;
            break;
        default:
            WaitKey();
            break;
        
    }

   
}















void StartDange()
{
   for (int room= 0; room < 5; room++)
{
    Console.WriteLine($"\n=== Вы в {room+1} комнате из 5 ===");
    Hero enemy = EnemyFactory.CreateGoblin();


    while (warrior.IsAlive && enemy.IsAlive)
    {
        int? choice = null;
        Console.WriteLine("1. Атаковать\n2. Инвентарь");
        choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.WriteLine();
                Console.WriteLine("--- БИТВА НАЧИНАЕТСЯ ---");
            {
                Console.WriteLine(warrior);
                Console.WriteLine(enemy);
                Console.WriteLine("-----------------------");

                warrior.Attack(enemy);

                if (!enemy.IsAlive)
                {
                    Console.WriteLine($"{enemy.Name} мертв. {warrior.Name} победил!");
                    warrior.AddReward(enemy);
                    break;
                }

                enemy.MakeTurn(warrior);


                if (!warrior.IsAlive)
                {
                    Console.WriteLine($"{warrior.Name} мертв. {enemy.Name} победил!");
                    break;
                }

                Console.WriteLine();
            }
                break;

            case 2:
                
                for (int i = 0; i < warrior.Items.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {warrior.Items[i].Name}");
                }

                Console.WriteLine("Выберите предмет (или 0 для отмены):");
                int itemChoice = int.Parse(Console.ReadLine());

                if (itemChoice > 0 && itemChoice <= warrior.Items.Count)
                {

                    warrior.UseItem(warrior.Items[itemChoice - 1]);
                    enemy.Attack(warrior);
                }

                break;
        }

        if (!warrior.IsAlive)
        {
            Console.WriteLine($"Вы мертвы! Вы погибли на {room+1} этаже! Попробуйте снова!");
            Console.WriteLine("\nнажмите любую клавишу чтобы попасть в меню! ");
            warrior.Health=warrior.MaxHealth;
            Console.ReadKey(true);
            return ;
        }
        

    }
}

if (warrior.IsAlive)
{
    Console.WriteLine($"Поздравляем вы смогли пройти данж! Вы настоящий герой!" +
                      $"А всем героям должна читаться награда! Поэтому у вас есть выбор:\n" +
                      $"Eсть три сундука в каждом находится неизвестный легендарный предмет!\n");
    Console.Write("Какой сундук вы бы хотели открыть?\n" +
                      "(Напишите 1/2/3):");
    int choice = int.Parse(Console.ReadLine()??"1");
    Chest chest = new Chest();

    Item prize = chest.Open(choice);

    warrior.Items.Add(prize);

    Console.WriteLine($"\n✨ Вы открыли сундук и получили: {prize.Name}!");
    warrior.Health=warrior.MaxHealth;
} 

}


void WaitKey()
{
    Console.WriteLine("\nнажмите на любую кнопку!");
    Console.ReadKey(true);
}



void ShowStore()
{
    store.GenerateStore();
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine("========================================");
    Console.WriteLine($" МАГАЗИН У МИХАЛЫЧА (Золото: {warrior.Gold} з.)");
    Console.WriteLine("========================================");
    Console.WriteLine("1. Просмотреть магазин");
    Console.WriteLine("2. Обновить ассортимент");
    Console.WriteLine("3. Продать вещи");
    Console.WriteLine("0. Выйти из магазина");
    Console.WriteLine("========================================");
    Console.Write("Ваш выбор:");
    Console.ForegroundColor = ConsoleColor.White;
    int choiceOfStore = int.Parse(Console.ReadLine());

    switch (choiceOfStore)
    {
        
        case 1:
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            store.DisplayStore();
            Console.Write("\n введите номер предметы который хотите купить (0: если ничего не будете брать): ");
            int indexOfBuy = int.Parse(Console.ReadLine());
            if (indexOfBuy != 0)
            {
                store.BuyItem(warrior, indexOfBuy);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                WaitKey();
                break;
            }
            break;
        case 2:
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            store.GenerateStore();
            Console.WriteLine("\n магазин обновлён!");
            WaitKey();
            Console.ForegroundColor = ConsoleColor.White;
            break;
        case 3:
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            warrior.ShowInventory();
            Console.Write("\n введите номер предметы который хотите продать(0: если ничего не будете продавать): ");
            int indexOfSell = int.Parse(Console.ReadLine());
            if (indexOfSell != 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                store.SellItem(warrior, indexOfSell);
            }
       
            
            else
            {
                WaitKey();
                Console.ForegroundColor = ConsoleColor.White;
                break;
                
            }
            
            break;
        case 0:
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("вы выходите из магазина!");
            WaitKey();
            Console.ForegroundColor = ConsoleColor.White;
            break;
        default:
            WaitKey();
            break;
    }
}