using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Proga.Proga;

namespace Proga
{
    internal class Proga
    {
        public class Quest
        {
            public string Name { get; set; }
            public int Level { get; set; }
            public int Reward { get; set; }
            public bool IsCompleted { get; set; }
        }

        static void num1()
        {
            List<Quest> quests = new List<Quest>
            { 
            new Quest { Name = "Спасти принцессу", Level = 5, Reward = 100, IsCompleted = false },
            new Quest { Name = "Убить дракона", Level = 10, Reward = 500, IsCompleted = true },
            new Quest { Name = "Найти артефакт", Level = 3, Reward = 50, IsCompleted = true },
            new Quest { Name = "Захватить замок", Level = 8, Reward = 450, IsCompleted = true },
            };

            int playerLevel = 5;
            var availableQuests = quests
               .Where(q => q.Level <= playerLevel)
               .OrderByDescending(q => q.Reward);
            Console.WriteLine($"Доступные квесты для уровня {playerLevel}: ");
            foreach (var quest in availableQuests)
            {
                Console.WriteLine($"- {quest.Name} (Награда: {quest.Reward} опыта)");
            }

            int maxValue = quests.Max(i => i.Reward);
            var ItemMaxValue = quests.First(i => i.Reward == maxValue);
            Console.WriteLine($"Самый дорогой предмет: {ItemMaxValue.Name} с стоимостью {ItemMaxValue.Reward}");
        }
        static void Main(string[] args)
        {
            num1();
            Console.WriteLine();
        }
    }
}
