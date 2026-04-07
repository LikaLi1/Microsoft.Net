using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Order
{
    public int Id { get; set; }
    public string Product { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public double Total => Price * Quantity;
}

public static class Logger
{
    private static readonly string logFile = "app.log";

    public static void LogInfo(string message)
    {
        Log("[INFO]", message);
    }

    public static void LogWarning(string message)
    {
        Log("[WARNING]", message);
    }

    public static void LogError(string message)
    {
        Log("[ERROR]", message);
    }

    private static void Log(string level, string message)
    {
        string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {level} {message}";
        File.AppendAllText(logFile, logEntry + Environment.NewLine);
    }

    public static void LogErrorJson(string message, Exception ex)
    {
        var errorObject = new
        {
            timestamp = DateTime.Now,
            level = "ERROR",
            message,
            exception = ex.ToString()
        };
        string json = JsonSerializer.Serialize(errorObject);
        File.AppendAllText(logFile, json + Environment.NewLine);
    }
}

class Program
{
    private static readonly string ordersFile = "orders.json";
    private static readonly string backupFile = "orders_backup.json";

    private static List<Order> orders = new List<Order>();
    private static int nextId = 1;

    static void Main()
    {
        try
        {
            LoadOrders();
        }
        catch (Exception ex)
        {
            Logger.LogErrorJson("Ошибка при загрузке заказов", ex);
            Console.WriteLine("Произошла ошибка при загрузке данных. Программа завершена.");
            return;
        }

        Logger.LogInfo("Запуск программы");
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Показать все заказы");
            Console.WriteLine("2. Добавить заказ");
            Console.WriteLine("3. Удалить заказ");
            Console.WriteLine("4. Найти заказ по Id");
            Console.WriteLine("5. Выход");
            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowOrders();
                    break;
                case "2":
                    AddOrder();
                    break;
                case "3":
                    DeleteOrder();
                    break;
                case "4":
                    FindOrder();
                    break;
                case "5":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Некорректный выбор");
                    break;
            }
        }
        SaveOrders();
        Console.WriteLine("Программа завершена");
    }

    static void LoadOrders()
    {
        if (File.Exists(ordersFile))
        {
            try
            {
                string json = File.ReadAllText(ordersFile);
                orders = JsonSerializer.Deserialize<List<Order>>(json);
                if (orders == null)
                    orders = new List<Order>();
                else
                {
                    if (orders.Count > 0)
                        nextId = Math.Max(nextId, MaxId() + 1);
                }
            }
            catch (JsonException ex)
            {
                Logger.LogErrorJson("Ошибка десериализации файла заказов", ex);
                Console.WriteLine("Ошибка формата файла заказов. Загружен пустой список.");
                orders = new List<Order>();
            }
            catch (Exception ex)
            {
                Logger.LogErrorJson("Ошибка при чтении файла заказов", ex);
                throw;
            }
        }
        else
        {
            orders = new List<Order>();
        }
    }

    static void SaveOrders()
    {
        try
        {
            if (File.Exists(ordersFile))
            {
                File.Copy(ordersFile, backupFile, true);
            }
            string json = JsonSerializer.Serialize(orders, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ordersFile, json);
        }
        catch (Exception ex)
        {
            Logger.LogErrorJson("Ошибка при сохранении файла заказов", ex);
            Console.WriteLine("Ошибка при сохранении данных.");
        }
    }

    static void ShowOrders()
    {
        if (orders.Count == 0)
        {
            Console.WriteLine("Нет заказов.");
            return;
        }
        foreach (var order in orders)
        {
            Console.WriteLine($"Id: {order.Id}, Товар: {order.Product}, Цена: {order.Price}, Количество: {order.Quantity}, Итог: {order.Total}");
        }
    }

    static void AddOrder()
    {
        string product;
        double price;
        int quantity;

        Console.Write("Введите название товара: ");
        product = Console.ReadLine();

        Console.Write("Введите цену: ");
        if (!double.TryParse(Console.ReadLine(), out price) || price <= 0)
        {
            Console.WriteLine("Некорректная цена");
            Logger.LogWarning("Некорректный ввод цены при добавлении заказа");
            return;
        }

        Console.Write("Введите количество: ");
        if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
        {
            Console.WriteLine("Некорректное количество");
            Logger.LogWarning("Некорректный ввод количества при добавлении заказа");
            return;
        }

        var order = new Order
        {
            Id = nextId++,
            Product = product,
            Price = price,
            Quantity = quantity
        };
        orders.Add(order);
        SaveOrders();
        Logger.LogInfo($"Добавлен заказ Id={order.Id}");
        Console.WriteLine("Заказ добавлен");
    }

    static void DeleteOrder()
    {
        Console.Write("Введите Id заказа для удаления: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Некорректный ввод");
            Logger.LogWarning("Некорректный ввод при удалении заказа");
            return;
        }

        var order = orders.Find(o => o.Id == id);
        if (order != null)
        {
            orders.Remove(order);
            SaveOrders();
            Logger.LogInfo($"Удален заказ Id={id}");
            Console.WriteLine("Заказ удален");
        }
        else
        {
            Console.WriteLine("Заказ не найден");
            Logger.LogWarning($"Попытка удалить несуществующий заказ Id={id}");
        }
    }

    static void FindOrder()
    {
        Console.Write("Введите Id заказа для поиска: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Некорректный ввод");
            Logger.LogWarning("Некорректный ввод при поиске заказа");
            return;
        }

        var order = orders.Find(o => o.Id == id);
        if (order != null)
        {
            Console.WriteLine($"Id: {order.Id}, Товар: {order.Product}, Цена: {order.Price}, Количество: {order.Quantity}, Итог: {order.Total}");
        }
        else
        {
            Console.WriteLine("Заказ не найден");
        }
    }

    static int MaxId()
    {
        int max = 0;
        foreach (var order in orders)
        {
            if (order.Id > max)
                max = order.Id;
        }
        return max;
    }
}
