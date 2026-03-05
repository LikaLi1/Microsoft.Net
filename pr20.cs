using System;
using System.Collections.Generic;
using System.Linq;

namespace Proga
{
    internal class Program
    {
        public class Enemy
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public int Health { get; set; }
            public int Attack { get; set; }
            public int Defense { get; set; }
        }

        static void Main(string[] args)
        {
            List<Enemy> enemies = new List<Enemy>
            {
                new Enemy { Name = "Орк", Type = "Обычный", Health = 50, Attack = 10, Defense = 5 },
                new Enemy { Name = "Дракон", Type = "Босс", Health = 200, Attack = 25, Defense = 15 },
                new Enemy { Name = "Гоблин", Type = "Обычный", Health = 30, Attack = 8, Defense = 3 },
                new Enemy { Name = "Леший", Type = "Обычный", Health = 70, Attack = 20, Defense = 6 },
                new Enemy { Name = "Варвар", Type = "Обычный", Health = 80, Attack = 22, Defense = 8 },
                new Enemy { Name = "Ведьма", Type = "Обычный", Health = 60, Attack = 18, Defense = 4 },
                new Enemy { Name = "Джинн", Type = "Босс", Health = 250, Attack = 28, Defense = 12 }
            };

            var bosses = enemies.Where(e => e.Type.Equals("Босс", StringComparison.OrdinalIgnoreCase));
            Console.WriteLine("Боссы:");
            foreach (var boss in bosses)
            {
                Console.WriteLine($"{boss.Name} - Здоровье: {boss.Health}");
            }

            var maxAttackEnemy = enemies.OrderByDescending(e => e.Attack).FirstOrDefault();
            Console.WriteLine($"\nВраг с максимальной атакой: {maxAttackEnemy.Name} (Атака: {maxAttackEnemy.Attack})");

            var groupedByType = enemies.GroupBy(e => e.Type);
            Console.WriteLine("\nВраги по типам:");
            foreach (var group in groupedByType)
            {
                Console.WriteLine($"\nТип: {group.Key}");
                foreach (var enemy in group)
                {
                    Console.WriteLine($"- {enemy.Name}");
                }
            }

            var avgStatsByType = groupedByType.Select(g => new
            {
                Type = g.Key,
                AvgHealth = g.Average(e => e.Health),
                AvgAttack = g.Average(e => e.Attack),
                AvgDefense = g.Average(e => e.Defense)
            });
            Console.WriteLine("\nСредние характеристики по типам:");
            foreach (var stat in avgStatsByType)
            {
                Console.WriteLine($"Тип: {stat.Type}");
                Console.WriteLine($"  Среднее здоровье: {stat.AvgHealth:F2}");
                Console.WriteLine($"  Средняя атака: {stat.AvgAttack:F2}");
                Console.WriteLine($"  Средняя защита: {stat.AvgDefense:F2}");
            }

            double avgDefenseAll = enemies.Average(e => e.Defense);
            var belowAvgDefense = enemies.Where(e => e.Defense < avgDefenseAll);
            Console.WriteLine($"\nСредняя защита всех врагов: {avgDefenseAll:F2}");
            Console.WriteLine("Враги с защитой ниже средней:");
            foreach (var enemy in belowAvgDefense)
            {
                Console.WriteLine($"{enemy.Name} - Защита: {enemy.Defense}");
            }
        }
    }
}
