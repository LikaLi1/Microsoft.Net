using System;
using System.IO;

public class Logger
{
    private readonly string logDirectory = AppDomain.CurrentDomain.BaseDirectory;
    private readonly string logFileName = "app.log";
    private readonly long maxFileSize = 1 * 1024 * 1024;
    private int logFileIndex = 0;
    private string currentLogFilePath;

    public Logger()
    {
        currentLogFilePath = Path.Combine(logDirectory, logFileName);
        if (File.Exists(currentLogFilePath))
        {
            logFileIndex = GetNextLogFileIndex();
        }
        else
        {
            logFileIndex = 0;
        }
        currentLogFilePath = GetLogFilePath(logFileIndex);
    }

    public void Info(string message)
    {
        Log("[INFO]", message);
    }

    public void Warning(string message)
    {
        Log("[WARNING]", message);
    }

    public void Error(string message)
    {
        Log("[ERROR]", message);
    }

    private void Log(string level, string message)
    {
        string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {level} {message}";
        EnsureCapacity();
        File.AppendAllText(currentLogFilePath, logEntry + Environment.NewLine);
    }

    private void EnsureCapacity()
    {
        if (File.Exists(currentLogFilePath))
        {
            FileInfo fi = new FileInfo(currentLogFilePath);
            if (fi.Length >= maxFileSize)
            {
                logFileIndex++;
                currentLogFilePath = GetLogFilePath(logFileIndex);
            }
        }
    }

    private int GetNextLogFileIndex()
    {
        int index = 0;
        while (File.Exists(GetLogFilePath(index)))
        {
            index++;
        }
        return index;
    }

    private string GetLogFilePath(int index)
    {
        if (index == 0)
            return Path.Combine(logDirectory, logFileName);
        else
            return Path.Combine(logDirectory, $"app_{index}.log");
    }
}

class Program
{
    static void Main()
    {
        Logger logger = new Logger();

        logger.Info("Приложение запущено");
        logger.Warning("Это предупреждение");
        logger.Error("Это ошибка");
    }
}


using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

public class UserManager
{
    private readonly string filePath = "users.json";
    private List<User> users;

    public UserManager()
    {
        Load();
    }

    public void Load()
    {
        if (!File.Exists(filePath))
        {
            users = new List<User>();
            Save();
            return;
        }

        try
        {
            string json = File.ReadAllText(filePath);
            users = JsonSerializer.Deserialize<List<User>>(json);
            if (users == null)
                users = new List<User>();
        }
        catch (JsonException)
        {
            Console.WriteLine("Ошибка при чтении файла JSON — файл поврежден. Инициализация пустого списка.");
            users = new List<User>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            users = new List<User>();
        }
    }
    public bool AddUser(User newUser)
    {
        if (users.Any(u => u.Id == newUser.Id))
        {
            Console.WriteLine($"Пользователь с Id={newUser.Id} уже существует.");
            return false;
        }
        users.Add(newUser);
        Save();
        return true;
    }
    public bool RemoveUser(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            Console.WriteLine($"Пользователь с Id={id} не найден.");
            return false;
        }
        users.Remove(user);
        Save();
        return true;
    }
    public bool UpdateUser(User updatedUser)
    {
        var index = users.FindIndex(u => u.Id == updatedUser.Id);
        if (index == -1)
        {
            Console.WriteLine($"Пользователь с Id={updatedUser.Id} не найден.");
            return false;
        }
        users[index] = updatedUser;
        Save();
        return true;
    }
    public void Save()
    {
        try
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении файла: {ex.Message}");
        }
    }

    public void ShowAllUsers()
    {
        Console.WriteLine("Текущие пользователи:");
        foreach (var user in users)
        {
            Console.WriteLine($"Id: {user.Id}, Имя: {user.Name}, Возраст: {user.Age}");
        }
    }
}
class Program
{
    static void Main()
    {
        UserManager manager = new UserManager();

        manager.AddUser(new User { Id = 3, Name = "Павел", Age = 28 });

        manager.UpdateUser(new User { Id = 2, Name = "Анна Петровна", Age = 31 });

        manager.RemoveUser(1);

        manager.ShowAllUsers();
    }
}
