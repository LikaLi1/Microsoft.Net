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
                {"A", new List<(string, int)> {("B", 2), ("C", 5)}},
                {"B", new List<(string, int)> {("C", 1)}},
                {"C", new List<(string, int)> {("D", 3)}},
                {"D", new List<(string, int)>()},
            };

            var startNode = "A";
            var distances = Dijkstra(graph, startNode);

            foreach (var kvp in distances)
            {
                Console.WriteLine($"{kvp.Key} = {kvp.Value}");
            }

            Console.ReadKey();
        }

        static Dictionary<string, int> Dijkstra(Dictionary<string, List<(string neighbor, int weight)>> graph, string start)
        {
            var distances = new Dictionary<string, int>();
            var pq = new PriorityQueue<string, int>();

            foreach (var node in graph.Keys)
            {
                distances[node] = int.MaxValue;
            }
            distances[start] = 0;

            pq.Enqueue(start, 0);

            while (pq.Count > 0)
            {
                var currentNode = pq.Dequeue();

                int currentDist = distances[currentNode];

                if (graph.ContainsKey(currentNode))
                {
                    foreach (var (neighbor, weight) in graph[currentNode])
                    {
                        int newDist = currentDist + weight;

                        if (newDist < distances[neighbor])
                        {
                            distances[neighbor] = newDist;
                            pq.Enqueue(neighbor, newDist);
                        }
                    }
                }
            }

            return distances;
        }
    }
}
