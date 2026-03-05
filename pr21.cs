using System;
using System.Collections.Generic;
using System.Linq;

namespace Proga
{
    internal class Program
    {
        public class GameSession
        {
            public string Player { get; set; }
            public int Level { get; set; }
            public int Kills { get; set; }
            public int Deaths { get; set; }
            public int TimePlayed { get; set; }
        }

        static void Main(string[] args)
        {
            List<GameSession> sessions = new List<GameSession>
            {
                new GameSession { Player = "Игрок1", Level = 5, Kills = 10, Deaths = 2, TimePlayed = 30 },
                new GameSession { Player = "Игрок2", Level = 3, Kills = 5, Deaths = 1, TimePlayed = 15 },
                new GameSession { Player = "Игрок3", Level = 7, Kills = 20, Deaths = 3, TimePlayed = 45 },
                new GameSession { Player = "Игрок4", Level = 2, Kills = 3, Deaths = 4, TimePlayed = 10 },
                new GameSession { Player = "Игрок5", Level = 10, Kills = 50, Deaths = 5, TimePlayed = 60 },
                new GameSession { Player = "Игрок1", Level = 6, Kills = 15, Deaths = 2, TimePlayed = 40 },
                new GameSession { Player = "Игрок2", Level = 4, Kills = 8, Deaths = 3, TimePlayed = 25 }
            };

            var sessionsPlayer1 = sessions.Where(s => s.Player == "Игрок1");
            Console.WriteLine("Сессии Игрок1:");
            foreach (var s in sessionsPlayer1)
            {
                Console.WriteLine($"Уровень: {s.Level}, Убийств: {s.Kills}, Смертей: {s.Deaths}, Время: {s.TimePlayed} мин");
            }

            var maxKillsSession = sessions.OrderByDescending(s => s.Kills).FirstOrDefault();
            Console.WriteLine($"\nСессия с максимальными убийствами: {maxKillsSession.Player} ({maxKillsSession.Kills} убийств)");

            var groupedByPlayer = sessions.GroupBy(s => s.Player);
            Console.WriteLine("\nСессии по игрокам:");
            foreach (var group in groupedByPlayer)
            {
                Console.WriteLine($"\n{group.Key}:");
                foreach (var s in group)
                {
                    Console.WriteLine($"  Уровень: {s.Level}, Убийств: {s.Kills}, Смертей: {s.Deaths}, Время: {s.TimePlayed} мин");
                }
            }

            var avgStatsPerPlayer = groupedByPlayer.Select(g => new
            {
                Player = g.Key,
                AvgLevel = g.Average(s => s.Level),
                AvgKills = g.Average(s => s.Kills),
                AvgDeaths = g.Average(s => s.Deaths),
                AvgTime = g.Average(s => s.TimePlayed)
            });

            Console.WriteLine("\nСредние показатели по игрокам:");
            foreach (var stat in avgStatsPerPlayer)
            {
                Console.WriteLine($"{stat.Player}:");
                Console.WriteLine($"  Средний уровень: {stat.AvgLevel:F2}");
                Console.WriteLine($"  Средние убийства: {stat.AvgKills:F2}");
                Console.WriteLine($"  Средние смерти: {stat.AvgDeaths:F2}");
                Console.WriteLine($"  Среднее время: {stat.AvgTime:F2} мин");
            }
            
            var highKDA = sessions.Where(s => s.Deaths > 0 && (double)s.Kills / s.Deaths > 2);
            Console.WriteLine("\nСессии с соотношением убийств к смертям выше 2:");
            foreach (var s in highKDA)
            {
                Console.WriteLine($"{s.Player} - Убийств: {s.Kills}, Смертей: {s.Deaths}, КПД: {(double)s.Kills / s.Deaths:F2}");
            }
        }
    }
}
