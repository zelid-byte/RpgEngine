using System.Text.Json;

namespace ConsoleApp1;

static public class SaveSystem
{
    private static string filePath = "hero_save.json";

    private static JsonSerializerOptions options = new JsonSerializerOptions
    {
        WriteIndented = true,
    };

    public static void SaveHero(Hero hero)
    {
        string json = JsonSerializer.Serialize(hero, options);
        File.WriteAllText(filePath, json);
        Console.WriteLine("💾 Игра успешно сохранена в hero_save.json!");
    }

    public static Hero LoadHero()
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("⚠️ Файл сохранения не найден!");
            return null;
        }

        string json = File.ReadAllText(filePath);
        Console.WriteLine("📂 Сохранение успешно загружено!");
        return JsonSerializer.Deserialize<Hero>(json, options);
    }
}