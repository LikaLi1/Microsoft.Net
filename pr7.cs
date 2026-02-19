using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Proga.Proga;

namespace Proga
{
    internal class Proga
    {
        public class Item
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public string Rarity { get; set; }
            public int Value { get; set; }
        }

        static void num1()
        {
            List<Item> items = new List<Item>
            {
                new Item { Name = "Меч", Type = "Оружие", Rarity = "Редкий", Value = 100 },
                new Item { Name = "Щит", Type = "Броня", Rarity = "Обычный", Value = 50 },
                new Item { Name = "Зелье", Type = "Зелье", Rarity = "Редкий", Value = 40 },
                new Item { Name = "Дробик", Type = "Оружие", Rarity = "Эпик", Value = 200 },
            };

            var ItemRarityRare = items.Where(i => i.Rarity == "Редкий");
            foreach (var item in ItemRarityRare)
            {
                Console.WriteLine($"{item.Name} - {item.Rarity}");
            }

            int maxValue = items.Max(i => i.Value);
            var ItemMaxValue = items.First(i => i.Value == maxValue);
            Console.WriteLine($"Самый дорогой предмет: {ItemMaxValue.Name} с стоимостью {ItemMaxValue.Value}");

            var groupedItems = items.GroupBy(i => i.Type);
            foreach (var group in groupedItems)
            {
                Console.WriteLine($"Тип: {group.Key}");
                foreach (var item in group)
                {
                    Console.WriteLine($" - {item.Name}");
                }
            }

            Console.WriteLine("\nСредняя стоимость предметов по типам:");
            foreach (var group in groupedItems)
            {
                double averageValue = group.Average(i => i.Value);
                Console.WriteLine($"{group.Key}: {averageValue:F2}");
            }

            double averageValueAll = items.Average(i => i.Value);
            var itemsAboveAverage = items.Where(i => i.Value > averageValueAll);
            Console.WriteLine($"\nСредняя стоимость всех предметов: {averageValueAll:F2}");
            Console.WriteLine("Предметы с ценой выше средней:");
            foreach (var item in itemsAboveAverage)
            {
                Console.WriteLine($"{item.Name} - {item.Value}");
            }
        }
        static void Main(string[] args)
        {
            num1();
            Console.WriteLine();
        }
    }
}
