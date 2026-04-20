using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

// Модель
public class Order
{
    public int Id { get; set; }
    public string Product { get; set; }
    public double Price { get; set; }
}

// Интерфейсы
public interface IOrderRepository
{
    void Save(Order order);
    List<Order> GetAll();
    void Delete(int orderId);
}

public interface ILogger
{
    void Log(string message);
}

public interface IOrderService
{
    void CreateOrder(Order order);
    List<Order> GetOrders();
    void RemoveOrder(int orderId);
}

// Реализация репозитория файловый (JSON)
public class FileOrderRepository : IOrderRepository
{
    private readonly string _filePath = "orders.json";

    public void Save(Order order)
    {
        var orders = GetAll();
        order.Id = orders.Any() ? orders.Max(o => o.Id) + 1 : 1;
        orders.Add(order);
        SaveToFile(orders);
    }

    public List<Order> GetAll()
    {
        if (!File.Exists(_filePath))
            return new List<Order>();
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Order>>(json) ?? new List<Order>();
    }

    public void Delete(int orderId)
    {
        var orders = GetAll();
        orders = orders.Where(o => o.Id != orderId).ToList();
        SaveToFile(orders);
    }

    private void SaveToFile(List<Order> orders)
    {
        var json = JsonSerializer.Serialize(orders);
        File.WriteAllText(_filePath, json);
    }
}

// Реализация логгера — в файл
public class FileLogger : ILogger
{
    private readonly string _logFile = "app.log";
    public void Log(string message)
    {
        var logMessage = $"[{DateTime.Now}] {message}";
        File.AppendAllText(_logFile, logMessage + Environment.NewLine);
    }
}

// Вариант консольного логгера (по желанию)
public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {message}");
    }
}

// Реализация сервиса
public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly ILogger _logger;

    public OrderService(IOrderRepository repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public void CreateOrder(Order order)
    {
        try
        {
            if (order.Price <= 0)
                throw new ArgumentException("Цена должна быть больше 0");
            _repository.Save(order);
            _logger.Log($"Создан заказ: {order.Product}, цена: {order.Price}");
        }
        catch (Exception ex)
        {
            _logger.Log($"Ошибка при создании заказа: {ex.Message}");
        }
    }

    public List<Order> GetOrders()
    {
        try
        {
            return _repository.GetAll();
        }
        catch (Exception ex)
        {
            _logger.Log($"Ошибка при получении заказов: {ex.Message}");
            return new List<Order>();
        }
    }

    public void RemoveOrder(int orderId)
    {
        try
        {
            _repository.Delete(orderId);
            _logger.Log($"Заказ с ID {orderId} удалён");
        }
        catch (Exception ex)
        {
            _logger.Log($"Ошибка при удалении заказа: {ex.Message}");
        }
    }
}

// Главное приложение
class Program
{
    static void Main()
    {
        var services = new ServiceCollection();
      
        services.AddTransient<ILogger, FileLogger>();
        services.AddTransient<IOrderRepository, FileOrderRepository>();
        services.AddTransient<IOrderService, OrderService>();

        var provider = services.BuildServiceProvider();

        var orderService = provider.GetService<IOrderService>();

        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Добавить заказ");
            Console.WriteLine("2. Показать заказы");
            Console.WriteLine("3. Удалить заказ");
            Console.WriteLine("4. Выход");
            Console.Write("Выберите действие: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите название продукта: ");
                    var product = Console.ReadLine();

                    Console.Write("Введите цену: ");
                    if (double.TryParse(Console.ReadLine(), out double price))
                    {
                        var order = new Order { Product = product, Price = price };
                        orderService.CreateOrder(order);
                    }
                    else
                    {
                        Console.WriteLine("Некорректное значение цены.");
                    }
                    break;

                case "2":
                    var orders = orderService.GetOrders();
                    Console.WriteLine("Заказы:");
                    foreach (var o in orders)
                    {
                        Console.WriteLine($"ID: {o.Id}, Товар: {o.Product}, Цена: {o.Price}");
                    }
                    break;

                case "3":
                    Console.Write("Введите ID заказа для удаления: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        orderService.RemoveOrder(id);
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ID.");
                    }
                    break;

                case "4":
                    Console.WriteLine("Выход из программы.");
                    return;

                default:
                    Console.WriteLine("Некорректный выбор");
                    break;
            }
        }
    }
}
