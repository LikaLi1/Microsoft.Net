using System;

class GameObject
{
    public string Name { get; set; }
    public GameObject(string name)
    {
        Name = name;
        Console.WriteLine($"Создан объект: {Name}");
    }

    ~GameObject()
    {
        Console.WriteLine($"Уничтожен объект: {Name}");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Начало игры");

        GameObject player = new GameObject("Игрок");
        GameObject enemy = new GameObject("Враг");

        player = null;
        enemy = null;

        GC.Collect();
        GC.WaitForPendingFinalizers();

        Console.WriteLine("Конец игры");
    }
}
