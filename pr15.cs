using System;

class GameItem
{
    public string Name { get; set; }
    public GameItem(string name)
    {
        Name = name;
        Console.WriteLine($"Создан объект: {Name}");
    }

    ~GameItem()
    {
        Console.WriteLine($"Уничтожен объект: {Name}");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Начало игры");

        WeakReference itemRef = new WeakReference(new GameItem("Игрок"));

        if (itemRef.IsAlive)
        {
            Console.WriteLine("Предмет ещё существует");
        }

        GC.Collect();
        itemRef = new WeakReference(new GameItem("макака"));

        if (itemRef.IsAlive)
        {
            Console.WriteLine("Предмет ещё существует");
        }
        else 
        {
            Console.WriteLine("Предмет уничтожен сборщиком мусора");
        }
    }
}
