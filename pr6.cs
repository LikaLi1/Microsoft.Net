using System;
using System.Net.Http.Headers;
using System.Net.Security;

namespace Proga
{
    internal class proga
    {
        void num1()
        {
            List<Hero> heroes = new List<Hero>
            {
                new Hero { Name = "Воин", Level = 3 },
                new Hero { Name = "Маг", Level = 7 },
                new Hero { Name = "Лучник", Level = 5 },
                new Hero { Name = "Рыцарь", Level = 8 },
            };

            var highLevelHeroes = heroes.Where(h => h.Level > 5);
            foreach(var hero in highLevelHeroes)
            {
                Console.WriteLine($"{hero.Name} - Уровень {hero.Level}");
            }
        }
    }
    class hero2
    {
        public string Name { get; set; }
        public string Level { get; set; }
    }
}
