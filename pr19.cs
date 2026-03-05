using System;
using System.Collections.Generic;
using System.Linq;

namespace Proga
{
    internal class Program
    {
        public class Hero
        {
            public string Name { get; set; }
            public string Class { get; set; }
            public int Level { get; set; }
            public int Health { get; set; }
            public int Attack { get; set; }
            public int Defense { get; set; }
        }

        static void Main(string[] args)
        {
            List<Hero> heroes = new List<Hero>
            {
                new Hero { Name = "Гендальф", Class = "Маг", Level = 10, Health = 80, Attack = 15, Defense = 5 },
                new Hero { Name = "Арагорн", Class = "Воин", Level = 8, Health = 100, Attack = 12, Defense = 10 },
                new Hero { Name = "Люция", Class = "Лучница", Level = 7, Health = 70, Attack = 18, Defense = 3 },
                new Hero { Name = "Боромир", Class = "Воин", Level = 9, Health = 90, Attack = 14, Defense = 8 },
                new Hero { Name = "Гимли", Class = "Воин", Level = 6, Health = 85, Attack = 13, Defense = 7 },
            };

            var mages = heroes.Where(h => h.Class.ToLower() == "маг");
            Console.WriteLine("Герои класса 'Маг':");
            foreach (var hero in mages)
            {
                Console.WriteLine($"{hero.Name} - Уровень: {hero.Level}");
            }

            var heroMaxHealth = heroes.OrderByDescending(h => h.Health).FirstOrDefault();
            Console.WriteLine($"\nГерой с максимальным здоровьем: {heroMaxHealth.Name} (Здоровье: {heroMaxHealth.Health})");

            var groupedByClass = heroes.GroupBy(h => h.Class);
            Console.WriteLine("\nГерои по классам:");
            foreach (var group in groupedByClass)
            {
                Console.WriteLine($"\nКласс: {group.Key}");
                foreach (var hero in group)
                {
                    Console.WriteLine($" - {hero.Name} (Уровень: {hero.Level})");
                }
            }

            var averageStatsByClass = groupedByClass.Select(g => new
            {
                Class = g.Key,
                AvgHealth = g.Average(h => h.Health),
                AvgAttack = g.Average(h => h.Attack),
                AvgDefense = g.Average(h => h.Defense),
                AvgLevel = g.Average(h => h.Level)
            });
            Console.WriteLine("\nСредние характеристики по классам:");
            foreach (var stat in averageStatsByClass)
            {
                Console.WriteLine($"{stat.Class}:");
                Console.WriteLine($"  Среднее здоровье: {stat.AvgHealth:F2}");
                Console.WriteLine($"  Средняя атака: {stat.AvgAttack:F2}");
                Console.WriteLine($"  Средняя защита: {stat.AvgDefense:F2}");
                Console.WriteLine($"  Средний уровень: {stat.AvgLevel:F2}");
            }

            double avgAttackAll = heroes.Average(h => h.Attack);
            var heroesAboveAvgAttack = heroes.Where(h => h.Attack > avgAttackAll);
            Console.WriteLine($"\nСредняя атака всех героев: {avgAttackAll:F2}");
            Console.WriteLine("Герои с атакой выше средней:");
            foreach (var hero in heroesAboveAvgAttack)
            {
                Console.WriteLine($"{hero.Name} - Атака: {hero.Attack}");
            }

            Console.WriteLine("\nЗадание выполнено!");
        }
    }
}
