using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Proga.Proga;

namespace Proga
{
    internal class Proga
    {
        public class Hero
        {
            public string Name { get; set; }
            public int Level { get; set; }
        }
        public class Item
        {
            public string Name { get; set; }
            public int Price { get; set; }
        }
        public class Quest 
        {
            public string Name { get; set; }
            public int MinLevel { get; set; }
            public int Reward { get; set; }
        }

        static void num1()
        {
            List<Hero> heroes = new List<Hero>
            {
                new Hero { Name = "Воин", Level = 3 },
                new Hero { Name = "Маг", Level = 7 },
                new Hero { Name = "Лучник", Level = 5 },
                new Hero { Name = "Рыцарь", Level = 8 },
            };

            var highLevelHeroes = heroes.Where(h => h.Level > 5);
            foreach (var hero in highLevelHeroes)
            {
                Console.WriteLine($"{hero.Name} - Уровень {hero.Level}");
            }
        }

        static void num2()
        {
            List<Item> items = new List<Item>
            {
                new Item { Name = "Меч", Price = 100 },
                new Item { Name = "Щит", Price = 50 },
                new Item { Name = "Зелье", Price = 20 },
                new Item { Name = "Шлем", Price = 80 },
            };

            var sortedItems = items.OrderByDescending(i => i.Price);
            foreach (var item in sortedItems)
            {
                Console.WriteLine($"{item.Name} - {item.Price} золота");
            }
        }
        static void num3()
        {
            List<Quest> quests = new List<Quest>
            {
                new Quest { Name = "Спасти принцессу", MinLevel = 5, Reward = 100 },
                new Quest { Name = "Убить дракона", MinLevel = 10, Reward = 500 },
                new Quest { Name = "Найти артефакт", MinLevel = 3, Reward = 50 },
            };

            int playerLevel = 7;

            var availableQuests = quests
                .Where(q => q.MinLevel <= playerLevel)
                .OrderByDescending(q => q.Reward);

            Console.WriteLine($"Доступные квесты для уровня {playerLevel}: ");
            foreach (var quest in availableQuests)
            {
                Console.WriteLine($"- {quest.Name} (Награда: {quest.Reward} опыта)");
            }
        }

        static void Main(string[] args)
        {
            num1();
            Console.WriteLine();
            num2();
            Console.WriteLine();
            num3();
        }
    }
}
