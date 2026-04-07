using System;
using System.IO;

public class Logger
{
    private readonly string _logDirectory = AppDomain.CurrentDomain.BaseDirectory;
    private string _currentLogFile;
    private const long MaxFileSizeBytes = 1 * 1024 * 1024;
    private int _fileIndex = 0;

    public Logger()
    {
        RotateLogFile();
    }

    public void Info(string message)
    {
        Log("INFO", message);
    }

    public void Warning(string message)
    {
        Log("WARNING", message);
    }

    public void Error(string message)
    {
        Log("ERROR", message);
    }

    private void Log(string level, string message)
    {
        try
        {
            CheckRotate();

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logEntry = $"[{timestamp}] [{level}] {message}";
            File.AppendAllText(_currentLogFile, logEntry + Environment.NewLine);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Logging error: " + ex.Message);
        }
    }

    private void CheckRotate()
    {
        if (File.Exists(_currentLogFile))
        {
            var fileInfo = new FileInfo(_currentLogFile);
            if (fileInfo.Length > MaxFileSizeBytes)
            {
                RotateLogFile();
            }
        }
    }

    private void RotateLogFile()
    {
        _fileIndex++;
        _currentLogFile = Path.Combine(_logDirectory, $"app_{_fileIndex}.log");
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
    private readonly string _filePath = "users.json";
    private List<User> _users;

    public UserManager()
    {
        Load();
    }

    private void Load()
    {
        if (!File.Exists(_filePath))
        {
            _users = new List<User>();
            Save();
            return;
        }

        try
        {
            string json = File.ReadAllText(_filePath);
            _users = JsonSerializer.Deserialize<List<User>>(json);
            if (_users == null)
                _users = new List<User>();
        }
        catch (JsonException)
        {
            Console.WriteLine("Ошибка при чтении JSON файла.");
            _users = new List<User>();
        }
    }

    public void AddUser(User user)
    {
        if (_users.Any(u => u.Id == user.Id))
        {
            Console.WriteLine("Пользователь с таким Id уже существует.");
            return;
        }
        _users.Add(user);
        Save();
    }

    public bool RemoveUser(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            _users.Remove(user);
            Save();
            return true;
        }
        return false;
    }

    public bool UpdateUser(User updatedUser)
    {
        var user = _users.FirstOrDefault(u => u.Id == updatedUser.Id);
        if (user != null)
        {
            user.Name = updatedUser.Name;
            user.Age = updatedUser.Age;
            Save();
            return true;
        }
        return false;
    }

    public List<User> GetAllUsers() => _users;

    private void Save()
    {
        try
        {
            string json = JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при сохранении файла: " + ex.Message);
        }
    }
}


using System;
using System.IO;
using System.Linq;

public class LogAnalyzer
{
    public void Analyze(string logFilePath)
    {
        if (!File.Exists(logFilePath))
        {
            Console.WriteLine("Файл не найден");
            return;
        }

        int infoCount = 0, warningCount = 0, errorCount = 0;
        var messages = new System.Collections.Generic.List<string>();

        foreach (var line in File.ReadLines(logFilePath))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string lowerLine = line.ToLower();

            if (lowerLine.Contains("info"))
                infoCount++;
            else if (lowerLine.Contains("warning"))
                warningCount++;
            else if (lowerLine.Contains("error"))
                errorCount++;

            if (lowerLine.Contains("info"))
                messages.Add("INFO");
            else if (lowerLine.Contains("warning"))
                messages.Add("WARNING");
            else if (lowerLine.Contains("error"))
                messages.Add("ERROR");
        }

        var mostCommon = messages
            .GroupBy(m => m)
            .OrderByDescending(g => g.Count())
            .FirstOrDefault()?.Key ?? "Нет сообщений";

        using var writer = new StreamWriter("report.txt");
        writer.WriteLine($"INFO: {infoCount}");
        writer.WriteLine($"WARNING: {warningCount}");
        writer.WriteLine($"ERROR: {errorCount}");
        writer.WriteLine($"Самый частый: {mostCommon}");
    }
}


using System;
using System.IO;

public class DirectoryBackup
{
    public void Backup(string sourceDir)
    {
        try
        {
            if (!Directory.Exists(sourceDir))
            {
                Console.WriteLine("Исходная папка не существует");
                return;
            }

            string dateStr = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string backupDirName = $"backup_{dateStr}";
            string backupDirPath = Path.Combine(Directory.GetCurrentDirectory(), backupDirName);

            CopyDirectory(sourceDir, backupDirPath);
            Console.WriteLine("Резервное копирование завершено");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при копировании: " + ex.Message);
        }
    }

    private void CopyDirectory(string sourceDir, string targetDir)
    {
        Directory.CreateDirectory(targetDir);

        foreach (string file in Directory.GetFiles(sourceDir))
        {
            string fileName = Path.GetFileName(file);
            string destFile = Path.Combine(targetDir, fileName);
            try
            {
                File.Copy(file, destFile, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка копирования файла {fileName}: {ex.Message}");
            }
        }

        foreach (string dir in Directory.GetDirectories(sourceDir))
        {
            string dirName = Path.GetFileName(dir);
            string destDir = Path.Combine(targetDir, dirName);
            CopyDirectory(dir, destDir);
        }
    }
}


using System;
using System.IO;
using System.Text.Json;
using System.Xml.Linq;

public class JsonToXmlConverter
{
    public void Convert(string jsonFilePath, string xmlFilePath)
    {
        try
        {
            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine("JSON файл не найден");
                return;
            }

            string jsonText = File.ReadAllText(jsonFilePath);
            var jsonDoc = JsonDocument.Parse(jsonText);

            var root = new XElement("Product");

            if (jsonDoc.RootElement.ValueKind == JsonValueKind.Object)
            {
                foreach (var property in jsonDoc.RootElement.EnumerateObject())
                {
                    root.Add(new XElement(property.Name, property.Value.ToString()));
                }
            }

            var doc = new XDocument(root);
            doc.Save(xmlFilePath);
            Console.WriteLine("Конвертация завершена");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }
}


using System;
using System.IO;

public class FolderWatcher
{
    private FileSystemWatcher _watcher;

    public FolderWatcher(string path)
    {
        _watcher = new FileSystemWatcher(path, "*.txt");
        _watcher.Created += OnCreated;
        _watcher.Deleted += OnDeleted;
        _watcher.Changed += OnChanged;
        _watcher.EnableRaisingEvents = true;
    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"Создан файл: {e.Name}");
        LogEvent($"Создан файл: {e.Name}");
    }

    private void OnDeleted(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"Удалён файл: {e.Name}");
        LogEvent($"Удалён файл: {e.Name}");
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"Изменён файл: {e.Name}");
        LogEvent($"Изменён файл: {e.Name}");
    }

    private void LogEvent(string message)
    {
    }
}


using System;
using System.IO;

public class LargeFileProcessor
{
    public void ExtractErrorLines(string filePath, string outputPath)
    {
        try
        {
            using var reader = new StreamReader(filePath);
            using var writer = new StreamWriter(outputPath);

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains("ERROR"))
                {
                    writer.WriteLine(line);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }
}
