using System;

class Enemy
{
    public string Name { get; set; }
    public Enemy(string name)
    {
        Name = name;
        Console.WriteLine($"Создан объект: {Name}");
    }

    ~Enemy()
    {
        Console.WriteLine($"Уничтожен объект: {Name}");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Начало игры");

        for (int i = 0; i < 100000; i++)
        {
            new Enemy($"Враг {i}");
        }

        GC.Collect();
        GC.WaitForPendingFinalizers();

        Console.WriteLine("Конец игры");
    }
}
