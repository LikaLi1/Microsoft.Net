using System;
using System.Collections.Generic;

namespace Граф
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var graph = new Dictionary<string, List<(string neighbor, int weight)>>
            {
                {"A", new List<(string, int)> { "B", "C" }},
                {"B", new List<(string, int)> { "D" }},
                {"C", new List<(string, int)> { }},
                {"D", new List<(string, int)>()},
                {"E", new List<(string, int)>()},
            };

            string start = "A";
            string goal = "F";

            graph["D"] = new List<string> { "F" };
            graph["F"] = new List<string>();

            (bool exists, List<string> path) result = DFS(graph, start, goal);

            if (result.exists)
            {
                Console.WriteLine("The path has been found: " + string.Join(" -> ", result.path));
            }
            else
            {
                Console.WriteLine("Path not found");
            }

            Console.ReadKey();

        }

        static (bool, List<string>) DFS(Dictionary<string, List<string>> graph, string current, string goal, HashSet<string> visited = null, List<string> path = null)
        {
            if (visited == null)
                visited = new HashSet<string>();
            if (path == null)
                path = new List<string>();

            visited.Add(current);
            path.Add(current);

            if (current == goal)
            {
                return (true, new List<string>(path));
            }

            if (graph.ContainsKey(current))
            {
                foreach (var neighbor in graph[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        var (found, resultPath) = DFS(graph, neighbor, goal, visited, path);
                        if (found)
                            return (true, resultPath);
                    }
                }
            }

            path.RemoveAt(path.Count - 1);
            return (false, null);
        }
    }
}
